using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SurveyHttpClient.Models;

namespace SurveyHttpClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri(""); // insert the endpoint location
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await GetAll();
            //await GetItem();
            //await AddPost();
            //await UpdateItem();
            //await DeleteItem();
        }

        private async static Task GetAll()
        {
            try
            {
                string json;
                HttpResponseMessage response;

                response = await client.GetAsync("api/getall"); // change getall to what on controller

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    IEnumerable<User> items = JsonConvert.DeserializeObject<IEnumerable<User>>(json);

                    foreach (User item in items)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("Internal Server Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private async static Task GetItem()
        {
            try
            {
                HttpResponseMessage response;

                int userId = 20;
                User item;
                response = await client.GetAsync($"/api/User/{userId}"); // change endpoint to what on controller

                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadAsAsync<User>();
                    Console.WriteLine(item);
                }
                else
                {
                    Console.Write("Internal Server Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private async static Task AddPost()
        {
            try
            {
                string json;
                HttpResponseMessage response;

                User item = new User
                {
                    UserId = 35,
                    FirstName = "Michael",
                    LastName = "James",
                    Username = "michael",
                    Password = "james",
                    UserType = "Salesman"
                }; // change information

                json = JsonConvert.SerializeObject(item);
                response = await client.PostAsJsonAsync("/api/User", item); // change endpoint to what on controller

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");

                json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("item has been inserted" + json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private async static Task UpdateItem()
        {
            try
            {
                string json;
                HttpResponseMessage response;

                User item = new User
                {
                    UserId = 35,
                    FirstName = "Michael",
                    LastName = "James",
                    Username = "michael",
                    Password = "james",
                    UserType = "Salesman"
                }; // change information

                json = JsonConvert.SerializeObject(item);
                response = await client.PutAsJsonAsync($"/api/User/{item.UserId}", item); // change endpoint to what on controller

                Console.WriteLine($"status from PUT {response.StatusCode}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private async static Task DeleteItem()
        {
            try
            {
                HttpResponseMessage response;

                int userId = 35;
                response = await client.DeleteAsync($"/api/User/{userId}"); // change endpoint to what on controller

                Console.WriteLine($"status from DELETE {response.StatusCode}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

    }
}
