﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Utility
{
    [StaticConstructorOnStartup]
    public class ToolboxContent
    {
        static ToolboxContent()
        {
            UnknownBuildable = ContentFinder<Texture2D>.Get("UI/Toolbox/Unknown");
        }

        public static readonly Texture2D UnknownBuildable;

        public static MainButtonDef O21_MainTab_Drones;
    }
}
