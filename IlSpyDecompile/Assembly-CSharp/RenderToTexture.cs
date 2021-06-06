using System.Collections.Generic;
using Hearthstone.Extensions;
using Hearthstone.UI;
using UnityEngine;

public class RenderToTexture : MonoBehaviour, IPopupRendering
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

	private const RenderTextureFormat ALPHA_TEXTURE_FORMAT = RenderTextureFormat.R8;

	private const float OFFSET_DISTANCE = 300f;

	private const float MIN_OFFSET_DISTANCE = -4000f;

	private const float MAX_OFFSET_DISTANCE = -90000f;

	private readonly Vector3 ALPHA_OBJECT_OFFSET = new Vector3(0f, 1000f, 0f);

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

	public GameObject m_ObjectToRender;

	public GameObject m_AlphaObjectToRender;

	public bool m_HideRenderObject = true;

	public bool m_RealtimeRender;

	public bool m_RealtimeTranslation;

	public bool m_RenderMeshAsAlpha;

	public bool m_OpaqueObjectAlphaFill;

	public RenderToTextureMaterial m_RenderMaterial;

	public Material m_Material;

	public bool m_CreateRenderPlane = true;

	public GameObject m_RenderToObject;

	public string m_ShaderTextureName = string.Empty;

	public int m_Resolution = 128;

	public float m_Width = 1f;

	public float m_Height = 1f;

	public float m_NearClip = -0.1f;

	public float m_FarClip = 0.5f;

	public float m_BloomIntensity;

	public float m_BloomThreshold = 0.7f;

	public float m_BloomBlur = 0.3f;

	public float m_BloomResolutionRatio = 0.5f;

	public BloomRenderType m_BloomRenderType;

	public float m_BloomAlphaIntensity = 1f;

	public BloomBlendType m_BloomBlend;

	public Color m_BloomColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	public AlphaClipShader m_AlphaClipRenderStyle;

	public float m_AlphaClip = 15f;

	public float m_AlphaClipIntensity = 1.5f;

	public float m_AlphaClipAlphaIntensity = 1f;

	public Texture2D m_AlphaClipGradientMap;

	public float m_BlurAmount;

	public bool m_BlurAlphaOnly;

	public Color m_TintColor = Color.white;

	public int m_RenderQueueOffset = 3000;

	public int m_RenderQueue;

	public Color m_ClearColor = Color.clear;

	public Shader m_ReplacmentShader;

	public string m_ReplacmentTag;

	public string m_AlphaReplacementTag;

	public RenderTextureFormat m_RenderTextureFormat = RenderTextureFormat.Default;

	public Vector3 m_PositionOffset = Vector3.zero;

	public Vector3 m_CameraOffset = Vector3.zero;

	public LayerMask m_LayerMask = -1;

	public bool m_UniformWorldScale;

	public float m_OverrideCameraSize;

	public bool m_LateUpdate;

	public bool m_RenderOnStart = true;

	public bool m_RenderOnEnable = true;

	private bool m_renderEnabled = true;

	private bool m_init;

	private float m_WorldWidth;

	private float m_WorldHeight;

	private Vector3 m_WorldScale;

	private GameObject m_OffscreenGameObject;

	private GameObject m_CameraGO;

	private Camera m_Camera;

	private GameObject m_AlphaCameraGO;

	private Camera m_AlphaCamera;

	private GameObject m_BloomCaptureCameraGO;

	private Camera m_BloomCaptureCamera;

	private RenderTexture m_RenderTexture;

	private RenderTexture m_BloomRenderTexture;

	private RenderTexture m_BloomRenderBuffer1;

	private RenderTexture m_BloomRenderBuffer2;

	private GameObject m_PlaneGameObject;

	private GameObject m_BloomPlaneGameObject;

	private GameObject m_BloomCapturePlaneGameObject;

	private bool m_ObjectToRenderOrgPositionStored;

	private Transform m_ObjectToRenderOrgParent;

	private Vector3 m_ObjectToRenderOrgPosition = Vector3.zero;

	private Vector3 m_OriginalRenderPosition = Vector3.zero;

	private bool m_isDirty;

	private Shader m_AlphaFillShader;

	private Vector3 m_OffscreenPos;

	private Vector3 m_ObjectToRenderOffset = Vector3.zero;

	private Vector3 m_AlphaObjectToRenderOffset = Vector3.zero;

	private RenderToTextureMaterial m_PreviousRenderMaterial;

	private int m_previousRenderQueue;

	private List<Renderer> m_OpaqueObjectAlphaFillTransparent;

	private List<UberText> m_OpaqueObjectAlphaFillUberText;

	private bool m_hasMaterialInstance;

	private static MaterialManagerService s_MaterialManagerService;

	private Vector3 m_Offset = Vector3.zero;

	private static Vector3 s_offset = new Vector3(-4000f, -4000f, -4000f);

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

	private PopupRoot m_popupRoot;

	protected Vector3 Offset
	{
		get
		{
			if (m_Offset == Vector3.zero)
			{
				s_offset.x -= 300f;
				if (s_offset.x < -90000f)
				{
					s_offset.x = -4000f;
					s_offset.y -= 300f;
					if (s_offset.y < -90000f)
					{
						s_offset.y = -4000f;
						s_offset.z -= 300f;
						if (s_offset.z < -90000f)
						{
							s_offset.z = -4000f;
						}
					}
				}
				m_Offset = s_offset;
			}
			return m_Offset;
		}
	}

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

	public bool DontRefreshOnFocus { get; set; }

	private void Awake()
	{
		m_AlphaFillShader = ShaderUtils.FindShader("Custom/AlphaFillOpaque");
		if (!m_AlphaFillShader)
		{
			Debug.LogError("Failed to load RenderToTexture Shader: Custom/AlphaFillOpaque");
		}
		m_OffscreenPos = Offset;
		if (m_Material != null)
		{
			m_Material = Object.Instantiate(m_Material);
			m_hasMaterialInstance = true;
			GetMaterialManagerService().IgnoreMaterial(m_Material);
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
		if (!m_renderEnabled)
		{
			return;
		}
		if ((bool)m_RenderTexture && !m_RenderTexture.IsCreated())
		{
			Log.Graphics.Print("RenderToTexture Texture lost. Render Called");
			m_isDirty = true;
			RenderTex();
		}
		else if (!m_LateUpdate)
		{
			if (m_HideRenderObject && (bool)m_ObjectToRender)
			{
				PositionHiddenObjectsAndCameras();
			}
			if (m_RealtimeRender || m_isDirty)
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
			if (m_HideRenderObject && (bool)m_ObjectToRender)
			{
				PositionHiddenObjectsAndCameras();
			}
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
		if (!DontRefreshOnFocus && (bool)m_RenderTexture && state)
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
		RestoreAfterRender();
		if ((bool)m_ObjectToRender)
		{
			if (m_ObjectToRenderOrgParent != null)
			{
				m_ObjectToRender.transform.parent = m_ObjectToRenderOrgParent;
			}
			m_ObjectToRender.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
		if ((bool)m_PlaneGameObject)
		{
			Object.Destroy(m_PlaneGameObject);
		}
		if ((bool)m_BloomPlaneGameObject)
		{
			Object.Destroy(m_BloomPlaneGameObject);
		}
		if ((bool)m_BloomCapturePlaneGameObject)
		{
			Object.Destroy(m_BloomCapturePlaneGameObject);
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
		if ((bool)m_RenderToObject)
		{
			m_RenderToObject.GetComponent<Renderer>().enabled = true;
		}
		else if ((bool)m_PlaneGameObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
		if (render)
		{
			Render();
		}
	}

	public void Hide()
	{
		m_renderEnabled = false;
		if ((bool)m_RenderToObject)
		{
			m_RenderToObject.GetComponent<Renderer>().enabled = false;
		}
		else if ((bool)m_PlaneGameObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	public void SetDirty()
	{
		m_init = false;
		m_isDirty = true;
	}

	public Material GetRenderMaterial()
	{
		if ((bool)m_RenderToObject)
		{
			return m_RenderToObject.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_PlaneGameObject)
		{
			return m_PlaneGameObject.GetComponent<Renderer>().GetMaterial();
		}
		return m_Material;
	}

	public GameObject GetRenderToObject()
	{
		if ((bool)m_RenderToObject)
		{
			return m_RenderToObject;
		}
		return m_PlaneGameObject;
	}

	public RenderTexture GetRenderTexture()
	{
		return m_RenderTexture;
	}

	public Vector3 GetOffscreenPosition()
	{
		return m_OffscreenPos;
	}

	public Vector3 GetOffscreenPositionOffset()
	{
		return m_OffscreenPos - base.transform.position;
	}

	private void Init()
	{
		if (m_init)
		{
			return;
		}
		if (m_RealtimeTranslation)
		{
			m_OffscreenGameObject = new GameObject();
			m_OffscreenGameObject.name = $"R2TOffsetRenderRoot_{base.name}";
			m_OffscreenGameObject.transform.position = base.transform.position;
		}
		if ((bool)m_ObjectToRender)
		{
			if (!m_ObjectToRenderOrgPositionStored)
			{
				m_ObjectToRenderOrgParent = m_ObjectToRender.transform.parent;
				m_ObjectToRenderOrgPosition = m_ObjectToRender.transform.localPosition;
				m_ObjectToRenderOrgPositionStored = true;
			}
			if (m_HideRenderObject)
			{
				if (m_RealtimeTranslation)
				{
					m_ObjectToRender.transform.parent = m_OffscreenGameObject.transform;
					if ((bool)m_AlphaObjectToRender)
					{
						m_AlphaObjectToRender.transform.parent = m_OffscreenGameObject.transform;
					}
				}
				if ((bool)m_RenderToObject)
				{
					m_OriginalRenderPosition = m_RenderToObject.transform.position;
				}
				else
				{
					m_OriginalRenderPosition = base.transform.position;
				}
				if ((bool)m_ObjectToRender && m_ObjectToRenderOffset == Vector3.zero)
				{
					m_ObjectToRenderOffset = base.transform.position - m_ObjectToRender.transform.position;
				}
				if ((bool)m_AlphaObjectToRender && m_AlphaObjectToRenderOffset == Vector3.zero)
				{
					m_AlphaObjectToRenderOffset = base.transform.position - m_AlphaObjectToRender.transform.position;
				}
			}
		}
		else if (!m_ObjectToRenderOrgPositionStored)
		{
			m_ObjectToRenderOrgPosition = base.transform.localPosition;
			if (m_OffscreenGameObject != null)
			{
				m_OffscreenGameObject.transform.position = base.transform.position;
			}
			m_ObjectToRenderOrgPositionStored = true;
		}
		if (m_HideRenderObject)
		{
			if (m_RealtimeTranslation)
			{
				if (m_OffscreenGameObject != null)
				{
					m_OffscreenGameObject.transform.position = m_OffscreenPos;
				}
			}
			else if ((bool)m_ObjectToRender)
			{
				m_ObjectToRender.transform.position = m_OffscreenPos;
			}
			else
			{
				base.transform.position = m_OffscreenPos;
			}
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
			m_RenderToObject.GetComponent<Renderer>().GetMaterial().renderQueue = m_RenderQueueOffset + m_RenderQueue;
		}
		SetupMaterial();
		m_init = true;
	}

	private void RenderTex()
	{
		if (!m_renderEnabled)
		{
			return;
		}
		Init();
		if (!m_init || m_Camera == null)
		{
			return;
		}
		SetupForRender();
		if (m_RenderMaterial != m_PreviousRenderMaterial || m_RenderQueue != m_previousRenderQueue)
		{
			SetupMaterial();
		}
		if (m_HideRenderObject && (bool)m_ObjectToRender)
		{
			PositionHiddenObjectsAndCameras();
		}
		if (m_OpaqueObjectAlphaFill || m_RenderMeshAsAlpha || m_AlphaObjectToRender != null)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, m_RenderTexture.format);
			m_Camera.targetTexture = temporary;
			CameraRender();
			RenderTexture temporary2 = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, 16, RenderTextureFormat.R8);
			AlphaCameraRender(temporary2);
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
		if ((bool)m_RenderToObject)
		{
			Renderer renderer = m_RenderToObject.GetComponent<Renderer>();
			if (renderer == null)
			{
				renderer = m_RenderToObject.GetComponentInChildren<Renderer>();
			}
			if (m_ShaderTextureName != string.Empty)
			{
				renderer.GetMaterial().SetTexture(m_ShaderTextureName, m_RenderTexture);
			}
			else
			{
				renderer.GetMaterial().mainTexture = m_RenderTexture;
			}
		}
		else if ((bool)m_PlaneGameObject)
		{
			if (m_ShaderTextureName != string.Empty)
			{
				m_PlaneGameObject.GetComponent<Renderer>().GetMaterial().SetTexture(m_ShaderTextureName, m_RenderTexture);
			}
			else
			{
				m_PlaneGameObject.GetComponent<Renderer>().GetMaterial().mainTexture = m_RenderTexture;
			}
		}
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClip || m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			GameObject gameObject = m_PlaneGameObject;
			if ((bool)m_RenderToObject)
			{
				gameObject = m_RenderToObject;
			}
			Material material = gameObject.GetComponent<Renderer>().GetMaterial();
			material.SetFloat("_Cutoff", m_AlphaClip);
			material.SetFloat("_Intensity", m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", m_AlphaClipAlphaIntensity);
			if (m_AlphaClipRenderStyle == AlphaClipShader.ColorGradient)
			{
				material.SetTexture("_GradientTex", m_AlphaClipGradientMap);
			}
		}
		if (!m_RealtimeRender)
		{
			RestoreAfterRender();
		}
		if (m_popupRoot != null)
		{
			if (m_PlaneGameObject != null && m_PlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				m_PlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(m_popupRoot);
			}
			if (m_BloomPlaneGameObject != null && m_BloomPlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				m_BloomPlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(m_popupRoot);
			}
			if (m_BloomCapturePlaneGameObject != null && m_BloomCapturePlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				m_BloomCapturePlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(m_popupRoot);
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
		Material material2 = m_PlaneGameObject.GetComponent<Renderer>().GetMaterial();
		if (m_RenderMaterial == RenderToTextureMaterial.AlphaClipBloom)
		{
			Material material3 = m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial();
			material3.color = m_BloomColor;
			material3.mainTexture = renderTexture;
			if ((bool)m_PlaneGameObject)
			{
				material3.renderQueue = material2.renderQueue + 1;
			}
		}
		else if ((bool)m_RenderToObject)
		{
			Material material4 = m_RenderToObject.GetComponent<Renderer>().GetMaterial();
			material4.color = m_BloomColor;
			material4.mainTexture = renderTexture;
		}
		else
		{
			material2.color = m_BloomColor;
			material2.mainTexture = renderTexture;
		}
	}

	private void SetupForRender()
	{
		CalcWorldWidthHeightScale();
		if (!m_RenderTexture)
		{
			CreateTexture();
		}
		if (m_HideRenderObject)
		{
			if ((bool)m_PlaneGameObject)
			{
				m_PlaneGameObject.transform.localPosition = m_PositionOffset;
				m_PlaneGameObject.layer = base.gameObject.layer;
			}
			if (m_Camera != null)
			{
				m_Camera.backgroundColor = m_ClearColor;
			}
		}
	}

	private void SetupMaterial()
	{
		GameObject gameObject = m_PlaneGameObject;
		if ((bool)m_RenderToObject)
		{
			gameObject = m_RenderToObject;
			if (m_RenderMaterial == RenderToTextureMaterial.Custom)
			{
				return;
			}
		}
		if (gameObject == null)
		{
			return;
		}
		Renderer component = gameObject.GetComponent<Renderer>();
		switch (m_RenderMaterial)
		{
		case RenderToTextureMaterial.Additive:
			component.SetMaterial(AdditiveMaterial);
			break;
		case RenderToTextureMaterial.Transparent:
			component.SetMaterial(TransparentMaterial);
			break;
		case RenderToTextureMaterial.AlphaClip:
		{
			Material material2 = ((m_AlphaClipRenderStyle != 0) ? AlphaClipGradientMaterial : AlphaClipMaterial);
			component.SetMaterial(material2);
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
			component.SetMaterial(material);
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
				component.SetMaterial(AdditiveMaterial);
			}
			else if (m_BloomBlend == BloomBlendType.Transparent)
			{
				component.SetMaterial(TransparentMaterial);
			}
			break;
		default:
			if (m_Material != null)
			{
				component.SetMaterial(m_Material);
			}
			break;
		}
		Material material3 = component.GetMaterial();
		if (material3 != null)
		{
			material3.color *= m_TintColor;
		}
		if (m_BloomIntensity > 0f && (bool)m_BloomPlaneGameObject)
		{
			m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial().color = m_BloomColor;
		}
		component.sortingOrder = m_RenderQueue;
		material3.renderQueue = m_RenderQueueOffset + m_RenderQueue;
		if ((bool)m_BloomPlaneGameObject)
		{
			Renderer component2 = m_BloomPlaneGameObject.GetComponent<Renderer>();
			component2.sortingOrder = m_RenderQueue + 1;
			component2.GetMaterial().renderQueue = m_RenderQueueOffset + m_RenderQueue + 1;
		}
		m_PreviousRenderMaterial = m_RenderMaterial;
		m_previousRenderQueue = m_RenderQueue;
	}

	private void PositionHiddenObjectsAndCameras()
	{
		Vector3 zero = Vector3.zero;
		zero = ((!m_RenderToObject) ? (base.transform.position - m_OriginalRenderPosition) : (m_RenderToObject.transform.position - m_OriginalRenderPosition));
		if (m_RealtimeTranslation)
		{
			m_OffscreenGameObject.transform.position = m_OffscreenPos + zero;
			m_OffscreenGameObject.transform.rotation = base.transform.rotation;
			if ((bool)m_AlphaObjectToRender)
			{
				m_AlphaObjectToRender.transform.position = m_OffscreenPos - ALPHA_OBJECT_OFFSET - m_AlphaObjectToRenderOffset + zero;
				m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
			}
			return;
		}
		if ((bool)m_ObjectToRender)
		{
			m_ObjectToRender.transform.rotation = Quaternion.identity;
			m_ObjectToRender.transform.position = m_OffscreenPos - m_ObjectToRenderOffset + m_PositionOffset;
			m_ObjectToRender.transform.rotation = base.transform.rotation;
		}
		if ((bool)m_AlphaObjectToRender)
		{
			m_AlphaObjectToRender.transform.position = m_OffscreenPos - ALPHA_OBJECT_OFFSET;
			m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
		}
		if (!(m_CameraGO == null))
		{
			m_CameraGO.transform.rotation = Quaternion.identity;
			if ((bool)m_ObjectToRender)
			{
				m_CameraGO.transform.position = m_ObjectToRender.transform.position + m_CameraOffset;
			}
			else
			{
				m_CameraGO.transform.position = m_OffscreenPos + m_PositionOffset + m_CameraOffset;
			}
			m_CameraGO.transform.rotation = m_ObjectToRender.transform.rotation;
			m_CameraGO.transform.Rotate(90f, 0f, 0f);
		}
	}

	private void RestoreAfterRender()
	{
		if (m_HideRenderObject)
		{
			return;
		}
		if ((bool)m_ObjectToRender)
		{
			if (m_ObjectToRenderOrgParent != null)
			{
				m_ObjectToRender.transform.parent = m_ObjectToRenderOrgParent;
			}
			m_ObjectToRender.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
		else
		{
			base.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
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
		m_RenderTexture.Create();
		if ((bool)m_Camera)
		{
			m_Camera.targetTexture = m_RenderTexture;
		}
	}

	private void ReleaseTexture()
	{
		if (RenderTexture.active == m_RenderTexture)
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
			m_BloomRenderBuffer2 = null;
		}
	}

	private void CreateCamera()
	{
		if (m_Camera != null)
		{
			return;
		}
		m_CameraGO = new GameObject();
		m_Camera = m_CameraGO.AddComponent<Camera>();
		m_CameraGO.name = base.name + "_R2TRenderCamera";
		m_CameraGO.AddComponent<RenderToTextureCamera>();
		m_Camera.orthographic = true;
		if (m_HideRenderObject)
		{
			if (m_RealtimeTranslation)
			{
				m_OffscreenGameObject.transform.position = base.transform.position;
				m_CameraGO.transform.parent = m_OffscreenGameObject.transform;
				m_CameraGO.transform.localPosition = Vector3.zero + m_PositionOffset + m_CameraOffset;
				m_CameraGO.transform.rotation = base.transform.rotation;
				m_OffscreenGameObject.transform.position = m_OffscreenPos;
			}
			else
			{
				m_CameraGO.transform.parent = null;
				m_CameraGO.transform.position = m_OffscreenPos + m_PositionOffset + m_CameraOffset;
				m_CameraGO.transform.rotation = base.transform.rotation;
			}
		}
		else
		{
			m_CameraGO.transform.parent = base.transform;
			m_CameraGO.transform.position = base.transform.position + m_PositionOffset + m_CameraOffset;
			m_CameraGO.transform.rotation = base.transform.rotation;
		}
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
		m_Camera.clearFlags = CameraClearFlags.Color;
		m_Camera.backgroundColor = m_ClearColor;
		m_Camera.depthTextureMode = DepthTextureMode.None;
		m_Camera.renderingPath = RenderingPath.Forward;
		m_Camera.cullingMask = m_LayerMask;
		m_Camera.allowHDR = false;
		m_Camera.targetTexture = m_RenderTexture;
		m_Camera.enabled = false;
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
		if ((bool)m_PlaneGameObject && !m_HideRenderObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if ((bool)m_ReplacmentShader)
		{
			m_Camera.RenderWithShader(m_ReplacmentShader, m_ReplacmentTag);
		}
		else
		{
			m_Camera.Render();
		}
		if ((bool)m_PlaneGameObject && !m_HideRenderObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
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
			m_AlphaCameraGO.AddComponent<RenderToTextureCamera>();
			m_AlphaCameraGO.name = base.name + "_R2TAlphaRenderCamera";
			m_AlphaCamera.CopyFrom(m_Camera);
			m_AlphaCamera.enabled = false;
			m_AlphaCamera.backgroundColor = Color.clear;
			m_AlphaCamera.allowHDR = false;
			m_AlphaCameraGO.transform.parent = m_CameraGO.transform;
			if ((bool)m_AlphaObjectToRender)
			{
				m_AlphaCameraGO.transform.position = m_CameraGO.transform.position - ALPHA_OBJECT_OFFSET;
			}
			else
			{
				m_AlphaCameraGO.transform.position = m_CameraGO.transform.position;
			}
			m_AlphaCameraGO.transform.localRotation = Quaternion.identity;
		}
	}

	private void AlphaCameraRender(RenderTexture targetTexture)
	{
		m_AlphaCamera.targetTexture = targetTexture;
		m_AlphaCamera.orthographicSize = OrthoSize();
		m_AlphaCamera.farClipPlane = m_FarClip * m_WorldScale.z;
		m_AlphaCamera.nearClipPlane = m_NearClip * m_WorldScale.z;
		if ((bool)m_PlaneGameObject && !m_HideRenderObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
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
				text = m_ReplacmentTag;
			}
			m_AlphaCamera.RenderWithShader(m_AlphaFillShader, text);
		}
		else
		{
			m_AlphaCamera.Render();
		}
		if ((bool)m_PlaneGameObject && !m_HideRenderObject)
		{
			m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
			if ((bool)m_BloomPlaneGameObject)
			{
				m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
		m_AlphaCamera.targetTexture = null;
	}

	private void CreateBloomCaptureCamera()
	{
		if (!(m_BloomCaptureCamera != null))
		{
			m_BloomCaptureCameraGO = new GameObject();
			m_BloomCaptureCamera = m_BloomCaptureCameraGO.AddComponent<Camera>();
			m_BloomCaptureCameraGO.name = base.name + "_R2TBloomRenderCamera";
			m_BloomCaptureCamera.CopyFrom(m_Camera);
			m_BloomCaptureCamera.enabled = false;
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
		if (m_PlaneGameObject != null)
		{
			Object.DestroyImmediate(m_PlaneGameObject);
		}
		m_PlaneGameObject = CreateMeshPlane($"{base.name}_RenderPlane", m_Material);
		SceneUtils.SetHideFlags(m_PlaneGameObject, HideFlags.DontSave);
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
		m_BloomPlaneGameObject.transform.parent = m_PlaneGameObject.transform;
		m_BloomPlaneGameObject.transform.localPosition = new Vector3(0f, 0.15f, 0f);
		m_BloomPlaneGameObject.transform.localRotation = Quaternion.identity;
		m_BloomPlaneGameObject.transform.localScale = Vector3.one;
		m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial().color = m_BloomColor;
	}

	private void CreateBloomCapturePlane()
	{
		if (m_BloomCapturePlaneGameObject != null)
		{
			Object.DestroyImmediate(m_BloomCapturePlaneGameObject);
		}
		Material material = AdditiveMaterial;
		if (m_BloomBlend == BloomBlendType.Transparent)
		{
			material = TransparentMaterial;
		}
		m_BloomCapturePlaneGameObject = CreateMeshPlane($"{base.name}_BloomCaptureRenderPlane", material);
		m_BloomCapturePlaneGameObject.transform.parent = m_BloomCaptureCameraGO.transform;
		m_BloomCapturePlaneGameObject.transform.localPosition = Vector3.zero;
		m_BloomCapturePlaneGameObject.transform.localRotation = Quaternion.identity;
		m_BloomCapturePlaneGameObject.transform.Rotate(-90f, 0f, 0f);
		m_BloomCapturePlaneGameObject.transform.localScale = m_WorldScale;
		if ((bool)m_Material)
		{
			m_BloomCapturePlaneGameObject.GetComponent<Renderer>().SetMaterial(m_PlaneGameObject.GetComponent<Renderer>().GetMaterial());
		}
		if ((bool)m_RenderTexture)
		{
			m_BloomCapturePlaneGameObject.GetComponent<Renderer>().GetMaterial().mainTexture = m_RenderTexture;
		}
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
		mesh3.RecalculateBounds();
		Renderer component = gameObject.GetComponent<Renderer>();
		if ((bool)material)
		{
			component.SetMaterial(material);
		}
		component.sortingOrder = m_RenderQueue;
		component.GetMaterial().renderQueue = m_RenderQueueOffset + m_RenderQueue;
		m_previousRenderQueue = m_RenderQueue;
		return gameObject;
	}

	public void EnablePopupRendering(PopupRoot popupRoot)
	{
		m_popupRoot = popupRoot;
	}

	public void DisablePopupRendering()
	{
		m_popupRoot = null;
	}

	public bool ShouldPropagatePopupRendering()
	{
		return true;
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
			Debug.LogError(string.Format(" \"{0}\": RenderToTexture has a world scale of zero. \nm_WorldWidth: {1},   m_WorldHeight: {2}", m_WorldWidth, m_WorldHeight));
		}
	}

	private void CleanUp()
	{
		ReleaseTexture();
		if (m_hasMaterialInstance)
		{
			if (!GetMaterialManagerService().CanIgnoreMaterial(m_Material))
			{
				Object.Destroy(m_Material);
			}
			m_hasMaterialInstance = false;
		}
		if ((bool)m_CameraGO)
		{
			Object.Destroy(m_CameraGO);
		}
		if ((bool)m_AlphaCameraGO)
		{
			Object.Destroy(m_AlphaCameraGO);
		}
		if ((bool)m_PlaneGameObject)
		{
			Object.Destroy(m_PlaneGameObject);
		}
		if ((bool)m_BloomPlaneGameObject)
		{
			Object.Destroy(m_BloomPlaneGameObject);
		}
		if ((bool)m_BloomCaptureCameraGO)
		{
			Object.Destroy(m_BloomCaptureCameraGO);
		}
		if ((bool)m_BloomCapturePlaneGameObject)
		{
			Object.Destroy(m_BloomCapturePlaneGameObject);
		}
		if ((bool)m_OffscreenGameObject)
		{
			Object.Destroy(m_OffscreenGameObject);
		}
		if (m_ObjectToRender != null)
		{
			if (m_ObjectToRenderOrgParent != null)
			{
				m_ObjectToRender.transform.parent = m_ObjectToRenderOrgParent;
			}
			m_ObjectToRender.transform.localPosition = m_ObjectToRenderOrgPosition;
		}
		m_init = false;
		m_isDirty = true;
	}

	private static MaterialManagerService GetMaterialManagerService()
	{
		if (s_MaterialManagerService == null)
		{
			s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return s_MaterialManagerService;
	}
}
