using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloverDeserialize

{
    public class WebHookBody
    {
        public string appId { get; set; }
        public Dictionary<string, List<ChangeLog>> merchants { get; set; }

        public IEnumerable<Merchant> Merchants
        {
            get
            {
                return from x in merchants
                        select new Merchant { Name = x.Key, Changes = x.Value };
            }
        }
    }
    public class ChangeLog
    {
        public string objectId { get; set; }
        public string type { get; set; }
        public long ts { get; set; }

        public string ObjectType
        {
            get
            {
                return objectId.Split(':').First();
            }

        }
        public string ObjectKey
        {
            get
            {
                return objectId.Split(':').Last();
            }
        }
    }
    public class Merchant
    {
        public string Name { get; set; }
        public List<ChangeLog> Changes { get; set; }
    }
}
