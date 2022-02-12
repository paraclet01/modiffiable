using Microsoft.AspNetCore.Http;
using OPTICIP.API.Application.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class FileReader : IFileReader
    {
        public bool ReadFile(IFormFile file, string Path)
        {
            bool copied = false;
            try
            {
                if(file.Length > 0)
                {
                    using (var fileStream = System.IO.File.Create(Path))
                    {
                        file.CopyTo(fileStream);
                        copied = true;
                        fileStream.Close();
                    }
                }

                return copied;
            }
            catch (Exception ex)
            {
                //                return copied;
                throw ex;
            }
            
        }
    }
}
