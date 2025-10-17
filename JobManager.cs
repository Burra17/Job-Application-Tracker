using System;
using System.Collections.Generic;
using System.Linq;

namespace Job_Application_Tracker
{
    public class JobManager
    {
        // Attribut: Lista som innehåller alla jobbansökningar
        public List<JobApplication> Applications { get; set; } = new List<JobApplication>();

        // Konstruktor – fyller listan med exempeldata för testning
        public JobManager()
        {
            Applications = new List<JobApplication>
            {
                new JobApplication("Hammarby AB", "Backend Developer", 40000),
                new JobApplication("Chelsea Tech", "Frontend Developer", 42000)
                {
                    ApplicationStatus = JobApplication.Status.Interview,
                    ApplicationDate = new DateTime(2025, 9, 25),
                    ResponseDate = new DateTime(2025, 10, 5)
                },
                new JobApplication("Volvo Cars", "System Engineer", 48000)
                {
                    ApplicationStatus = JobApplication.Status.Offer,
                    ApplicationDate = new DateTime(2025, 8, 15),
                    ResponseDate = new DateTime(2025, 9, 1)
                },
                new JobApplication("Spotify", "DevOps Engineer", 50000)
                {
                    ApplicationStatus = JobApplication.Status.Rejected,
                    ApplicationDate = new DateTime(2025, 7, 30),
                    ResponseDate = new DateTime(2025, 8, 10)
                },
                new JobApplication("IKEA Digital", "Fullstack Developer", 45000)
            };
        }

        // ------------------- METODER -------------------

        // Lägg till en ny ansökan
        public void AddJob()
        {
            Console.Clear(); // Rensa konsolen
            Console.WriteLine("==== Lägg till ny ansökan ====\n");

            // Be användaren fylla i information
            Console.Write("Företagets namn: ");
            string companyName = Console.ReadLine();

            Console.Write("Jobbtitel: ");
            string positionTitle = Console.ReadLine();

            Console.Write("Förväntad lön: ");
            int salaryExpectation = Convert.ToInt32(Console.ReadLine());

            // Skapa nytt JobApplication-objekt och lägg till i listan
            Applications.Add(new JobApplication(companyName, positionTitle, salaryExpectation));

            // Bekräfta för användaren
            Console.WriteLine($"\nAnsökan till {companyName} registrerad!");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Uppdatera status på en ansökan
        public void UpdateStatus()
        {
            Console.Clear();
            Console.WriteLine("==== Uppdatera status på ansökan ====\n");

            // Kontrollera att det finns ansökningar
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen ansökan finns att uppdatera.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Visa alla ansökningar med nuvarande status
            Console.WriteLine("Nuvarande ansökningar och status:\n");
            foreach (var app in Applications)
            {
                Console.WriteLine($"{app.CompanyName} - {app.ApplicationStatus}");
            }

            // Be användaren ange vilket företag som ska uppdateras
            Console.Write("\nSkriv namnet på företaget du vill uppdatera: ");
            string inputCompany = Console.ReadLine();

            // Hitta ansökan baserat på företagsnamnet (ignorera stora/små bokstäver)
            var jobToUpdate = Applications.FirstOrDefault(a =>
                a.CompanyName.Equals(inputCompany, StringComparison.OrdinalIgnoreCase));

            if (jobToUpdate == null)
            {
                Console.WriteLine("\nIngen ansökan hittades med det företagsnamnet.");
            }
            else
            {
                // Be användaren ange ny status
                Console.Write("Skriv den nya statusen (Applied, Interview, Offer, Rejected): ");
                string inputStatus = Console.ReadLine();

                // Försök konvertera texten till enum, 'true' betyder att vi ignorerar stora/små bokstäver, 'newStatus' kommer att innehålla det konverterade värdet om det lyckas
                if (Enum.TryParse<JobApplication.Status>(inputStatus, true, out JobApplication.Status newStatus))
                {
                    // Om konverteringen lyckas, uppdatera ansökningens status
                    jobToUpdate.ApplicationStatus = newStatus;
                    Console.WriteLine($"\nStatus för {jobToUpdate.CompanyName} uppdaterad till {newStatus}.");
                }
                else
                {
                    // Om koverteringen misslyckas, meddela användaren.
                    Console.WriteLine("\nOgiltig status!");
                }
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Visa alla ansökningar
        public void ShowAll()
        {
            Console.Clear();
            Console.WriteLine("==== Alla registrerade ansökningar ====\n");

            // Kontrollera att listan inte är tom
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen ansökan har registrerats ännu.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Loopa igenom alla ansökningar och skriv ut
            foreach (var app in Applications)
            {
                PrintApplication(app); // Använd hjälpfunktion för att skriva ut färgglatt
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Filtrera ansökningar efter status 
        public void ShowByStatus()
        {
            Console.Clear();
            Console.WriteLine("==== Filtrera ansökningar efter status ====\n");

            // Kontrollera att listan inte är tom
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen ansökan finns att filtrera.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Be användaren ange status
            Console.Write("Vilken status vill du visa? (Applied, Interview, Offer, Rejected): ");
            string inputStatus = Console.ReadLine();

            // Försök konvertera texten till enum (ignorera stora/små bokstäver)
            // Om konverteringen misslyckas, skriv ut felmeddelande
            if (!Enum.TryParse<JobApplication.Status>(inputStatus, true, out JobApplication.Status filterStatus)) 
            {
                Console.WriteLine("\nOgiltig status!");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Filtrera med LINQ efter den valda statusen
            var filtered = Applications.Where(a => a.ApplicationStatus == filterStatus).ToList();

            if (filtered.Count == 0) // Om Inga ansökningar finns med den statusen
            {
                Console.WriteLine($"\nInga ansökningar med status {filterStatus}.");
            }
            else // Annars skriv ut Ansökningarna i de olika statusarna
            {
                Console.WriteLine($"\nAnsökningar med status {filterStatus}:\n"); 
                foreach (var app in filtered)
                {
                    PrintApplication(app); // Använd hjälpmetoden för att skriva ut snygg med färger
                }
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Visa statistik 
        public void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("==== Statistik över ansökningar ====\n");

            // Kontrollera att listan inte är tom
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen data finns att visa.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Totalt antal ansökningar
            int total = Applications.Count;
            Console.WriteLine($"Totalt antal ansökningar: {total}\n");

            // Gruppindelning per status med LINQ
            var grouped = Applications.GroupBy(a => a.ApplicationStatus);
            foreach (var g in grouped)
            {
                Console.WriteLine($"Status: {g.Key} - Antal: {g.Count()}"); // Skriver ut vilken status och hur många som finns i den gruppen
            }

            // Filtrera bort ansökningar som inte har fått något svar än
            var answered = Applications.Where(a => a.ResponseDate.HasValue).ToList();

            // Om det finns minst en ansökan med svar
            if (answered.Count > 0)
            {
                // Beräkna genomsnittlig svarstid i dagar
                // (Svardatum - ansökningsdatum) och ta medelvärdet
                double avgResponse = answered.Average(a => (a.ResponseDate.Value - a.ApplicationDate).TotalDays);

                // Skriv ut genomsnittlig svarstid med en decimal
                Console.WriteLine($"\nGenomsnittlig svarstid: {avgResponse:f1} dagar");
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Sortera ansökningar efter datum
        public void SortedApplications()
        {
            Console.Clear();
            Console.WriteLine("==== Ansökningar sorterade efter datum ====\n");

            if (Applications.Count == 0)   // Kontrollera om listan är tom
            {
                Console.WriteLine("Inga ansökningar finns att sortera.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Sortera ansökningarna med LINQ efter ansökningsdatum (äldst först)
            var sorted = Applications.OrderBy(a => a.ApplicationDate).ToList();

            // Skriv ut varje ansökan med en separat metod som formaterar snyggt
            foreach (var app in sorted)
            {
                PrintApplication(app); // Änvänder hjälpmetoden
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // Ta bort ansökan
        public void RemoveApplication()
        {
            Console.Clear();
            Console.WriteLine("==== Ta bort en ansökan ====\n");

            if (Applications.Count == 0) // Kollar om listan är tom
            {
                Console.WriteLine("Det finns inga ansökningar att ta bort.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
                return;
            }

            // Visa alla företagsnamn så användaren vet vad som finns
            Console.WriteLine("Följande ansökningar finns registrerade:\n");
            foreach (var app in Applications)
            {
                Console.WriteLine($"- {app.CompanyName}");
            }
            Console.WriteLine(); 

            // Be användaren ange företagsnamn
            Console.Write("Ange företagsnamnet för den ansökan du vill ta bort: ");
            string inputCompany = Console.ReadLine();

            // Hitta ansökan som matchar företagsnamnet (ignorera stora/små bokstäver)
            var appToRemove = Applications.FirstOrDefault(a =>
                a.CompanyName.Equals(inputCompany, StringComparison.OrdinalIgnoreCase));

            if (appToRemove == null) // Om ingen ansökan hittades
            {
                Console.WriteLine("\nIngen ansökan hittades med det företagsnamnet.");
            }
            else
            {   // TA bort ansökan från listan
                Applications.Remove(appToRemove);
                Console.WriteLine($"\nAnsökan till {appToRemove.CompanyName} har tagits bort.");
            }

            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
            Console.ReadKey();
        }

        // ------------------- HJÄLPMETOD -------------------

        // Skriv ut information om en ansökan med färg och layout
        private void PrintApplication(JobApplication app)
        {
            // Färg baserat på status med en switch-sats
            switch (app.ApplicationStatus)
            {
                case JobApplication.Status.Applied:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case JobApplication.Status.Offer:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case JobApplication.Status.Rejected:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case JobApplication.Status.Interview:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            // Skriv ut info
            Console.WriteLine($"Företag: {app.CompanyName}");
            Console.WriteLine($"Tjänst: {app.PositionTitle}");
            Console.WriteLine($"Status: {app.ApplicationStatus}");
            Console.WriteLine($"Ansökt: {app.ApplicationDate.ToShortDateString()}");
            Console.WriteLine($"Svar: {(app.ResponseDate.HasValue ? app.ResponseDate.Value.ToShortDateString() : "Inget svar ännu")}"); // Funkar som en if-sats om app.responsedate har ett värde skriver den ut det annars "Inget svar ännu"
            Console.WriteLine($"Förväntad lön: {app.SalaryExpectation} kr");
            Console.WriteLine(new string('-', 40)); // Linje mellan ansökningar

            Console.ResetColor();
        }
    }
}
