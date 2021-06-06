using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D35 RID: 3381
	[ActionCategory("Substance")]
	[Tooltip("Rebuilds all dirty textures. By default the rebuild is spread over multiple frames so it won't halt the game. Check Immediately to rebuild all textures in a single frame.")]
	public class RebuildTextures : FsmStateAction
	{
		// Token: 0x0600A2FB RID: 41723 RVA: 0x0033E401 File Offset: 0x0033C601
		public override void Reset()
		{
			this.substanceMaterial = null;
			this.immediately = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A2FC RID: 41724 RVA: 0x0033E41D File Offset: 0x0033C61D
		public override void OnEnter()
		{
			this.DoRebuildTextures();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A2FD RID: 41725 RVA: 0x0033E433 File Offset: 0x0033C633
		public override void OnUpdate()
		{
			this.DoRebuildTextures();
		}

		// Token: 0x0600A2FE RID: 41726 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoRebuildTextures()
		{
		}

		// Token: 0x0400895A RID: 35162
		[RequiredField]
		public FsmMaterial substanceMaterial;

		// Token: 0x0400895B RID: 35163
		[RequiredField]
		public FsmBool immediately;

		// Token: 0x0400895C RID: 35164
		public bool everyFrame;
	}
}
