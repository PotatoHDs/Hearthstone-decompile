using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F72 RID: 3954
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the speed of an Animation.")]
	public class SetAnimationSpeedAction : FsmStateAction
	{
		// Token: 0x0600AD4D RID: 44365 RVA: 0x00360822 File Offset: 0x0035EA22
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_AnimName = null;
			this.m_PhoneAnimName = null;
			this.m_Speed = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AD4E RID: 44366 RVA: 0x00360850 File Offset: 0x0035EA50
		public override void OnEnter()
		{
			if (!this.CacheAnim())
			{
				base.Finish();
				return;
			}
			this.UpdateSpeed();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD4F RID: 44367 RVA: 0x00360878 File Offset: 0x0035EA78
		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			if (ownerDefaultTarget.GetComponent<Animation>() == null)
			{
				Debug.LogWarning(string.Format("SetAnimationSpeedAction.OnUpdate() - GameObject {0} is missing an animation component", ownerDefaultTarget));
				base.Finish();
				return;
			}
			this.UpdateSpeed();
		}

		// Token: 0x0600AD50 RID: 44368 RVA: 0x003608D4 File Offset: 0x0035EAD4
		private bool CacheAnim()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return false;
			}
			string value = this.m_AnimName.Value;
			if (UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(this.m_PhoneAnimName.Value))
			{
				value = this.m_PhoneAnimName.Value;
			}
			this.m_animState = ownerDefaultTarget.GetComponent<Animation>()[value];
			return true;
		}

		// Token: 0x0600AD51 RID: 44369 RVA: 0x00360947 File Offset: 0x0035EB47
		private void UpdateSpeed()
		{
			this.m_animState.speed = this.m_Speed.Value;
		}

		// Token: 0x0400942B RID: 37931
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x0400942C RID: 37932
		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		// Token: 0x0400942D RID: 37933
		public FsmString m_PhoneAnimName;

		// Token: 0x0400942E RID: 37934
		public FsmFloat m_Speed;

		// Token: 0x0400942F RID: 37935
		public bool m_EveryFrame;

		// Token: 0x04009430 RID: 37936
		private AnimationState m_animState;
	}
}
