using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D36 RID: 3382
	[ActionCategory("Substance")]
	[Tooltip("Set a named bool property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralBoolean : FsmStateAction
	{
		// Token: 0x0600A300 RID: 41728 RVA: 0x0033E43B File Offset: 0x0033C63B
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.boolProperty = "";
			this.boolValue = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A301 RID: 41729 RVA: 0x0033E467 File Offset: 0x0033C667
		public override void OnEnter()
		{
			this.DoSetProceduralFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A302 RID: 41730 RVA: 0x0033E47D File Offset: 0x0033C67D
		public override void OnUpdate()
		{
			this.DoSetProceduralFloat();
		}

		// Token: 0x0600A303 RID: 41731 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetProceduralFloat()
		{
		}

		// Token: 0x0400895D RID: 35165
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		// Token: 0x0400895E RID: 35166
		[RequiredField]
		[Tooltip("The named bool property in the material.")]
		public FsmString boolProperty;

		// Token: 0x0400895F RID: 35167
		[RequiredField]
		[Tooltip("The value to set the property to.")]
		public FsmBool boolValue;

		// Token: 0x04008960 RID: 35168
		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;
	}
}
