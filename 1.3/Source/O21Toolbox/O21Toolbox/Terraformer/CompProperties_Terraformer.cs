﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Terraformer
{
    public class CompProperties_Terraformer : CompProperties
    {
        /// <summary>
        /// Terraformers that should function as part of the same grid should all share the same tag.
        /// Overlapping terraformers that share a tag but do different things will have strange behaviours.
        /// </summary>
        public string terraformerTag;

        /// <summary>
        /// If null, no nodes will spawn from this. Otherwise a random node from the list is chosen.
        /// 
        /// </summary>
        public List<ThingDefCount> nodeList = null;

        /// <summary>
        /// If not set, then terraformTicksExact is used instead.
        /// Ticks between terraform attempts, randomly chosen between min and max.
        /// Lower number = faster.
        /// </summary>
        public int terraformTicksMin = -1;
        public int terraformTicksMax = -1;

        /// <summary>
        /// Lower number = faster.
        /// 60,000 ticks = 1 full day in-game.
        /// </summary>
        public int terraformTicksExact = 10000;

        /// <summary>
        /// Radius in which nodes can spawn.
        /// 0 means no nodes can spawn.
        /// </summary>
        public int nodeRange = 0;

        /// <summary>
        /// If true, this node will stop functioning without a connection to a sustainer.
        /// Sustainers can function through their child nodes, so can sustain indirectly.
        /// </summary>
        public bool failsWithoutParent = false;

        /// <summary>
        /// If true, failing will cause the terraformer to wither.
        /// </summary>
        public bool failureWithers = false;
        /// <summary>
        /// Number of ticks that must fail to wither away.
        /// </summary>
        public int witherTicks = 10000;
        /// <summary>
        /// If true, successful ticks will slowly restore the withering which has occurred.
        /// </summary>
        public bool successRestores = true;

        /// <summary>
        /// If true, this node will be able to sustain others linked to it.
        /// </summary>
        public bool canSustainNodes = true;

        /// <summary>
        /// The def listing used to determine what the terraformer changes, and what to.
        /// </summary>
        public TerraformerRulesDef terraformerRules = null;

        /// <summary>
        /// Radius which will be affected by the terraformer.
        /// 0 means this node will not terraform.
        /// </summary>
        public float terraformRange = 0;

        /// <summary>
        /// Makes the terraformer have a larger area it can choose from for random tiles to change, makes spread more organic and less uniform.
        /// </summary>
        public float terraformRangeGap = 0;

        /// <summary>
        /// Whether or not the terraformer uses power.
        /// </summary>
        public bool requiresPower = false;

        /// <summary>
        /// Whether or not spread can pass through walls, allowing containment/blocking of terraforming based on region.
        /// </summary>
        public bool wallsPreventSpread = false;

        /// <summary>
        /// Whether or not the range is configurable by the player.
        /// </summary>
        public bool configurableRange = false;

        /// <summary>
        /// If true, this will kill off any plants that are not listed in the plants the terraformer places.
        /// </summary>
        public bool killUnlistedPlants = false;

        /// <summary>
        /// If True, edited tiles will slowly restore if the terraformer has been destroyed.
        /// </summary>
        public bool canNatureRestore = false;

        public List<TerrainDef> TerrainCandidates
        {
            get
            {
                if (terraformerRules.terrainRules.NullOrEmpty())
                {
                    return new List<TerrainDef>();
                }

                List<TerrainDef> candidates = new List<TerrainDef>();
                for (int i = 0; i < terraformerRules.terrainRules.Count(); i++)
                {
                    if (!terraformerRules.terrainRules[i].terrainDefsWhitelist.NullOrEmpty())
                    {
                        for (int j = 0; j < terraformerRules.terrainRules[i].terrainDefsWhitelist.Count; j++)
                        {
                            candidates.Add(terraformerRules.terrainRules[i].terrainDefsWhitelist[j]);
                        }
                    }
                }
                return candidates;
            }
        }

        public List<TerrainDef> PotentialResults
        {
            get
            {
                List<TerrainDef> results = new List<TerrainDef>();

                if (terraformerRules.terrainRules.NullOrEmpty())
                {
                    return results;
                }

                for (int i = 0; i < terraformerRules.terrainRules.Count(); i++)
                {
                    for (int j = 0; j < terraformerRules.terrainRules[i].terrainResult.Count(); j++)
                    {
                        results.Add(terraformerRules.terrainRules[i].terrainResult[j]);
                    }
                }

                return results;
            }
        }

        public List<ThingDef> EdificeCandidates
        {
            get
            {
                List<ThingDef> candidates = new List<ThingDef>();

                if (terraformerRules.edificeRules.NullOrEmpty())
                {
                    return candidates;
                }

                for (int i = 0; i < terraformerRules.edificeRules.Count(); i++)
                {
                    if (!terraformerRules.edificeRules[i].thingWhitelist.NullOrEmpty())
                    {
                        for (int j = 0; j < terraformerRules.edificeRules[i].thingWhitelist.Count; j++)
                        {
                            candidates.Add(terraformerRules.edificeRules[i].thingWhitelist[j]);
                        }
                    }
                }
                return candidates;
            }
        }
    }
}
