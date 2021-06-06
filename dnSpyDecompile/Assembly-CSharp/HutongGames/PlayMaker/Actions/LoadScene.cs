using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8D RID: 3469
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Loads the scene by its name or index in Build Settings. ")]
	public class LoadScene : FsmStateAction
	{
		// Token: 0x0600A4C7 RID: 42183 RVA: 0x0034412C File Offset: 0x0034232C
		public override void Reset()
		{
			this.sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			this.sceneByName = null;
			this.sceneAtIndex = null;
			this.loadSceneMode = null;
			this.success = null;
			this.successEvent = null;
			this.failureEvent = null;
		}

		// Token: 0x0600A4C8 RID: 42184 RVA: 0x00344160 File Offset: 0x00342360
		public override void OnEnter()
		{
			bool flag = this.DoLoadScene();
			if (!this.success.IsNone)
			{
				this.success.Value = flag;
			}
			if (flag)
			{
				base.Fsm.Event(this.successEvent);
			}
			else
			{
				base.Fsm.Event(this.failureEvent);
			}
			base.Finish();
		}

		// Token: 0x0600A4C9 RID: 42185 RVA: 0x003441BC File Offset: 0x003423BC
		private bool DoLoadScene()
		{
			if (this.sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex)
			{
				if (SceneManager.GetActiveScene().buildIndex == this.sceneAtIndex.Value)
				{
					return false;
				}
				SceneManager.LoadScene(this.sceneAtIndex.Value, (LoadSceneMode)this.loadSceneMode.Value);
			}
			else
			{
				if (SceneManager.GetActiveScene().name == this.sceneByName.Value)
				{
					return false;
				}
				SceneManager.LoadScene(this.sceneByName.Value, (LoadSceneMode)this.loadSceneMode.Value);
			}
			return true;
		}

		// Token: 0x04008B2A RID: 35626
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		// Token: 0x04008B2B RID: 35627
		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		// Token: 0x04008B2C RID: 35628
		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B2D RID: 35629
		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		// Token: 0x04008B2E RID: 35630
		[ActionSection("Result")]
		[Tooltip("True if the scene was loaded")]
		public FsmBool success;

		// Token: 0x04008B2F RID: 35631
		[Tooltip("Event sent if the scene was loaded")]
		public FsmEvent successEvent;

		// Token: 0x04008B30 RID: 35632
		[Tooltip("Event sent if a problem occurred, check log for information")]
		public FsmEvent failureEvent;
	}
}
