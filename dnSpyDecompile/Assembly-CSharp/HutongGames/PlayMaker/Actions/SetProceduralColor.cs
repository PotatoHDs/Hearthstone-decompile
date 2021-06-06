using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D37 RID: 3383
	[ActionCategory("Substance")]
	[Tooltip("Set a named color property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralColor : FsmStateAction
	{
		// Token: 0x0600A305 RID: 41733 RVA: 0x0033E485 File Offset: 0x0033C685
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.colorProperty = "";
			this.colorValue = Color.white;
			this.everyFrame = false;
		}

		// Token: 0x0600A306 RID: 41734 RVA: 0x0033E4B5 File Offset: 0x0033C6B5
		public override void OnEnter()
		{
			this.DoSetProceduralFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A307 RID: 41735 RVA: 0x0033E4CB File Offset: 0x0033C6CB
		public override void OnUpdate()
		{
			this.DoSetProceduralFloat();
		}

		// Token: 0x0600A308 RID: 41736 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetProceduralFloat()
		{
		}

		// Token: 0x04008961 RID: 35169
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		// Token: 0x04008962 RID: 35170
		[RequiredField]
		[Tooltip("The named color property in the material.")]
		public FsmString colorProperty;

		// Token: 0x04008963 RID: 35171
		[RequiredField]
		[Tooltip("The value to set the property to.")]
		public FsmColor colorValue;

		// Token: 0x04008964 RID: 35172
		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;
	}
}
