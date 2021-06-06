using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Changes a GameObject's alpha over time, if it supports alpha changes.")]
	public class iTweenFadeToAction : iTweenFsmAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		[Tooltip("An alpha value the GameObject will animate To.")]
		public FsmFloat m_Alpha;

		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		[Tooltip("Run this action on all child objects.")]
		public FsmBool m_IncludeChildren;

		public override void Reset()
		{
			base.Reset();
			id = new FsmString
			{
				UseVariable = true
			};
			m_Alpha = 0f;
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
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			itweenType = "color";
			if (m_IncludeChildren.Value)
			{
				foreach (Transform item in ownerDefaultTarget.transform)
				{
					DoiTweenOnChild(item.gameObject);
				}
			}
			DoiTweenOnParent(ownerDefaultTarget);
		}

		private void DoiTweenOnChild(GameObject go)
		{
			Hashtable hashtable = InitTweenArgTable();
			if (time.Value <= 0f)
			{
				hashtable.Add("time", 0f);
				iTween.FadeUpdate(go, hashtable);
			}
			else
			{
				hashtable["time"] = (time.IsNone ? 1f : time.Value);
				iTween.FadeTo(go, hashtable);
			}
		}

		private void DoiTweenOnParent(GameObject go)
		{
			Hashtable hashtable = InitTweenArgTable();
			if (time.Value <= 0f)
			{
				hashtable.Add("time", 0f);
				iTween.FadeUpdate(go, hashtable);
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
				iTween.FadeTo(go, hashtable);
			}
		}

		private Hashtable InitTweenArgTable()
		{
			return new Hashtable
			{
				{ "alpha", m_Alpha.Value },
				{
					"name",
					id.IsNone ? "" : id.Value
				},
				{
					"delay",
					delay.IsNone ? 0f : delay.Value
				},
				{ "easetype", easeType },
				{ "looptype", loopType },
				{
					"ignoretimescale",
					!realTime.IsNone && realTime.Value
				},
				{ "includechildren", false }
			};
		}
	}
}
