using Medical_Office_UWP_Client_v1._0.Models;
using Medical_Office_UWP_Client_v1._0.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Office_UWP_Client_v1._0.Data
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HttpClient client = new HttpClient();

        public PatientRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Patient>> GetPatients()
        {
            HttpResponseMessage response = await client.GetAsync("/api/Patients");
            if (response.IsSuccessStatusCode)
            {
                List<Patient> Patients = await response.Content.ReadAsAsync<List<Patient>>();
                return Patients;
            }
            else
            {
                throw new Exception("Could not access the list of Patients.");
            }
        }

        public async Task<List<Patient>> GetPatientsByDoctor(int DoctorID)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/patients/byDoctor/{DoctorID}");
            if (response.IsSuccessStatusCode)
            {
                List<Patient> Patients = await response.Content.ReadAsAsync<List<Patient>>();
                return Patients;
            }
            else
            {
                throw new Exception("Could not access the list of Patients.");
            }
        }

        public async Task<Patient> GetPatient(int ID)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/Patients/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Patient Patient = await response.Content.ReadAsAsync<Patient>();
                return Patient;
            }
            else
            {
                throw new Exception("Could not access that Patient.");
            }
        }
    }
}
