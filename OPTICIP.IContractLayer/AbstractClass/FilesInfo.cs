using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OPTICIP.IContractLayer
{
    public abstract class FilesInfo
    {
        public bool CreateFolder(string paths, String FolderName)
        {
            try
            {
                string fullPath = Path.Combine(paths, FolderName);
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }
    }
}
