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
    public class DefModExt_BigBox : DefModExtension
    {
        public Vector2 size;
        public Vector3 offset;
        public bool directionBased;
        public Vector2 westSize;
        public Vector3 westOffset;
        public Vector2 northSize;
        public Vector3 northOffset;
        public Vector2 eastSize;
        public Vector3 eastOffset;
        public Vector2 southSize;
        public Vector3 southOffset;
    }
}
