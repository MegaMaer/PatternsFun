using System;
using System.Collections.Generic;
using Domain.Dto;
using Domain.Interfaces;
using Utils;
using System.ComponentModel.DataAnnotations;

namespace Domain.Persons
{
    public class Pilot : Entity, IPilot
    {
        private readonly IList<Vehicle> _carVehicles = new List<Vehicle>();

        public Pilot(string name, string debutDate, int age, string team)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Pilot need a name!");
            if (age < 17) throw new Exception("Too young age for a pilot!");
            if (string.IsNullOrWhiteSpace(team)) throw new Exception("Team name can't be empty");

            Name = name;
            DebutDate = DateTime.Parse(debutDate);
            Age = age;
            Team = team;
        }

        [Obsolete]
        public Pilot()
        {
        }

        public virtual string Name { get; protected set; }
        public virtual string Team { get; protected set; }
        public virtual int Age { get; set; }

        [Display(Name = "Debut date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime DebutDate { get; protected set; }


        public virtual IList<Vehicle> CarVehicles
        {
            get { return _carVehicles; }
        }

        public virtual double ExpierenceTime
        {
            get { return (DateTime.Now.Date - DebutDate.Date).TotalDays; }
            set { }
        }

        public virtual PaddockAccessLevels AccessLevel
        {
            get { return PaddockAccessLevels.ClubPaddock; }
        }

        #region Implementation of IPilot

        public virtual void Update(string status)
        {
            Console.WriteLine("Race status : {0} for {1}", status, Name);
            Logger.AddMsgToLog("race status: " + status + " for " + Name);
        }

        #endregion

        public virtual Pilot PilotEdit(Pilot oldPilot, Pilot newPilot)
        {
            var editedPilot = oldPilot;
            editedPilot.Name = newPilot.Name;
            editedPilot.Age = newPilot.Age;
            editedPilot.DebutDate = newPilot.DebutDate;


            return editedPilot;
        }

        public virtual void AddCar(Vehicle car)
        {
            _carVehicles.Add(car);
        }

        public virtual void PilotEdit(PilotUpdateDto pilotUpdateDto)
        {
            Id = pilotUpdateDto.Id;
            Name = pilotUpdateDto.Name;
            Age = pilotUpdateDto.Age;
            DebutDate = DateTime.Parse(pilotUpdateDto.Debutdate);
            Team = pilotUpdateDto.Team;
        }
    }
}