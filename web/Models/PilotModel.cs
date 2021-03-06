﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Persons;

namespace Web.Models
{
    public class PilotModel
    {
        public PilotModel(Pilot pilot)
        {
            Id = pilot.Id;
            Name = pilot.Name;
            Age = pilot.Age;
            Team = pilot.Team;
            DebutDate = pilot.DebutDate;
            ExperienceTime = Convert.ToInt32((DateTime.Now - DebutDate).TotalDays);
            CarCount = pilot.CarVehicles.Count;
        }

        public PilotModel()
        {
            DebutDate = DateTime.Today;
        }
        [ScaffoldColumn (false)]
        public long Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Enter correct name")]
        public string Name { get; set; }

        [Required]
        [Range(18, 100)]
        public int Age { get; set; }

      
        [Required]
        public string Team { get; set; }

        [Display(Name = "Debut date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DebutDate { get; set; }
        [DataType(DataType.Duration)]
        public int ExperienceTime { get; set; }

        public List<Vehicle> VehiclesList { get; set; }
        public int CarCount { get; set; }
    }
}