﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Teleporter
{
    [DefOf]
    public static class TeleporterDefOf
    {
        static TeleporterDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TeleporterDefOf));
        }

        public static JobDef O21_UseTeleporter;
        public static JobDef O21_UseRecall;
    }
}
