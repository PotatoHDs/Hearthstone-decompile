using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3A RID: 3386
	[ActionCategory("Substance")]
	[Tooltip("Set a named Vector3 property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralVector3 : FsmStateAction
	{
		// Token: 0x0600A314 RID: 41748 RVA: 0x0033E55D File Offset: 0x0033C75D
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.vector3Property = null;
			this.vector3Value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A315 RID: 41749 RVA: 0x0033E57B File Offset: 0x0033C77B
		public override void OnEnter()
		{
			this.DoSetProceduralVector();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A316 RID: 41750 RVA: 0x0033E591 File Offset: 0x0033C791
		public override void OnUpdate()
		{
			this.DoSetProceduralVector();
		}

		// Token: 0x0600A317 RID: 41751 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetProceduralVector()
		{
		}

		// Token: 0x0400896D RID: 35181
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		// Token: 0x0400896E RID: 35182
		[RequiredField]
		[Tooltip("The named vector property in the material.")]
		public FsmString vector3Property;

		// Token: 0x0400896F RID: 35183
		[RequiredField]
		[Tooltip("The value to set the property to.\nNOTE: Use Set Procedural Vector3 for Vector3 values.")]
		public FsmVector3 vector3Value;

		// Token: 0x04008970 RID: 35184
		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;
	}
}
