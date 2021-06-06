using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D87 RID: 3463
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene path.")]
	public class GetScenePath : GetSceneActionBase
	{
		// Token: 0x0600A4AD RID: 42157 RVA: 0x00343AD4 File Offset: 0x00341CD4
		public override void Reset()
		{
			base.Reset();
			this.path = null;
		}

		// Token: 0x0600A4AE RID: 42158 RVA: 0x00343AE3 File Offset: 0x00341CE3
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetScenePath();
			base.Finish();
		}

		// Token: 0x0600A4AF RID: 42159 RVA: 0x00343AF7 File Offset: 0x00341CF7
		private void DoGetScenePath()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.path.IsNone)
			{
				this.path.Value = this._scene.path;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008B08 RID: 35592
		[ActionSection("Result")]
		[Tooltip("The scene path")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString path;
	}
}
