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
    public class DeathActionWorker_InstantDessication : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            if(corpse != null && corpse.Map != null)
            {
                CompRottable comp = corpse.TryGetComp<CompRottable>();
                if (comp != null)
                {
                    comp.RotImmediately();
                    if (corpse.InnerPawn.def.race.BloodDef != null)
                    {
                        FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, corpse.InnerPawn.def.race.BloodDef, 5);
                    }
                    FleckMaker.AttachedOverlay(corpse, FleckDefOf.DustPuffThick, Vector3.zero, 10f);
                }
                else
                {
                    LogUtil.LogWarning($"Tried using DeathActionWorker_InstantDessication on {corpse.InnerPawn.def.defName} which cannot rot.");
                }
            }

        }
    }
}
