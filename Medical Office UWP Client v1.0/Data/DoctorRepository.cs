﻿using Medical_Office_UWP_Client_v1._0.Models;
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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HttpClient client = new HttpClient();

        public DoctorRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            HttpResponseMessage response = await client.GetAsync("/api/doctors");
            if (response.IsSuccessStatusCode)
            {
                List<Doctor> doctors = await response.Content.ReadAsAsync<List<Doctor>>();
                return doctors;
            }
            else
            {
                throw new Exception("Could not access the list of Doctors.");
            }
        }
        public async Task<Doctor> GetDoctor(int DoctorID)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/doctors/{DoctorID}");
            if (response.IsSuccessStatusCode)
            {
                Doctor doctor = await response.Content.ReadAsAsync<Doctor>();
                return doctor;
            }
            else
            {
                throw new Exception("Could not access that Doctor.");
            }
        }
    }
}
