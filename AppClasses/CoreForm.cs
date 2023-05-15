using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIIIMusicVolumeSlider.AppClasses
{
    public partial class CoreForm : Form
    {
        public CoreForm()
        {
            InitializeComponent();

            if (!File.Exists("AppHelp.txt"))
            {
                CmnMethods.AppMsgBox("The 'AppHelp.txt' file is missing.\nPlease ensure that this file is present next to the app to use the Help option.", "Warning", MessageBoxIcon.Warning);
            }

            if (!File.Exists("UserSettings.xml"))
            {
                DisableComponents();
                EnVoRadiobutton.Checked = true;
                PackedRadioButton.Checked = true;
                SliderTrackBar.Value = 5;
                SliderTrackBar.Select();

                CmnMethods.AppMsgBox("Please set the path of the 'FFXiiiLauncher.exe' file present in the FINAL FANTASY XIII folder.", "Information", MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    UserSettings loadFromXml = UserSettings.LoadSettings();
                    PathTextBox.Text = loadFromXml.ExePath;
                    if (!File.Exists(PathTextBox.Text + "FFXiiiLauncher.exe"))
                    {
                        CmnMethods.AppMsgBox("Launcher executable file is not present in the location that was set before.\nPlease set the correct path of this file before setting the volume.", "Warning", MessageBoxIcon.Warning);
                        PathTextBox.Text = "";
                    }

                    switch (loadFromXml.VoiceOver)
                    {
                        case "en":
                            EnVoRadiobutton.Checked = true;
                            break;
                        case "jp":
                            JpVoRadiobutton.Checked = true;
                            break;
                        default:
                            CmnMethods.AppMsgBox("Voiceover setting saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            EnVoRadiobutton.Checked = true;
                            break;
                    }

                    switch (loadFromXml.FileSystem)
                    {
                        case "packed":
                            PackedRadioButton.Checked = true;
                            break;
                        case "nova":
                            NovaRadioButton.Checked = true;
                            break;
                        default:
                            CmnMethods.AppMsgBox("FileSystem setting saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            PackedRadioButton.Checked = true;
                            break;
                    }

                    switch (loadFromXml.SliderValue)
                    {
                        case 0:
                            SliderTrackBar.Value = 0;
                            break;
                        case 1:
                            SliderTrackBar.Value = 1;
                            break;
                        case 2:
                            SliderTrackBar.Value = 2;
                            break;
                        case 3:
                            SliderTrackBar.Value = 3;
                            break;
                        case 4:
                            SliderTrackBar.Value = 4;
                            break;
                        case 5:
                            SliderTrackBar.Value = 5;
                            break;
                        case 6:
                            SliderTrackBar.Value = 6;
                            break;
                        case 7:
                            SliderTrackBar.Value = 7;
                            break;
                        case 8:
                            SliderTrackBar.Value = 8;
                            break;
                        case 9:
                            SliderTrackBar.Value = 9;
                            break;
                        case 10:
                            SliderTrackBar.Value = 10;
                            break;
                        default:
                            CmnMethods.AppMsgBox("Slider value saved in xml file is invalid.", "Warning", MessageBoxIcon.Warning);
                            SliderTrackBar.Value = 5;
                            break;
                    }

                    SliderTrackBar.Select();
                }
                catch (Exception)
                {
                    CmnMethods.AppMsgBox("Data entered in UserSettings file is corrupt.\nPlease re configure the settings again", "Warning", MessageBoxIcon.Warning);
                    CmnMethods.IfFileExistsDel("UserSettings.xml");
                    DisableComponents();
                    EnVoRadiobutton.Checked = true;
                    PackedRadioButton.Checked = true;
                    SliderTrackBar.Value = 5;
                    SliderTrackBar.Select();
                }
            }
        }


        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var xiiiLauncherExe = "FFXiiiLauncher.exe";
            OpenFileDialog pathSelect = new OpenFileDialog
            {
                FileName = xiiiLauncherExe,
                Filter = xiiiLauncherExe + $"|{xiiiLauncherExe}",
                RestoreDirectory = true
            };

            if (pathSelect.ShowDialog() == DialogResult.OK)
            {
                EnableComponents();

                var xiiiFilePath = pathSelect.FileName;
                var xiiiFolder = Path.GetDirectoryName(xiiiFilePath);

                PathTextBox.Text = xiiiFolder + "\\";
                SliderTrackBar.Select();
            }
        }


        public void EnableComponents()
        {
            EnVoRadiobutton.Enabled = true;
            JpVoRadiobutton.Enabled = true;
            PackedRadioButton.Enabled = true;
            NovaRadioButton.Enabled = true;
            SliderTrackBar.Enabled = true;
            SetVolumeButton.Enabled = true;
        }


        public void DisableComponents()
        {
            EnVoRadiobutton.Enabled = false;
            JpVoRadiobutton.Enabled = false;
            PackedRadioButton.Enabled = false;
            NovaRadioButton.Enabled = false;
            SliderTrackBar.Enabled = false;
            SetVolumeButton.Enabled = false;
        }


        private void SetVolumeButton_Click(object sender, EventArgs e)
        {
            DisableComponents();

            var whitePath = PathTextBox.Text;

            PathTextBox.ReadOnly = true;

            SetVolumeButton.Text = "Setting...";
            int SliderVal = SliderTrackBar.Value;

            SaveValuesToXml();

            if (PackedRadioButton.Checked.Equals(true))
            {
                var langCode = "";
                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    langCode = "u";
                }
                if (JpVoRadiobutton.Checked.Equals(true))
                {
                    langCode = "c";
                }

                Task.Run(() =>
                {
                    try
                    {
                        var filelistimgfile = whitePath + "white_data\\sys\\filelist" + langCode + ".win32.bin";
                        var whiteimgFile = whitePath + "white_data\\sys\\white_img" + langCode + ".win32.bin";

                        if (File.Exists(filelistimgfile) && File.Exists(whiteimgFile))
                        {
                            try
                            {
                                PatchPrep.PackedMode(filelistimgfile, whitePath, langCode, whiteimgFile, SliderVal);
                            }
                            catch (Exception ex)
                            {
                                CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            CmnMethods.AppMsgBox("Required game files are missing.\nPlease check if the voice over option that you have set in this app is available for your game.", "Error", MessageBoxIcon.Error);
                        }
                    }
                    finally
                    {
                        BeginInvoke(new Action(() => { EnableComponents(); }));
                        SetVolumeButton.BeginInvoke(new Action(() => SetVolumeButton.Text = "Set Volume"));
                        BeginInvoke(new Action(() => { PathTextBox.ReadOnly = false; }));
                        BeginInvoke(new Action(() => { SliderTrackBar.Select(); }));
                    }
                });
            }

            if (NovaRadioButton.Checked.Equals(true))
            {
                var unpackedMusicDir = "";
                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    unpackedMusicDir = PathTextBox.Text + "white_data\\sound\\pack\\8000\\usa";
                }
                else if (JpVoRadiobutton.Checked.Equals(true))
                {
                    unpackedMusicDir = PathTextBox.Text + "white_data\\sound\\pack\\8000";
                    string[] unpackedDirCheck = Directory.GetFiles(unpackedMusicDir, "*.scd", SearchOption.TopDirectoryOnly);
                    if (unpackedDirCheck.Length.Equals(0))
                    {
                        CmnMethods.AppMsgBox("Unable to locate unpacked base game music files.\nOnly the unpacked DLC music files will be patched.", "Warning", MessageBoxIcon.Warning);
                    }
                }

                if (Directory.Exists(unpackedMusicDir))
                {
                    try
                    {
                        PatchPrep.NovaMode(unpackedMusicDir, SliderVal);
                    }
                    catch (Exception ex)
                    {
                        CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                    }
                }
                else
                {
                    CmnMethods.AppMsgBox("Unable to locate the unpacked music folders.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
                }

                EnableComponents();
                SetVolumeButton.Text = "Set Volume";
                PathTextBox.ReadOnly = false;
                SliderTrackBar.Select();
            }
        }


        public void SaveValuesToXml()
        {
            if (!File.Exists(PathTextBox.Text + "FFXiiiLauncher.exe"))
            {
                CmnMethods.AppMsgBox("Unable to locate Launcher executable file.\nPlease set the correct game path.", "Error", MessageBoxIcon.Error);
            }
            else
            {
                UserSettings saveXml = new UserSettings
                {
                    ExePath = PathTextBox.Text
                };

                if (EnVoRadiobutton.Checked.Equals(true))
                {
                    saveXml.VoiceOver = "en";
                }

                if (JpVoRadiobutton.Checked.Equals(true))
                {
                    saveXml.VoiceOver = "jp";
                }

                if (PackedRadioButton.Checked.Equals(true))
                {
                    saveXml.FileSystem = "packed";
                }

                if (NovaRadioButton.Checked.Equals(true))
                {
                    saveXml.FileSystem = "nova";
                }

                int SliderVal = SliderTrackBar.Value;
                switch (SliderVal)
                {
                    case 0:
                        saveXml.SliderValue = 0;
                        break;
                    case 1:
                        saveXml.SliderValue = 1;
                        break;
                    case 2:
                        saveXml.SliderValue = 2;
                        break;
                    case 3:
                        saveXml.SliderValue = 3;
                        break;
                    case 4:
                        saveXml.SliderValue = 4;
                        break;
                    case 5:
                        saveXml.SliderValue = 5;
                        break;
                    case 6:
                        saveXml.SliderValue = 6;
                        break;
                    case 7:
                        saveXml.SliderValue = 7;
                        break;
                    case 8:
                        saveXml.SliderValue = 8;
                        break;
                    case 9:
                        saveXml.SliderValue = 9;
                        break;
                    case 10:
                        saveXml.SliderValue = 10;
                        break;
                }

                saveXml.SaveSettings();
            }
        }


        private void AboutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutForm appAbout = new AboutForm();
            System.Media.SystemSounds.Asterisk.Play();
            appAbout.ShowDialog();
        }


        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("AppHelp.txt"))
            {
                try
                {
                    Process.Start("AppHelp.txt");
                }
                catch (Exception ex)
                {
                    CmnMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                }
            }
            else
            {
                CmnMethods.AppMsgBox("Unable to locate the help text file.\nPlease ensure that this text file is present next to the app before using this option.", "Error", MessageBoxIcon.Error);
            }
        }
    }
}