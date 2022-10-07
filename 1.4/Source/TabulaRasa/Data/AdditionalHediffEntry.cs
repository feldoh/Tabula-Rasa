﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TabulaRasa
{
    public class AdditionalHediffEntry
    {
        public HediffDef hediff;

        public FloatRange severityRange = new FloatRange();

        public float weight;
    }
}
