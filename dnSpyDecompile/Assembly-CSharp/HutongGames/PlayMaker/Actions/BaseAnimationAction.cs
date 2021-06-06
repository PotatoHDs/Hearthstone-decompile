using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD6 RID: 3030
	public abstract class BaseAnimationAction : ComponentAction<Animation>
	{
		// Token: 0x06009CBE RID: 40126 RVA: 0x00326744 File Offset: 0x00324944
		public override void OnActionTargetInvoked(object targetObject)
		{
			AnimationClip animationClip = targetObject as AnimationClip;
			if (animationClip == null)
			{
				return;
			}
			Animation component = base.Owner.GetComponent<Animation>();
			if (component != null)
			{
				component.AddClip(animationClip, animationClip.name);
			}
		}
	}
}
