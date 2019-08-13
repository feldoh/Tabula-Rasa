﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Spaceship
{
    public class Util_TraderKind
    {
        public TraderKindDef GetSpaceshipCargo(Faction faction, SpaceshipKind type)
        {
            return DefDatabase<TraderKindDef>.GetNamed(faction.def.defName + "" + type.ToString());
        }
    }
}