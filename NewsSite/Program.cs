using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NewsSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        /*
         * I Chrome finns en bugg/feature som gör att authenticate-kakan inte sätts om domänen heter localhost. (då sker ingen inloggning)
         * Det funkar i Debug mode (F5) men inte i när du bara kör Run (Ctrl-F5)
         * 
         * Alternativ I
         * - Använd Chrome men starta alltid i debug mode med F5
         * 
         * Alternativ II
         * - Använd Firefox eller Edge när du testar 
         * 
         * Alternativ III
         * - Ändra i hosts-filen
         * - Kommentera av .UseUrls nedan
         */
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("newssite")
                .UseStartup<Startup>()
                .Build();
    }
}
