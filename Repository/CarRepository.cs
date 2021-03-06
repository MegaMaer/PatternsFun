﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.CarTypes;
using Domain.Dto;
using Domain.EnginesTypes;
using Domain.Persons;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Repository.Interfaces;
using Utils;

namespace Repository
{
    public class CarRepository : Repository, ICarRepository
    {
        public CarRepository(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public IList<CarDetailsDto> GetCarDetails()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => cAlias)
                        .JoinAlias(() => cAlias.Engine, () => geAlias)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => geAlias.HorsePowers).WithAlias(() => cddtoAlias.HorsePowers)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                        )
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public CarDetailsDto GetCarDetails(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => cAlias)
                        .JoinAlias(() => cAlias.Engine, () => geAlias)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => geAlias.HorsePowers).WithAlias(() => cddtoAlias.HorsePowers)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                        )
                        .Where(() => cAlias.Id == id)
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

                    return res.First();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public IList<CarDetailsDto> GetCarDetailsWithPilot()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => pAlias)
                        .JoinAlias(() => pAlias.CarVehicles, () => cAlias, JoinType.RightOuterJoin)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                            .Select(Projections.SqlFunction("coalesce", NHibernateUtil.String,
                                Projections.Property<Pilot>(p => p.Name),
                                Projections.Constant("No pilot in this Vehicle", NHibernateUtil.String)))
                            .WithAlias(() => cddtoAlias.PilotName)
                        )
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public CarDetailsDto GetCarDetailsWithPilotbyCarId(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                Engine geAlias = null;
                ElectroEngine eAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => pAlias)
                        .JoinAlias(() => pAlias.CarVehicles, () => cAlias, JoinType.RightOuterJoin)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                            .Select(Projections.SqlFunction("coalesce", NHibernateUtil.String,
                                Projections.Property<Pilot>(p => p.Name),
                                Projections.Constant("No pilot in this Vehicle", NHibernateUtil.String)))
                            .WithAlias(() => cddtoAlias.PilotName)
                        )
                        .Where(() => cAlias.Id == id)
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

                    return res.First();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public void UpdateCarInfo(Car oldCar, CarUpdateDto newCar)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var edited = oldCar.CarEdit(newCar);
                    _session.SaveOrUpdate(edited);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        public void DeleteCar(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var carToDelete = _session.Load<Car>(id);
                    _session.Delete(carToDelete);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        public IList<Car> GetAllCars()
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var res = _session.QueryOver<Car>()
                        .Fetch(x => x).Lazy()
                        .List();

                    tran.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public IList<SportCar> GetAllSportCars()
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var res = _session.QueryOver<SportCar>()
                        .List();

                    tran.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return new List<SportCar>();
                }
            }
        }
    }
}