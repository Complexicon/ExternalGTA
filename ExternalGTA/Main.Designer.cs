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
			this.label1.BackColor = System.Drawing.SystemColors.MenuText;
			this.label1.Font = new System.Drawing.Font("Aero Matics", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Complexicon\'s External v1337";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listMenu
			// 
			this.listMenu.BackColor = System.Drawing.Color.DarkRed;
			this.listMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listMenu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listMenu.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listMenu.ForeColor = System.Drawing.SystemColors.Window;
			this.listMenu.FormattingEnabled = true;
			this.listMenu.ItemHeight = 20;
			this.listMenu.Location = new System.Drawing.Point(0, 30);
			this.listMenu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.listMenu.Name = "listMenu";
			this.listMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.listMenu.Size = new System.Drawing.Size(250, 240);
			this.listMenu.TabIndex = 1;
			this.listMenu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
			// 
			// infoBox
			// 
			this.infoBox.AutoSize = true;
			this.infoBox.Font = new System.Drawing.Font("Aero Matics", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoBox.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.infoBox.Location = new System.Drawing.Point(6, 276);
			this.infoBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.infoBox.Name = "infoBox";
			this.infoBox.Size = new System.Drawing.Size(0, 15);
			this.infoBox.TabIndex = 2;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkRed;
			this.ClientSize = new System.Drawing.Size(250, 300);
			this.Controls.Add(this.infoBox);
			this.Controls.Add(this.listMenu);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Main";
			this.Text = "Complexicons External";
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listMenu;
		public System.Windows.Forms.Label infoBox;
	}
}

