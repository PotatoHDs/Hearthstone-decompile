using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD2 RID: 3282
	[ActionCategory("iTween")]
	[Tooltip("Translates a GameObject's position over time.")]
	public class iTweenMoveAdd : iTweenFsmAction
	{
		// Token: 0x0600A104 RID: 41220 RVA: 0x0033353C File Offset: 0x0033173C
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.time = 1f;
			this.delay = 0f;
			this.loopType = iTween.LoopType.none;
			this.vector = new FsmVector3
			{
				UseVariable = true
			};
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
			this.space = Space.World;
			this.orientToPath = false;
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

		// Token: 0x0600A105 RID: 41221 RVA: 0x003335FA File Offset: 0x003317FA
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A106 RID: 41222 RVA: 0x0033361D File Offset: 0x0033181D
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A107 RID: 41223 RVA: 0x0033362C File Offset: 0x0033182C
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("amount", this.vector.IsNone ? Vector3.zero : this.vector.Value);
			hashtable.Add(this.speed.IsNone ? "time" : "speed", this.speed.IsNone ? (this.time.IsNone ? 1f : this.time.Value) : this.speed.Value);
			hashtable.Add("delay", this.delay.IsNone ? 0f : this.delay.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("oncomplete", "iTweenOnComplete");
			hashtable.Add("oncompleteparams", this.itweenID);
			hashtable.Add("onstart", "iTweenOnStart");
			hashtable.Add("onstartparams", this.itweenID);
			hashtable.Add("ignoretimescale", !this.realTime.IsNone && this.realTime.Value);
			hashtable.Add("space", this.space);
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
			iTween.MoveAdd(ownerDefaultTarget, hashtable);
		}

		// Token: 0x040086A2 RID: 34466
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086A3 RID: 34467
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x040086A4 RID: 34468
		[RequiredField]
		[Tooltip("A vector that will be added to a GameObjects position.")]
		public FsmVector3 vector;

		// Token: 0x040086A5 RID: 34469
		[Tooltip("For the time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086A6 RID: 34470
		[Tooltip("For the time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x040086A7 RID: 34471
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x040086A8 RID: 34472
		[Tooltip("For the shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x040086A9 RID: 34473
		[Tooltip("For the type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x040086AA RID: 34474
		public Space space;

		// Token: 0x040086AB RID: 34475
		[ActionSection("LookAt")]
		[Tooltip("For whether or not the GameObject will orient to its direction of travel. False by default.")]
		public FsmBool orientToPath;

		// Token: 0x040086AC RID: 34476
		[Tooltip("A target object the GameObject will look at.")]
		public FsmGameObject lookAtObject;

		// Token: 0x040086AD RID: 34477
		[Tooltip("A target position the GameObject will look at.")]
		public FsmVector3 lookAtVector;

		// Token: 0x040086AE RID: 34478
		[Tooltip("The time in seconds the object will take to look at either the Look At Target or Orient To Path. 0 by default")]
		public FsmFloat lookTime;

		// Token: 0x040086AF RID: 34479
		[Tooltip("Restricts rotation to the supplied axis only. Just put there strinc like 'x' or 'xz'")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
