using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F61 RID: 3937
	[ActionCategory("Pegasus")]
	[Tooltip("Sends an event when a swipe is detected.")]
	public class PegasusSwipeGestureEventAction : FsmStateAction
	{
		// Token: 0x0600AD0E RID: 44302 RVA: 0x0035FB6D File Offset: 0x0035DD6D
		public override void Reset()
		{
			this.minSwipeDistance = 0.1f;
			this.swipeLeftEvent = null;
			this.swipeRightEvent = null;
			this.swipeUpEvent = null;
			this.swipeDownEvent = null;
		}

		// Token: 0x0600AD0F RID: 44303 RVA: 0x0035FB9C File Offset: 0x0035DD9C
		public override void OnEnter()
		{
			if (!UniversalInputManager.Get().IsTouchMode() || !this.mouseSupport.Value)
			{
				base.Finish();
				return;
			}
			this.screenDiagonalSize = Mathf.Sqrt((float)(Screen.width * Screen.width + Screen.height * Screen.height));
			this.minSwipeDistancePixels = this.minSwipeDistance.Value * this.screenDiagonalSize;
		}

		// Token: 0x0600AD10 RID: 44304 RVA: 0x0035FC04 File Offset: 0x0035DE04
		public override void OnUpdate()
		{
			Touch touch;
			if (Input.touchCount > 0)
			{
				touch = Input.touches[0];
			}
			else
			{
				if (!this.mouseSupport.Value)
				{
					return;
				}
				touch = default(Touch);
				touch.position = Input.mousePosition;
				if (Input.GetMouseButtonDown(0))
				{
					touch.phase = TouchPhase.Began;
				}
				else
				{
					if (!this.touchStarted || !Input.GetMouseButtonUp(0))
					{
						return;
					}
					touch.phase = TouchPhase.Ended;
				}
			}
			switch (touch.phase)
			{
			case TouchPhase.Began:
				this.touchStarted = true;
				this.touchStartPos = touch.position;
				return;
			case TouchPhase.Moved:
			case TouchPhase.Stationary:
				break;
			case TouchPhase.Ended:
				if (this.touchStarted)
				{
					this.TestForSwipeGesture(touch);
					this.touchStarted = false;
					return;
				}
				break;
			case TouchPhase.Canceled:
				this.touchStarted = false;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600AD11 RID: 44305 RVA: 0x0035FCD4 File Offset: 0x0035DED4
		private void TestForSwipeGesture(Touch touch)
		{
			Vector2 position = touch.position;
			if (Vector2.Distance(position, this.touchStartPos) > this.minSwipeDistancePixels)
			{
				float x = position.y - this.touchStartPos.y;
				float y = position.x - this.touchStartPos.x;
				float num = 57.29578f * Mathf.Atan2(y, x);
				num = (360f + num - 45f) % 360f;
				Debug.Log(num);
				if (num < 90f)
				{
					base.Fsm.Event(this.swipeRightEvent);
					return;
				}
				if (num < 180f)
				{
					base.Fsm.Event(this.swipeDownEvent);
					return;
				}
				if (num < 270f)
				{
					base.Fsm.Event(this.swipeLeftEvent);
					return;
				}
				base.Fsm.Event(this.swipeUpEvent);
			}
		}

		// Token: 0x040093ED RID: 37869
		[Tooltip("How far a touch has to travel to be considered a swipe. Uses normalized distance (e.g. 1 = 1 screen diagonal distance). Should generally be a very small number.")]
		public FsmFloat minSwipeDistance;

		// Token: 0x040093EE RID: 37870
		[Tooltip("Event to send when swipe left detected.")]
		public FsmEvent swipeLeftEvent;

		// Token: 0x040093EF RID: 37871
		[Tooltip("Event to send when swipe right detected.")]
		public FsmEvent swipeRightEvent;

		// Token: 0x040093F0 RID: 37872
		[Tooltip("Event to send when swipe up detected.")]
		public FsmEvent swipeUpEvent;

		// Token: 0x040093F1 RID: 37873
		[Tooltip("Event to send when swipe down detected.")]
		public FsmEvent swipeDownEvent;

		// Token: 0x040093F2 RID: 37874
		[Tooltip("If checked, accept mouse gestures as touch input.")]
		public FsmBool mouseSupport;

		// Token: 0x040093F3 RID: 37875
		private float screenDiagonalSize;

		// Token: 0x040093F4 RID: 37876
		private float minSwipeDistancePixels;

		// Token: 0x040093F5 RID: 37877
		private bool touchStarted;

		// Token: 0x040093F6 RID: 37878
		private Vector2 touchStartPos;
	}
}
