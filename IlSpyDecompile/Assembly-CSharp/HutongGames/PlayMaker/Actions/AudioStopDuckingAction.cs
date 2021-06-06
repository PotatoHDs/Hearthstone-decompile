using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Stops the ducking started by AudioStartDuckingAction on this object.")]
	public class AudioStopDuckingAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Game Object whose ducking we want to stop.")]
		public FsmOwnerDefault m_GameObject;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				SoundDucker soundDucker = null;
				soundDucker = ownerDefaultTarget.GetComponent<SoundDucker>();
				if (soundDucker != null)
				{
					soundDucker.StopDucking();
					Object.Destroy(soundDucker);
				}
			}
			Finish();
		}
	}
}
