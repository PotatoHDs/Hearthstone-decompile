using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3F RID: 3135
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject has children.")]
	public class GameObjectHasChildren : FsmStateAction
	{
		// Token: 0x06009E9F RID: 40607 RVA: 0x0032BF2E File Offset: 0x0032A12E
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x0032BF53 File Offset: 0x0032A153
		public override void OnEnter()
		{
			this.DoHasChildren();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x0032BF69 File Offset: 0x0032A169
		public override void OnUpdate()
		{
			this.DoHasChildren();
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x0032BF74 File Offset: 0x0032A174
		private void DoHasChildren()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			bool flag = ownerDefaultTarget.transform.childCount > 0;
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x040083FB RID: 33787
		[RequiredField]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040083FC RID: 33788
		[Tooltip("Event to send if the GameObject has children.")]
		public FsmEvent trueEvent;

		// Token: 0x040083FD RID: 33789
		[Tooltip("Event to send if the GameObject does not have children.")]
		public FsmEvent falseEvent;

		// Token: 0x040083FE RID: 33790
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x040083FF RID: 33791
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
