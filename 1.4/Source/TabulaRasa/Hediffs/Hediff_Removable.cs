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
    public class Hediff_Removable : Hediff
    {
        public override bool ShouldRemove => true;

        public Hediff_Removable()
        {
        }
    }
}
