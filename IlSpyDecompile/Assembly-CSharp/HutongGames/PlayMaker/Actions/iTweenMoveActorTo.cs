using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Move an object's actor.  Used for spells that are dynamically loaded.")]
	public class iTweenMoveActorTo : iTweenFsmAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		[Tooltip("Position the GameObject will animate to.")]
		public FsmVector3 vectorPosition;

		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		public override void Reset()
		{
			base.Reset();
			id = new FsmString
			{
				UseVariable = true
			};
			vectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			time = 1f;
			delay = 0f;
			easeType = iTween.EaseType.linear;
			loopType = iTween.LoopType.none;
		}

		public override void OnEnter()
		{
			OnEnteriTween(gameObject);
			if (loopType != 0)
			{
				IsLoop(aValue: true);
			}
			DoiTween();
		}

		public override void OnExit()
		{
			OnExitiTween(gameObject);
		}

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
			if (!(gameObject == null))
			{
				itweenType = "move";
				Hashtable hashtable = new Hashtable();
				hashtable.Add("position", vectorPosition);
				hashtable.Add("name", id.IsNone ? "" : id.Value);
				hashtable.Add("delay", delay.IsNone ? 0f : delay.Value);
				hashtable.Add("easetype", easeType);
				hashtable.Add("looptype", loopType);
				hashtable.Add("ignoretimescale", !realTime.IsNone && realTime.Value);
				if (time.Value <= 0f)
				{
					hashtable.Add("time", 0f);
					iTween.FadeUpdate(gameObject, hashtable);
					base.Fsm.Event(startEvent);
					base.Fsm.Event(finishEvent);
					Finish();
				}
				else
				{
					hashtable["time"] = (time.IsNone ? 1f : time.Value);
					hashtable.Add("oncomplete", "iTweenOnComplete");
					hashtable.Add("oncompleteparams", itweenID);
					hashtable.Add("onstart", "iTweenOnStart");
					hashtable.Add("onstartparams", itweenID);
					hashtable.Add("oncompletetarget", ownerDefaultTarget);
					iTween.MoveTo(gameObject, hashtable);
				}
			}
		}
	}
}
