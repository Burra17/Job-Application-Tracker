using System.Security.Cryptography.X509Certificates;

namespace Job_Application_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapa ett JobManager objektS
            var jobManager = new JobManager();
            // En Whileloop för att köra så länge användaren vill med switch case baserat på användarens val
            bool running = true;

            while (running)
            {
                Menu.ShowMenu();// Visa meny
                string choice = Console.ReadLine(); // Läsa in användarens val

                switch (choice)
                {
                    case "1":
                        jobManager.AddJob();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        jobManager.UpdateStatus(); 
                        break;
                    case "7":
                        break;
                    case "8":
                        running = false;
                        Console.Write("Avslutar Programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltligt val försök igen.");
                        break;
                }
            }
        }
    }
}
