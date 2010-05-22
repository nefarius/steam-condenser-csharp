namespace SteamCondenserTestApplication
{
	partial class MainForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnGetInfo = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblTags = new System.Windows.Forms.Label();
			this.lblVAC = new System.Windows.Forms.Label();
			this.lblType = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblMap = new System.Windows.Forms.Label();
			this.lblPlayers = new System.Windows.Forms.Label();
			this.lblGameDescription = new System.Windows.Forms.Label();
			this.lblPing = new System.Windows.Forms.Label();
			this.lblIPAddress = new System.Windows.Forms.Label();
			this.lblServerName = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lvPlayers = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lvRules = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnSpectate = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGetInfo
			// 
			this.btnGetInfo.Location = new System.Drawing.Point(242, 12);
			this.btnGetInfo.Name = "btnGetInfo";
			this.btnGetInfo.Size = new System.Drawing.Size(118, 36);
			this.btnGetInfo.TabIndex = 0;
			this.btnGetInfo.Text = "GET INFOS";
			this.btnGetInfo.UseVisualStyleBackColor = true;
			this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfos_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(14, 28);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(204, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "87.106.215.177:27017";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(11, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "IP:Port";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.lblTags);
			this.groupBox1.Controls.Add(this.lblVAC);
			this.groupBox1.Controls.Add(this.lblType);
			this.groupBox1.Controls.Add(this.lblVersion);
			this.groupBox1.Controls.Add(this.lblMap);
			this.groupBox1.Controls.Add(this.lblPlayers);
			this.groupBox1.Controls.Add(this.lblGameDescription);
			this.groupBox1.Controls.Add(this.lblPing);
			this.groupBox1.Controls.Add(this.lblIPAddress);
			this.groupBox1.Controls.Add(this.lblServerName);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(14, 83);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(285, 453);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ServerInfos";
			// 
			// lblTags
			// 
			this.lblTags.AutoSize = true;
			this.lblTags.Location = new System.Drawing.Point(62, 251);
			this.lblTags.Name = "lblTags";
			this.lblTags.Size = new System.Drawing.Size(41, 13);
			this.lblTags.TabIndex = 4;
			this.lblTags.Text = "lblTags";
			// 
			// lblVAC
			// 
			this.lblVAC.AutoSize = true;
			this.lblVAC.Location = new System.Drawing.Point(62, 224);
			this.lblVAC.Name = "lblVAC";
			this.lblVAC.Size = new System.Drawing.Size(38, 13);
			this.lblVAC.TabIndex = 4;
			this.lblVAC.Text = "lblVAC";
			// 
			// lblType
			// 
			this.lblType.AutoSize = true;
			this.lblType.Location = new System.Drawing.Point(62, 195);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(41, 13);
			this.lblType.TabIndex = 4;
			this.lblType.Text = "lblType";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(62, 167);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(52, 13);
			this.lblVersion.TabIndex = 4;
			this.lblVersion.Text = "lblVersion";
			// 
			// lblMap
			// 
			this.lblMap.AutoSize = true;
			this.lblMap.Location = new System.Drawing.Point(62, 139);
			this.lblMap.Name = "lblMap";
			this.lblMap.Size = new System.Drawing.Size(38, 13);
			this.lblMap.TabIndex = 4;
			this.lblMap.Text = "lblMap";
			// 
			// lblPlayers
			// 
			this.lblPlayers.AutoSize = true;
			this.lblPlayers.Location = new System.Drawing.Point(62, 109);
			this.lblPlayers.Name = "lblPlayers";
			this.lblPlayers.Size = new System.Drawing.Size(51, 13);
			this.lblPlayers.TabIndex = 4;
			this.lblPlayers.Text = "lblPlayers";
			// 
			// lblGameDescription
			// 
			this.lblGameDescription.AutoSize = true;
			this.lblGameDescription.Location = new System.Drawing.Point(62, 81);
			this.lblGameDescription.Name = "lblGameDescription";
			this.lblGameDescription.Size = new System.Drawing.Size(98, 13);
			this.lblGameDescription.TabIndex = 4;
			this.lblGameDescription.Text = "lblGameDescription";
			// 
			// lblPing
			// 
			this.lblPing.AutoSize = true;
			this.lblPing.Location = new System.Drawing.Point(62, 279);
			this.lblPing.Name = "lblPing";
			this.lblPing.Size = new System.Drawing.Size(38, 13);
			this.lblPing.TabIndex = 4;
			this.lblPing.Text = "lblPing";
			// 
			// lblIPAddress
			// 
			this.lblIPAddress.AutoSize = true;
			this.lblIPAddress.Location = new System.Drawing.Point(62, 54);
			this.lblIPAddress.Name = "lblIPAddress";
			this.lblIPAddress.Size = new System.Drawing.Size(65, 13);
			this.lblIPAddress.TabIndex = 4;
			this.lblIPAddress.Text = "lblIPAddress";
			// 
			// lblServerName
			// 
			this.lblServerName.AutoSize = true;
			this.lblServerName.Location = new System.Drawing.Point(62, 27);
			this.lblServerName.Name = "lblServerName";
			this.lblServerName.Size = new System.Drawing.Size(76, 13);
			this.lblServerName.TabIndex = 4;
			this.lblServerName.Text = "lblServerName";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label11.Location = new System.Drawing.Point(6, 279);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(36, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "Ping:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label10.Location = new System.Drawing.Point(6, 251);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 13);
			this.label10.TabIndex = 3;
			this.label10.Text = "Tags:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(6, 224);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 13);
			this.label9.TabIndex = 3;
			this.label9.Text = "VAC:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label8.Location = new System.Drawing.Point(6, 195);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(39, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Type:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(6, 167);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "Version:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(6, 139);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Map:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(6, 109);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Players:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(6, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Game:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(6, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "IP:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(6, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Name:";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.lvPlayers);
			this.groupBox2.Location = new System.Drawing.Point(305, 83);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(310, 454);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Players";
			// 
			// lvPlayers
			// 
			this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lvPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPlayers.FullRowSelect = true;
			this.lvPlayers.Location = new System.Drawing.Point(3, 16);
			this.lvPlayers.Name = "lvPlayers";
			this.lvPlayers.Size = new System.Drawing.Size(304, 435);
			this.lvPlayers.TabIndex = 0;
			this.lvPlayers.UseCompatibleStateImageBehavior = false;
			this.lvPlayers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "#";
			this.columnHeader1.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Time";
			this.columnHeader3.Width = 70;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Bot";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.lvRules);
			this.groupBox3.Location = new System.Drawing.Point(621, 83);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(294, 454);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Rules";
			// 
			// lvRules
			// 
			this.lvRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.lvRules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRules.FullRowSelect = true;
			this.lvRules.Location = new System.Drawing.Point(3, 16);
			this.lvRules.Name = "lvRules";
			this.lvRules.Size = new System.Drawing.Size(288, 435);
			this.lvRules.TabIndex = 0;
			this.lvRules.UseCompatibleStateImageBehavior = false;
			this.lvRules.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "#";
			this.columnHeader5.Width = 30;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "CVar";
			this.columnHeader6.Width = 119;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Value";
			this.columnHeader7.Width = 86;
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Enabled = false;
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConnect.Location = new System.Drawing.Point(796, 12);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(119, 35);
			this.btnConnect.TabIndex = 7;
			this.btnConnect.Text = "CONNECT";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnSpectate
			// 
			this.btnSpectate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSpectate.Enabled = false;
			this.btnSpectate.Location = new System.Drawing.Point(671, 12);
			this.btnSpectate.Name = "btnSpectate";
			this.btnSpectate.Size = new System.Drawing.Size(119, 35);
			this.btnSpectate.TabIndex = 7;
			this.btnSpectate.Text = "SPECTATE";
			this.btnSpectate.UseVisualStyleBackColor = true;
			this.btnSpectate.Click += new System.EventHandler(this.btnSpectate_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnGetInfo;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(927, 548);
			this.Controls.Add(this.btnSpectate);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnGetInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Steam Condensor ExampleApp";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGetInfo;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblTags;
		private System.Windows.Forms.Label lblVAC;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblMap;
		private System.Windows.Forms.Label lblPlayers;
		private System.Windows.Forms.Label lblGameDescription;
		private System.Windows.Forms.Label lblPing;
		private System.Windows.Forms.Label lblIPAddress;
		private System.Windows.Forms.Label lblServerName;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListView lvPlayers;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView lvRules;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnSpectate;
	}
}

