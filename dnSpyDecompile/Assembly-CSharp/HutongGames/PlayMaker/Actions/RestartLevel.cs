using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D73 RID: 3443
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Restarts current level.")]
	public class RestartLevel : FsmStateAction
	{
		// Token: 0x0600A440 RID: 42048 RVA: 0x00342818 File Offset: 0x00340A18
		public override void OnEnter()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
			base.Finish();
		}
	}
}
