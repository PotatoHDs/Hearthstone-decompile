using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD6 RID: 3286
	[ActionCategory("iTween")]
	[Tooltip("Similar to MoveTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a 'live' set of changing values. Does not utilize an EaseType.")]
	public class iTweenMoveUpdate : FsmStateAction
	{
		// Token: 0x0600A119 RID: 41241 RVA: 0x00334B5C File Offset: 0x00332D5C
		public override void Reset()
		{
			this.transformPosition = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.space = Space.World;
			this.orientToPath = new FsmBool
			{
				Value = true
			};
			this.lookAtObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.lookAtVector = new FsmVector3
			{
				UseVariable = true
			};
			this.lookTime = 0f;
			this.axis = iTweenFsmAction.AxisRestriction.none;
		}

		// Token: 0x0600A11A RID: 41242 RVA: 0x00334BF4 File Offset: 0x00332DF4
		public override void OnEnter()
		{
			this.hash = new Hashtable();
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.Finish();
				return;
			}
			if (this.transformPosition.IsNone)
			{
				this.hash.Add("position", this.vectorPosition.IsNone ? Vector3.zero : this.vectorPosition.Value);
			}
			else if (this.vectorPosition.IsNone)
			{
				this.hash.Add("position", this.transformPosition.Value.transform);
			}
			else if (this.space == Space.World || this.go.transform.parent == null)
			{
				this.hash.Add("position", this.transformPosition.Value.transform.position + this.vectorPosition.Value);
			}
			else
			{
				this.hash.Add("position", this.go.transform.parent.InverseTransformPoint(this.transformPosition.Value.transform.position) + this.vectorPosition.Value);
			}
			this.hash.Add("time", this.time.IsNone ? 1f : this.time.Value);
			this.hash.Add("islocal", this.space == Space.Self);
			this.hash.Add("axis", (this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis));
			if (!this.orientToPath.IsNone)
			{
				this.hash.Add("orienttopath", this.orientToPath.Value);
			}
			if (this.lookAtObject.IsNone)
			{
				if (!this.lookAtVector.IsNone)
				{
					this.hash.Add("looktarget", this.lookAtVector.Value);
				}
			}
			else
			{
				this.hash.Add("looktarget", this.lookAtObject.Value.transform);
			}
			if (!this.lookAtObject.IsNone || !this.lookAtVector.IsNone)
			{
				this.hash.Add("looktime", this.lookTime.IsNone ? 0f : this.lookTime.Value);
			}
			this.DoiTween();
		}

		// Token: 0x0600A11B RID: 41243 RVA: 0x00334EC4 File Offset: 0x003330C4
		public override void OnUpdate()
		{
			this.hash.Remove("position");
			if (this.transformPosition.IsNone)
			{
				this.hash.Add("position", this.vectorPosition.IsNone ? Vector3.zero : this.vectorPosition.Value);
			}
			else if (this.vectorPosition.IsNone)
			{
				this.hash.Add("position", this.transformPosition.Value.transform);
			}
			else if (this.space == Space.World)
			{
				this.hash.Add("position", this.transformPosition.Value.transform.position + this.vectorPosition.Value);
			}
			else
			{
				this.hash.Add("position", this.transformPosition.Value.transform.localPosition + this.vectorPosition.Value);
			}
			this.DoiTween();
		}

		// Token: 0x0600A11C RID: 41244 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x0600A11D RID: 41245 RVA: 0x00334FDA File Offset: 0x003331DA
		private void DoiTween()
		{
			iTween.MoveUpdate(this.go, this.hash);
		}

		// Token: 0x040086E2 RID: 34530
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086E3 RID: 34531
		[Tooltip("Move From a transform rotation.")]
		public FsmGameObject transformPosition;

		// Token: 0x040086E4 RID: 34532
		[Tooltip("The position the GameObject will animate from.  If transformPosition is set, this is used as an offset.")]
		public FsmVector3 vectorPosition;

		// Token: 0x040086E5 RID: 34533
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086E6 RID: 34534
		[Tooltip("Whether to animate in local or world space.")]
		public Space space;

		// Token: 0x040086E7 RID: 34535
		[ActionSection("LookAt")]
		[Tooltip("Whether or not the GameObject will orient to its direction of travel. False by default.")]
		public FsmBool orientToPath;

		// Token: 0x040086E8 RID: 34536
		[Tooltip("A target object the GameObject will look at.")]
		public FsmGameObject lookAtObject;

		// Token: 0x040086E9 RID: 34537
		[Tooltip("A target position the GameObject will look at.")]
		public FsmVector3 lookAtVector;

		// Token: 0x040086EA RID: 34538
		[Tooltip("The time in seconds the object will take to look at either the Look At Target or Orient To Path. 0 by default")]
		public FsmFloat lookTime;

		// Token: 0x040086EB RID: 34539
		[Tooltip("Restricts rotation to the supplied axis only.")]
		public iTweenFsmAction.AxisRestriction axis;

		// Token: 0x040086EC RID: 34540
		private Hashtable hash;

		// Token: 0x040086ED RID: 34541
		private GameObject go;
	}
}
