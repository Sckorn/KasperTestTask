using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace KasperTestTaskServer
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFileDataService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IFileDataService
    {
        [OperationContract]
        [WebGet]
        int WriteFileDataToDb(String baseName, String filePath, int fileSize);
    }
}
