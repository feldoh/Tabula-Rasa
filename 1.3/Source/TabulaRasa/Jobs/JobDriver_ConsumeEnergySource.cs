﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace TabulaRasa
{
    public class JobDriver_ConsumeEnergySource : JobDriver
    {
        private const TargetIndex PowerDestIndex = TargetIndex.A;
        private const TargetIndex OtherPawnIndex = TargetIndex.B;

        public Need_Energy energyNeed;
        public bool isUsedFromInventory = false;
        public bool thingIsSplitOff = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref isUsedFromInventory, "isUsedFromInventory");
            Scribe_Values.Look(ref thingIsSplitOff, "thingIsSplitOff");
        }

        public override void Notify_Starting()
        {
            base.Notify_Starting();

            energyNeed = GetActor().needs.TryGetNeed<Need_Energy>();

            if (startTick == Find.TickManager.TicksGame && TargetThingAIsCarriedOrInInventory())
            {
                isUsedFromInventory = true;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if (!pawn.CanReserve(TargetA))
                return false;

            if (TargetB.IsValid)
            {
                if (!pawn.CanReserve(TargetB))
                {
                    return false;
                }
                else
                {
                    pawn.Reserve(TargetB, job, errorOnFailed: errorOnFailed);
                }
            }

            pawn.Reserve(TargetA, job, errorOnFailed: errorOnFailed);

            return true;
        }

        public override string GetReport()
        {
            if (TargetB.IsValid)
            {
                return ReportStringProcessed(job.def.GetModExtension<DefModExt_ExtraStrings>().extraReportString);
            }
            else
            {
                return base.GetReport();
            }
        }

        public bool TargetThingAIsCarriedOrInInventory()
        {
            return TargetThingA is Thing thing && (thing.ParentHolder is Pawn_CarryTracker || thing.ParentHolder is Pawn_InventoryTracker);
        }

        public override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedNullOrForbidden(PowerDestIndex);
            if (!TargetB.IsValid)
                AddFailCondition(() => energyNeed == null);

            if (!isUsedFromInventory)
            {
                yield return Toils_Reserve.Reserve(PowerDestIndex);
                if (TargetB.IsValid)
                {
                    yield return Toils_Reserve.Reserve(OtherPawnIndex);
                }

                yield return Toils_Goto.GotoThing(PowerDestIndex, PathEndMode.OnCell).FailOnSomeonePhysicallyInteracting(PowerDestIndex);
                yield return Toils_Reserve.Release(PowerDestIndex);
            }
            else
            {
                yield return new Toil()
                {
                    initAction = delegate ()
                    {
                        if (!thingIsSplitOff && pawn.carryTracker.CarriedThing != TargetThingA)
                        {
                            Thing splitOffThing = TargetThingA.SplitOff(job.count);
                            thingIsSplitOff = true;
                            GenPlace.TryPlaceThing(splitOffThing, pawn.Position, pawn.Map, ThingPlaceMode.Near);

                            TargetThingA = splitOffThing;
                        }
                    }
                };
            }

            yield return Toils_Haul.StartCarryThing(TargetIndex.A, false, true, false);
            this.AddFinishAction(delegate ()
            {
                if (pawn.carryTracker is Pawn_CarryTracker carryTracker && carryTracker.CarriedThing is Thing thing)
                {
                    if (isUsedFromInventory)
                    {
                        pawn.inventory.innerContainer.TryAddOrTransfer(thing, true);
                    }
                    else
                    {
                        carryTracker.TryDropCarriedThing(pawn.Position, ThingPlaceMode.Near, out Thing resultThing);
                    }
                }
            });

            if (TargetB.IsValid)
            {
                yield return Toils_Goto.GotoThing(OtherPawnIndex, PathEndMode.Touch).FailOnForbidden(OtherPawnIndex);

                yield return Toils_General.Wait(100).WithProgressBarToilDelay(TargetIndex.A, false);

                Toil rechargeToil = new Toil();
                rechargeToil.AddFinishAction(delegate ()
                {
                    Thing carriedThing = pawn.carryTracker.CarriedThing;
                    if (carriedThing != null)
                    {
                        Comp_EnergySource energyComp = carriedThing.TryGetComp<Comp_EnergySource>();
                        if (energyComp != null)
                            energyComp.RechargeEnergyNeed((Pawn)TargetB.Thing);

                        pawn.carryTracker.DestroyCarriedThing();
                    }
                });

                yield return rechargeToil;
                yield return Toils_Reserve.Release(OtherPawnIndex);
            }
            else
            {
                yield return Toils_General.Wait(100).WithProgressBarToilDelay(TargetIndex.A, false);

                Toil rechargeToil = new Toil();
                rechargeToil.AddFinishAction(delegate ()
                {
                    Thing carriedThing = pawn.carryTracker.CarriedThing;
                    if (carriedThing != null)
                    {
                        Comp_EnergySource energyComp = carriedThing.TryGetComp<Comp_EnergySource>();
                        if (energyComp != null)
                            energyComp.RechargeEnergyNeed(pawn);

                        pawn.carryTracker.DestroyCarriedThing();
                    }
                });

                yield return rechargeToil;
            }
        }
    }
}
