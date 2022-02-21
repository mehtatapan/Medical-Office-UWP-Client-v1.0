﻿using Medical_Office_UWP_Client_v1._0.Data;
using Medical_Office_UWP_Client_v1._0.Models;
using Medical_Office_UWP_Client_v1._0.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Medical_Office_UWP_Client_v1._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;

        public MainPage()
        {
            this.InitializeComponent();
            doctorRepository = new DoctorRepository();
            patientRepository = new PatientRepository();
            FillDropDown();
        }

        private async void ShowPatients(int? DoctorID)
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Patient> patients;
                if (DoctorID.GetValueOrDefault() > 0)
                {
                    patients = await patientRepository.GetPatientsByDoctor(DoctorID.GetValueOrDefault());
                }
                else
                {
                    patients = await patientRepository.GetPatients();
                }
                patientList.ItemsSource = patients;

            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillDropDown();
        }
        private async void FillDropDown()
        {
            //Show Progress
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Doctor> doctors = await doctorRepository.GetDoctors();
                //Add the All Option
                doctors.Insert(0, new Doctor { ID = 0, LastName = " - All Doctors" });
                //Bind to the ComboBox
                DoctorCombo.ItemsSource = doctors;
                ShowPatients(null);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }

        private void DoctorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor selDoc = (Doctor)DoctorCombo.SelectedItem;
            ShowPatients(selDoc?.ID);
        }
    }
}
