using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3E RID: 3134
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a Game Object has a tag.")]
	public class GameObjectCompareTag : FsmStateAction
	{
		// Token: 0x06009E9A RID: 40602 RVA: 0x0032BE74 File Offset: 0x0032A074
		public override void Reset()
		{
			this.gameObject = null;
			this.tag = "Untagged";
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E9B RID: 40603 RVA: 0x0032BEA9 File Offset: 0x0032A0A9
		public override void OnEnter()
		{
			this.DoCompareTag();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E9C RID: 40604 RVA: 0x0032BEBF File Offset: 0x0032A0BF
		public override void OnUpdate()
		{
			this.DoCompareTag();
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x0032BEC8 File Offset: 0x0032A0C8
		private void DoCompareTag()
		{
			bool flag = false;
			if (this.gameObject.Value != null)
			{
				flag = this.gameObject.Value.CompareTag(this.tag.Value);
			}
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x040083F5 RID: 33781
		[RequiredField]
		[Tooltip("The GameObject to test.")]
		public FsmGameObject gameObject;

		// Token: 0x040083F6 RID: 33782
		[RequiredField]
		[UIHint(UIHint.Tag)]
		[Tooltip("The Tag to check for.")]
		public FsmString tag;

		// Token: 0x040083F7 RID: 33783
		[Tooltip("Event to send if the GameObject has the Tag.")]
		public FsmEvent trueEvent;

		// Token: 0x040083F8 RID: 33784
		[Tooltip("Event to send if the GameObject does not have the Tag.")]
		public FsmEvent falseEvent;

		// Token: 0x040083F9 RID: 33785
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		// Token: 0x040083FA RID: 33786
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
