using System;
using UnityEngine;

// Token: 0x020009CE RID: 2510
public class OnAnimationExitEvent : StateMachineBehaviour
{
	// Token: 0x060088CD RID: 35021 RVA: 0x002C0AF0 File Offset: 0x002BECF0
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		this.timeElapsed += Time.deltaTime;
		if (stateInfo.length - this.timeElapsed <= this.exitOffset && !this.exitEventInvoked)
		{
			GameUtils.OnAnimationExitEvent.Invoke(this.animationName);
			this.exitEventInvoked = true;
		}
	}

	// Token: 0x060088CE RID: 35022 RVA: 0x002C0B4D File Offset: 0x002BED4D
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!this.exitEventInvoked)
		{
			GameUtils.OnAnimationExitEvent.Invoke(this.animationName);
			this.exitEventInvoked = true;
		}
	}

	// Token: 0x040072D9 RID: 29401
	public string animationName;

	// Token: 0x040072DA RID: 29402
	public float exitOffset;

	// Token: 0x040072DB RID: 29403
	private float timeElapsed;

	// Token: 0x040072DC RID: 29404
	private bool exitEventInvoked;
}
