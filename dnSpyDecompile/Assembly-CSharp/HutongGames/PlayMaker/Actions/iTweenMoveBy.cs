using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD3 RID: 3283
	[ActionCategory("iTween")]
	[Tooltip("Adds the supplied vector to a GameObject's position.")]
	public class iTweenMoveBy : iTweenFsmAction
	{
		// Token: 0x0600A109 RID: 41225 RVA: 0x00333950 File Offset: 0x00331B50
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

		// Token: 0x0600A10A RID: 41226 RVA: 0x00333A0E File Offset: 0x00331C0E
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A10B RID: 41227 RVA: 0x00333A31 File Offset: 0x00331C31
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A10C RID: 41228 RVA: 0x00333A40 File Offset: 0x00331C40
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
			iTween.MoveBy(ownerDefaultTarget, hashtable);
		}

		// Token: 0x040086B0 RID: 34480
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086B1 RID: 34481
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x040086B2 RID: 34482
		[RequiredField]
		[Tooltip("The vector to add to the GameObject's position.")]
		public FsmVector3 vector;

		// Token: 0x040086B3 RID: 34483
		[Tooltip("For the time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086B4 RID: 34484
		[Tooltip("For the time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x040086B5 RID: 34485
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x040086B6 RID: 34486
		[Tooltip("For the shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x040086B7 RID: 34487
		[Tooltip("For the type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x040086B8 RID: 34488
		public Space space;

		// Token: 0x040086B9 RID: 34489
		[ActionSection("LookAt")]
		[Tooltip("For whether or not the GameObject will orient to its direction of travel. False by default.")]
		public FsmBool orientToPath;

		// Token: 0x040086BA RID: 34490
		[Tooltip("For a target the GameObject will look at.")]
		public FsmGameObject lookAtObject;

		// Token: 0x040086BB RID: 34491
		[Tooltip("For a target the GameObject will look at.")]
		public FsmVector3 lookAtVector;

		// Token: 0x040086BC RID: 34492
		[Tooltip("For the time in seconds the object will take to look at either the 'looktarget' or 'orienttopath'. 0 by default")]
		public FsmFloat lookTime;

		// Token: 0x040086BD RID: 34493
		[Tooltip("Restricts rotation to the supplied axis only. Just put there strinc like 'x' or 'xz'")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
