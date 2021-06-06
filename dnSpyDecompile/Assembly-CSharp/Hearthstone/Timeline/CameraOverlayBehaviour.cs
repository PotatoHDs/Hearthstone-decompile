using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010E9 RID: 4329
	[Serializable]
	public class CameraOverlayBehaviour : TimelineEffectBehaviour<CameraOverlayHelper>
	{
		// Token: 0x0600BE17 RID: 48663 RVA: 0x0039EF78 File Offset: 0x0039D178
		protected override object[] GetHelperInitializationData()
		{
			if (this.m_ambientColor == null)
			{
				return null;
			}
			return new object[]
			{
				this.m_ambientColor,
				this.m_renderMode,
				this.m_cameraOverlaySprite,
				this.m_cameraOverlayMaterial,
				new object[]
				{
					this.m_fxTilingX,
					this.m_fxTilingY,
					this.m_fxOffsetX,
					this.m_fxOffsetY,
					this.m_fxColorMult,
					this.m_fxColorAdd,
					this.m_fxAlphaMult,
					this.m_fx2ndColor,
					this.m_fx2ndTilingX,
					this.m_fx2ndTilingY,
					this.m_fx2ndOffsetX,
					this.m_fx2ndOffsetY,
					this.m_fx2ndColorMult,
					this.m_fx2ndRot,
					this.m_fxDissolveFact,
					this.m_fxDissolveSmooth,
					this.m_fxDissolveMinAlpha,
					this.m_fxFresnelPower,
					this.m_fxFresnelFrontColor,
					this.m_fxFresnelFrontMult,
					this.m_fxFresnelSideColor,
					this.m_fxFresnelSideMult,
					this.m_fxGlow,
					this.m_fxGlowAddMult,
					this.m_fxFadeDist,
					this.m_fxFadePosY,
					this.m_fxMaskAlphaMult,
					this.m_fxMaskAlphaBetweenA,
					this.m_fxMaskAlphaBetweenB,
					this.m_fxMaskAlphaBetweenC,
					this.m_fxMaskAlphaBetweenD,
					this.m_fxMaskScaleA,
					this.m_fxMaskScaleB,
					this.m_fxMaskScaleC,
					this.m_fxMaskScaleD
				}
			};
		}

		// Token: 0x0600BE18 RID: 48664 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void InitializeFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x0600BE19 RID: 48665 RVA: 0x0039F120 File Offset: 0x0039D320
		protected override void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime)
		{
			base.Helper.UpdateEffect((float)playable.GetTime<Playable>(), !playable.GetGraph<Playable>().IsPlaying());
		}

		// Token: 0x04009ABB RID: 39611
		[SerializeField]
		private CameraOverlayBehaviour.RenderMode m_renderMode;

		// Token: 0x04009ABC RID: 39612
		[SerializeField]
		private Gradient m_ambientColor;

		// Token: 0x04009ABD RID: 39613
		[SerializeField]
		private Sprite m_cameraOverlaySprite;

		// Token: 0x04009ABE RID: 39614
		[SerializeField]
		private Material m_cameraOverlayMaterial;

		// Token: 0x04009ABF RID: 39615
		[SerializeField]
		private AnimationCurve m_fxTilingX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC0 RID: 39616
		[SerializeField]
		private AnimationCurve m_fxTilingY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC1 RID: 39617
		[SerializeField]
		private AnimationCurve m_fxOffsetX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AC2 RID: 39618
		[SerializeField]
		private AnimationCurve m_fxOffsetY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AC3 RID: 39619
		[SerializeField]
		private AnimationCurve m_fxColorMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC4 RID: 39620
		[SerializeField]
		private AnimationCurve m_fxColorAdd = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AC5 RID: 39621
		[SerializeField]
		private AnimationCurve m_fxAlphaMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC6 RID: 39622
		[SerializeField]
		private Gradient m_fx2ndColor;

		// Token: 0x04009AC7 RID: 39623
		[SerializeField]
		private AnimationCurve m_fx2ndTilingX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC8 RID: 39624
		[SerializeField]
		private AnimationCurve m_fx2ndTilingY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AC9 RID: 39625
		[SerializeField]
		private AnimationCurve m_fx2ndOffsetX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009ACA RID: 39626
		[SerializeField]
		private AnimationCurve m_fx2ndOffsetY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009ACB RID: 39627
		[SerializeField]
		private AnimationCurve m_fx2ndColorMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ACC RID: 39628
		[SerializeField]
		private AnimationCurve m_fx2ndRot = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009ACD RID: 39629
		[SerializeField]
		private AnimationCurve m_fxDissolveFact = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009ACE RID: 39630
		[SerializeField]
		private AnimationCurve m_fxDissolveSmooth = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.1f),
			new Keyframe(1f, 0.1f)
		});

		// Token: 0x04009ACF RID: 39631
		[SerializeField]
		private AnimationCurve m_fxDissolveMinAlpha = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AD0 RID: 39632
		[SerializeField]
		private AnimationCurve m_fxFresnelPower = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AD1 RID: 39633
		[SerializeField]
		private Gradient m_fxFresnelFrontColor;

		// Token: 0x04009AD2 RID: 39634
		[SerializeField]
		private AnimationCurve m_fxFresnelFrontMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AD3 RID: 39635
		[SerializeField]
		private Gradient m_fxFresnelSideColor;

		// Token: 0x04009AD4 RID: 39636
		[SerializeField]
		private AnimationCurve m_fxFresnelSideMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AD5 RID: 39637
		[SerializeField]
		private AnimationCurve m_fxGlow = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AD6 RID: 39638
		[SerializeField]
		private AnimationCurve m_fxGlowAddMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AD7 RID: 39639
		[SerializeField]
		private AnimationCurve m_fxFadeDist = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.3f),
			new Keyframe(1f, 0.3f)
		});

		// Token: 0x04009AD8 RID: 39640
		[SerializeField]
		private AnimationCurve m_fxFadePosY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, -0.06f),
			new Keyframe(1f, -0.06f)
		});

		// Token: 0x04009AD9 RID: 39641
		[SerializeField]
		private AnimationCurve m_fxMaskAlphaMult = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADA RID: 39642
		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenA = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADB RID: 39643
		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenB = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADC RID: 39644
		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenC = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADD RID: 39645
		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenD = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADE RID: 39646
		[SerializeField]
		private AnimationCurve m_fxMaskScaleA = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009ADF RID: 39647
		[SerializeField]
		private AnimationCurve m_fxMaskScaleB = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04009AE0 RID: 39648
		[SerializeField]
		private AnimationCurve m_fxMaskScaleC = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04009AE1 RID: 39649
		[SerializeField]
		private AnimationCurve m_fxMaskScaleD = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x020028AA RID: 10410
		[Serializable]
		public enum RenderMode
		{
			// Token: 0x0400FA93 RID: 64147
			CommonFX,
			// Token: 0x0400FA94 RID: 64148
			SpriteOverlay,
			// Token: 0x0400FA95 RID: 64149
			_,
			// Token: 0x0400FA96 RID: 64150
			GlobalIllumination,
			// Token: 0x0400FA97 RID: 64151
			PostProcessing
		}
	}
}
