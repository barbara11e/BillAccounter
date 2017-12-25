using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
   public  class CurrencyRequest
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        const string AppID = "37206e97ab5146fd9f30e00dab689f76";
        const int timeout = 5; 

        public string GetCurrency()
        {

            string ResultCurrency =null;
   
            using (HttpClient client = new HttpClient())
            {
                //адрес запроса для получения текущего курса доллара в рублях
                var request = $"https://openexchangerates.org/api/latest.json?app_id={AppID}";

                Uri uri;
                if (!Uri.TryCreate(request, UriKind.Absolute, out uri))
                    throw new InvalidDataException("Невозможно создание URI.");

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
                        throw new InvalidOperationException("Нет доступа к серверу.");

                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        //REVIEW: Правильно, где логгер?
                        ///
                        /// LOGGER
                        log.Error("Запрос не может быть выполнен.");
                        throw new InvalidOperationException("Запрос не может быть выполнен.");
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
                        //REVIEW: Да-да, логгер
                        /// Console.WriteLine($"Exception is {ex}");    LOGGER
                        log.Error("Десериализация не прошла успешно.");
                        throw;
                    }
                });
            }
            return ResultCurrency;
        }
    }
}
