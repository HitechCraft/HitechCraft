namespace HitechCraft.GameLauncherAPI.Managers
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public static class SkinManager
    {
        public static byte[] ScaleSkinToDefaultMCSize(byte[] imgBytes)
        {
            using (var ms = new MemoryStream(imgBytes))
            {
                var image = Image.FromStream(ms);

                if (image.Width != image.Height)
                {
                    return ScaleImage(64, 32, image);
                }

                return ScaleImage(64, 64, image);
            }
        }


        private static byte[] ScaleImage(int toWidth, int toHeight, Image image)
        {
            var destImage = new Bitmap(toWidth, toHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                graphics.DrawImage(image, new Rectangle(0, 0, toWidth, toHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }

            using (var ms = new MemoryStream())
            {
                destImage.Save(ms, ImageFormat.Png);

                return ms.ToArray();
            }
        }
    }
}