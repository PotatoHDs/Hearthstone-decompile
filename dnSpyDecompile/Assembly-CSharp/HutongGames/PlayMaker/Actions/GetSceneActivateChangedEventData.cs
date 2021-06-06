using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7E RID: 3454
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the last activateChanged Scene Event data when event was sent from the action 'SendSceneActiveChangedEvent")]
	public class GetSceneActivateChangedEventData : FsmStateAction
	{
		// Token: 0x0600A485 RID: 42117 RVA: 0x00343324 File Offset: 0x00341524
		public override void Reset()
		{
			this.newName = null;
			this.newPath = null;
			this.newIsValid = null;
			this.newBuildIndex = null;
			this.newIsLoaded = null;
			this.newRootCount = null;
			this.newRootGameObjects = null;
			this.newIsDirty = null;
			this.previousName = null;
			this.previousPath = null;
			this.previousIsValid = null;
			this.previousBuildIndex = null;
			this.previousIsLoaded = null;
			this.previousRootCount = null;
			this.previousRootGameObjects = null;
			this.previousIsDirty = null;
		}

		// Token: 0x0600A486 RID: 42118 RVA: 0x003433A1 File Offset: 0x003415A1
		public override void OnEnter()
		{
			this.DoGetSceneProperties();
			base.Finish();
		}

		// Token: 0x0600A487 RID: 42119 RVA: 0x003433AF File Offset: 0x003415AF
		public override void OnUpdate()
		{
			this.DoGetSceneProperties();
		}

		// Token: 0x0600A488 RID: 42120 RVA: 0x003433B8 File Offset: 0x003415B8
		private void DoGetSceneProperties()
		{
			this._scene = SendActiveSceneChangedEvent.lastPreviousActiveScene;
			if (!this.previousName.IsNone)
			{
				this.previousName.Value = this._scene.name;
			}
			if (!this.previousBuildIndex.IsNone)
			{
				this.previousBuildIndex.Value = this._scene.buildIndex;
			}
			if (!this.previousPath.IsNone)
			{
				this.previousPath.Value = this._scene.path;
			}
			if (!this.previousIsValid.IsNone)
			{
				this.previousIsValid.Value = this._scene.IsValid();
			}
			if (!this.previousIsDirty.IsNone)
			{
				this.previousIsDirty.Value = this._scene.isDirty;
			}
			if (!this.previousIsLoaded.IsNone)
			{
				this.previousIsLoaded.Value = this._scene.isLoaded;
			}
			if (!this.previousRootCount.IsNone)
			{
				this.previousRootCount.Value = this._scene.rootCount;
			}
			if (!this.previousRootGameObjects.IsNone)
			{
				if (this._scene.IsValid())
				{
					FsmArray fsmArray = this.previousRootGameObjects;
					object[] rootGameObjects = this._scene.GetRootGameObjects();
					fsmArray.Values = rootGameObjects;
				}
				else
				{
					this.previousRootGameObjects.Resize(0);
				}
			}
			this._scene = SendActiveSceneChangedEvent.lastNewActiveScene;
			if (!this.newName.IsNone)
			{
				this.newName.Value = this._scene.name;
			}
			if (!this.newBuildIndex.IsNone)
			{
				this.newBuildIndex.Value = this._scene.buildIndex;
			}
			if (!this.newPath.IsNone)
			{
				this.newPath.Value = this._scene.path;
			}
			if (!this.newIsValid.IsNone)
			{
				this.newIsValid.Value = this._scene.IsValid();
			}
			if (!this.newIsDirty.IsNone)
			{
				this.newIsDirty.Value = this._scene.isDirty;
			}
			if (!this.newIsLoaded.IsNone)
			{
				this.newIsLoaded.Value = this._scene.isLoaded;
			}
			if (!this.newRootCount.IsNone)
			{
				this.newRootCount.Value = this._scene.rootCount;
			}
			if (!this.newRootGameObjects.IsNone)
			{
				if (this._scene.IsValid())
				{
					FsmArray fsmArray2 = this.newRootGameObjects;
					object[] rootGameObjects = this._scene.GetRootGameObjects();
					fsmArray2.Values = rootGameObjects;
					return;
				}
				this.newRootGameObjects.Resize(0);
			}
		}

		// Token: 0x04008ADD RID: 35549
		[ActionSection("New Active Scene")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The new active scene name")]
		public FsmString newName;

		// Token: 0x04008ADE RID: 35550
		[Tooltip("The new active scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString newPath;

		// Token: 0x04008ADF RID: 35551
		[Tooltip("true if the new active scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool newIsValid;

		// Token: 0x04008AE0 RID: 35552
		[Tooltip("The new active scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt newBuildIndex;

		// Token: 0x04008AE1 RID: 35553
		[Tooltip("true if the new active scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool newIsLoaded;

		// Token: 0x04008AE2 RID: 35554
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the new active scene is modified.")]
		public FsmBool newIsDirty;

		// Token: 0x04008AE3 RID: 35555
		[Tooltip("The new active scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt newRootCount;

		// Token: 0x04008AE4 RID: 35556
		[Tooltip("The new active scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray newRootGameObjects;

		// Token: 0x04008AE5 RID: 35557
		[ActionSection("Previous Active Scene")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The previous active scene name")]
		public FsmString previousName;

		// Token: 0x04008AE6 RID: 35558
		[Tooltip("The previous active scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString previousPath;

		// Token: 0x04008AE7 RID: 35559
		[Tooltip("true if the previous active scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool previousIsValid;

		// Token: 0x04008AE8 RID: 35560
		[Tooltip("The previous active scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt previousBuildIndex;

		// Token: 0x04008AE9 RID: 35561
		[Tooltip("true if the previous active scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool previousIsLoaded;

		// Token: 0x04008AEA RID: 35562
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the previous active scene is modified.")]
		public FsmBool previousIsDirty;

		// Token: 0x04008AEB RID: 35563
		[Tooltip("The previous active scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt previousRootCount;

		// Token: 0x04008AEC RID: 35564
		[Tooltip("The previous active scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray previousRootGameObjects;

		// Token: 0x04008AED RID: 35565
		private Scene _scene;
	}
}
