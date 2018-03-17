﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Crosshair
{
    public partial class crosshair : Form
    {
        private GlobalKeyboardHook gHook;
        private bool active = true;

        public crosshair()
        {
            InitializeComponent();
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            gHook.HookedKeys.Add(Keys.RShiftKey);
            gHook.hook();
            notifyIcon.Visible = false;
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.RShiftKey)
                return;
            active = !active;
            if (active)
            {
                this.Visible = true;
                center();
                notifyIcon.Visible = false;
            }
            else
            {
                this.Visible = false;
                notifyIcon.Visible = true;
            }
        }

        private void center()
        {
            int w = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Width / 2);
            int h = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Height / 2);
            this.Location = new Point(w, h);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            center();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            exit();
        }

        private void crosshair_FormClosed(object sender, FormClosedEventArgs e)
        {
            exit();
        }

        private void exit()
        {
            gHook.unhook();
            notifyIcon.Visible = false;
            Environment.Exit(0);
        }
    }
}