using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Application_Tracker
{
    internal class JobManager
    {
        // Attribut
        public List<JobApplication> Applications { get; set; } = new List<JobApplication>(); // Samling av alla ansökningar

        // Konstruktor – fyller listan med dummydata för testning
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

        // Metoder

        // Lägger till en ny ansökan
        public void AddJob()
        {
            Console.WriteLine("Företagets namn? "); // Be användare om företagsnamn
            string companyName = Console.ReadLine(); 

            Console.WriteLine("Jobbtitel? "); // Be användare om jobbtitel
            string positionTitle = Console.ReadLine();

            Console.WriteLine("Förväntad lön? "); // Be användare om förväntad lön
            int salaryExpectation = Convert.ToInt32(Console.ReadLine());

            var job = new JobApplication(companyName, positionTitle, salaryExpectation); // Skapa Jobapplication-objekt
            Applications.Add(job); // Lägg till i listan

            Console.WriteLine($"Ansökan till {companyName} registrerad!"); // Skriv ut till användaren
        }

        // Ändrar status på en befintlig ansökan
        public void UpdateStatus()
        {
            if (Applications.Count == 0) // Kollar så att inte listan är tom
            {
                Console.WriteLine("Ingen ansökan finns att uppdatera.");
                return;
            }

            foreach (var status in Applications) // Skriva ut Statusen för jobbansökningarna
            {
                Console.WriteLine($"Företag: {status.CompanyName} med status {status.ApplicationStatus}");
            }

            Console.WriteLine("Skriv namnet på företaget du vill uppdatera status: "); // Be användare om företagsnamn
            string inputCompany = Console.ReadLine();

            // Leta efter om CompanyName är lika som det användaren skrev in och ignorera stora, små bokstäver. FirstOrDefault returnerar första elementet som matchar villkoret.
            var jobToUpdate = Applications.FirstOrDefault(a => a.CompanyName.Equals(inputCompany, StringComparison.OrdinalIgnoreCase));

            if (jobToUpdate == null) // Om företaget inte hittas i listan
            {
                Console.WriteLine("Ingen ansökan hittades med det företagsnamnet.");
                return;
            }

            // Be användaren skriva in den nya statusen
            Console.Write("Skriv den nya statusen (Applied, Interview, Offer, Rejected): ");
            string inputStatus = Console.ReadLine();

            // Försöker konvertera den text som användaren skrev in till ett giltigt Status-enumvärde, 
            // där stora och små bokstäver ignoreras. Om konverteringen lyckas lagras värdet i newStatus.
            bool success = Enum.TryParse<JobApplication.Status>(inputStatus, true, out JobApplication.Status newStatus);

            if (!success)
            {
                Console.WriteLine("Ogiltig status!");
                return;
            }

            // Uppdatera status
            jobToUpdate.ApplicationStatus = newStatus;

            // Bekräfta ändring
            Console.WriteLine($"Status för {jobToUpdate.CompanyName} uppdaterad till {jobToUpdate.ApplicationStatus}.");

        }

        // Visar alla ansökningar
        public void ShowAll()
        {
            // Kolla om listan är tom
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen ansökan har registrerats ännu.");
                return;
            }

            Console.WriteLine("Alla registrerade ansökningar: ");

            // Loopa igenom alla ansökningar
            foreach (var app in Applications)
            {
                // Färgkodning beroende på status
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

                // Visa information om ansökningarna
                Console.WriteLine($"Företag: {app.CompanyName}"); 
                Console.WriteLine($"Tjänst: {app.PositionTitle}");
                Console.WriteLine($"Status: {app.ApplicationStatus}");
                Console.WriteLine($"Ansökt: {app.ApplicationDate.ToShortDateString()}");
                // Funkar som en if sats om Responsedate har värde skriv ut det annars skriv ut inget svar ännu.
                Console.WriteLine($"Svar: {(app.ResponseDate.HasValue ? app.ResponseDate.Value.ToShortDateString() : "Inget svar ännu")}");
                Console.WriteLine($"Förväntad lön: {app.SalaryExpectation} kr");

                //  Återställ färg
                Console.ResetColor();
            }
        }

        // Filtrerar ansökningar efter status (VG-del)
        public void ShowByStatus()
        {
            // Kolla om listan är tom
            if (Applications.Count == 0)
            {
                Console.WriteLine("Ingen ansökan finns att filtrera.");
                return;
            }

            // Be användaren skriva in status att filtrera på
            Console.WriteLine("Vilken status vill du visa? (Applied, Interview, Offer, Rejected)");
            string inputStatus = Console.ReadLine();

            // Försök konvertera användarens input till enum (ignorerar stora/små bokstäver)
            bool success = Enum.TryParse<JobApplication.Status>(inputStatus, true, out JobApplication.Status filterStatus);

            if (!success)// Om den inte hittar någon med den statusen i listan
            {
                Console.WriteLine("Ogiltig status!");
                return;
            }

            // Filtrera listan med LINQ – behåll bara ansökningar med vald status
            var filteredApplications = Applications
                .Where(a => a.ApplicationStatus == filterStatus);

            // Visa filtrerade ansökningar
            Console.WriteLine($"Ansökningar med status {filterStatus}:");
            foreach (var app in filteredApplications)
            {
                // Färgkodning beroende på status
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

                // Visa information om ansökningarna
                Console.WriteLine($"Företag: {app.CompanyName}");
                Console.WriteLine($"Tjänst: {app.PositionTitle}");
                Console.WriteLine($"Status: {app.ApplicationStatus}");
                Console.WriteLine($"Ansökt: {app.ApplicationDate.ToShortDateString()}");
                // Funkar som en if sats om Responsedate har värde skriv ut det annars skriv ut inget svar ännu.
                Console.WriteLine($"Svar: {(app.ResponseDate.HasValue ? app.ResponseDate.Value.ToShortDateString() : "Inget svar ännu")}");
                Console.WriteLine($"Förväntad lön: {app.SalaryExpectation} kr");
                Console.WriteLine();

                //  Återställ färg
                Console.ResetColor();
            }
        }

        // Visar statistik över ansökningar (VG-del)
        public void ShowStatistics()
        {
            if ( Applications.Count == 0 )
            { 
                Console.WriteLine("Listan är tom");
                return; 
            }

            int totalApplications = Applications.Count; // Skapa en variabel för hur många ansökningar som finns med hjälp av count
            Console.WriteLine($"Totalt antal ansökningar: {totalApplications}"); // Skriva ut antalet till användaren

            var groupByStatus = Applications.GroupBy(a => a.ApplicationStatus); // Grupperar listan efter status genom att använda LINQ
            foreach (var group in groupByStatus)
            {
                Console.WriteLine($"Status: {group.Key} - Antal: {group.Count()}"); // Skriver ut group.key för att få statusen och sedan count för antalet i gryppen
            }

            // Filtrera bort ansökningar utan svar (ResponseDate = null)
            var answeredApplications = Applications.Where(a => a.ResponseDate.HasValue);

            // Beräkna genomsnittlig svarstid i antal dagar
            double averageResponseTime = answeredApplications.Average(a => (a.ResponseDate.Value - a.ApplicationDate).TotalDays);
            Console.WriteLine($"Genomsnittlig svarstid är {averageResponseTime:f1} dagar"); // Skriver ut antal dagar med en decimal
        }
    }
}
