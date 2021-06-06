using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BAB RID: 2987
	[Tooltip("Animate base action - DON'T USE IT!")]
	public abstract class CurveFsmAction : FsmStateAction
	{
		// Token: 0x06009BEC RID: 39916 RVA: 0x003220A4 File Offset: 0x003202A4
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
			this.distances = new float[0];
			this.endTimes = new float[0];
			this.keyOffsets = new float[0];
			this.curves = new AnimationCurve[0];
			this.finishAction = false;
			this.start = false;
		}

		// Token: 0x06009BED RID: 39917 RVA: 0x0032216C File Offset: 0x0032036C
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

		// Token: 0x06009BEE RID: 39918 RVA: 0x003221F8 File Offset: 0x003203F8
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
			this.distances = new float[this.fromFloats.Length];
			for (int k = 0; k < this.fromFloats.Length; k++)
			{
				this.distances[k] = this.toFloats[k] - this.fromFloats[k];
			}
		}

		// Token: 0x06009BEF RID: 39919 RVA: 0x003224A0 File Offset: 0x003206A0
		public override void OnUpdate()
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
					}
					else
					{
						this.delayTime -= Time.deltaTime;
					}
				}
				else
				{
					this.isRunning = true;
					this.start = false;
					this.startTime = FsmTime.RealtimeSinceStartup;
					this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
				}
			}
			if (this.isRunning && !this.finishAction)
			{
				if (this.realTime)
				{
					this.deltaTime = FsmTime.RealtimeSinceStartup - this.startTime - this.lastTime;
					this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
					if (!this.speed.IsNone)
					{
						this.currentTime += this.deltaTime * this.speed.Value;
					}
					else
					{
						this.currentTime += this.deltaTime;
					}
				}
				else if (!this.speed.IsNone)
				{
					this.currentTime += Time.deltaTime * this.speed.Value;
				}
				else
				{
					this.currentTime += Time.deltaTime;
				}
				for (int i = 0; i < this.curves.Length; i++)
				{
					if (this.curves[i] != null && this.curves[i].keys.Length != 0)
					{
						if (this.calculations[i] != CurveFsmAction.Calculation.None)
						{
							switch (this.calculations[i])
							{
							case CurveFsmAction.Calculation.AddToValue:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = this.fromFloats[i] + (this.distances[i] * (this.currentTime / this.time.Value) + this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time));
								}
								else
								{
									this.resultFloats[i] = this.fromFloats[i] + (this.distances[i] * (this.currentTime / this.endTimes[i]) + this.curves[i].Evaluate(this.currentTime));
								}
								break;
							case CurveFsmAction.Calculation.SubtractFromValue:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = this.fromFloats[i] + (this.distances[i] * (this.currentTime / this.time.Value) - this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time));
								}
								else
								{
									this.resultFloats[i] = this.fromFloats[i] + (this.distances[i] * (this.currentTime / this.endTimes[i]) - this.curves[i].Evaluate(this.currentTime));
								}
								break;
							case CurveFsmAction.Calculation.SubtractValueFromCurve:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) - this.distances[i] * (this.currentTime / this.time.Value) + this.fromFloats[i];
								}
								else
								{
									this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime) - this.distances[i] * (this.currentTime / this.endTimes[i]) + this.fromFloats[i];
								}
								break;
							case CurveFsmAction.Calculation.MultiplyValue:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) * this.distances[i] * (this.currentTime / this.time.Value) + this.fromFloats[i];
								}
								else
								{
									this.resultFloats[i] = this.curves[i].Evaluate(this.currentTime) * this.distances[i] * (this.currentTime / this.endTimes[i]) + this.fromFloats[i];
								}
								break;
							case CurveFsmAction.Calculation.DivideValue:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = ((this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) != 0f) ? (this.fromFloats[i] + this.distances[i] * (this.currentTime / this.time.Value) / this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time)) : float.MaxValue);
								}
								else
								{
									this.resultFloats[i] = ((this.curves[i].Evaluate(this.currentTime) != 0f) ? (this.fromFloats[i] + this.distances[i] * (this.currentTime / this.endTimes[i]) / this.curves[i].Evaluate(this.currentTime)) : float.MaxValue);
								}
								break;
							case CurveFsmAction.Calculation.DivideCurveByValue:
								if (!this.time.IsNone)
								{
									this.resultFloats[i] = ((this.fromFloats[i] != 0f) ? (this.curves[i].Evaluate(this.currentTime / this.time.Value * this.curves[i].keys[this.curves[i].length - 1].time) / (this.distances[i] * (this.currentTime / this.time.Value)) + this.fromFloats[i]) : float.MaxValue);
								}
								else
								{
									this.resultFloats[i] = ((this.fromFloats[i] != 0f) ? (this.curves[i].Evaluate(this.currentTime) / (this.distances[i] * (this.currentTime / this.endTimes[i])) + this.fromFloats[i]) : float.MaxValue);
								}
								break;
							}
						}
						else if (!this.time.IsNone)
						{
							this.resultFloats[i] = this.fromFloats[i] + this.distances[i] * (this.currentTime / this.time.Value);
						}
						else
						{
							this.resultFloats[i] = this.fromFloats[i] + this.distances[i] * (this.currentTime / this.endTimes[i]);
						}
					}
					else if (!this.time.IsNone)
					{
						this.resultFloats[i] = this.fromFloats[i] + this.distances[i] * (this.currentTime / this.time.Value);
					}
					else if (this.largestEndTime == 0f)
					{
						this.resultFloats[i] = this.fromFloats[i] + this.distances[i] * (this.currentTime / 1f);
					}
					else
					{
						this.resultFloats[i] = this.fromFloats[i] + this.distances[i] * (this.currentTime / this.largestEndTime);
					}
				}
				if (this.isRunning)
				{
					this.finishAction = true;
					for (int j = 0; j < this.endTimes.Length; j++)
					{
						if (this.currentTime < this.endTimes[j])
						{
							this.finishAction = false;
						}
					}
					this.isRunning = !this.finishAction;
				}
			}
		}

		// Token: 0x04008164 RID: 33124
		[Tooltip("Define animation time, scaling the curve to fit.")]
		public FsmFloat time;

		// Token: 0x04008165 RID: 33125
		[Tooltip("If you define speed, your animation will speed up or slow down.")]
		public FsmFloat speed;

		// Token: 0x04008166 RID: 33126
		[Tooltip("Delayed animation start.")]
		public FsmFloat delay;

		// Token: 0x04008167 RID: 33127
		[Tooltip("Animation curve start from any time. If IgnoreCurveOffset is true the animation starts right after the state become entered.")]
		public FsmBool ignoreCurveOffset;

		// Token: 0x04008168 RID: 33128
		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x04008169 RID: 33129
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x0400816A RID: 33130
		private float startTime;

		// Token: 0x0400816B RID: 33131
		private float currentTime;

		// Token: 0x0400816C RID: 33132
		private float[] endTimes;

		// Token: 0x0400816D RID: 33133
		private float lastTime;

		// Token: 0x0400816E RID: 33134
		private float deltaTime;

		// Token: 0x0400816F RID: 33135
		private float delayTime;

		// Token: 0x04008170 RID: 33136
		private float[] keyOffsets;

		// Token: 0x04008171 RID: 33137
		protected AnimationCurve[] curves;

		// Token: 0x04008172 RID: 33138
		protected CurveFsmAction.Calculation[] calculations;

		// Token: 0x04008173 RID: 33139
		protected float[] resultFloats;

		// Token: 0x04008174 RID: 33140
		protected float[] fromFloats;

		// Token: 0x04008175 RID: 33141
		protected float[] toFloats;

		// Token: 0x04008176 RID: 33142
		private float[] distances;

		// Token: 0x04008177 RID: 33143
		protected bool finishAction;

		// Token: 0x04008178 RID: 33144
		protected bool isRunning;

		// Token: 0x04008179 RID: 33145
		protected bool looping;

		// Token: 0x0400817A RID: 33146
		private bool start;

		// Token: 0x0400817B RID: 33147
		private float largestEndTime;

		// Token: 0x0200278B RID: 10123
		public enum Calculation
		{
			// Token: 0x0400F471 RID: 62577
			None,
			// Token: 0x0400F472 RID: 62578
			AddToValue,
			// Token: 0x0400F473 RID: 62579
			SubtractFromValue,
			// Token: 0x0400F474 RID: 62580
			SubtractValueFromCurve,
			// Token: 0x0400F475 RID: 62581
			MultiplyValue,
			// Token: 0x0400F476 RID: 62582
			DivideValue,
			// Token: 0x0400F477 RID: 62583
			DivideCurveByValue
		}
	}
}
