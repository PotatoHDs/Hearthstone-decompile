using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
	private const int NO_WORK_FRAMES_BEFORE_DEACTIVATE = 2;

	private const string BLUR_SHADER_NAME = "Custom/FullScreen/Blur";

	private const string BLUR_DESATURATION_SHADER_NAME = "Custom/FullScreen/DesaturationBlur";

	private const string BLEND_SHADER_NAME = "Custom/FullScreen/Blend";

	private const int BLUR_BUFFER_SIZE = 512;

	private const float BLUR_SECOND_PASS_REDUCTION = 0.5f;

	private const float BLUR_PASS_1_OFFSET = 1f;

	private const float BLUR_PASS_2_OFFSET = 0.4f;

	private const float BLUR_PASS_3_OFFSET = -0.2f;

	public Camera m_IgnoreCamera;

	private int m_LowQualityFreezeBufferSize = 512;

	private bool m_BlurEnabled;

	public float m_BlurBlend = 1f;

	private float m_BlurAmount = 2f;

	private float m_BlurDesaturation;

	private float m_BlurBrightness = 1f;

	private int m_DeactivateFrameCount;

	private Shader m_BlurShader;

	private Shader m_BlurDesaturationShader;

	private Shader m_BlendShader;

	private bool m_FrozenState;

	private bool m_CaptureFrozenImage;

	private Texture2D m_FrozenScreenTexture;

	private UniversalInputManager m_UniversalInputManager;

	private Camera m_Camera;

	private Material m_BlurMaterial;

	private Material m_BlurDesatMaterial;

	private Material m_BlendMaterial;

	protected Material blurMaterial
	{
		get
		{
			if (m_BlurMaterial == null)
			{
				m_BlurMaterial = new Material(m_BlurShader);
				SceneUtils.SetHideFlags(m_BlurMaterial, HideFlags.DontSave);
			}
			return m_BlurMaterial;
		}
	}

	protected Material blurDesatMaterial
	{
		get
		{
			if (m_BlurDesatMaterial == null)
			{
				m_BlurDesatMaterial = new Material(m_BlurDesaturationShader);
				SceneUtils.SetHideFlags(m_BlurDesatMaterial, HideFlags.DontSave);
			}
			return m_BlurDesatMaterial;
		}
	}

	protected Material blendMaterial
	{
		get
		{
			if (m_BlendMaterial == null)
			{
				m_BlendMaterial = new Material(m_BlendShader);
				SceneUtils.SetHideFlags(m_BlendMaterial, HideFlags.DontSave);
			}
			return m_BlendMaterial;
		}
	}

	public bool BlurEnabled
	{
		get
		{
			return m_BlurEnabled;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			m_BlurEnabled = value;
		}
	}

	public float BlurBlend
	{
		get
		{
			return m_BlurBlend;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlurEnabled = true;
			m_BlurBlend = value;
		}
	}

	public float BlurAmount
	{
		get
		{
			return m_BlurAmount;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlurEnabled = true;
			m_BlurAmount = value;
		}
	}

	public float BlurDesaturation
	{
		get
		{
			return m_BlurDesaturation;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlurEnabled = true;
			m_BlurDesaturation = value;
		}
	}

	public float BlurBrightness
	{
		get
		{
			return m_BlurBrightness;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlurEnabled = true;
			m_BlurBrightness = value;
		}
	}

	private void OnEnable()
	{
		if (m_IgnoreCamera != null)
		{
			m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
		}
	}

	protected void OnDisable()
	{
		if (m_FrozenState)
		{
			Unfreeze();
		}
		SetDefaults();
		if ((bool)m_BlurMaterial)
		{
			Object.Destroy(m_BlurMaterial);
		}
		if ((bool)m_BlurDesatMaterial)
		{
			Object.Destroy(m_BlurDesatMaterial);
		}
		if ((bool)m_BlendMaterial)
		{
			Object.Destroy(m_BlendMaterial);
		}
	}

	protected void Awake()
	{
		m_Camera = GetComponent<Camera>();
		if (m_IgnoreCamera != null)
		{
			int cullingMask = m_IgnoreCamera.cullingMask;
			CameraClearFlags clearFlags = m_IgnoreCamera.clearFlags;
			m_IgnoreCamera.CopyFrom(m_Camera);
			m_IgnoreCamera.cullingMask = cullingMask;
			m_IgnoreCamera.clearFlags = clearFlags;
			m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
			m_IgnoreCamera.depth = m_Camera.depth + 1f;
			m_IgnoreCamera.transform.localPosition = Vector3.zero;
			m_IgnoreCamera.transform.localRotation = Quaternion.identity;
			m_IgnoreCamera.transform.localScale = Vector3.one;
		}
	}

	protected void Start()
	{
		base.gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("FreezeFrame not supported");
			base.enabled = false;
			return;
		}
		if (m_BlurShader == null)
		{
			m_BlurShader = ShaderUtils.FindShader("Custom/FullScreen/Blur");
		}
		if (!m_BlurShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/Blur");
			base.enabled = false;
		}
		if (!m_BlurShader || !blurMaterial.shader.isSupported)
		{
			Debug.LogError("FreezeFrame Effect Shader not supported: Custom/FullScreen/Blur");
			base.enabled = false;
			return;
		}
		if (m_BlurDesaturationShader == null)
		{
			m_BlurDesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationBlur");
		}
		if (!m_BlurDesaturationShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/DesaturationBlur");
			base.enabled = false;
		}
		if (m_BlendShader == null)
		{
			m_BlendShader = ShaderUtils.FindShader("Custom/FullScreen/Blend");
		}
		if (!m_BlendShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/Blend");
			base.enabled = false;
		}
	}

	private void SetDefaults()
	{
		m_BlurEnabled = false;
		m_BlurBlend = 1f;
		m_BlurAmount = 2f;
		m_BlurDesaturation = 0f;
		m_BlurBrightness = 1f;
	}

	public void Disable()
	{
		base.enabled = false;
		SetDefaults();
		FullScreenFXMgr.Get()?.WillReset();
	}

	[ContextMenu("Freeze")]
	public void Freeze()
	{
		base.enabled = true;
		if (!m_FrozenState)
		{
			m_BlurEnabled = true;
			m_BlurBlend = 1f;
			m_BlurAmount = 1.5f;
			m_BlurDesaturation = 0f;
			m_BlurBrightness = 1f;
			m_CaptureFrozenImage = true;
			int lowQualityFreezeBufferSize = m_LowQualityFreezeBufferSize;
			int lowQualityFreezeBufferSize2 = m_LowQualityFreezeBufferSize;
			if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
			{
				lowQualityFreezeBufferSize = m_LowQualityFreezeBufferSize;
				lowQualityFreezeBufferSize2 = m_LowQualityFreezeBufferSize;
			}
			else
			{
				lowQualityFreezeBufferSize = Screen.currentResolution.width;
				lowQualityFreezeBufferSize2 = Screen.currentResolution.height;
			}
			m_FrozenScreenTexture = new Texture2D(lowQualityFreezeBufferSize, lowQualityFreezeBufferSize2, TextureFormat.RGB24, mipChain: false, linear: true);
			m_FrozenScreenTexture.filterMode = FilterMode.Point;
			m_FrozenScreenTexture.wrapMode = TextureWrapMode.Clamp;
		}
	}

	[ContextMenu("Unfreeze")]
	public void Unfreeze()
	{
		m_BlurEnabled = false;
		m_BlurBlend = 0f;
		m_FrozenState = false;
		if (m_FrozenScreenTexture != null)
		{
			Object.DestroyImmediate(m_FrozenScreenTexture);
			m_FrozenScreenTexture = null;
		}
		Disable();
	}

	public bool isActive()
	{
		if (!base.enabled)
		{
			return false;
		}
		if (m_FrozenState)
		{
			return true;
		}
		return false;
	}

	private void Blur(RenderTexture source, RenderTexture destination, Material blurMat)
	{
		float sizeX = source.width;
		float sizeY = source.height;
		CalcTextureSize(source.width, source.height, 512, out sizeX, out sizeY);
		RenderTexture temporary = RenderTexture.GetTemporary((int)sizeX, (int)sizeY, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)(sizeX * 0.5f), (int)(sizeY * 0.5f), 0);
		temporary.wrapMode = TextureWrapMode.Clamp;
		temporary2.wrapMode = TextureWrapMode.Clamp;
		blurMat.SetFloat("_BlurOffset", 1f);
		blurMat.SetFloat("_FirstPass", 1f);
		Graphics.Blit(source, temporary, blurMat);
		blurMat.SetFloat("_BlurOffset", 0.4f);
		blurMat.SetFloat("_FirstPass", 0f);
		Graphics.Blit(temporary, temporary2, blurMat);
		blurMat.SetFloat("_BlurOffset", -0.2f);
		blurMat.SetFloat("_FirstPass", 0f);
		Graphics.Blit(temporary2, destination, blurMat);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	private void CalcTextureSize(int currentWidth, int currentHeight, int resolution, out float sizeX, out float sizeY)
	{
		float num = currentWidth;
		float num2 = currentHeight;
		float num3 = resolution;
		if (num > num2)
		{
			sizeX = num3;
			sizeY = num3 * (num2 / num);
		}
		else
		{
			sizeX = num3 * (num / num2);
			sizeY = num3;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (source == null || source.width == 0 || source.height == 0)
		{
			return;
		}
		if (m_CaptureFrozenImage && !m_FrozenState)
		{
			Material material = blurMaterial;
			material.SetFloat("_Brightness", m_BlurBrightness);
			if (m_BlurDesaturation > 0f)
			{
				material = blurDesatMaterial;
				material.SetFloat("_Desaturation", m_BlurDesaturation);
			}
			int lowQualityFreezeBufferSize = m_LowQualityFreezeBufferSize;
			int lowQualityFreezeBufferSize2 = m_LowQualityFreezeBufferSize;
			if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
			{
				lowQualityFreezeBufferSize = m_LowQualityFreezeBufferSize;
				lowQualityFreezeBufferSize2 = m_LowQualityFreezeBufferSize;
			}
			else
			{
				lowQualityFreezeBufferSize = Screen.currentResolution.width;
				lowQualityFreezeBufferSize2 = Screen.currentResolution.height;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(lowQualityFreezeBufferSize, lowQualityFreezeBufferSize2);
			Blur(source, temporary, material);
			RenderTexture.active = temporary;
			m_FrozenScreenTexture.ReadPixels(new Rect(0f, 0f, lowQualityFreezeBufferSize, lowQualityFreezeBufferSize2), 0, 0, recalculateMipMaps: false);
			m_FrozenScreenTexture.Apply();
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
			m_CaptureFrozenImage = false;
			m_FrozenState = true;
			m_DeactivateFrameCount = 0;
		}
		if (m_FrozenState)
		{
			if ((bool)m_FrozenScreenTexture)
			{
				Material material2 = blendMaterial;
				material2.SetFloat("_Amount", 1f);
				material2.SetTexture("_BlendTex", m_FrozenScreenTexture);
				if (QualitySettings.antiAliasing > 0)
				{
					material2.SetFloat("_Flip", 1f);
				}
				else
				{
					material2.SetFloat("_Flip", 0f);
				}
				if (destination != null)
				{
					destination.DiscardContents();
				}
				Graphics.Blit(source, destination, material2);
				m_DeactivateFrameCount = 0;
				return;
			}
			Debug.LogWarning("m_FrozenScreenTexture is null. FreezeFrame disabled");
			m_FrozenState = false;
		}
		if (!m_FrozenState)
		{
			Material material3 = blendMaterial;
			material3.SetFloat("_Amount", 0f);
			material3.SetTexture("_BlendTex", null);
			Graphics.Blit(source, destination, material3);
			if (m_DeactivateFrameCount > 2)
			{
				m_DeactivateFrameCount = 0;
				Disable();
			}
			else
			{
				m_DeactivateFrameCount++;
			}
		}
	}
}
