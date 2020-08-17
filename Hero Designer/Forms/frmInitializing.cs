﻿using System;
using System.Windows.Forms;
using Base.IO_Classes;

namespace Hero_Designer.Forms
{
    public partial class frmInitializing : Form, IMessager
    {
        public frmInitializing()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer, true);
            Load += On_Load;
        }

        private void On_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            Width = BackgroundImage.Width;
            Height = BackgroundImage.Height;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public void SetMessage(string text)
        {
            if (Label1.InvokeRequired)
            {
                void Action() => SetMessage(text);
                Invoke((Action)Action);
            }
            else
            {
                if (Label1.Text == text)
                    return;
                Label1.Text = text;
                Label1.Refresh();
                Refresh();
            }
        }

        void tmrOp_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1.0)
                Opacity += 0.05;
            else
                tmrOp.Enabled = false;
        }
    }
}