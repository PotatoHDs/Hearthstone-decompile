using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D82 RID: 3458
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene isDirty flag. true if the scene is modified. ")]
	public class GetSceneIsDirty : GetSceneActionBase
	{
		// Token: 0x0600A497 RID: 42135 RVA: 0x00343717 File Offset: 0x00341917
		public override void Reset()
		{
			base.Reset();
			this.isDirty = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A498 RID: 42136 RVA: 0x0034372D File Offset: 0x0034192D
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneIsDirty();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A499 RID: 42137 RVA: 0x00343749 File Offset: 0x00341949
		public override void OnUpdate()
		{
			this.DoGetSceneIsDirty();
		}

		// Token: 0x0600A49A RID: 42138 RVA: 0x00343751 File Offset: 0x00341951
		private void DoGetSceneIsDirty()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.isDirty.IsNone)
			{
				this.isDirty.Value = this._scene.isDirty;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008AF2 RID: 35570
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		// Token: 0x04008AF3 RID: 35571
		[Tooltip("Event sent if the scene is modified.")]
		public FsmEvent isDirtyEvent;

		// Token: 0x04008AF4 RID: 35572
		[Tooltip("Event sent if the scene is unmodified.")]
		public FsmEvent isNotDirtyEvent;

		// Token: 0x04008AF5 RID: 35573
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
