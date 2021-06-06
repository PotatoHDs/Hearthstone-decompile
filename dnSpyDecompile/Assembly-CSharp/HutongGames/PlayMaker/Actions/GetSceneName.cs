using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D86 RID: 3462
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene name.")]
	public class GetSceneName : GetSceneActionBase
	{
		// Token: 0x0600A4A9 RID: 42153 RVA: 0x00343A72 File Offset: 0x00341C72
		public override void Reset()
		{
			base.Reset();
			this.name = null;
		}

		// Token: 0x0600A4AA RID: 42154 RVA: 0x00343A81 File Offset: 0x00341C81
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneName();
			base.Finish();
		}

		// Token: 0x0600A4AB RID: 42155 RVA: 0x00343A95 File Offset: 0x00341C95
		private void DoGetSceneName()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.name.IsNone)
			{
				this.name.Value = this._scene.name;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008B07 RID: 35591
		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;
	}
}
