using Medical_Office_UWP_Client_v1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Office_UWP_Client_v1._0.Data
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetPatients();
        Task<Patient> GetPatient(int ID);
        Task<List<Patient>> GetPatientsByDoctor(int DoctorID);
    }
}
