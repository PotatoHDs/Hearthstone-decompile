using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D81 RID: 3457
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the number of scenes in Build Settings.")]
	public class GetSceneCountInBuildSettings : FsmStateAction
	{
		// Token: 0x0600A493 RID: 42131 RVA: 0x003436EE File Offset: 0x003418EE
		public override void Reset()
		{
			this.sceneCountInBuildSettings = null;
		}

		// Token: 0x0600A494 RID: 42132 RVA: 0x003436F7 File Offset: 0x003418F7
		public override void OnEnter()
		{
			this.DoGetSceneCountInBuildSettings();
			base.Finish();
		}

		// Token: 0x0600A495 RID: 42133 RVA: 0x00343705 File Offset: 0x00341905
		private void DoGetSceneCountInBuildSettings()
		{
			this.sceneCountInBuildSettings.Value = SceneManager.sceneCountInBuildSettings;
		}

		// Token: 0x04008AF1 RID: 35569
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of scenes in Build Settings.")]
		public FsmInt sceneCountInBuildSettings;
	}
}
