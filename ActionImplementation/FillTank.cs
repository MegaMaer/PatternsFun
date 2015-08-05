﻿// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 03 08 2015

using System;
using Domain.CarTypes;
using InterfacesActions;
using Utils;

namespace ActionImplementation
{
    public class FillTank : ICarActionOnCreation
    {
        public void FillCarTank(Car car)
        {
            car.FillTank(100);
            Console.WriteLine("Tank filled in {0} ", car.Name);
            Logger.AddMsgToLog("Tank Filled in "+ car.Name);
        }
    }
}