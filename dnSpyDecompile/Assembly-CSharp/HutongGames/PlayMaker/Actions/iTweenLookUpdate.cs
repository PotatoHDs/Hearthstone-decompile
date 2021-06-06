using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD1 RID: 3281
	[ActionCategory("iTween")]
	[Tooltip("Rotates a GameObject to look at a supplied Transform or Vector3 over time.")]
	public class iTweenLookUpdate : FsmStateAction
	{
		// Token: 0x0600A0FE RID: 41214 RVA: 0x003332B6 File Offset: 0x003314B6
		public override void Reset()
		{
			this.transformTarget = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorTarget = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.axis = iTweenFsmAction.AxisRestriction.none;
		}

		// Token: 0x0600A0FF RID: 41215 RVA: 0x003332F4 File Offset: 0x003314F4
		public override void OnEnter()
		{
			this.hash = new Hashtable();
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.Finish();
				return;
			}
			if (this.transformTarget.IsNone)
			{
				this.hash.Add("looktarget", this.vectorTarget.IsNone ? Vector3.zero : this.vectorTarget.Value);
			}
			else if (this.vectorTarget.IsNone)
			{
				this.hash.Add("looktarget", this.transformTarget.Value.transform);
			}
			else
			{
				this.hash.Add("looktarget", this.transformTarget.Value.transform.position + this.vectorTarget.Value);
			}
			this.hash.Add("time", this.time.IsNone ? 1f : this.time.Value);
			this.hash.Add("axis", (this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis));
			this.DoiTween();
		}

		// Token: 0x0600A100 RID: 41216 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x0600A101 RID: 41217 RVA: 0x00333458 File Offset: 0x00331658
		public override void OnUpdate()
		{
			this.hash.Remove("looktarget");
			if (this.transformTarget.IsNone)
			{
				this.hash.Add("looktarget", this.vectorTarget.IsNone ? Vector3.zero : this.vectorTarget.Value);
			}
			else if (this.vectorTarget.IsNone)
			{
				this.hash.Add("looktarget", this.transformTarget.Value.transform);
			}
			else
			{
				this.hash.Add("looktarget", this.transformTarget.Value.transform.position + this.vectorTarget.Value);
			}
			this.DoiTween();
		}

		// Token: 0x0600A102 RID: 41218 RVA: 0x00333527 File Offset: 0x00331727
		private void DoiTween()
		{
			iTween.LookUpdate(this.go, this.hash);
		}

		// Token: 0x0400869B RID: 34459
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400869C RID: 34460
		[Tooltip("Look at a transform position.")]
		public FsmGameObject transformTarget;

		// Token: 0x0400869D RID: 34461
		[Tooltip("A target position the GameObject will look at. If Transform Target is defined this is used as a look offset.")]
		public FsmVector3 vectorTarget;

		// Token: 0x0400869E RID: 34462
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400869F RID: 34463
		[Tooltip("Restricts rotation to the supplied axis only. Just put there strinc like 'x' or 'xz'")]
		public iTweenFsmAction.AxisRestriction axis;

		// Token: 0x040086A0 RID: 34464
		private Hashtable hash;

		// Token: 0x040086A1 RID: 34465
		private GameObject go;
	}
}
