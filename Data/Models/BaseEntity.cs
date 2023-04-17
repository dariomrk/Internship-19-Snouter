﻿using Data.Interfaces;

namespace Data.Models
{
    public abstract class BaseEntity<TId> : IEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
