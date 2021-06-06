using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D96 RID: 3478
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Unload a scene asynchronously by its name or index in Build Settings. Destroys all GameObjects associated with the given scene and removes the scene from the SceneManager.")]
	public class UnloadSceneAsynch : FsmStateAction
	{
		// Token: 0x0600A4F4 RID: 42228 RVA: 0x00344E94 File Offset: 0x00343094
		public override void Reset()
		{
			this.sceneReference = UnloadSceneAsynch.SceneReferenceOptions.SceneAtBuildIndex;
			this.sceneByName = null;
			this.sceneAtBuildIndex = null;
			this.sceneAtIndex = null;
			this.sceneByPath = null;
			this.sceneByGameObject = null;
			this.operationPriority = new FsmInt
			{
				UseVariable = true
			};
			this.isDone = null;
			this.progress = null;
			this.doneEvent = null;
			this.sceneNotFoundEvent = null;
		}

		// Token: 0x0600A4F5 RID: 42229 RVA: 0x00344EF9 File Offset: 0x003430F9
		public override void OnEnter()
		{
			this.isDone.Value = false;
			this.progress.Value = 0f;
			if (!this.DoUnLoadAsynch())
			{
				base.Fsm.Event(this.sceneNotFoundEvent);
				base.Finish();
			}
		}

		// Token: 0x0600A4F6 RID: 42230 RVA: 0x00344F38 File Offset: 0x00343138
		private bool DoUnLoadAsynch()
		{
			try
			{
				switch (this.sceneReference)
				{
				case UnloadSceneAsynch.SceneReferenceOptions.ActiveScene:
					this._asyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
					break;
				case UnloadSceneAsynch.SceneReferenceOptions.SceneAtBuildIndex:
					this._asyncOperation = SceneManager.UnloadSceneAsync(this.sceneAtBuildIndex.Value);
					break;
				case UnloadSceneAsynch.SceneReferenceOptions.SceneAtIndex:
					this._asyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(this.sceneAtIndex.Value));
					break;
				case UnloadSceneAsynch.SceneReferenceOptions.SceneByName:
					this._asyncOperation = SceneManager.UnloadSceneAsync(this.sceneByName.Value);
					break;
				case UnloadSceneAsynch.SceneReferenceOptions.SceneByPath:
					this._asyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath(this.sceneByPath.Value));
					break;
				case UnloadSceneAsynch.SceneReferenceOptions.SceneByGameObject:
				{
					GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.sceneByGameObject);
					if (ownerDefaultTarget == null)
					{
						throw new Exception("Null GameObject");
					}
					this._asyncOperation = SceneManager.UnloadSceneAsync(ownerDefaultTarget.scene);
					break;
				}
				}
			}
			catch (Exception ex)
			{
				base.LogError(ex.Message);
				return false;
			}
			if (!this.operationPriority.IsNone)
			{
				this._asyncOperation.priority = this.operationPriority.Value;
			}
			return true;
		}

		// Token: 0x0600A4F7 RID: 42231 RVA: 0x00345074 File Offset: 0x00343274
		public override void OnUpdate()
		{
			if (this._asyncOperation == null)
			{
				return;
			}
			if (this._asyncOperation.isDone)
			{
				this.isDone.Value = true;
				this.progress.Value = this._asyncOperation.progress;
				this._asyncOperation = null;
				base.Fsm.Event(this.doneEvent);
				base.Finish();
				return;
			}
			this.progress.Value = this._asyncOperation.progress;
		}

		// Token: 0x0600A4F8 RID: 42232 RVA: 0x003450EE File Offset: 0x003432EE
		public override void OnExit()
		{
			this._asyncOperation = null;
		}

		// Token: 0x04008B7B RID: 35707
		[Tooltip("The reference options of the Scene")]
		public UnloadSceneAsynch.SceneReferenceOptions sceneReference;

		// Token: 0x04008B7C RID: 35708
		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		// Token: 0x04008B7D RID: 35709
		[Tooltip("The build index of the scene to unload.")]
		public FsmInt sceneAtBuildIndex;

		// Token: 0x04008B7E RID: 35710
		[Tooltip("The index of the scene to unload.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B7F RID: 35711
		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		// Token: 0x04008B80 RID: 35712
		[Tooltip("The GameObject unload scene of")]
		public FsmOwnerDefault sceneByGameObject;

		// Token: 0x04008B81 RID: 35713
		[Tooltip("lets you tweak in which order async operation calls will be performed. Leave to none for default")]
		public FsmInt operationPriority;

		// Token: 0x04008B82 RID: 35714
		[ActionSection("Result")]
		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		// Token: 0x04008B83 RID: 35715
		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		// Token: 0x04008B84 RID: 35716
		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		// Token: 0x04008B85 RID: 35717
		[Tooltip("Event sent if the scene to load was not found")]
		public FsmEvent sceneNotFoundEvent;

		// Token: 0x04008B86 RID: 35718
		private AsyncOperation _asyncOperation;

		// Token: 0x020027AA RID: 10154
		public enum SceneReferenceOptions
		{
			// Token: 0x0400F51F RID: 62751
			ActiveScene,
			// Token: 0x0400F520 RID: 62752
			SceneAtBuildIndex,
			// Token: 0x0400F521 RID: 62753
			SceneAtIndex,
			// Token: 0x0400F522 RID: 62754
			SceneByName,
			// Token: 0x0400F523 RID: 62755
			SceneByPath,
			// Token: 0x0400F524 RID: 62756
			SceneByGameObject
		}
	}
}
