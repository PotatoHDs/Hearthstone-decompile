using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F12 RID: 3858
	[ActionCategory("Pegasus")]
	[Tooltip("Enables an Animator and plays one of its states.")]
	public class AnimatorPlayAction : FsmStateAction
	{
		// Token: 0x0600ABC9 RID: 43977 RVA: 0x00359F5C File Offset: 0x0035815C
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

		// Token: 0x0600ABCA RID: 43978 RVA: 0x00359F90 File Offset: 0x00358190
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
				int layer = -1;
				if (!this.m_LayerName.IsNone)
				{
					layer = AnimationUtil.GetLayerIndexFromName(component, this.m_LayerName.Value);
				}
				float normalizedTime = float.NegativeInfinity;
				if (!this.m_StartTimePercent.IsNone)
				{
					normalizedTime = 0.01f * this.m_StartTimePercent.Value;
				}
				component.enabled = true;
				component.Play(this.m_StateName.Value, layer, normalizedTime);
			}
			base.Finish();
		}

		// Token: 0x0400927F RID: 37503
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009280 RID: 37504
		public FsmString m_StateName;

		// Token: 0x04009281 RID: 37505
		public FsmString m_LayerName;

		// Token: 0x04009282 RID: 37506
		[Tooltip("Percent of time into the animation at which to start playing.")]
		[HasFloatSlider(0f, 100f)]
		public FsmFloat m_StartTimePercent;
	}
}
