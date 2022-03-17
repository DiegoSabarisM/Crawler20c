using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {

        public async static Task Main(string[] args)
        {

            if (args.Length < 1)
            {
                throw new ArgumentNullException();
            }

            var websiteUrl = args[0];
            var httpClient = new HttpClient();

            if (!Uri.IsWellFormedUriString(websiteUrl, UriKind.Absolute)) {
                throw new ArgumentException();
            }

            try
            {
                var response = await httpClient.GetAsync(websiteUrl);


                if (response.IsSuccessStatusCode) {

                    var content = await response.Content.ReadAsStringAsync();
                    var regex = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                    var matchCollection = regex.Matches(content);
                    HashSet<String> adresSet = new HashSet<string>();
                    foreach (var item in matchCollection)
                    {
                        adresSet.Add(item.ToString());
                    }
                    if(adresSet.Count == 0)
                        Console.WriteLine("Nie znaleziono adresów email");
                    else
                    {
                        foreach(String s in adresSet)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Strona niedostępna");
                }
                
                httpClient.Dispose();

            }
            catch (Exception e) {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
        }
    }
    }

