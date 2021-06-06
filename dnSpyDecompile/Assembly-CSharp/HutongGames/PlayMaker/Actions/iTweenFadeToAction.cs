using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4F RID: 3919
	[ActionCategory("Pegasus")]
	[Tooltip("Changes a GameObject's alpha over time, if it supports alpha changes.")]
	public class iTweenFadeToAction : iTweenFsmAction
	{
		// Token: 0x0600ACC2 RID: 44226 RVA: 0x0035E300 File Offset: 0x0035C500
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.m_Alpha = 0f;
			this.time = 1f;
			this.delay = 0f;
			this.easeType = iTween.EaseType.linear;
			this.loopType = iTween.LoopType.none;
		}

		// Token: 0x0600ACC3 RID: 44227 RVA: 0x0035E364 File Offset: 0x0035C564
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600ACC4 RID: 44228 RVA: 0x0035E387 File Offset: 0x0035C587
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600ACC5 RID: 44229 RVA: 0x0035E398 File Offset: 0x0035C598
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.itweenType = "color";
			if (this.m_IncludeChildren.Value)
			{
				foreach (object obj in ownerDefaultTarget.transform)
				{
					Transform transform = (Transform)obj;
					this.DoiTweenOnChild(transform.gameObject);
				}
			}
			this.DoiTweenOnParent(ownerDefaultTarget);
		}

		// Token: 0x0600ACC6 RID: 44230 RVA: 0x0035E434 File Offset: 0x0035C634
		private void DoiTweenOnChild(GameObject go)
		{
			Hashtable hashtable = this.InitTweenArgTable();
			if (this.time.Value <= 0f)
			{
				hashtable.Add("time", 0f);
				iTween.FadeUpdate(go, hashtable);
				return;
			}
			hashtable["time"] = (this.time.IsNone ? 1f : this.time.Value);
			iTween.FadeTo(go, hashtable);
		}

		// Token: 0x0600ACC7 RID: 44231 RVA: 0x0035E4B0 File Offset: 0x0035C6B0
		private void DoiTweenOnParent(GameObject go)
		{
			Hashtable hashtable = this.InitTweenArgTable();
			if (this.time.Value <= 0f)
			{
				hashtable.Add("time", 0f);
				iTween.FadeUpdate(go, hashtable);
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
			iTween.FadeTo(go, hashtable);
		}

		// Token: 0x0600ACC8 RID: 44232 RVA: 0x0035E5A0 File Offset: 0x0035C7A0
		private Hashtable InitTweenArgTable()
		{
			return new Hashtable
			{
				{
					"alpha",
					this.m_Alpha.Value
				},
				{
					"name",
					this.id.IsNone ? "" : this.id.Value
				},
				{
					"delay",
					this.delay.IsNone ? 0f : this.delay.Value
				},
				{
					"easetype",
					this.easeType
				},
				{
					"looptype",
					this.loopType
				},
				{
					"ignoretimescale",
					!this.realTime.IsNone && this.realTime.Value
				},
				{
					"includechildren",
					false
				}
			};
		}

		// Token: 0x04009391 RID: 37777
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009392 RID: 37778
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04009393 RID: 37779
		[Tooltip("An alpha value the GameObject will animate To.")]
		public FsmFloat m_Alpha;

		// Token: 0x04009394 RID: 37780
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04009395 RID: 37781
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04009396 RID: 37782
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04009397 RID: 37783
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04009398 RID: 37784
		[Tooltip("Run this action on all child objects.")]
		public FsmBool m_IncludeChildren;
	}
}
