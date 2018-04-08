using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDbConnector;

namespace KasperTestTaskServer.Db
{
    public class FileServiceDataModel : DataModel
    {
        public FileServiceDataModel(String connection_string, String provider_name, int pool_size) 
            : base(connection_string, provider_name, pool_size)
        {

        }

        public int AddFileRecord(String basename, String full_name, double size)
        {
            Dictionary<String, object> parameters = new Dictionary<string, object> {
                { "@base_name", basename },
                { "@full_name", full_name },
                { "@size", size }
            };

            var ret = ExecProcValue("KasperTestTask.f_add_file", parameters);

            return int.Parse(ret);
        }
    }
}
