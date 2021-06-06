using UnityEngine;
using UnityEngine.Playables;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Plays the timeline on specified gameObject with a 'Playable Director' component.")]
	public class TimelinePlay : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(PlayableDirector))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Only Finish() when the timeline has stopped. DO NOT USE FOR LOOPING OR INFINITE TIMELINES.")]
		public bool finishOnTimelineStopped;

		private PlayableDirector m_timeline;

		public override void Reset()
		{
			gameObject = null;
			m_timeline.stopped -= OnTimelineStopped;
			m_timeline = null;
			finishOnTimelineStopped = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				m_timeline = ownerDefaultTarget.GetComponent<PlayableDirector>();
				if (m_timeline != null)
				{
					m_timeline.Play();
				}
			}
			if (finishOnTimelineStopped)
			{
				m_timeline.stopped += OnTimelineStopped;
			}
			else
			{
				Finish();
			}
		}

		private void OnTimelineStopped(PlayableDirector aDirector)
		{
			if (m_timeline == aDirector)
			{
				Finish();
			}
		}
	}
}
