using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace InventoryManagmentMobile.Services
{
    public class ApiService<T> where T : class, new()
    {
        // private CobrosContext _context;
        public string UrlToken { get; set; }
        public ApiService()
        {
            //_context = context;
            UrlToken = Preferences.Get("token", "Default Value");
        }


        private async Task<List<T1>> GetListData<T1>(string v)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.Timeout = TimeSpan.FromSeconds(60);
                    //client.BaseAddress = new Uri(connection); //http://extranet.brugal.com.do:8026
                    //client.BaseAddress = new Uri(_context.Configs.FirstOrDefault().Url);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");


                    var url1 = v;
                    var result = await client.GetAsync(url1);
                    // string empr = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<T1>>(content);
                        return Items;


                    }
                    else
                    {
                        return new List<T1>();
                    }
                    throw new Exception(result.ReasonPhrase);

                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetListData(string UrlBase, string Url)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.Timeout = TimeSpan.FromSeconds(60);
                    //client.BaseAddress = new Uri(connection); //http://extranet.brugal.com.do:8026
                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                    if (!String.IsNullOrEmpty(UrlToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UrlToken);
                    }
                    var url1 = Url;
                    var result = await client.GetAsync(url1);
                    // string empr = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<T>>(content);
                        return Items;


                    }
                    else
                    {
                        return new List<T>();
                    }
                    throw new Exception(result.ReasonPhrase);

                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<T> GetData(string UrlBase, string Url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(660);
                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                    var token = Preferences.Get("token", string.Empty);

                    if (!String.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    
                    var result = await client.GetAsync(Url);

                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    
                    throw new Exception("Se produjo el siguiente error al tratar al ejecutar la consulta al server: " + result.ReasonPhrase);   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<T> PostData(string UrlBase, string Url, List<T> Ltrans)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                    var token = Preferences.Get("token", string.Empty);

                    if (!String.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var data = JsonConvert.SerializeObject(Ltrans);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(Url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return  JsonConvert.DeserializeObject<T>(responseData);
                    }

                    throw new Exception("Se produjo el siguiente error al tratar de enviar la información al server: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<T> PostData(string UrlBase, string Url, object Ltrans)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                    var token = Preferences.Get("token", string.Empty);

                    if (!String.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var data = JsonConvert.SerializeObject(Ltrans);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(Url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(responseData);
                    }

                    throw new Exception("Se produjo el siguiente error al tratar de enviar la información al server: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<T> PutData(string UrlBase, string Url, List<T> Ltrans)
        {
            T LS = new T();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                    if (!String.IsNullOrEmpty(UrlToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UrlToken);
                    }
                    var data = JsonConvert.SerializeObject(Ltrans);

                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    string url = Url;
                    var response = await client.PutAsync(url, content);
                    var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

                    LS = result;



                    return LS;
                }

            }
            catch (Exception ex)
            {

                return LS;
            }
        }
        public async Task<T> PutData(string UrlBase, string Url, T Ltrans)
        {
            T LS = new T();

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                    if (!String.IsNullOrEmpty(UrlToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", UrlToken);
                    }
                    var data = JsonConvert.SerializeObject(Ltrans);

                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    string url = Url;
                    var response = await client.PutAsync(url, content);
                    var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

                    LS = result;



                    return LS;
                }

            }
            catch (Exception ex)
            {

                return LS;
            }
        }
    }
}
