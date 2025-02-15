﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace O21Toolbox.AutomatedProducer
{
    /// <summary>
    /// Grabs a thing and fills the AutomatedProducer with it.
    /// </summary>
    public class JobDriver_FillProducer : JobDriver
    {
        private const TargetIndex CarryThingIndex = TargetIndex.A;

        private const TargetIndex DestIndex = TargetIndex.B;

        private Building_AutomatedProducer AutoProducer
        {
            get
            {
                return (Building_AutomatedProducer)job.GetTarget(TargetIndex.B);
            }
        }

        public override string GetReport()
        {
            Thing thing;
            if (this.pawn.carryTracker.CarriedThing != null)
            {
                thing = this.pawn.carryTracker.CarriedThing;
            }
            else
            {
                thing = base.TargetThingA;
            }
            return "ReportHaulingTo".Translate(thing.LabelCap, job.targetB.Thing.LabelShort);
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if (!pawn.CanReserve(TargetA))
                return false;

            if (!pawn.CanReserve(TargetB))
                return false;

            pawn.Reserve(TargetA, job, errorOnFailed: errorOnFailed);
            pawn.Reserve(TargetB, job, errorOnFailed: errorOnFailed);

            return true;
        }

        [DebuggerHidden]
        public override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(CarryThingIndex);
            this.FailOnDestroyedNullOrForbidden(DestIndex);
            this.FailOn(() => AutoProducer.TryGetComp<Comp_AutomatedProducer>().currentStatus != ProducerStatus.awaitingResources);

            //Reserve
            yield return Toils_Reserve.Reserve(CarryThingIndex, 1, -1, null);
            yield return Toils_Reserve.ReserveQueue(CarryThingIndex, 1, -1, null);
            yield return Toils_Reserve.Reserve(DestIndex, 1, -1, null);
            yield return Toils_Reserve.ReserveQueue(DestIndex, 1, -1, null);

            //Get to Haul target toil.
            Toil getToHaulTarget = Toils_Goto.GotoThing(CarryThingIndex, PathEndMode.ClosestTouch).FailOnSomeonePhysicallyInteracting(CarryThingIndex);
            yield return getToHaulTarget;

            yield return Toils_Construct.UninstallIfMinifiable(CarryThingIndex).FailOnSomeonePhysicallyInteracting(CarryThingIndex);
            yield return Toils_Haul.StartCarryThing(CarryThingIndex, false, true);
            yield return Toils_Haul.JumpIfAlsoCollectingNextTargetInQueue(getToHaulTarget, CarryThingIndex);

            //Carry to haul Container toil.
            Toil carryToContainer = Toils_Haul.CarryHauledThingToContainer();
            yield return carryToContainer;
        }
    }
}
