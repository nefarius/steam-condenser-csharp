/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Packets.RCON;

namespace SteamCondenser.Steam.Sockets
{
	public class GoldSrcSocket : ServerQuerySocket
	{
		
		private bool isHLTV;
		private long rconChallenge = -1;
		
		public GoldSrcSocket(IPAddress ipAddress, int portNumber)
			: this(ipAddress, portNumber, false)
		{
		}

		public GoldSrcSocket(IPAddress ipAddress, int portNumber, bool isHLTV)
			: base(ipAddress, portNumber)
		{
			this.isHLTV = isHLTV;
		}
		
		public override SteamPacket GetReply()
		{
			int bytesRead;
			SteamPacket packet;
			bytesRead = this.ReceivePacket(SteamPacket.PACKET_SIZE);
			
			if (this.PacketIsSplit()) {
				byte[] splitData;
				int packetCount, packetNumber;
				int requestId;
				byte packetNumberAndCount;
				List<byte[]> splitPackets = new List<byte[]>();
				
				do { 
					requestId = this.bufferReader.ReadInt32().ReverseBytes();
					packetNumberAndCount = this.bufferReader.ReadByte();
					packetCount = packetNumberAndCount & 0xF;
					packetNumber = (packetNumberAndCount & 0xF0) >> 4;
					
					// read preamble only in the first packet, otherwise we cut off stuff
					if (packetNumber == 0) this.bufferReader.ReadInt32();
					
					splitData = this.bufferReader.ReadBytes((int)(this.bufferReader.BaseStream.Length - this.bufferReader.BaseStream.Position));
					
					splitPackets.Add(splitData);
					if (splitPackets.Count < packetCount)
					{
						try {
							bytesRead = this.ReceivePacket();
						}
						catch {
							bytesRead = 0;
						}
					}
					else { bytesRead = 0; }
					// TODO: use packetNumber nad packetCount
				} while (bytesRead > 0 && this.PacketIsSplit());
				

				packet = SteamPacket.ReassemblePacket(splitPackets);
			}
			else {
				packet = this.CreatePacket();
			}
			
			return packet;
		}
		
		public string RconExec(string password, string command)
		{
			if (this.rconChallenge == -1 || this.isHLTV) {
				this.RconGetChallenge();
			}
			
			RconSend("rcon " + rconChallenge +  " " + password + " " + command);
			string response;
			if (this.isHLTV) {
				try { 
					SteamPacket packet = this.GetReply();
					if (packet == null)
					{
						throw new PacketFormatException();
					}
					response = (packet as RCONGoldSrcResponsePacket).Response;
				}
				catch { response = ""; }
			} else {
				response = ((RCONGoldSrcResponsePacket)this.GetReply()).Response;
			}
			
			if (response.StartsWith("Bad rcon_password.") || response.StartsWith("You have been banned from this server")) {
				throw new RCONNoAuthException(response);
			}
			
			try { 
				do { 
					RCONGoldSrcResponsePacket packet = this.GetReply() as RCONGoldSrcResponsePacket;
					if (packet == null) {
						throw new PacketFormatException();
					}
					response += packet.Response;
				} while (true);
			} catch { }

			return response;
		}
		
		public void RconGetChallenge()
		{
			this.RconSend("challenge rcon");
			SteamPacket steamPacket = this.GetReply();
			RCONGoldSrcResponsePacket responsePacket = steamPacket as RCONGoldSrcResponsePacket;
			
			if (responsePacket == null)
				throw new PacketFormatException();
			string response = responsePacket.Response;

			if (response.Equals("You have been banned from this server.")) {
				throw new RCONNoAuthException(response);
			} else if (response.Equals("You have been banned from this server")) {
				throw new RCONBanException(response);
			}
			
			int startIndex = 14;
			int i = startIndex;
			while (i < response.Length && response[i] >= '0' && response[i] <= '9') {
				i++;
			}
			
			this.rconChallenge = Convert.ToInt64(response.Substring(startIndex, i - startIndex));
		}
		
		private void RconSend(string command, params object[] obj)
		{
			RconSend(command, String.Format(command, obj));
		}
		
		private void RconSend(string command)
		{
			this.Send(new RCONGoldSrcRequestPacket(command));
		}

	}
}
