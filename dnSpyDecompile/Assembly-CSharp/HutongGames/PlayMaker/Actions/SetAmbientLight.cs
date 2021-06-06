using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA5 RID: 3493
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the Ambient Light Color for the scene.")]
	public class SetAmbientLight : FsmStateAction
	{
		// Token: 0x0600A534 RID: 42292 RVA: 0x003461B9 File Offset: 0x003443B9
		public override void Reset()
		{
			this.ambientColor = Color.gray;
			this.everyFrame = false;
		}

		// Token: 0x0600A535 RID: 42293 RVA: 0x003461D2 File Offset: 0x003443D2
		public override void OnEnter()
		{
			this.DoSetAmbientColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A536 RID: 42294 RVA: 0x003461E8 File Offset: 0x003443E8
		public override void OnUpdate()
		{
			this.DoSetAmbientColor();
		}

		// Token: 0x0600A537 RID: 42295 RVA: 0x003461F0 File Offset: 0x003443F0
		private void DoSetAmbientColor()
		{
			RenderSettings.ambientLight = this.ambientColor.Value;
		}

		// Token: 0x04008BCD RID: 35789
		[RequiredField]
		public FsmColor ambientColor;

		// Token: 0x04008BCE RID: 35790
		public bool everyFrame;
	}
}
