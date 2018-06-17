using System.IO;

namespace NettyBase.Utils
{
    class FileEssentials
    {
        public static void Write(string path, string text)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.Write(text);
                writer.Close();
            }
        }
    }
}
