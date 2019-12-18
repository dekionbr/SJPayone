using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SJPayone.Models;

namespace SJPayone.Controllers
{
    public class PayOneBaseController : ControllerBase
    {
        internal readonly IConfiguration _config;

        public PayOneBaseController(IConfiguration config)
        {
            _config = config;
            var settings = new PayOneSettings();
            _config.GetSection("PayOneSettings").Bind(settings);
            payone_settings = settings;
        }

        internal string payone_url => _config.GetValue<string>("PayOneUrl");
        internal object payone_settings { get; set; }

        private void AddRequestData(RestRequest request, params object[] list)
        {
            foreach (object currentObject in list)
            {
                foreach (PropertyInfo p in currentObject.GetType().GetProperties())
                {
                    request.AddParameter(p.Name.ToLower(), Convert.ToString(p.GetValue(currentObject)));
                }
            }
        }

        public string SendRequest(params object[] list)
        {
            try
            {
                var client = new RestClient(payone_url);

                var request = new RestRequest();
                request.Method = Method.POST;
                request.RequestFormat = DataFormat.Json;
                
                AddRequestData(request, list);

                IRestResponse response = client.Execute(request);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:

                        if (response.ContentType.StartsWith("text/html"))
                        {
                            throw new Exception("Recebendo uma HTML no lugar do Json");
                        }

                        break;

                    default:

                        if (response.ResponseStatus == ResponseStatus.Error)
                        {
                            throw response.ErrorException;
                        }
                        break;
                }

                return response.Content;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}