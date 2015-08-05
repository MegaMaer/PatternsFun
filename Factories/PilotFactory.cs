﻿// File: PilotFactory.cs in
// PatternsFun by Serghei Adam 
// Created 04 08 2015 
// Edited 04 08 2015

using System;
using Domain.Domain.Persons;

namespace Factories
{
    public static class PilotFactory
    {
        public static Pilot CreateNewPilot(string name, DateTime debutDateTime, int age, string teamName)
        {
            var pilot = new Pilot(name, debutDateTime, age, teamName);

            return pilot;
        }
    }
}