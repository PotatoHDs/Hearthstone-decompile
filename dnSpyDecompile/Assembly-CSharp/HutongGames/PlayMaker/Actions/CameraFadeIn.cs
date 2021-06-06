using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE8 RID: 3048
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Fade from a fullscreen Color. NOTE: Uses OnGUI so requires a PlayMakerGUI component in the scene.")]
	public class CameraFadeIn : FsmStateAction
	{
		// Token: 0x06009D11 RID: 40209 RVA: 0x00327AB9 File Offset: 0x00325CB9
		public override void Reset()
		{
			this.color = Color.black;
			this.time = 1f;
			this.finishEvent = null;
		}

		// Token: 0x06009D12 RID: 40210 RVA: 0x00327AE2 File Offset: 0x00325CE2
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			this.colorLerp = this.color.Value;
		}

		// Token: 0x06009D13 RID: 40211 RVA: 0x00327B0C File Offset: 0x00325D0C
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.currentTime = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.currentTime += Time.deltaTime;
			}
			this.colorLerp = Color.Lerp(this.color.Value, Color.clear, this.currentTime / this.time.Value);
			if (this.currentTime > this.time.Value)
			{
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				base.Finish();
			}
		}

		// Token: 0x06009D14 RID: 40212 RVA: 0x00327BA6 File Offset: 0x00325DA6
		public override void OnGUI()
		{
			Color color = GUI.color;
			GUI.color = this.colorLerp;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), ActionHelpers.WhiteTexture);
			GUI.color = color;
		}

		// Token: 0x04008292 RID: 33426
		[RequiredField]
		[Tooltip("Color to fade from. E.g., Fade up from black.")]
		public FsmColor color;

		// Token: 0x04008293 RID: 33427
		[RequiredField]
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Fade in time in seconds.")]
		public FsmFloat time;

		// Token: 0x04008294 RID: 33428
		[Tooltip("Event to send when finished.")]
		public FsmEvent finishEvent;

		// Token: 0x04008295 RID: 33429
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x04008296 RID: 33430
		private float startTime;

		// Token: 0x04008297 RID: 33431
		private float currentTime;

		// Token: 0x04008298 RID: 33432
		private Color colorLerp;
	}
}
