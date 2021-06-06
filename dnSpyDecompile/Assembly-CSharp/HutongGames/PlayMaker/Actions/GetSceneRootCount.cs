using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D89 RID: 3465
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene RootCount, the number of root transforms of this scene.")]
	public class GetSceneRootCount : GetSceneActionBase
	{
		// Token: 0x0600A4B5 RID: 42165 RVA: 0x00343D04 File Offset: 0x00341F04
		public override void Reset()
		{
			base.Reset();
			this.rootCount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A4B6 RID: 42166 RVA: 0x00343D1A File Offset: 0x00341F1A
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneRootCount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4B7 RID: 42167 RVA: 0x00343D36 File Offset: 0x00341F36
		public override void OnUpdate()
		{
			this.DoGetSceneRootCount();
		}

		// Token: 0x0600A4B8 RID: 42168 RVA: 0x00343D3E File Offset: 0x00341F3E
		private void DoGetSceneRootCount()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.rootCount.IsNone)
			{
				this.rootCount.Value = this._scene.rootCount;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008B12 RID: 35602
		[ActionSection("Result")]
		[Tooltip("The scene RootCount")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		// Token: 0x04008B13 RID: 35603
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
