using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class UberTextRenderToTexture
{
	private struct UsedMesh
	{
		public bool IsValid;

		public Mesh Mesh;

		public Vector2 PlaneSize;

		public Color GradientUpperColor;

		public Color GradientLowerColor;

		public int ReferenceCounter;

		public int Id;
	}

	private const int INVALID_MESH_ID = -1;

	private GameObject m_renderOnObject;

	private Renderer m_renderOnObjectRenderer;

	private GameObject m_planeGameObject;

	private MeshFilter m_planeMeshFilter;

	private MeshRenderer m_planeMeshRenderer;

	private GameObject m_shadowPlaneGameObject;

	private MeshFilter m_shadowMeshFilter;

	private MeshRenderer m_shadowMeshRenderer;

	private Vector2 m_currentPlaneSize;

	private int m_currentUsedMeshId = -1;

	private PlaneMaterialQuery m_planeMaterialQuery = new PlaneMaterialQuery();

	private AntiAlisaingMaterialQuery m_antialiasingMaterialQuery = new AntiAlisaingMaterialQuery();

	private ShadowMaterialQuery m_shadowMaterialQuery = new ShadowMaterialQuery();

	private UberTextMaterial m_planeMaterial;

	private UberTextMaterial m_renderOnObjectMaterial;

	private UberTextMaterial m_shadowMaterial;

	private UberTextMaterial m_antialiasMaterial;

	private bool m_shouldBeVisible;

	private bool m_init;

	private PopupRenderer m_popupRendererComponent;

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

	private static readonly List<UsedMesh> s_usedMeshesList = new List<UsedMesh>(5);

	private static int s_newUsedMeshId = 0;

	private static int RENDER_LAYER_BIT = GameLayer.InvisibleRender.LayerBit();

	private static Camera s_renderCamera;

	private static GameObject s_cameraGameObject;

	private static bool s_hasLookedForSupportedTextureFormat;

	private static RenderTextureFormat s_textureFormat = RenderTextureFormat.DefaultHDR;

	private RenderTexture m_renderTexture;

	public void InitOnRenderObject(GameObject renderOnObject, Vector2Int renderTextureSize)
	{
		if (!m_renderOnObject || m_renderOnObject != renderOnObject)
		{
			m_renderOnObject = renderOnObject;
			m_renderOnObjectRenderer = renderOnObject.GetComponent<Renderer>();
		}
		CreateRenderTexture(renderTextureSize);
		CreateCamera();
		if (!m_init)
		{
			m_init = true;
		}
	}

	public void InitOnPlane(GameObject parent, Vector2 planeSize, Color gradientUpperColor, Color gradientLowerColor, Vector2Int renderTextureSize)
	{
		CreateRenderTexture(renderTextureSize);
		CreateCamera();
		if (!m_init || !m_planeGameObject)
		{
			m_currentPlaneSize = planeSize;
			m_init = true;
			CreatePlane(parent, gradientUpperColor, gradientLowerColor);
		}
		else if (m_currentPlaneSize != planeSize)
		{
			m_currentPlaneSize = planeSize;
			m_planeMeshFilter.sharedMesh = AcquireAppropriateMesh(m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		}
	}

	public void InitShadow(GameObject parent, Vector3 localPosition)
	{
		if (m_init && !m_renderOnObject)
		{
			if (!m_shadowPlaneGameObject)
			{
				CreateShadowPlane(parent, localPosition);
			}
			else
			{
				m_shadowPlaneGameObject.transform.localPosition = localPosition;
				m_shadowMeshFilter.sharedMesh = AcquireAppropriateMesh(m_currentPlaneSize, Color.clear, Color.clear);
				m_shadowMeshRenderer.enabled = true;
			}
			m_shadowMaterialQuery.WithTexture(m_renderTexture);
		}
	}

	public void Destroy()
	{
		ReleaseCurrentMesh();
		RenderTextureTracker.Get().DestroyRenderTexture(m_renderTexture);
		UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
		uberTextMaterialManager.ReleaseMaterial(m_planeMaterial);
		uberTextMaterialManager.ReleaseMaterial(m_renderOnObjectMaterial);
		uberTextMaterialManager.ReleaseMaterial(m_shadowMaterial);
		uberTextMaterialManager.ReleaseMaterial(m_antialiasMaterial);
		if ((bool)m_planeGameObject)
		{
			Object.DestroyImmediate(m_planeGameObject);
		}
		if ((bool)m_shadowPlaneGameObject)
		{
			Object.DestroyImmediate(m_shadowPlaneGameObject);
		}
	}

	public void DoRenderToTexture()
	{
		s_renderCamera.targetTexture = m_renderTexture;
		if ((bool)m_renderOnObject && (bool)m_renderOnObjectRenderer)
		{
			m_renderOnObjectMaterial = ApplyMaterialIfNeeded(m_renderOnObjectRenderer, m_renderOnObjectMaterial, m_planeMaterialQuery);
		}
		else if ((bool)m_planeGameObject)
		{
			m_planeMaterial = ApplyMaterialIfNeeded(m_planeMeshRenderer, m_planeMaterial, m_planeMaterialQuery);
			if ((bool)m_shadowMeshRenderer && m_shadowMeshRenderer.enabled)
			{
				m_shadowMaterial = ApplyMaterialIfNeeded(m_shadowMeshRenderer, m_shadowMaterial, m_shadowMaterialQuery);
			}
		}
		s_renderCamera.Render();
	}

	public void ApplyAntialiasing()
	{
		if ((bool)m_renderOnObject)
		{
			m_antialiasMaterial = ApplyMaterialIfNeeded(m_renderOnObjectRenderer, m_antialiasMaterial, m_antialiasingMaterialQuery);
		}
		else if ((bool)m_planeGameObject)
		{
			m_antialiasMaterial = ApplyMaterialIfNeeded(m_planeMeshRenderer, m_antialiasMaterial, m_antialiasingMaterialQuery);
		}
	}

	public void SetActive(bool active)
	{
		if ((bool)m_planeGameObject)
		{
			m_planeGameObject.SetActive(active);
		}
		if ((bool)m_shadowPlaneGameObject)
		{
			m_shadowPlaneGameObject.SetActive(active);
		}
	}

	public void SetAllVisible(bool visible)
	{
		if ((bool)m_planeMeshRenderer)
		{
			m_planeMeshRenderer.enabled = visible;
		}
		if ((bool)m_renderOnObjectRenderer)
		{
			m_renderOnObjectRenderer.enabled = visible;
		}
		if ((bool)m_shadowMeshRenderer)
		{
			m_shadowMeshRenderer.enabled = visible;
		}
	}

	public void SetGradientColors(Color gradientUpperColor, Color gradientLowerColor)
	{
		if ((bool)m_planeMeshFilter && GetCurrentUsedMesh().IsValid)
		{
			m_planeMeshFilter.sharedMesh = AcquireAppropriateMesh(m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		}
	}

	public void SetPlaneLocalPosition(Vector3 position)
	{
		if ((bool)m_planeGameObject)
		{
			m_planeGameObject.transform.localPosition = position;
		}
	}

	public void SetPlaneRotation(Quaternion rotation)
	{
		if ((bool)m_planeGameObject)
		{
			m_planeGameObject.transform.rotation = rotation;
		}
	}

	public void DoPlaneRotate(Vector3 rotation)
	{
		if ((bool)m_planeGameObject)
		{
			m_planeGameObject.transform.Rotate(rotation);
		}
	}

	public void SetLayer(int layer)
	{
		if ((bool)m_planeGameObject)
		{
			m_planeGameObject.layer = layer;
		}
		if ((bool)m_shadowPlaneGameObject)
		{
			m_shadowPlaneGameObject.layer = layer;
		}
		if ((bool)m_renderOnObject)
		{
			m_renderOnObject.layer = layer;
		}
	}

	public bool HasLayer(int layer)
	{
		bool flag = false;
		if ((bool)m_planeGameObject)
		{
			flag = m_planeGameObject.layer == layer;
		}
		if (flag && (bool)m_shadowPlaneGameObject)
		{
			flag = m_shadowPlaneGameObject.layer == layer;
		}
		return flag;
	}

	public void SetAntialiasOffset(float offset)
	{
		Vector2 vector = m_renderTexture.texelSize * offset;
		m_antialiasingMaterialQuery.WithOffsetX(vector.x);
		m_antialiasingMaterialQuery.WithOffsetY(vector.y);
	}

	public void SetAntialiasEdge(float edge)
	{
		m_antialiasingMaterialQuery.WithEdge(edge);
	}

	public void SetShadowOffset(float offset)
	{
		Vector2 vector = m_renderTexture.texelSize * offset;
		m_shadowMaterialQuery.WithOffsetX(vector.x);
		m_shadowMaterialQuery.WithOffsetY(vector.y);
	}

	public void SetShadowColor(Color color)
	{
		m_shadowMaterialQuery.WithColor(color);
	}

	public PopupRenderer GetPopupRenderer()
	{
		if (!m_popupRendererComponent)
		{
			if ((bool)m_renderOnObject && (bool)m_renderOnObjectRenderer)
			{
				m_popupRendererComponent = m_renderOnObject.GetComponent<PopupRenderer>();
				if (!m_popupRendererComponent)
				{
					m_popupRendererComponent = m_renderOnObject.AddComponent<PopupRenderer>();
				}
			}
			else if ((bool)m_planeGameObject)
			{
				m_popupRendererComponent = m_planeGameObject.AddComponent<PopupRenderer>();
			}
		}
		return m_popupRendererComponent;
	}

	public void DisablePopupRenderer()
	{
		if ((bool)m_popupRendererComponent)
		{
			m_popupRendererComponent.DisablePopupRendering();
		}
	}

	public void SetCameraPosition(UberText.AlignmentOptions alignment, UberText.AnchorOptions anchor, Vector2 size, float offset)
	{
		float num = -3000f;
		Vector2 vector = size * 0.5f;
		if (alignment == UberText.AlignmentOptions.Left)
		{
			num += vector.x;
		}
		if (alignment == UberText.AlignmentOptions.Right)
		{
			num -= vector.x;
		}
		float num2 = 0f;
		if (anchor == UberText.AnchorOptions.Upper)
		{
			num2 += vector.y;
		}
		if (anchor == UberText.AnchorOptions.Lower)
		{
			num2 -= vector.y;
		}
		Vector3 position = new Vector3(num, 3000f - num2, offset);
		s_cameraGameObject.transform.position = position;
		s_renderCamera.orthographicSize = vector.y;
	}

	public void SetCameraBackgroundColor(Color color)
	{
		s_renderCamera.backgroundColor = new Color(color.r, color.g, color.b, 0f);
	}

	public void RemoveShadow()
	{
		if ((bool)m_shadowMeshRenderer)
		{
			m_shadowMeshRenderer.enabled = false;
		}
	}

	public void SetRenderQueueIncrement(int value)
	{
		if ((bool)m_planeMeshRenderer)
		{
			m_planeMeshRenderer.sortingOrder = value;
		}
		m_planeMaterialQuery.WithIncrementRenderQueue(value);
		m_antialiasingMaterialQuery.WithIncrementRenderQueue(value);
	}

	public void SetShadowRenderQueueIncrement(int value)
	{
		if ((bool)m_shadowMeshRenderer)
		{
			m_shadowMeshRenderer.sortingOrder = value;
		}
		m_shadowMaterialQuery.WithIncrementRenderQueue(value);
	}

	public bool IsRenderTextureCreated()
	{
		if ((bool)m_renderTexture)
		{
			return m_renderTexture.IsCreated();
		}
		return false;
	}

	private void CreatePlane(GameObject parent, Color gradientUpperColor, Color gradientLowerColor)
	{
		string text = "UberText_RenderPlane_" + parent.name;
		for (int num = parent.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = parent.transform.GetChild(num);
			if ((bool)child && child.gameObject.name == text)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}
		m_planeGameObject = new GameObject();
		m_planeGameObject.name = text;
		SceneUtils.SetHideFlags(m_planeGameObject, HideFlags.DontSave);
		m_planeGameObject.layer = parent.layer;
		m_planeGameObject.transform.parent = parent.transform;
		m_planeGameObject.transform.localScale = Vector3.one;
		m_planeMeshFilter = m_planeGameObject.AddComponent<MeshFilter>();
		m_planeMeshFilter.sharedMesh = AcquireAppropriateMesh(m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		m_planeMeshRenderer = m_planeGameObject.AddComponent<MeshRenderer>();
	}

	private Mesh AcquireAppropriateMesh(Vector2 meshSize, Color gradientUpperColor, Color gradientLowerColor)
	{
		if (m_currentUsedMeshId != -1)
		{
			ReleaseCurrentMesh();
		}
		Mesh mesh = null;
		int i;
		for (i = 0; i < s_usedMeshesList.Count; i++)
		{
			UsedMesh usedMesh = s_usedMeshesList[i];
			if (usedMesh.PlaneSize == meshSize && usedMesh.GradientUpperColor == gradientUpperColor && usedMesh.GradientLowerColor == gradientLowerColor)
			{
				mesh = usedMesh.Mesh;
				break;
			}
		}
		if (!mesh)
		{
			mesh = CreateMesh(meshSize, gradientUpperColor, gradientLowerColor);
			s_usedMeshesList.Add(new UsedMesh
			{
				Mesh = mesh,
				PlaneSize = meshSize,
				GradientUpperColor = gradientUpperColor,
				GradientLowerColor = gradientLowerColor,
				ReferenceCounter = 1,
				IsValid = true,
				Id = s_newUsedMeshId
			});
			m_currentUsedMeshId = s_newUsedMeshId;
			s_newUsedMeshId++;
		}
		else
		{
			UsedMesh value = s_usedMeshesList[i];
			value.ReferenceCounter++;
			s_usedMeshesList[i] = value;
			m_currentUsedMeshId = value.Id;
		}
		return mesh;
	}

	private void ReleaseCurrentMesh()
	{
		if (m_currentUsedMeshId == -1)
		{
			return;
		}
		for (int num = s_usedMeshesList.Count - 1; num >= 0; num--)
		{
			UsedMesh value = s_usedMeshesList[num];
			if (value.Id == m_currentUsedMeshId)
			{
				value.ReferenceCounter--;
				if (value.ReferenceCounter <= 0)
				{
					s_usedMeshesList.RemoveAt(num);
					m_currentUsedMeshId = -1;
				}
				else
				{
					s_usedMeshesList[num] = value;
				}
				break;
			}
		}
	}

	private Mesh CreateMesh(Vector2 meshSize, Color gradientUpperColor, Color gradientLowerColor)
	{
		float num = meshSize.x * 0.5f;
		float num2 = meshSize.y * 0.5f;
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - num, 0f, 0f - num2),
			new Vector3(num, 0f, 0f - num2),
			new Vector3(0f - num, 0f, num2),
			new Vector3(num, 0f, num2)
		};
		if (gradientLowerColor != Color.clear || gradientUpperColor != Color.clear)
		{
			mesh.colors = new Color[4] { gradientUpperColor, gradientUpperColor, gradientLowerColor, gradientLowerColor };
		}
		mesh.uv = PLANE_UVS;
		mesh.normals = PLANE_NORMALS;
		mesh.triangles = PLANE_TRIANGLES;
		mesh.RecalculateBounds();
		return mesh;
	}

	private UsedMesh GetCurrentUsedMesh()
	{
		if (m_currentUsedMeshId != -1)
		{
			foreach (UsedMesh s_usedMeshes in s_usedMeshesList)
			{
				if (s_usedMeshes.Id == m_currentUsedMeshId)
				{
					return s_usedMeshes;
				}
			}
		}
		return default(UsedMesh);
	}

	private void CreateCamera()
	{
		if (!s_renderCamera)
		{
			s_cameraGameObject = new GameObject();
			s_cameraGameObject.AddComponent<RenderToTextureCamera>();
			s_cameraGameObject.name = "UberText_RenderCamera";
			SceneUtils.SetHideFlags(s_cameraGameObject, HideFlags.HideAndDontSave);
			s_renderCamera = s_cameraGameObject.AddComponent<Camera>();
			s_renderCamera.orthographic = true;
			s_renderCamera.nearClipPlane = -0.1f;
			s_renderCamera.farClipPlane = 0.1f;
			Camera main = Camera.main;
			if ((bool)main)
			{
				s_renderCamera.depth = main.depth - 50f;
			}
			s_renderCamera.clearFlags = CameraClearFlags.Color;
			s_renderCamera.depthTextureMode = DepthTextureMode.None;
			s_renderCamera.renderingPath = RenderingPath.Forward;
			s_renderCamera.cullingMask = RENDER_LAYER_BIT;
			s_renderCamera.enabled = false;
		}
	}

	private void CreateRenderTexture(Vector2Int size)
	{
		if ((bool)m_renderTexture)
		{
			if (m_renderTexture.width == size.x && m_renderTexture.height == size.y)
			{
				return;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(m_renderTexture);
		}
		FindSupportedTextureFormat();
		m_renderTexture = RenderTextureTracker.Get().CreateNewTexture(size.x, size.y, 0, s_textureFormat);
		SceneUtils.SetHideFlags(m_renderTexture, HideFlags.HideAndDontSave);
		m_planeMaterialQuery.WithTexture(m_renderTexture);
		m_antialiasingMaterialQuery.WithTexture(m_renderTexture);
	}

	private void FindSupportedTextureFormat()
	{
		if (!s_hasLookedForSupportedTextureFormat)
		{
			s_hasLookedForSupportedTextureFormat = true;
			bool flag = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444) && PlatformSettings.RuntimeOS != OSCategory.PC;
			if (s_textureFormat == RenderTextureFormat.DefaultHDR && flag)
			{
				s_textureFormat = RenderTextureFormat.ARGB4444;
			}
			if (s_textureFormat == RenderTextureFormat.DefaultHDR && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
			{
				s_textureFormat = RenderTextureFormat.ARGB32;
			}
		}
	}

	private void CreateShadowPlane(GameObject parent, Vector3 localPostion)
	{
		m_shadowPlaneGameObject = new GameObject();
		m_shadowPlaneGameObject.name = "UberText_ShadowPlane_" + parent.name;
		SceneUtils.SetHideFlags(m_shadowPlaneGameObject, HideFlags.DontSave);
		m_shadowPlaneGameObject.layer = parent.layer;
		m_shadowPlaneGameObject.transform.parent = m_planeGameObject.transform;
		m_shadowPlaneGameObject.transform.localRotation = Quaternion.identity;
		m_shadowPlaneGameObject.transform.localScale = Vector3.one;
		m_shadowPlaneGameObject.transform.localPosition = localPostion;
		m_shadowMeshFilter = m_shadowPlaneGameObject.AddComponent<MeshFilter>();
		m_shadowMeshFilter.sharedMesh = AcquireAppropriateMesh(m_currentPlaneSize, Color.clear, Color.clear);
		m_shadowMeshRenderer = m_shadowPlaneGameObject.AddComponent<MeshRenderer>();
	}

	private UberTextMaterial ApplyMaterialIfNeeded(Renderer renderer, UberTextMaterial currentMaterial, UberTextMaterialQuery query)
	{
		if (currentMaterial == null || !currentMaterial.HasQuery(query))
		{
			UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
			if (currentMaterial != null)
			{
				uberTextMaterialManager.ReleaseMaterial(currentMaterial);
			}
			currentMaterial = uberTextMaterialManager.FetchMaterial(query);
			Material material = currentMaterial.Acquire();
			renderer.SetSharedMaterial(material);
		}
		else if (!currentMaterial.IsStillBound(renderer))
		{
			currentMaterial.Rebound(renderer, 0);
		}
		return currentMaterial;
	}
}
