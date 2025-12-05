//  Copyright (c) 2007 Rocky Lo. All Rights Reserved.
//
//  This file is part of the VNC system.
//
//  The VNC system is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307,
//  USA.
//
// If the source code for the VNC system is not available from the place
// whence you received this file, check http://www.uk.research.att.com/vnc or contact
// the authors on vnc@uk.research.att.com for information on obtaining it.

using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Vnc.Viewer
{
    /// <remarks>
    ///   This class tells users that the viewer is waiting for an incoming connection.
    /// </remarks>
    internal class ListenDlg : DevExpress.XtraEditors.XtraForm
    {
        private const byte Delta = 100; // TODO: Find an optimal value.

        private TcpListener listener = null;
        private Timer timer = new Timer();

        private Label msgLbl = new Label();

        private Button cancelBtn = null;
        private  ToolStripButton cancelItem = null;

        internal ListenDlg(TcpListener listener)
        {
            if (listener == null)
                throw new ArgumentException("listener", "Cannot be null");
            this.listener = listener;

            if (App.DevCap.Lvl >= DevCapLvl.PocketPc)
            {
                cancelBtn = new Button();
                if (App.DevCap.Lvl == DevCapLvl.PocketPc && App.DevCap.Res >= ResLvl.High)
                {
                    cancelBtn.Width *= 2;
                    cancelBtn.Height *= 2;
                }
            }
            else
                cancelItem = new ToolStripButton();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            timer.Enabled = false;
        }

        private void Ticked(object sender, EventArgs e)
        {
            if (!listener.Pending())
                return;
            DialogResult = DialogResult.OK;
        }

        private void Cancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Cancel();
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (UInt32)Keys.Escape)
            {
                e.Handled = true;
                Cancel();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            KeyPressEventHandler keyPressHdr = new KeyPressEventHandler(KeyPressed);

            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = App.GetStr("Waiting for a server...");
            this.MainMenuStrip = new MenuStrip();

            msgLbl.Location = new Point(App.DialogSpacing, App.DialogSpacing);
            msgLbl.Text = App.GetStr("Listening...");
            Graphics graphics = CreateGraphics();
            msgLbl.Size = graphics.MeasureString(msgLbl.Text, Font).ToSize();
            graphics.Dispose();
            Controls.Add(msgLbl);

            if (App.DevCap.Lvl >= DevCapLvl.PocketPc)
            {
                cancelBtn.Location = new Point(App.DialogSpacing, msgLbl.Bottom + App.DialogSpacing);
                cancelBtn.Text = App.GetStr("Cancel");
                cancelBtn.DialogResult = DialogResult.Cancel;
                cancelBtn.KeyPress += keyPressHdr;
                Controls.Add(cancelBtn);
            }
            else
            {
                cancelItem.Text = App.GetStr("Cancel");
                cancelItem.Click += new EventHandler(CancelClicked);
                MainMenuStrip.Items.Add(cancelItem);
            }

            timer.Tick += new EventHandler(Ticked);
            timer.Interval = Delta;
            timer.Enabled = true;
        }
    }
}