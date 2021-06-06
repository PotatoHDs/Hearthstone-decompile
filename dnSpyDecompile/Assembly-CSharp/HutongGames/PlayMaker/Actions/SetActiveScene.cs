using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D94 RID: 3476
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Set the scene to be active.")]
	public class SetActiveScene : FsmStateAction
	{
		// Token: 0x0600A4EC RID: 42220 RVA: 0x00344AEC File Offset: 0x00342CEC
		public override void Reset()
		{
			this.sceneReference = SetActiveScene.SceneReferenceOptions.SceneAtIndex;
			this.sceneByName = null;
			this.sceneAtBuildIndex = null;
			this.sceneAtIndex = null;
			this.sceneByPath = null;
			this.sceneByGameObject = null;
			this.success = null;
			this.successEvent = null;
			this.sceneFound = null;
			this.sceneNotActivatedEvent = null;
			this.sceneNotFoundEvent = null;
		}

		// Token: 0x0600A4ED RID: 42221 RVA: 0x00344B48 File Offset: 0x00342D48
		public override void OnEnter()
		{
			this.DoSetActivate();
			if (!this.success.IsNone)
			{
				this.success.Value = this._success;
			}
			if (!this.sceneFound.IsNone)
			{
				this.sceneFound.Value = this._sceneFound;
			}
			if (this._success)
			{
				base.Fsm.Event(this.successEvent);
			}
		}

		// Token: 0x0600A4EE RID: 42222 RVA: 0x00344BB0 File Offset: 0x00342DB0
		private void DoSetActivate()
		{
			try
			{
				switch (this.sceneReference)
				{
				case SetActiveScene.SceneReferenceOptions.SceneAtIndex:
					this._scene = SceneManager.GetSceneAt(this.sceneAtIndex.Value);
					break;
				case SetActiveScene.SceneReferenceOptions.SceneByName:
					this._scene = SceneManager.GetSceneByName(this.sceneByName.Value);
					break;
				case SetActiveScene.SceneReferenceOptions.SceneByPath:
					this._scene = SceneManager.GetSceneByPath(this.sceneByPath.Value);
					break;
				case SetActiveScene.SceneReferenceOptions.SceneByGameObject:
				{
					GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.sceneByGameObject);
					if (ownerDefaultTarget == null)
					{
						throw new Exception("Null GameObject");
					}
					this._scene = ownerDefaultTarget.scene;
					break;
				}
				}
			}
			catch (Exception ex)
			{
				base.LogError(ex.Message);
				this._sceneFound = false;
				base.Fsm.Event(this.sceneNotFoundEvent);
				return;
			}
			if (this._scene == default(Scene))
			{
				this._sceneFound = false;
				base.Fsm.Event(this.sceneNotFoundEvent);
				return;
			}
			this._success = SceneManager.SetActiveScene(this._scene);
			this._sceneFound = true;
		}

		// Token: 0x04008B64 RID: 35684
		[Tooltip("The reference options of the Scene")]
		public SetActiveScene.SceneReferenceOptions sceneReference;

		// Token: 0x04008B65 RID: 35685
		[Tooltip("The name of the scene to activate. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		// Token: 0x04008B66 RID: 35686
		[Tooltip("The build index of the scene to activate.")]
		public FsmInt sceneAtBuildIndex;

		// Token: 0x04008B67 RID: 35687
		[Tooltip("The index of the scene to activate.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B68 RID: 35688
		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		// Token: 0x04008B69 RID: 35689
		[Tooltip("The GameObject scene to activate")]
		public FsmOwnerDefault sceneByGameObject;

		// Token: 0x04008B6A RID: 35690
		[ActionSection("Result")]
		[Tooltip("True if set active succeeded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		// Token: 0x04008B6B RID: 35691
		[Tooltip("Event sent if setActive succeeded ")]
		public FsmEvent successEvent;

		// Token: 0x04008B6C RID: 35692
		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool sceneFound;

		// Token: 0x04008B6D RID: 35693
		[Tooltip("Event sent if scene not activated yet")]
		[UIHint(UIHint.Variable)]
		public FsmEvent sceneNotActivatedEvent;

		// Token: 0x04008B6E RID: 35694
		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent sceneNotFoundEvent;

		// Token: 0x04008B6F RID: 35695
		private Scene _scene;

		// Token: 0x04008B70 RID: 35696
		private bool _sceneFound;

		// Token: 0x04008B71 RID: 35697
		private bool _success;

		// Token: 0x020027A8 RID: 10152
		public enum SceneReferenceOptions
		{
			// Token: 0x0400F512 RID: 62738
			SceneAtBuildIndex,
			// Token: 0x0400F513 RID: 62739
			SceneAtIndex,
			// Token: 0x0400F514 RID: 62740
			SceneByName,
			// Token: 0x0400F515 RID: 62741
			SceneByPath,
			// Token: 0x0400F516 RID: 62742
			SceneByGameObject
		}
	}
}
