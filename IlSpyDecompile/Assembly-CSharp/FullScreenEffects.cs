using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class FullScreenEffects : MonoBehaviour
{
	private const int NO_WORK_FRAMES_BEFORE_DEACTIVATE = 2;

	private const string BLUR_SHADER_NAME = "Custom/FullScreen/Blur";

	private const string BLUR_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurVignetting";

	private const string BLUR_DESATURATION_SHADER_NAME = "Custom/FullScreen/DesaturationBlur";

	private const string BLEND_SHADER_NAME = "Custom/FullScreen/Blend";

	private const string VIGNETTING_SHADER_NAME = "Custom/FullScreen/Vignetting";

	private const string BLEND_TO_COLOR_SHADER_NAME = "Custom/FullScreen/BlendToColor";

	private const string DESATURATION_SHADER_NAME = "Custom/FullScreen/Desaturation";

	private const string DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/DesaturationVignetting";

	private const string BLUR_DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurDesaturationVignetting";

	private const int BLUR_BUFFER_SIZE = 512;

	private const float BLUR_SECOND_PASS_REDUCTION = 0.5f;

	private const float BLUR_PASS_1_OFFSET = 1f;

	private const float BLUR_PASS_2_OFFSET = 0.4f;

	private const float BLUR_PASS_3_OFFSET = -0.2f;

	public Texture2D m_VignettingMask;

	public Camera m_IgnoreCamera;

	private bool m_BlurEnabled;

	public float m_BlurBlend = 1f;

	private float m_BlurAmount = 2f;

	private float m_BlurDesaturation;

	private float m_BlurBrightness = 1f;

	private bool m_VignettingEnable;

	private float m_VignettingIntensity;

	private bool m_BlendToColorEnable;

	private Color m_BlendColor = Color.white;

	private float m_BlendToColorAmount;

	private bool m_DesaturationEnabled;

	private float m_Desaturation;

	private bool m_WireframeRender;

	private int m_DeactivateFrameCount;

	private Shader m_BlurShader;

	private Shader m_BlurVignettingShader;

	private Shader m_BlurDesaturationShader;

	private Shader m_BlendShader;

	private Shader m_VignettingShader;

	private Shader m_BlendToColorShader;

	private Shader m_DesaturationShader;

	private Shader m_DesaturationVignettingShader;

	private Shader m_BlurDesaturationVignettingShader;

	private Camera m_Camera;

	private CommandBuffer m_EffectBuffer;

	private bool m_InitComplete;

	private Material m_BlurMaterial;

	private Material m_BlurVignettingMaterial;

	private Material m_BlurDesatMaterial;

	private Material m_BlendMaterial;

	private Material m_VignettingMaterial;

	private Material m_BlendToColorMaterial;

	private Material m_DesaturationMaterial;

	private Material m_DesaturationVignettingMaterial;

	private Material m_BlurDesaturationVignettingMaterial;

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

	protected Material blurVignettingMaterial
	{
		get
		{
			if (m_BlurVignettingMaterial == null)
			{
				m_BlurVignettingMaterial = new Material(m_BlurVignettingShader);
				SceneUtils.SetHideFlags(m_BlurVignettingMaterial, HideFlags.DontSave);
			}
			return m_BlurVignettingMaterial;
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

	protected Material VignettingMaterial
	{
		get
		{
			if (m_VignettingMaterial == null)
			{
				m_VignettingMaterial = new Material(m_VignettingShader);
				SceneUtils.SetHideFlags(m_VignettingMaterial, HideFlags.DontSave);
			}
			return m_VignettingMaterial;
		}
	}

	protected Material BlendToColorMaterial
	{
		get
		{
			if (m_BlendToColorMaterial == null)
			{
				m_BlendToColorMaterial = new Material(m_BlendToColorShader);
				SceneUtils.SetHideFlags(m_BlendToColorMaterial, HideFlags.DontSave);
			}
			return m_BlendToColorMaterial;
		}
	}

	protected Material DesaturationMaterial
	{
		get
		{
			if (m_DesaturationMaterial == null)
			{
				m_DesaturationMaterial = new Material(m_DesaturationShader);
				SceneUtils.SetHideFlags(m_DesaturationMaterial, HideFlags.DontSave);
			}
			return m_DesaturationMaterial;
		}
	}

	protected Material DesaturationVignettingMaterial
	{
		get
		{
			if (m_DesaturationVignettingMaterial == null)
			{
				m_DesaturationVignettingMaterial = new Material(m_DesaturationVignettingShader);
				SceneUtils.SetHideFlags(m_DesaturationVignettingMaterial, HideFlags.DontSave);
			}
			return m_DesaturationVignettingMaterial;
		}
	}

	protected Material BlurDesaturationVignettingMaterial
	{
		get
		{
			if (m_BlurDesaturationVignettingMaterial == null)
			{
				m_BlurDesaturationVignettingMaterial = new Material(m_BlurDesaturationVignettingShader);
				SceneUtils.SetHideFlags(m_BlurDesaturationVignettingMaterial, HideFlags.DontSave);
			}
			return m_BlurDesaturationVignettingMaterial;
		}
	}

	public Camera Camera => m_Camera;

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
			UpdateEffectBuffers();
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
			UpdateEffectBuffers();
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
			UpdateEffectBuffers();
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
			UpdateEffectBuffers();
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
			UpdateEffectBuffers();
		}
	}

	public bool VignettingEnable
	{
		get
		{
			return m_VignettingEnable;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			m_VignettingEnable = value;
			UpdateEffectBuffers();
		}
	}

	public float VignettingIntensity
	{
		get
		{
			return m_VignettingIntensity;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_VignettingEnable = true;
			m_VignettingIntensity = value;
			UpdateEffectBuffers();
		}
	}

	public bool BlendToColorEnable
	{
		get
		{
			return m_BlendToColorEnable;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			m_BlendToColorEnable = value;
			UpdateEffectBuffers();
		}
	}

	public Color BlendColor
	{
		get
		{
			return m_BlendColor;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlendToColorEnable = true;
			m_BlendColor = value;
			UpdateEffectBuffers();
		}
	}

	public float BlendToColorAmount
	{
		get
		{
			return m_BlendToColorAmount;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_BlendToColorEnable = true;
			m_BlendToColorAmount = value;
			UpdateEffectBuffers();
		}
	}

	public bool DesaturationEnabled
	{
		get
		{
			return m_DesaturationEnabled;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			m_DesaturationEnabled = value;
			UpdateEffectBuffers();
		}
	}

	public float Desaturation
	{
		get
		{
			return m_Desaturation;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			m_DesaturationEnabled = true;
			m_Desaturation = value;
			UpdateEffectBuffers();
		}
	}

	public bool IsActive
	{
		get
		{
			if (!base.gameObject.activeInHierarchy || !base.enabled)
			{
				return false;
			}
			return HasActiveEffects;
		}
	}

	public bool HasActiveEffects
	{
		get
		{
			if (m_BlurEnabled && m_BlurBlend > 0f)
			{
				return true;
			}
			if (m_VignettingEnable)
			{
				return true;
			}
			if (m_BlendToColorEnable)
			{
				return true;
			}
			if (m_DesaturationEnabled)
			{
				return true;
			}
			if (m_WireframeRender)
			{
				return true;
			}
			return false;
		}
	}

	private void OnEnable()
	{
		if (m_IgnoreCamera != null)
		{
			m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
		}
		m_Camera.forceIntoRenderTexture = true;
	}

	protected void OnDisable()
	{
		SetDefaults();
		if ((bool)m_BlurMaterial)
		{
			Object.Destroy(m_BlurMaterial);
		}
		if ((bool)m_BlurVignettingMaterial)
		{
			Object.Destroy(m_BlurVignettingMaterial);
		}
		if ((bool)m_BlurDesatMaterial)
		{
			Object.Destroy(m_BlurDesatMaterial);
		}
		if ((bool)m_BlendMaterial)
		{
			Object.Destroy(m_BlendMaterial);
		}
		if ((bool)m_VignettingMaterial)
		{
			Object.Destroy(m_VignettingMaterial);
		}
		if ((bool)m_BlendToColorMaterial)
		{
			Object.Destroy(m_BlendToColorMaterial);
		}
		if ((bool)m_DesaturationMaterial)
		{
			Object.Destroy(m_DesaturationMaterial);
		}
		if ((bool)m_DesaturationVignettingMaterial)
		{
			Object.Destroy(m_DesaturationVignettingMaterial);
		}
		if ((bool)m_BlurDesaturationVignettingMaterial)
		{
			Object.Destroy(m_BlurDesaturationVignettingMaterial);
		}
	}

	protected void OnDestroy()
	{
		CheatMgr.Get()?.UnregisterCheatHandler("wireframe", OnProcessCheat_RenderWireframe);
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
			m_IgnoreCamera.depth = m_Camera.depth + 10f;
			m_IgnoreCamera.transform.localPosition = Vector3.zero;
			m_IgnoreCamera.transform.localRotation = Quaternion.identity;
			m_IgnoreCamera.transform.localScale = Vector3.one;
		}
	}

	protected void Start()
	{
		CheatMgr.Get()?.RegisterCheatHandler("wireframe", OnProcessCheat_RenderWireframe);
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("Fullscreen Effects not supported");
			base.enabled = false;
			return;
		}
		if (m_BlurShader == null)
		{
			m_BlurShader = ShaderUtils.FindShader("Custom/FullScreen/Blur");
		}
		if (!m_BlurShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blur");
			base.enabled = false;
		}
		if (!m_BlurShader || !blurMaterial.shader.isSupported)
		{
			Debug.LogError("Fullscreen Effect Shader not supported: Custom/FullScreen/Blur");
			base.enabled = false;
			return;
		}
		if (m_BlurVignettingShader == null)
		{
			m_BlurVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurVignetting");
		}
		if (!m_BlurVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurVignetting");
			base.enabled = false;
		}
		if (m_BlurDesaturationShader == null)
		{
			m_BlurDesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationBlur");
		}
		if (!m_BlurDesaturationShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationBlur");
			base.enabled = false;
		}
		if (m_BlendShader == null)
		{
			m_BlendShader = ShaderUtils.FindShader("Custom/FullScreen/Blend");
		}
		if (!m_BlendShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blend");
			base.enabled = false;
		}
		if (m_VignettingShader == null)
		{
			m_VignettingShader = ShaderUtils.FindShader("Custom/FullScreen/Vignetting");
		}
		if (!m_VignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Vignetting");
			base.enabled = false;
		}
		if (m_BlendToColorShader == null)
		{
			m_BlendToColorShader = ShaderUtils.FindShader("Custom/FullScreen/BlendToColor");
		}
		if (!m_BlendToColorShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlendToColor");
			base.enabled = false;
		}
		if (m_DesaturationShader == null)
		{
			m_DesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/Desaturation");
		}
		if (!m_DesaturationShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Desaturation");
			base.enabled = false;
		}
		if (m_DesaturationVignettingShader == null)
		{
			m_DesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationVignetting");
		}
		if (!m_DesaturationVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationVignetting");
			base.enabled = false;
		}
		if (m_BlurDesaturationVignettingShader == null)
		{
			m_BlurDesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurDesaturationVignetting");
		}
		if (!m_BlurDesaturationVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurDesaturationVignetting");
			base.enabled = false;
		}
		m_InitComplete = true;
		UpdateEffectBuffers();
	}

	private void Update()
	{
		if (!IsActive)
		{
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

	private void SetDefaults()
	{
		m_BlurEnabled = false;
		m_BlurBlend = 1f;
		m_BlurAmount = 2f;
		m_BlurDesaturation = 0f;
		m_BlurBrightness = 1f;
		m_VignettingEnable = false;
		m_VignettingIntensity = 0f;
		m_BlendToColorEnable = false;
		m_BlendColor = Color.white;
		m_BlendToColorAmount = 0f;
		m_DesaturationEnabled = false;
		m_Desaturation = 0f;
		UpdateEffectBuffers();
	}

	public void Disable()
	{
		base.enabled = false;
		SetDefaults();
		FullScreenFXMgr.Get()?.WillReset();
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

	private void OnPreRender()
	{
		if (m_WireframeRender)
		{
			GL.wireframe = true;
		}
	}

	private void OnPostRender()
	{
		GL.wireframe = false;
	}

	private bool OnProcessCheat_RenderWireframe(string func, string[] args, string rawArgs)
	{
		if (m_WireframeRender)
		{
			m_WireframeRender = false;
			return true;
		}
		m_WireframeRender = true;
		base.enabled = true;
		return true;
	}

	private void UpdateEffectBuffers()
	{
		if (!m_InitComplete)
		{
			return;
		}
		m_Camera.RemoveCommandBuffers(CameraEvent.AfterImageEffects);
		m_EffectBuffer = null;
		bool flag = false;
		if (m_BlurEnabled && m_BlurBlend > 0f)
		{
			Material blurDesaturationVignettingMaterial = blurMaterial;
			blurDesaturationVignettingMaterial.SetFloat("_Brightness", m_BlurBrightness);
			if (m_BlurDesaturation > 0f && !m_VignettingEnable)
			{
				blurDesaturationVignettingMaterial = blurDesatMaterial;
				blurDesaturationVignettingMaterial.SetFloat("_Desaturation", m_BlurDesaturation);
			}
			else if (m_VignettingEnable && m_BlurDesaturation == 0f)
			{
				blurDesaturationVignettingMaterial = blurVignettingMaterial;
				blurDesaturationVignettingMaterial.SetFloat("_Amount", m_VignettingIntensity);
				blurDesaturationVignettingMaterial.SetTexture("_MaskTex", m_VignettingMask);
			}
			else if (m_VignettingEnable && m_BlurDesaturation > 0f)
			{
				blurDesaturationVignettingMaterial = BlurDesaturationVignettingMaterial;
				blurDesaturationVignettingMaterial.SetFloat("_Amount", m_VignettingIntensity);
				blurDesaturationVignettingMaterial.SetTexture("_MaskTex", m_VignettingMask);
				blurDesaturationVignettingMaterial.SetFloat("_Desaturation", m_BlurDesaturation);
			}
			m_EffectBuffer = InitBlurBuffer(blurDesaturationVignettingMaterial);
			flag = true;
		}
		if (m_DesaturationEnabled && !flag)
		{
			Material material = DesaturationMaterial;
			if (m_VignettingEnable)
			{
				material = DesaturationVignettingMaterial;
				material.SetFloat("_Amount", m_VignettingIntensity);
				material.SetTexture("_MaskTex", m_VignettingMask);
			}
			material.SetFloat("_Desaturation", m_Desaturation);
			m_EffectBuffer = InitEffectBuffer(material);
			flag = true;
		}
		if (m_VignettingEnable && !flag)
		{
			Material vignettingMaterial = VignettingMaterial;
			vignettingMaterial.SetFloat("_Amount", m_VignettingIntensity);
			vignettingMaterial.SetTexture("_MaskTex", m_VignettingMask);
			m_EffectBuffer = InitEffectBuffer(vignettingMaterial);
			flag = true;
		}
		if (m_BlendToColorEnable && !flag)
		{
			Material blendToColorMaterial = BlendToColorMaterial;
			blendToColorMaterial.SetFloat("_Amount", m_BlendToColorAmount);
			blendToColorMaterial.SetColor("_Color", m_BlendColor);
			m_EffectBuffer = InitEffectBuffer(blendToColorMaterial);
			flag = true;
		}
		if (m_EffectBuffer != null)
		{
			m_Camera.AddCommandBuffer(CameraEvent.AfterImageEffects, m_EffectBuffer);
		}
	}

	private CommandBuffer InitEffectBuffer(Material material, CommandBuffer cb = null)
	{
		if (material == null)
		{
			return null;
		}
		if (cb == null)
		{
			cb = new CommandBuffer();
		}
		cb.name = "FullScreenEffectBuffer";
		int num = Shader.PropertyToID("_ScreenCopyTexture");
		cb.GetTemporaryRT(num, -1, -1, 0, FilterMode.Bilinear);
		cb.SetRenderTarget(num, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
		cb.Blit(BuiltinRenderTextureType.CameraTarget, num);
		cb.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
		cb.Blit(num, BuiltinRenderTextureType.CurrentActive, material);
		cb.ReleaseTemporaryRT(num);
		return cb;
	}

	private CommandBuffer InitBlurBuffer(Material blurMat)
	{
		if (blurMat == null)
		{
			return null;
		}
		CommandBuffer commandBuffer = new CommandBuffer();
		commandBuffer.name = "FullScreenEffectBuffer";
		float sizeX = Screen.width;
		float sizeY = Screen.height;
		CalcTextureSize(Screen.width, Screen.height, 512, out sizeX, out sizeY);
		int num = Shader.PropertyToID("_Blur1");
		int num2 = Shader.PropertyToID("_Blur2");
		int num3 = Shader.PropertyToID("_ScreenCopyTexture");
		commandBuffer.GetTemporaryRT(num3, -1, -1, 0, FilterMode.Bilinear);
		commandBuffer.GetTemporaryRT(num, (int)sizeX, (int)sizeY, 0, FilterMode.Bilinear);
		commandBuffer.GetTemporaryRT(num2, (int)(sizeX * 0.5f), (int)(sizeY * 0.5f), 0, FilterMode.Bilinear);
		commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, num3);
		commandBuffer.SetGlobalFloat("_BlurOffset", 1f);
		commandBuffer.SetGlobalFloat("_FirstPass", 1f);
		commandBuffer.Blit(num3, num, blurMat);
		commandBuffer.SetGlobalFloat("_BlurOffset", 0.4f);
		commandBuffer.SetGlobalFloat("_FirstPass", 0f);
		commandBuffer.Blit(num, num2, blurMat);
		commandBuffer.ReleaseTemporaryRT(num);
		commandBuffer.SetGlobalFloat("_BlurOffset", -0.2f);
		commandBuffer.SetGlobalFloat("_FirstPass", 0f);
		if (m_BlurBlend >= 1f)
		{
			commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			commandBuffer.Blit(num2, BuiltinRenderTextureType.CurrentActive, blurMat);
			commandBuffer.ReleaseTemporaryRT(num2);
		}
		else
		{
			int num4 = Shader.PropertyToID("_Blend");
			commandBuffer.GetTemporaryRT(num4, 512, 512, 0, FilterMode.Bilinear);
			commandBuffer.Blit(num2, num4, blurMat);
			commandBuffer.ReleaseTemporaryRT(num2);
			commandBuffer.SetGlobalFloat("_Amount", m_BlurBlend);
			commandBuffer.SetGlobalTexture("_BlendTex", num4);
			commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			commandBuffer.Blit(num3, BuiltinRenderTextureType.CurrentActive, m_BlendMaterial);
			commandBuffer.ReleaseTemporaryRT(num4);
		}
		commandBuffer.ReleaseTemporaryRT(num3);
		return commandBuffer;
	}
}
