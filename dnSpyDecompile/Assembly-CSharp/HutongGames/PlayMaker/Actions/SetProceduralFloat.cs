using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D38 RID: 3384
	[ActionCategory("Substance")]
	[Tooltip("Set a named float property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralFloat : FsmStateAction
	{
		// Token: 0x0600A30A RID: 41738 RVA: 0x0033E4D3 File Offset: 0x0033C6D3
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.floatProperty = "";
			this.floatValue = 0f;
			this.everyFrame = false;
		}

		// Token: 0x0600A30B RID: 41739 RVA: 0x0033E503 File Offset: 0x0033C703
		public override void OnEnter()
		{
			this.DoSetProceduralFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A30C RID: 41740 RVA: 0x0033E519 File Offset: 0x0033C719
		public override void OnUpdate()
		{
			this.DoSetProceduralFloat();
		}

		// Token: 0x0600A30D RID: 41741 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetProceduralFloat()
		{
		}

		// Token: 0x04008965 RID: 35173
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		// Token: 0x04008966 RID: 35174
		[RequiredField]
		[Tooltip("The named float property in the material.")]
		public FsmString floatProperty;

		// Token: 0x04008967 RID: 35175
		[RequiredField]
		[Tooltip("The value to set the property to.")]
		public FsmFloat floatValue;

		// Token: 0x04008968 RID: 35176
		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;
	}
}
