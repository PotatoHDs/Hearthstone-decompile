using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D8A RID: 3466
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene Root GameObjects.")]
	public class GetSceneRootGameObjects : GetSceneActionBase
	{
		// Token: 0x0600A4BA RID: 42170 RVA: 0x00343D7D File Offset: 0x00341F7D
		public override void Reset()
		{
			base.Reset();
			this.rootGameObjects = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A4BB RID: 42171 RVA: 0x00343D93 File Offset: 0x00341F93
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneRootGameObjects();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4BC RID: 42172 RVA: 0x00343DAF File Offset: 0x00341FAF
		public override void OnUpdate()
		{
			this.DoGetSceneRootGameObjects();
		}

		// Token: 0x0600A4BD RID: 42173 RVA: 0x00343DB8 File Offset: 0x00341FB8
		private void DoGetSceneRootGameObjects()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.rootGameObjects.IsNone)
			{
				FsmArray fsmArray = this.rootGameObjects;
				object[] values = this._scene.GetRootGameObjects();
				fsmArray.Values = values;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008B14 RID: 35604
		[ActionSection("Result")]
		[Tooltip("The scene Root GameObjects")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray rootGameObjects;

		// Token: 0x04008B15 RID: 35605
		[Tooltip("Repeat every Frame")]
		public bool everyFrame;
	}
}
