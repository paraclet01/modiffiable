using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.IContractLayer
{
    public interface IFileHandler
    {
        
        bool CreateDataFile(List<String> data, String Path);
    }
}
