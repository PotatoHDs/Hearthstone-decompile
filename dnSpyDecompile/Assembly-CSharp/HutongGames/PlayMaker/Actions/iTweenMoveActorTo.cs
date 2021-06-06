using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F50 RID: 3920
	[ActionCategory("Pegasus")]
	[Tooltip("Move an object's actor.  Used for spells that are dynamically loaded.")]
	public class iTweenMoveActorTo : iTweenFsmAction
	{
		// Token: 0x0600ACCA RID: 44234 RVA: 0x0035E6A0 File Offset: 0x0035C8A0
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.vectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.delay = 0f;
			this.easeType = iTween.EaseType.linear;
			this.loopType = iTween.LoopType.none;
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x0035E706 File Offset: 0x0035C906
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600ACCC RID: 44236 RVA: 0x0035E729 File Offset: 0x0035C929
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600ACCD RID: 44237 RVA: 0x0035E738 File Offset: 0x0035C938
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Actor actor = SceneUtils.FindComponentInParents<Actor>(ownerDefaultTarget);
			if (actor == null)
			{
				return;
			}
			GameObject gameObject = actor.gameObject;
			if (gameObject == null)
			{
				return;
			}
			this.itweenType = "move";
			Hashtable hashtable = new Hashtable();
			hashtable.Add("position", this.vectorPosition);
			hashtable.Add("name", this.id.IsNone ? "" : this.id.Value);
			hashtable.Add("delay", this.delay.IsNone ? 0f : this.delay.Value);
			hashtable.Add("easetype", this.easeType);
			hashtable.Add("looptype", this.loopType);
			hashtable.Add("ignoretimescale", !this.realTime.IsNone && this.realTime.Value);
			if (this.time.Value <= 0f)
			{
				hashtable.Add("time", 0f);
				iTween.FadeUpdate(gameObject, hashtable);
				base.Fsm.Event(this.startEvent);
				base.Fsm.Event(this.finishEvent);
				base.Finish();
				return;
			}
			hashtable["time"] = (this.time.IsNone ? 1f : this.time.Value);
			hashtable.Add("oncomplete", "iTweenOnComplete");
			hashtable.Add("oncompleteparams", this.itweenID);
			hashtable.Add("onstart", "iTweenOnStart");
			hashtable.Add("onstartparams", this.itweenID);
			hashtable.Add("oncompletetarget", ownerDefaultTarget);
			iTween.MoveTo(gameObject, hashtable);
		}

		// Token: 0x04009399 RID: 37785
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400939A RID: 37786
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400939B RID: 37787
		[Tooltip("Position the GameObject will animate to.")]
		public FsmVector3 vectorPosition;

		// Token: 0x0400939C RID: 37788
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400939D RID: 37789
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400939E RID: 37790
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x0400939F RID: 37791
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
