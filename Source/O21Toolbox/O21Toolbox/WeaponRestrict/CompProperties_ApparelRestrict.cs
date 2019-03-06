﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.WeaponRestrict
{
    public class CompProperties_ApparelRestrict : CompProperties
    {
        public List<ThingDef> RequiredApparel = null;

        public bool AllRequired = false;
    }
}
