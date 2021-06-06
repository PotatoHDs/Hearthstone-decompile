using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D95 RID: 3477
	[Obsolete("Use UnloadSceneAsynch Instead")]
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Unload Scene. Note that assets are currently not unloaded, in order to free up asset memory call Resources.UnloadUnusedAssets.")]
	public class UnloadScene : FsmStateAction
	{
		// Token: 0x0600A4F0 RID: 42224 RVA: 0x00344CD8 File Offset: 0x00342ED8
		public override void Reset()
		{
			this.sceneReference = UnloadScene.SceneReferenceOptions.SceneAtBuildIndex;
			this.sceneByName = null;
			this.sceneAtBuildIndex = null;
			this.sceneAtIndex = null;
			this.sceneByPath = null;
			this.sceneByGameObject = null;
			this.unloaded = null;
			this.unloadedEvent = null;
			this.failureEvent = null;
		}

		// Token: 0x0600A4F1 RID: 42225 RVA: 0x00344D24 File Offset: 0x00342F24
		public override void OnEnter()
		{
			bool flag = false;
			try
			{
				switch (this.sceneReference)
				{
				case UnloadScene.SceneReferenceOptions.ActiveScene:
					flag = SceneManager.UnloadScene(SceneManager.GetActiveScene());
					break;
				case UnloadScene.SceneReferenceOptions.SceneAtBuildIndex:
					flag = SceneManager.UnloadScene(this.sceneAtBuildIndex.Value);
					break;
				case UnloadScene.SceneReferenceOptions.SceneAtIndex:
					flag = SceneManager.UnloadScene(SceneManager.GetSceneAt(this.sceneAtIndex.Value));
					break;
				case UnloadScene.SceneReferenceOptions.SceneByName:
					flag = SceneManager.UnloadScene(this.sceneByName.Value);
					break;
				case UnloadScene.SceneReferenceOptions.SceneByPath:
					flag = SceneManager.UnloadScene(SceneManager.GetSceneByPath(this.sceneByPath.Value));
					break;
				case UnloadScene.SceneReferenceOptions.SceneByGameObject:
				{
					GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.sceneByGameObject);
					if (ownerDefaultTarget == null)
					{
						throw new Exception("Null GameObject");
					}
					flag = SceneManager.UnloadScene(ownerDefaultTarget.scene);
					break;
				}
				}
			}
			catch (Exception ex)
			{
				base.LogError(ex.Message);
			}
			if (!this.unloaded.IsNone)
			{
				this.unloaded.Value = flag;
			}
			if (flag)
			{
				base.Fsm.Event(this.unloadedEvent);
			}
			else
			{
				base.Fsm.Event(this.failureEvent);
			}
			base.Finish();
		}

		// Token: 0x0600A4F2 RID: 42226 RVA: 0x00344E5C File Offset: 0x0034305C
		public override string ErrorCheck()
		{
			switch (this.sceneReference)
			{
			default:
				return string.Empty;
			}
		}

		// Token: 0x04008B72 RID: 35698
		[Tooltip("The reference options of the Scene")]
		public UnloadScene.SceneReferenceOptions sceneReference;

		// Token: 0x04008B73 RID: 35699
		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		// Token: 0x04008B74 RID: 35700
		[Tooltip("The build index of the scene to unload.")]
		public FsmInt sceneAtBuildIndex;

		// Token: 0x04008B75 RID: 35701
		[Tooltip("The index of the scene to unload.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B76 RID: 35702
		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		// Token: 0x04008B77 RID: 35703
		[Tooltip("The GameObject unload scene of")]
		public FsmOwnerDefault sceneByGameObject;

		// Token: 0x04008B78 RID: 35704
		[ActionSection("Result")]
		[Tooltip("True if scene was unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmBool unloaded;

		// Token: 0x04008B79 RID: 35705
		[Tooltip("Event sent if scene was unloaded ")]
		public FsmEvent unloadedEvent;

		// Token: 0x04008B7A RID: 35706
		[Tooltip("Event sent scene was not unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmEvent failureEvent;

		// Token: 0x020027A9 RID: 10153
		public enum SceneReferenceOptions
		{
			// Token: 0x0400F518 RID: 62744
			ActiveScene,
			// Token: 0x0400F519 RID: 62745
			SceneAtBuildIndex,
			// Token: 0x0400F51A RID: 62746
			SceneAtIndex,
			// Token: 0x0400F51B RID: 62747
			SceneByName,
			// Token: 0x0400F51C RID: 62748
			SceneByPath,
			// Token: 0x0400F51D RID: 62749
			SceneByGameObject
		}
	}
}
