using System;
using UnityEngine;
using UnityEngine.Playables;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F97 RID: 3991
	[ActionCategory("Pegasus")]
	[Tooltip("Plays the timeline on specified gameObject with a 'Playable Director' component.")]
	public class TimelinePlay : FsmStateAction
	{
		// Token: 0x0600ADEE RID: 44526 RVA: 0x00362AC8 File Offset: 0x00360CC8
		public override void Reset()
		{
			this.gameObject = null;
			this.m_timeline.stopped -= this.OnTimelineStopped;
			this.m_timeline = null;
			this.finishOnTimelineStopped = false;
		}

		// Token: 0x0600ADEF RID: 44527 RVA: 0x00362AF8 File Offset: 0x00360CF8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this.m_timeline = ownerDefaultTarget.GetComponent<PlayableDirector>();
				if (this.m_timeline != null)
				{
					this.m_timeline.Play();
				}
			}
			if (this.finishOnTimelineStopped)
			{
				this.m_timeline.stopped += this.OnTimelineStopped;
				return;
			}
			base.Finish();
		}

		// Token: 0x0600ADF0 RID: 44528 RVA: 0x00362B6B File Offset: 0x00360D6B
		private void OnTimelineStopped(PlayableDirector aDirector)
		{
			if (this.m_timeline == aDirector)
			{
				base.Finish();
			}
		}

		// Token: 0x040094BD RID: 38077
		[RequiredField]
		[CheckForComponent(typeof(PlayableDirector))]
		public FsmOwnerDefault gameObject;

		// Token: 0x040094BE RID: 38078
		[Tooltip("Only Finish() when the timeline has stopped. DO NOT USE FOR LOOPING OR INFINITE TIMELINES.")]
		public bool finishOnTimelineStopped;

		// Token: 0x040094BF RID: 38079
		private PlayableDirector m_timeline;
	}
}
