using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;


namespace PSManagement.Service
{
    //Clase para serializar a través de la api
    class Vat
    {
        //Contiene una lista con distintos tipos de porcentajes de impuestos
        public List<VatRates> Rates { get; set; }
    }

    //Clase que contiene la información de un tipo de impuesto
    class VatRates
    {
        //Nombre del impuesto
        public string Name { get; set; }

        //Porcentajes
        public int[] Rates { get; set; }
    }


    /// <summary>
    /// Clase estática para recuperar el iva a través de un servicio.
    /// </summary>
    public static class VatApiService
    {
        /// <summary>
        /// A través de la url del servicio mapea la información y devuelve el tipo de impuesto que se encuentra en la 2ª posición de la lista de impuestos 'Standard'
        /// </summary>
        /// <returns>Devuelve el impuesto 'Standard' perteneciente a España, en cualquier otro caso devuelve un porcentaje del 10%</returns>
        public static int RescatarIva()
        {
            try
            {
                string url = Properties.Settings.Default.UrlApi;
                var json = new WebClient().DownloadString(url);
                Vat vat = JsonConvert.DeserializeObject<Vat>(json);

                return vat.Rates[2].Rates[0];
            }
            catch (Exception)
            {
                return 10;
            }
        }
    }
}
