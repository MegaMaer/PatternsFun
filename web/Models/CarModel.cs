﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain.EnginesTypes;

namespace Web.Models
{
    public class CarModel
    {
        public CarModel(IList<SelectListItem> pilots)
        {
            Pilots = pilots;
        }

        [Obsolete]
        public CarModel()
        {
        }

        [Required]
        public string Name { get; set; }
        [Range(1, 65535)]
        public double Weight { get; set; }

        [Range(1, 65535)]
        public int HorsePowers { get; set; }
        [Range(1,24)]
        public EngineTypes EngineType { get; set; }

        public int TankVolume { get; set; }

        [UIHint("TextBoxEditor")]
        public string AdditionalInfo { get; set; }


        public long PilotId { get; set; }

        [Display(Name = "Owner")]
        public IList<SelectListItem> Pilots { get; set; }
    }
}