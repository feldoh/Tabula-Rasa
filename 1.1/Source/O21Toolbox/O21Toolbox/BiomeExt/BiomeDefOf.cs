﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.BiomeExt
{
    public class BiomeDefOf
	{
		public static BiomeWorkerSpecial WorkerSpecial(this BiomeDef biome)
		{
			return biome.Worker as BiomeWorkerSpecial;
		}
	}
}