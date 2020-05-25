using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManaMockApi
{
    public class WebConfiguration
    {
        public string StorageAccountConnectionString { get; set; }
        public string StorageAccountBaseUrl { get; set; }
        public string ProductContainerName { get; set; }
    }
}
