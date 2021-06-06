using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D88 RID: 3464
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene isDirty flag. true if the scene is modified. ")]
	public class GetSceneProperties : GetSceneActionBase
	{
		// Token: 0x0600A4B1 RID: 42161 RVA: 0x00343B38 File Offset: 0x00341D38
		public override void Reset()
		{
			base.Reset();
			this.name = null;
			this.path = null;
			this.buildIndex = null;
			this.isValid = null;
			this.isLoaded = null;
			this.rootCount = null;
			this.rootGameObjects = null;
			this.isDirty = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A4B2 RID: 42162 RVA: 0x00343B8A File Offset: 0x00341D8A
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneProperties();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4B3 RID: 42163 RVA: 0x00343BA8 File Offset: 0x00341DA8
		private void DoGetSceneProperties()
		{
			if (!this._sceneFound)
			{
				return;
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
				}
				else
				{
					this.rootGameObjects.Resize(0);
				}
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008B09 RID: 35593
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;

		// Token: 0x04008B0A RID: 35594
		[Tooltip("The scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString path;

		// Token: 0x04008B0B RID: 35595
		[Tooltip("The scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt buildIndex;

		// Token: 0x04008B0C RID: 35596
		[Tooltip("true if the scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		// Token: 0x04008B0D RID: 35597
		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		// Token: 0x04008B0E RID: 35598
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		// Token: 0x04008B0F RID: 35599
		[Tooltip("The scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		// Token: 0x04008B10 RID: 35600
		[Tooltip("The scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray rootGameObjects;

		// Token: 0x04008B11 RID: 35601
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
