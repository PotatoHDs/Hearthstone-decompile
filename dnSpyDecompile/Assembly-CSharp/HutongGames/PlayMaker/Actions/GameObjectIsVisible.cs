using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C42 RID: 3138
	[ActionCategory(ActionCategory.Logic)]
	[ActionTarget(typeof(GameObject), "gameObject", false)]
	[Tooltip("Tests if a Game Object is visible.")]
	public class GameObjectIsVisible : ComponentAction<Renderer>
	{
		// Token: 0x06009EAD RID: 40621 RVA: 0x0032C10F File Offset: 0x0032A30F
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EAE RID: 40622 RVA: 0x0032C134 File Offset: 0x0032A334
		public override void OnEnter()
		{
			this.DoIsVisible();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EAF RID: 40623 RVA: 0x0032C14A File Offset: 0x0032A34A
		public override void OnUpdate()
		{
			this.DoIsVisible();
		}

		// Token: 0x06009EB0 RID: 40624 RVA: 0x0032C154 File Offset: 0x0032A354
		private void DoIsVisible()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				bool isVisible = base.renderer.isVisible;
				this.storeResult.Value = isVisible;
				base.Fsm.Event(isVisible ? this.trueEvent : this.falseEvent);
			}
		}

		// Token: 0x0400840A RID: 33802
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400840B RID: 33803
		[Tooltip("Event to send if the GameObject is visible.")]
		public FsmEvent trueEvent;

		// Token: 0x0400840C RID: 33804
		[Tooltip("Event to send if the GameObject is NOT visible.")]
		public FsmEvent falseEvent;

		// Token: 0x0400840D RID: 33805
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x0400840E RID: 33806
		public bool everyFrame;
	}
}
