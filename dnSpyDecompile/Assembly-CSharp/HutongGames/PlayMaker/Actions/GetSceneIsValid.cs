using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D84 RID: 3460
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get a scene isValid flag. A scene may be invalid if, for example, you tried to open a scene that does not exist. In this case, the scene returned from EditorSceneManager.OpenScene would return False for IsValid. ")]
	public class GetSceneIsValid : GetSceneActionBase
	{
		// Token: 0x0600A4A1 RID: 42145 RVA: 0x00343809 File Offset: 0x00341A09
		public override void Reset()
		{
			base.Reset();
			this.isValid = null;
		}

		// Token: 0x0600A4A2 RID: 42146 RVA: 0x00343818 File Offset: 0x00341A18
		public override void OnEnter()
		{
			base.OnEnter();
			this.DoGetSceneIsValid();
			base.Finish();
		}

		// Token: 0x0600A4A3 RID: 42147 RVA: 0x0034382C File Offset: 0x00341A2C
		private void DoGetSceneIsValid()
		{
			if (!this._sceneFound)
			{
				return;
			}
			if (!this.isValid.IsNone)
			{
				this.isValid.Value = this._scene.IsValid();
			}
			if (this._scene.IsValid())
			{
				base.Fsm.Event(this.isValidEvent);
			}
			else
			{
				base.Fsm.Event(this.isNotValidEvent);
			}
			base.Fsm.Event(this.sceneFoundEvent);
		}

		// Token: 0x04008AFA RID: 35578
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is loaded.")]
		public FsmBool isValid;

		// Token: 0x04008AFB RID: 35579
		[Tooltip("Event sent if the scene is valid.")]
		public FsmEvent isValidEvent;

		// Token: 0x04008AFC RID: 35580
		[Tooltip("Event sent if the scene is not valid.")]
		public FsmEvent isNotValidEvent;
	}
}
