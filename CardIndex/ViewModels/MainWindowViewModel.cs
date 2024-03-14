﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CardIndex.Business.Services;
using CardIndex.Core.Entities;

namespace CardIndex.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private EmployeeService _service = new EmployeeService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EmployeeViewModel> Employees { get; } = new ObservableCollection<EmployeeViewModel>();

        public EmployeeViewModel SelectedEmployee { get; set;}

        public bool CanEdit => SelectedEmployee != null;

        public bool CanDelete => SelectedEmployee != null;

        public string FileName { get; set; }

        public MainWindowViewModel()
        {
            GetAllEmployees();
        }

        public void GetAllEmployees()
        {
            var allEmployees = _service.GetAllEmployees();

            Employees.Clear();

            foreach (Employee employee in allEmployees)
            {
                Employees.Add(
                    new EmployeeViewModel
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        BirthDate = employee.BirthDate,
                        Position = employee.Position,
                        Department = employee.Department,
                        EmploymentDate = employee.EmploymentDate
                    });
            }
        }

        public void CreateNewEmployee(IEmployeeWindow window)
        {
            var employee = new EmployeeViewModel();
            window.ViewModel = employee;
        }

        public void SaveEmployee(IEmployeeWindow window)
        {
            var employeeViewModel = window.ViewModel;
            _service.SaveEmployee(new Employee
            {
                Id= employeeViewModel.Id,
                FirstName = employeeViewModel.FirstName,
                MiddleName = employeeViewModel.MiddleName,
                LastName = employeeViewModel.LastName,
                BirthDate = employeeViewModel.BirthDate,
                Position = employeeViewModel.Position,
                Department = employeeViewModel.Department,
                EmploymentDate = employeeViewModel.EmploymentDate
            });

            GetAllEmployees();
        }

        public void EditEmployee(IEmployeeWindow window)
        {
            var employee = SelectedEmployee;
            window.ViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Position = employee.Position,
                Department = employee.Department,
                EmploymentDate= employee.EmploymentDate
            };
        }

        public bool HasFileName => !string.IsNullOrEmpty(FileName);

        public void UpdateButtons()
        {
            OnPropertyChanged(nameof(CanEdit));
            OnPropertyChanged(nameof(CanDelete));
        }

        public void DeleteSelectedItem()
        {
            if (SelectedEmployee == null)
                return;

            _service.Delete(SelectedEmployee.Id);
            GetAllEmployees();
        }

        public void Save()
        {
            _service.Save(FileName);
        }

        public void SaveAs(string filename)
        {
            FileName = filename;
            _service.Save(FileName);
        }

        public void Open(string filename)
        {
            FileName = filename;
            _service.Open(FileName);
            GetAllEmployees();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
