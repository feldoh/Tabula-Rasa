﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21Toolbox.Laser
{
    public abstract class SpinningLaserGunBase : LaserGun
    {
        public enum State
        {
            Idle = 0,
            Spinup = 1,
            Spinning = 2
        };

        int previousTick = 0;
        public State state = State.Idle;

        float rotation = 0;
        float rotationSpeed = 0;
        float targetRotationSpeed;
        float rotationAcceleration = 0;
        int rotationAccelerationTicksRemaing = 0;

        public new SpinningLaserGunDef def
        {
            get { return base.def as SpinningLaserGunDef; }
        }

        public void ReachRotationSpeed(float target, int ticksUntil)
        {
            targetRotationSpeed = target;

            if (ticksUntil <= 0)
            {
                rotationAccelerationTicksRemaing = 0;
                rotationSpeed = target;
            }

            rotationAccelerationTicksRemaing = ticksUntil;
            rotationAcceleration = (target - rotationSpeed) / ticksUntil;
        }

        private Graphic GetGraphicForTick(int ticksPassed)
        {
            if (rotationAccelerationTicksRemaing > 0)
            {
                if (ticksPassed > rotationAccelerationTicksRemaing)
                    ticksPassed = rotationAccelerationTicksRemaing;

                rotationAccelerationTicksRemaing -= ticksPassed;
                rotationSpeed += ticksPassed * rotationAcceleration;

                if (rotationAccelerationTicksRemaing <= 0)
                {
                    rotationSpeed = targetRotationSpeed;
                }
            }

            rotation += rotationSpeed * ticksPassed;

            int frame = ((int)rotation) % def.frames.Count;
            return def.frames[frame].Graphic;
        }

        public abstract void UpdateState();

        public override Graphic Graphic
        {
            get
            {
                if (def.frames.Count == 0) return DefaultGraphic;

                UpdateState();

                var tick = Find.TickManager.TicksGame;
                var res = GetGraphicForTick(tick - previousTick);
                previousTick = tick;

                return res;
            }
        }
    }
}
