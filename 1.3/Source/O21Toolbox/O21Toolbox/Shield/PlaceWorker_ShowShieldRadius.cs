﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Shield
{
	public class PlaceWorker_ShowShieldRadius : PlaceWorker
	{
		public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
		{
			CompProperties_Shield compProperties = def.GetCompProperties<CompProperties_Shield>();
			if (compProperties != null)
			{
				GenDraw.DrawCircleOutline(center.ToVector3Shifted(), compProperties.shieldScaleLimits.max);
				GenDraw.DrawCircleOutline(center.ToVector3Shifted(), compProperties.shieldScaleLimits.min);
			}
		}
	}
}
