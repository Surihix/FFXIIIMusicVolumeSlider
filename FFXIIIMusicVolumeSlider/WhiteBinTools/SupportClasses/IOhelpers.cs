using System.IO;

namespace FFXIIIMusicVolumeSlider.WhiteBinTools.SupportClasses
{
    public static class IOhelpers
    {
        public static void IfDirExistsDel(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}