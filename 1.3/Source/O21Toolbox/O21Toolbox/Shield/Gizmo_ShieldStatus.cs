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
	[StaticConstructorOnStartup]
	public class Gizmo_ShieldStatus : Gizmo
	{
		public Comp_ShieldBuilding shield;

		private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));

		private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
		public Gizmo_ShieldStatus()
		{
			this.order = -100f;
		}

		public override float GetWidth(float maxWidth)
		{
			return 140f;
		}

        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect rect = new Rect(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
			Rect rect2 = rect.ContractedBy(6f);
			Widgets.DrawWindowBackground(rect);
			Rect rect3 = rect2;
			rect3.height = rect.height / 2f;
			Text.Font = GameFont.Tiny;
			Widgets.Label(rect3, this.shield.Props.stressLabel);
			Rect rect4 = rect2;
			rect4.yMin = rect2.y + rect2.height / 2f;
			float fillPercent = this.shield.CurStressLevel / Mathf.Max(1f, shield.MaxStressLevel);
			Widgets.FillableBar(rect4, fillPercent, Gizmo_ShieldStatus.FullShieldBarTex, Gizmo_ShieldStatus.EmptyShieldBarTex, false);
			Text.Font = GameFont.Small;
			Text.Anchor = TextAnchor.MiddleCenter;
			Widgets.Label(rect4, (this.shield.CurStressLevel * 100f).ToString("F0") + " / " + (shield.MaxStressLevel * 100f).ToString("F0"));
			Text.Anchor = TextAnchor.UpperLeft;
			return new GizmoResult(GizmoState.Clear);
		}
	}
}
