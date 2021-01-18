using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCloud.YoloCreatorDataTrain
{
    public static class ImageHelper
    {
        public static System.Drawing.Imaging.ImageFormat GetImageFormat(string fileName, out string extension)
        {
            extension = Path.GetExtension(fileName);

            switch (extension.ToLower())
            {
                case @".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;

                case @".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;

                case @".ico":
                    return System.Drawing.Imaging.ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;

                case @".png":
                    return System.Drawing.Imaging.ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return System.Drawing.Imaging.ImageFormat.Tiff;

                case @".wmf":
                    return System.Drawing.Imaging.ImageFormat.Wmf;

                default:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
        }

        public static Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            return src.Clone(new Rectangle(x, y, width, height), src.PixelFormat);
        }
        public static bool IsImageFile(string tempF)
        {
            var extension = Path.GetExtension(tempF);

            switch (extension.ToLower())
            {
                case @".bmp":
                case @".gif":
                case @".ico":
                case @".jpg":
                case @".jpeg":
                case @".png":
                case @".tif":
                case @".tiff":
                case @".wmf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
