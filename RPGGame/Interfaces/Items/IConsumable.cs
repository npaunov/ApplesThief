﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAndatHypori.Interfaces.Items
{
    public interface IConsumable : IItem
    {
        int Duration { get; set; }
    }
}
