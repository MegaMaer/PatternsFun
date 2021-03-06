﻿// File: BoxesProxy.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Paddock;
using Domain.Persons;

namespace Domain.Patterns.Proxy
{
    public class BoxesProxy : IAccess
    {
        private readonly Boxes _realBoxes;
        private readonly List<Fan> _realFans = new List<Fan>();
        private readonly Pilot _realPilot;

        public BoxesProxy(List<Fan> fan, Boxes boxes)
        {
            _realFans = fan;
            _realBoxes = boxes;
        }

        public BoxesProxy(Fan fan, Boxes boxes)
        {
            _realBoxes = boxes;
            _realFans.Add(fan);
        }

        public BoxesProxy(Pilot pilot, Boxes boxes)
        {
            _realBoxes = boxes;
            _realPilot = pilot;
        }

        #region Implementation of IAccess

        public void GrantAcces()
        {
            foreach (var realFan in _realFans)
            {
                if (realFan.AccessLevel == PaddockAccessLevels.ClubPaddock)
                    _realBoxes.GrantAcces();
                else
                    Console.WriteLine("{1},Sorry you don't have acces to {0} boxes", _realBoxes.Owner, realFan.Name);
            }
        }

        public void PilotAcces()
        {
            if (_realPilot.Team.Contains(_realBoxes.Owner))
                _realBoxes.PilotAcces();
            else
                Console.WriteLine("sorry {0},  may be next year :)", _realPilot.Name);
        }

        #endregion
    }
}