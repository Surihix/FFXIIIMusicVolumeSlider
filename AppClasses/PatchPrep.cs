using FFXIIIMusicVolumeSlider.AppClasses.VolumeClasses;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFXIIIMusicVolumeSlider.AppClasses
{
    internal class PatchPrep
    {
        public static void PackedMode(string filelistFileVar, string whitePathVar, string langCodeVar, string whiteimgFileVar, int sliderValueVar)
        {
            UnpackBin.FilePaths(filelistFileVar);

            var filelistPathsFile = whitePathVar + "white_data\\sys\\filelist" + langCodeVar + ".win32.txt";

            uint totalFileCount = 0;
            totalFileCount = (uint)File.ReadAllLines(filelistPathsFile).Count();
            totalFileCount--;

            var whiteMusicDir = "";
            switch (langCodeVar)
            {
                case "u":
                    whiteMusicDir = "sound\\pack\\8000\\usa";
                    break;
                case "c":
                    whiteMusicDir = "sound\\pack\\8000";
                    break;
            }

            using (var imgBinPathsReader = new StreamReader(filelistPathsFile))
            {
                using (var imgBin = new FileStream(whiteimgFileVar, FileMode.Open, FileAccess.Write))
                {
                    using (var imgBinWriter = new BinaryWriter(imgBin))
                    {

                        for (int m = 0; m < totalFileCount; m++)
                        {
                            string[] parsedFileLine = imgBinPathsReader.ReadLine().Split(':');
                            var fPos = Convert.ToUInt32(parsedFileLine[0], 16) * 2048;
                            var fPath = parsedFileLine[3];

                            var fDir = Path.GetDirectoryName(fPath);

                            if (fDir.Contains(whiteMusicDir))
                            {
                                var fname = Path.GetFileName(fPath);

                                AdjustVolume.SCD(imgBinWriter, fPos + 168, fname, sliderValueVar);
                            }
                        }
                    }
                }
            }

            CmnMethods.IfFileExistsDel(whitePathVar + "white_data\\sys\\filelist" + langCodeVar + ".win32.txt");

            PatchSucess(sliderValueVar);
        }


        public static void NovaMode(string unpackedMusicDirVar, int sliderValueVar)
        {
            string[] musicDir = Directory.GetFiles(unpackedMusicDirVar, "*.scd", SearchOption.AllDirectories);

            if (musicDir.Length.Equals(0))
            {
                CmnMethods.AppMsgBox("Unpacked music folder is empty.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
                return;
            }

            foreach (var musicFile in musicDir)
            {
                var musicFileName = new FileInfo(musicFile).Name;

                using (var scdFile = new FileStream(musicFile, FileMode.Open, FileAccess.Write))
                {
                    using (var scdWriter = new BinaryWriter(scdFile))
                    {
                        AdjustVolume.SCD(scdWriter, 168, musicFileName, sliderValueVar);
                    }
                }
            }

            PatchSucess(sliderValueVar);
        }


        public static void PatchSucess(int sliderValueVar)
        {
            CmnMethods.AppMsgBox("Music volume is set to level " + sliderValueVar + ".", "Success", MessageBoxIcon.Information);
        }
    }
}