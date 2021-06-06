using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CEB RID: 3307
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Loads a Level by Name. NOTE: Before you can load a level, you have to add it to the list of levels defined in File->Build Settings...")]
	public class LoadLevel : FsmStateAction
	{
		// Token: 0x0600A17F RID: 41343 RVA: 0x00337C06 File Offset: 0x00335E06
		public override void Reset()
		{
			this.levelName = "";
			this.additive = false;
			this.async = false;
			this.loadedEvent = null;
			this.dontDestroyOnLoad = false;
		}

		// Token: 0x0600A180 RID: 41344 RVA: 0x00337C3C File Offset: 0x00335E3C
		public override void OnEnter()
		{
			if (!Application.CanStreamedLevelBeLoaded(this.levelName.Value))
			{
				base.Fsm.Event(this.failedEvent);
				base.Finish();
				return;
			}
			if (this.dontDestroyOnLoad.Value)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.Owner.transform.root.gameObject);
			}
			if (this.additive)
			{
				if (this.async)
				{
					this.asyncOperation = SceneManager.LoadSceneAsync(this.levelName.Value, LoadSceneMode.Additive);
					Debug.Log("LoadLevelAdditiveAsyc: " + this.levelName.Value);
					return;
				}
				SceneManager.LoadScene(this.levelName.Value, LoadSceneMode.Additive);
				Debug.Log("LoadLevelAdditive: " + this.levelName.Value);
			}
			else
			{
				if (this.async)
				{
					this.asyncOperation = SceneManager.LoadSceneAsync(this.levelName.Value, LoadSceneMode.Single);
					Debug.Log("LoadLevelAsync: " + this.levelName.Value);
					return;
				}
				SceneManager.LoadScene(this.levelName.Value, LoadSceneMode.Single);
				Debug.Log("LoadLevel: " + this.levelName.Value);
			}
			base.Log("LOAD COMPLETE");
			base.Fsm.Event(this.loadedEvent);
			base.Finish();
		}

		// Token: 0x0600A181 RID: 41345 RVA: 0x00337D90 File Offset: 0x00335F90
		public override void OnUpdate()
		{
			if (this.asyncOperation.isDone)
			{
				base.Fsm.Event(this.loadedEvent);
				base.Finish();
			}
		}

		// Token: 0x0400877A RID: 34682
		[RequiredField]
		[Tooltip("The name of the level to load. NOTE: Must be in the list of levels defined in File->Build Settings... ")]
		public FsmString levelName;

		// Token: 0x0400877B RID: 34683
		[Tooltip("Load the level additively, keeping the current scene.")]
		public bool additive;

		// Token: 0x0400877C RID: 34684
		[Tooltip("Load the level asynchronously in the background.")]
		public bool async;

		// Token: 0x0400877D RID: 34685
		[Tooltip("Event to send when the level has loaded. NOTE: This only makes sense if the FSM is still in the scene!")]
		public FsmEvent loadedEvent;

		// Token: 0x0400877E RID: 34686
		[Tooltip("Keep this GameObject in the new level. NOTE: The GameObject and components is disabled then enabled on load; uncheck Reset On Disable to keep the active state.")]
		public FsmBool dontDestroyOnLoad;

		// Token: 0x0400877F RID: 34687
		[Tooltip("Event to send if the level cannot be loaded.")]
		public FsmEvent failedEvent;

		// Token: 0x04008780 RID: 34688
		private AsyncOperation asyncOperation;
	}
}
