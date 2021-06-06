using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class RenderToTexture2 : MonoBehaviour
{
	public enum RenderToTextureMaterial
	{
		Custom,
		Transparent,
		Additive,
		Bloom,
		AlphaClip,
		AlphaClipBloom
	}

	public enum AlphaClipShader
	{
		Standard,
		ColorGradient
	}

	public enum BloomRenderType
	{
		Color,
		Alpha
	}

	public enum BloomBlendType
	{
		Additive,
		Transparent
	}

	private const string BLUR_SHADER_NAME = "Hidden/R2TBlur";

	private const string BLUR_ALPHA_SHADER_NAME = "Hidden/R2TAlphaBlur";

	private const string ALPHA_BLEND_SHADER_NAME = "Hidden/R2TColorAlphaCombine";

	private const string ALPHA_BLEND_ADD_SHADER_NAME = "Hidden/R2TColorAlphaCombineAdd";

	private const string ALPHA_FILL_SHADER_NAME = "Custom/AlphaFillOpaque";

	private const string BLOOM_SHADER_NAME = "Hidden/R2TBloom";

	private const string BLOOM_ALPHA_SHADER_NAME = "Hidden/R2TBloomAlpha";

	private const string ADDITIVE_SHADER_NAME = "Hidden/R2TAdditive";

	private const string TRANSPARENT_SHADER_NAME = "Hidden/R2TTransparent";

	private const string ALPHA_CLIP_SHADER_NAME = "Hidden/R2TAlphaClip";

	private const string ALPHA_CLIP_BLOOM_SHADER_NAME = "Hidden/R2TAlphaClipBloom";

	private const string ALPHA_CLIP_GRADIENT_SHADER_NAME = "Hidden/R2TAlphaClipGradient";

	private const RenderTextureFormat ALPHA_TEXTURE_FORMAT = RenderTextureFormat.ARGB32;

	private const float RENDER_SIZE_QUALITY_LOW = 0.75f;

	private const float RENDER_SIZE_QUALITY_MEDIUM = 1f;

	private const float RENDER_SIZE_QUALITY_HIGH = 1.5f;

	private readonly Vector2[] PLANE_UVS = new Vector2[4]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	private readonly Vector3[] PLANE_NORMALS = new Vector3[4]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	private readonly int[] PLANE_TRIANGLES = new int[6] { 3, 1, 2, 2, 1, 0 };

	[Tooltip("The highest level FX object that you want to render onto a texture.")]
	public GameObject m_ObjectToRender;

	[Tooltip("An alpha object to render. If an object is provided, alpha passes will happen regardless of other settings. You still must provide a normal ObjectToRender as well.")]
	public GameObject m_AlphaObjectToRender;

	[Tooltip("This will ensure that the texture is rendered every update, otherwise the render texture will only be rendered on generation and then when rendering or updates are manually called.")]
	public bool m_RealtimeRender = true;

	[Tooltip("Render the texture with an alpha pass. You do not have to supply an \"Alpha Object to Render\"")]
	public bool m_RenderMeshAsAlpha;

	[Tooltip("Render the object with an alpha pass and an alpha blend shader. You do not have to supply an \"Alpha object to Render\"")]
	public bool m_OpaqueObjectAlphaFill;

	[Tooltip("Determines what types of default materials are created. To get a better idea, search the project for RT2 shaders. Only use \"Custom\" if you are supplying a custom material below, as Custom materials will ignore most alpha and bloom settings below.")]
	public RenderToTextureMaterial m_RenderMaterial = RenderToTextureMaterial.Transparent;

	[Tooltip("The format of the RenderTexture that will be created.")]
	public RenderTextureFormat m_RenderTextureFormat;

	[Tooltip("A custom material. If none is supplied, a default material will be created. This is an easy way to provide custom material settings and shaders.")]
	public Material m_Material;

	[Space(10f)]
	[Tooltip("Check this true if you are not providing a custom mesh to apply the RenderTexture to. This will create a simple quad the texture will be projected upon.")]
	public bool m_CreateRenderPlane = true;

	[Tooltip("A custom game object that the RenderTexture will render to instead of a default mesh plane. If a game object is provided, this will OVERRIDE \"Create Render Plane\". If you see issues with Bloom settings, try using a render plane.")]
	public GameObject m_RenderToObject;

	[Tooltip("The name of a custom shader to be used for the material of the RenderTexture.")]
	public string m_ShaderTextureName = string.Empty;

	[Space(10f)]
	[Tooltip("The maximum dimension that the render texture will be on any side. Keep this to a power of 2.")]
	public int m_Resolution = 128;

	[Tooltip("The width of the render texture in pixels adjusted by resolution.")]
	public float m_Width = 1f;

	[Tooltip("The height of the render texture in pixels adjusted by resolution.")]
	public float m_Height = 1f;

	[Tooltip("A setting to use a uniform world scale instead of a lossy scale when calculating the size for textures and generated quads. You will need to disable and re-enable to see changes in this setting.")]
	public bool m_UniformWorldScale;

	[Space(10f)]
	[Tooltip("The intensity level of the bloom.")]
	public float m_BloomIntensity;

	[Tooltip("The minimum value at which bloom will start being applied.")]
	public float m_BloomThreshold = 0.7f;

	[Tooltip("Sets the amount of blur that will be applied to the texture in bloom processes.")]
	public float m_BloomBlur = 0.3f;

	[Tooltip("The size of the bloom in comparison to the RenderTexture's size. 1.0 is 100% of the size of the RenderTexture.")]
	public float m_BloomResolutionRatio = 0.5f;

	[Tooltip("Whether or not the bloom renders with alpha or a solid color.")]
	public BloomRenderType m_BloomRenderType;

	[Tooltip("A decimal value for the alpha intensity of the bloom shader. Only works if Bloom Render Type is set to \"Alpha\".")]
	public float m_BloomAlphaIntensity = 1f;

	[Tooltip("The blend type for the bloom. Use Additive for a whiter, intense, more blown out look, and Transparent for a gentler bloom.")]
	public BloomBlendType m_BloomBlend;

	[Tooltip("A color that will be applied to the bloom.")]
	public Color m_BloomColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	[Space(10f)]
	[Tooltip("Set the style of the Alpha clip shader. If you use gradient, you must provide an \"Alpha Clip Gradient Map\" below. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public AlphaClipShader m_AlphaClipRenderStyle;

	[Tooltip("A gradient map to be used if the \"Alpha Clip Render Style\" is set to \"Color Gradient\". \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public Texture2D m_AlphaClipGradientMap;

	[Tooltip("The Cutoff for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClip = 15f;

	[Tooltip("The Intensity for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClipIntensity = 1.5f;

	[Tooltip("The Alpha Intensity for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClipAlphaIntensity = 1f;

	[Tooltip("The amount of blur to use when sampling. \"Opaque Object Alpha Fill\" or \"Render Mesh as Alpha\" must be turned on, or an \"Alpha Object to Render\" must be provided.")]
	public float m_BlurAmount;

	[Tooltip("Apply a shader material that will blur only alpha settings. You do not need any other settings turned on for this to take affect.")]
	public bool m_BlurAlphaOnly;

	[Tooltip("A tint that will be applied to the RenderTexture mesh plane material.")]
	public Color m_TintColor = Color.white;

	[Space(10f)]
	[Tooltip("An offset for setting where the materials of the RenderTexture should fall in the rendering order. '3000' is the value for the 'Transparent' queue level.")]
	public int m_RenderQueueOffset = 3000;

	[Tooltip("A setting for the render order that comes after the Render Queue Offset. If working with multiple RenderToTexture2's, you can use this to manually adjust their render order.")]
	public int m_RenderQueue;

	[Space(10f)]
	[Tooltip("The closest point to the camera at which drawing will occur.")]
	public float m_NearClip = -0.1f;

	[Tooltip("The furthest distance from the camera at which drawing will occur.")]
	public float m_FarClip = 0.5f;

	[Tooltip("Set the Clear Flags for the camera that will be used to render the texture. Recommended to keep this on Depth.")]
	public CameraClearFlags m_ClearFlags = CameraClearFlags.Depth;

	[Tooltip("Set the background color for the camera that will be used to render the texture.")]
	public Color m_ClearColor = Color.clear;

	[Tooltip("A replacement shader that will be applied to the main camera projecting onto the RenderTexture.")]
	public Shader m_ReplacementShader;

	[Tooltip("A replacement tag filter that the replacement shader will use when applied to the main camera. See Unity Docs for Replacement Tag info.")]
	public string m_ReplacementTag;

	[Tooltip("Replacement tag filters for the Alpha Camera replacement shader. If OpaqueObjectAlphaFill is checked, this will be overwritten as \"RenderType\". See Unity Docs for Replacement Tag info.")]
	public string m_AlphaReplacementTag;

	[Tooltip("Allows a custom value for the camera's Orthographics size. See Unity docs for camera.orthographicSize.")]
	public float m_OverrideCameraSize;

	[Tooltip("The render camera's transform's base position offset.")]
	public Vector3 m_PositionOffset = Vector3.zero;

	[Tooltip("The additive offset between the camera and the object it's rendering. This is added after the above position offset.")]
	public Vector3 m_CameraOffset = Vector3.zero;

	[Tooltip("The layer mask that will be used as the culling mask for the camera. Recommended: InvisibleRender.")]
	public LayerMask m_LayerMask = -1;

	[Space(10f)]
	[Tooltip("Defers rendering until the LateUpdate function instead of Update. Also will skip some Bloom process effects.")]
	public bool m_LateUpdate;

	[Tooltip("Do a rendering pass when this component runs its Start() function.")]
	public bool m_RenderOnStart = true;

	[Tooltip("Do a rendering pass when this component is enabled, either in Editor or in Game.")]
	public bool m_RenderOnEnable = true;

	private bool m_renderEnabled = true;

	private bool m_init;

	private float m_WorldWidth;

	private float m_WorldHeight;

	private Vector3 m_WorldScale;

	private GameObject m_CameraGO;

	private Camera m_Camera;

	private GameObject m_AlphaCameraGO;

	private Camera m_AlphaCamera;

	private GameObject m_BloomCaptureCameraGO;

	private Camera m_BloomCaptureCamera;

	private RenderTexture m_RenderTexture;

	private RenderTexture m_AlphaRenderTexture;

	private RenderTexture m_BloomRenderTexture;

	private RenderTexture m_BloomRenderBuffer1;

	private RenderTexture m_BloomRenderBuffer2;

	private GameObject m_GameObject;

	private GameObject m_BloomPlaneGameObject;

	private Vector3 m_ObjectToRenderOrgPosition = Vector3.zero;

	private bool m_isDirty;

	private Shader m_AlphaFillShader;

	private RenderToTextureMaterial m_PreviousRenderMaterial;

	private List<Renderer> m_OpaqueObjectAlphaFillTransparent;

	private List<UberText> m_OpaqueObjectAlphaFillUberText;

	private HideFlags m_DevFlag = HideFlags.HideAndDontSave;

	private Shader m_AlphaBlendShader;

	private Material m_AlphaBlendMaterial;

	private Shader m_AlphaBlendAddShader;

	private Material m_AlphaBlendAddMaterial;

	private Shader m_AdditiveShader;

	private Material m_AdditiveMaterial;

	private Shader m_BloomShader;

	private Material m_BloomMaterial;

	private Shader m_BloomShaderAlpha;

	private Material m_BloomMaterialAlpha;

	private Shader m_BlurShader;

	private Material m_BlurMaterial;

	private Shader m_AlphaBlurShader;

	private Material m_AlphaBlurMaterial;

	private Shader m_TransparentShader;

	private Material m_TransparentMaterial;

	private Shader m_AlphaClipShader;

	private Material m_AlphaClipMaterial;

	private Shader m_AlphaClipBloomShader;

	private Material m_AlphaClipBloomMaterial;

	private Shader m_AlphaClipGradientShader;

	private Material m_AlphaClipGradientMaterial;

	protected Material AlphaBlendMaterial
	{
		get
		{
			if (m_AlphaBlendMaterial == null)
			{
				if (m_AlphaBlendShader == null)
				{
					m_AlphaBlendShader = ShaderUtils.FindShader("Hidden/R2TColorAlphaCombine");
					if (!m_AlphaBlendShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TColorAlphaCombine");
					}
				}
				m_AlphaBlendMaterial = new Material(m_AlphaBlendShader);
				SceneUtils.SetHideFlags(m_AlphaBlendMaterial, HideFlags.DontSave);
			}
			return m_AlphaBlendMaterial;
		}
	}

	protected Material AlphaBlendAddMaterial
	{
		get
		{
			if (m_AlphaBlendAddMaterial == null)
			{
				if (m_AlphaBlendAddShader == null)
				{
					m_AlphaBlendAddShader = ShaderUtils.FindShader("Hidden/R2TColorAlphaCombineAdd");
					if (!m_AlphaBlendAddShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TColorAlphaCombineAdd");
					}
				}
				m_AlphaBlendAddMaterial = new Material(m_AlphaBlendAddShader);
				SceneUtils.SetHideFlags(m_AlphaBlendAddMaterial, HideFlags.DontSave);
			}
			return m_AlphaBlendAddMaterial;
		}
	}

	protected Material AdditiveMaterial
	{
		get
		{
			if (m_AdditiveMaterial == null)
			{
				if (m_AdditiveShader == null)
				{
					m_AdditiveShader = ShaderUtils.FindShader("Hidden/R2TAdditive");
					if (!m_AdditiveShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAdditive");
					}
				}
				m_AdditiveMaterial = new Material(m_AdditiveShader);
				SceneUtils.SetHideFlags(m_AdditiveMaterial, HideFlags.DontSave);
			}
			return m_AdditiveMaterial;
		}
	}

	protected Material BloomMaterial
	{
		get
		{
			if (m_BloomMaterial == null)
			{
				if (m_BloomShader == null)
				{
					m_BloomShader = ShaderUtils.FindShader("Hidden/R2TBloom");
					if (!m_BloomShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBloom");
					}
				}
				m_BloomMaterial = new Material(m_BloomShader);
				SceneUtils.SetHideFlags(m_BloomMaterial, HideFlags.DontSave);
			}
			return m_BloomMaterial;
		}
	}

	protected Material BloomMaterialAlpha
	{
		get
		{
			if (m_BloomMaterialAlpha == null)
			{
				if (m_BloomShaderAlpha == null)
				{
					m_BloomShaderAlpha = ShaderUtils.FindShader("Hidden/R2TBloomAlpha");
					if (!m_BloomShaderAlpha)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBloomAlpha");
					}
				}
				m_BloomMaterialAlpha = new Material(m_BloomShaderAlpha);
				SceneUtils.SetHideFlags(m_BloomMaterialAlpha, HideFlags.DontSave);
			}
			return m_BloomMaterialAlpha;
		}
	}

	protected Material BlurMaterial
	{
		get
		{
			if (m_BlurMaterial == null)
			{
				if (m_BlurShader == null)
				{
					m_BlurShader = ShaderUtils.FindShader("Hidden/R2TBlur");
					if (!m_BlurShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBlur");
					}
				}
				m_BlurMaterial = new Material(m_BlurShader);
				SceneUtils.SetHideFlags(m_BlurMaterial, HideFlags.DontSave);
			}
			return m_BlurMaterial;
		}
	}

	protected Material AlphaBlurMaterial
	{
		get
		{
			if (m_AlphaBlurMaterial == null)
			{
				if (m_AlphaBlurShader == null)
				{
					m_AlphaBlurShader = ShaderUtils.FindShader("Hidden/R2TAlphaBlur");
					if (!m_AlphaBlurShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaBlur");
					}
				}
				m_AlphaBlurMaterial = new Material(m_AlphaBlurShader);
				SceneUtils.SetHideFlags(m_AlphaBlurMaterial, HideFlags.DontSave);
			}
			return m_AlphaBlurMaterial;
		}
	}

	protected Material TransparentMaterial
	{
		get
		{
			if (m_TransparentMaterial == null)
			{
				if (m_TransparentShader == null)
				{
					m_TransparentShader = ShaderUtils.FindShader("Hidden/R2TTransparent");
					if (!m_TransparentShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TTransparent");
					}
				}
				m_TransparentMaterial = new Material(m_TransparentShader);
				SceneUtils.SetHideFlags(m_TransparentMaterial, HideFlags.DontSave);
			}
			return m_TransparentMaterial;
		}
	}

	protected Material AlphaClipMaterial
	{
		get
		{
			if (m_AlphaClipMaterial == null)
			{
				if (m_AlphaClipShader == null)
				{
					m_AlphaClipShader = ShaderUtils.FindShader("Hidden/R2TAlphaClip");
					if (!m_AlphaClipShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClip");
					}
				}
				m_AlphaClipMaterial = new Material(m_AlphaClipShader);
				SceneUtils.SetHideFlags(m_AlphaClipMaterial, HideFlags.DontSave);
			}
			return m_AlphaClipMaterial;
		}
	}

	protected Material AlphaClipBloomMaterial
	{
		get
		{
			if (m_AlphaClipBloomMaterial == null)
			{
				if (m_AlphaClipBloomShader == null)
				{
					m_AlphaClipBloomShader = ShaderUtils.FindShader("Hidden/R2TAlphaClipBloom");
					if (!m_AlphaClipBloomShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClipBloom");
					}
				}
				m_AlphaClipBloomMaterial = new Material(m_AlphaClipBloomShader);
				SceneUtils.SetHideFlags(m_AlphaClipBloomMaterial, HideFlags.DontSave);
			}
			return m_AlphaClipBloomMaterial;
		}
	}

	protected Material AlphaClipGradientMaterial
	{
		get
		{
			if (m_AlphaClipGradientMaterial == null)
			{
				if (m_AlphaClipGradientShader == null)
				{
					m_AlphaClipGradientShader = ShaderUtils.FindShader("Hidden/R2TAlphaClipGradient");
					if (!m_AlphaClipGradientShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClipGradient");
					}
				}
				m_AlphaClipGradientMaterial = new Material(m_AlphaClipGradientShader);
				SceneUtils.SetHideFlags(m_AlphaClipGradientMaterial, HideFlags.DontSave);
			}
			return m_AlphaClipGradientMaterial;
		}
	}

	private void Awake()
	{
		m_AlphaFillShader = ShaderUtils.FindShader("Custom/AlphaFillOpaque");
		if (!m_AlphaFillShader)
		{
			Debug.LogError("Failed to load RenderToTexture Shader: Custom/AlphaFillOpaque");
		}
		if (m_Material != null)
		{
			m_Material = Object.Instantiate(m_Material);
		}
	}

	private void Start()
	{
		if (m_RenderOnStart)
		{
			m_isDirty = true;
		}
		Init();
	}

	private void Update()
	{
		if (m_renderEnabled)
		{
			if ((bool)m_RenderTexture && !m_RenderTexture.IsCreated())
			{
				Log.Graphics.Print("RenderToTexture Texture lost. Render Called");
				m_isDirty = true;
				m_RenderTexture.name = "Rendered Texture";
				RenderTex();
			}
			else if (!m_LateUpdate && (m_RealtimeRender || m_isDirty))
			{
				RenderTex();
			}
		}
	}

	private void LateUpdate()
	{
		if (!m_renderEnabled)
		{
			return;
		}
		if (m_LateUpdate)
		{
			if (m_RealtimeRender || m_isDirty)
			{
				RenderTex();
			}
		}
		else if (m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom || m_RenderMaterial == RenderToTextureMaterial.Bloom)
		{
			RenderBloom();
		}
		else if ((bool)m_BloomPlaneGameObject)
		{
			Object.DestroyImmediate(m_BloomPlaneGameObject);
		}
	}

	private void OnApplicationFocus(bool state)
	{
		if ((bool)m_RenderTexture && state)
		{
			m_isDirty = true;
			RenderTex();
		}
	}

	private void OnDrawGizmos()
	{
		if (base.enabled)
		{
			if (m_FarClip < 0f)
			{
				m_FarClip = 0f;
			}
			if (m_NearClip > 0f)
			{
				m_NearClip = 0f;
			}
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Vector3 vector = new Vector3(0f, (0f - m_NearClip) * 0.5f, 0f);
			Gizmos.color = new Color(0.1f, 0.5f, 0.7f, 0.8f);
			Gizmos.DrawWireCube(vector + m_PositionOffset, new Vector3(m_Width, 0f - m_NearClip, m_Height));
			Gizmos.color = new Color(0.2f, 0.2f, 0.9f, 0.8f);
			Gizmos.DrawWireCube(new Vector3(0f, (0f - m_FarClip) * 0.5f, 0f) + m_PositionOffset, new Vector3(m_Width, 0f - m_FarClip, m_Height));
			Gizmos.color = new Color(0.8f, 0.8f, 1f, 1f);
			Gizmos.DrawWireCube(m_PositionOffset, new Vector3(m_Width, 0f, m_Height));
			Gizmos.matrix = Matrix4x4.identity;
		}
	}

	private void OnDisable()
	{
		CleanUp();
		if ((bool)m_ObjectToRender)
		{
			m_ObjectToRender.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
		if (m_CreateRenderPlane)
		{
			Object.Destroy(m_GameObject);
		}
		if ((bool)m_BloomPlaneGameObject)
		{
			Object.Destroy(m_BloomPlaneGameObject);
		}
		if ((bool)m_BloomCaptureCameraGO)
		{
			Object.Destroy(m_BloomCaptureCameraGO);
		}
		ReleaseTexture();
		if ((bool)m_Camera)
		{
			m_Camera.enabled = false;
		}
		if ((bool)m_AlphaCamera)
		{
			m_AlphaCamera.enabled = false;
		}
		m_init = false;
		m_isDirty = true;
	}

	private void OnDestroy()
	{
		CleanUp();
	}

	private void OnEnable()
	{
		if (m_RenderOnEnable)
		{
			RenderTex();
		}
	}

	public RenderTexture Render()
	{
		m_isDirty = true;
		return m_RenderTexture;
	}

	public RenderTexture RenderNow()
	{
		RenderTex();
		return m_RenderTexture;
	}

	public void ForceTextureRebuild()
	{
		if (base.enabled)
		{
			ReleaseTexture();
			m_isDirty = true;
			RenderTex();
		}
	}

	public void Show()
	{
		Show(render: false);
	}

	public void Show(bool render)
	{
		m_renderEnabled = true;
		m_GameObject.GetComponent<Renderer>().enabled = true;
		if ((bool)m_BloomPlaneGameObject)
		{
			m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
		}
		if (render)
		{
			Render();
		}
	}

	public void Hide()
	{
		m_renderEnabled = false;
		m_GameObject.GetComponent<Renderer>().enabled = false;
		if ((bool)m_BloomPlaneGameObject)
		{
			m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	public void SetDirty()
	{
		m_init = false;
		m_isDirty = true;
	}

	public Material GetRenderMaterial()
	{
		if ((bool)m_GameObject)
		{
			return m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
		}
		return m_Material;
	}

	public GameObject GetRenderToObject()
	{
		return m_GameObject;
	}

	public RenderTexture GetRenderTexture()
	{
		return m_RenderTexture;
	}

	private void Init()
	{
		if (!m_init)
		{
			if ((bool)m_ObjectToRender && (bool)m_AlphaObjectToRender)
			{
				m_AlphaObjectToRender.transform.parent = m_ObjectToRender.transform;
			}
			if (m_ObjectToRender == null)
			{
				m_ObjectToRender = base.gameObject;
			}
			CalcWorldWidthHeightScale();
			CreateTexture();
			CreateCamera();
			if (m_OpaqueObjectAlphaFill || m_RenderMeshAsAlpha || m_AlphaObjectToRender != null)
			{
				CreateAlphaCamera();
			}
			if (!m_RenderToObject && m_CreateRenderPlane)
			{
				CreateRenderPlane();
			}
			if ((bool)m_RenderToObject)
			{
				m_RenderToObject.GetComponent<Renderer>().GetSharedMaterial().renderQueue = m_RenderQueueOffset + m_RenderQueue;
				m_GameObject = m_RenderToObject;
			}
			SetupMaterial();
			m_init = true;
		}
	}

	private void RenderTex()
	{
		if (!m_renderEnabled)
		{
			return;
		}
		if ((bool)m_RenderTexture)
		{
			RenderTexture.active = m_RenderTexture;
			GL.Clear(clearDepth: true, clearColor: false, Color.white);
			RenderTexture.active = null;
		}
		if ((bool)m_AlphaRenderTexture)
		{
			RenderTexture.active = m_AlphaRenderTexture;
			GL.Clear(clearDepth: true, clearColor: false, new Color(0f, 0f, 0f, 0f));
			RenderTexture.active = null;
		}
		if ((bool)m_BloomRenderTexture)
		{
			RenderTexture.active = m_BloomRenderTexture;
			GL.Clear(clearDepth: true, clearColor: false, new Color(0f, 0f, 0f, 0f));
			RenderTexture.active = null;
		}
		Init();
		if (!m_init || m_Camera == null)
		{
			return;
		}
		SetupForRender();
		if (m_RenderMaterial != m_PreviousRenderMaterial)
		{
			SetupMaterial();
		}
		if ((bool)m_ObjectToRender)
		{
			PositionObjectsAndCameras();
		}
		if (m_OpaqueObjectAlphaFill || m_RenderMeshAsAlpha || m_AlphaObjectToRender != null)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, m_RenderTexture.format);
			m_Camera.targetTexture = temporary;
			CameraRender();
			RenderTexture temporary2 = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, RenderTextureFormat.ARGB32);
			m_AlphaCamera.targetTexture = temporary2;
			AlphaCameraRender();
			if (m_OpaqueObjectAlphaFill)
			{
				AlphaBlendAddMaterial.SetTexture("_AlphaTex", temporary2);
			}
			else
			{
				AlphaBlendMaterial.SetTexture("_AlphaTex", temporary2);
			}
			if (m_BlurAmount > 0f)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, m_RenderTexture.format);
				if (m_OpaqueObjectAlphaFill)
				{
					Graphics.Blit(temporary, temporary3, AlphaBlendAddMaterial);
				}
				else
				{
					Graphics.Blit(temporary, temporary3, AlphaBlendMaterial);
				}
				CameraRender();
				Material sampleMat = BlurMaterial;
				if (m_BlurAlphaOnly)
				{
					sampleMat = AlphaBlurMaterial;
				}
				m_RenderTexture.DiscardContents();
				Sample(temporary3, m_RenderTexture, sampleMat, m_BlurAmount);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				m_RenderTexture.DiscardContents();
				if (m_OpaqueObjectAlphaFill)
				{
					Graphics.Blit(temporary, m_RenderTexture, AlphaBlendAddMaterial);
				}
				else
				{
					Graphics.Blit(temporary, m_RenderTexture, AlphaBlendMaterial);
				}
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else if (m_BlurAmount > 0f)
		{
			RenderTexture temporary4 = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, m_RenderTexture.format);
			m_Camera.targetTexture = temporary4;
			CameraRender();
			Material sampleMat2 = BlurMaterial;
			if (m_BlurAlphaOnly)
			{
				sampleMat2 = m_AlphaBlurMaterial;
			}
			m_RenderTexture.DiscardContents();
			Sample(temporary4, m_RenderTexture, sampleMat2, m_BlurAmount);
			RenderTexture.ReleaseTemporary(temporary4);
		}
		else
		{
			m_Camera.targetTexture = m_RenderTexture;
			CameraRender();
		}
		Renderer renderer = m_GameObject.GetComponent<Renderer>();
		if (renderer == null)
		{
			renderer = m_GameObject.GetComponentInChildren<Renderer>();
		}
		Material sharedMaterial = renderer.GetSharedMaterial();
		if (m_ShaderTextureName != string.Empty)
		{
			sharedMaterial.SetTexture(m_ShaderTextureName, m_RenderTexture);
		}
		else if (sharedMaterial.HasProperty("_MainTex"))
		{
			sharedMaterial.SetTexture("_MainTex", m_RenderTexture);
		}
		else
		{
			sharedMaterial.mainTexture = m_RenderTexture;
		}
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClip || m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			Material sharedMaterial2 = m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.SetFloat("_Cutoff", m_AlphaClip);
			sharedMaterial2.SetFloat("_Intensity", m_AlphaClipIntensity);
			sharedMaterial2.SetFloat("_AlphaIntensity", m_AlphaClipAlphaIntensity);
			if (m_AlphaClipRenderStyle == AlphaClipShader.ColorGradient)
			{
				sharedMaterial2.SetTexture("_GradientTex", m_AlphaClipGradientMap);
			}
		}
		m_isDirty = false;
	}

	private void RenderBloom()
	{
		if (m_BloomIntensity == 0f)
		{
			if ((bool)m_BloomPlaneGameObject)
			{
				Object.DestroyImmediate(m_BloomPlaneGameObject);
			}
			return;
		}
		if (m_BloomIntensity == 0f)
		{
			if ((bool)m_BloomPlaneGameObject)
			{
				Object.DestroyImmediate(m_BloomPlaneGameObject);
			}
			return;
		}
		int num = (int)((float)m_RenderTexture.width * Mathf.Clamp01(m_BloomResolutionRatio));
		int num2 = (int)((float)m_RenderTexture.height * Mathf.Clamp01(m_BloomResolutionRatio));
		RenderTexture renderTexture = m_RenderTexture;
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			if (!m_BloomPlaneGameObject)
			{
				CreateBloomPlane();
			}
			if (!m_BloomRenderTexture)
			{
				m_BloomRenderTexture = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32);
			}
		}
		if (!m_BloomRenderBuffer1)
		{
			m_BloomRenderBuffer1 = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32);
		}
		if (!m_BloomRenderBuffer2)
		{
			m_BloomRenderBuffer2 = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32);
		}
		if ((bool)m_BloomRenderTexture)
		{
			SceneUtils.SetHideFlags(m_BloomRenderTexture, m_DevFlag);
		}
		SceneUtils.SetHideFlags(m_BloomRenderBuffer1, m_DevFlag);
		SceneUtils.SetHideFlags(m_BloomRenderBuffer2, m_DevFlag);
		Material material = BloomMaterial;
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			material = AlphaClipBloomMaterial;
			renderTexture = m_BloomRenderTexture;
			if (!m_BloomCaptureCameraGO)
			{
				CreateBloomCaptureCamera();
			}
			m_BloomCaptureCamera.targetTexture = m_BloomRenderTexture;
			material.SetFloat("_Cutoff", m_AlphaClip);
			material.SetFloat("_Intensity", m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", m_AlphaClipAlphaIntensity);
			m_BloomCaptureCamera.Render();
		}
		if (m_BloomRenderType == BloomRenderType.Alpha)
		{
			material = BloomMaterialAlpha;
			material.SetFloat("_AlphaIntensity", m_BloomAlphaIntensity);
		}
		float num3 = 1f / (float)num;
		float num4 = 1f / (float)num2;
		material.SetFloat("_Threshold", m_BloomThreshold);
		material.SetFloat("_Intensity", m_BloomIntensity / (1f - m_BloomThreshold));
		material.SetVector("_OffsetA", new Vector4(1.5f * num3, 1.5f * num4, -1.5f * num3, 1.5f * num4));
		material.SetVector("_OffsetB", new Vector4(-1.5f * num3, -1.5f * num4, 1.5f * num3, -1.5f * num4));
		m_BloomRenderBuffer2.DiscardContents();
		Graphics.Blit(renderTexture, m_BloomRenderBuffer2, material, 1);
		num3 *= 4f * m_BloomBlur;
		num4 *= 4f * m_BloomBlur;
		material.SetVector("_OffsetA", new Vector4(1.5f * num3, 0f, -1.5f * num3, 0f));
		material.SetVector("_OffsetB", new Vector4(0.5f * num3, 0f, -0.5f * num3, 0f));
		m_BloomRenderBuffer1.DiscardContents();
		Graphics.Blit(m_BloomRenderBuffer2, m_BloomRenderBuffer1, material, 2);
		material.SetVector("_OffsetA", new Vector4(0f, 1.5f * num4, 0f, -1.5f * num4));
		material.SetVector("_OffsetB", new Vector4(0f, 0.5f * num4, 0f, -0.5f * num4));
		renderTexture.DiscardContents();
		Graphics.Blit(m_BloomRenderBuffer1, renderTexture, material, 2);
		Material sharedMaterial = m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			Material sharedMaterial2 = m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.color = m_BloomColor;
			sharedMaterial2.mainTexture = renderTexture;
			if (m_CreateRenderPlane)
			{
				sharedMaterial2.renderQueue = sharedMaterial.renderQueue + 1;
			}
		}
		else
		{
			sharedMaterial.color = m_BloomColor;
			sharedMaterial.mainTexture = renderTexture;
		}
	}

	private void SetupForRender()
	{
		CalcWorldWidthHeightScale();
		if (!m_RenderTexture)
		{
			CreateTexture();
		}
		if (m_CreateRenderPlane)
		{
			m_GameObject.layer = base.gameObject.layer;
		}
		if (m_Camera != null)
		{
			m_Camera.backgroundColor = m_ClearColor;
		}
	}

	private void PositionObjectsAndCameras()
	{
		if (!m_CreateRenderPlane)
		{
			m_ObjectToRender.transform.rotation = Quaternion.identity;
			m_ObjectToRender.transform.rotation = base.transform.rotation;
		}
		if ((bool)m_AlphaObjectToRender)
		{
			m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
		}
		if (!(m_CameraGO == null))
		{
			m_CameraGO.transform.rotation = Quaternion.identity;
			if (!m_CreateRenderPlane)
			{
				m_CameraGO.transform.position = m_ObjectToRender.transform.position + m_CameraOffset;
			}
			m_CameraGO.transform.rotation = m_ObjectToRender.transform.rotation;
			m_CameraGO.transform.Rotate(90f, 0f, 0f);
		}
	}

	private void SetupMaterial()
	{
		if (m_RenderMaterial == RenderToTextureMaterial.Custom || m_GameObject == null)
		{
			return;
		}
		Renderer component = m_GameObject.GetComponent<Renderer>();
		switch (m_RenderMaterial)
		{
		case RenderToTextureMaterial.Additive:
			component.SetSharedMaterial(AdditiveMaterial);
			break;
		case RenderToTextureMaterial.Transparent:
			component.SetSharedMaterial(TransparentMaterial);
			break;
		case RenderToTextureMaterial.AlphaClip:
		{
			Material material2 = ((m_AlphaClipRenderStyle != 0) ? AlphaClipGradientMaterial : AlphaClipMaterial);
			component.SetSharedMaterial(material2);
			material2.SetFloat("_Cutoff", m_AlphaClip);
			material2.SetFloat("_Intensity", m_AlphaClipIntensity);
			material2.SetFloat("_AlphaIntensity", m_AlphaClipAlphaIntensity);
			if (m_AlphaClipRenderStyle == AlphaClipShader.ColorGradient)
			{
				material2.SetTexture("_GradientTex", m_AlphaClipGradientMap);
			}
			break;
		}
		case RenderToTextureMaterial.AlphaClipBloom:
		{
			Material material = ((m_AlphaClipRenderStyle != 0) ? AlphaClipGradientMaterial : AlphaClipMaterial);
			component.SetSharedMaterial(material);
			material.SetFloat("_Cutoff", m_AlphaClip);
			material.SetFloat("_Intensity", m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", m_AlphaClipAlphaIntensity);
			if (m_AlphaClipRenderStyle == AlphaClipShader.ColorGradient)
			{
				material.SetTexture("_GradientTex", m_AlphaClipGradientMap);
			}
			break;
		}
		case RenderToTextureMaterial.Bloom:
			if (m_BloomBlend == BloomBlendType.Additive)
			{
				component.SetSharedMaterial(AdditiveMaterial);
			}
			else if (m_BloomBlend == BloomBlendType.Transparent)
			{
				component.SetSharedMaterial(TransparentMaterial);
			}
			break;
		default:
			if (m_Material != null)
			{
				component.SetSharedMaterial(m_Material);
			}
			break;
		}
		Material sharedMaterial = component.GetSharedMaterial();
		sharedMaterial.color *= m_TintColor;
		if (m_BloomIntensity > 0f && (bool)m_BloomPlaneGameObject)
		{
			m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().color = m_BloomColor;
		}
		sharedMaterial.renderQueue = m_RenderQueueOffset + m_RenderQueue;
		if ((bool)m_BloomPlaneGameObject)
		{
			m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().renderQueue = m_RenderQueueOffset + m_RenderQueue + 1;
		}
		m_PreviousRenderMaterial = m_RenderMaterial;
	}

	private void CreateTexture()
	{
		if (m_RenderTexture != null)
		{
			return;
		}
		Vector2 vector = CalcTextureSize();
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null)
		{
			if (graphicsManager.RenderQualityLevel == GraphicsQuality.Low)
			{
				vector *= 0.75f;
			}
			else if (graphicsManager.RenderQualityLevel == GraphicsQuality.Medium)
			{
				vector *= 1f;
			}
			else if (graphicsManager.RenderQualityLevel == GraphicsQuality.High)
			{
				vector *= 1.5f;
			}
		}
		m_RenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, m_RenderTextureFormat);
		SceneUtils.SetHideFlags(m_RenderTexture, m_DevFlag);
		m_RenderTexture.Create();
		if (m_RenderMeshAsAlpha)
		{
			m_AlphaRenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, m_RenderTextureFormat);
			SceneUtils.SetHideFlags(m_AlphaRenderTexture, m_DevFlag);
			m_AlphaRenderTexture.Create();
		}
		if ((bool)m_Camera)
		{
			m_Camera.targetTexture = m_RenderTexture;
		}
		if ((bool)m_AlphaCamera)
		{
			m_AlphaCamera.targetTexture = m_AlphaRenderTexture;
		}
	}

	private void ReleaseTexture()
	{
		if (RenderTexture.active == m_RenderTexture)
		{
			RenderTexture.active = null;
		}
		if (RenderTexture.active == m_AlphaRenderTexture)
		{
			RenderTexture.active = null;
		}
		if (RenderTexture.active == m_BloomRenderTexture)
		{
			RenderTexture.active = null;
		}
		if (m_RenderTexture != null)
		{
			if ((bool)m_Camera)
			{
				m_Camera.targetTexture = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(m_RenderTexture);
			m_RenderTexture = null;
		}
		if (m_AlphaRenderTexture != null)
		{
			if ((bool)m_AlphaCamera)
			{
				m_AlphaCamera.targetTexture = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(m_AlphaRenderTexture);
			m_AlphaRenderTexture = null;
		}
		if (m_BloomRenderTexture != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_BloomRenderTexture);
			m_BloomRenderTexture = null;
		}
		if (m_BloomRenderBuffer1 != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_BloomRenderBuffer1);
			m_BloomRenderBuffer1 = null;
		}
		if (m_BloomRenderBuffer2 != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_BloomRenderBuffer2);
		}
		m_BloomRenderBuffer2 = null;
	}

	private void CreateCamera()
	{
		if (!(m_Camera != null))
		{
			m_CameraGO = new GameObject();
			m_Camera = m_CameraGO.AddComponent<Camera>();
			m_CameraGO.name = base.name + "_R2TRenderCamera";
			m_Camera.cullingMask = m_LayerMask;
			SceneUtils.SetHideFlags(m_CameraGO, m_DevFlag);
			m_Camera.orthographic = true;
			m_CameraGO.transform.parent = base.transform;
			m_CameraGO.transform.position = base.transform.position + m_PositionOffset + m_CameraOffset;
			m_CameraGO.transform.rotation = base.transform.rotation;
			m_CameraGO.transform.Rotate(90f, 0f, 0f);
			if (m_FarClip < 0f)
			{
				m_FarClip = 0f;
			}
			if (m_NearClip > 0f)
			{
				m_NearClip = 0f;
			}
			m_Camera.nearClipPlane = m_NearClip * m_WorldScale.y;
			m_Camera.farClipPlane = m_FarClip * m_WorldScale.y;
			Camera main = Camera.main;
			if (main != null)
			{
				m_Camera.depth = main.depth - 2f;
			}
			m_Camera.clearFlags = m_ClearFlags;
			m_Camera.backgroundColor = m_ClearColor;
			m_Camera.depthTextureMode = DepthTextureMode.None;
			m_Camera.renderingPath = RenderingPath.Forward;
			m_Camera.allowHDR = false;
			m_Camera.targetTexture = m_RenderTexture;
			m_Camera.enabled = false;
		}
	}

	private float OrthoSize()
	{
		if (m_OverrideCameraSize > 0f)
		{
			return m_OverrideCameraSize;
		}
		float num = 0f;
		if (m_WorldWidth > m_WorldHeight)
		{
			return Mathf.Min(m_WorldWidth * 0.5f, m_WorldHeight * 0.5f);
		}
		return m_WorldHeight * 0.5f;
	}

	private void CameraRender()
	{
		m_Camera.orthographicSize = OrthoSize();
		m_Camera.farClipPlane = m_FarClip * m_WorldScale.z;
		m_Camera.nearClipPlane = m_NearClip * m_WorldScale.z;
		if (m_CreateRenderPlane)
		{
			m_GameObject.GetComponent<Renderer>().enabled = false;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if ((bool)m_ReplacementShader)
		{
			m_Camera.RenderWithShader(m_ReplacementShader, m_ReplacementTag);
		}
		else
		{
			m_Camera.Render();
		}
		if (m_CreateRenderPlane)
		{
			m_GameObject.GetComponent<Renderer>().enabled = true;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}

	private void CreateAlphaCamera()
	{
		if (!(m_AlphaCamera != null))
		{
			m_AlphaCameraGO = new GameObject();
			m_AlphaCamera = m_AlphaCameraGO.AddComponent<Camera>();
			m_AlphaCameraGO.name = base.name + "_R2TAlphaRenderCamera";
			SceneUtils.SetHideFlags(m_AlphaCameraGO, m_DevFlag);
			m_AlphaCamera.CopyFrom(m_Camera);
			m_AlphaCamera.enabled = false;
			m_AlphaCamera.cullingMask = m_LayerMask;
			m_AlphaCamera.backgroundColor = Color.clear;
			m_AlphaCamera.allowHDR = false;
			m_AlphaCameraGO.transform.parent = m_CameraGO.transform;
			m_AlphaCameraGO.transform.position = m_CameraGO.transform.position;
			m_AlphaCameraGO.transform.localRotation = Quaternion.identity;
		}
	}

	private void AlphaCameraRender()
	{
		m_AlphaCamera.orthographicSize = OrthoSize();
		m_AlphaCamera.farClipPlane = m_FarClip * m_WorldScale.z;
		m_AlphaCamera.nearClipPlane = m_NearClip * m_WorldScale.z;
		if (m_CreateRenderPlane)
		{
			m_GameObject.GetComponent<Renderer>().enabled = false;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if (m_OpaqueObjectAlphaFill)
		{
			m_AlphaCamera.RenderWithShader(m_AlphaFillShader, "RenderType");
		}
		else if (m_AlphaObjectToRender == null)
		{
			string text = m_AlphaReplacementTag;
			if (text == string.Empty)
			{
				text = m_ReplacementTag;
			}
			m_AlphaCamera.RenderWithShader(m_AlphaFillShader, text);
		}
		else
		{
			m_AlphaCamera.Render();
		}
		if (m_CreateRenderPlane)
		{
			m_GameObject.GetComponent<Renderer>().enabled = true;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}

	private void CreateBloomCaptureCamera()
	{
		if (!(m_BloomCaptureCamera != null))
		{
			m_BloomCaptureCameraGO = new GameObject();
			m_BloomCaptureCamera = m_BloomCaptureCameraGO.AddComponent<Camera>();
			m_BloomCaptureCameraGO.name = base.name + "_R2TBloomRenderCamera";
			SceneUtils.SetHideFlags(m_BloomCaptureCameraGO, m_DevFlag);
			m_BloomCaptureCamera.CopyFrom(m_Camera);
			m_BloomCaptureCamera.enabled = false;
			m_BloomCaptureCamera.cullingMask = m_LayerMask;
			m_BloomCaptureCamera.depth = m_Camera.depth + 1f;
			m_BloomCaptureCameraGO.transform.parent = m_Camera.transform;
			m_BloomCaptureCameraGO.transform.localPosition = Vector3.zero;
			m_BloomCaptureCameraGO.transform.localRotation = Quaternion.identity;
		}
	}

	private Vector2 CalcTextureSize()
	{
		Vector2 result = new Vector2(m_Resolution, m_Resolution);
		if (m_WorldWidth > m_WorldHeight)
		{
			result.x = m_Resolution;
			result.y = (float)m_Resolution * (m_WorldHeight / m_WorldWidth);
		}
		else
		{
			result.x = (float)m_Resolution * (m_WorldWidth / m_WorldHeight);
			result.y = m_Resolution;
		}
		return result;
	}

	private void CreateRenderPlane()
	{
		if (m_CreateRenderPlane && m_GameObject != null)
		{
			Object.DestroyImmediate(m_GameObject);
		}
		m_GameObject = CreateMeshPlane($"{base.name}_RenderPlane", m_Material);
		SceneUtils.SetHideFlags(m_GameObject, m_DevFlag);
	}

	private void CreateBloomPlane()
	{
		if (m_BloomPlaneGameObject != null)
		{
			Object.DestroyImmediate(m_BloomPlaneGameObject);
		}
		Material material = AdditiveMaterial;
		if (m_BloomBlend == BloomBlendType.Transparent)
		{
			material = TransparentMaterial;
		}
		m_BloomPlaneGameObject = CreateMeshPlane($"{base.name}_BloomRenderPlane", material);
		m_BloomPlaneGameObject.transform.parent = m_GameObject.transform;
		m_BloomPlaneGameObject.transform.localPosition = new Vector3(0f, 0.15f, 0f);
		m_BloomPlaneGameObject.transform.localRotation = Quaternion.identity;
		m_BloomPlaneGameObject.transform.localScale = Vector3.one;
		m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().color = m_BloomColor;
		SceneUtils.SetHideFlags(m_BloomPlaneGameObject, m_DevFlag);
	}

	private GameObject CreateMeshPlane(string name, Material material)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = name;
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = m_PositionOffset;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
		Mesh mesh = new Mesh();
		float num = m_Width * 0.5f;
		float num2 = m_Height * 0.5f;
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - num, 0f, 0f - num2),
			new Vector3(num, 0f, 0f - num2),
			new Vector3(0f - num, 0f, num2),
			new Vector3(num, 0f, num2)
		};
		mesh.uv = PLANE_UVS;
		mesh.normals = PLANE_NORMALS;
		mesh.triangles = PLANE_TRIANGLES;
		Mesh mesh3 = (gameObject.GetComponent<MeshFilter>().mesh = mesh);
		SceneUtils.SetHideFlags(mesh3, m_DevFlag);
		mesh3.RecalculateBounds();
		if ((bool)material)
		{
			material.renderQueue = m_RenderQueueOffset + m_RenderQueue;
			gameObject.GetComponent<Renderer>().SetSharedMaterial(material);
		}
		return gameObject;
	}

	private void Sample(RenderTexture source, RenderTexture dest, Material sampleMat, float offset)
	{
		Graphics.BlitMultiTap(source, dest, sampleMat, new Vector2(0f - offset, 0f - offset), new Vector2(0f - offset, offset), new Vector2(offset, offset), new Vector2(offset, 0f - offset));
	}

	private void CalcWorldWidthHeightScale()
	{
		Quaternion rotation = base.transform.rotation;
		Vector3 localScale = base.transform.localScale;
		Transform parent = base.transform.parent;
		base.transform.rotation = Quaternion.identity;
		bool flag = false;
		if (base.transform.lossyScale.magnitude == 0f)
		{
			base.transform.parent = null;
			base.transform.localScale = Vector3.one;
			flag = true;
		}
		if (m_UniformWorldScale)
		{
			float num = Mathf.Max(base.transform.lossyScale.x, base.transform.lossyScale.y, base.transform.lossyScale.z);
			m_WorldScale = new Vector3(num, num, num);
		}
		else
		{
			m_WorldScale = base.transform.lossyScale;
		}
		m_WorldWidth = m_Width * m_WorldScale.x;
		m_WorldHeight = m_Height * m_WorldScale.y;
		if (flag)
		{
			base.transform.parent = parent;
			base.transform.localScale = localScale;
		}
		base.transform.rotation = rotation;
		if (m_WorldWidth == 0f || m_WorldHeight == 0f)
		{
			Debug.LogError($" RenderToTexture has a world scale of zero. \nm_WorldWidth: {m_WorldWidth},   m_WorldHeight: {m_WorldHeight}");
		}
	}

	private void CleanUp()
	{
		ReleaseTexture();
		if ((bool)m_CameraGO)
		{
			Object.Destroy(m_CameraGO);
		}
		if ((bool)m_AlphaCameraGO)
		{
			Object.Destroy(m_AlphaCameraGO);
		}
		if ((bool)m_BloomPlaneGameObject)
		{
			Object.Destroy(m_BloomPlaneGameObject);
		}
		if ((bool)m_BloomCaptureCameraGO)
		{
			Object.Destroy(m_BloomCaptureCameraGO);
		}
		if (m_ObjectToRender != null)
		{
			m_ObjectToRender.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
		m_init = false;
		m_isDirty = true;
	}
}
