using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CEC RID: 3308
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Loads a Level by Index number. Before you can load a level, you have to add it to the list of levels defined in File->Build Settings...")]
	public class LoadLevelNum : FsmStateAction
	{
		// Token: 0x0600A183 RID: 41347 RVA: 0x00337DB6 File Offset: 0x00335FB6
		public override void Reset()
		{
			this.levelIndex = null;
			this.additive = false;
			this.loadedEvent = null;
			this.dontDestroyOnLoad = false;
		}

		// Token: 0x0600A184 RID: 41348 RVA: 0x00337DDC File Offset: 0x00335FDC
		public override void OnEnter()
		{
			if (!Application.CanStreamedLevelBeLoaded(this.levelIndex.Value))
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
				SceneManager.LoadScene(this.levelIndex.Value, LoadSceneMode.Additive);
			}
			else
			{
				SceneManager.LoadScene(this.levelIndex.Value, LoadSceneMode.Single);
			}
			base.Fsm.Event(this.loadedEvent);
			base.Finish();
		}

		// Token: 0x04008781 RID: 34689
		[RequiredField]
		[Tooltip("The level index in File->Build Settings")]
		public FsmInt levelIndex;

		// Token: 0x04008782 RID: 34690
		[Tooltip("Load the level additively, keeping the current scene.")]
		public bool additive;

		// Token: 0x04008783 RID: 34691
		[Tooltip("Event to send after the level is loaded.")]
		public FsmEvent loadedEvent;

		// Token: 0x04008784 RID: 34692
		[Tooltip("Keep this GameObject in the new level. NOTE: The GameObject and components is disabled then enabled on load; uncheck Reset On Disable to keep the active state.")]
		public FsmBool dontDestroyOnLoad;

		// Token: 0x04008785 RID: 34693
		[Tooltip("Event to send if the level cannot be loaded.")]
		public FsmEvent failedEvent;
	}
}
