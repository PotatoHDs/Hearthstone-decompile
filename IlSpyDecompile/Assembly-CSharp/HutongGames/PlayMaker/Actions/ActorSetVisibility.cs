using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Show or Hide an Actor without messing up the game.")]
	public class ActorSetVisibility : ActorAction
	{
		public FsmGameObject m_ActorObject;

		[Tooltip("Should the Actor be set to visible or invisible?")]
		public FsmBool m_Visible;

		[Tooltip("Don't touch the Actor's SpellTable when setting visibility")]
		public FsmBool m_IgnoreSpells;

		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool m_ResetOnExit;

		protected bool m_initialVisibility;

		protected override GameObject GetActorOwner()
		{
			return m_ActorObject.Value;
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_Visible = false;
			m_IgnoreSpells = false;
			m_ResetOnExit = false;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			m_initialVisibility = m_actor.IsShown();
			if (m_Visible.Value)
			{
				ShowActor();
			}
			else
			{
				HideActor();
			}
			Finish();
		}

		public override void OnExit()
		{
			if (m_ResetOnExit)
			{
				if (m_initialVisibility)
				{
					ShowActor();
				}
				else
				{
					HideActor();
				}
			}
		}

		public void ShowActor()
		{
			m_actor.Show(m_IgnoreSpells.Value);
		}

		public void HideActor()
		{
			m_actor.Hide(m_IgnoreSpells.Value);
		}
	}
}
