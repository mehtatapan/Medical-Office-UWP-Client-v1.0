using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Office_UWP_Client_v1._0.Models
{
    public class Patient
    {
        public int ID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int a = today.Year - DOB.Year
                    - (today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0);
                return "Age: " + a.ToString().PadLeft(3);
            }
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string OHIP { get; set; }

        public DateTime DOB { get; set; }

        public byte ExpYrVisits { get; set; }

        public byte[] RowVersion { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

    }
}
