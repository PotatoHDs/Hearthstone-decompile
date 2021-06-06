using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Hearthstone.Timeline
{
	public class CameraOverlayHelper : TimelineEffectHelper
	{
		private enum ReceivedOriginalValuesType
		{
			None,
			GlobalIllumination
		}

		private Gradient m_gradient = new Gradient();

		private Color m_originalColor;

		private CameraOverlayBehaviour.RenderMode m_renderMode = CameraOverlayBehaviour.RenderMode.GlobalIllumination;

		private ReceivedOriginalValuesType m_receivedOriginalValuesType;

		private GameObject m_overlayObject;

		private Sprite m_overlaySprite;

		private Material m_overlayMaterial;

		private bool[] m_originalOverrideStates = new bool[0];

		private float[] m_originalMixerValues = new float[9];

		private object[] m_valuesForCommonFX;

		protected override void Initialize(params object[] values)
		{
			if (values.Length >= 5 && (object[])values[4] != null && ((object[])values[4]).Length >= 34)
			{
				m_gradient = (Gradient)values[0];
				m_renderMode = (CameraOverlayBehaviour.RenderMode)Enum.Parse(typeof(CameraOverlayBehaviour.RenderMode), values[1].ToString());
				Sprite sprite;
				if (values[2] != null && (object)(sprite = values[2] as Sprite) != null && sprite != null)
				{
					m_overlaySprite = sprite;
				}
				Material material;
				if (values[3] != null && (object)(material = values[3] as Material) != null && material != null)
				{
					m_overlayMaterial = UnityEngine.Object.Instantiate(material);
				}
				m_valuesForCommonFX = (object[])values[4];
				switch (m_renderMode)
				{
				case CameraOverlayBehaviour.RenderMode.CommonFX:
					InitializeCommonFX();
					break;
				case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
					InitializeSpriteOverlay();
					break;
				case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
					InitializeGlobalIllumination();
					break;
				case CameraOverlayBehaviour.RenderMode._:
					break;
				}
			}
		}

		protected override void CopyOriginalValuesFrom<T>(T other)
		{
			CameraOverlayHelper cameraOverlayHelper = other as CameraOverlayHelper;
			m_originalColor = cameraOverlayHelper.m_originalColor;
			m_originalOverrideStates = new bool[cameraOverlayHelper.m_originalOverrideStates.Length];
			for (int i = 0; i < m_originalOverrideStates.Length; i++)
			{
				m_originalOverrideStates[i] = cameraOverlayHelper.m_originalOverrideStates[i];
			}
			m_originalMixerValues = new float[cameraOverlayHelper.m_originalMixerValues.Length];
			for (int j = 0; j < m_originalMixerValues.Length; j++)
			{
				m_originalMixerValues[j] = cameraOverlayHelper.m_originalMixerValues[j];
			}
			if (cameraOverlayHelper.m_renderMode == CameraOverlayBehaviour.RenderMode.GlobalIllumination)
			{
				m_receivedOriginalValuesType = ReceivedOriginalValuesType.GlobalIllumination;
			}
		}

		protected override void OnKill()
		{
			if (m_overlayObject != null)
			{
				UnityEngine.Object.DestroyImmediate(m_overlayObject);
			}
			if (m_overlayMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(m_overlayMaterial);
			}
		}

		protected override void ResetTarget()
		{
			switch (m_renderMode)
			{
			case CameraOverlayBehaviour.RenderMode.CommonFX:
				ResetTargetCommonFX();
				break;
			case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
				ResetTargetSpriteOverlay();
				break;
			case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
				ResetTargetGlobalIllumination();
				break;
			case CameraOverlayBehaviour.RenderMode._:
				break;
			}
		}

		protected override void UpdateTarget(float normalizedTime)
		{
			Color color = m_gradient.Evaluate(normalizedTime);
			switch (m_renderMode)
			{
			case CameraOverlayBehaviour.RenderMode.CommonFX:
				UpdateTargetCommonFX(normalizedTime, color);
				break;
			case CameraOverlayBehaviour.RenderMode.SpriteOverlay:
				UpdateTargetSpriteOverlay(normalizedTime, color);
				break;
			case CameraOverlayBehaviour.RenderMode.GlobalIllumination:
				UpdateTargetGlobalIllumination(normalizedTime, color);
				break;
			case CameraOverlayBehaviour.RenderMode._:
				break;
			}
		}

		private void InitializeCommonFX()
		{
			if (m_overlayMaterial == null)
			{
				m_overlayMaterial = new Material(Shader.Find("FX/CommonFX"));
			}
			Canvas canvas = TimelineEffectUtility.CreateCanvas("Camera Overlay Canvas");
			Image image = canvas.GetComponent<RectTransform>().CreateFillImage("Image");
			image.material = m_overlayMaterial;
			Texture texture = m_overlayMaterial.GetTexture("_MainTex");
			AspectRatioFitter component = image.GetComponent<AspectRatioFitter>();
			if (texture != null && component != null)
			{
				float aspectRatio = (float)texture.width / (float)texture.height;
				component.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
				component.aspectRatio = aspectRatio;
			}
			m_overlayObject = canvas.gameObject;
			m_overlayObject.SetActive(value: false);
		}

		private void ResetTargetCommonFX()
		{
			if (m_overlayObject != null)
			{
				m_overlayObject.SetActive(value: false);
			}
		}

		private float TryEvaluateCurve(object possibleCurve, float t)
		{
			return ((AnimationCurve)possibleCurve)?.Evaluate(t) ?? 0f;
		}

		private Color TryEvaluateGradient(object possibleGradient, float t)
		{
			return ((Gradient)possibleGradient)?.Evaluate(t) ?? Color.clear;
		}

		private void TrySetMatFloat(Material material, string prop, object curve, float t)
		{
			if (material.HasProperty(prop))
			{
				material.SetFloat(prop, TryEvaluateCurve(curve, t));
			}
		}

		private void TrySetMatColor(Material material, string prop, object gradient, float t)
		{
			if (material.HasProperty(prop))
			{
				material.SetColor(prop, TryEvaluateGradient(gradient, t));
			}
		}

		private void TrySetMatVector(Material material, string prop, object[] vector, float t)
		{
			if (material.HasProperty(prop) && vector.Length != 0)
			{
				Vector4 value = default(Vector4);
				value.x = TryEvaluateCurve(vector[0], t);
				if (vector.Length > 1)
				{
					value.y = TryEvaluateCurve(vector[1], t);
				}
				if (vector.Length > 2)
				{
					value.z = TryEvaluateCurve(vector[2], t);
				}
				if (vector.Length > 3)
				{
					value.w = TryEvaluateCurve(vector[3], t);
				}
				material.SetVector(prop, value);
			}
		}

		private void UpdateTargetCommonFX(float normalizedTime, Color color)
		{
			if (m_overlayMaterial != null)
			{
				m_overlayMaterial.color = color;
				if (m_overlayMaterial.HasProperty("_MainTex"))
				{
					m_overlayMaterial.SetTextureScale("_MainTex", new Vector2(TryEvaluateCurve(m_valuesForCommonFX[0], normalizedTime), TryEvaluateCurve(m_valuesForCommonFX[1], normalizedTime)));
					m_overlayMaterial.SetTextureOffset("_MainTex", new Vector2(TryEvaluateCurve(m_valuesForCommonFX[2], normalizedTime), TryEvaluateCurve(m_valuesForCommonFX[3], normalizedTime)));
				}
				TrySetMatFloat(m_overlayMaterial, "_ColorMul", m_valuesForCommonFX[4], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_ColorAdd", m_valuesForCommonFX[5], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_AlphaMul", m_valuesForCommonFX[6], normalizedTime);
				TrySetMatColor(m_overlayMaterial, "_Color2", m_valuesForCommonFX[7], normalizedTime);
				if (m_overlayMaterial.HasProperty("_SecondTex"))
				{
					m_overlayMaterial.SetTextureScale("_SecondTex", new Vector2(TryEvaluateCurve(m_valuesForCommonFX[8], normalizedTime), TryEvaluateCurve(m_valuesForCommonFX[9], normalizedTime)));
					m_overlayMaterial.SetTextureOffset("_SecondTex", new Vector2(TryEvaluateCurve(m_valuesForCommonFX[10], normalizedTime), TryEvaluateCurve(m_valuesForCommonFX[11], normalizedTime)));
				}
				TrySetMatFloat(m_overlayMaterial, "_ColorMul2", m_valuesForCommonFX[12], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_SecondTexInitRot", m_valuesForCommonFX[13], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_DissolveFactor", m_valuesForCommonFX[14], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_DissolveSmoothness", m_valuesForCommonFX[15], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_MinAlpha", m_valuesForCommonFX[16], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_FresnelPower", m_valuesForCommonFX[17], normalizedTime);
				TrySetMatColor(m_overlayMaterial, "_FresnelFrontColor", m_valuesForCommonFX[18], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_FresnelFrontColorMultiplier", m_valuesForCommonFX[19], normalizedTime);
				TrySetMatColor(m_overlayMaterial, "_FresnelBorderColor", m_valuesForCommonFX[20], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_FresnelBorderColorMultiplier", m_valuesForCommonFX[21], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_GlowIntensity", m_valuesForCommonFX[22], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_GlowAddMul", m_valuesForCommonFX[23], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_FadeoutDistance", m_valuesForCommonFX[24], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_FadeoutY", m_valuesForCommonFX[25], normalizedTime);
				TrySetMatFloat(m_overlayMaterial, "_VertexMaskAlphaMul", m_valuesForCommonFX[26], normalizedTime);
				TrySetMatVector(m_overlayMaterial, "_VertexMaskLinearGradient", new object[4]
				{
					m_valuesForCommonFX[27],
					m_valuesForCommonFX[28],
					m_valuesForCommonFX[29],
					m_valuesForCommonFX[30]
				}, normalizedTime);
				TrySetMatVector(m_overlayMaterial, "_VertexMaskUVScaleOffset", new object[4]
				{
					m_valuesForCommonFX[31],
					m_valuesForCommonFX[32],
					m_valuesForCommonFX[33],
					m_valuesForCommonFX[34]
				}, normalizedTime);
			}
			if (m_overlayObject != null)
			{
				m_overlayObject.SetActive(value: true);
			}
		}

		private void InitializeSpriteOverlay()
		{
			if (m_overlayMaterial == null)
			{
				m_overlayMaterial = new Material(Shader.Find("Sprites/Default"));
			}
			Canvas canvas = TimelineEffectUtility.CreateCanvas("Camera Overlay Canvas");
			Image image = canvas.GetComponent<RectTransform>().CreateFillImage("Image", m_overlaySprite);
			image.material = m_overlayMaterial;
			AspectRatioFitter component = image.GetComponent<AspectRatioFitter>();
			if (component != null)
			{
				float aspectRatio = 1f;
				if (m_overlaySprite != null)
				{
					aspectRatio = m_overlaySprite.rect.width / m_overlaySprite.rect.height;
				}
				else if (m_overlayMaterial.HasProperty("_MainTex"))
				{
					Texture texture = m_overlayMaterial.GetTexture("_MainTex");
					if (texture != null)
					{
						aspectRatio = (float)texture.width / (float)texture.height;
					}
				}
				component.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
				component.aspectRatio = aspectRatio;
			}
			m_overlayObject = canvas.gameObject;
			m_overlayObject.SetActive(value: false);
		}

		private void ResetTargetSpriteOverlay()
		{
			if (m_overlayObject != null)
			{
				m_overlayObject.SetActive(value: false);
			}
		}

		private void UpdateTargetSpriteOverlay(float normalizedTime, Color color)
		{
			if (m_overlayMaterial != null)
			{
				m_overlayMaterial.color = color;
			}
			if (m_overlayObject != null)
			{
				m_overlayObject.SetActive(value: true);
			}
		}

		private void InitializeGlobalIllumination()
		{
			if (m_receivedOriginalValuesType != ReceivedOriginalValuesType.GlobalIllumination)
			{
				m_originalColor = RenderSettings.ambientLight;
			}
			if (RenderSettings.ambientMode != AmbientMode.Flat)
			{
				Debug.LogWarning("Lighting settings are not correct for the Global Illumination render method.");
			}
		}

		private void ResetTargetGlobalIllumination()
		{
			RenderSettings.ambientLight = m_originalColor;
		}

		private void UpdateTargetGlobalIllumination(float normalizedTime, Color color)
		{
			RenderSettings.ambientLight = Color.Lerp(m_originalColor, color, color.a);
		}
	}
}
