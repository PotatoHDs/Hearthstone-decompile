using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D80 RID: 3456
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the total number of currently loaded scenes.")]
	public class GetSceneCount : FsmStateAction
	{
		// Token: 0x0600A48E RID: 42126 RVA: 0x003436AE File Offset: 0x003418AE
		public override void Reset()
		{
			this.sceneCount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A48F RID: 42127 RVA: 0x003436BE File Offset: 0x003418BE
		public override void OnEnter()
		{
			this.DoGetSceneCount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A490 RID: 42128 RVA: 0x003436D4 File Offset: 0x003418D4
		public override void OnUpdate()
		{
			this.DoGetSceneCount();
		}

		// Token: 0x0600A491 RID: 42129 RVA: 0x003436DC File Offset: 0x003418DC
		private void DoGetSceneCount()
		{
			this.sceneCount.Value = SceneManager.sceneCount;
		}

		// Token: 0x04008AEF RID: 35567
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of currently loaded scenes.")]
		public FsmInt sceneCount;

		// Token: 0x04008AF0 RID: 35568
		[Tooltip("Repeat every Frame")]
		public bool everyFrame;
	}
}
