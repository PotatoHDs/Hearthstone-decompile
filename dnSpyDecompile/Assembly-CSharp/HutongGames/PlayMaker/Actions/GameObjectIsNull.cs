using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C41 RID: 3137
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject Variable has a null value. E.g., If the FindGameObject action failed to find an object.")]
	public class GameObjectIsNull : FsmStateAction
	{
		// Token: 0x06009EA8 RID: 40616 RVA: 0x0032C07B File Offset: 0x0032A27B
		public override void Reset()
		{
			this.gameObject = null;
			this.isNull = null;
			this.isNotNull = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EA9 RID: 40617 RVA: 0x0032C0A0 File Offset: 0x0032A2A0
		public override void OnEnter()
		{
			this.DoIsGameObjectNull();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EAA RID: 40618 RVA: 0x0032C0B6 File Offset: 0x0032A2B6
		public override void OnUpdate()
		{
			this.DoIsGameObjectNull();
		}

		// Token: 0x06009EAB RID: 40619 RVA: 0x0032C0C0 File Offset: 0x0032A2C0
		private void DoIsGameObjectNull()
		{
			bool flag = this.gameObject.Value == null;
			if (this.storeResult != null)
			{
				this.storeResult.Value = flag;
			}
			base.Fsm.Event(flag ? this.isNull : this.isNotNull);
		}

		// Token: 0x04008405 RID: 33797
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject variable to test.")]
		public FsmGameObject gameObject;

		// Token: 0x04008406 RID: 33798
		[Tooltip("Event to send if the GamObject is null.")]
		public FsmEvent isNull;

		// Token: 0x04008407 RID: 33799
		[Tooltip("Event to send if the GamObject is NOT null.")]
		public FsmEvent isNotNull;

		// Token: 0x04008408 RID: 33800
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008409 RID: 33801
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
