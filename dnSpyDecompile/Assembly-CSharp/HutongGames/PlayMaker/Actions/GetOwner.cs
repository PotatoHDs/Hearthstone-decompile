using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C79 RID: 3193
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Game Object that owns the FSM and stores it in a game object variable.")]
	public class GetOwner : FsmStateAction
	{
		// Token: 0x06009FA6 RID: 40870 RVA: 0x0032EF8D File Offset: 0x0032D18D
		public override void Reset()
		{
			this.storeGameObject = null;
		}

		// Token: 0x06009FA7 RID: 40871 RVA: 0x0032EF96 File Offset: 0x0032D196
		public override void OnEnter()
		{
			this.storeGameObject.Value = base.Owner;
			base.Finish();
		}

		// Token: 0x04008538 RID: 34104
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;
	}
}
