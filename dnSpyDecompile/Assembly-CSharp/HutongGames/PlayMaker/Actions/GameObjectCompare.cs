using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3D RID: 3133
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compares 2 Game Objects and sends Events based on the result.")]
	public class GameObjectCompare : FsmStateAction
	{
		// Token: 0x06009E95 RID: 40597 RVA: 0x0032BDB6 File Offset: 0x00329FB6
		public override void Reset()
		{
			this.gameObjectVariable = null;
			this.compareTo = null;
			this.equalEvent = null;
			this.notEqualEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x0032BDE2 File Offset: 0x00329FE2
		public override void OnEnter()
		{
			this.DoGameObjectCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x0032BDF8 File Offset: 0x00329FF8
		public override void OnUpdate()
		{
			this.DoGameObjectCompare();
		}

		// Token: 0x06009E98 RID: 40600 RVA: 0x0032BE00 File Offset: 0x0032A000
		private void DoGameObjectCompare()
		{
			bool flag = base.Fsm.GetOwnerDefaultTarget(this.gameObjectVariable) == this.compareTo.Value;
			this.storeResult.Value = flag;
			if (flag && this.equalEvent != null)
			{
				base.Fsm.Event(this.equalEvent);
				return;
			}
			if (!flag && this.notEqualEvent != null)
			{
				base.Fsm.Event(this.notEqualEvent);
			}
		}

		// Token: 0x040083EF RID: 33775
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Title("Game Object")]
		[Tooltip("A Game Object variable to compare.")]
		public FsmOwnerDefault gameObjectVariable;

		// Token: 0x040083F0 RID: 33776
		[RequiredField]
		[Tooltip("Compare the variable with this Game Object")]
		public FsmGameObject compareTo;

		// Token: 0x040083F1 RID: 33777
		[Tooltip("Send this event if Game Objects are equal")]
		public FsmEvent equalEvent;

		// Token: 0x040083F2 RID: 33778
		[Tooltip("Send this event if Game Objects are not equal")]
		public FsmEvent notEqualEvent;

		// Token: 0x040083F3 RID: 33779
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the check in a Bool Variable. (True if equal, false if not equal).")]
		public FsmBool storeResult;

		// Token: 0x040083F4 RID: 33780
		[Tooltip("Repeat every frame. Useful if you're waiting for a true or false result.")]
		public bool everyFrame;
	}
}
