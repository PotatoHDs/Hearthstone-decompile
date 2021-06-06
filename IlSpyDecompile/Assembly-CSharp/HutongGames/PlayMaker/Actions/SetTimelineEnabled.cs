using UnityEngine;
using UnityEngine.Playables;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Enables the 'Playable Director' component on the given game object.")]
	public class SetTimelineEnabled : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(PlayableDirector))]
		public FsmOwnerDefault gameObject;

		public bool enableTimeline;

		public override void Reset()
		{
			gameObject = null;
			enableTimeline = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				PlayableDirector component = ownerDefaultTarget.GetComponent<PlayableDirector>();
				if (component != null)
				{
					component.enabled = enableTimeline;
				}
			}
			Finish();
		}
	}
}
