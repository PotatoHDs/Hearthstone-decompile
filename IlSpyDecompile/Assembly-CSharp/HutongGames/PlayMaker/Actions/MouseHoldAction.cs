using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Fires events based on how long you hold the left mouse button over a GameObject.")]
	public class MouseHoldAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Whether or not to fire the Hold Event after the Hold Time.")]
		public FsmBool m_UseHoldTime;

		[Tooltip("How many seconds to wait before firing the Hold Event.")]
		public FsmFloat m_HoldTime;

		[Tooltip("Fired after the Hold Time passes if Use Hold Time is enabled.")]
		public FsmEvent m_HoldEvent;

		[Tooltip("Fired if the player mouses off OR releases the left mouse button before the Hold Time.")]
		public FsmEvent m_CancelEvent;

		private float m_holdingSec;

		private bool m_suppressHoldEvent;

		public override void Reset()
		{
			m_GameObject = null;
			m_HoldTime = 0f;
			m_HoldEvent = null;
			m_CancelEvent = null;
			m_holdingSec = 0f;
			m_suppressHoldEvent = false;
		}

		public override void OnEnter()
		{
			m_holdingSec = 0f;
			m_suppressHoldEvent = false;
			CheckHold(updating: false);
		}

		public override void OnUpdate()
		{
			CheckHold(updating: true);
		}

		public override string ErrorCheck()
		{
			if (m_UseHoldTime.Value && FsmEvent.IsNullOrEmpty(m_HoldEvent))
			{
				return "Use Hold Time is checked but there is no Hold Event";
			}
			if (m_HoldTime.Value < 0f)
			{
				return "Hold Time cannot be less than 0";
			}
			return string.Empty;
		}

		private void CheckHold(bool updating)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!InputUtil.IsPlayMakerMouseInputAllowed(ownerDefaultTarget))
			{
				return;
			}
			bool flag = UniversalInputManager.Get().InputIsOver(ownerDefaultTarget);
			bool mouseButton = UniversalInputManager.Get().GetMouseButton(0);
			if (updating)
			{
				if (flag && mouseButton)
				{
					HandleHold();
				}
				else
				{
					HandleCancel();
				}
			}
			if (flag && mouseButton)
			{
				m_holdingSec += Time.deltaTime;
			}
		}

		private void HandleHold()
		{
			if (!m_suppressHoldEvent && m_UseHoldTime.Value && m_holdingSec >= m_HoldTime.Value)
			{
				m_suppressHoldEvent = true;
				base.Fsm.Event(m_HoldEvent);
			}
		}

		private void HandleCancel()
		{
			float holdingSec = m_holdingSec;
			m_holdingSec = 0f;
			if (m_suppressHoldEvent)
			{
				m_suppressHoldEvent = false;
			}
			else if (m_UseHoldTime.Value && holdingSec >= m_HoldTime.Value)
			{
				base.Fsm.Event(m_HoldEvent);
			}
			else
			{
				base.Fsm.Event(m_CancelEvent);
			}
		}
	}
}
