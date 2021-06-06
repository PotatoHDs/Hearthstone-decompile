using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7D RID: 3453
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Create an empty new scene with the given name additively. The path of the new scene will be empty")]
	public class CreateScene : FsmStateAction
	{
		// Token: 0x0600A482 RID: 42114 RVA: 0x00343302 File Offset: 0x00341502
		public override void Reset()
		{
			this.sceneName = null;
		}

		// Token: 0x0600A483 RID: 42115 RVA: 0x0034330B File Offset: 0x0034150B
		public override void OnEnter()
		{
			SceneManager.CreateScene(this.sceneName.Value);
			base.Finish();
		}

		// Token: 0x04008ADC RID: 35548
		[RequiredField]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmString sceneName;
	}
}
