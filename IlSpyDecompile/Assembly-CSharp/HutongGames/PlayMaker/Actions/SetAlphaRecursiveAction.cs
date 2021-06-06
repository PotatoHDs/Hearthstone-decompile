using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the alpha on a game object and its children.")]
	public class SetAlphaRecursiveAction : FsmStateAction
	{
		public FsmOwnerDefault m_GameObject;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Alpha;

		public bool m_EveryFrame;

		public bool m_IncludeChildren;

		public override void Reset()
		{
			m_GameObject = null;
			m_Alpha = 0f;
			m_EveryFrame = false;
			m_IncludeChildren = false;
		}

		public override void OnEnter()
		{
			if (base.Fsm.GetOwnerDefaultTarget(m_GameObject) == null)
			{
				Finish();
				return;
			}
			UpdateAlpha();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			UpdateAlpha();
		}

		private void UpdateAlpha()
		{
			if (!m_Alpha.IsNone)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
				if (!(ownerDefaultTarget == null))
				{
					RenderUtils.SetAlpha(ownerDefaultTarget, m_Alpha.Value);
				}
			}
		}
	}
}
