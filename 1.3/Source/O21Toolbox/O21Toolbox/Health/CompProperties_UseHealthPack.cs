﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace O21Toolbox.Health
{
    public class CompProperties_UseHealthPack : CompProperties_UseEffect
    {
        public List<HediffDef> ignoredHediffs = new List<HediffDef>();
    }
}
