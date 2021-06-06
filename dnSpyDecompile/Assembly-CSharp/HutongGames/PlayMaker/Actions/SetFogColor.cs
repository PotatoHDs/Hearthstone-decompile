using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB9 RID: 3513
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the color of the Fog in the scene.")]
	public class SetFogColor : FsmStateAction
	{
		// Token: 0x0600A58E RID: 42382 RVA: 0x00346E27 File Offset: 0x00345027
		public override void Reset()
		{
			this.fogColor = Color.white;
			this.everyFrame = false;
		}

		// Token: 0x0600A58F RID: 42383 RVA: 0x00346E40 File Offset: 0x00345040
		public override void OnEnter()
		{
			this.DoSetFogColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A590 RID: 42384 RVA: 0x00346E56 File Offset: 0x00345056
		public override void OnUpdate()
		{
			this.DoSetFogColor();
		}

		// Token: 0x0600A591 RID: 42385 RVA: 0x00346E5E File Offset: 0x0034505E
		private void DoSetFogColor()
		{
			RenderSettings.fogColor = this.fogColor.Value;
		}

		// Token: 0x04008C15 RID: 35861
		[RequiredField]
		public FsmColor fogColor;

		// Token: 0x04008C16 RID: 35862
		public bool everyFrame;
	}
}
