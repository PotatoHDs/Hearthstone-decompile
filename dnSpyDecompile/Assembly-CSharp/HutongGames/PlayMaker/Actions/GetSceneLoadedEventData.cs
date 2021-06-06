using System;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D85 RID: 3461
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the last Loaded Scene Event data when event was sent from the action 'SendSceneLoadedEvent")]
	public class GetSceneLoadedEventData : FsmStateAction
	{
		// Token: 0x0600A4A5 RID: 42149 RVA: 0x003438A8 File Offset: 0x00341AA8
		public override void Reset()
		{
			this.loadedMode = null;
			this.name = null;
			this.path = null;
			this.isValid = null;
			this.buildIndex = null;
			this.isLoaded = null;
			this.rootCount = null;
			this.rootGameObjects = null;
			this.isDirty = null;
		}

		// Token: 0x0600A4A6 RID: 42150 RVA: 0x003438F4 File Offset: 0x00341AF4
		public override void OnEnter()
		{
			this.DoGetSceneProperties();
			base.Finish();
		}

		// Token: 0x0600A4A7 RID: 42151 RVA: 0x00343904 File Offset: 0x00341B04
		private void DoGetSceneProperties()
		{
			this._scene = SendSceneLoadedEvent.lastLoadedScene;
			if (!this.name.IsNone)
			{
				this.loadedMode.Value = SendSceneLoadedEvent.lastLoadedMode;
			}
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

		// Token: 0x04008AFD RID: 35581
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene loaded mode")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadedMode;

		// Token: 0x04008AFE RID: 35582
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;

		// Token: 0x04008AFF RID: 35583
		[Tooltip("The scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString path;

		// Token: 0x04008B00 RID: 35584
		[Tooltip("true if the scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		// Token: 0x04008B01 RID: 35585
		[Tooltip("The scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt buildIndex;

		// Token: 0x04008B02 RID: 35586
		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		// Token: 0x04008B03 RID: 35587
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		// Token: 0x04008B04 RID: 35588
		[Tooltip("The scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		// Token: 0x04008B05 RID: 35589
		[Tooltip("The scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray rootGameObjects;

		// Token: 0x04008B06 RID: 35590
		private Scene _scene;
	}
}
