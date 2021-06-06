using System;
using UnityEngine;

namespace HutongGames
{
	// Token: 0x02000B8B RID: 2955
	public class EasingFunction
	{
		// Token: 0x06009AE9 RID: 39657 RVA: 0x0031D395 File Offset: 0x0031B595
		public static float Linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x06009AEA RID: 39658 RVA: 0x0031D3A0 File Offset: 0x0031B5A0
		public static float Spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x06009AEB RID: 39659 RVA: 0x0031D404 File Offset: 0x0031B604
		public static float EaseInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x06009AEC RID: 39660 RVA: 0x0031D412 File Offset: 0x0031B612
		public static float EaseOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x06009AED RID: 39661 RVA: 0x0031D428 File Offset: 0x0031B628
		public static float EaseInOutQuad(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value + start;
			}
			value -= 1f;
			return -end * 0.5f * (value * (value - 2f) - 1f) + start;
		}

		// Token: 0x06009AEE RID: 39662 RVA: 0x0031D47C File Offset: 0x0031B67C
		public static float EaseInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x06009AEF RID: 39663 RVA: 0x0031D48C File Offset: 0x0031B68C
		public static float EaseOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x06009AF0 RID: 39664 RVA: 0x0031D4AC File Offset: 0x0031B6AC
		public static float EaseInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value + 2f) + start;
		}

		// Token: 0x06009AF1 RID: 39665 RVA: 0x0031D4FD File Offset: 0x0031B6FD
		public static float EaseInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x06009AF2 RID: 39666 RVA: 0x0031D50F File Offset: 0x0031B70F
		public static float EaseOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x06009AF3 RID: 39667 RVA: 0x0031D534 File Offset: 0x0031B734
		public static float EaseInOutQuart(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value + start;
			}
			value -= 2f;
			return -end * 0.5f * (value * value * value * value - 2f) + start;
		}

		// Token: 0x06009AF4 RID: 39668 RVA: 0x0031D58A File Offset: 0x0031B78A
		public static float EaseInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x06009AF5 RID: 39669 RVA: 0x0031D59E File Offset: 0x0031B79E
		public static float EaseOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x06009AF6 RID: 39670 RVA: 0x0031D5C4 File Offset: 0x0031B7C4
		public static float EaseInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value * value * value + 2f) + start;
		}

		// Token: 0x06009AF7 RID: 39671 RVA: 0x0031D61D File Offset: 0x0031B81D
		public static float EaseInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value * 1.5707964f) + end + start;
		}

		// Token: 0x06009AF8 RID: 39672 RVA: 0x0031D637 File Offset: 0x0031B837
		public static float EaseOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value * 1.5707964f) + start;
		}

		// Token: 0x06009AF9 RID: 39673 RVA: 0x0031D64E File Offset: 0x0031B84E
		public static float EaseInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end * 0.5f * (Mathf.Cos(3.1415927f * value) - 1f) + start;
		}

		// Token: 0x06009AFA RID: 39674 RVA: 0x0031D672 File Offset: 0x0031B872
		public static float EaseInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x0031D694 File Offset: 0x0031B894
		public static float EaseOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x0031D6B8 File Offset: 0x0031B8B8
		public static float EaseInOutExpo(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
			}
			value -= 1f;
			return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
		}

		// Token: 0x06009AFD RID: 39677 RVA: 0x0031D728 File Offset: 0x0031B928
		public static float EaseInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x0031D748 File Offset: 0x0031B948
		public static float EaseOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x0031D76C File Offset: 0x0031B96C
		public static float EaseInOutCirc(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
			}
			value -= 2f;
			return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x0031D7D8 File Offset: 0x0031B9D8
		public static float EaseInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - EasingFunction.EaseOutBounce(0f, end, num - value) + start;
		}

		// Token: 0x06009B01 RID: 39681 RVA: 0x0031D804 File Offset: 0x0031BA04
		public static float EaseOutBounce(float start, float end, float value)
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

		// Token: 0x06009B02 RID: 39682 RVA: 0x0031D8A0 File Offset: 0x0031BAA0
		public static float EaseInOutBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num * 0.5f)
			{
				return EasingFunction.EaseInBounce(0f, end, value * 2f) * 0.5f + start;
			}
			return EasingFunction.EaseOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
		}

		// Token: 0x06009B03 RID: 39683 RVA: 0x0031D904 File Offset: 0x0031BB04
		public static float EaseInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x06009B04 RID: 39684 RVA: 0x0031D938 File Offset: 0x0031BB38
		public static float EaseOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x06009B05 RID: 39685 RVA: 0x0031D974 File Offset: 0x0031BB74
		public static float EaseInOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
			}
			value -= 2f;
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
		}

		// Token: 0x06009B06 RID: 39686 RVA: 0x0031D9F0 File Offset: 0x0031BBF0
		public static float EaseInElastic(float start, float end, float value)
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
			return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
		}

		// Token: 0x06009B07 RID: 39687 RVA: 0x0031DA94 File Offset: 0x0031BC94
		public static float EaseOutElastic(float start, float end, float value)
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
				num4 = num2 * 0.25f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
		}

		// Token: 0x06009B08 RID: 39688 RVA: 0x0031DB30 File Offset: 0x0031BD30
		public static float EaseInOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num * 0.5f) == 2f)
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
			if (value < 1f)
			{
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
			}
			return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
		}

		// Token: 0x06009B09 RID: 39689 RVA: 0x002BB989 File Offset: 0x002B9B89
		public static float LinearD(float start, float end, float value)
		{
			return end - start;
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x0031DC1E File Offset: 0x0031BE1E
		public static float EaseInQuadD(float start, float end, float value)
		{
			return 2f * (end - start) * value;
		}

		// Token: 0x06009B0B RID: 39691 RVA: 0x0031DC2B File Offset: 0x0031BE2B
		public static float EaseOutQuadD(float start, float end, float value)
		{
			end -= start;
			return -end * value - end * (value - 2f);
		}

		// Token: 0x06009B0C RID: 39692 RVA: 0x0031DC40 File Offset: 0x0031BE40
		public static float EaseInOutQuadD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * value;
			}
			value -= 1f;
			return end * (1f - value);
		}

		// Token: 0x06009B0D RID: 39693 RVA: 0x0031DC6E File Offset: 0x0031BE6E
		public static float EaseInCubicD(float start, float end, float value)
		{
			return 3f * (end - start) * value * value;
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x0031DC7D File Offset: 0x0031BE7D
		public static float EaseOutCubicD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return 3f * end * value * value;
		}

		// Token: 0x06009B0F RID: 39695 RVA: 0x0031DC98 File Offset: 0x0031BE98
		public static float EaseInOutCubicD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 1.5f * end * value * value;
			}
			value -= 2f;
			return 1.5f * end * value * value;
		}

		// Token: 0x06009B10 RID: 39696 RVA: 0x0031DCD0 File Offset: 0x0031BED0
		public static float EaseInQuartD(float start, float end, float value)
		{
			return 4f * (end - start) * value * value * value;
		}

		// Token: 0x06009B11 RID: 39697 RVA: 0x0031DCE1 File Offset: 0x0031BEE1
		public static float EaseOutQuartD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -4f * end * value * value * value;
		}

		// Token: 0x06009B12 RID: 39698 RVA: 0x0031DCFE File Offset: 0x0031BEFE
		public static float EaseInOutQuartD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 2f * end * value * value * value;
			}
			value -= 2f;
			return -2f * end * value * value * value;
		}

		// Token: 0x06009B13 RID: 39699 RVA: 0x0031DD3A File Offset: 0x0031BF3A
		public static float EaseInQuintD(float start, float end, float value)
		{
			return 5f * (end - start) * value * value * value * value;
		}

		// Token: 0x06009B14 RID: 39700 RVA: 0x0031DD4D File Offset: 0x0031BF4D
		public static float EaseOutQuintD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return 5f * end * value * value * value * value;
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x0031DD6C File Offset: 0x0031BF6C
		public static float EaseInOutQuintD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 2.5f * end * value * value * value * value;
			}
			value -= 2f;
			return 2.5f * end * value * value * value * value;
		}

		// Token: 0x06009B16 RID: 39702 RVA: 0x0031DDAC File Offset: 0x0031BFAC
		public static float EaseInSineD(float start, float end, float value)
		{
			return (end - start) * 0.5f * 3.1415927f * Mathf.Sin(1.5707964f * value);
		}

		// Token: 0x06009B17 RID: 39703 RVA: 0x0031DDCA File Offset: 0x0031BFCA
		public static float EaseOutSineD(float start, float end, float value)
		{
			end -= start;
			return 1.5707964f * end * Mathf.Cos(value * 1.5707964f);
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x0031DDE5 File Offset: 0x0031BFE5
		public static float EaseInOutSineD(float start, float end, float value)
		{
			end -= start;
			return end * 0.5f * 3.1415927f * Mathf.Cos(3.1415927f * value);
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x0031DE06 File Offset: 0x0031C006
		public static float EaseInExpoD(float start, float end, float value)
		{
			return 6.931472f * (end - start) * Mathf.Pow(2f, 10f * (value - 1f));
		}

		// Token: 0x06009B1A RID: 39706 RVA: 0x0031DE29 File Offset: 0x0031C029
		public static float EaseOutExpoD(float start, float end, float value)
		{
			end -= start;
			return 3.465736f * end * Mathf.Pow(2f, 1f - 10f * value);
		}

		// Token: 0x06009B1B RID: 39707 RVA: 0x0031DE50 File Offset: 0x0031C050
		public static float EaseInOutExpoD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return 3.465736f * end * Mathf.Pow(2f, 10f * (value - 1f));
			}
			value -= 1f;
			return 3.465736f * end / Mathf.Pow(2f, 10f * value);
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x0031DEB5 File Offset: 0x0031C0B5
		public static float EaseInCircD(float start, float end, float value)
		{
			return (end - start) * value / Mathf.Sqrt(1f - value * value);
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x0031DECB File Offset: 0x0031C0CB
		public static float EaseOutCircD(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * value / Mathf.Sqrt(1f - value * value);
		}

		// Token: 0x06009B1E RID: 39710 RVA: 0x0031DEF0 File Offset: 0x0031C0F0
		public static float EaseInOutCircD(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * value / (2f * Mathf.Sqrt(1f - value * value));
			}
			value -= 2f;
			return -end * value / (2f * Mathf.Sqrt(1f - value * value));
		}

		// Token: 0x06009B1F RID: 39711 RVA: 0x0031DF50 File Offset: 0x0031C150
		public static float EaseInBounceD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return EasingFunction.EaseOutBounceD(0f, end, num - value);
		}

		// Token: 0x06009B20 RID: 39712 RVA: 0x0031DF78 File Offset: 0x0031C178
		public static float EaseOutBounceD(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return 2f * end * 7.5625f * value;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return 2f * end * 7.5625f * value;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return 2f * end * 7.5625f * value;
			}
			value -= 0.95454544f;
			return 2f * end * 7.5625f * value;
		}

		// Token: 0x06009B21 RID: 39713 RVA: 0x0031E00C File Offset: 0x0031C20C
		public static float EaseInOutBounceD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num * 0.5f)
			{
				return EasingFunction.EaseInBounceD(0f, end, value * 2f) * 0.5f;
			}
			return EasingFunction.EaseOutBounceD(0f, end, value * 2f - num) * 0.5f;
		}

		// Token: 0x06009B22 RID: 39714 RVA: 0x0031E064 File Offset: 0x0031C264
		public static float EaseInBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			return 3f * (num + 1f) * (end - start) * value * value - 2f * num * (end - start) * value;
		}

		// Token: 0x06009B23 RID: 39715 RVA: 0x0031E09C File Offset: 0x0031C29C
		public static float EaseOutBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * ((num + 1f) * value * value + 2f * value * ((num + 1f) * value + num));
		}

		// Token: 0x06009B24 RID: 39716 RVA: 0x0031E0E0 File Offset: 0x0031C2E0
		public static float EaseInOutBackD(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return 0.5f * end * (num + 1f) * value * value + end * value * ((num + 1f) * value - num);
			}
			value -= 2f;
			num *= 1.525f;
			return 0.5f * end * ((num + 1f) * value * value + 2f * value * ((num + 1f) * value + num));
		}

		// Token: 0x06009B25 RID: 39717 RVA: 0x0031E170 File Offset: 0x0031C370
		public static float EaseInElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
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
			float num5 = 6.2831855f;
			return -num3 * num * num5 * Mathf.Cos(num5 * (num * (value - 1f) - num4) / num2) / num2 - 3.465736f * num3 * Mathf.Sin(num5 * (num * (value - 1f) - num4) / num2) * Mathf.Pow(2f, 10f * (value - 1f) + 1f);
		}

		// Token: 0x06009B26 RID: 39718 RVA: 0x0031E228 File Offset: 0x0031C428
		public static float EaseOutElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 * 0.25f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * 3.1415927f * num * Mathf.Pow(2f, 1f - 10f * value) * Mathf.Cos(6.2831855f * (num * value - num4) / num2) / num2 - 3.465736f * num3 * Mathf.Pow(2f, 1f - 10f * value) * Mathf.Sin(6.2831855f * (num * value - num4) / num2);
		}

		// Token: 0x06009B27 RID: 39719 RVA: 0x0031E2E8 File Offset: 0x0031C4E8
		public static float EaseInOutElasticD(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
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
			if (value < 1f)
			{
				value -= 1f;
				return -3.465736f * num3 * Mathf.Pow(2f, 10f * value) * Mathf.Sin(6.2831855f * (num * value - 2f) / num2) - num3 * 3.1415927f * num * Mathf.Pow(2f, 10f * value) * Mathf.Cos(6.2831855f * (num * value - num4) / num2) / num2;
			}
			value -= 1f;
			return num3 * 3.1415927f * num * Mathf.Cos(6.2831855f * (num * value - num4) / num2) / (num2 * Mathf.Pow(2f, 10f * value)) - 3.465736f * num3 * Mathf.Sin(6.2831855f * (num * value - num4) / num2) / Mathf.Pow(2f, 10f * value);
		}

		// Token: 0x06009B28 RID: 39720 RVA: 0x0031E418 File Offset: 0x0031C618
		public static float SpringD(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			end -= start;
			return end * (6f * (1f - value) / 5f + 1f) * (-2.2f * Mathf.Pow(1f - value, 1.2f) * Mathf.Sin(3.1415927f * value * (2.5f * value * value * value + 0.2f)) + Mathf.Pow(1f - value, 2.2f) * (3.1415927f * (2.5f * value * value * value + 0.2f) + 23.561945f * value * value * value) * Mathf.Cos(3.1415927f * value * (2.5f * value * value * value + 0.2f)) + 1f) - 6f * end * (Mathf.Pow(1f - value, 2.2f) * Mathf.Sin(3.1415927f * value * (2.5f * value * value * value + 0.2f)) + value / 5f);
		}

		// Token: 0x06009B29 RID: 39721 RVA: 0x0031E520 File Offset: 0x0031C720
		public static float CustomCurve(float start, float end, float value)
		{
			if (EasingFunction.AnimationCurve == null)
			{
				return Mathf.Lerp(start, end, value);
			}
			return Mathf.Lerp(start, end, EasingFunction.AnimationCurve.Evaluate(value));
		}

		// Token: 0x06009B2A RID: 39722 RVA: 0x0031E544 File Offset: 0x0031C744
		public static EasingFunction.Function GetEasingFunction(EasingFunction.Ease easingFunction)
		{
			if (easingFunction == EasingFunction.Ease.CustomCurve)
			{
				return new EasingFunction.Function(EasingFunction.CustomCurve);
			}
			if (easingFunction == EasingFunction.Ease.EaseInQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuad);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuad);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuad);
			}
			if (easingFunction == EasingFunction.Ease.EaseInCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInCubic);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutCubic);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutCubic);
			}
			if (easingFunction == EasingFunction.Ease.EaseInQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuart);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuart);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuart);
			}
			if (easingFunction == EasingFunction.Ease.EaseInQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuint);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuint);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuint);
			}
			if (easingFunction == EasingFunction.Ease.EaseInSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseInSine);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutSine);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutSine);
			}
			if (easingFunction == EasingFunction.Ease.EaseInExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseInExpo);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutExpo);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutExpo);
			}
			if (easingFunction == EasingFunction.Ease.EaseInCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseInCirc);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutCirc);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutCirc);
			}
			if (easingFunction == EasingFunction.Ease.Linear)
			{
				return new EasingFunction.Function(EasingFunction.Linear);
			}
			if (easingFunction == EasingFunction.Ease.Spring)
			{
				return new EasingFunction.Function(EasingFunction.Spring);
			}
			if (easingFunction == EasingFunction.Ease.EaseInBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseInBounce);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutBounce);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutBounce);
			}
			if (easingFunction == EasingFunction.Ease.EaseInBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseInBack);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutBack);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutBack);
			}
			if (easingFunction == EasingFunction.Ease.EaseInElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInElastic);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutElastic);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutElastic);
			}
			return null;
		}

		// Token: 0x06009B2B RID: 39723 RVA: 0x0031E79C File Offset: 0x0031C99C
		public static EasingFunction.Function GetEasingFunctionDerivative(EasingFunction.Ease easingFunction)
		{
			if (easingFunction == EasingFunction.Ease.EaseInQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuadD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuadD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuad)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuadD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInCubicD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutCubicD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutCubic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutCubicD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuartD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuartD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuart)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuartD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseInQuintD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutQuintD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutQuint)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutQuintD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseInSineD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutSineD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutSine)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutSineD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseInExpoD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutExpoD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutExpo)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutExpoD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseInCircD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutCircD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutCirc)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutCircD);
			}
			if (easingFunction == EasingFunction.Ease.Linear)
			{
				return new EasingFunction.Function(EasingFunction.LinearD);
			}
			if (easingFunction == EasingFunction.Ease.Spring)
			{
				return new EasingFunction.Function(EasingFunction.SpringD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseInBounceD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutBounceD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutBounce)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutBounceD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseInBackD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutBackD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutBack)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutBackD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInElasticD);
			}
			if (easingFunction == EasingFunction.Ease.EaseOutElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseOutElasticD);
			}
			if (easingFunction == EasingFunction.Ease.EaseInOutElastic)
			{
				return new EasingFunction.Function(EasingFunction.EaseInOutElasticD);
			}
			return null;
		}

		// Token: 0x040080CC RID: 32972
		private const float NATURAL_LOG_OF_2 = 0.6931472f;

		// Token: 0x040080CD RID: 32973
		public static AnimationCurve AnimationCurve;

		// Token: 0x02002788 RID: 10120
		public enum Ease
		{
			// Token: 0x0400F446 RID: 62534
			EaseInQuad,
			// Token: 0x0400F447 RID: 62535
			EaseOutQuad,
			// Token: 0x0400F448 RID: 62536
			EaseInOutQuad,
			// Token: 0x0400F449 RID: 62537
			EaseInCubic,
			// Token: 0x0400F44A RID: 62538
			EaseOutCubic,
			// Token: 0x0400F44B RID: 62539
			EaseInOutCubic,
			// Token: 0x0400F44C RID: 62540
			EaseInQuart,
			// Token: 0x0400F44D RID: 62541
			EaseOutQuart,
			// Token: 0x0400F44E RID: 62542
			EaseInOutQuart,
			// Token: 0x0400F44F RID: 62543
			EaseInQuint,
			// Token: 0x0400F450 RID: 62544
			EaseOutQuint,
			// Token: 0x0400F451 RID: 62545
			EaseInOutQuint,
			// Token: 0x0400F452 RID: 62546
			EaseInSine,
			// Token: 0x0400F453 RID: 62547
			EaseOutSine,
			// Token: 0x0400F454 RID: 62548
			EaseInOutSine,
			// Token: 0x0400F455 RID: 62549
			EaseInExpo,
			// Token: 0x0400F456 RID: 62550
			EaseOutExpo,
			// Token: 0x0400F457 RID: 62551
			EaseInOutExpo,
			// Token: 0x0400F458 RID: 62552
			EaseInCirc,
			// Token: 0x0400F459 RID: 62553
			EaseOutCirc,
			// Token: 0x0400F45A RID: 62554
			EaseInOutCirc,
			// Token: 0x0400F45B RID: 62555
			Linear,
			// Token: 0x0400F45C RID: 62556
			Spring,
			// Token: 0x0400F45D RID: 62557
			EaseInBounce,
			// Token: 0x0400F45E RID: 62558
			EaseOutBounce,
			// Token: 0x0400F45F RID: 62559
			EaseInOutBounce,
			// Token: 0x0400F460 RID: 62560
			EaseInBack,
			// Token: 0x0400F461 RID: 62561
			EaseOutBack,
			// Token: 0x0400F462 RID: 62562
			EaseInOutBack,
			// Token: 0x0400F463 RID: 62563
			EaseInElastic,
			// Token: 0x0400F464 RID: 62564
			EaseOutElastic,
			// Token: 0x0400F465 RID: 62565
			EaseInOutElastic,
			// Token: 0x0400F466 RID: 62566
			CustomCurve
		}

		// Token: 0x02002789 RID: 10121
		// (Invoke) Token: 0x06013A4F RID: 80463
		public delegate float Function(float s, float e, float v);
	}
}
