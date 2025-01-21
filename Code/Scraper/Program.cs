using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "PSN Web Scraper";

        while (true)
        {
            ShowWatermark();
            Console.Write("  Enter PSN Name >> ");
            string username = Console.ReadLine();

            var accountDetails = await GetPsnAccountDetails(username);

            if (accountDetails != null)
            {
                
                Console.WriteLine($"\n  ————————————————————————————\n  Online ID: {accountDetails.OnlineId}");
                Console.WriteLine($"  User ID: {accountDetails.UserId}");
                Console.WriteLine($"  Encode ID: {accountDetails.EncodeId}\n  ————————————————————————————");
            }
            else
            {
                Console.WriteLine("\nAccount not found.");
            }

            while (true)
            {
                Console.WriteLine("\n1. Continue (search for another PSN name)");
                Console.WriteLine("2. Exit (close the program)");
                Console.Write("  Choose an option >> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    break;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Exiting the program...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
        }
    }

    public static async Task<AccountDetails> GetPsnAccountDetails(string username)
    {
        try
        {
            string url = $"https://psn.flipscreen.games/search.php?username={username}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var accountDetails = JsonSerializer.Deserialize<AccountDetails>(responseBody);
                return accountDetails;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static void ShowWatermark()
    {
        Console.Clear();
        Console.WriteLine("\n  ██████╗ ███████╗███╗   ██╗    ██╗    ██╗███████╗██████╗     ███████╗ ██████╗██████╗  █████╗ ██████╗ ███████╗██████╗");
        Console.WriteLine("  ██╔══██╗██╔════╝████╗  ██║    ██║    ██║██╔════╝██╔══██╗    ██╔════╝██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗");
        Console.WriteLine("  ██████╔╝███████╗██╔██╗ ██║    ██║ █╗ ██║█████╗  ██████╔╝    ███████╗██║     ██████╔╝███████║██████╔╝█████╗  ██████╔╝");
        Console.WriteLine("  ██╔═══╝ ╚════██║██║╚██╗██║    ██║███╗██║██╔══╝  ██╔══██╗    ╚════██║██║     ██╔══██╗██╔══██║██╔═══╝ ██╔══╝  ██╔══██╗");
        Console.WriteLine("  ██║     ███████║██║ ╚████║    ╚███╔███╔╝███████╗██████╔╝    ███████║╚██████╗██║  ██║██║  ██║██║     ███████╗██║  ██║");
        Console.WriteLine("  ╚═╝     ╚══════╝╚═╝  ╚═══╝     ╚══╝╚══╝ ╚══════╝╚═════╝     ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚══════╝╚═╝  ╚═╝\n");
    }

    public class AccountDetails
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("online_id")]
        public string OnlineId { get; set; }

        [JsonPropertyName("encoded_id")]
        public string EncodeId { get; set; }
    }
}
