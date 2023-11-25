using System.Windows.Forms;

namespace FFXIIIMusicVolumeSlider
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutOKbutton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}