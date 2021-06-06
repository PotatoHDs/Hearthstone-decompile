using System.Collections;
using UnityEngine;

public class ProjectedShadow : MonoBehaviour
{
	private const int RENDER_SIZE = 64;

	private const string SHADER_NAME = "Custom/ProjectedShadow";

	private const string CONTACT_SHADER_NAME = "Custom/ContactShadow";

	private const string SHADER_FALLOFF_RAMP = "Textures/ProjectedShadowRamp";

	private const string EDGE_FALLOFF_TEXTURE = "Textures/ProjectedShadowEdgeAlpha";

	private const string GAMEOBJECT_NAME_EXT = "ShadowProjector";

	private const string UNLIT_WHITE_SHADER_NAME = "Custom/Unlit/Color/White";

	private const string UNLIT_LIGHTGREY_SHADER_NAME = "Custom/Unlit/Color/LightGrey";

	private const string UNLIT_DARKGREY_SHADER_NAME = "Custom/Unlit/Color/DarkGrey";

	private const string MULTISAMPLE_SHADER_NAME = "Custom/Selection/HighlightMultiSample";

	private const float NEARCLIP_PLANE = 0f;

	private const float SHADOW_OFFSET_SCALE = 0.3f;

	private const float RENDERMASK_OFFSET = 0.11f;

	private const float RENDERMASK_BLUR = 0.6f;

	private const float RENDERMASK_BLUR2 = 0.8f;

	private const float CONTACT_SHADOW_SCALE = 0.98f;

	private const float CONTACT_SHADOW_FADE_IN_HEIGHT = 0.08f;

	private const float CONTACT_SHADOW_INTENSITY = 3.5f;

	private const int CONTACT_SHADOW_RESOLUTION = 80;

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

	public float m_ShadowProjectorSize = 1.5f;

	public bool m_ShadowEnabled;

	public bool m_AutoBoardHeightDisable;

	public float m_AutoDisableHeight;

	public bool m_ContinuousRendering;

	public float m_ProjectionFarClip = 10f;

	public Vector3 m_ProjectionOffset;

	public bool m_ContactShadow;

	public Vector3 m_ContactOffset = Vector3.zero;

	public bool m_isDirtyContactShadow = true;

	public bool m_enabledAlongsideRealtimeShadows;

	private static float s_offset = -12000f;

	private static Color s_ShadowColor = new Color(0.098f, 0.098f, 0.235f, 0.45f);

	private GameObject m_RootObject;

	private GameObject m_ProjectorGameObject;

	private Transform m_ProjectorTransform;

	private Projector m_Projector;

	private Camera m_Camera;

	private RenderTexture m_ShadowTexture;

	private RenderTexture m_ContactShadowTexture;

	private float m_AdjustedShadowProjectorSize = 1.5f;

	private float m_BoardHeight = 0.2f;

	private bool m_HasBoardHeight;

	private Mesh m_PlaneMesh;

	private GameObject m_PlaneGameObject;

	private Texture2D m_ShadowFalloffRamp;

	private Texture2D m_EdgeFalloffTexture;

	private Shader m_ShadowShader;

	private Shader m_UnlitWhiteShader;

	private Shader m_UnlitDarkGreyShader;

	private Shader m_UnlitLightGreyShader;

	private Material m_ShadowMaterial;

	private Shader m_ContactShadowShader;

	private Material m_ContactShadowMaterial;

	private Shader m_MultiSampleShader;

	private Material m_MultiSampleMaterial;

	protected Material ShadowMaterial
	{
		get
		{
			if (m_ShadowMaterial == null)
			{
				m_ShadowMaterial = new Material(m_ShadowShader);
				SceneUtils.SetHideFlags(m_ShadowMaterial, HideFlags.DontSave);
				m_ShadowMaterial.SetTexture("_Ramp", m_ShadowFalloffRamp);
				m_ShadowMaterial.SetTexture("_MainTex", m_ShadowTexture);
				m_ShadowMaterial.SetColor("_Color", s_ShadowColor);
				m_ShadowMaterial.SetTexture("_Edge", m_EdgeFalloffTexture);
			}
			return m_ShadowMaterial;
		}
	}

	protected Material ContactShadowMaterial
	{
		get
		{
			if (m_ContactShadowMaterial == null)
			{
				m_ContactShadowMaterial = new Material(m_ContactShadowShader);
				m_ContactShadowMaterial.SetFloat("_Intensity", 3.5f);
				m_ContactShadowMaterial.SetColor("_Color", s_ShadowColor);
				SceneUtils.SetHideFlags(m_ContactShadowMaterial, HideFlags.DontSave);
			}
			return m_ContactShadowMaterial;
		}
	}

	protected Material MultiSampleMaterial
	{
		get
		{
			if (m_MultiSampleMaterial == null)
			{
				m_MultiSampleMaterial = new Material(m_MultiSampleShader);
				SceneUtils.SetHideFlags(m_MultiSampleMaterial, HideFlags.DontSave);
			}
			return m_MultiSampleMaterial;
		}
	}

	protected void Start()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
		}
		if (m_ShadowShader == null)
		{
			m_ShadowShader = ShaderUtils.FindShader("Custom/ProjectedShadow");
		}
		if (!m_ShadowShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/ProjectedShadow");
			base.enabled = false;
		}
		if (m_ContactShadowShader == null)
		{
			m_ContactShadowShader = ShaderUtils.FindShader("Custom/ContactShadow");
		}
		if (!m_ContactShadowShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/ContactShadow");
			base.enabled = false;
		}
		if (m_ShadowFalloffRamp == null)
		{
			m_ShadowFalloffRamp = Resources.Load("Textures/ProjectedShadowRamp") as Texture2D;
		}
		if (!m_ShadowFalloffRamp)
		{
			Debug.LogError("Failed to load Projected Shadow Ramp: Textures/ProjectedShadowRamp");
			base.enabled = false;
		}
		if (m_EdgeFalloffTexture == null)
		{
			m_EdgeFalloffTexture = Resources.Load("Textures/ProjectedShadowEdgeAlpha") as Texture2D;
		}
		if (!m_EdgeFalloffTexture)
		{
			Debug.LogError("Failed to load Projected Shadow Edge Falloff Texture: Textures/ProjectedShadowEdgeAlpha");
			base.enabled = false;
		}
		if (m_MultiSampleShader == null)
		{
			m_MultiSampleShader = ShaderUtils.FindShader("Custom/Selection/HighlightMultiSample");
		}
		if (!m_MultiSampleShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Selection/HighlightMultiSample");
			base.enabled = false;
		}
		m_UnlitWhiteShader = ShaderUtils.FindShader("Custom/Unlit/Color/White");
		if (!m_UnlitWhiteShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/White");
		}
		m_UnlitLightGreyShader = ShaderUtils.FindShader("Custom/Unlit/Color/LightGrey");
		if (!m_UnlitLightGreyShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/LightGrey");
		}
		m_UnlitDarkGreyShader = ShaderUtils.FindShader("Custom/Unlit/Color/DarkGrey");
		if (!m_UnlitDarkGreyShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/DarkGrey");
		}
		if (Board.Get() != null)
		{
			StartCoroutine(AssignBoardHeight_WaitForBoardStandardGameLoaded());
		}
		Actor component = GetComponent<Actor>();
		if (component != null)
		{
			m_RootObject = component.GetRootObject();
		}
		else
		{
			GameObject gameObject = SceneUtils.FindChildBySubstring(base.gameObject, "RootObject");
			if (gameObject != null)
			{
				m_RootObject = gameObject;
			}
			else
			{
				m_RootObject = base.gameObject;
			}
		}
		m_ShadowMaterial = ShadowMaterial;
	}

	private IEnumerator AssignBoardHeight_WaitForBoardStandardGameLoaded()
	{
		if (!HearthstoneServices.TryGet<SceneMgr>(out var sceneMgr))
		{
			yield break;
		}
		while (sceneMgr.GetMode() == SceneMgr.Mode.GAMEPLAY && Gameplay.Get().GetBoardLayout() == null)
		{
			yield return null;
		}
		if (sceneMgr.GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			Transform transform = Board.Get().FindBone("CenterPointBone");
			if (transform != null)
			{
				m_BoardHeight = transform.position.y;
				m_HasBoardHeight = true;
			}
		}
	}

	protected void LateUpdate()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
			return;
		}
		Render();
		if (m_ContactShadow)
		{
			RenderContactShadow();
		}
	}

	private void OnDisable()
	{
		if (m_Projector != null)
		{
			m_Projector.enabled = false;
		}
		if ((bool)m_PlaneMesh)
		{
			Object.DestroyImmediate(m_PlaneMesh);
			Object.DestroyImmediate(m_PlaneGameObject.GetComponent<MeshFilter>().mesh);
			m_PlaneGameObject.GetComponent<MeshFilter>().mesh = null;
			m_PlaneMesh = null;
		}
		if ((bool)m_PlaneGameObject)
		{
			Object.DestroyImmediate(m_PlaneGameObject);
			m_PlaneGameObject = null;
		}
		if (RenderTexture.active == m_ShadowTexture || RenderTexture.active == m_ContactShadowTexture)
		{
			RenderTexture.active = null;
		}
		if ((bool)m_ShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_ShadowTexture);
			m_ShadowTexture = null;
		}
		if ((bool)m_ContactShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_ContactShadowTexture);
			m_ContactShadowTexture = null;
		}
	}

	protected void OnDestroy()
	{
		if ((bool)m_ContactShadowMaterial)
		{
			Object.Destroy(m_ContactShadowMaterial);
		}
		if ((bool)m_ShadowMaterial)
		{
			Object.Destroy(m_ShadowMaterial);
		}
		if ((bool)m_MultiSampleMaterial)
		{
			Object.Destroy(m_MultiSampleMaterial);
		}
		if ((bool)m_Camera)
		{
			Object.Destroy(m_Camera.gameObject);
		}
		if ((bool)m_ProjectorGameObject)
		{
			Object.Destroy(m_ProjectorGameObject);
		}
		if ((bool)m_ShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_ShadowTexture);
			m_ShadowTexture = null;
		}
		if ((bool)m_ContactShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_ContactShadowTexture);
			m_ContactShadowTexture = null;
		}
		if ((bool)m_PlaneMesh)
		{
			Object.DestroyImmediate(m_PlaneMesh);
			Object.DestroyImmediate(m_PlaneGameObject.GetComponent<MeshFilter>().mesh);
			m_PlaneGameObject.GetComponent<MeshFilter>().mesh = null;
			m_PlaneMesh = null;
		}
		if ((bool)m_PlaneGameObject)
		{
			Object.DestroyImmediate(m_PlaneGameObject);
			m_PlaneGameObject = null;
		}
	}

	private void OnDrawGizmos()
	{
		float num = m_ShadowProjectorSize * TransformUtil.ComputeWorldScale(base.transform).x * 2f;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = new Color(0.6f, 0.15f, 0.6f);
		if (m_ContactShadow)
		{
			Gizmos.DrawWireCube(m_ContactOffset, new Vector3(num, 0f, num));
		}
		else
		{
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(num, 0f, num));
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	public void Render()
	{
		if (!m_ShadowEnabled || ((bool)m_RootObject && !m_RootObject.activeSelf))
		{
			if ((bool)m_Projector && m_Projector.enabled)
			{
				m_Projector.enabled = false;
			}
			if ((bool)m_PlaneGameObject)
			{
				m_PlaneGameObject.SetActive(value: false);
			}
			return;
		}
		float x = TransformUtil.ComputeWorldScale(base.transform).x;
		m_AdjustedShadowProjectorSize = m_ShadowProjectorSize * x;
		if (m_Projector == null)
		{
			CreateProjector();
		}
		if (m_Camera == null)
		{
			CreateCamera();
		}
		float y = base.transform.position.y;
		float num = (m_HasBoardHeight ? m_BoardHeight : (y - Mathf.Max(0f, m_AutoDisableHeight) - float.Epsilon));
		float num2 = (y - num) * 0.3f;
		m_AdjustedShadowProjectorSize += Mathf.Lerp(0f, 0.5f, num2 * 0.5f);
		if (m_ContactShadow)
		{
			float num3 = num + 0.08f;
			if (num2 < num3)
			{
				if (m_PlaneGameObject == null)
				{
					m_isDirtyContactShadow = true;
				}
				else if (!m_PlaneGameObject.activeSelf)
				{
					m_isDirtyContactShadow = true;
				}
				float value = Mathf.Clamp((num3 - num2) / 0.08f, 0f, 1f);
				if ((bool)m_ContactShadowTexture && (bool)m_PlaneGameObject)
				{
					Renderer component = m_PlaneGameObject.GetComponent<Renderer>();
					Material material = (component ? component.GetSharedMaterial() : null);
					if ((bool)material)
					{
						material.mainTexture = m_ContactShadowTexture;
						material.color = s_ShadowColor;
						material.SetFloat("_Alpha", value);
					}
				}
			}
			else if (m_PlaneGameObject != null)
			{
				m_PlaneGameObject.SetActive(value: false);
			}
		}
		if (num2 < m_AutoDisableHeight && m_AutoBoardHeightDisable)
		{
			m_Projector.enabled = false;
			Object.DestroyImmediate(m_ShadowTexture);
			m_ShadowTexture = null;
			return;
		}
		m_Projector.enabled = true;
		float num4 = 0f;
		if (base.transform.parent != null)
		{
			num4 = Mathf.Lerp(-0.7f, 1.8f, base.transform.parent.position.x / 17f * -1f) * num2;
		}
		Vector3 position = new Vector3(base.transform.position.x - num4 - num2 * 0.25f, base.transform.position.y, base.transform.position.z - num2 * 0.8f);
		m_ProjectorTransform.position = position;
		m_ProjectorTransform.Translate(m_ProjectionOffset);
		if (!m_ContinuousRendering)
		{
			Quaternion rotation = base.transform.rotation;
			float num5 = (1f - rotation.z) * 0.5f + 0.5f;
			float num6 = rotation.x * 0.5f;
			m_Projector.aspectRatio = num5 - num6;
			m_Projector.orthographicSize = m_AdjustedShadowProjectorSize + num6;
			m_ProjectorTransform.rotation = Quaternion.identity;
			m_ProjectorTransform.Rotate(90f, rotation.eulerAngles.y, 0f);
		}
		else
		{
			m_ProjectorTransform.rotation = Quaternion.identity;
			m_ProjectorTransform.Rotate(90f, 0f, 0f);
			m_Projector.orthographicSize = m_AdjustedShadowProjectorSize;
		}
		int num7 = 64;
		if (m_ShadowTexture == null)
		{
			m_ShadowTexture = RenderTextureTracker.Get().CreateNewTexture(num7, num7, 32);
			m_ShadowTexture.wrapMode = TextureWrapMode.Clamp;
			RenderShadowMask();
		}
		else if (m_ContinuousRendering || !m_ShadowTexture.IsCreated())
		{
			RenderShadowMask();
		}
	}

	public static void SetShadowColor(Color color)
	{
		s_ShadowColor = color;
	}

	public void EnableShadow()
	{
		m_ShadowEnabled = true;
	}

	public void EnableShadow(float FadeInTime)
	{
		m_ShadowEnabled = true;
		Hashtable args = iTween.Hash("from", 0, "to", 1, "time", FadeInTime, "easetype", iTween.EaseType.easeInCubic, "onupdate", "UpdateShadowColor", "onupdatetarget", base.gameObject, "name", "ProjectedShadowFade");
		iTween.StopByName(base.gameObject, "ProjectedShadowFade");
		iTween.ValueTo(base.gameObject, args);
	}

	public void DisableShadow()
	{
		DisableShadowProjector();
	}

	public void DisableShadow(float FadeOutTime)
	{
		if (!(m_Projector == null) && m_ShadowEnabled)
		{
			Hashtable args = iTween.Hash("from", 1, "to", 0, "time", FadeOutTime, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "UpdateShadowColor", "onupdatetarget", base.gameObject, "name", "ProjectedShadowFade", "oncomplete", "DisableShadowProjector");
			iTween.StopByName(base.gameObject, "ProjectedShadowFade");
			iTween.ValueTo(base.gameObject, args);
		}
	}

	public void UpdateContactShadow(Spell spell, SpellStateType prevStateType, object userData)
	{
		UpdateContactShadow();
	}

	public void UpdateContactShadow(Spell spell, object userData)
	{
		UpdateContactShadow();
	}

	public void UpdateContactShadow(Spell spell)
	{
		UpdateContactShadow();
	}

	public void UpdateContactShadow()
	{
		if (m_ContactShadow)
		{
			m_isDirtyContactShadow = true;
		}
	}

	private void DisableShadowProjector()
	{
		if (m_Projector != null)
		{
			m_Projector.enabled = false;
		}
		m_ShadowEnabled = false;
	}

	private void UpdateShadowColor(float val)
	{
		if (!(m_Projector == null) && !(m_Projector.material == null))
		{
			Color value = Color.Lerp(new Color(0.5f, 0.5f, 0.5f, 0.5f), s_ShadowColor, val);
			m_Projector.material.SetColor("_Color", value);
		}
	}

	private void RenderShadowMask()
	{
		m_ShadowTexture.DiscardContents();
		m_Camera.depth = Camera.main.depth - 3f;
		m_Camera.clearFlags = CameraClearFlags.Color;
		Vector3 position = base.transform.position;
		Vector3 localScale = base.transform.localScale;
		s_offset -= 10f;
		if (s_offset < -19000f)
		{
			s_offset = -12000f;
		}
		Vector3 position2 = Vector3.left * s_offset;
		base.transform.position = position2;
		m_Camera.transform.position = position2;
		m_Camera.transform.rotation = Quaternion.identity;
		m_Camera.transform.Rotate(90f, 0f, 0f);
		RenderTexture temporary = RenderTexture.GetTemporary(80, 80);
		RenderTexture temporary2 = RenderTexture.GetTemporary(80, 80);
		m_Camera.targetTexture = temporary;
		float x = TransformUtil.ComputeWorldScale(base.transform).x;
		m_Camera.orthographicSize = m_ShadowProjectorSize * x - 0.11f - 0.05f;
		m_Camera.RenderWithShader(m_UnlitWhiteShader, "Highlight");
		Sample(temporary, temporary2, 0.6f);
		Sample(temporary2, m_ShadowTexture, 0.8f);
		ShadowMaterial.SetTexture("_MainTex", m_ShadowTexture);
		ShadowMaterial.SetColor("_Color", s_ShadowColor);
		base.transform.position = position;
		base.transform.localScale = localScale;
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	private IEnumerator DelayRenderContactShadow()
	{
		yield return null;
		m_isDirtyContactShadow = true;
	}

	private void RenderContactShadow()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
		}
		if (!(m_ContactShadowTexture != null) || m_isDirtyContactShadow || !m_ContactShadowTexture.IsCreated())
		{
			float x = TransformUtil.ComputeWorldScale(base.transform).x;
			m_AdjustedShadowProjectorSize = m_ShadowProjectorSize * x;
			if (m_Camera == null)
			{
				CreateCamera();
			}
			if (m_PlaneGameObject == null)
			{
				CreateRenderPlane();
			}
			m_PlaneGameObject.SetActive(value: true);
			if (m_ContactShadowTexture == null)
			{
				m_ContactShadowTexture = RenderTextureTracker.Get().CreateNewTexture(80, 80, 32);
			}
			Quaternion localRotation = base.transform.localRotation;
			Vector3 position = base.transform.position;
			Vector3 localScale = base.transform.localScale;
			s_offset -= 10f;
			if (s_offset < -19000f)
			{
				s_offset = -12000f;
			}
			Vector3 position2 = Vector3.left * s_offset;
			base.transform.position = position2;
			base.transform.rotation = Quaternion.identity;
			SetWorldScale(base.transform, Vector3.one);
			m_Camera.transform.position = position2;
			m_Camera.transform.rotation = Quaternion.identity;
			m_Camera.transform.Rotate(90f, 0f, 0f);
			RenderTexture temporary = RenderTexture.GetTemporary(80, 80);
			m_Camera.depth = Camera.main.depth - 3f;
			m_Camera.clearFlags = CameraClearFlags.Color;
			m_Camera.targetTexture = temporary;
			m_Camera.orthographicSize = m_ShadowProjectorSize - 0.11f - 0.05f;
			m_Camera.RenderWithShader(m_UnlitDarkGreyShader, "Highlight");
			m_ContactShadowTexture.DiscardContents();
			Sample(temporary, m_ContactShadowTexture, 0.6f);
			base.transform.localRotation = localRotation;
			base.transform.position = position;
			base.transform.localScale = localScale;
			m_PlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().mainTexture = m_ContactShadowTexture;
			m_isDirtyContactShadow = false;
			RenderTexture.ReleaseTemporary(temporary);
		}
	}

	private void Sample(RenderTexture source, RenderTexture dest, float off)
	{
		Graphics.BlitMultiTap(source, dest, MultiSampleMaterial, new Vector2(0f - off, 0f - off), new Vector2(0f - off, off), new Vector2(off, off), new Vector2(off, 0f - off));
	}

	private void CreateProjector()
	{
		if (m_Projector != null)
		{
			Object.Destroy(m_Projector);
			m_Projector = null;
		}
		if (m_ProjectorGameObject != null)
		{
			Object.Destroy(m_ProjectorGameObject);
			m_ProjectorGameObject = null;
			m_ProjectorTransform = null;
		}
		m_ProjectorGameObject = new GameObject(string.Format("{0}_{1}", base.name, "ShadowProjector"));
		m_Projector = m_ProjectorGameObject.AddComponent<Projector>();
		m_ProjectorTransform = m_ProjectorGameObject.transform;
		m_ProjectorTransform.Rotate(90f, 0f, 0f);
		if (m_RootObject != null)
		{
			m_ProjectorTransform.parent = m_RootObject.transform;
		}
		m_Projector.nearClipPlane = 0f;
		m_Projector.farClipPlane = m_ProjectionFarClip;
		m_Projector.orthographic = true;
		m_Projector.orthographicSize = m_AdjustedShadowProjectorSize;
		SceneUtils.SetHideFlags(m_Projector, HideFlags.HideAndDontSave);
		m_Projector.material = m_ShadowMaterial;
	}

	private void CreateCamera()
	{
		if (m_Camera != null)
		{
			Object.Destroy(m_Camera);
		}
		GameObject gameObject = new GameObject();
		m_Camera = gameObject.AddComponent<Camera>();
		gameObject.name = base.name + "_ShadowCamera";
		SceneUtils.SetHideFlags(gameObject, HideFlags.HideAndDontSave);
		m_Camera.orthographic = true;
		m_Camera.orthographicSize = m_AdjustedShadowProjectorSize;
		m_Camera.transform.position = base.transform.position;
		m_Camera.transform.rotation = base.transform.rotation;
		m_Camera.transform.Rotate(90f, 0f, 0f);
		if (m_RootObject != null)
		{
			m_Camera.transform.parent = m_RootObject.transform;
		}
		m_Camera.nearClipPlane = -3f;
		m_Camera.farClipPlane = 3f;
		if (Camera.main != null)
		{
			m_Camera.depth = Camera.main.depth - 5f;
		}
		else
		{
			m_Camera.depth = -4f;
		}
		m_Camera.backgroundColor = Color.black;
		m_Camera.clearFlags = CameraClearFlags.Color;
		m_Camera.depthTextureMode = DepthTextureMode.None;
		m_Camera.renderingPath = RenderingPath.Forward;
		m_Camera.allowHDR = false;
		m_Camera.SetReplacementShader(m_UnlitWhiteShader, "Highlight");
		m_Camera.enabled = false;
	}

	private void CreateRenderPlane()
	{
		if (m_PlaneGameObject != null)
		{
			Object.DestroyImmediate(m_PlaneGameObject);
		}
		m_PlaneGameObject = new GameObject();
		m_PlaneGameObject.name = base.name + "_ContactShadowRenderPlane";
		if (m_RootObject != null)
		{
			m_PlaneGameObject.transform.parent = m_RootObject.transform;
		}
		m_PlaneGameObject.transform.localPosition = m_ContactOffset;
		m_PlaneGameObject.transform.localRotation = Quaternion.identity;
		m_PlaneGameObject.transform.localScale = new Vector3(0.98f, 1f, 0.98f);
		m_PlaneGameObject.AddComponent<MeshFilter>();
		m_PlaneGameObject.AddComponent<MeshRenderer>();
		SceneUtils.SetHideFlags(m_PlaneGameObject, HideFlags.HideAndDontSave);
		Mesh mesh = new Mesh();
		mesh.name = "ContactShadowMeshPlane";
		float shadowProjectorSize = m_ShadowProjectorSize;
		float shadowProjectorSize2 = m_ShadowProjectorSize;
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - shadowProjectorSize, 0f, 0f - shadowProjectorSize2),
			new Vector3(shadowProjectorSize, 0f, 0f - shadowProjectorSize2),
			new Vector3(0f - shadowProjectorSize, 0f, shadowProjectorSize2),
			new Vector3(shadowProjectorSize, 0f, shadowProjectorSize2)
		};
		mesh.uv = PLANE_UVS;
		mesh.normals = PLANE_NORMALS;
		mesh.triangles = PLANE_TRIANGLES;
		Mesh mesh3 = (m_PlaneMesh = (m_PlaneGameObject.GetComponent<MeshFilter>().mesh = mesh));
		m_PlaneMesh.RecalculateBounds();
		m_ContactShadowMaterial = ContactShadowMaterial;
		m_ContactShadowMaterial.color = s_ShadowColor;
		if ((bool)m_ContactShadowMaterial)
		{
			m_PlaneGameObject.GetComponent<Renderer>().SetSharedMaterial(m_ContactShadowMaterial);
		}
	}

	public void SetWorldScale(Transform xform, Vector3 scale)
	{
		GameObject obj = new GameObject();
		Transform transform = obj.transform;
		transform.parent = null;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		Transform parent = xform.parent;
		xform.parent = transform;
		xform.localScale = scale;
		xform.parent = parent;
		Object.Destroy(obj);
	}
}
