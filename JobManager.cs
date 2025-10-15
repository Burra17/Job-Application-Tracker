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
            // TODO: Implementera
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
