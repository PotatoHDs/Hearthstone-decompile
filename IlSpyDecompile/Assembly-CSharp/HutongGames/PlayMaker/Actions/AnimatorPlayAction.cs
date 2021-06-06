using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Enables an Animator and plays one of its states.")]
	public class AnimatorPlayAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		public FsmString m_StateName;

		public FsmString m_LayerName;

		[Tooltip("Percent of time into the animation at which to start playing.")]
		[HasFloatSlider(0f, 100f)]
		public FsmFloat m_StartTimePercent;

		public override void Reset()
		{
			m_GameObject = null;
			m_StateName = null;
			m_LayerName = new FsmString
			{
				UseVariable = true
			};
			m_StartTimePercent = 0f;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!ownerDefaultTarget)
			{
				Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if ((bool)component)
			{
				int layer = -1;
				if (!m_LayerName.IsNone)
				{
					layer = AnimationUtil.GetLayerIndexFromName(component, m_LayerName.Value);
				}
				float normalizedTime = float.NegativeInfinity;
				if (!m_StartTimePercent.IsNone)
				{
					normalizedTime = 0.01f * m_StartTimePercent.Value;
				}
				component.enabled = true;
				component.Play(m_StateName.Value, layer, normalizedTime);
			}
			Finish();
		}
	}
}
