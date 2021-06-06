using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA6 RID: 2982
	public abstract class AnimateFsmAction : FsmStateAction
	{
		// Token: 0x06009BCF RID: 39887 RVA: 0x00320C44 File Offset: 0x0031EE44
		public override void Reset()
		{
			this.finishEvent = null;
			this.realTime = false;
			this.time = new FsmFloat
			{
				UseVariable = true
			};
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
			this.delay = new FsmFloat
			{
				UseVariable = true
			};
			this.ignoreCurveOffset = new FsmBool
			{
				Value = true
			};
			this.resultFloats = new float[0];
			this.fromFloats = new float[0];
			this.toFloats = new float[0];
			this.endTimes = new float[0];
			this.keyOffsets = new float[0];
			this.curves = new AnimationCurve[0];
			this.finishAction = false;
			this.start = false;
		}

		// Token: 0x06009BD0 RID: 39888 RVA: 0x00320D00 File Offset: 0x0031EF00
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
			this.deltaTime = 0f;
			this.currentTime = 0f;
			this.isRunning = false;
			this.finishAction = false;
			this.looping = false;
			this.delayTime = (this.delay.IsNone ? 0f : (this.delayTime = this.delay.Value));
			this.start = true;
		}

		// Token: 0x06009BD1 RID: 39889 RVA: 0x00320D8C File Offset: 0x0031EF8C
		protected void Init()
		{
			this.endTimes = new float[this.curves.Length];
			this.keyOffsets = new float[this.curves.Length];
			this.largestEndTime = 0f;
			for (int i = 0; i < this.curves.Length; i++)
			{
				if (this.curves[i] != null && this.curves[i].keys.Length != 0)
				{
					this.keyOffsets[i] = ((this.curves[i].keys.Length != 0) ? (this.time.IsNone ? this.curves[i].keys[0].time : (this.time.Value / this.curves[i].keys[this.curves[i].length - 1].time * this.curves[i].keys[0].time)) : 0f);
					this.currentTime = (this.ignoreCurveOffset.IsNone ? 0f : (this.ignoreCurveOffset.Value ? this.keyOffsets[i] : 0f));
					if (!this.time.IsNone)
					{
						this.endTimes[i] = this.time.Value;
					}
					else
					{
						this.endTimes[i] = this.curves[i].keys[this.curves[i].length - 1].time;
					}
					if (this.largestEndTime < this.endTimes[i])
					{
						this.largestEndTime = this.endTimes[i];
					}
					if (!this.looping)
					{
						this.looping = ActionHelpers.IsLoopingWrapMode(this.curves[i].postWrapMode);
					}
				}
				else
				{
					this.endTimes[i] = -1f;
				}
			}
			for (int j = 0; j < this.curves.Length; j++)
			{
				if (this.largestEndTime > 0f && this.endTimes[j] == -1f)
				{
					this.endTimes[j] = this.largestEndTime;
				}
				else if (this.largestEndTime == 0f && this.endTimes[j] == -1f)
				{
					if (this.time.IsNone)
					{
						this.endTimes[j] = 1f;
					}
					else
					{
						this.endTimes[j] = this.time.Value;
					}
				}
			}
			this.UpdateAnimation();
		}

		// Token: 0x06009BD2 RID: 39890 RVA: 0x00320FFA File Offset: 0x0031F1FA
		public override void OnUpdate()
		{
			this.CheckStart();
			if (this.isRunning)
			{
				this.UpdateTime();
				this.UpdateAnimation();
				this.CheckFinished();
			}
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x0032101C File Offset: 0x0031F21C
		private void CheckStart()
		{
			if (!this.isRunning && this.start)
			{
				if (this.delayTime >= 0f)
				{
					if (this.realTime)
					{
						this.deltaTime = FsmTime.RealtimeSinceStartup - this.startTime - this.lastTime;
						this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
						this.delayTime -= this.deltaTime;
						return;
					}
					this.delayTime -= Time.deltaTime;
					return;
				}
				else
				{
					this.isRunning = true;
					this.start = false;
				}
			}
		}

		// Token: 0x06009BD4 RID: 39892 RVA: 0x003210B0 File Offset: 0x0031F2B0
		private void UpdateTime()
		{
			if (this.realTime)
			{
				this.deltaTime = FsmTime.RealtimeSinceStartup - this.startTime - this.lastTime;
				this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
				if (!this.speed.IsNone)
				{
					this.currentTime += this.deltaTime * this.speed.Value;
					return;
				}
				this.currentTime += this.deltaTime;
				return;
			}
			else
			{
				if (!this.speed.IsNone)
				{
					this.currentTime += Time.deltaTime * this.speed.Value;
					return;
				}
				this.currentTime += Time.deltaTime;
				return;
			}
		}

		// Token: 0x06009BD5 RID: 39893 RVA: 0x00321170 File Offset: 0x0031F370
		public void UpdateAnimation()
		{
			for (int i = 0; i < this.curves.Length; i++)
			{
				if (this.curves[i] != null && this.curves[i].keys.Length != 0)
				{
					if (this.calculations[i] != AnimateFsmAction.Calculation.None)
					{
						switch (this.calculations[i])
						{
						case AnimateFsmAction.Calculation.SetValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time);
							}
							else
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime);
							}
							break;
						case AnimateFsmAction.Calculation.AddToValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = this.fromFloats[i] + this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time);
							}
							else
							{
								this.resultFloats[i] = this.fromFloats[i] + this.curves[i].Evaluate(this.currentTime);
							}
							break;
						case AnimateFsmAction.Calculation.SubtractFromValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = this.fromFloats[i] - this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time);
							}
							else
							{
								this.resultFloats[i] = this.fromFloats[i] - this.curves[i].Evaluate(this.currentTime);
							}
							break;
						case AnimateFsmAction.Calculation.SubtractValueFromCurve:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) - this.fromFloats[i];
							}
							else
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime) - this.fromFloats[i];
							}
							break;
						case AnimateFsmAction.Calculation.MultiplyValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) * this.fromFloats[i];
							}
							else
							{
								this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime) * this.fromFloats[i];
							}
							break;
						case AnimateFsmAction.Calculation.DivideValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = ((this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) != 0f) ? (this.fromFloats[i] / this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time)) : float.MaxValue);
							}
							else
							{
								this.resultFloats[i] = ((this.curves[i].Evaluate(this.currentTime) != 0f) ? (this.fromFloats[i] / this.curves[i].Evaluate(this.currentTime)) : float.MaxValue);
							}
							break;
						case AnimateFsmAction.Calculation.DivideCurveByValue:
							if (!this.time.IsNone)
							{
								this.resultFloats[i] = ((this.fromFloats[i] != 0f) ? (this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) / this.fromFloats[i]) : float.MaxValue);
							}
							else
							{
								this.resultFloats[i] = ((this.fromFloats[i] != 0f) ? (this.curves[i].Evaluate(this.currentTime) / this.fromFloats[i]) : float.MaxValue);
							}
							break;
						}
					}
					else
					{
						this.resultFloats[i] = this.fromFloats[i];
					}
				}
				else
				{
					this.resultFloats[i] = this.fromFloats[i];
				}
			}
		}

		// Token: 0x06009BD6 RID: 39894 RVA: 0x003216A4 File Offset: 0x0031F8A4
		private void CheckFinished()
		{
			if (this.isRunning && !this.looping)
			{
				this.finishAction = true;
				for (int i = 0; i < this.endTimes.Length; i++)
				{
					if (this.currentTime < this.endTimes[i])
					{
						this.finishAction = false;
					}
				}
				this.isRunning = !this.finishAction;
			}
		}

		// Token: 0x04008128 RID: 33064
		[Tooltip("Define animation time,\u00a0scaling the curve to fit.")]
		public FsmFloat time;

		// Token: 0x04008129 RID: 33065
		[Tooltip("If you define speed, your animation will speed up or slow down.")]
		public FsmFloat speed;

		// Token: 0x0400812A RID: 33066
		[Tooltip("Delayed animation start.")]
		public FsmFloat delay;

		// Token: 0x0400812B RID: 33067
		[Tooltip("Animation curve start from any time. If IgnoreCurveOffset is true the animation starts right after the state become entered.")]
		public FsmBool ignoreCurveOffset;

		// Token: 0x0400812C RID: 33068
		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x0400812D RID: 33069
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x0400812E RID: 33070
		private float startTime;

		// Token: 0x0400812F RID: 33071
		private float currentTime;

		// Token: 0x04008130 RID: 33072
		private float[] endTimes;

		// Token: 0x04008131 RID: 33073
		private float lastTime;

		// Token: 0x04008132 RID: 33074
		private float deltaTime;

		// Token: 0x04008133 RID: 33075
		private float delayTime;

		// Token: 0x04008134 RID: 33076
		private float[] keyOffsets;

		// Token: 0x04008135 RID: 33077
		protected AnimationCurve[] curves;

		// Token: 0x04008136 RID: 33078
		protected AnimateFsmAction.Calculation[] calculations;

		// Token: 0x04008137 RID: 33079
		protected float[] resultFloats;

		// Token: 0x04008138 RID: 33080
		protected float[] fromFloats;

		// Token: 0x04008139 RID: 33081
		protected float[] toFloats;

		// Token: 0x0400813A RID: 33082
		protected bool finishAction;

		// Token: 0x0400813B RID: 33083
		protected bool isRunning;

		// Token: 0x0400813C RID: 33084
		protected bool looping;

		// Token: 0x0400813D RID: 33085
		private bool start;

		// Token: 0x0400813E RID: 33086
		private float largestEndTime;

		// Token: 0x0200278A RID: 10122
		public enum Calculation
		{
			// Token: 0x0400F468 RID: 62568
			None,
			// Token: 0x0400F469 RID: 62569
			SetValue,
			// Token: 0x0400F46A RID: 62570
			AddToValue,
			// Token: 0x0400F46B RID: 62571
			SubtractFromValue,
			// Token: 0x0400F46C RID: 62572
			SubtractValueFromCurve,
			// Token: 0x0400F46D RID: 62573
			MultiplyValue,
			// Token: 0x0400F46E RID: 62574
			DivideValue,
			// Token: 0x0400F46F RID: 62575
			DivideCurveByValue
		}
	}
}
