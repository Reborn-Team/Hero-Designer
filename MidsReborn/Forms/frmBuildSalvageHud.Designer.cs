﻿
namespace Mids_Reborn.Forms
{
    partial class frmBuildSalvageHud
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pSalvageSummary = new System.Windows.Forms.Panel();
            this.lblCatalysts = new System.Windows.Forms.Label();
            this.lblBoosters = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblEnhObtained = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ibClose = new mrbControls.ImageButton();
            this.pSalvageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pSalvageSummary
            // 
            this.pSalvageSummary.BackColor = System.Drawing.Color.Transparent;
            this.pSalvageSummary.Controls.Add(this.lblCatalysts);
            this.pSalvageSummary.Controls.Add(this.lblBoosters);
            this.pSalvageSummary.Controls.Add(this.pictureBox3);
            this.pSalvageSummary.Controls.Add(this.pictureBox2);
            this.pSalvageSummary.Controls.Add(this.lblEnhObtained);
            this.pSalvageSummary.Controls.Add(this.pictureBox1);
            this.pSalvageSummary.Location = new System.Drawing.Point(0, 0);
            this.pSalvageSummary.Margin = new System.Windows.Forms.Padding(0);
            this.pSalvageSummary.Name = "pSalvageSummary";
            this.pSalvageSummary.Size = new System.Drawing.Size(516, 32);
            this.pSalvageSummary.TabIndex = 18;
            // 
            // lblCatalysts
            // 
            this.lblCatalysts.AutoSize = true;
            this.lblCatalysts.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatalysts.ForeColor = System.Drawing.Color.White;
            this.lblCatalysts.Location = new System.Drawing.Point(260, 8);
            this.lblCatalysts.Name = "lblCatalysts";
            this.lblCatalysts.Size = new System.Drawing.Size(26, 15);
            this.lblCatalysts.TabIndex = 6;
            this.lblCatalysts.Text = "x50";
            // 
            // lblBoosters
            // 
            this.lblBoosters.AutoSize = true;
            this.lblBoosters.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblBoosters.ForeColor = System.Drawing.Color.White;
            this.lblBoosters.Location = new System.Drawing.Point(407, 8);
            this.lblBoosters.Name = "lblBoosters";
            this.lblBoosters.Size = new System.Drawing.Size(26, 15);
            this.lblBoosters.TabIndex = 5;
            this.lblBoosters.Text = "x50";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Mids_Reborn.Resources.EnhancementBooster;
            this.pictureBox3.Location = new System.Drawing.Point(370, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Mids_Reborn.Resources.EnhancementCatalyst;
            this.pictureBox2.Location = new System.Drawing.Point(222, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblEnhObtained
            // 
            this.lblEnhObtained.AutoSize = true;
            this.lblEnhObtained.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnhObtained.ForeColor = System.Drawing.Color.White;
            this.lblEnhObtained.Location = new System.Drawing.Point(36, 8);
            this.lblEnhObtained.Name = "lblEnhObtained";
            this.lblEnhObtained.Size = new System.Drawing.Size(109, 15);
            this.lblEnhObtained.TabIndex = 1;
            this.lblEnhObtained.Text = "Obtained: 100/100";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Mids_Reborn.Resources.AncientScroll;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ibClose
            // 
            this.ibClose.Checked = false;
            this.ibClose.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.ibClose.KnockoutLocationPoint = new System.Drawing.Point(0, 0);
            this.ibClose.Location = new System.Drawing.Point(526, 6);
            this.ibClose.Name = "ibClose";
            this.ibClose.Size = new System.Drawing.Size(105, 22);
            this.ibClose.TabIndex = 19;
            this.ibClose.TextOff = "Exit check mode";
            this.ibClose.TextOn = "Alt Text";
            this.ibClose.Toggle = false;
            this.ibClose.ButtonClicked += new mrbControls.ImageButton.ButtonClickedEventHandler(this.ibClose_ButtonClicked);
            // 
            // frmBuildSalvageHud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 32);
            this.Controls.Add(this.ibClose);
            this.Controls.Add(this.pSalvageSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBuildSalvageHud";
            this.Text = "Build Salvage Status";
            this.pSalvageSummary.ResumeLayout(false);
            this.pSalvageSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSalvageSummary;
        private System.Windows.Forms.Label lblCatalysts;
        private System.Windows.Forms.Label lblBoosters;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblEnhObtained;
        private System.Windows.Forms.PictureBox pictureBox1;
        private mrbControls.ImageButton ibClose;
    }
}