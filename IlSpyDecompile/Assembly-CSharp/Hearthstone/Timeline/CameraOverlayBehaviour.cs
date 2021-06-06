using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	public class CameraOverlayBehaviour : TimelineEffectBehaviour<CameraOverlayHelper>
	{
		[Serializable]
		public enum RenderMode
		{
			CommonFX,
			SpriteOverlay,
			_,
			GlobalIllumination,
			PostProcessing
		}

		[SerializeField]
		private RenderMode m_renderMode;

		[SerializeField]
		private Gradient m_ambientColor;

		[SerializeField]
		private Sprite m_cameraOverlaySprite;

		[SerializeField]
		private Material m_cameraOverlayMaterial;

		[SerializeField]
		private AnimationCurve m_fxTilingX = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxTilingY = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxOffsetX = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxOffsetY = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxColorMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxColorAdd = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxAlphaMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private Gradient m_fx2ndColor;

		[SerializeField]
		private AnimationCurve m_fx2ndTilingX = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fx2ndTilingY = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fx2ndOffsetX = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fx2ndOffsetY = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fx2ndColorMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fx2ndRot = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxDissolveFact = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxDissolveSmooth = new AnimationCurve(new Keyframe(0f, 0.1f), new Keyframe(1f, 0.1f));

		[SerializeField]
		private AnimationCurve m_fxDissolveMinAlpha = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxFresnelPower = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private Gradient m_fxFresnelFrontColor;

		[SerializeField]
		private AnimationCurve m_fxFresnelFrontMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private Gradient m_fxFresnelSideColor;

		[SerializeField]
		private AnimationCurve m_fxFresnelSideMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxGlow = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxGlowAddMult = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxFadeDist = new AnimationCurve(new Keyframe(0f, 0.3f), new Keyframe(1f, 0.3f));

		[SerializeField]
		private AnimationCurve m_fxFadePosY = new AnimationCurve(new Keyframe(0f, -0.06f), new Keyframe(1f, -0.06f));

		[SerializeField]
		private AnimationCurve m_fxMaskAlphaMult = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenA = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenB = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenC = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskAlphaBetweenD = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskScaleA = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskScaleB = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private AnimationCurve m_fxMaskScaleC = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		[SerializeField]
		private AnimationCurve m_fxMaskScaleD = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 0f));

		protected override object[] GetHelperInitializationData()
		{
			if (m_ambientColor == null)
			{
				return null;
			}
			return new object[5]
			{
				m_ambientColor,
				m_renderMode,
				m_cameraOverlaySprite,
				m_cameraOverlayMaterial,
				new object[35]
				{
					m_fxTilingX, m_fxTilingY, m_fxOffsetX, m_fxOffsetY, m_fxColorMult, m_fxColorAdd, m_fxAlphaMult, m_fx2ndColor, m_fx2ndTilingX, m_fx2ndTilingY,
					m_fx2ndOffsetX, m_fx2ndOffsetY, m_fx2ndColorMult, m_fx2ndRot, m_fxDissolveFact, m_fxDissolveSmooth, m_fxDissolveMinAlpha, m_fxFresnelPower, m_fxFresnelFrontColor, m_fxFresnelFrontMult,
					m_fxFresnelSideColor, m_fxFresnelSideMult, m_fxGlow, m_fxGlowAddMult, m_fxFadeDist, m_fxFadePosY, m_fxMaskAlphaMult, m_fxMaskAlphaBetweenA, m_fxMaskAlphaBetweenB, m_fxMaskAlphaBetweenC,
					m_fxMaskAlphaBetweenD, m_fxMaskScaleA, m_fxMaskScaleB, m_fxMaskScaleC, m_fxMaskScaleD
				}
			};
		}

		protected override void InitializeFrame(Playable playable, FrameData info, object playerData)
		{
		}

		protected override void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime)
		{
			base.Helper.UpdateEffect((float)playable.GetTime(), !playable.GetGraph().IsPlaying());
		}
	}
}
