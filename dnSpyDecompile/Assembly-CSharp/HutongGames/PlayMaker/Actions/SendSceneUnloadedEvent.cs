using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D93 RID: 3475
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when a scene was unloaded.")]
	public class SendSceneUnloadedEvent : FsmStateAction
	{
		// Token: 0x0600A4E7 RID: 42215 RVA: 0x00344A89 File Offset: 0x00342C89
		public override void Reset()
		{
			this.sceneUnloaded = null;
		}

		// Token: 0x0600A4E8 RID: 42216 RVA: 0x00344A92 File Offset: 0x00342C92
		public override void OnEnter()
		{
			SceneManager.sceneUnloaded += this.SceneManager_sceneUnloaded;
			base.Finish();
		}

		// Token: 0x0600A4E9 RID: 42217 RVA: 0x00344AAB File Offset: 0x00342CAB
		private void SceneManager_sceneUnloaded(Scene scene)
		{
			Debug.Log(scene.name);
			SendSceneUnloadedEvent.lastUnLoadedScene = scene;
			base.Fsm.Event(this.sceneUnloaded);
			base.Finish();
		}

		// Token: 0x0600A4EA RID: 42218 RVA: 0x00344AD6 File Offset: 0x00342CD6
		public override void OnExit()
		{
			SceneManager.sceneUnloaded -= this.SceneManager_sceneUnloaded;
		}

		// Token: 0x04008B62 RID: 35682
		[RequiredField]
		[Tooltip("The event to send when scene was unloaded")]
		public FsmEvent sceneUnloaded;

		// Token: 0x04008B63 RID: 35683
		public static Scene lastUnLoadedScene;
	}
}
