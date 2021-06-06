using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Hearthstone.Timeline
{
	// Token: 0x020010EA RID: 4330
	public class CameraOverlayHelper : TimelineEffectHelper
	{
		// Token: 0x0600BE1B RID: 48667 RVA: 0x0039F904 File Offset: 0x0039DB04
		protected override void Initialize(params object[] values)
		{
			if (values.Length < 5 || (object[])values[4] == null || ((object[])values[4]).Length < 34)
			{
				return;
			}
			this.m_gradient = (Gradient)values[0];
			this.m_renderMode = (CameraOverlayBehaviour.RenderMode)Enum.Parse(typeof(CameraOverlayBehaviour.RenderMode), values[1].ToString());
			Sprite sprite;
			if (values[2] != null && (sprite = (values[2] as Sprite)) != null && sprite != null)
			{
				this.m_overlaySprite = sprite;
			}
			Material material;
			if (values[3] != null && (material = (values[3] as Material)) != null && material != null)
			{
				this.m_overlayMaterial = UnityEngine.Object.Instantiate<Material>(material);
			}
			this.m_valuesForCommonFX = (object[])values[4];
			switch (this.m_renderMode)
			{
			case CameraOverlayBehaviour.RenderMode.CommonFX:
				this.InitializeCommonFX();
				return;
			case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
				this.InitializeSpriteOverlay();
				return;
			case CameraOverlayBehaviour.RenderMode._:
				break;
			case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
				this.InitializeGlobalIllumination();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600BE1C RID: 48668 RVA: 0x0039F9E8 File Offset: 0x0039DBE8
		protected override void CopyOriginalValuesFrom<T>(T other)
		{
			CameraOverlayHelper cameraOverlayHelper = other as CameraOverlayHelper;
			this.m_originalColor = cameraOverlayHelper.m_originalColor;
			this.m_originalOverrideStates = new bool[cameraOverlayHelper.m_originalOverrideStates.Length];
			for (int i = 0; i < this.m_originalOverrideStates.Length; i++)
			{
				this.m_originalOverrideStates[i] = cameraOverlayHelper.m_originalOverrideStates[i];
			}
			this.m_originalMixerValues = new float[cameraOverlayHelper.m_originalMixerValues.Length];
			for (int j = 0; j < this.m_originalMixerValues.Length; j++)
			{
				this.m_originalMixerValues[j] = cameraOverlayHelper.m_originalMixerValues[j];
			}
			if (cameraOverlayHelper.m_renderMode == CameraOverlayBehaviour.RenderMode.GlobalIllumination)
			{
				this.m_receivedOriginalValuesType = CameraOverlayHelper.ReceivedOriginalValuesType.GlobalIllumination;
			}
		}

		// Token: 0x0600BE1D RID: 48669 RVA: 0x0039FA89 File Offset: 0x0039DC89
		protected override void OnKill()
		{
			if (this.m_overlayObject != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_overlayObject);
			}
			if (this.m_overlayMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_overlayMaterial);
			}
		}

		// Token: 0x0600BE1E RID: 48670 RVA: 0x0039FAC0 File Offset: 0x0039DCC0
		protected override void ResetTarget()
		{
			switch (this.m_renderMode)
			{
			case CameraOverlayBehaviour.RenderMode.CommonFX:
				this.ResetTargetCommonFX();
				return;
			case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
				this.ResetTargetSpriteOverlay();
				return;
			case CameraOverlayBehaviour.RenderMode._:
				break;
			case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
				this.ResetTargetGlobalIllumination();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600BE1F RID: 48671 RVA: 0x0039FB00 File Offset: 0x0039DD00
		protected override void UpdateTarget(float normalizedTime)
		{
			Color color = this.m_gradient.Evaluate(normalizedTime);
			switch (this.m_renderMode)
			{
			case CameraOverlayBehaviour.RenderMode.CommonFX:
				this.UpdateTargetCommonFX(normalizedTime, color);
				return;
			case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
				this.UpdateTargetSpriteOverlay(normalizedTime, color);
				return;
			case CameraOverlayBehaviour.RenderMode._:
				break;
			case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
				this.UpdateTargetGlobalIllumination(normalizedTime, color);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600BE20 RID: 48672 RVA: 0x0039FB54 File Offset: 0x0039DD54
		private void InitializeCommonFX()
		{
			if (this.m_overlayMaterial == null)
			{
				this.m_overlayMaterial = new Material(Shader.Find("FX/CommonFX"));
			}
			Canvas canvas = TimelineEffectUtility.CreateCanvas("Camera Overlay Canvas", 999);
			Image image = canvas.GetComponent<RectTransform>().CreateFillImage("Image", null);
			image.material = this.m_overlayMaterial;
			Texture texture = this.m_overlayMaterial.GetTexture("_MainTex");
			AspectRatioFitter component = image.GetComponent<AspectRatioFitter>();
			if (texture != null && component != null)
			{
				float aspectRatio = (float)texture.width / (float)texture.height;
				component.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
				component.aspectRatio = aspectRatio;
			}
			this.m_overlayObject = canvas.gameObject;
			this.m_overlayObject.SetActive(false);
		}

		// Token: 0x0600BE21 RID: 48673 RVA: 0x0039FC10 File Offset: 0x0039DE10
		private void ResetTargetCommonFX()
		{
			if (this.m_overlayObject != null)
			{
				this.m_overlayObject.SetActive(false);
			}
		}

		// Token: 0x0600BE22 RID: 48674 RVA: 0x0039FC2C File Offset: 0x0039DE2C
		private float TryEvaluateCurve(object possibleCurve, float t)
		{
			AnimationCurve animationCurve = (AnimationCurve)possibleCurve;
			if (animationCurve != null)
			{
				return animationCurve.Evaluate(t);
			}
			return 0f;
		}

		// Token: 0x0600BE23 RID: 48675 RVA: 0x0039FC50 File Offset: 0x0039DE50
		private Color TryEvaluateGradient(object possibleGradient, float t)
		{
			Gradient gradient = (Gradient)possibleGradient;
			if (gradient != null)
			{
				return gradient.Evaluate(t);
			}
			return Color.clear;
		}

		// Token: 0x0600BE24 RID: 48676 RVA: 0x0039FC74 File Offset: 0x0039DE74
		private void TrySetMatFloat(Material material, string prop, object curve, float t)
		{
			if (material.HasProperty(prop))
			{
				material.SetFloat(prop, this.TryEvaluateCurve(curve, t));
			}
		}

		// Token: 0x0600BE25 RID: 48677 RVA: 0x0039FC8F File Offset: 0x0039DE8F
		private void TrySetMatColor(Material material, string prop, object gradient, float t)
		{
			if (material.HasProperty(prop))
			{
				material.SetColor(prop, this.TryEvaluateGradient(gradient, t));
			}
		}

		// Token: 0x0600BE26 RID: 48678 RVA: 0x0039FCAC File Offset: 0x0039DEAC
		private void TrySetMatVector(Material material, string prop, object[] vector, float t)
		{
			if (material.HasProperty(prop) && vector.Length != 0)
			{
				Vector4 value = default(Vector4);
				value.x = this.TryEvaluateCurve(vector[0], t);
				if (vector.Length > 1)
				{
					value.y = this.TryEvaluateCurve(vector[1], t);
				}
				if (vector.Length > 2)
				{
					value.z = this.TryEvaluateCurve(vector[2], t);
				}
				if (vector.Length > 3)
				{
					value.w = this.TryEvaluateCurve(vector[3], t);
				}
				material.SetVector(prop, value);
			}
		}

		// Token: 0x0600BE27 RID: 48679 RVA: 0x0039FD30 File Offset: 0x0039DF30
		private void UpdateTargetCommonFX(float normalizedTime, Color color)
		{
			if (this.m_overlayMaterial != null)
			{
				this.m_overlayMaterial.color = color;
				if (this.m_overlayMaterial.HasProperty("_MainTex"))
				{
					this.m_overlayMaterial.SetTextureScale("_MainTex", new Vector2(this.TryEvaluateCurve(this.m_valuesForCommonFX[0], normalizedTime), this.TryEvaluateCurve(this.m_valuesForCommonFX[1], normalizedTime)));
					this.m_overlayMaterial.SetTextureOffset("_MainTex", new Vector2(this.TryEvaluateCurve(this.m_valuesForCommonFX[2], normalizedTime), this.TryEvaluateCurve(this.m_valuesForCommonFX[3], normalizedTime)));
				}
				this.TrySetMatFloat(this.m_overlayMaterial, "_ColorMul", this.m_valuesForCommonFX[4], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_ColorAdd", this.m_valuesForCommonFX[5], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_AlphaMul", this.m_valuesForCommonFX[6], normalizedTime);
				this.TrySetMatColor(this.m_overlayMaterial, "_Color2", this.m_valuesForCommonFX[7], normalizedTime);
				if (this.m_overlayMaterial.HasProperty("_SecondTex"))
				{
					this.m_overlayMaterial.SetTextureScale("_SecondTex", new Vector2(this.TryEvaluateCurve(this.m_valuesForCommonFX[8], normalizedTime), this.TryEvaluateCurve(this.m_valuesForCommonFX[9], normalizedTime)));
					this.m_overlayMaterial.SetTextureOffset("_SecondTex", new Vector2(this.TryEvaluateCurve(this.m_valuesForCommonFX[10], normalizedTime), this.TryEvaluateCurve(this.m_valuesForCommonFX[11], normalizedTime)));
				}
				this.TrySetMatFloat(this.m_overlayMaterial, "_ColorMul2", this.m_valuesForCommonFX[12], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_SecondTexInitRot", this.m_valuesForCommonFX[13], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_DissolveFactor", this.m_valuesForCommonFX[14], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_DissolveSmoothness", this.m_valuesForCommonFX[15], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_MinAlpha", this.m_valuesForCommonFX[16], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_FresnelPower", this.m_valuesForCommonFX[17], normalizedTime);
				this.TrySetMatColor(this.m_overlayMaterial, "_FresnelFrontColor", this.m_valuesForCommonFX[18], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_FresnelFrontColorMultiplier", this.m_valuesForCommonFX[19], normalizedTime);
				this.TrySetMatColor(this.m_overlayMaterial, "_FresnelBorderColor", this.m_valuesForCommonFX[20], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_FresnelBorderColorMultiplier", this.m_valuesForCommonFX[21], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_GlowIntensity", this.m_valuesForCommonFX[22], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_GlowAddMul", this.m_valuesForCommonFX[23], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_FadeoutDistance", this.m_valuesForCommonFX[24], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_FadeoutY", this.m_valuesForCommonFX[25], normalizedTime);
				this.TrySetMatFloat(this.m_overlayMaterial, "_VertexMaskAlphaMul", this.m_valuesForCommonFX[26], normalizedTime);
				this.TrySetMatVector(this.m_overlayMaterial, "_VertexMaskLinearGradient", new object[]
				{
					this.m_valuesForCommonFX[27],
					this.m_valuesForCommonFX[28],
					this.m_valuesForCommonFX[29],
					this.m_valuesForCommonFX[30]
				}, normalizedTime);
				this.TrySetMatVector(this.m_overlayMaterial, "_VertexMaskUVScaleOffset", new object[]
				{
					this.m_valuesForCommonFX[31],
					this.m_valuesForCommonFX[32],
					this.m_valuesForCommonFX[33],
					this.m_valuesForCommonFX[34]
				}, normalizedTime);
			}
			if (this.m_overlayObject != null)
			{
				this.m_overlayObject.SetActive(true);
			}
		}

		// Token: 0x0600BE28 RID: 48680 RVA: 0x003A00F4 File Offset: 0x0039E2F4
		private void InitializeSpriteOverlay()
		{
			if (this.m_overlayMaterial == null)
			{
				this.m_overlayMaterial = new Material(Shader.Find("Sprites/Default"));
			}
			Canvas canvas = TimelineEffectUtility.CreateCanvas("Camera Overlay Canvas", 999);
			Image image = canvas.GetComponent<RectTransform>().CreateFillImage("Image", this.m_overlaySprite);
			image.material = this.m_overlayMaterial;
			AspectRatioFitter component = image.GetComponent<AspectRatioFitter>();
			if (component != null)
			{
				float aspectRatio = 1f;
				if (this.m_overlaySprite != null)
				{
					aspectRatio = this.m_overlaySprite.rect.width / this.m_overlaySprite.rect.height;
				}
				else if (this.m_overlayMaterial.HasProperty("_MainTex"))
				{
					Texture texture = this.m_overlayMaterial.GetTexture("_MainTex");
					if (texture != null)
					{
						aspectRatio = (float)texture.width / (float)texture.height;
					}
				}
				component.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
				component.aspectRatio = aspectRatio;
			}
			this.m_overlayObject = canvas.gameObject;
			this.m_overlayObject.SetActive(false);
		}

		// Token: 0x0600BE29 RID: 48681 RVA: 0x0039FC10 File Offset: 0x0039DE10
		private void ResetTargetSpriteOverlay()
		{
			if (this.m_overlayObject != null)
			{
				this.m_overlayObject.SetActive(false);
			}
		}

		// Token: 0x0600BE2A RID: 48682 RVA: 0x003A020C File Offset: 0x0039E40C
		private void UpdateTargetSpriteOverlay(float normalizedTime, Color color)
		{
			if (this.m_overlayMaterial != null)
			{
				this.m_overlayMaterial.color = color;
			}
			if (this.m_overlayObject != null)
			{
				this.m_overlayObject.SetActive(true);
			}
		}

		// Token: 0x0600BE2B RID: 48683 RVA: 0x003A0242 File Offset: 0x0039E442
		private void InitializeGlobalIllumination()
		{
			if (this.m_receivedOriginalValuesType != CameraOverlayHelper.ReceivedOriginalValuesType.GlobalIllumination)
			{
				this.m_originalColor = RenderSettings.ambientLight;
			}
			if (RenderSettings.ambientMode != AmbientMode.Flat)
			{
				Debug.LogWarning("Lighting settings are not correct for the Global Illumination render method.");
			}
		}

		// Token: 0x0600BE2C RID: 48684 RVA: 0x003A026A File Offset: 0x0039E46A
		private void ResetTargetGlobalIllumination()
		{
			RenderSettings.ambientLight = this.m_originalColor;
		}

		// Token: 0x0600BE2D RID: 48685 RVA: 0x003A0277 File Offset: 0x0039E477
		private void UpdateTargetGlobalIllumination(float normalizedTime, Color color)
		{
			RenderSettings.ambientLight = Color.Lerp(this.m_originalColor, color, color.a);
		}

		// Token: 0x04009AE2 RID: 39650
		private Gradient m_gradient = new Gradient();

		// Token: 0x04009AE3 RID: 39651
		private Color m_originalColor;

		// Token: 0x04009AE4 RID: 39652
		private CameraOverlayBehaviour.RenderMode m_renderMode = CameraOverlayBehaviour.RenderMode.GlobalIllumination;

		// Token: 0x04009AE5 RID: 39653
		private CameraOverlayHelper.ReceivedOriginalValuesType m_receivedOriginalValuesType;

		// Token: 0x04009AE6 RID: 39654
		private GameObject m_overlayObject;

		// Token: 0x04009AE7 RID: 39655
		private Sprite m_overlaySprite;

		// Token: 0x04009AE8 RID: 39656
		private Material m_overlayMaterial;

		// Token: 0x04009AE9 RID: 39657
		private bool[] m_originalOverrideStates = new bool[0];

		// Token: 0x04009AEA RID: 39658
		private float[] m_originalMixerValues = new float[9];

		// Token: 0x04009AEB RID: 39659
		private object[] m_valuesForCommonFX;

		// Token: 0x020028AB RID: 10411
		private enum ReceivedOriginalValuesType
		{
			// Token: 0x0400FA99 RID: 64153
			None,
			// Token: 0x0400FA9A RID: 64154
			GlobalIllumination
		}
	}
}
