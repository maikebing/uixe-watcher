using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.Msg
{
    public class LaneInfo : LaneStatus
    {
        static Dictionary<string, byte[]> _imgcache = new Dictionary<string, byte[]>();
        public LaneInfo() : base()
        {

        }
        public void Parse(string json)
        {
            var ls = JsonConvert.DeserializeObject<LaneStatus>(json, new JsonSerializerSettings() { DateTimeZoneHandling= DateTimeZoneHandling.Local  });
            InheritHelper.FillProperties(this, ls);
        }
        public LaneInfo(string plazaid, string laneid, string laneno, string ipaddress) : base()
        {
            PlazaId = plazaid;
            LaneId = laneid;
            LaneName = laneno;
            IPAddress = ipaddress;
        }
        public string LaneName { get; set; }
        public string PlazaId { get; set; }
        public string LaneId { get; set; }
        public string IPAddress { get; set; }
        public string CameraRtspUrl  { set; get; }  

        public byte[] YuPengDeng => GetImage(Properties.Resources.YuPeng, nameof(YuPengDengStatus), YuPengDengStatus);

        public byte[] JiaoTongDeng => GetImage(Properties.Resources.JiaoTong, nameof(JiaoTongDengStatus), JiaoTongDengStatus);


        public byte[] LanGan => GetImage(Properties.Resources.LanGan, nameof(LanGanStatus), LanGanStatus);



        public byte[] Coil1 => GetImage(Properties.Resources.Car, nameof(Coil1Status), Coil1Status);


        public byte[] Coil2 => GetImage(Properties.Resources.Trans, nameof(Coil2Status), Coil2Status);

        public byte[] Coil3 => GetImage(Properties.Resources.Photo, nameof(Coil3Status), Coil3Status);
        public byte[] Coil4 => GetImage(Properties.Resources.Leave, nameof(Coil4Status), Coil4Status);

        public byte[] Printer => GetImage(Properties.Resources.Printer, nameof(PrinterStatus), PrinterStatus);


        public byte[] Network => GetImage(Properties.Resources.Network1, nameof(NetworkStatus), NetworkStatus);

        public byte[] RSU => GetImage(Properties.Resources.RSU, nameof(RSUStatus), RSUStatus);




        public byte[] Reader => GetImage(Properties.Resources.Reader, nameof(ReaderStatus), ReaderStatus);


        public byte[] Weight => GetImage(Properties.Resources.Weight, nameof(WeightStatus), WeightStatus);

        public byte[] VPR => GetImage(Properties.Resources.CodeRecognition, nameof(VPRStatus), VPRStatus);

        public byte[] Camera => GetImage(Properties.Resources.Camera, nameof(CameraStatus), CameraStatus);

        public byte[] Yellow => GetImage(Properties.Resources.yellow, nameof(YellowStatus), YellowStatus);
        public byte[] QRPay => GetImage(Properties.Resources.QRPayStatus, nameof(QRPayStatus), QRPayStatus);
        public byte[] BaoJing => GetImage(Properties.Resources.BaoJing, nameof(BaoJingStatus), BaoJingStatus);
        public byte[] LWD => GetImage(Properties.Resources.lwd, nameof(LWDStatus), LWDStatus);



        public string CardBox => $"{CarBoxID};{CarBoxMax};{ CarBoxNow};{LaneName?.Substring(0, 1)}";



        public byte[] GetImage(Image img, string name, bool value)
        {
            string keyname = $"{name}{value}";
            if (!_imgcache.ContainsKey(keyname))
            {
                Rectangle cropArea = new Rectangle(value ? 0 : img.Width / 2, 0, img.Width / 2, img.Height);
                using (Bitmap bmpImage = new Bitmap(img))
                {
                    using (Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat))
                    {
                        using (var ms = new System.IO.MemoryStream())
                        {
                            //保存成新文件
                            bmpCrop.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            //释放对象
                            bmpCrop.Dispose();
                            _imgcache.Add(keyname, ms.ToArray());
                        }
                    }
                }
            }
            return _imgcache[keyname];

        }

    }//LaneInfo

}