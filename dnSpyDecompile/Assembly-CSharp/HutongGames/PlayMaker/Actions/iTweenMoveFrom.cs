using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD4 RID: 3284
	[ActionCategory("iTween")]
	[Tooltip("Instantly changes a GameObject's position to a supplied destination then returns it to it's starting position over time.")]
	public class iTweenMoveFrom : iTweenFsmAction
	{
		// Token: 0x0600A10E RID: 41230 RVA: 0x00333D64 File Offset: 0x00331F64
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.transformPosition = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.delay = 0f;
			this.loopType = iTween.LoopType.none;
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
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

		// Token: 0x0600A10F RID: 41231 RVA: 0x00333E3A File Offset: 0x0033203A
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A110 RID: 41232 RVA: 0x00333E5D File Offset: 0x0033205D
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A111 RID: 41233 RVA: 0x00333E6C File Offset: 0x0033206C
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vectorPosition.IsNone ? Vector3.zero : this.vectorPosition.Value;
			if (!this.transformPosition.IsNone && this.transformPosition.Value)
			{
				vector = ((this.space == Space.World || ownerDefaultTarget.transform.parent == null) ? (this.transformPosition.Value.transform.position + vector) : (ownerDefaultTarget.transform.parent.InverseTransformPoint(this.transformPosition.Value.transform.position) + vector));
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("position", vector);
			hashtable.Add(this.speed.IsNone ? "time" : "speed", this.speed.IsNone ? (this.time.IsNone ? 1f : this.time.Value) : this.speed.Value);
			hashtable.Add("delay", this.delay.IsNone ? 0f : this.delay.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "iTweenOnComplete");
			hashtable.Add("oncompleteparams", this.itweenID);
			hashtable.Add("onstart", "iTweenOnStart");
			hashtable.Add("onstartparams", this.itweenID);
			hashtable.Add("ignoretimescale", !this.realTime.IsNone && this.realTime.Value);
			hashtable.Add("islocal", this.space == Space.Self);
			hashtable.Add("name", this.id.IsNone ? "" : this.id.Value);
			hashtable.Add("axis", (this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis));
			if (!this.orientToPath.IsNone)
			{
				hashtable.Add("orienttopath", this.orientToPath.Value);
			}
			if (!this.lookAtObject.IsNone)
			{
				hashtable.Add("looktarget", this.lookAtVector.IsNone ? this.lookAtObject.Value.transform.position : (this.lookAtObject.Value.transform.position + this.lookAtVector.Value));
			}
			else if (!this.lookAtVector.IsNone)
			{
				hashtable.Add("looktarget", this.lookAtVector.Value);
			}
			if (!this.lookAtObject.IsNone || !this.lookAtVector.IsNone)
			{
				hashtable.Add("looktime", this.lookTime.IsNone ? 0f : this.lookTime.Value);
			}
			this.itweenType = "move";
			iTween.MoveFrom(ownerDefaultTarget, hashtable);
		}

		// Token: 0x040086BE RID: 34494
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086BF RID: 34495
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x040086C0 RID: 34496
		[Tooltip("Move From a transform rotation.")]
		public FsmGameObject transformPosition;

		// Token: 0x040086C1 RID: 34497
		[Tooltip("The position the GameObject will animate from. If Transform Position is defined this is used as a local offset.")]
		public FsmVector3 vectorPosition;

		// Token: 0x040086C2 RID: 34498
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086C3 RID: 34499
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x040086C4 RID: 34500
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x040086C5 RID: 34501
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x040086C6 RID: 34502
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x040086C7 RID: 34503
		[Tooltip("Whether to animate in local or world space.")]
		public Space space;

		// Token: 0x040086C8 RID: 34504
		[ActionSection("LookAt")]
		[Tooltip("Whether or not the GameObject will orient to its direction of travel. False by default.")]
		public FsmBool orientToPath;

		// Token: 0x040086C9 RID: 34505
		[Tooltip("A target object the GameObject will look at.")]
		public FsmGameObject lookAtObject;

		// Token: 0x040086CA RID: 34506
		[Tooltip("A target position the GameObject will look at.")]
		public FsmVector3 lookAtVector;

		// Token: 0x040086CB RID: 34507
		[Tooltip("The time in seconds the object will take to look at either the Look At Target or Orient To Path. 0 by default")]
		public FsmFloat lookTime;

		// Token: 0x040086CC RID: 34508
		[Tooltip("Restricts rotation to the supplied axis only.")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
