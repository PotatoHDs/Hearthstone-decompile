using UnityEngine;

[ExecuteAlways]
public class ScreenEffectsRender : MonoBehaviour
{
	private const int GLOW_RANDER_BUFFER_RESOLUTION = 512;

	private const string BLOOM_SHADER_NAME = "Hidden/ScreenEffectsGlow";

	private const string DISTORTION_SHADER_NAME = "Hidden/ScreenEffectDistortion";

	private const string GLOW_MASK_SHADER = "Hidden/ScreenEffectsMask";

	[HideInInspector]
	public RenderTexture m_EffectsTexture;

	[HideInInspector]
	public Camera m_EffectsObjectsCamera;

	public bool m_Debug;

	private int m_width;

	private int m_height;

	private int m_previousWidth;

	private int m_previousHeight;

	private RenderTexture m_MaskRenderTexture;

	private Shader m_MaskShader;

	private Shader m_GlowShader;

	private Material m_GlowMaterial;

	private Shader m_DistortionShader;

	private Material m_DistortionMaterial;

	protected Material GlowMaterial
	{
		get
		{
			if (m_GlowMaterial == null)
			{
				if (m_GlowShader == null)
				{
					m_GlowShader = Shader.Find("Hidden/ScreenEffectsGlow");
					if (!m_GlowShader)
					{
						Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectsGlow");
					}
				}
				m_GlowMaterial = new Material(m_GlowShader);
				SceneUtils.SetHideFlags(m_GlowMaterial, HideFlags.HideAndDontSave);
			}
			return m_GlowMaterial;
		}
	}

	protected Material DistortionMaterial
	{
		get
		{
			if (m_DistortionMaterial == null)
			{
				if (m_DistortionShader == null)
				{
					m_DistortionShader = Shader.Find("Hidden/ScreenEffectDistortion");
					if (!m_DistortionShader)
					{
						Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectDistortion");
					}
				}
				m_DistortionMaterial = new Material(m_DistortionShader);
				SceneUtils.SetHideFlags(m_DistortionMaterial, HideFlags.HideAndDontSave);
			}
			return m_DistortionMaterial;
		}
	}

	private void Awake()
	{
		if (ScreenEffectsMgr.Get() == null)
		{
			base.enabled = false;
		}
		if (m_MaskShader == null)
		{
			m_MaskShader = Shader.Find("Hidden/ScreenEffectsMask");
			if (!m_MaskShader)
			{
				Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectsMask");
			}
		}
	}

	private void Update()
	{
		RenderEffectsObjects();
	}

	private void OnDisable()
	{
		Object.DestroyImmediate(GlowMaterial);
		Object.DestroyImmediate(DistortionMaterial);
		if (m_MaskRenderTexture != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_MaskRenderTexture);
			m_MaskRenderTexture = null;
		}
		if (m_GlowMaterial != null)
		{
			Object.DestroyImmediate(m_GlowMaterial);
		}
		if (m_DistortionMaterial != null)
		{
			Object.DestroyImmediate(m_DistortionMaterial);
		}
	}

	private void OnEnable()
	{
		SetupEffect();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderGlow(source, destination);
	}

	private void SetupEffect()
	{
	}

	private void RenderEffectsObjects()
	{
		if (m_EffectsObjectsCamera == null)
		{
			base.enabled = false;
			return;
		}
		float num = (float)Screen.width / (float)Screen.height;
		int num2 = (int)(512f * num);
		int num3 = 512;
		if (num2 != m_previousWidth || num3 != m_previousHeight)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_MaskRenderTexture);
			m_MaskRenderTexture = null;
		}
		if (m_MaskRenderTexture == null)
		{
			m_MaskRenderTexture = RenderTextureTracker.Get().CreateNewTexture(num2, num3, 32);
			m_MaskRenderTexture.filterMode = FilterMode.Bilinear;
			m_previousWidth = num2;
			m_previousHeight = num3;
		}
		if (m_EffectsObjectsCamera.targetTexture == null && m_MaskRenderTexture != null)
		{
			m_EffectsObjectsCamera.targetTexture = m_MaskRenderTexture;
		}
		m_MaskRenderTexture.DiscardContents();
		m_EffectsObjectsCamera.RenderWithShader(m_MaskShader, "RenderType");
	}

	private void RenderGlow(RenderTexture source, RenderTexture destination)
	{
		if (!m_MaskRenderTexture)
		{
			Graphics.Blit(source, destination);
			return;
		}
		int width = m_MaskRenderTexture.width;
		int height = m_MaskRenderTexture.height;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
		temporary.filterMode = FilterMode.Bilinear;
		GlowMaterial.SetFloat("_BlurOffset", 1f);
		Graphics.Blit(m_MaskRenderTexture, temporary, GlowMaterial, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0, source.format);
		temporary2.filterMode = FilterMode.Bilinear;
		GlowMaterial.SetFloat("_BlurOffset", 2f);
		Graphics.Blit(temporary, temporary2, GlowMaterial, 0);
		GlowMaterial.SetFloat("_BlurOffset", 3f);
		temporary.DiscardContents();
		Graphics.Blit(temporary2, temporary, GlowMaterial, 0);
		GlowMaterial.SetFloat("_BlurOffset", 5f);
		temporary2.DiscardContents();
		Graphics.Blit(temporary, temporary2, GlowMaterial, 0);
		GlowMaterial.SetTexture("_BlurTex", temporary2);
		if (!m_Debug)
		{
			Graphics.Blit(source, destination, GlowMaterial, 1);
		}
		else
		{
			Graphics.Blit(source, destination, GlowMaterial, 2);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	private void SetupDistortion()
	{
		if (!(m_EffectsTexture == null))
		{
			DistortionMaterial.SetTexture("_EffectTex", m_EffectsTexture);
		}
	}

	private Material DistortionMaterialRender(RenderTexture source)
	{
		return DistortionMaterial;
	}

	private void RenderDistortion(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, DistortionMaterialRender(source));
	}
}
