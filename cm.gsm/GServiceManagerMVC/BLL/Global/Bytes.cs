using System;
using System.IO;

namespace GServiceManagerMVC.BLL.Global
{
    public class Bytes
    {
        public byte[] GetBytesFromFile(string fullFilePath)
        {
            FileStream fs = null;

            try
            {
                fs = File.OpenRead(fullFilePath);

                byte[] bytes = new byte[fs.Length];

                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));

                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }
    }
}