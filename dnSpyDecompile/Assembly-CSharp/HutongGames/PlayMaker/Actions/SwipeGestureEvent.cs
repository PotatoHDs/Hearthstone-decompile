using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1C RID: 3612
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an event when a swipe is detected.")]
	public class SwipeGestureEvent : FsmStateAction
	{
		// Token: 0x0600A73D RID: 42813 RVA: 0x0034BC19 File Offset: 0x00349E19
		public override void Reset()
		{
			this.minSwipeDistance = 0.1f;
			this.swipeLeftEvent = null;
			this.swipeRightEvent = null;
			this.swipeUpEvent = null;
			this.swipeDownEvent = null;
		}

		// Token: 0x0600A73E RID: 42814 RVA: 0x0034BC47 File Offset: 0x00349E47
		public override void OnEnter()
		{
			this.screenDiagonalSize = Mathf.Sqrt((float)(Screen.width * Screen.width + Screen.height * Screen.height));
			this.minSwipeDistancePixels = this.minSwipeDistance.Value * this.screenDiagonalSize;
		}

		// Token: 0x0600A73F RID: 42815 RVA: 0x0034BC84 File Offset: 0x00349E84
		public override void OnUpdate()
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.touches[0];
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
		}

		// Token: 0x0600A740 RID: 42816 RVA: 0x0034BCFC File Offset: 0x00349EFC
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

		// Token: 0x04008DBC RID: 36284
		[Tooltip("How far a touch has to travel to be considered a swipe. Uses normalized distance (e.g. 1 = 1 screen diagonal distance). Should generally be a very small number.")]
		public FsmFloat minSwipeDistance;

		// Token: 0x04008DBD RID: 36285
		[Tooltip("Event to send when swipe left detected.")]
		public FsmEvent swipeLeftEvent;

		// Token: 0x04008DBE RID: 36286
		[Tooltip("Event to send when swipe right detected.")]
		public FsmEvent swipeRightEvent;

		// Token: 0x04008DBF RID: 36287
		[Tooltip("Event to send when swipe up detected.")]
		public FsmEvent swipeUpEvent;

		// Token: 0x04008DC0 RID: 36288
		[Tooltip("Event to send when swipe down detected.")]
		public FsmEvent swipeDownEvent;

		// Token: 0x04008DC1 RID: 36289
		private float screenDiagonalSize;

		// Token: 0x04008DC2 RID: 36290
		private float minSwipeDistancePixels;

		// Token: 0x04008DC3 RID: 36291
		private bool touchStarted;

		// Token: 0x04008DC4 RID: 36292
		private Vector2 touchStartPos;
	}
}
