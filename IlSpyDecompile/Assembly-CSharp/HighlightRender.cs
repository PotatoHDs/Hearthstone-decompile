using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighlightRender : MonoBehaviour
{
	private readonly string MULTISAMPLE_SHADER_NAME = "Custom/Selection/HighlightMultiSample";

	private readonly string MULTISAMPLE_BLEND_SHADER_NAME = "Custom/Selection/HighlightMultiSampleBlend";

	private readonly string BLEND_SHADER_NAME = "Custom/Selection/HighlightMaskBlend";

	private readonly string HIGHLIGHT_SHADER_NAME = "Custom/Selection/Highlight";

	private readonly string UNLIT_COLOR_SHADER_NAME = "Custom/UnlitColor";

	private readonly string UNLIT_GREY_SHADER_NAME = "Custom/Unlit/Color/Grey";

	private readonly string UNLIT_LIGHTGREY_SHADER_NAME = "Custom/Unlit/Color/LightGrey";

	private readonly string UNLIT_DARKGREY_SHADER_NAME = "Custom/Unlit/Color/DarkGrey";

	private readonly string UNLIT_BLACK_SHADER_NAME = "Custom/Unlit/Color/BlackOverlay";

	private readonly string UNLIT_WHITE_SHADER_NAME = "Custom/Unlit/Color/White";

	private const float RENDER_SIZE1 = 0.3f;

	private const float RENDER_SIZE2 = 0.3f;

	private const float RENDER_SIZE3 = 0.5f;

	private const float RENDER_SIZE4 = 0.92f;

	private const float ORTHO_SIZE1 = 0.2f;

	private const float ORTHO_SIZE2 = 0.25f;

	private const float ORTHO_SIZE3 = 0.01f;

	private const float ORTHO_SIZE4 = -0.05f;

	private const float BLUR_BLEND1 = 1.25f;

	private const float BLUR_BLEND2 = 1.25f;

	private const float BLUR_BLEND3 = 1f;

	private const float BLUR_BLEND4 = 1.5f;

	private const int SILHOUETTE_RENDER_SIZE = 200;

	private const int SILHOUETTE_RENDER_DEPTH = 24;

	private const int MAX_HIGHLIGHT_EXCLUDE_PARENT_SEARCH = 25;

	public Transform m_RootTransform;

	public float m_SilouetteRenderSize = 1f;

	public float m_SilouetteClipSize = 1f;

	private GameObject m_RenderPlane;

	private float m_RenderScale = 1f;

	private Vector3 m_OrgPosition;

	private Quaternion m_OrgRotation;

	private Vector3 m_OrgScale;

	private Shader m_MultiSampleShader;

	private Shader m_MultiSampleBlendShader;

	private Shader m_BlendShader;

	private Shader m_HighlightShader;

	private Shader m_UnlitColorShader;

	private Shader m_UnlitGreyShader;

	private Shader m_UnlitLightGreyShader;

	private Shader m_UnlitDarkGreyShader;

	private Shader m_UnlitBlackShader;

	private Shader m_UnlitWhiteShader;

	private RenderTexture m_CameraTexture;

	private Camera m_Camera;

	private float m_CameraOrthoSize;

	private Map<Renderer, bool> m_VisibilityStates;

	private Map<Transform, Vector3> m_ObjectsOrginalPosition;

	private int m_RenderSizeX = 200;

	private int m_RenderSizeY = 200;

	private bool m_Initialized;

	private static float s_offset = -2000f;

	private Material m_Material;

	private Material m_MultiSampleMaterial;

	private Material m_MultiSampleBlendMaterial;

	private Material m_BlendMaterial;

	protected Material HighlightMaterial
	{
		get
		{
			if (m_Material == null)
			{
				m_Material = new Material(m_HighlightShader);
				SceneUtils.SetHideFlags(m_Material, HideFlags.DontSave);
			}
			return m_Material;
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

	protected Material MultiSampleBlendMaterial
	{
		get
		{
			if (m_MultiSampleBlendMaterial == null)
			{
				m_MultiSampleBlendMaterial = new Material(m_MultiSampleBlendShader);
				SceneUtils.SetHideFlags(m_MultiSampleBlendMaterial, HideFlags.DontSave);
			}
			return m_MultiSampleBlendMaterial;
		}
	}

	protected Material BlendMaterial
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

	public RenderTexture SilhouetteTexture => m_CameraTexture;

	protected void OnApplicationFocus(bool state)
	{
	}

	protected void OnDisable()
	{
		if ((bool)m_Material)
		{
			Object.Destroy(m_Material);
		}
		if ((bool)m_MultiSampleMaterial)
		{
			Object.Destroy(m_MultiSampleMaterial);
		}
		if ((bool)m_BlendMaterial)
		{
			Object.Destroy(m_BlendMaterial);
		}
		if ((bool)m_MultiSampleBlendMaterial)
		{
			Object.Destroy(m_MultiSampleBlendMaterial);
		}
		if (m_VisibilityStates != null)
		{
			m_VisibilityStates.Clear();
		}
		if (m_CameraTexture != null)
		{
			if (RenderTexture.active == m_CameraTexture)
			{
				RenderTexture.active = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(m_CameraTexture);
			m_CameraTexture = null;
		}
		m_Initialized = false;
	}

	protected void Initialize()
	{
		if (m_Initialized)
		{
			return;
		}
		m_Initialized = true;
		if (m_HighlightShader == null)
		{
			m_HighlightShader = ShaderUtils.FindShader(HIGHLIGHT_SHADER_NAME);
		}
		if (!m_HighlightShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + HIGHLIGHT_SHADER_NAME);
			base.enabled = false;
		}
		GetComponent<Renderer>().GetMaterial().shader = m_HighlightShader;
		if (m_MultiSampleShader == null)
		{
			m_MultiSampleShader = ShaderUtils.FindShader(MULTISAMPLE_SHADER_NAME);
		}
		if (!m_MultiSampleShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + MULTISAMPLE_SHADER_NAME);
			base.enabled = false;
		}
		if (m_MultiSampleBlendShader == null)
		{
			m_MultiSampleBlendShader = ShaderUtils.FindShader(MULTISAMPLE_BLEND_SHADER_NAME);
		}
		if (!m_MultiSampleBlendShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + MULTISAMPLE_BLEND_SHADER_NAME);
			base.enabled = false;
		}
		if (m_BlendShader == null)
		{
			m_BlendShader = ShaderUtils.FindShader(BLEND_SHADER_NAME);
		}
		if (!m_BlendShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + BLEND_SHADER_NAME);
			base.enabled = false;
		}
		if (m_RootTransform == null)
		{
			Transform parent = base.transform.parent.parent;
			if ((bool)parent.GetComponent<ActorStateMgr>())
			{
				m_RootTransform = parent.parent;
			}
			else
			{
				m_RootTransform = parent;
			}
			if (m_RootTransform == null)
			{
				Debug.LogError("m_RootTransform is null. Highlighting disabled!");
				base.enabled = false;
			}
		}
		m_VisibilityStates = new Map<Renderer, bool>();
		HighlightSilhouetteInclude[] componentsInChildren = m_RootTransform.GetComponentsInChildren<HighlightSilhouetteInclude>();
		if (componentsInChildren != null)
		{
			HighlightSilhouetteInclude[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Renderer component = array[i].gameObject.GetComponent<Renderer>();
				if (!(component == null))
				{
					m_VisibilityStates.Add(component, value: false);
				}
			}
		}
		m_UnlitColorShader = ShaderUtils.FindShader(UNLIT_COLOR_SHADER_NAME);
		if (!m_UnlitColorShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_COLOR_SHADER_NAME);
		}
		m_UnlitGreyShader = ShaderUtils.FindShader(UNLIT_GREY_SHADER_NAME);
		if (!m_UnlitGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_GREY_SHADER_NAME);
		}
		m_UnlitLightGreyShader = ShaderUtils.FindShader(UNLIT_LIGHTGREY_SHADER_NAME);
		if (!m_UnlitLightGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_LIGHTGREY_SHADER_NAME);
		}
		m_UnlitDarkGreyShader = ShaderUtils.FindShader(UNLIT_DARKGREY_SHADER_NAME);
		if (!m_UnlitDarkGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_DARKGREY_SHADER_NAME);
		}
		m_UnlitBlackShader = ShaderUtils.FindShader(UNLIT_BLACK_SHADER_NAME);
		if (!m_UnlitBlackShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_BLACK_SHADER_NAME);
		}
		m_UnlitWhiteShader = ShaderUtils.FindShader(UNLIT_WHITE_SHADER_NAME);
		if (!m_UnlitWhiteShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + UNLIT_WHITE_SHADER_NAME);
		}
	}

	protected void Update()
	{
		if ((bool)m_CameraTexture && m_Initialized && !m_CameraTexture.IsCreated())
		{
			CreateSilhouetteTexture();
		}
	}

	[ContextMenu("Export Silhouette Texture")]
	public void ExportSilhouetteTexture()
	{
		RenderTexture.active = m_CameraTexture;
		Texture2D texture2D = new Texture2D(m_RenderSizeX, m_RenderSizeY, TextureFormat.RGB24, mipChain: false);
		texture2D.ReadPixels(new Rect(0f, 0f, m_RenderSizeX, m_RenderSizeY), 0, 0, recalculateMipMaps: false);
		texture2D.Apply();
		string text = Application.dataPath + "/SilhouetteTexture.png";
		File.WriteAllBytes(text, texture2D.EncodeToPNG());
		RenderTexture.active = null;
		Debug.Log($"Silhouette Texture Created: {text}");
	}

	public void CreateSilhouetteTexture()
	{
		CreateSilhouetteTexture(force: false);
	}

	public void CreateSilhouetteTexture(bool force)
	{
		Initialize();
		if (VisibilityStatesChanged() || force)
		{
			SetupRenderObjects();
			if (!(m_RenderPlane == null) && m_RenderSizeX >= 1 && m_RenderSizeY >= 1)
			{
				bool flag = GetComponent<Renderer>().enabled;
				GetComponent<Renderer>().enabled = false;
				RenderTexture temporary = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.3f), (int)((float)m_RenderSizeY * 0.3f), 24);
				RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.3f), (int)((float)m_RenderSizeY * 0.3f), 24);
				RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.3f), (int)((float)m_RenderSizeY * 0.3f), 24);
				RenderTexture temporary4 = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.5f), (int)((float)m_RenderSizeY * 0.5f), 24);
				RenderTexture temporary5 = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.5f), (int)((float)m_RenderSizeY * 0.5f), 24);
				RenderTexture temporary6 = RenderTexture.GetTemporary(m_RenderSizeX, m_RenderSizeY, 24);
				RenderTexture temporary7 = RenderTexture.GetTemporary(m_RenderSizeX, m_RenderSizeY, 24);
				RenderTexture temporary8 = RenderTexture.GetTemporary(m_RenderSizeX, m_RenderSizeY, 24);
				RenderTexture temporary9 = RenderTexture.GetTemporary((int)((float)m_RenderSizeX * 0.92f), (int)((float)m_RenderSizeY * 0.92f), 24);
				m_CameraTexture.DiscardContents();
				m_Camera.clearFlags = CameraClearFlags.Color;
				m_Camera.depth = Camera.main.depth - 1f;
				m_Camera.clearFlags = CameraClearFlags.Color;
				m_Camera.orthographicSize = m_CameraOrthoSize + 0.1f * m_SilouetteClipSize;
				m_Camera.targetTexture = temporary9;
				m_Camera.RenderWithShader(m_UnlitWhiteShader, "Highlight");
				m_Camera.depth = Camera.main.depth - 5f;
				m_Camera.orthographicSize = m_CameraOrthoSize - 0.2f * m_SilouetteRenderSize;
				m_Camera.targetTexture = temporary;
				m_Camera.RenderWithShader(m_UnlitDarkGreyShader, "Highlight");
				m_Camera.depth = Camera.main.depth - 4f;
				m_Camera.orthographicSize = m_CameraOrthoSize - 0.25f * m_SilouetteRenderSize;
				m_Camera.targetTexture = temporary2;
				m_Camera.RenderWithShader(m_UnlitGreyShader, "Highlight");
				SampleBlend(temporary, temporary2, temporary3, 1.25f);
				m_Camera.depth = Camera.main.depth - 3f;
				m_Camera.orthographicSize = m_CameraOrthoSize - 0.01f * m_SilouetteRenderSize;
				m_Camera.targetTexture = temporary4;
				m_Camera.RenderWithShader(m_UnlitLightGreyShader, "Highlight");
				SampleBlend(temporary3, temporary4, temporary5, 1.25f);
				m_Camera.depth = Camera.main.depth - 2f;
				m_Camera.orthographicSize = m_CameraOrthoSize - -0.05f * m_SilouetteRenderSize;
				m_Camera.targetTexture = temporary6;
				m_Camera.RenderWithShader(m_UnlitWhiteShader, "Highlight");
				SampleBlend(temporary5, temporary6, temporary7, 1f);
				Sample(temporary7, temporary8, 1.5f);
				BlendMaterial.SetTexture("_BlendTex", temporary9);
				float num = 0.8f;
				Graphics.BlitMultiTap(temporary8, m_CameraTexture, BlendMaterial, new Vector2(0f - num, 0f - num), new Vector2(0f - num, num), new Vector2(num, num), new Vector2(num, 0f - num));
				RenderTexture.ReleaseTemporary(temporary);
				RenderTexture.ReleaseTemporary(temporary2);
				RenderTexture.ReleaseTemporary(temporary3);
				RenderTexture.ReleaseTemporary(temporary4);
				RenderTexture.ReleaseTemporary(temporary5);
				RenderTexture.ReleaseTemporary(temporary6);
				RenderTexture.ReleaseTemporary(temporary7);
				RenderTexture.ReleaseTemporary(temporary8);
				RenderTexture.ReleaseTemporary(temporary9);
				m_Camera.orthographicSize = m_CameraOrthoSize;
				GetComponent<Renderer>().enabled = flag;
				RestoreRenderObjects();
			}
		}
	}

	public bool isTextureCreated()
	{
		if ((bool)m_CameraTexture)
		{
			return m_CameraTexture.IsCreated();
		}
		return false;
	}

	private void SetupRenderObjects()
	{
		if (m_RootTransform == null)
		{
			m_RenderPlane = null;
			return;
		}
		s_offset -= 10f;
		if (s_offset < -9000f)
		{
			s_offset = -2000f;
		}
		m_OrgPosition = m_RootTransform.position;
		m_OrgRotation = m_RootTransform.rotation;
		m_OrgScale = m_RootTransform.localScale;
		Vector3 position = Vector3.left * s_offset;
		m_RootTransform.position = position;
		SetWorldScale(m_RootTransform, Vector3.one);
		m_RootTransform.rotation = Quaternion.identity;
		Bounds bounds = GetComponent<Renderer>().bounds;
		float x = bounds.size.x;
		float num = bounds.size.z;
		if (num < bounds.size.y)
		{
			num = bounds.size.y;
		}
		if (x > num)
		{
			m_RenderSizeX = 200;
			m_RenderSizeY = (int)(200f * (num / x));
		}
		else
		{
			m_RenderSizeX = (int)(200f * (x / num));
			m_RenderSizeY = 200;
		}
		m_CameraOrthoSize = num * 0.5f;
		if (m_CameraTexture == null)
		{
			if (m_RenderSizeX < 1 || m_RenderSizeY < 1)
			{
				m_RenderSizeX = 200;
				m_RenderSizeY = 200;
			}
			m_CameraTexture = RenderTextureTracker.Get().CreateNewTexture(m_RenderSizeX, m_RenderSizeY, 24);
			m_CameraTexture.format = RenderTextureFormat.ARGB32;
		}
		HighlightState componentInChildren = m_RootTransform.GetComponentInChildren<HighlightState>();
		if (componentInChildren == null)
		{
			Debug.LogError("Can not find Highlight(HighlightState component) object for selection highlighting.");
			m_RenderPlane = null;
			return;
		}
		componentInChildren.transform.localPosition = Vector3.zero;
		HighlightRender componentInChildren2 = m_RootTransform.GetComponentInChildren<HighlightRender>();
		if (componentInChildren2 == null)
		{
			Debug.LogError("Can not find render plane object(HighlightRender component) for selection highlighting.");
			m_RenderPlane = null;
		}
		else
		{
			m_RenderPlane = componentInChildren2.gameObject;
			m_RenderScale = GetWorldScale(m_RenderPlane.transform).x;
			CreateCamera(componentInChildren2.transform);
		}
	}

	private void RestoreRenderObjects()
	{
		m_RootTransform.position = m_OrgPosition;
		m_RootTransform.rotation = m_OrgRotation;
		m_RootTransform.localScale = m_OrgScale;
		m_RenderPlane = null;
	}

	private bool VisibilityStatesChanged()
	{
		bool result = false;
		HighlightSilhouetteInclude[] componentsInChildren = m_RootTransform.GetComponentsInChildren<HighlightSilhouetteInclude>();
		List<Renderer> list = new List<Renderer>();
		HighlightSilhouetteInclude[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Renderer component = array[i].gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		foreach (Renderer item in list)
		{
			bool flag = item.enabled && item.gameObject.activeInHierarchy;
			if (!m_VisibilityStates.ContainsKey(item))
			{
				m_VisibilityStates.Add(item, flag);
				if (flag)
				{
					result = true;
				}
			}
			else if (m_VisibilityStates[item] != flag)
			{
				m_VisibilityStates[item] = flag;
				result = true;
			}
		}
		return result;
	}

	private List<GameObject> GetExcludedObjects()
	{
		List<GameObject> list = new List<GameObject>();
		HighlightExclude[] componentsInChildren = m_RootTransform.GetComponentsInChildren<HighlightExclude>();
		if (componentsInChildren == null)
		{
			return null;
		}
		HighlightExclude[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Transform[] componentsInChildren2 = array[i].GetComponentsInChildren<Transform>();
			if (componentsInChildren2 != null)
			{
				Transform[] array2 = componentsInChildren2;
				foreach (Transform transform in array2)
				{
					list.Add(transform.gameObject);
				}
			}
		}
		list.Add(base.gameObject);
		list.Add(base.transform.parent.gameObject);
		return list;
	}

	private bool isHighlighExclude(Transform objXform)
	{
		if (objXform == null)
		{
			return true;
		}
		HighlightExclude component = objXform.GetComponent<HighlightExclude>();
		if ((bool)component && component.enabled)
		{
			return true;
		}
		Transform parent = objXform.transform.parent;
		if (parent != null)
		{
			int num = 0;
			while ((parent != m_RootTransform || parent != null) && !(parent == null) && num <= 25)
			{
				HighlightExclude component2 = parent.GetComponent<HighlightExclude>();
				if (component2 != null && component2.ExcludeChildren)
				{
					return true;
				}
				parent = parent.parent;
				num++;
			}
		}
		return false;
	}

	private void CreateCamera(Transform renderPlane)
	{
		if (!(m_Camera != null))
		{
			GameObject gameObject = new GameObject();
			m_Camera = gameObject.AddComponent<Camera>();
			gameObject.name = renderPlane.name + "_SilhouetteCamera";
			SceneUtils.SetHideFlags(gameObject, HideFlags.HideAndDontSave);
			m_Camera.orthographic = true;
			m_Camera.orthographicSize = m_CameraOrthoSize;
			m_Camera.transform.position = renderPlane.position;
			m_Camera.transform.rotation = renderPlane.rotation;
			m_Camera.transform.Rotate(90f, 180f, 0f);
			m_Camera.transform.parent = renderPlane;
			m_Camera.nearClipPlane = 0f - m_RenderScale + 1f;
			m_Camera.farClipPlane = m_RenderScale + 1f;
			m_Camera.depth = Camera.main.depth - 5f;
			m_Camera.backgroundColor = Color.black;
			m_Camera.clearFlags = CameraClearFlags.Color;
			m_Camera.depthTextureMode = DepthTextureMode.None;
			m_Camera.renderingPath = RenderingPath.VertexLit;
			m_Camera.allowHDR = false;
			m_Camera.SetReplacementShader(m_UnlitColorShader, "");
			m_Camera.targetTexture = null;
			m_Camera.enabled = false;
		}
	}

	private void Sample(RenderTexture source, RenderTexture dest, float off)
	{
		if (!(source == null) && !(dest == null))
		{
			dest.DiscardContents();
			Graphics.BlitMultiTap(source, dest, MultiSampleMaterial, new Vector2(0f - off, 0f - off), new Vector2(0f - off, off), new Vector2(off, off), new Vector2(off, 0f - off));
		}
	}

	private void SampleBlend(RenderTexture source, RenderTexture blend, RenderTexture dest, float off)
	{
		if (!(source == null) && !(dest == null) && !(blend == null))
		{
			MultiSampleBlendMaterial.SetTexture("_BlendTex", blend);
			dest.DiscardContents();
			Graphics.BlitMultiTap(source, dest, MultiSampleBlendMaterial, new Vector2(0f - off, 0f - off), new Vector2(0f - off, off), new Vector2(off, off), new Vector2(off, 0f - off));
		}
	}

	public static Vector3 GetWorldScale(Transform transform)
	{
		Vector3 vector = transform.localScale;
		Transform parent = transform.parent;
		while (parent != null)
		{
			vector = Vector3.Scale(vector, parent.localScale);
			parent = parent.parent;
		}
		return vector;
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
