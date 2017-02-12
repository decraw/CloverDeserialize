using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloverDeserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonstr = System.IO.File.ReadAllText("Clover.json");
            var webhook = Newtonsoft.Json.JsonConvert.DeserializeObject<WebHookBody>(jsonstr);
            // Only return the records when no later update
            var flattened = (from merchDict in webhook.merchants
                             from chgLog in merchDict.Value
                             where !merchDict.Value.Any(chgLog2 => chgLog2.objectId == chgLog.objectId && chgLog2.ts > chgLog.ts)
                             select new { merchant = merchDict.Key, change = chgLog }).ToList();

            var grouped = flattened.GroupBy(f => f.merchant).ToDictionary(f => f.Key, f=> f.Select(ch => ch.change).ToList());
            var webHookDeDuped = new WebHookBody { appId = webhook.appId, merchants = grouped };
            var merch = webHookDeDuped.Merchants.ToList();
        }
    }
}
