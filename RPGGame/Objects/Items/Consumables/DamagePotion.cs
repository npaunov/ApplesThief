﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TeamAppleThief.Objects.Items.Consumables
{
    public class DamagePotion : Potion
    {
        public DamagePotion()
        {
            this.Duration = 1500;
            this.AttackPointsBuff = 10;
        }
    }
}
