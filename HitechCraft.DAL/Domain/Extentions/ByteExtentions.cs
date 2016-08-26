namespace HitechCraft.DAL.Domain.Extentions
{
    using System.Linq;

    public static class ByteExtentions
    {
        public static bool IsEquals(this byte[] bytes, byte[] otherBytes)
        {
            if (bytes.Length != otherBytes.Length || bytes.Where((t, i) => t != otherBytes[i]).Any())
            {
                return false;
            }

            return true;
        }
    }
}
