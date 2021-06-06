using UnityEngine;

public class CameraFade : MonoBehaviour
{
	public Color m_Color = Color.black;

	public float m_Fade = 1f;

	public bool m_RenderOverAll;

	private Texture2D m_TempTexture;

	private GameObject m_PlaneGameObject;

	private Material m_Material;

	private Camera m_Camera;

	private float m_CameraDepth = 14f;

	private Renderer m_Renderer;

	private void Awake()
	{
		m_TempTexture = new Texture2D(1, 1);
		m_TempTexture.SetPixel(0, 0, Color.white);
		m_TempTexture.Apply();
		m_Camera = GetComponent<Camera>();
		m_Renderer = GetComponent<Renderer>();
		if (m_Camera == null)
		{
			Debug.LogError("CameraFade faild to find camera component!");
			base.enabled = false;
		}
		m_CameraDepth = m_Camera.depth;
		SetupCamera();
	}

	private void Update()
	{
		if (m_Fade <= 0f)
		{
			if (m_Renderer != null && m_Renderer.enabled)
			{
				m_Renderer.enabled = false;
			}
			if (m_Camera.enabled)
			{
				m_Camera.enabled = false;
			}
			return;
		}
		if (m_Renderer == null)
		{
			CreateRenderPlane();
		}
		if (!m_Renderer.enabled)
		{
			m_Renderer.enabled = true;
		}
		if (!m_Camera.enabled)
		{
			m_Camera.enabled = true;
		}
		if (m_RenderOverAll)
		{
			if (m_Camera.depth < 100f)
			{
				m_Camera.depth = 100f;
			}
		}
		else if (m_Camera.depth != m_CameraDepth)
		{
			m_Camera.depth = m_CameraDepth;
		}
		Color color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Fade);
		m_Material.color = color;
	}

	private void CreateRenderPlane()
	{
		base.gameObject.AddComponent<MeshFilter>();
		m_Renderer = base.gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[4]
		{
			new Vector3(-10f, -10f, 0f),
			new Vector3(10f, -10f, 0f),
			new Vector3(-10f, 10f, 0f),
			new Vector3(10f, 10f, 0f)
		};
		mesh.colors = new Color[4]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white
		};
		mesh.uv = new Vector2[4]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.normals = new Vector3[4]
		{
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		mesh.triangles = new int[6] { 3, 1, 2, 2, 1, 0 };
		m_Renderer.GetComponent<MeshFilter>().mesh = mesh;
		m_Material = new Material(ShaderUtils.FindShader("Hidden/CameraFade"));
		m_Renderer.SetSharedMaterial(m_Material);
	}

	private void SetupCamera()
	{
		m_Camera.farClipPlane = 1f;
		m_Camera.nearClipPlane = -1f;
		m_Camera.clearFlags = CameraClearFlags.Nothing;
		m_Camera.orthographicSize = 0.5f;
	}
}
