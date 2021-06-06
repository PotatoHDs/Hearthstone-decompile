using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F13 RID: 3859
	[ActionCategory("Pegasus")]
	[Tooltip("Enables an Animator and plays one of its states and waits for it to complete.")]
	public class AnimatorPlaythroughAction : FsmStateAction
	{
		// Token: 0x0600ABCC RID: 43980 RVA: 0x0035A033 File Offset: 0x00358233
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_StateName = null;
			this.m_LayerName = new FsmString
			{
				UseVariable = true
			};
			this.m_StartTimePercent = 0f;
		}

		// Token: 0x0600ABCD RID: 43981 RVA: 0x0035A068 File Offset: 0x00358268
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (!ownerDefaultTarget)
			{
				base.Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component)
			{
				int num = -1;
				if (!this.m_LayerName.IsNone)
				{
					num = AnimationUtil.GetLayerIndexFromName(component, this.m_LayerName.Value);
				}
				float normalizedTime = float.NegativeInfinity;
				if (!this.m_StartTimePercent.IsNone)
				{
					normalizedTime = 0.01f * this.m_StartTimePercent.Value;
				}
				component.enabled = true;
				component.Play(this.m_StateName.Value, num, normalizedTime);
				this.m_checkComplete = component;
				this.m_checkLayer = ((num == -1) ? 0 : num);
				return;
			}
			base.Finish();
		}

		// Token: 0x0600ABCE RID: 43982 RVA: 0x0035A124 File Offset: 0x00358324
		public override void OnUpdate()
		{
			if (this.m_checkComplete == null)
			{
				return;
			}
			if (this.m_checkComplete.GetCurrentAnimatorStateInfo(this.m_checkLayer).normalizedTime > 1f)
			{
				this.m_checkComplete = null;
				base.Finish();
			}
		}

		// Token: 0x04009283 RID: 37507
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009284 RID: 37508
		public FsmString m_StateName;

		// Token: 0x04009285 RID: 37509
		public FsmString m_LayerName;

		// Token: 0x04009286 RID: 37510
		[Tooltip("Percent of time into the animation at which to start playing.")]
		[HasFloatSlider(0f, 100f)]
		public FsmFloat m_StartTimePercent;

		// Token: 0x04009287 RID: 37511
		private AnimatorStateInfo m_currentAnimationState;

		// Token: 0x04009288 RID: 37512
		private Animator m_checkComplete;

		// Token: 0x04009289 RID: 37513
		private int m_checkLayer = -1;
	}
}
