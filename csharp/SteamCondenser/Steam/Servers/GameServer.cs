/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Sockets;
using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Servers
{
	public abstract class GameServer
	{
		private const int REQUEST_CHALLENGE = 0;
		private const int REQUEST_INFO = 1;
		private const int REQUEST_PLAYER = 2;
		private const int REQUEST_RULES = 3;

		
		protected uint challengeNumber = 0xFFFFFFFF;
		protected int ping;
		protected ServerQuerySocket querySocket;
		protected List<SteamPlayer> playerList;
		protected List<ServerRule> serverRules = new List<ServerRule>();
		protected ServerInfo serverInfo = null;
		
		protected GameServer(int portNumber)
		{
			if (portNumber < 0 || portNumber > 65535) {
				throw new ArgumentException("The listening port of the server has to be a number greater than 0 and less than 65535.");
			}
		}
		
		public int Ping  { 
			get {
				if (ping == 0) UpdatePing();
				return ping;
			}
			protected set { ping = value; }
		}
		

		public IList<SteamPlayer> PlayerList {
			get {
				if (playerList == null) UpdatePlayerInfo();
				return playerList.AsReadOnly(); 
			}
		}
		
		public IList<ServerRule> ServerRules {
			get {
				if (this.serverRules == null) UpdateRulesInfo();
				return serverRules.AsReadOnly();
			}
		}
		
		public ServerInfo ServerInfo {
			get { 
				if (this.serverInfo == null) this.UpdateServerInfo();
				return serverInfo; 
			}
		}
		
		private void SendRequest(SteamPacket requestData)
		{
			this.querySocket.Send(requestData);
		}

		private SteamPacket GetReply()
		{
			return this.querySocket.GetReply();
		}		
		
		public void UpdatePing()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2A_PING));
			DateTime pingStart = DateTime.Now;
			this.querySocket.GetReply();
			ping = DateTime.Now.Subtract(pingStart).Milliseconds;
		}
		
		public void UpdatePlayerInfo()
		{
			this.UpdatePlayerInfo(null);
		}
		
		public void UpdatePlayerInfo(string rconPassword)
		{
			handleResponseForRequest(GameServer.REQUEST_PLAYER);
		}
		
		public void UpdateRulesInfo()
		{
			this.UpdateRulesInfo(null);
		}
		public void UpdateServerInfo()
		{
			handleResponseForRequest(GameServer.REQUEST_INFO);
		}
		
		public void UpdateRulesInfo(string rconPassword)
		{
			handleResponseForRequest(GameServer.REQUEST_RULES);
		}
		public void UpdateChallengeNumber()
		{
			this.handleResponseForRequest(GameServer.REQUEST_CHALLENGE);
		}
		
		private void handleResponseForRequest(int requestType)
		{
			handleResponseForRequest(requestType, true);
		}
		
		private void handleResponseForRequest(int requestType, bool repeatOnFailure)
		{
			bool success = false;
			SteamPacketTypes expectedResponse;
			SteamPacket requestPacket = null;
			switch (requestType) 
			{
			case GameServer.REQUEST_CHALLENGE:
				expectedResponse = SteamPacketTypes.S2C_CHALLENGE;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_SERVERQUERY_GETCHALLENGE);
				break;
			case GameServer.REQUEST_INFO:
				expectedResponse = SteamPacketTypes.S2A_INFO;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_INFO);
				break;
			case GameServer.REQUEST_PLAYER:
				expectedResponse = SteamPacketTypes.S2A_PLAYER;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_PLAYER, challengeNumber.ReverseBytes().GetBytes());
				break;
			case GameServer.REQUEST_RULES:
				expectedResponse = SteamPacketTypes.S2A_RULES;
				requestPacket = new SteamPacket(SteamPacketTypes.A2S_RULES, challengeNumber.ReverseBytes().GetBytes());
				break;
			default:
				throw new SteamCondenserException("Called with wrong request type.");
			}
			
			this.SendRequest(requestPacket);
			
			success = false;
			do
			{
				SteamPacket responsePacket = this.GetReply();
				success = (responsePacket.PacketType == expectedResponse);
				
				if (responsePacket is SourceServerInfoResponsePacket) {
					success = true;
					this.serverInfo = (responsePacket as SourceServerInfoResponsePacket).ServerInfo;
				} else if (responsePacket is PlayersResponsePacket) {
					playerList = (responsePacket as PlayersResponsePacket).PlayerList;
				} else if (responsePacket is ChallengeResponsePacket) {
					this.challengeNumber = (uint)(responsePacket as ChallengeResponsePacket).ChallengeID;
				} else if (responsePacket is ServerRulesResponsePacket) {
					this.serverRules = (responsePacket as ServerRulesResponsePacket).ServerRules;
				}
				
			} while (repeatOnFailure && !success);
		}
	}
}
