﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace O21Toolbox.NotQuiteHumanoid
{
    public class NQH_Pawn : Pawn
    {
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            if(this.story == null)
            {
                this.story = new Pawn_StoryTracker(this);
            }
            if(this.Faction == Faction.OfPlayer)
            {
                if(this.playerSettings == null)
                {
                    this.playerSettings = new Pawn_PlayerSettings(this);
                }
                if(this.drafter == null)
                {
                    this.drafter = new Pawn_DraftController(this);
                }
                if(this.jobs == null)
                {
                    this.jobs = new Pawn_JobTracker(this);
                }
            }
            Lord lord = this.Map.lordManager.lords.ToList().Find(x => x.ownedPawns.Any(y => y.Faction.IsPlayer));
            if (lord != null)
            {
                Log.Message("Assigned Lord to " + this.kindDef.label);
                lord.AddPawn(this);
            }
        }
    }
}
