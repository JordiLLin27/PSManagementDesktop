using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.Service
{
    class Vat
    {
        public List<VatRates> Rates { get; set; }

    }

    class VatRates
    {
        public string Name { get; set; }
        public int[] Rates { get; set; }
    }

    public static class VatApiService
    {
        public static int RescatarIva()
        {
            int[] rate = new int[1];
            string url = @"http://api.vatlookup.eu/rates/es";
            var json = new WebClient().DownloadString(url);
            Vat vat = JsonConvert.DeserializeObject<Vat>(json);


            return vat.Rates[2].Rates[0];
        }
    }
}
