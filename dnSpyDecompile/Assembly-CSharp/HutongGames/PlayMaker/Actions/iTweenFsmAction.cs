using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCE RID: 3278
	[Tooltip("iTween base action - don't use!")]
	public abstract class iTweenFsmAction : FsmStateAction
	{
		// Token: 0x0600A0EF RID: 41199 RVA: 0x00332AE0 File Offset: 0x00330CE0
		public override void Reset()
		{
			this.startEvent = null;
			this.finishEvent = null;
			this.realTime = new FsmBool
			{
				Value = false
			};
			this.stopOnExit = new FsmBool
			{
				Value = true
			};
			this.loopDontFinish = new FsmBool
			{
				Value = true
			};
			this.itweenType = "";
		}

		// Token: 0x0600A0F0 RID: 41200 RVA: 0x00332B3C File Offset: 0x00330D3C
		protected void OnEnteriTween(FsmOwnerDefault anOwner)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(anOwner);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.itweenEvents = ownerDefaultTarget.AddComponent<iTweenFSMEvents>();
			this.itweenEvents.targetGameObjectName = ownerDefaultTarget.name;
			this.itweenEvents.itweenFSMAction = this;
			iTweenFSMEvents.itweenIDCount++;
			this.itweenID = iTweenFSMEvents.itweenIDCount;
			this.itweenEvents.itweenID = iTweenFSMEvents.itweenIDCount;
			this.itweenEvents.donotfinish = (!this.loopDontFinish.IsNone && this.loopDontFinish.Value);
		}

		// Token: 0x0600A0F1 RID: 41201 RVA: 0x00332BD6 File Offset: 0x00330DD6
		protected void IsLoop(bool aValue)
		{
			if (this.itweenEvents != null)
			{
				this.itweenEvents.islooping = aValue;
			}
		}

		// Token: 0x0600A0F2 RID: 41202 RVA: 0x00332BF4 File Offset: 0x00330DF4
		protected void OnExitiTween(FsmOwnerDefault anOwner)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(anOwner);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.itweenEvents)
			{
				UnityEngine.Object.Destroy(this.itweenEvents);
			}
			if (this.stopOnExit.IsNone)
			{
				iTween.Stop(ownerDefaultTarget, this.itweenType);
				return;
			}
			if (this.stopOnExit.Value)
			{
				iTween.Stop(ownerDefaultTarget, this.itweenType);
			}
		}

		// Token: 0x0400867F RID: 34431
		[ActionSection("Events")]
		public FsmEvent startEvent;

		// Token: 0x04008680 RID: 34432
		public FsmEvent finishEvent;

		// Token: 0x04008681 RID: 34433
		[Tooltip("Setting this to true will allow the animation to continue independent of the current time which is helpful for animating menus after a game has been paused by setting Time.timeScale=0.")]
		public FsmBool realTime;

		// Token: 0x04008682 RID: 34434
		public FsmBool stopOnExit;

		// Token: 0x04008683 RID: 34435
		public FsmBool loopDontFinish;

		// Token: 0x04008684 RID: 34436
		internal iTweenFSMEvents itweenEvents;

		// Token: 0x04008685 RID: 34437
		protected string itweenType = "";

		// Token: 0x04008686 RID: 34438
		protected int itweenID = -1;

		// Token: 0x0200279C RID: 10140
		public enum AxisRestriction
		{
			// Token: 0x0400F4DA RID: 62682
			none,
			// Token: 0x0400F4DB RID: 62683
			x,
			// Token: 0x0400F4DC RID: 62684
			y,
			// Token: 0x0400F4DD RID: 62685
			z,
			// Token: 0x0400F4DE RID: 62686
			xy,
			// Token: 0x0400F4DF RID: 62687
			xz,
			// Token: 0x0400F4E0 RID: 62688
			yz
		}
	}
}
