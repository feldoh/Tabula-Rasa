﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.ApparelExt
{
    public class CompProperties_Evolving : CompProperties
    {
        public CompProperties_Evolving()
        {
            this.compClass = typeof(Comp_Evolving);
        }
    }
}
