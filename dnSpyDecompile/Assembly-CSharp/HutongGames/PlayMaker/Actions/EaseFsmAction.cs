using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB0 RID: 2992
	[Tooltip("Ease base action - don't use!")]
	public abstract class EaseFsmAction : FsmStateAction
	{
		// Token: 0x06009C05 RID: 39941 RVA: 0x00323854 File Offset: 0x00321A54
		public override void Reset()
		{
			this.easeType = EaseFsmAction.EaseType.linear;
			this.time = new FsmFloat
			{
				Value = 1f
			};
			this.delay = new FsmFloat
			{
				UseVariable = true
			};
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
			this.reverse = new FsmBool
			{
				Value = false
			};
			this.realTime = false;
			this.finishEvent = null;
			this.ease = null;
			this.runningTime = 0f;
			this.lastTime = 0f;
			this.percentage = 0f;
			this.fromFloats = new float[0];
			this.toFloats = new float[0];
			this.resultFloats = new float[0];
			this.finishAction = false;
			this.start = false;
			this.finished = false;
			this.isRunning = false;
		}

		// Token: 0x06009C06 RID: 39942 RVA: 0x0032392C File Offset: 0x00321B2C
		public override void OnEnter()
		{
			this.finished = false;
			this.isRunning = false;
			this.SetEasingFunction();
			this.runningTime = 0f;
			this.percentage = (this.reverse.IsNone ? 0f : (this.reverse.Value ? 1f : 0f));
			this.finishAction = false;
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
			this.delayTime = (this.delay.IsNone ? 0f : (this.delayTime = this.delay.Value));
			this.start = true;
		}

		// Token: 0x06009C07 RID: 39943 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009C08 RID: 39944 RVA: 0x003239E4 File Offset: 0x00321BE4
		public override void OnUpdate()
		{
			if (this.start && !this.isRunning)
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
			if (this.isRunning && !this.finished)
			{
				if (this.reverse.IsNone || !this.reverse.Value)
				{
					this.UpdatePercentage();
					if (this.percentage < 1f)
					{
						for (int i = 0; i < this.fromFloats.Length; i++)
						{
							this.resultFloats[i] = this.ease(this.fromFloats[i], this.toFloats[i], this.percentage);
						}
						return;
					}
					this.finishAction = true;
					this.finished = true;
					this.isRunning = false;
					return;
				}
				else
				{
					this.UpdatePercentage();
					if (this.percentage > 0f)
					{
						for (int j = 0; j < this.fromFloats.Length; j++)
						{
							this.resultFloats[j] = this.ease(this.fromFloats[j], this.toFloats[j], this.percentage);
						}
						return;
					}
					this.finishAction = true;
					this.finished = true;
					this.isRunning = false;
				}
			}
		}

		// Token: 0x06009C09 RID: 39945 RVA: 0x00323B9C File Offset: 0x00321D9C
		protected void UpdatePercentage()
		{
			if (this.realTime)
			{
				this.deltaTime = FsmTime.RealtimeSinceStartup - this.startTime - this.lastTime;
				this.lastTime = FsmTime.RealtimeSinceStartup - this.startTime;
				if (!this.speed.IsNone)
				{
					this.runningTime += this.deltaTime * this.speed.Value;
				}
				else
				{
					this.runningTime += this.deltaTime;
				}
			}
			else if (!this.speed.IsNone)
			{
				this.runningTime += Time.deltaTime * this.speed.Value;
			}
			else
			{
				this.runningTime += Time.deltaTime;
			}
			if (!this.reverse.IsNone && this.reverse.Value)
			{
				this.percentage = 1f - this.runningTime / this.time.Value;
				return;
			}
			this.percentage = this.runningTime / this.time.Value;
		}

		// Token: 0x06009C0A RID: 39946 RVA: 0x00323CB4 File Offset: 0x00321EB4
		protected void SetEasingFunction()
		{
			switch (this.easeType)
			{
			case EaseFsmAction.EaseType.easeInQuad:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInQuad);
				return;
			case EaseFsmAction.EaseType.easeOutQuad:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutQuad);
				return;
			case EaseFsmAction.EaseType.easeInOutQuad:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutQuad);
				return;
			case EaseFsmAction.EaseType.easeInCubic:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInCubic);
				return;
			case EaseFsmAction.EaseType.easeOutCubic:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutCubic);
				return;
			case EaseFsmAction.EaseType.easeInOutCubic:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutCubic);
				return;
			case EaseFsmAction.EaseType.easeInQuart:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInQuart);
				return;
			case EaseFsmAction.EaseType.easeOutQuart:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutQuart);
				return;
			case EaseFsmAction.EaseType.easeInOutQuart:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutQuart);
				return;
			case EaseFsmAction.EaseType.easeInQuint:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInQuint);
				return;
			case EaseFsmAction.EaseType.easeOutQuint:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutQuint);
				return;
			case EaseFsmAction.EaseType.easeInOutQuint:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutQuint);
				return;
			case EaseFsmAction.EaseType.easeInSine:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInSine);
				return;
			case EaseFsmAction.EaseType.easeOutSine:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutSine);
				return;
			case EaseFsmAction.EaseType.easeInOutSine:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutSine);
				return;
			case EaseFsmAction.EaseType.easeInExpo:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInExpo);
				return;
			case EaseFsmAction.EaseType.easeOutExpo:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutExpo);
				return;
			case EaseFsmAction.EaseType.easeInOutExpo:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutExpo);
				return;
			case EaseFsmAction.EaseType.easeInCirc:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInCirc);
				return;
			case EaseFsmAction.EaseType.easeOutCirc:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutCirc);
				return;
			case EaseFsmAction.EaseType.easeInOutCirc:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutCirc);
				return;
			case EaseFsmAction.EaseType.linear:
				this.ease = new EaseFsmAction.EasingFunction(this.linear);
				return;
			case EaseFsmAction.EaseType.spring:
				this.ease = new EaseFsmAction.EasingFunction(this.spring);
				return;
			case EaseFsmAction.EaseType.bounce:
				this.ease = new EaseFsmAction.EasingFunction(this.bounce);
				return;
			case EaseFsmAction.EaseType.easeInBack:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInBack);
				return;
			case EaseFsmAction.EaseType.easeOutBack:
				this.ease = new EaseFsmAction.EasingFunction(this.easeOutBack);
				return;
			case EaseFsmAction.EaseType.easeInOutBack:
				this.ease = new EaseFsmAction.EasingFunction(this.easeInOutBack);
				return;
			case EaseFsmAction.EaseType.elastic:
				this.ease = new EaseFsmAction.EasingFunction(this.elastic);
				return;
			case EaseFsmAction.EaseType.punch:
				this.ease = new EaseFsmAction.EasingFunction(this.elastic);
				return;
			default:
				return;
			}
		}

		// Token: 0x06009C0B RID: 39947 RVA: 0x002DD135 File Offset: 0x002DB335
		protected float linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x06009C0C RID: 39948 RVA: 0x00323F6C File Offset: 0x0032216C
		protected float clerp(float start, float end, float value)
		{
			float num = 0f;
			float num2 = 360f;
			float num3 = Mathf.Abs((num2 - num) / 2f);
			float result;
			if (end - start < -num3)
			{
				float num4 = (num2 - start + end) * value;
				result = start + num4;
			}
			else if (end - start > num3)
			{
				float num4 = -(num2 - end + start) * value;
				result = start + num4;
			}
			else
			{
				result = start + (end - start) * value;
			}
			return result;
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x00323FD8 File Offset: 0x003221D8
		protected float spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x06009C0E RID: 39950 RVA: 0x002DD210 File Offset: 0x002DB410
		protected float easeInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x06009C0F RID: 39951 RVA: 0x002DD21E File Offset: 0x002DB41E
		protected float easeOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x0032403C File Offset: 0x0032223C
		protected float easeInOutQuad(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value + start;
			}
			value -= 1f;
			return -end / 2f * (value * (value - 2f) - 1f) + start;
		}

		// Token: 0x06009C11 RID: 39953 RVA: 0x002DD288 File Offset: 0x002DB488
		protected float easeInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x06009C12 RID: 39954 RVA: 0x002DD298 File Offset: 0x002DB498
		protected float easeOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x06009C13 RID: 39955 RVA: 0x00324090 File Offset: 0x00322290
		protected float easeInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value + start;
			}
			value -= 2f;
			return end / 2f * (value * value * value + 2f) + start;
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x002DD309 File Offset: 0x002DB509
		protected float easeInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x002DD31B File Offset: 0x002DB51B
		protected float easeOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x06009C16 RID: 39958 RVA: 0x003240E4 File Offset: 0x003222E4
		protected float easeInOutQuart(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value * value + start;
			}
			value -= 2f;
			return -end / 2f * (value * value * value * value - 2f) + start;
		}

		// Token: 0x06009C17 RID: 39959 RVA: 0x002DD396 File Offset: 0x002DB596
		protected float easeInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x06009C18 RID: 39960 RVA: 0x002DD3AA File Offset: 0x002DB5AA
		protected float easeOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x06009C19 RID: 39961 RVA: 0x0032413C File Offset: 0x0032233C
		protected float easeInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end / 2f * (value * value * value * value * value + 2f) + start;
		}

		// Token: 0x06009C1A RID: 39962 RVA: 0x002DD429 File Offset: 0x002DB629
		protected float easeInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value / 1f * 1.5707964f) + end + start;
		}

		// Token: 0x06009C1B RID: 39963 RVA: 0x002DD449 File Offset: 0x002DB649
		protected float easeOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value / 1f * 1.5707964f) + start;
		}

		// Token: 0x06009C1C RID: 39964 RVA: 0x002DD466 File Offset: 0x002DB666
		protected float easeInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end / 2f * (Mathf.Cos(3.1415927f * value / 1f) - 1f) + start;
		}

		// Token: 0x06009C1D RID: 39965 RVA: 0x002DD490 File Offset: 0x002DB690
		protected float easeInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
		}

		// Token: 0x06009C1E RID: 39966 RVA: 0x002DD4B8 File Offset: 0x002DB6B8
		protected float easeOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value / 1f) + 1f) + start;
		}

		// Token: 0x06009C1F RID: 39967 RVA: 0x00324198 File Offset: 0x00322398
		protected float easeInOutExpo(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end / 2f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
			}
			value -= 1f;
			return end / 2f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
		}

		// Token: 0x06009C20 RID: 39968 RVA: 0x002DD554 File Offset: 0x002DB754
		protected float easeInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x06009C21 RID: 39969 RVA: 0x002DD574 File Offset: 0x002DB774
		protected float easeOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x06009C22 RID: 39970 RVA: 0x00324208 File Offset: 0x00322408
		protected float easeInOutCirc(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return -end / 2f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
			}
			value -= 2f;
			return end / 2f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
		}

		// Token: 0x06009C23 RID: 39971 RVA: 0x00324274 File Offset: 0x00322474
		protected float bounce(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return end * (7.5625f * value * value) + start;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return end * (7.5625f * value * value + 0.75f) + start;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return end * (7.5625f * value * value + 0.9375f) + start;
			}
			value -= 0.95454544f;
			return end * (7.5625f * value * value + 0.984375f) + start;
		}

		// Token: 0x06009C24 RID: 39972 RVA: 0x00324310 File Offset: 0x00322510
		protected float easeInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x00324344 File Offset: 0x00322544
		protected float easeOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value = value / 1f - 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x00324384 File Offset: 0x00322584
		protected float easeInOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return end / 2f * (value * value * ((num + 1f) * value - num)) + start;
			}
			value -= 2f;
			num *= 1.525f;
			return end / 2f * (value * value * ((num + 1f) * value + num) + 2f) + start;
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x00324400 File Offset: 0x00322600
		protected float punch(float amplitude, float value)
		{
			if (value == 0f)
			{
				return 0f;
			}
			if (value == 1f)
			{
				return 0f;
			}
			float num = 0.3f;
			float num2 = num / 6.2831855f * Mathf.Asin(0f);
			return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.2831855f / num);
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00324474 File Offset: 0x00322674
		protected float elastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
		}

		// Token: 0x0400819C RID: 33180
		[RequiredField]
		public FsmFloat time;

		// Token: 0x0400819D RID: 33181
		public FsmFloat speed;

		// Token: 0x0400819E RID: 33182
		public FsmFloat delay;

		// Token: 0x0400819F RID: 33183
		public EaseFsmAction.EaseType easeType = EaseFsmAction.EaseType.linear;

		// Token: 0x040081A0 RID: 33184
		public FsmBool reverse;

		// Token: 0x040081A1 RID: 33185
		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x040081A2 RID: 33186
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x040081A3 RID: 33187
		protected EaseFsmAction.EasingFunction ease;

		// Token: 0x040081A4 RID: 33188
		protected float runningTime;

		// Token: 0x040081A5 RID: 33189
		protected float lastTime;

		// Token: 0x040081A6 RID: 33190
		protected float startTime;

		// Token: 0x040081A7 RID: 33191
		protected float deltaTime;

		// Token: 0x040081A8 RID: 33192
		protected float delayTime;

		// Token: 0x040081A9 RID: 33193
		protected float percentage;

		// Token: 0x040081AA RID: 33194
		protected float[] fromFloats = new float[0];

		// Token: 0x040081AB RID: 33195
		protected float[] toFloats = new float[0];

		// Token: 0x040081AC RID: 33196
		protected float[] resultFloats = new float[0];

		// Token: 0x040081AD RID: 33197
		protected bool finishAction;

		// Token: 0x040081AE RID: 33198
		protected bool start;

		// Token: 0x040081AF RID: 33199
		protected bool finished;

		// Token: 0x040081B0 RID: 33200
		protected bool isRunning;

		// Token: 0x0200278C RID: 10124
		// (Invoke) Token: 0x06013A53 RID: 80467
		protected delegate float EasingFunction(float start, float end, float value);

		// Token: 0x0200278D RID: 10125
		public enum EaseType
		{
			// Token: 0x0400F479 RID: 62585
			easeInQuad,
			// Token: 0x0400F47A RID: 62586
			easeOutQuad,
			// Token: 0x0400F47B RID: 62587
			easeInOutQuad,
			// Token: 0x0400F47C RID: 62588
			easeInCubic,
			// Token: 0x0400F47D RID: 62589
			easeOutCubic,
			// Token: 0x0400F47E RID: 62590
			easeInOutCubic,
			// Token: 0x0400F47F RID: 62591
			easeInQuart,
			// Token: 0x0400F480 RID: 62592
			easeOutQuart,
			// Token: 0x0400F481 RID: 62593
			easeInOutQuart,
			// Token: 0x0400F482 RID: 62594
			easeInQuint,
			// Token: 0x0400F483 RID: 62595
			easeOutQuint,
			// Token: 0x0400F484 RID: 62596
			easeInOutQuint,
			// Token: 0x0400F485 RID: 62597
			easeInSine,
			// Token: 0x0400F486 RID: 62598
			easeOutSine,
			// Token: 0x0400F487 RID: 62599
			easeInOutSine,
			// Token: 0x0400F488 RID: 62600
			easeInExpo,
			// Token: 0x0400F489 RID: 62601
			easeOutExpo,
			// Token: 0x0400F48A RID: 62602
			easeInOutExpo,
			// Token: 0x0400F48B RID: 62603
			easeInCirc,
			// Token: 0x0400F48C RID: 62604
			easeOutCirc,
			// Token: 0x0400F48D RID: 62605
			easeInOutCirc,
			// Token: 0x0400F48E RID: 62606
			linear,
			// Token: 0x0400F48F RID: 62607
			spring,
			// Token: 0x0400F490 RID: 62608
			bounce,
			// Token: 0x0400F491 RID: 62609
			easeInBack,
			// Token: 0x0400F492 RID: 62610
			easeOutBack,
			// Token: 0x0400F493 RID: 62611
			easeInOutBack,
			// Token: 0x0400F494 RID: 62612
			elastic,
			// Token: 0x0400F495 RID: 62613
			punch
		}
	}
}
