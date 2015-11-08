using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using RammScreen.Properties;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace RammScreen
{
    public partial class MainForm : Form
    {
        string webpath = "http://www.rammscreen.ga/screenshot/";
        string ftppath = "/web/screenshots/guest/";
        string imgname;
        string imgpath;
        Brush brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0));
        //These variables control the mouse position
        int selectX;
        int selectY;
        int selectWidth;
        int selectHeight;
        public Pen selectPen;

        //This variable control when you start the right click
        bool start = false;

        public MainForm()
        {
            InitializeComponent();
            Top = 0;
            Left = 0;
        }

        private void Save()
        {
            //validate if something selected
            if (selectWidth > 0)
            {
                imgname = DateTime.Now.ToString("yyyy_MM_dd_H_mm_ss") + "_RammScreen.png";
                imgpath = Path.Combine(Path.GetTempPath(), imgname);
                Rectangle rect = new Rectangle(selectX, selectY, selectWidth, selectHeight);
                //create bitmap with original dimensions
                Bitmap OriginalImage = new Bitmap(captureBox.Image, captureBox.Width, captureBox.Height);
                //create bitmap with selected dimensions
                Bitmap _img = new Bitmap(selectWidth, selectHeight);
                //create graphic variable
                Graphics g = Graphics.FromImage(_img);
                //set graphic attributes
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
                //insert image stream into clipboard
                Clipboard.SetText(webpath + imgname);
                //Store temp image & upload
                try
                {
                    _img.Save(imgpath, ImageFormat.Png);
                    //upload image to ftp server
                    FTP ftpClient = new FTP(Settings.Default.FTPHost, Settings.Default.FTPUser, Settings.Default.FTPPass);
                    if (ftpClient.Upload(ftppath + imgname, imgpath))
                    {
                        Process.Start(webpath + imgname);
                    }
                    File.Delete(imgpath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //End application
            Application.Exit();
        }

        private void captureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics gg = captureBox.CreateGraphics();
            //validate if there is an image
            if (captureBox.Image == null)
                return;
            //validate if right-click was trigger
            if (start)
            {
                //refresh picture box
                captureBox.Refresh();
                //set corner square to mouse coordinates
                Rectangle rec = new Rectangle();
                selectWidth = e.X - selectX;
                selectHeight = e.Y - selectY;
                rec.X = selectX;
                rec.Y = selectY;
                rec.Width = selectWidth;
                rec.Height = selectHeight;
                gg.FillRectangle(brush, rec);
                gg.DrawRectangle(selectPen, rec);
                captureBox.CreateGraphics().DrawString(selectWidth.ToString() + "\n" + selectHeight.ToString(), Font, Brushes.WhiteSmoke, selectX, selectY);
                //draw dotted rectangle
            }
        }

        private void captureBox_MouseDownUp(object sender, MouseEventArgs e)
        {
            //validate when user right-click
            if (!start)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //starts coordinates for rectangle
                    selectX = e.X;
                    selectY = e.Y;
                    selectPen = new Pen(brush, 1);
                    selectPen.DashStyle = DashStyle.Solid;
                }
                //refresh picture box
                captureBox.Refresh();
                //start control variable for draw rectangle
                start = true;
            }
            else
            {
                //validate if there is image
                if (captureBox.Image == null)
                    return;
                //same functionality when mouse is over
                if (e.Button == MouseButtons.Left)
                {
                    captureBox.Refresh();
                    selectWidth = e.X - selectX;
                    selectHeight = e.Y - selectY;
                    captureBox.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);

                }
                start = false;
                //function save image to clipboard
                Save();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Hide the Form
            Hide();
            //Create the Bitmap
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Create the Graphic Variable with screen Dimensions
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            //Copy Image from the screen
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            //Create a temporal memory stream for the image
            using (MemoryStream s = new MemoryStream())
            {
                //save graphic variable into memory
                printscreen.Save(s, ImageFormat.Bmp);
                captureBox.Size = new Size(Width, Height);
                //set the picture box with temporary stream
                captureBox.Image = Image.FromStream(s);
            }
            //Show Form
            Show();
            //Cross Cursor
            Cursor = Cursors.Cross;
        }
    }
}
