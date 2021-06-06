using System.Collections.Generic;
using UnityEngine;

public class DiamondRenderToTexture : MonoBehaviour
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

	public struct TransformData
	{
		public Vector3 position;

		public Vector3 localScale;

		public Quaternion rotation;

		public Vector3 up;

		public Vector3 forward;

		public int layer;

		public Transform objectParent;

		public Transform atlasedComponentParent;
	}

	public struct RenderCommand
	{
		public Renderer Renderer;

		public Material Material;

		public int MeshIndex;
	}

	private static readonly Vector3 ALPHA_OBJECT_OFFSET = new Vector3(0f, 1000f, 0f);

	private static readonly Color GIZMOS_COLOR = new Color(1f, 1f, 0f, 0.8f);

	public GameObject m_ObjectToRender;

	public GameObject m_AlphaObjectToRender;

	public bool m_AllowRepetition;

	public bool m_HideRenderObject = true;

	public bool m_RealtimeRender;

	public bool m_RealtimeTranslation;

	public bool m_OpaqueObjectAlphaFill;

	public RenderToTextureMaterial m_RenderMaterial;

	public Material m_Material;

	public bool m_CreateRenderPlane;

	public Color m_ClearColor = Color.clear;

	public GameObject m_RenderToObject;

	[Range(1f, 2048f)]
	public int m_Resolution = 128;

	public Vector3 m_bounds = Vector3.one;

	public bool m_UniformWorldScale;

	public Vector3 m_PositionOffset = Vector3.zero;

	private const string TRANSPARENT_SHADER_NAME = "Hidden/R2TTransparent";

	private Shader m_TransparentShader;

	private Material m_TransparentMaterial;

	private bool m_isRegisteredToManager;

	private bool m_isDirty;

	private DiamondRenderToTextureService m_diamondRenderToTextureService;

	private Vector3 m_worldSize;

	private Vector3 m_worldScale;

	private TransformData m_transformSnapshot;

	private Bounds m_renderBounds = new Bounds(Vector3.zero, Vector3.zero);

	private Renderer m_outputRenderer;

	private Renderer[] m_captureRenderers;

	private TransformData m_atlasPositionSnapshot;

	private Transform m_selfOriginalParent;

	private Transform m_objectToRenderOriginalParent;

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

	public GameObject OffscreenGameObject { get; private set; }

	public Vector2Int TextureSize { get; private set; }

	public Bounds RendererBounds => m_renderBounds;

	public Vector3 PivotPosition => m_PositionOffset - Vector3.Scale(new Vector3(-1f, 1f, 1f), m_bounds / 2f);

	public Vector3 WorldPivotOffset => base.transform.TransformPoint(PivotPosition) - base.transform.position;

	public TransformData TransformSnapshot => m_transformSnapshot;

	public bool HasAtlasPosition { get; set; }

	public Vector3 WorldBounds => m_worldSize;

	public Vector3 ObjectToRenderOffset { get; private set; }

	public List<RenderCommand> OpaqueRenderCommands { get; private set; }

	public List<RenderCommand> TransparentRenderCommands { get; private set; }

	private void Start()
	{
		m_diamondRenderToTextureService = HearthstoneServices.Get<DiamondRenderToTextureService>();
		if (!m_ObjectToRender)
		{
			m_isDirty = true;
			return;
		}
		FetchObjectRequiredData();
		RegisterToService();
	}

	private void Update()
	{
		if (base.transform.hasChanged)
		{
			m_isDirty = true;
		}
		if (m_isDirty)
		{
			FetchObjectRequiredData();
		}
		if (!m_isRegisteredToManager)
		{
			RegisterToService();
		}
	}

	private void OnValidate()
	{
		CalcWorldWidthHeightScale();
		m_isDirty = true;
	}

	private void OnDisable()
	{
		UnregisterFromService();
	}

	private void OnEnable()
	{
		FetchObjectRequiredData();
		RegisterToService();
	}

	private void OnDestroy()
	{
		UnregisterFromService();
	}

	private void OnDrawGizmosSelected()
	{
		if (base.enabled && (bool)m_ObjectToRender)
		{
			Gizmos.matrix = Matrix4x4.TRS(m_ObjectToRender.transform.position, base.transform.rotation, base.transform.lossyScale);
			Gizmos.color = GIZMOS_COLOR;
			Gizmos.DrawSphere(m_PositionOffset, 0.1f);
			Gizmos.DrawWireCube(m_PositionOffset, m_bounds);
			Gizmos.DrawSphere(PivotPosition, 0.1f);
			Vector3 pos = m_PositionOffset + new Vector3(0f, m_bounds.y / 2f, 0f);
			GizmosDrawArrow(pos, Vector3.forward, Color.blue);
			GizmosDrawArrow(pos, Vector3.up, Color.green);
			Gizmos.matrix = Matrix4x4.identity;
		}
	}

	public bool IsEqual(DiamondRenderToTexture other)
	{
		if (other.m_ObjectToRender.GetInstanceID() != m_ObjectToRender.GetInstanceID())
		{
			return false;
		}
		return true;
	}

	public void OnAddedToAtlas(RenderTexture atlasTexture, Rect atlasUV)
	{
		UpdatePlaneUVS(atlasUV);
		UpdateMaterial(atlasTexture);
	}

	public void PushTransform()
	{
		Transform transform = m_ObjectToRender.transform;
		m_transformSnapshot.position = transform.position;
		m_transformSnapshot.localScale = transform.localScale;
		m_transformSnapshot.rotation = transform.rotation;
		m_transformSnapshot.layer = m_ObjectToRender.layer;
		m_transformSnapshot.up = base.transform.up;
		m_transformSnapshot.forward = base.transform.forward;
		m_transformSnapshot.objectParent = transform.parent;
		m_transformSnapshot.atlasedComponentParent = base.transform.parent;
	}

	public void ResetTransform(Vector3 position)
	{
		Transform obj = m_ObjectToRender.transform;
		obj.parent = null;
		obj.localScale = Vector3.one;
		obj.position = position;
		Transform obj2 = base.transform;
		obj2.parent = null;
		obj2.localScale = Vector3.one;
		obj2.position = position;
		CalcWorldWidthHeightScale();
	}

	public void RestoreParents()
	{
		m_ObjectToRender.transform.parent = m_transformSnapshot.objectParent;
		base.transform.parent = m_transformSnapshot.atlasedComponentParent;
	}

	public void PopTransform()
	{
		Transform obj = m_ObjectToRender.transform;
		obj.position = TransformSnapshot.position;
		obj.localScale = TransformSnapshot.localScale;
		obj.rotation = TransformSnapshot.rotation;
		m_ObjectToRender.layer = m_transformSnapshot.layer;
		base.transform.up = m_transformSnapshot.up;
		base.transform.forward = m_transformSnapshot.forward;
	}

	public void Refresh()
	{
		m_isDirty = true;
	}

	public void CaptureAtlasPosition()
	{
		HasAtlasPosition = true;
		Transform transform = base.transform;
		Transform transform2 = m_ObjectToRender.transform;
		m_atlasPositionSnapshot.position = transform2.position;
		m_atlasPositionSnapshot.localScale = transform2.localScale;
		m_atlasPositionSnapshot.rotation = transform2.rotation;
		m_atlasPositionSnapshot.up = transform.up;
		m_atlasPositionSnapshot.forward = transform.forward;
	}

	public bool MaintainsAtlasPosition()
	{
		Transform transform = m_ObjectToRender.transform;
		if (!transform.hasChanged)
		{
			return true;
		}
		bool num = m_atlasPositionSnapshot.position == transform.position;
		bool flag = m_atlasPositionSnapshot.localScale == transform.localScale;
		bool flag2 = m_atlasPositionSnapshot.rotation == transform.rotation;
		return num && flag && flag2;
	}

	public void RestoreAtlasPosition()
	{
		Transform obj = m_ObjectToRender.transform;
		obj.position = m_atlasPositionSnapshot.position;
		obj.localScale = m_atlasPositionSnapshot.localScale;
		obj.rotation = m_atlasPositionSnapshot.rotation;
		base.transform.position = m_atlasPositionSnapshot.position;
		base.transform.localScale = m_atlasPositionSnapshot.localScale;
		base.transform.rotation = Quaternion.LookRotation(m_atlasPositionSnapshot.forward, m_atlasPositionSnapshot.up);
	}

	public void RestoreOriginalParents()
	{
		if ((bool)m_objectToRenderOriginalParent && (bool)m_ObjectToRender)
		{
			m_ObjectToRender.transform.parent = m_objectToRenderOriginalParent;
		}
		if ((bool)m_selfOriginalParent && (bool)base.transform)
		{
			base.transform.parent = m_selfOriginalParent;
		}
	}

	private void FetchObjectRequiredData()
	{
		if ((bool)m_ObjectToRender)
		{
			CaptureOriginalParents();
			FetchCapturedRenderers();
			FetchOutputRenderer();
			CalculateObjectToRenderOffset();
			CalcRendererBounds();
			CalcTextureSize();
			CreateRenderCommands();
			HasAtlasPosition = false;
			m_isDirty = false;
		}
	}

	private void SetupAuxRenderObjects()
	{
		if (!m_ObjectToRender)
		{
			return;
		}
		if (m_RealtimeTranslation)
		{
			OffscreenGameObject = new GameObject("R2TOffsetRenderRoot_" + base.name);
			OffscreenGameObject.transform.position = base.transform.position;
			m_ObjectToRender.transform.SetParent(OffscreenGameObject.transform);
		}
		if (m_HideRenderObject)
		{
			if (m_RealtimeTranslation && (bool)m_AlphaObjectToRender)
			{
				m_AlphaObjectToRender.transform.SetParent(OffscreenGameObject.transform);
			}
			if ((bool)m_AlphaObjectToRender)
			{
				m_AlphaObjectToRender.transform.position = base.transform.position - ALPHA_OBJECT_OFFSET;
			}
		}
	}

	private void CalcWorldWidthHeightScale()
	{
		Transform obj = base.transform;
		Quaternion rotation = obj.rotation;
		Vector3 localScale = obj.localScale;
		Transform parent = obj.parent;
		obj.rotation = Quaternion.identity;
		Vector3 lossyScale = obj.lossyScale;
		bool flag = false;
		if (lossyScale.magnitude == 0f)
		{
			base.transform.parent = null;
			base.transform.localScale = Vector3.one;
			flag = true;
		}
		if (m_UniformWorldScale)
		{
			float num = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
			m_worldScale = new Vector3(num, num, num);
		}
		else
		{
			m_worldScale = lossyScale;
		}
		m_worldSize = new Vector3(m_bounds.x * m_worldScale.x, m_bounds.y * m_worldScale.y, m_bounds.z * m_worldScale.z);
		if (flag)
		{
			base.transform.parent = parent;
			base.transform.localScale = localScale;
		}
		base.transform.rotation = rotation;
		if (m_worldSize.x == 0f || m_worldSize.y == 0f)
		{
			Debug.LogError(string.Format(" \"{0}\": RenderToTexture has a world scale of zero. \nm_WorldWidth: {1},   m_WorldHeight: {2}", m_worldSize.x, m_worldSize.y));
		}
	}

	private void CalcTextureSize()
	{
		float num = m_bounds.y / m_bounds.x;
		TextureSize = new Vector2Int(m_Resolution, Mathf.RoundToInt((float)m_Resolution * num));
	}

	private void CalculateObjectToRenderOffset()
	{
		Vector3 objectToRenderOffset = base.transform.position - m_ObjectToRender.transform.position;
		objectToRenderOffset.z = 0f;
		ObjectToRenderOffset = objectToRenderOffset;
	}

	private void CalcRendererBounds()
	{
		Renderer[] captureRenderers = m_captureRenderers;
		foreach (Renderer renderer in captureRenderers)
		{
			if (m_renderBounds.size == Vector3.zero)
			{
				m_renderBounds = renderer.bounds;
			}
			else
			{
				m_renderBounds.Encapsulate(renderer.bounds);
			}
		}
	}

	private void FetchOutputRenderer()
	{
		if ((bool)m_RenderToObject && !m_outputRenderer)
		{
			m_outputRenderer = m_RenderToObject.GetComponent<Renderer>();
			if (!m_outputRenderer)
			{
				Debug.LogError("RenderToObject should have a renderer!");
			}
		}
	}

	private void CaptureOriginalParents()
	{
		if ((bool)m_ObjectToRender && !m_objectToRenderOriginalParent)
		{
			m_objectToRenderOriginalParent = m_ObjectToRender.transform.parent;
		}
		if (!m_selfOriginalParent)
		{
			m_selfOriginalParent = base.transform.parent;
		}
	}

	private void FetchCapturedRenderers()
	{
		if ((bool)m_ObjectToRender)
		{
			m_captureRenderers = m_ObjectToRender.GetComponentsInChildren<Renderer>();
		}
	}

	private void RegisterToService()
	{
		if (!m_isRegisteredToManager && m_diamondRenderToTextureService != null && (bool)m_ObjectToRender && (bool)m_outputRenderer)
		{
			bool flag = m_diamondRenderToTextureService.Register(this);
			if (flag)
			{
				SetupAuxRenderObjects();
			}
			m_isRegisteredToManager = flag;
		}
	}

	private void UnregisterFromService()
	{
		if (m_isRegisteredToManager)
		{
			m_diamondRenderToTextureService.Unregister(this);
			m_isRegisteredToManager = false;
		}
	}

	private void UpdatePlaneUVS(Rect atlasUV)
	{
		if ((bool)m_RenderToObject)
		{
			Mesh mesh = m_RenderToObject.GetComponent<MeshFilter>().mesh;
			Vector2[] uv = mesh.uv;
			Rect currentUVBounds = GetCurrentUVBounds(uv);
			Vector2 vector = new Vector2(atlasUV.width / currentUVBounds.width, atlasUV.height / currentUVBounds.height);
			Vector2 vector2 = new Vector2(atlasUV.xMin - currentUVBounds.xMin, atlasUV.yMin - currentUVBounds.yMin);
			for (int i = 0; i < uv.Length; i++)
			{
				Vector2 vector3 = uv[i];
				vector3.x = vector3.x * vector.x + vector2.x;
				vector3.y = vector3.y * vector.y + vector2.y;
				uv[i] = vector3;
			}
			mesh.uv = uv;
		}
	}

	private Rect GetCurrentUVBounds(Vector2[] currentUv)
	{
		Vector2 one = Vector2.one;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < currentUv.Length; i++)
		{
			Vector2 vector = currentUv[i];
			if (vector.x < one.x)
			{
				one.x = vector.x;
			}
			if (vector.y < one.y)
			{
				one.y = vector.y;
			}
			if (vector.x > zero.x)
			{
				zero.x = vector.x;
			}
			if (vector.y > zero.y)
			{
				zero.y = vector.y;
			}
		}
		return new Rect(one.x, one.y, zero.x - one.x, zero.y - one.y);
	}

	private void UpdateMaterial(RenderTexture atlasTexture)
	{
		if ((bool)m_outputRenderer)
		{
			if (m_RenderMaterial == RenderToTextureMaterial.Transparent)
			{
				TransparentMaterial.mainTexture = atlasTexture;
				m_outputRenderer.SetMaterial(TransparentMaterial);
			}
			else
			{
				m_outputRenderer.GetMaterial().mainTexture = atlasTexture;
			}
		}
	}

	public void UpdateMaterialBlend(bool inPlay)
	{
		TransparentMaterial.SetFloat("_LightingBlend", inPlay ? 1f : 0f);
	}

	private void CreateRenderCommands()
	{
		if (OpaqueRenderCommands == null)
		{
			OpaqueRenderCommands = new List<RenderCommand>(m_captureRenderers.Length);
		}
		else
		{
			OpaqueRenderCommands.Clear();
		}
		if (TransparentRenderCommands == null)
		{
			TransparentRenderCommands = new List<RenderCommand>(m_captureRenderers.Length);
		}
		else
		{
			TransparentRenderCommands.Clear();
		}
		Renderer[] captureRenderers = m_captureRenderers;
		foreach (Renderer renderer in captureRenderers)
		{
			List<Material> sharedMaterials = renderer.GetSharedMaterials();
			MeshRenderer obj = renderer as MeshRenderer;
			SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
			int num = 1;
			if ((bool)obj)
			{
				MeshFilter component = renderer.GetComponent<MeshFilter>();
				if (!component)
				{
					continue;
				}
				num = component.sharedMesh.subMeshCount;
			}
			if ((bool)skinnedMeshRenderer)
			{
				num = skinnedMeshRenderer.sharedMesh.subMeshCount;
			}
			for (int j = 0; j < num; j++)
			{
				int num2 = j;
				if (num2 >= sharedMaterials.Count)
				{
					num2 = 0;
				}
				Material material = sharedMaterials[num2];
				RenderCommand item;
				if (material.renderQueue < 3000)
				{
					List<RenderCommand> opaqueRenderCommands = OpaqueRenderCommands;
					item = new RenderCommand
					{
						Renderer = renderer,
						Material = material,
						MeshIndex = j
					};
					opaqueRenderCommands.Add(item);
				}
				else
				{
					List<RenderCommand> transparentRenderCommands = TransparentRenderCommands;
					item = new RenderCommand
					{
						Renderer = renderer,
						Material = material,
						MeshIndex = j
					};
					transparentRenderCommands.Add(item);
				}
			}
		}
	}

	private static void GizmosDrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20f)
	{
		Gizmos.color = color;
		Gizmos.DrawRay(pos, direction);
		Vector3 vector = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f + arrowHeadAngle, 0f) * new Vector3(0f, 0f, 1f);
		Vector3 vector2 = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f - arrowHeadAngle, 0f) * new Vector3(0f, 0f, 1f);
		Gizmos.DrawRay(pos + direction, vector * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, vector2 * arrowHeadLength);
	}
}
