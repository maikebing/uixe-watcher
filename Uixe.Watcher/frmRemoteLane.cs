using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCS.BOSS.VNC;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher
{
    public partial class frmRemoteLane : DevExpress.XtraEditors.XtraForm
    {
        private readonly LaneInfo _lane;
        private readonly bool _onlyview;

        public frmRemoteLane(LaneInfo lane,bool onlyview=true )
        {
            _lane = lane;
            _onlyview = onlyview;
            InitializeComponent();
        }

        private  async void frmRemoteLane_Load(object sender, EventArgs e)
        {
            var vnc = await VNCUtils.Login(this.panel1, _lane.IPAddress, 5900, "kissme");
            if (vnc != null) vnc.Text = $"{_lane.LaneName}  {_lane.IPAddress} ";
        }
    }
}