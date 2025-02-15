﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;

using O21Toolbox.Utility;

namespace O21Toolbox.AutoHeal
{
    public class Hediff_GrowingPart : Hediff_AddedPart
    {

        public override bool ShouldRemove
        {
            get
            {
                return this.Severity >= this.def.maxSeverity;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }

        public override string TipStringExtra
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(base.TipStringExtra);
                stringBuilder.AppendLine(this.def.GetModExtension<DefModExtension_AutoHealProps>().growthText + GenText.ToStringPercent(this.Severity));
                return stringBuilder.ToString();
            }
        }

        public override void PostRemoved()
        {
            base.PostRemoved();
            bool flag = this.Severity >= 1f;
            if (flag && this.pawn.health.hediffSet.GetFirstHediffOfDef(this.def.GetModExtension<DefModExtension_AutoHealProps>().autoHealHediff, false).def.TryGetModExtension<DefModExtension_AutoHealProps>().curedBodyPart != null)
            {
                this.pawn.ReplaceHediffFromBodypart(base.Part, HediffDefOf.MissingBodyPart, this.pawn.health.hediffSet.GetFirstHediffOfDef(this.def.GetModExtension<DefModExtension_AutoHealProps>().autoHealHediff, false).def.GetModExtension<DefModExtension_AutoHealProps>().curedBodyPart);
            }
            else if (flag)
            {
                this.pawn.ReplaceHediffFromBodypart(base.Part, HediffDefOf.MissingBodyPart, HediffDefOf_AutoHeal.O21_AutoHeal_CuredBodypart);
            }
        }
    }
}
