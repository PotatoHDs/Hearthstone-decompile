using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7F RID: 3455
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Returns the index of a scene in the Build Settings. Always returns -1 if the scene was loaded through an AssetBundle.")]
	public class GetSceneBuildIndex : GetSceneActionBase
	{
		// Token: 0x0600A48A RID: 42122 RVA: 0x00343644 File Offset: 0x00341844
		public override void Reset()
		{
			base.Reset();
			this.buildIndex = null;
		}

		// Token: 0x0600A48B RID: 42123 RVA: 0x00343653 File Offset: 0x00341853
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneBuildIndex();
			base.Finish();
		}

		// Token: 0x0600A48C RID: 42124 RVA: 0x00343667 File Offset: 0x00341867
		private void DoGetSceneBuildIndex()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.buildIndex.IsNone)
			{
				this.buildIndex.Value = this._scene.buildIndex;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008AEE RID: 35566
		[ActionSection("Result")]
		[Tooltip("The scene Build Index")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt buildIndex;
	}
}
