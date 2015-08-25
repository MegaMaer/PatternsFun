﻿using Domain;

namespace Repository.Interfaces
{
    public interface IPilotRepository
    {
        void AddPilot<TEntity>(TEntity entity) where TEntity : Entity;
        void UpdatePilotAge(long pilotId, int newAge);
        void DeletePilot(long id);


        void AddCar(Domain.Persons.Pilot pilot, Domain.CarTypes.Car car);
    }
}