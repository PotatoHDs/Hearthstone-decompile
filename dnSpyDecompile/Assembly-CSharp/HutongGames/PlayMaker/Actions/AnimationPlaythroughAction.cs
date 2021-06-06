using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F11 RID: 3857
	[ActionCategory("Pegasus")]
	[Tooltip("Plays an Animation on a Game Object. Does not wait for the animation to finish.")]
	public class AnimationPlaythroughAction : FsmStateAction
	{
		// Token: 0x0600ABC5 RID: 43973 RVA: 0x00359E59 File Offset: 0x00358059
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_AnimName = null;
			this.m_PhoneAnimName = null;
			this.m_PlayMode = PlayMode.StopAll;
			this.m_CrossFadeSec = 0.3f;
		}

		// Token: 0x0600ABC6 RID: 43974 RVA: 0x00359E88 File Offset: 0x00358088
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Debug.LogWarning("AnimationPlaythroughAction GameObject is null!");
				base.Finish();
				return;
			}
			string value = this.m_AnimName.Value;
			if (UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(this.m_PhoneAnimName.Value))
			{
				value = this.m_PhoneAnimName.Value;
			}
			if (string.IsNullOrEmpty(value))
			{
				base.Finish();
				return;
			}
			this.StartAnimation(ownerDefaultTarget, value);
			base.Finish();
		}

		// Token: 0x0600ABC7 RID: 43975 RVA: 0x00359F14 File Offset: 0x00358114
		private void StartAnimation(GameObject go, string animName)
		{
			float value = this.m_CrossFadeSec.Value;
			if (value <= Mathf.Epsilon)
			{
				go.GetComponent<Animation>().Play(animName, this.m_PlayMode);
				return;
			}
			go.GetComponent<Animation>().CrossFade(animName, value, this.m_PlayMode);
		}

		// Token: 0x0400927A RID: 37498
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x0400927B RID: 37499
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		// Token: 0x0400927C RID: 37500
		public FsmString m_PhoneAnimName;

		// Token: 0x0400927D RID: 37501
		[Tooltip("How to treat previously playing animations.")]
		public PlayMode m_PlayMode;

		// Token: 0x0400927E RID: 37502
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time taken to cross fade to this animation.")]
		public FsmFloat m_CrossFadeSec;
	}
}
