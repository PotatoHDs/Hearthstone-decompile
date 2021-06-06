using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
	private event OnAnimationEvent AnimationEventRecieved;

	public void RegisterAnimationEventListener(OnAnimationEvent listener)
	{
		AnimationEventRecieved += listener;
	}

	public void UnregisterAnimationEventListener(OnAnimationEvent listener)
	{
		AnimationEventRecieved -= listener;
	}

	public void FireAnimationEvent(Object obj)
	{
		if (this.AnimationEventRecieved != null)
		{
			this.AnimationEventRecieved(obj);
		}
	}
}
