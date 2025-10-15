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

            Console.WriteLine($"Ansökan till {companyName} registrerad!");
        }

        // Ändrar status på en befintlig ansökan
        public void UpdateStatus()
        {
            // TODO: Implementera
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
