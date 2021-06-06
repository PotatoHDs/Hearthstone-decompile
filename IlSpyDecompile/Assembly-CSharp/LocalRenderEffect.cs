using UnityEngine;

public class LocalRenderEffect : MonoBehaviour
{
	private readonly string ADDITIVE_SHADER_NAME = "Hero/Additive";

	private readonly string BLOOM_SHADER_NAME = "Hidden/LocalRenderBloom";

	private readonly string BLUR_SHADER_NAME = "Hidden/LocalRenderBlur";

	private readonly float RENDER_PLANE_OFFSET = 0.05f;

	public localRenderEffects m_Effect;

	public int m_Resolution = 128;

	public float m_Width = 1f;

	public float m_Height = 1f;

	public float m_Depth = 5f;

	public LayerMask m_LayerMask = -1;

	public Color m_Color = Color.gray;

	public float m_BlurAmount = 0.6f;

	private GameObject m_CameraGO;

	private Camera m_Camera;

	private RenderTexture m_RenderTexture;

	private Mesh m_PlaneMesh;

	private GameObject m_PlaneGameObject;

	private float m_PreviousWidth;

	private float m_PreviousHeight;

	private Shader m_BloomShader;

	private Material m_BloomMaterial;

	private Shader m_BlurShader;

	private Material m_BlurMaterial;

	private Shader m_AdditiveShader;

	private Material m_AdditiveMaterial;

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

	protected Material BloomMaterial
	{
		get
		{
			if (m_BloomMaterial == null)
			{
				m_BloomMaterial = new Material(m_BloomShader);
				SceneUtils.SetHideFlags(m_BloomMaterial, HideFlags.DontSave);
			}
			return m_BloomMaterial;
		}
	}

	protected Material BlurMaterial
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

	protected Material AdditiveMaterial
	{
		get
		{
			if (m_AdditiveMaterial == null)
			{
				m_AdditiveMaterial = new Material(m_AdditiveShader);
				SceneUtils.SetHideFlags(m_AdditiveMaterial, HideFlags.DontSave);
			}
			return m_AdditiveMaterial;
		}
	}

	protected void Start()
	{
		m_BloomShader = ShaderUtils.FindShader(BLOOM_SHADER_NAME);
		if (!m_BloomShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + BLOOM_SHADER_NAME);
		}
		m_BlurShader = ShaderUtils.FindShader(BLUR_SHADER_NAME);
		if (!m_BlurShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + BLUR_SHADER_NAME);
		}
		m_AdditiveShader = ShaderUtils.FindShader(ADDITIVE_SHADER_NAME);
		if (!m_AdditiveShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + ADDITIVE_SHADER_NAME);
		}
		CreateTexture();
		CreateCamera();
		CreateRenderPlane();
	}

	protected void Update()
	{
		Render();
	}

	protected void OnDestroy()
	{
		if ((bool)m_Camera)
		{
			m_Camera.targetTexture = null;
			m_Camera.enabled = false;
			Object.Destroy(m_Camera);
			Object.Destroy(m_CameraGO);
		}
		RenderTexture.active = null;
		if (m_BloomMaterial == null)
		{
			Object.Destroy(m_BloomMaterial);
		}
		if (m_BlurMaterial == null)
		{
			Object.Destroy(m_BlurMaterial);
		}
		if (m_AdditiveMaterial == null)
		{
			Object.Destroy(m_AdditiveMaterial);
		}
		if ((bool)m_RenderTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_RenderTexture);
		}
	}

	private void OnDrawGizmos()
	{
		if (m_Depth < 0f)
		{
			m_Depth = 0f;
		}
		Gizmos.DrawWireCube(new Vector3(base.transform.position.x, base.transform.position.y - m_Depth * 0.5f, base.transform.position.z), new Vector3(m_Width, m_Depth, m_Height));
	}

	public void Render()
	{
		if (m_Effect == localRenderEffects.Bloom || m_Effect == localRenderEffects.Blur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(m_RenderTexture.width, m_RenderTexture.height, m_RenderTexture.depth, m_RenderTexture.format);
			m_Camera.targetTexture = temporary;
			m_Camera.Render();
			Sample(temporary, m_RenderTexture, BlurMaterial, m_BlurAmount);
			RenderTexture.ReleaseTemporary(temporary);
			Renderer component = m_PlaneGameObject.GetComponent<Renderer>();
			component.SetSharedMaterial(BloomMaterial);
			Material sharedMaterial = component.GetSharedMaterial();
			sharedMaterial.SetColor("_TintColor", m_Color);
			sharedMaterial.mainTexture = m_RenderTexture;
		}
		else
		{
			m_Camera.targetTexture = m_RenderTexture;
			m_Camera.Render();
		}
	}

	private void CreateTexture()
	{
		if (!(m_RenderTexture != null))
		{
			Vector2 vector = CalcTextureSize();
			m_RenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, RenderTextureFormat.ARGB32);
		}
	}

	private void CreateCamera()
	{
		if (!(m_Camera != null))
		{
			m_CameraGO = new GameObject();
			m_Camera = m_CameraGO.AddComponent<Camera>();
			m_CameraGO.name = base.name + "_TextRenderCamera";
			SceneUtils.SetHideFlags(m_CameraGO, HideFlags.HideAndDontSave);
			m_Camera.orthographic = true;
			m_CameraGO.transform.parent = base.transform;
			m_CameraGO.transform.rotation = Quaternion.identity;
			m_CameraGO.transform.position = base.transform.position;
			m_CameraGO.transform.Rotate(90f, 0f, 0f);
			m_Camera.nearClipPlane = 0f;
			m_Camera.farClipPlane = m_Depth;
			m_Camera.depth = Camera.main.depth - 1f;
			m_Camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			m_Camera.clearFlags = CameraClearFlags.Color;
			m_Camera.depthTextureMode = DepthTextureMode.None;
			m_Camera.renderingPath = RenderingPath.Forward;
			m_Camera.cullingMask = m_LayerMask;
			m_Camera.allowHDR = false;
			m_Camera.targetTexture = m_RenderTexture;
			m_Camera.enabled = false;
			m_Camera.orthographicSize = Mathf.Min(m_Width * 0.5f, m_Height * 0.5f);
		}
	}

	private Vector2 CalcTextureSize()
	{
		float num = m_Height / m_Width;
		return new Vector2(m_Resolution, (float)m_Resolution * num);
	}

	private void CreateRenderPlane()
	{
		if (m_Width != m_PreviousWidth || m_Height != m_PreviousHeight)
		{
			if (m_PlaneGameObject != null)
			{
				Object.Destroy(m_PlaneGameObject);
			}
			m_PlaneGameObject = base.gameObject;
			m_PlaneGameObject.AddComponent<MeshFilter>();
			m_PlaneGameObject.AddComponent<MeshRenderer>();
			Mesh mesh = new Mesh();
			mesh.name = "TextMeshPlane";
			float num = m_Width * 0.5f;
			float num2 = m_Height * 0.5f;
			mesh.vertices = new Vector3[4]
			{
				new Vector3(0f - num, RENDER_PLANE_OFFSET, 0f - num2),
				new Vector3(num, RENDER_PLANE_OFFSET, 0f - num2),
				new Vector3(0f - num, RENDER_PLANE_OFFSET, num2),
				new Vector3(num, RENDER_PLANE_OFFSET, num2)
			};
			mesh.uv = PLANE_UVS;
			mesh.normals = PLANE_NORMALS;
			mesh.triangles = PLANE_TRIANGLES;
			Mesh mesh3 = (m_PlaneMesh = (m_PlaneGameObject.GetComponent<MeshFilter>().mesh = mesh));
			m_PlaneMesh.RecalculateBounds();
			BloomMaterial.mainTexture = m_RenderTexture;
			m_PlaneGameObject.GetComponent<Renderer>().SetSharedMaterial(AdditiveMaterial);
			m_PreviousWidth = m_Width;
			m_PreviousHeight = m_Height;
		}
	}

	private void Sample(RenderTexture source, RenderTexture dest, Material sampleMat, float offset)
	{
		dest.DiscardContents();
		Graphics.BlitMultiTap(source, dest, sampleMat, new Vector2(0f - offset, 0f - offset), new Vector2(0f - offset, offset), new Vector2(offset, offset), new Vector2(offset, 0f - offset));
	}
}
