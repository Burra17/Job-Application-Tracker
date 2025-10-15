using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Application_Tracker
{
    internal class JobManager
    {
        // 🔹 Attribut
        public List<JobApplication> Applications { get; set; } = new List<JobApplication>(); // Samling av alla ansökningar

        // 🔹 Metoder

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
            // TODO: Implementera
        }

        // Filtrerar ansökningar efter status (VG-del)
        public void ShowByStatus()
        {
            // TODO: Implementera (LINQ)
        }

        // Visar statistik över ansökningar (VG-del)
        public void ShowStatistics()
        {
            // TODO: Implementera (Count, Average, OrderBy, Where)
        }
    }
}
