﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
