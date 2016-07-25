namespace HitechCraft.WebApplication.Manager
{
    #region Using Directives

    using System.IO;
    using System.Web;

    #endregion

    public static class ImageManager
    {
        public static byte[] GetImageBytes(HttpPostedFileBase image)
        {
            byte[] imageData = null;

            if (image != null)
            {
                using (var binaryReader = new BinaryReader(image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(image.ContentLength);
                }
            }

            return imageData;
        }
    }
}