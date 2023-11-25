using FFXIIIMusicVolumeSlider.VolumeClasses;
using FFXIIIMusicVolumeSlider.WhiteBinTools;
using FFXIIIMusicVolumeSlider.WhiteBinTools.SupportClasses;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFXIIIMusicVolumeSlider
{
    internal class PatchPrep
    {
        public static void PackedMode(string filelistFileVar, string whitePathVar, string langCodeVar, string whiteimgFileVar, int sliderValueVar)
        {
            UnpackTypeC.UnpackFilelistPaths(CmnEnums.GameCodes.ff131, filelistFileVar);

            var filelistPathsFile = whitePathVar + "white_data\\sys\\filelist" + langCodeVar + ".win32.bin.txt";

            uint totalFileCount = (uint)File.ReadAllLines(filelistPathsFile).Count();
            totalFileCount -= 1;

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

            using (var imgPathsReader = new StreamReader(filelistPathsFile))
            {
                using (var imgBin = new FileStream(whiteimgFileVar, FileMode.Open, FileAccess.Write))
                {
                    using (var imgBinWriter = new BinaryWriter(imgBin))
                    {

                        for (int m = 0; m < totalFileCount; m++)
                        {
                            string[] parsedFileLine = imgPathsReader.ReadLine().Split(':');
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

            CmnMethods.IfFileExistsDel(filelistPathsFile);

            PatchSucess(sliderValueVar);
        }


        public static void NovaMode(string unpackedMusicDirVar, int sliderValueVar)
        {
            string[] musicDir = Directory.GetFiles(unpackedMusicDirVar, "*.scd", SearchOption.AllDirectories);

            if (musicDir.Length.Equals(0))
            {
                CmnMethods.AppMsgBox("One or more unpacked music folders are empty.\nPlease unpack the game data correctly with the Nova mod manager and then try setting the volume.", "Error", MessageBoxIcon.Error);
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