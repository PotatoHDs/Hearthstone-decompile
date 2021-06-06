using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D91 RID: 3473
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when the active scene has changed.")]
	public class SendActiveSceneChangedEvent : FsmStateAction
	{
		// Token: 0x0600A4DB RID: 42203 RVA: 0x0034495F File Offset: 0x00342B5F
		public override void Reset()
		{
			this.activeSceneChanged = null;
		}

		// Token: 0x0600A4DC RID: 42204 RVA: 0x00344968 File Offset: 0x00342B68
		public override void OnEnter()
		{
			SceneManager.activeSceneChanged += this.SceneManager_activeSceneChanged;
			base.Finish();
		}

		// Token: 0x0600A4DD RID: 42205 RVA: 0x00344981 File Offset: 0x00342B81
		private void SceneManager_activeSceneChanged(Scene previousActiveScene, Scene activeScene)
		{
			SendActiveSceneChangedEvent.lastNewActiveScene = activeScene;
			SendActiveSceneChangedEvent.lastPreviousActiveScene = previousActiveScene;
			base.Fsm.Event(this.activeSceneChanged);
			base.Finish();
		}

		// Token: 0x0600A4DE RID: 42206 RVA: 0x003449A6 File Offset: 0x00342BA6
		public override void OnExit()
		{
			SceneManager.activeSceneChanged -= this.SceneManager_activeSceneChanged;
		}

		// Token: 0x04008B5A RID: 35674
		[RequiredField]
		[Tooltip("The event to send when an active scene changed")]
		public FsmEvent activeSceneChanged;

		// Token: 0x04008B5B RID: 35675
		public static Scene lastPreviousActiveScene;

		// Token: 0x04008B5C RID: 35676
		public static Scene lastNewActiveScene;
	}
}
