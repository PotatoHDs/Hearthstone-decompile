using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE9 RID: 3049
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Fade to a fullscreen Color. NOTE: Uses OnGUI so requires a PlayMakerGUI component in the scene.")]
	public class CameraFadeOut : FsmStateAction
	{
		// Token: 0x06009D16 RID: 40214 RVA: 0x00327BE2 File Offset: 0x00325DE2
		public override void Reset()
		{
			this.color = Color.black;
			this.time = 1f;
			this.finishEvent = null;
		}

		// Token: 0x06009D17 RID: 40215 RVA: 0x00327C0B File Offset: 0x00325E0B
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			this.colorLerp = Color.clear;
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x00327C30 File Offset: 0x00325E30
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
			this.colorLerp = Color.Lerp(Color.clear, this.color.Value, this.currentTime / this.time.Value);
			if (this.currentTime > this.time.Value && this.finishEvent != null)
			{
				base.Fsm.Event(this.finishEvent);
			}
		}

		// Token: 0x06009D19 RID: 40217 RVA: 0x00327CC4 File Offset: 0x00325EC4
		public override void OnGUI()
		{
			Color color = GUI.color;
			GUI.color = this.colorLerp;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), ActionHelpers.WhiteTexture);
			GUI.color = color;
		}

		// Token: 0x04008299 RID: 33433
		[RequiredField]
		[Tooltip("Color to fade to. E.g., Fade to black.")]
		public FsmColor color;

		// Token: 0x0400829A RID: 33434
		[RequiredField]
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Fade out time in seconds.")]
		public FsmFloat time;

		// Token: 0x0400829B RID: 33435
		[Tooltip("Event to send when finished.")]
		public FsmEvent finishEvent;

		// Token: 0x0400829C RID: 33436
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x0400829D RID: 33437
		private float startTime;

		// Token: 0x0400829E RID: 33438
		private float currentTime;

		// Token: 0x0400829F RID: 33439
		private Color colorLerp;
	}
}
