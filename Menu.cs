using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Application_Tracker
{
    public static class Menu
    {
        //Metod för att skriva ut meny
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Lägg till ny ansökan: ");
            Console.WriteLine("2. Visa alla ansökningar: ");
            Console.WriteLine("3. Filtrera ansökningar efter status: ");
            Console.WriteLine("4. Sortera ansökningar efter datum: ");
            Console.WriteLine("5. Visa statistik: ");
            Console.WriteLine("6. Uppdatera status på en ansökan: ");
            Console.WriteLine("7. Ta bort en ansökan: ");
            Console.WriteLine("8. Avsluta programmet: ");
        }
    }
}
