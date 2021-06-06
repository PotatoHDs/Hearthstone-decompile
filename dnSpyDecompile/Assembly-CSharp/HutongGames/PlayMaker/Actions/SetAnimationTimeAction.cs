using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F73 RID: 3955
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the speed of an Animation.")]
	public class SetAnimationTimeAction : FsmStateAction
	{
		// Token: 0x0600AD53 RID: 44371 RVA: 0x0036095F File Offset: 0x0035EB5F
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_AnimName = null;
			this.m_PhoneAnimName = null;
			this.m_Time = 0f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AD54 RID: 44372 RVA: 0x0036098D File Offset: 0x0035EB8D
		public override void OnEnter()
		{
			if (!this.CacheAnim())
			{
				base.Finish();
				return;
			}
			this.UpdateTime();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD55 RID: 44373 RVA: 0x003609B4 File Offset: 0x0035EBB4
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
			this.UpdateTime();
		}

		// Token: 0x0600AD56 RID: 44374 RVA: 0x00360A10 File Offset: 0x0035EC10
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

		// Token: 0x0600AD57 RID: 44375 RVA: 0x00360A83 File Offset: 0x0035EC83
		private void UpdateTime()
		{
			this.m_animState.time = this.m_Time.Value;
		}

		// Token: 0x04009431 RID: 37937
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009432 RID: 37938
		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		// Token: 0x04009433 RID: 37939
		public FsmString m_PhoneAnimName;

		// Token: 0x04009434 RID: 37940
		public FsmFloat m_Time;

		// Token: 0x04009435 RID: 37941
		public bool m_EveryFrame;

		// Token: 0x04009436 RID: 37942
		private AnimationState m_animState;
	}
}
