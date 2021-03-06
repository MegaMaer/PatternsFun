﻿using System;
using Domain.CarTypes;
using InterfacesActions;
using Utils;

namespace ActionImplementation
{
    public class FillTank : ICarActionOnCreation
    {
        #region ICarActionOnCreation Members

        public void FillCarTank(Car car)
        {
            car.FillTank(100);
            Console.WriteLine("Tank filled in {0} ", car.Name);
            Logger.AddMsgToLog("Tank Filled in " + car.Name);
        }

        #endregion
    }
}