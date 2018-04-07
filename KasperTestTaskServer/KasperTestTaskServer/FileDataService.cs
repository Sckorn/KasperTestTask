using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KasperTestTaskServer
{
    public class FileDataService : IFileDataService
    {
        public int WriteFileDataToDb(String baseName, String filePath, int fileSize)
        {
            try
            {
                return Globals.DbModel.AddFileRecord(baseName, filePath, fileSize);
            }
            catch (Exception e)
            {
                Globals.Trace("Exception in add file service: " + e.Message);
                return -1;
            }
        }
    }
}
