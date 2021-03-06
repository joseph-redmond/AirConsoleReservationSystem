﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConsoleLibrary.Models
{
    public class PassengerModel
    {
        public PassengerModel(string firstName, string lastName, string passportNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PassportNumber = passportNumber;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { 
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string PassportNumber { get; }
    }
}
