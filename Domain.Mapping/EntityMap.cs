﻿using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity : Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("100").Not.Nullable();
            Version(x => x.Version).Not.Nullable();
            DynamicUpdate();
        }
    }
}