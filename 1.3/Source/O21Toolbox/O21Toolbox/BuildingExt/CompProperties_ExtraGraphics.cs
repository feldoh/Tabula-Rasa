﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace O21Toolbox.BuildingExt
{
    public class CompProperties_ExtraGraphics : CompProperties
    {
        public CompProperties_ExtraGraphics()
        {
            compClass = typeof(Comp_ExtraGraphics);
        }

        public List<ExtraGraphicDetails> extraGraphics = new List<ExtraGraphicDetails>();
    }

    public class ExtraGraphicDetails
    {
        public string label = "";
        public string path;
    }
}
