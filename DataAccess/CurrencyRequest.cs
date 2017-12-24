using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
   public  class CurrencyRequest
    {
        const string AppID = "37206e97ab5146fd9f30e00dab689f76";
        const int timeout = 5; 

        public string GetCurrency()
        {

            string ResultCurrency =null;
            //var arg = Environment.GetCommandLineArgs()[1];/////
            //if (String.IsNullOrWhiteSpace(arg))
            //    throw new ArgumentNullException("No param!");


            using (HttpClient client = new HttpClient())
            {
                var request = $"https://openexchangerates.org/api/latest.json?app_id={AppID}";

                Uri uri;
                if (!Uri.TryCreate(request, UriKind.Absolute, out uri))
                    throw new InvalidDataException("Could not create uri!");

                var tRequest = client.GetAsync(uri, HttpCompletionOption.ResponseContentRead);

                var timeSpent = 0;
                while (tRequest.Status != TaskStatus.RanToCompletion &&
                    tRequest.Status != TaskStatus.Faulted && timeSpent < timeout)
                {
                    timeSpent++;
                    Thread.Sleep(1000);
                }

                tRequest.ContinueWith((r) =>
                {
                    if (r.Status != TaskStatus.RanToCompletion)
                        throw new InvalidOperationException();

                    var result = r.Result;
                    if (result == null)
                        throw new InvalidOperationException("Can't reach server");

                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        ///
                        /// LOGGER
                        throw new InvalidOperationException("Can't process request");
                    }

                    try
                    {
                        var tProcess = result.Content.ReadAsStreamAsync();
                        tProcess.ContinueWith((rProc) =>
                        {
                            using (var str = rProc.Result)
                            {
                                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(CurrencyRate));
                                var o = s.ReadObject(str) as CurrencyRate;
                                ResultCurrency = o.Rate.ToString();
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                       /// Console.WriteLine($"Exception is {ex}");    LOGGER
                        throw;
                    }
                });
            }
            return ResultCurrency;
        }
    }
}
