using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8C RID: 3468
	public abstract class GetSceneActionBase : FsmStateAction
	{
		// Token: 0x0600A4C4 RID: 42180 RVA: 0x00343FA8 File Offset: 0x003421A8
		public override void Reset()
		{
			base.Reset();
			this.sceneReference = GetSceneActionBase.SceneAllReferenceOptions.ActiveScene;
			this.sceneAtIndex = null;
			this.sceneByName = null;
			this.sceneByPath = null;
			this.sceneByGameObject = null;
			this.sceneFound = null;
			this.sceneFoundEvent = null;
			this.sceneNotFoundEvent = null;
		}

		// Token: 0x0600A4C5 RID: 42181 RVA: 0x00343FE8 File Offset: 0x003421E8
		public override void OnEnter()
		{
			try
			{
				switch (this.sceneReference)
				{
				case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
					this._scene = SceneManager.GetActiveScene();
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
					this._scene = SceneManager.GetSceneAt(this.sceneAtIndex.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
					this._scene = SceneManager.GetSceneByName(this.sceneByName.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
					this._scene = SceneManager.GetSceneByPath(this.sceneByPath.Value);
					break;
				case GetSceneActionBase.SceneAllReferenceOptions.SceneByGameObject:
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
			}
			if (this._scene == default(Scene))
			{
				this._sceneFound = false;
				if (!this.sceneFound.IsNone)
				{
					this.sceneFound.Value = false;
				}
				base.Fsm.Event(this.sceneNotFoundEvent);
				return;
			}
			this._sceneFound = true;
			if (!this.sceneFound.IsNone)
			{
				this.sceneFound.Value = true;
			}
		}

		// Token: 0x04008B20 RID: 35616
		[Tooltip("The reference option of the Scene")]
		public GetSceneActionBase.SceneAllReferenceOptions sceneReference;

		// Token: 0x04008B21 RID: 35617
		[Tooltip("The scene Index.")]
		public FsmInt sceneAtIndex;

		// Token: 0x04008B22 RID: 35618
		[Tooltip("The scene Name.")]
		public FsmString sceneByName;

		// Token: 0x04008B23 RID: 35619
		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		// Token: 0x04008B24 RID: 35620
		[Tooltip("The Scene of GameObject")]
		public FsmOwnerDefault sceneByGameObject;

		// Token: 0x04008B25 RID: 35621
		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool sceneFound;

		// Token: 0x04008B26 RID: 35622
		[Tooltip("Event sent if SceneReference resolves to a scene")]
		public FsmEvent sceneFoundEvent;

		// Token: 0x04008B27 RID: 35623
		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent sceneNotFoundEvent;

		// Token: 0x04008B28 RID: 35624
		[Tooltip("The Scene Cache")]
		protected Scene _scene;

		// Token: 0x04008B29 RID: 35625
		[Tooltip("True if a scene was found, use _scene to access it")]
		protected bool _sceneFound;

		// Token: 0x020027A4 RID: 10148
		public enum SceneReferenceOptions
		{
			// Token: 0x0400F502 RID: 62722
			SceneAtIndex,
			// Token: 0x0400F503 RID: 62723
			SceneByName,
			// Token: 0x0400F504 RID: 62724
			SceneByPath
		}

		// Token: 0x020027A5 RID: 10149
		public enum SceneSimpleReferenceOptions
		{
			// Token: 0x0400F506 RID: 62726
			SceneAtIndex,
			// Token: 0x0400F507 RID: 62727
			SceneByName
		}

		// Token: 0x020027A6 RID: 10150
		public enum SceneBuildReferenceOptions
		{
			// Token: 0x0400F509 RID: 62729
			SceneAtBuildIndex,
			// Token: 0x0400F50A RID: 62730
			SceneByName
		}

		// Token: 0x020027A7 RID: 10151
		public enum SceneAllReferenceOptions
		{
			// Token: 0x0400F50C RID: 62732
			ActiveScene,
			// Token: 0x0400F50D RID: 62733
			SceneAtIndex,
			// Token: 0x0400F50E RID: 62734
			SceneByName,
			// Token: 0x0400F50F RID: 62735
			SceneByPath,
			// Token: 0x0400F510 RID: 62736
			SceneByGameObject
		}
	}
}
