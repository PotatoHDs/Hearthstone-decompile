using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C78 RID: 3192
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Each time this action is called it gets the next child of a GameObject. This lets you quickly loop through all the children of an object to perform actions on them. NOTE: To find a specific child use Find Child.")]
	public class GetNextChild : FsmStateAction
	{
		// Token: 0x06009FA2 RID: 40866 RVA: 0x0032EE4A File Offset: 0x0032D04A
		public override void Reset()
		{
			this.gameObject = null;
			this.storeNextChild = null;
			this.loopEvent = null;
			this.finishedEvent = null;
			this.resetFlag = null;
		}

		// Token: 0x06009FA3 RID: 40867 RVA: 0x0032EE6F File Offset: 0x0032D06F
		public override void OnEnter()
		{
			if (this.resetFlag.Value)
			{
				this.nextChildIndex = 0;
				this.resetFlag.Value = false;
			}
			this.DoGetNextChild(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009FA4 RID: 40868 RVA: 0x0032EEB0 File Offset: 0x0032D0B0
		private void DoGetNextChild(GameObject parent)
		{
			if (parent == null)
			{
				return;
			}
			if (this.go != parent)
			{
				this.go = parent;
				this.nextChildIndex = 0;
			}
			if (this.nextChildIndex >= this.go.transform.childCount)
			{
				this.nextChildIndex = 0;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.storeNextChild.Value = parent.transform.GetChild(this.nextChildIndex).gameObject;
			if (this.nextChildIndex >= this.go.transform.childCount)
			{
				this.nextChildIndex = 0;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.nextChildIndex++;
			if (this.loopEvent != null)
			{
				base.Fsm.Event(this.loopEvent);
			}
		}

		// Token: 0x04008531 RID: 34097
		[RequiredField]
		[Tooltip("The parent GameObject. Note, if GameObject changes, this action will reset and start again at the first child.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008532 RID: 34098
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next child in a GameObject variable.")]
		public FsmGameObject storeNextChild;

		// Token: 0x04008533 RID: 34099
		[Tooltip("Event to send to get the next child.")]
		public FsmEvent loopEvent;

		// Token: 0x04008534 RID: 34100
		[Tooltip("Event to send when there are no more children.")]
		public FsmEvent finishedEvent;

		// Token: 0x04008535 RID: 34101
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x04008536 RID: 34102
		private GameObject go;

		// Token: 0x04008537 RID: 34103
		private int nextChildIndex;
	}
}
