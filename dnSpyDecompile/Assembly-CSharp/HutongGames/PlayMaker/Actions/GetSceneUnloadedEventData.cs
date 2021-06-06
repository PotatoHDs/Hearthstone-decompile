using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8B RID: 3467
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the last Unloaded Scene Event data when event was sent from the action 'SendSceneUnloadedEvent")]
	public class GetSceneUnloadedEventData : FsmStateAction
	{
		// Token: 0x0600A4BF RID: 42175 RVA: 0x00343E04 File Offset: 0x00342004
		public override void Reset()
		{
			this.name = null;
			this.path = null;
			this.buildIndex = null;
			this.isLoaded = null;
			this.rootCount = null;
			this.rootGameObjects = null;
			this.isDirty = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A4C0 RID: 42176 RVA: 0x00343E3E File Offset: 0x0034203E
		public override void OnEnter()
		{
			this.DoGetSceneProperties();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4C1 RID: 42177 RVA: 0x00343E54 File Offset: 0x00342054
		public override void OnUpdate()
		{
			this.DoGetSceneProperties();
		}

		// Token: 0x0600A4C2 RID: 42178 RVA: 0x00343E5C File Offset: 0x0034205C
		private void DoGetSceneProperties()
		{
			this._scene = SendSceneUnloadedEvent.lastUnLoadedScene;
			if (!this.name.IsNone)
			{
				this.name.Value = this._scene.name;
			}
			if (!this.buildIndex.IsNone)
			{
				this.buildIndex.Value = this._scene.buildIndex;
			}
			if (!this.path.IsNone)
			{
				this.path.Value = this._scene.path;
			}
			if (!this.isValid.IsNone)
			{
				this.isValid.Value = this._scene.IsValid();
			}
			if (!this.isDirty.IsNone)
			{
				this.isDirty.Value = this._scene.isDirty;
			}
			if (!this.isLoaded.IsNone)
			{
				this.isLoaded.Value = this._scene.isLoaded;
			}
			if (!this.rootCount.IsNone)
			{
				this.rootCount.Value = this._scene.rootCount;
			}
			if (!this.rootGameObjects.IsNone)
			{
				if (this._scene.IsValid())
				{
					FsmArray fsmArray = this.rootGameObjects;
					object[] values = this._scene.GetRootGameObjects();
					fsmArray.Values = values;
					return;
				}
				this.rootGameObjects.Resize(0);
			}
		}

		// Token: 0x04008B16 RID: 35606
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;

		// Token: 0x04008B17 RID: 35607
		[Tooltip("The scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString path;

		// Token: 0x04008B18 RID: 35608
		[Tooltip("The scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt buildIndex;

		// Token: 0x04008B19 RID: 35609
		[Tooltip("true if the scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		// Token: 0x04008B1A RID: 35610
		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		// Token: 0x04008B1B RID: 35611
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		// Token: 0x04008B1C RID: 35612
		[Tooltip("The scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		// Token: 0x04008B1D RID: 35613
		[Tooltip("The scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray rootGameObjects;

		// Token: 0x04008B1E RID: 35614
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x04008B1F RID: 35615
		private Scene _scene;
	}
}
