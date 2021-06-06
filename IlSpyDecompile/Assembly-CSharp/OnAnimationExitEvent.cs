using UnityEngine;

public class OnAnimationExitEvent : StateMachineBehaviour
{
	public string animationName;

	public float exitOffset;

	private float timeElapsed;

	private bool exitEventInvoked;

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		timeElapsed += Time.deltaTime;
		if (stateInfo.length - timeElapsed <= exitOffset && !exitEventInvoked)
		{
			GameUtils.OnAnimationExitEvent.Invoke(animationName);
			exitEventInvoked = true;
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!exitEventInvoked)
		{
			GameUtils.OnAnimationExitEvent.Invoke(animationName);
			exitEventInvoked = true;
		}
	}
}
