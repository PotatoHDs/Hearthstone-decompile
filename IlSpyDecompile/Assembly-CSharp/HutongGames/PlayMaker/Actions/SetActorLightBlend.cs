using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Show or Hide an Actor without messing up the game.")]
	public class SetActorLightBlend : FsmStateAction
	{
		public FsmGameObject m_ActorObject;

		[Tooltip("Light Blend Value")]
		public FsmFloat m_BlendValue;

		[Tooltip("Update Every Frame")]
		public bool m_EveryFrame;

		protected float m_initialLightBlendValue;

		protected Actor m_actor;

		public override void Reset()
		{
			m_ActorObject = null;
			m_BlendValue = 1f;
			m_EveryFrame = false;
			m_actor = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null && FindActor())
			{
				m_actor.SetLightBlend(m_BlendValue.Value);
			}
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (m_actor == null)
			{
				FindActor();
			}
			if (m_actor != null)
			{
				m_actor.SetLightBlend(m_BlendValue.Value);
			}
		}

		private bool FindActor()
		{
			if (m_ActorObject.IsNone || m_ActorObject.Value == null)
			{
				return false;
			}
			GameObject value = m_ActorObject.Value;
			if (value != null)
			{
				m_actor = value.GetComponentInChildren<Actor>();
				if (m_actor == null)
				{
					m_actor = SceneUtils.FindComponentInThisOrParents<Actor>(value);
					if (m_actor == null)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
