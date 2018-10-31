namespace ExternalGTA
{
    partial class Main
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
            if (disposing && (components != null))
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
			this.label1 = new System.Windows.Forms.Label();
			this.listMenu = new System.Windows.Forms.ListBox();
			this.infoBox = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.MenuText;
			this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label1.Location = new System.Drawing.Point(9, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(242, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Complexicon\'s External v0.2.1";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// listMenu
			// 
			this.listMenu.BackColor = System.Drawing.Color.DarkRed;
			this.listMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listMenu.Font = new System.Drawing.Font("Franklin Gothic Medium", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listMenu.ForeColor = System.Drawing.SystemColors.Window;
			this.listMenu.FormattingEnabled = true;
			this.listMenu.ItemHeight = 17;
			this.listMenu.Location = new System.Drawing.Point(13, 34);
			this.listMenu.Name = "listMenu";
			this.listMenu.Size = new System.Drawing.Size(234, 257);
			this.listMenu.TabIndex = 1;
			// 
			// infoBox
			// 
			this.infoBox.AutoSize = true;
			this.infoBox.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoBox.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.infoBox.Location = new System.Drawing.Point(12, 294);
			this.infoBox.Name = "infoBox";
			this.infoBox.Size = new System.Drawing.Size(0, 17);
			this.infoBox.TabIndex = 2;
			this.infoBox.Click += new System.EventHandler(this.infoBox_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Maroon;
			this.ClientSize = new System.Drawing.Size(259, 320);
			this.Controls.Add(this.infoBox);
			this.Controls.Add(this.listMenu);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Main";
			this.Text = "Complexicons External";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listMenu;
		public System.Windows.Forms.Label infoBox;
	}
}

