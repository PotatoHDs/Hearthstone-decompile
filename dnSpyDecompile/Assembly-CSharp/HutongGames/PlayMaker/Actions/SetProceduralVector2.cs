using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D39 RID: 3385
	[ActionCategory("Substance")]
	[Tooltip("Set a named Vector2 property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralVector2 : FsmStateAction
	{
		// Token: 0x0600A30F RID: 41743 RVA: 0x0033E521 File Offset: 0x0033C721
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.vector2Property = null;
			this.vector2Value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A310 RID: 41744 RVA: 0x0033E53F File Offset: 0x0033C73F
		public override void OnEnter()
		{
			this.DoSetProceduralVector();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A311 RID: 41745 RVA: 0x0033E555 File Offset: 0x0033C755
		public override void OnUpdate()
		{
			this.DoSetProceduralVector();
		}

		// Token: 0x0600A312 RID: 41746 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoSetProceduralVector()
		{
		}

		// Token: 0x04008969 RID: 35177
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		// Token: 0x0400896A RID: 35178
		[RequiredField]
		[Tooltip("The named vector property in the material.")]
		public FsmString vector2Property;

		// Token: 0x0400896B RID: 35179
		[RequiredField]
		[Tooltip("The Vector3 value to set the property to.\nNOTE: Use Set Procedural Vector2 for Vector3 values.")]
		public FsmVector2 vector2Value;

		// Token: 0x0400896C RID: 35180
		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;
	}
}
