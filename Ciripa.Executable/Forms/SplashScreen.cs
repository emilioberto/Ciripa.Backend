using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ciripa.Executable.Forms
{
    public partial class SplashScreen : Form
    {
        private const int DefaultWidth = 800;
        private const int DefaultHeight = 600;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SplashScreen
            // 
            BackgroundImage = Image.FromStream(new MemoryStream(Convert.FromBase64String(LogoBase64)));
            Icon = new Icon(new MemoryStream(Convert.FromBase64String(IconBase64)));
            ClientSize = new Size(DefaultWidth, DefaultHeight);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(DefaultWidth, DefaultHeight);
            MinimizeBox = false;
            MinimumSize = new Size(DefaultWidth, DefaultHeight);
            Name = "SplashScreen";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }
    }
}