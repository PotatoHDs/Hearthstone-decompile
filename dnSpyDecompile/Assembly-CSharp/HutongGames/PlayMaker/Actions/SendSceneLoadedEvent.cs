using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D92 RID: 3474
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when a scene was loaded. Use the Safe version when you want to access content of the loaded scene. Use GetSceneloadedEventData to find out about the loaded Scene and load mode")]
	public class SendSceneLoadedEvent : FsmStateAction
	{
		// Token: 0x0600A4E0 RID: 42208 RVA: 0x003449B9 File Offset: 0x00342BB9
		public override void Reset()
		{
			this.sceneLoaded = null;
		}

		// Token: 0x0600A4E1 RID: 42209 RVA: 0x003449C2 File Offset: 0x00342BC2
		public override void OnEnter()
		{
			this._loaded = -1;
			SceneManager.sceneLoaded += this.SceneManager_sceneLoaded;
		}

		// Token: 0x0600A4E2 RID: 42210 RVA: 0x003449DC File Offset: 0x00342BDC
		private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
		{
			SendSceneLoadedEvent.lastLoadedScene = scene;
			SendSceneLoadedEvent.lastLoadedMode = mode;
			base.Fsm.Event(this.sceneLoaded);
			this._loaded = Time.frameCount;
			if (this.sceneLoadedSafe == null)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4E3 RID: 42211 RVA: 0x00344A14 File Offset: 0x00342C14
		public override void OnUpdate()
		{
			if (this._loaded > -1 && Time.frameCount > this._loaded)
			{
				this._loaded = -1;
				base.Fsm.Event(this.sceneLoadedSafe);
				base.Finish();
			}
		}

		// Token: 0x0600A4E4 RID: 42212 RVA: 0x00344A4A File Offset: 0x00342C4A
		public override void OnExit()
		{
			SceneManager.sceneLoaded -= this.SceneManager_sceneLoaded;
		}

		// Token: 0x0600A4E5 RID: 42213 RVA: 0x00344A5D File Offset: 0x00342C5D
		public override string ErrorCheck()
		{
			if (this.sceneLoaded == null && this.sceneLoadedSafe == null)
			{
				return "At least one event setup is required";
			}
			return string.Empty;
		}

		// Token: 0x04008B5D RID: 35677
		[Tooltip("The event to send when a scene was loaded")]
		public FsmEvent sceneLoaded;

		// Token: 0x04008B5E RID: 35678
		[Tooltip("The event to send when a scene was loaded, with a one frame delay to make sure the scene content was indeed initialized fully")]
		public FsmEvent sceneLoadedSafe;

		// Token: 0x04008B5F RID: 35679
		public static Scene lastLoadedScene;

		// Token: 0x04008B60 RID: 35680
		public static LoadSceneMode lastLoadedMode;

		// Token: 0x04008B61 RID: 35681
		private int _loaded = -1;
	}
}
