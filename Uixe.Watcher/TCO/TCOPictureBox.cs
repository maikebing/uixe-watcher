using Cyotek.Windows.Forms;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Uixe.Watcher.Controls
{
    public partial class TCOPictureBox : XtraUserControl
    {
        public TCOPictureBox()
        {
            InitializeComponent();
        }

        private void saveSToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveBmp = new SaveFileDialog();
            SaveBmp.Filter = "图片(*.jpg)|*.jpg";
            if (SaveBmp.ShowDialog() == DialogResult.OK)
            {
                string saveBmpPath = SaveBmp.FileName.Replace(@".jpg", "") + ".jpg";
                try
                {
                    Bitmap bmp = new Bitmap(img);
                    Bitmap bmp2 = new Bitmap(pic.Image.Width, pic.Image.Height);
                    Graphics draw = Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, 0, 0);
                    bmp.Save(saveBmpPath, ImageFormat.Jpeg);
                    bmp.Dispose();//释放bmp文件资源
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("保存失败");
                }
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private void BtnZoomOut30_Click(object sender, EventArgs e)
        {
            ZoomPicture(100);
            ReLoc();
        }

        private void ZoomPicture(int zoom)
        {
            Image.GetThumbnailImageAbort myCallback =
              new Image.GetThumbnailImageAbort(ThumbnailCallback);
            if (img != null)
            {
                Bitmap myBitmap = new Bitmap(img);
                if (zoom == 0) pic.Image = img;

                Image myThumbnail = myBitmap.GetThumbnailImage(
                pic.Image.Width + zoom, pic.Image.Height + zoom, myCallback, IntPtr.Zero);
                Graphics g = this.CreateGraphics();
                g.DrawImage(myThumbnail, 0, 0);
                pic.Image = myThumbnail;
            }
        }

        private void BtnZoomIn30_Click(object sender, EventArgs e)
        {
            ZoomPicture(-100);
            ReLoc();
        }

        private void BtnZoom30_Click(object sender, EventArgs e)
        {
            ZoomPicture(0);
            ReLoc();
        }

        private Point p = new Point(0, 0);
        private bool isdown;

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            isdown = true;
            p = e.Location;
            pic.Cursor = System.Windows.Forms.Cursors.SizeAll;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            int x = e.X - p.X;
            int y = e.Y - p.Y;
            pic.Location = new Point(pic.Location.X + x, pic.Location.Y + y);
            pic.Cursor = Cursors.Default;
            isdown = false;
        }

        private void TCOPictureBox_Load(object sender, EventArgs e)
        {
            pic.Image = null;
            pic.ImageLocation = "";
            ReLoc();
        }

        private void ReLoc()
        {
            if (pic.Height > panel1.Height)
            {
                pic.Top = 0;
            }
            else
            {
                pic.Top = (panel1.Height - pic.Height) / 2;
            }
            if (pic.Width > panel1.Width)
            {
                pic.Left = 0;
            }
            else
            {
                pic.Left = (panel1.Width - pic.Width) / 2;
            }
        }

        private Image img;

        private void pic_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            img = (Image)pic.Image.Clone();
            imageBox1.Image = img;
            ReLoc();
        }

        [DefaultValue(ImageBoxSizeMode.Normal)]
        public ImageBoxSizeMode SizeMode
        {
            get { return imageBox1.SizeMode; }
            set { imageBox1.SizeMode = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ImageLocation
        {
            get { return pic.ImageLocation; }
            set
            {
                pic.ImageLocation = value;
                try
                {
                    pic.LoadAsync();
                }
                catch (Exception)
                {
                }
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Image
        {
            get { return pic.Image; }
            set
            {
                pic.Image = value;
                imageBox1.Image = value;
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            ReLoc();
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdown)
            {
                int x = e.X - p.X;
                int y = e.Y - p.Y;
                pic.Location = new Point(pic.Location.X + x, pic.Location.Y + y);
                pic.Cursor = Cursors.Default;
            }
        }
    }
}