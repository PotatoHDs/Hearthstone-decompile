using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCC RID: 3276
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Tests if a Game Object's Rigid Body is Kinematic.")]
	public class IsKinematic : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A0E5 RID: 41189 RVA: 0x003329A0 File Offset: 0x00330BA0
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0E6 RID: 41190 RVA: 0x003329C5 File Offset: 0x00330BC5
		public override void OnEnter()
		{
			this.DoIsKinematic();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0E7 RID: 41191 RVA: 0x003329DB File Offset: 0x00330BDB
		public override void OnUpdate()
		{
			this.DoIsKinematic();
		}

		// Token: 0x0600A0E8 RID: 41192 RVA: 0x003329E4 File Offset: 0x00330BE4
		private void DoIsKinematic()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				bool isKinematic = base.rigidbody.isKinematic;
				this.store.Value = isKinematic;
				base.Fsm.Event(isKinematic ? this.trueEvent : this.falseEvent);
			}
		}

		// Token: 0x04008675 RID: 34421
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008676 RID: 34422
		public FsmEvent trueEvent;

		// Token: 0x04008677 RID: 34423
		public FsmEvent falseEvent;

		// Token: 0x04008678 RID: 34424
		[UIHint(UIHint.Variable)]
		public FsmBool store;

		// Token: 0x04008679 RID: 34425
		public bool everyFrame;
	}
}
