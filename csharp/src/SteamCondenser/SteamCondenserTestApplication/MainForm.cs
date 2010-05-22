using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using SteamCondenser.Steam;
using SteamCondenser.Steam.Servers;

namespace SteamCondenserTestApplication
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private GameServer currentGameServer;

		private void btnGetInfos_Click(object sender, EventArgs e)
		{
			
			// IP einlesen
			IPAddress ipAddr = IPAddress.Parse(textBox1.Text.Split(':')[0]);
			int port = 27015;

			if(textBox1.Text.Split(':').Length > 1)
			{
				port = int.Parse(textBox1.Text.Split(':')[1]);
			}

			if(currentGameServer == null || currentGameServer.IPAddress.ToString() != ipAddr.ToString())
			{
				currentGameServer = GameServer.GetGameServer(ipAddr, port);
			}

			UpdateServerInfos();
		}

		private void UpdateServerInfos()
		{
			if(currentGameServer != null)
			{
				//try
				//{
					currentGameServer.UpdateAllInfos();

					lblIPAddress.Text = string.Format("{0}:{1}", currentGameServer.IPAddress, currentGameServer.Port);
					lblGameDescription.Text = string.Format("{0} ({1}) / {2}", currentGameServer.GameDescription, currentGameServer.GameDirectory, currentGameServer.ApplicationID);
					lblMap.Text = currentGameServer.MapName;
					lblPlayers.Text = string.Format("{0} / {1} ({2} Bots)", currentGameServer.Players, currentGameServer.MaxPlayers, currentGameServer.BotsCount);
					lblServerName.Text = currentGameServer.ServerName;
					lblPing.Text = string.Format("{0:0}, Ø {1:0.0}", currentGameServer.Ping, currentGameServer.PingAverage);
					lblTags.Text = string.Join(", ", currentGameServer.Tags);
					lblType.Text = string.Format("{0}, {1}", currentGameServer.ServerType, currentGameServer.OSType);
					lblVAC.Text = currentGameServer.IsSecure.ToString();
					lblVersion.Text = string.Format("{0} (Protocol {1})", currentGameServer.GameVersion, currentGameServer.ProtocolVersion);

					lvPlayers.Items.Clear();

					for(int i = 0; i < currentGameServer.Players; i++)
					{
						ListViewItem lvItem = new ListViewItem((i+1).ToString());

						if(i < currentGameServer.PlayerList.Count)
						{
							lvItem.SubItems.Add(currentGameServer.PlayerList[i].Name);
							lvItem.SubItems.Add(currentGameServer.PlayerList[i].ConnectTime.ToString());
							lvItem.SubItems.Add(currentGameServer.PlayerList[i].IsBot.ToString());
						}
						else
						{
							lvItem.SubItems.Add("?");
							lvItem.SubItems.Add("");
							lvItem.SubItems.Add("");
						}

						lvPlayers.Items.Add(lvItem);
					}

					lvRules.Items.Clear();

					for(int i = 0; i < currentGameServer.ServerRules.Count; i++)
					{
						ListViewItem lvItem = new ListViewItem((i+1).ToString());
						lvItem.SubItems.Add(currentGameServer.ServerRules[i].CVar);
						lvItem.SubItems.Add(currentGameServer.ServerRules[i].Value);

						lvRules.Items.Add(lvItem);
					}

					btnConnect.Enabled = (currentGameServer.GamePort != 0);
					btnSpectate.Enabled = (currentGameServer.SpectatorPort != 0);

				//}
				//catch(Exception)
				//{
				//    MessageBox.Show("Error: Could not get Server info", Application.ProductName);
				//}
			}
			else
			{
				lblIPAddress.Text = "";
				lblGameDescription.Text = "";
				lblMap.Text = "";
				lblPlayers.Text = "";
				lblServerName.Text = "";
				lblPing.Text = "";
				lblTags.Text = "";
				lblType.Text = "";
				lblVAC.Text = "";
				lblVersion.Text = "";

				lvPlayers.Items.Clear();
				lvRules.Items.Clear();
			}
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

			UpdateServerInfos();
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			if(currentGameServer != null)
			{
				if(currentGameServer.GamePort != 0)
				{
					System.Diagnostics.Process.Start(string.Format("steam://connect/{0}:{1}", currentGameServer.IPAddress, currentGameServer.GamePort));
				}
			}
		}

		private void btnSpectate_Click(object sender, EventArgs e)
		{
			if(currentGameServer != null)
			{
				if(currentGameServer.SpectatorPort != 0)
				{
					System.Diagnostics.Process.Start(string.Format("steam://connect/{0}:{1}", currentGameServer.IPAddress, currentGameServer.SpectatorPort));
				}
			}
		}
	}
}