using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F56 RID: 3926
	[ActionCategory("Pegasus")]
	[Tooltip("Raycast from camera and send events. Mouse Down can have a rendom chance to send event")]
	public class MousePickRandomAction : FsmStateAction
	{
		// Token: 0x0600ACE6 RID: 44262 RVA: 0x0035F078 File Offset: 0x0035D278
		public override void Reset()
		{
			this.GameObject = null;
			this.additionalColliders = new FsmGameObject[0];
			this.RandomGateClicksMin = 0;
			this.RandomGateClicksMax = 0;
			this.ResetOnOpen = false;
			this.mouseDownGateOpen = null;
			this.mouseDownGateClosed = null;
			this.mouseOver = null;
			this.mouseUp = null;
			this.mouseOff = null;
			this.checkFirstFrame = true;
			this.everyFrame = true;
			this.oneShot = false;
			this.ClickCount = 0;
		}

		// Token: 0x0600ACE7 RID: 44263 RVA: 0x0035F100 File Offset: 0x0035D300
		public override void OnEnter()
		{
			if (this.RandomGateClicksMin.Value > this.RandomGateClicksMax.Value)
			{
				this.RandomGateClicksMin = this.RandomGateClicksMax;
			}
			if (this.m_opened)
			{
				this.m_RandomValue = UnityEngine.Random.Range(this.RandomGateClicksMin.Value, this.RandomGateClicksMax.Value);
				this.m_opened = false;
			}
			if (this.checkFirstFrame)
			{
				this.DoMousePickEvent();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600ACE8 RID: 44264 RVA: 0x0035F17D File Offset: 0x0035D37D
		public override void OnUpdate()
		{
			this.DoMousePickEvent();
		}

		// Token: 0x0600ACE9 RID: 44265 RVA: 0x0035F188 File Offset: 0x0035D388
		private void DoMousePickEvent()
		{
			bool flag = this.mouseOver != null || this.mouseOff != null;
			bool flag2 = this.mouseUp != null;
			bool flag3 = this.mouseDownGateOpen != null || this.mouseDownGateClosed != null;
			if (!flag && (!flag2 || !InputCollection.GetMouseButtonUp(0)) && (!flag3 || !InputCollection.GetMouseButtonDown(0)))
			{
				return;
			}
			GameObject gameObject = (this.GameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.GameObject.GameObject.Value;
			if (!InputUtil.IsPlayMakerMouseInputAllowed(gameObject))
			{
				return;
			}
			bool flag4 = UniversalInputManager.Get().InputIsOver(gameObject.gameObject);
			if (!flag4 && this.additionalColliders.Length != 0)
			{
				for (int i = 0; i < this.additionalColliders.Length; i++)
				{
					GameObject value = this.additionalColliders[i].Value;
					if (!(value == null))
					{
						flag4 = UniversalInputManager.Get().InputIsOver(value);
						if (flag4)
						{
							break;
						}
					}
				}
			}
			if (flag4)
			{
				if (UniversalInputManager.Get().GetMouseButtonDown(0))
				{
					this.ClickCount.Value = this.ClickCount.Value + 1;
					if (this.ClickCount.Value >= this.m_RandomValue)
					{
						this.m_opened = true;
						if (this.ResetOnOpen.Value)
						{
							this.ClickCount.Value = 0;
						}
						if (this.mouseDownGateOpen != null)
						{
							base.Fsm.Event(this.mouseDownGateOpen);
						}
					}
					else if (this.mouseDownGateClosed != null)
					{
						base.Fsm.Event(this.mouseDownGateClosed);
					}
					if (this.oneShot)
					{
						base.Finish();
					}
				}
				if (this.mouseOver != null)
				{
					base.Fsm.Event(this.mouseOver);
				}
				if (this.mouseUp != null && UniversalInputManager.Get().GetMouseButtonUp(0))
				{
					base.Fsm.Event(this.mouseUp);
					if (this.oneShot)
					{
						base.Finish();
						return;
					}
				}
			}
			else if (this.mouseOff != null)
			{
				base.Fsm.Event(this.mouseOff);
			}
		}

		// Token: 0x0600ACEA RID: 44266 RVA: 0x000D5239 File Offset: 0x000D3439
		public override string ErrorCheck()
		{
			return "";
		}

		// Token: 0x040093BB RID: 37819
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault GameObject;

		// Token: 0x040093BC RID: 37820
		[Tooltip("Additional Colliders for mouse pick")]
		public FsmGameObject[] additionalColliders;

		// Token: 0x040093BD RID: 37821
		[Tooltip("Min number of clicks before random gate open")]
		public FsmInt RandomGateClicksMin = 0;

		// Token: 0x040093BE RID: 37822
		[Tooltip("Max number of clicks before random gate open")]
		public FsmInt RandomGateClicksMax = 0;

		// Token: 0x040093BF RID: 37823
		[Tooltip("Resets count to 0 once triggered")]
		public FsmBool ResetOnOpen = false;

		// Token: 0x040093C0 RID: 37824
		[Tooltip("Mouse Down event. Random Gate open (true)")]
		public FsmEvent mouseDownGateOpen;

		// Token: 0x040093C1 RID: 37825
		[Tooltip("Mouse Down event. Random Gate is closed (false)")]
		public FsmEvent mouseDownGateClosed;

		// Token: 0x040093C2 RID: 37826
		[Tooltip("Mouse Over event")]
		public FsmEvent mouseOver;

		// Token: 0x040093C3 RID: 37827
		[Tooltip("Mouse Up event")]
		public FsmEvent mouseUp;

		// Token: 0x040093C4 RID: 37828
		[Tooltip("Mouse Off event")]
		public FsmEvent mouseOff;

		// Token: 0x040093C5 RID: 37829
		[Tooltip("Check for clicks as soon as the state machine enters this state.")]
		public bool checkFirstFrame;

		// Token: 0x040093C6 RID: 37830
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040093C7 RID: 37831
		[Tooltip("Stop processing after an event is triggered.")]
		public bool oneShot;

		// Token: 0x040093C8 RID: 37832
		[Tooltip("Click Count")]
		public FsmInt ClickCount = 0;

		// Token: 0x040093C9 RID: 37833
		private int m_RandomValue;

		// Token: 0x040093CA RID: 37834
		private bool m_opened = true;
	}
}
