using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Application_Tracker
{
    public class JobApplication
    {
        //Attribut
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public Status ApplicationStatus { get; set; }
        public enum Status
        {
            Applied,
            Interview,
            Offer,
            Rejected
        }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; } 

        //Konstruktor
        public JobApplication(string companyName, string positionTitle, int salaryExpectation)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            SalaryExpectation = salaryExpectation;
            ApplicationStatus = Status.Applied;
            ApplicationDate = DateTime.Now;
            ResponseDate = null;
        }


        //Metoder
        public void GetDaysSinceApplied() // returnerar antal dagar sedan ansökan skickades
        {

        }

        public void GetSummary() // returnerar en kort sammanfattning av ansökan
        { 

        }

    }
}
