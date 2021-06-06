using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F55 RID: 3925
	[ActionCategory("Pegasus")]
	[Tooltip("Fires events based on how long you hold the left mouse button over a GameObject.")]
	public class MouseHoldAction : FsmStateAction
	{
		// Token: 0x0600ACDE RID: 44254 RVA: 0x0035EEAC File Offset: 0x0035D0AC
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_HoldTime = 0f;
			this.m_HoldEvent = null;
			this.m_CancelEvent = null;
			this.m_holdingSec = 0f;
			this.m_suppressHoldEvent = false;
		}

		// Token: 0x0600ACDF RID: 44255 RVA: 0x0035EEE5 File Offset: 0x0035D0E5
		public override void OnEnter()
		{
			this.m_holdingSec = 0f;
			this.m_suppressHoldEvent = false;
			this.CheckHold(false);
		}

		// Token: 0x0600ACE0 RID: 44256 RVA: 0x0035EF00 File Offset: 0x0035D100
		public override void OnUpdate()
		{
			this.CheckHold(true);
		}

		// Token: 0x0600ACE1 RID: 44257 RVA: 0x0035EF09 File Offset: 0x0035D109
		public override string ErrorCheck()
		{
			if (this.m_UseHoldTime.Value && FsmEvent.IsNullOrEmpty(this.m_HoldEvent))
			{
				return "Use Hold Time is checked but there is no Hold Event";
			}
			if (this.m_HoldTime.Value < 0f)
			{
				return "Hold Time cannot be less than 0";
			}
			return string.Empty;
		}

		// Token: 0x0600ACE2 RID: 44258 RVA: 0x0035EF48 File Offset: 0x0035D148
		private void CheckHold(bool updating)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
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
					this.HandleHold();
				}
				else
				{
					this.HandleCancel();
				}
			}
			if (flag && mouseButton)
			{
				this.m_holdingSec += Time.deltaTime;
			}
		}

		// Token: 0x0600ACE3 RID: 44259 RVA: 0x0035EFB8 File Offset: 0x0035D1B8
		private void HandleHold()
		{
			if (this.m_suppressHoldEvent)
			{
				return;
			}
			if (this.m_UseHoldTime.Value && this.m_holdingSec >= this.m_HoldTime.Value)
			{
				this.m_suppressHoldEvent = true;
				base.Fsm.Event(this.m_HoldEvent);
			}
		}

		// Token: 0x0600ACE4 RID: 44260 RVA: 0x0035F008 File Offset: 0x0035D208
		private void HandleCancel()
		{
			float holdingSec = this.m_holdingSec;
			this.m_holdingSec = 0f;
			if (this.m_suppressHoldEvent)
			{
				this.m_suppressHoldEvent = false;
				return;
			}
			if (this.m_UseHoldTime.Value && holdingSec >= this.m_HoldTime.Value)
			{
				base.Fsm.Event(this.m_HoldEvent);
				return;
			}
			base.Fsm.Event(this.m_CancelEvent);
		}

		// Token: 0x040093B4 RID: 37812
		[RequiredField]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093B5 RID: 37813
		[Tooltip("Whether or not to fire the Hold Event after the Hold Time.")]
		public FsmBool m_UseHoldTime;

		// Token: 0x040093B6 RID: 37814
		[Tooltip("How many seconds to wait before firing the Hold Event.")]
		public FsmFloat m_HoldTime;

		// Token: 0x040093B7 RID: 37815
		[Tooltip("Fired after the Hold Time passes if Use Hold Time is enabled.")]
		public FsmEvent m_HoldEvent;

		// Token: 0x040093B8 RID: 37816
		[Tooltip("Fired if the player mouses off OR releases the left mouse button before the Hold Time.")]
		public FsmEvent m_CancelEvent;

		// Token: 0x040093B9 RID: 37817
		private float m_holdingSec;

		// Token: 0x040093BA RID: 37818
		private bool m_suppressHoldEvent;
	}
}
