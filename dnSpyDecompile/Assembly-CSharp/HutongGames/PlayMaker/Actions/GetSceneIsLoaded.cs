using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D83 RID: 3459
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene isLoaded flag.")]
	public class GetSceneIsLoaded : GetSceneActionBase
	{
		// Token: 0x0600A49C RID: 42140 RVA: 0x00343790 File Offset: 0x00341990
		public override void Reset()
		{
			base.Reset();
			this.isLoaded = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A49D RID: 42141 RVA: 0x003437A6 File Offset: 0x003419A6
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneIsLoaded();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A49E RID: 42142 RVA: 0x003437C2 File Offset: 0x003419C2
		public override void OnUpdate()
		{
			this.DoGetSceneIsLoaded();
		}

		// Token: 0x0600A49F RID: 42143 RVA: 0x003437CA File Offset: 0x003419CA
		private void DoGetSceneIsLoaded()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.isLoaded.IsNone)
			{
				this.isLoaded.Value = this._scene.isLoaded;
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008AF6 RID: 35574
		[ActionSection("Result")]
		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		// Token: 0x04008AF7 RID: 35575
		[Tooltip("Event sent if the scene is loaded.")]
		public FsmEvent isLoadedEvent;

		// Token: 0x04008AF8 RID: 35576
		[Tooltip("Event sent if the scene is not loaded.")]
		public FsmEvent isNotLoadedEvent;

		// Token: 0x04008AF9 RID: 35577
		[Tooltip("Repeat every Frame")]
		public bool everyFrame;
	}
}
