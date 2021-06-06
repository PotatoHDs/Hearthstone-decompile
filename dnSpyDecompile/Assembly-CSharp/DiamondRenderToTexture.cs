using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A6F RID: 2671
public class DiamondRenderToTexture : MonoBehaviour
{
	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x06008F5E RID: 36702 RVA: 0x002E722C File Offset: 0x002E542C
	protected Material TransparentMaterial
	{
		get
		{
			if (this.m_TransparentMaterial == null)
			{
				if (this.m_TransparentShader == null)
				{
					this.m_TransparentShader = ShaderUtils.FindShader("Hidden/R2TTransparent");
					if (!this.m_TransparentShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TTransparent");
					}
				}
				this.m_TransparentMaterial = new Material(this.m_TransparentShader);
				SceneUtils.SetHideFlags(this.m_TransparentMaterial, HideFlags.DontSave);
			}
			return this.m_TransparentMaterial;
		}
	}

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x06008F5F RID: 36703 RVA: 0x002E72A0 File Offset: 0x002E54A0
	// (set) Token: 0x06008F60 RID: 36704 RVA: 0x002E72A8 File Offset: 0x002E54A8
	public GameObject OffscreenGameObject { get; private set; }

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x06008F61 RID: 36705 RVA: 0x002E72B1 File Offset: 0x002E54B1
	// (set) Token: 0x06008F62 RID: 36706 RVA: 0x002E72B9 File Offset: 0x002E54B9
	public Vector2Int TextureSize { get; private set; }

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x06008F63 RID: 36707 RVA: 0x002E72C2 File Offset: 0x002E54C2
	public Bounds RendererBounds
	{
		get
		{
			return this.m_renderBounds;
		}
	}

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x06008F64 RID: 36708 RVA: 0x002E72CA File Offset: 0x002E54CA
	public Vector3 PivotPosition
	{
		get
		{
			return this.m_PositionOffset - Vector3.Scale(new Vector3(-1f, 1f, 1f), this.m_bounds / 2f);
		}
	}

	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x06008F65 RID: 36709 RVA: 0x002E7300 File Offset: 0x002E5500
	public Vector3 WorldPivotOffset
	{
		get
		{
			return base.transform.TransformPoint(this.PivotPosition) - base.transform.position;
		}
	}

	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x06008F66 RID: 36710 RVA: 0x002E7323 File Offset: 0x002E5523
	public DiamondRenderToTexture.TransformData TransformSnapshot
	{
		get
		{
			return this.m_transformSnapshot;
		}
	}

	// Token: 0x17000814 RID: 2068
	// (get) Token: 0x06008F67 RID: 36711 RVA: 0x002E732B File Offset: 0x002E552B
	// (set) Token: 0x06008F68 RID: 36712 RVA: 0x002E7333 File Offset: 0x002E5533
	public bool HasAtlasPosition { get; set; }

	// Token: 0x17000815 RID: 2069
	// (get) Token: 0x06008F69 RID: 36713 RVA: 0x002E733C File Offset: 0x002E553C
	public Vector3 WorldBounds
	{
		get
		{
			return this.m_worldSize;
		}
	}

	// Token: 0x17000816 RID: 2070
	// (get) Token: 0x06008F6A RID: 36714 RVA: 0x002E7344 File Offset: 0x002E5544
	// (set) Token: 0x06008F6B RID: 36715 RVA: 0x002E734C File Offset: 0x002E554C
	public Vector3 ObjectToRenderOffset { get; private set; }

	// Token: 0x17000817 RID: 2071
	// (get) Token: 0x06008F6C RID: 36716 RVA: 0x002E7355 File Offset: 0x002E5555
	// (set) Token: 0x06008F6D RID: 36717 RVA: 0x002E735D File Offset: 0x002E555D
	public List<DiamondRenderToTexture.RenderCommand> OpaqueRenderCommands { get; private set; }

	// Token: 0x17000818 RID: 2072
	// (get) Token: 0x06008F6E RID: 36718 RVA: 0x002E7366 File Offset: 0x002E5566
	// (set) Token: 0x06008F6F RID: 36719 RVA: 0x002E736E File Offset: 0x002E556E
	public List<DiamondRenderToTexture.RenderCommand> TransparentRenderCommands { get; private set; }

	// Token: 0x06008F70 RID: 36720 RVA: 0x002E7377 File Offset: 0x002E5577
	private void Start()
	{
		this.m_diamondRenderToTextureService = HearthstoneServices.Get<DiamondRenderToTextureService>();
		if (!this.m_ObjectToRender)
		{
			this.m_isDirty = true;
			return;
		}
		this.FetchObjectRequiredData();
		this.RegisterToService();
	}

	// Token: 0x06008F71 RID: 36721 RVA: 0x002E73A5 File Offset: 0x002E55A5
	private void Update()
	{
		if (base.transform.hasChanged)
		{
			this.m_isDirty = true;
		}
		if (this.m_isDirty)
		{
			this.FetchObjectRequiredData();
		}
		if (!this.m_isRegisteredToManager)
		{
			this.RegisterToService();
		}
	}

	// Token: 0x06008F72 RID: 36722 RVA: 0x002E73D7 File Offset: 0x002E55D7
	private void OnValidate()
	{
		this.CalcWorldWidthHeightScale();
		this.m_isDirty = true;
	}

	// Token: 0x06008F73 RID: 36723 RVA: 0x002E73E6 File Offset: 0x002E55E6
	private void OnDisable()
	{
		this.UnregisterFromService();
	}

	// Token: 0x06008F74 RID: 36724 RVA: 0x002E73EE File Offset: 0x002E55EE
	private void OnEnable()
	{
		this.FetchObjectRequiredData();
		this.RegisterToService();
	}

	// Token: 0x06008F75 RID: 36725 RVA: 0x002E73E6 File Offset: 0x002E55E6
	private void OnDestroy()
	{
		this.UnregisterFromService();
	}

	// Token: 0x06008F76 RID: 36726 RVA: 0x002E73FC File Offset: 0x002E55FC
	private void OnDrawGizmosSelected()
	{
		if (!base.enabled || !this.m_ObjectToRender)
		{
			return;
		}
		Gizmos.matrix = Matrix4x4.TRS(this.m_ObjectToRender.transform.position, base.transform.rotation, base.transform.lossyScale);
		Gizmos.color = DiamondRenderToTexture.GIZMOS_COLOR;
		Gizmos.DrawSphere(this.m_PositionOffset, 0.1f);
		Gizmos.DrawWireCube(this.m_PositionOffset, this.m_bounds);
		Gizmos.DrawSphere(this.PivotPosition, 0.1f);
		Vector3 pos = this.m_PositionOffset + new Vector3(0f, this.m_bounds.y / 2f, 0f);
		DiamondRenderToTexture.GizmosDrawArrow(pos, Vector3.forward, Color.blue, 0.25f, 20f);
		DiamondRenderToTexture.GizmosDrawArrow(pos, Vector3.up, Color.green, 0.25f, 20f);
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x06008F77 RID: 36727 RVA: 0x002E74F2 File Offset: 0x002E56F2
	public bool IsEqual(DiamondRenderToTexture other)
	{
		return other.m_ObjectToRender.GetInstanceID() == this.m_ObjectToRender.GetInstanceID();
	}

	// Token: 0x06008F78 RID: 36728 RVA: 0x002E750F File Offset: 0x002E570F
	public void OnAddedToAtlas(RenderTexture atlasTexture, Rect atlasUV)
	{
		this.UpdatePlaneUVS(atlasUV);
		this.UpdateMaterial(atlasTexture);
	}

	// Token: 0x06008F79 RID: 36729 RVA: 0x002E7520 File Offset: 0x002E5720
	public void PushTransform()
	{
		Transform transform = this.m_ObjectToRender.transform;
		this.m_transformSnapshot.position = transform.position;
		this.m_transformSnapshot.localScale = transform.localScale;
		this.m_transformSnapshot.rotation = transform.rotation;
		this.m_transformSnapshot.layer = this.m_ObjectToRender.layer;
		this.m_transformSnapshot.up = base.transform.up;
		this.m_transformSnapshot.forward = base.transform.forward;
		this.m_transformSnapshot.objectParent = transform.parent;
		this.m_transformSnapshot.atlasedComponentParent = base.transform.parent;
	}

	// Token: 0x06008F7A RID: 36730 RVA: 0x002E75D8 File Offset: 0x002E57D8
	public void ResetTransform(Vector3 position)
	{
		Transform transform = this.m_ObjectToRender.transform;
		transform.parent = null;
		transform.localScale = Vector3.one;
		transform.position = position;
		Transform transform2 = base.transform;
		transform2.parent = null;
		transform2.localScale = Vector3.one;
		transform2.position = position;
		this.CalcWorldWidthHeightScale();
	}

	// Token: 0x06008F7B RID: 36731 RVA: 0x002E762C File Offset: 0x002E582C
	public void RestoreParents()
	{
		this.m_ObjectToRender.transform.parent = this.m_transformSnapshot.objectParent;
		base.transform.parent = this.m_transformSnapshot.atlasedComponentParent;
	}

	// Token: 0x06008F7C RID: 36732 RVA: 0x002E7660 File Offset: 0x002E5860
	public void PopTransform()
	{
		Transform transform = this.m_ObjectToRender.transform;
		transform.position = this.TransformSnapshot.position;
		transform.localScale = this.TransformSnapshot.localScale;
		transform.rotation = this.TransformSnapshot.rotation;
		this.m_ObjectToRender.layer = this.m_transformSnapshot.layer;
		base.transform.up = this.m_transformSnapshot.up;
		base.transform.forward = this.m_transformSnapshot.forward;
	}

	// Token: 0x06008F7D RID: 36733 RVA: 0x002E76EC File Offset: 0x002E58EC
	public void Refresh()
	{
		this.m_isDirty = true;
	}

	// Token: 0x06008F7E RID: 36734 RVA: 0x002E76F8 File Offset: 0x002E58F8
	public void CaptureAtlasPosition()
	{
		this.HasAtlasPosition = true;
		Transform transform = base.transform;
		Transform transform2 = this.m_ObjectToRender.transform;
		this.m_atlasPositionSnapshot.position = transform2.position;
		this.m_atlasPositionSnapshot.localScale = transform2.localScale;
		this.m_atlasPositionSnapshot.rotation = transform2.rotation;
		this.m_atlasPositionSnapshot.up = transform.up;
		this.m_atlasPositionSnapshot.forward = transform.forward;
	}

	// Token: 0x06008F7F RID: 36735 RVA: 0x002E7774 File Offset: 0x002E5974
	public bool MaintainsAtlasPosition()
	{
		Transform transform = this.m_ObjectToRender.transform;
		if (!transform.hasChanged)
		{
			return true;
		}
		bool flag = this.m_atlasPositionSnapshot.position == transform.position;
		bool flag2 = this.m_atlasPositionSnapshot.localScale == transform.localScale;
		bool flag3 = this.m_atlasPositionSnapshot.rotation == transform.rotation;
		return flag && flag2 && flag3;
	}

	// Token: 0x06008F80 RID: 36736 RVA: 0x002E77E0 File Offset: 0x002E59E0
	public void RestoreAtlasPosition()
	{
		Transform transform = this.m_ObjectToRender.transform;
		transform.position = this.m_atlasPositionSnapshot.position;
		transform.localScale = this.m_atlasPositionSnapshot.localScale;
		transform.rotation = this.m_atlasPositionSnapshot.rotation;
		base.transform.position = this.m_atlasPositionSnapshot.position;
		base.transform.localScale = this.m_atlasPositionSnapshot.localScale;
		base.transform.rotation = Quaternion.LookRotation(this.m_atlasPositionSnapshot.forward, this.m_atlasPositionSnapshot.up);
	}

	// Token: 0x06008F81 RID: 36737 RVA: 0x002E787C File Offset: 0x002E5A7C
	public void RestoreOriginalParents()
	{
		if (this.m_objectToRenderOriginalParent && this.m_ObjectToRender)
		{
			this.m_ObjectToRender.transform.parent = this.m_objectToRenderOriginalParent;
		}
		if (this.m_selfOriginalParent && base.transform)
		{
			base.transform.parent = this.m_selfOriginalParent;
		}
	}

	// Token: 0x06008F82 RID: 36738 RVA: 0x002E78E4 File Offset: 0x002E5AE4
	private void FetchObjectRequiredData()
	{
		if (!this.m_ObjectToRender)
		{
			return;
		}
		this.CaptureOriginalParents();
		this.FetchCapturedRenderers();
		this.FetchOutputRenderer();
		this.CalculateObjectToRenderOffset();
		this.CalcRendererBounds();
		this.CalcTextureSize();
		this.CreateRenderCommands();
		this.HasAtlasPosition = false;
		this.m_isDirty = false;
	}

	// Token: 0x06008F83 RID: 36739 RVA: 0x002E7938 File Offset: 0x002E5B38
	private void SetupAuxRenderObjects()
	{
		if (!this.m_ObjectToRender)
		{
			return;
		}
		if (this.m_RealtimeTranslation)
		{
			this.OffscreenGameObject = new GameObject("R2TOffsetRenderRoot_" + base.name);
			this.OffscreenGameObject.transform.position = base.transform.position;
			this.m_ObjectToRender.transform.SetParent(this.OffscreenGameObject.transform);
		}
		if (this.m_HideRenderObject)
		{
			if (this.m_RealtimeTranslation && this.m_AlphaObjectToRender)
			{
				this.m_AlphaObjectToRender.transform.SetParent(this.OffscreenGameObject.transform);
			}
			if (this.m_AlphaObjectToRender)
			{
				this.m_AlphaObjectToRender.transform.position = base.transform.position - DiamondRenderToTexture.ALPHA_OBJECT_OFFSET;
			}
		}
	}

	// Token: 0x06008F84 RID: 36740 RVA: 0x002E7A18 File Offset: 0x002E5C18
	private void CalcWorldWidthHeightScale()
	{
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		Vector3 localScale = transform.localScale;
		Transform parent = transform.parent;
		transform.rotation = Quaternion.identity;
		Vector3 lossyScale = transform.lossyScale;
		bool flag = false;
		if (lossyScale.magnitude == 0f)
		{
			base.transform.parent = null;
			base.transform.localScale = Vector3.one;
			flag = true;
		}
		if (this.m_UniformWorldScale)
		{
			float num = Mathf.Max(new float[]
			{
				lossyScale.x,
				lossyScale.y,
				lossyScale.z
			});
			this.m_worldScale = new Vector3(num, num, num);
		}
		else
		{
			this.m_worldScale = lossyScale;
		}
		this.m_worldSize = new Vector3(this.m_bounds.x * this.m_worldScale.x, this.m_bounds.y * this.m_worldScale.y, this.m_bounds.z * this.m_worldScale.z);
		if (flag)
		{
			base.transform.parent = parent;
			base.transform.localScale = localScale;
		}
		base.transform.rotation = rotation;
		if (this.m_worldSize.x == 0f || this.m_worldSize.y == 0f)
		{
			Debug.LogError(string.Format(" \"{0}\": RenderToTexture has a world scale of zero. \nm_WorldWidth: {1},   m_WorldHeight: {2}", this.m_worldSize.x, this.m_worldSize.y));
		}
	}

	// Token: 0x06008F85 RID: 36741 RVA: 0x002E7B98 File Offset: 0x002E5D98
	private void CalcTextureSize()
	{
		float num = this.m_bounds.y / this.m_bounds.x;
		this.TextureSize = new Vector2Int(this.m_Resolution, Mathf.RoundToInt((float)this.m_Resolution * num));
	}

	// Token: 0x06008F86 RID: 36742 RVA: 0x002E7BDC File Offset: 0x002E5DDC
	private void CalculateObjectToRenderOffset()
	{
		Vector3 objectToRenderOffset = base.transform.position - this.m_ObjectToRender.transform.position;
		objectToRenderOffset.z = 0f;
		this.ObjectToRenderOffset = objectToRenderOffset;
	}

	// Token: 0x06008F87 RID: 36743 RVA: 0x002E7C20 File Offset: 0x002E5E20
	private void CalcRendererBounds()
	{
		foreach (Renderer renderer in this.m_captureRenderers)
		{
			if (this.m_renderBounds.size == Vector3.zero)
			{
				this.m_renderBounds = renderer.bounds;
			}
			else
			{
				this.m_renderBounds.Encapsulate(renderer.bounds);
			}
		}
	}

	// Token: 0x06008F88 RID: 36744 RVA: 0x002E7C7C File Offset: 0x002E5E7C
	private void FetchOutputRenderer()
	{
		if (this.m_RenderToObject && !this.m_outputRenderer)
		{
			this.m_outputRenderer = this.m_RenderToObject.GetComponent<Renderer>();
			if (!this.m_outputRenderer)
			{
				Debug.LogError("RenderToObject should have a renderer!");
			}
		}
	}

	// Token: 0x06008F89 RID: 36745 RVA: 0x002E7CCC File Offset: 0x002E5ECC
	private void CaptureOriginalParents()
	{
		if (this.m_ObjectToRender && !this.m_objectToRenderOriginalParent)
		{
			this.m_objectToRenderOriginalParent = this.m_ObjectToRender.transform.parent;
		}
		if (!this.m_selfOriginalParent)
		{
			this.m_selfOriginalParent = base.transform.parent;
		}
	}

	// Token: 0x06008F8A RID: 36746 RVA: 0x002E7D27 File Offset: 0x002E5F27
	private void FetchCapturedRenderers()
	{
		if (this.m_ObjectToRender)
		{
			this.m_captureRenderers = this.m_ObjectToRender.GetComponentsInChildren<Renderer>();
		}
	}

	// Token: 0x06008F8B RID: 36747 RVA: 0x002E7D48 File Offset: 0x002E5F48
	private void RegisterToService()
	{
		if (this.m_isRegisteredToManager || this.m_diamondRenderToTextureService == null || !this.m_ObjectToRender || !this.m_outputRenderer)
		{
			return;
		}
		bool flag = this.m_diamondRenderToTextureService.Register(this);
		if (flag)
		{
			this.SetupAuxRenderObjects();
		}
		this.m_isRegisteredToManager = flag;
	}

	// Token: 0x06008F8C RID: 36748 RVA: 0x002E7D9D File Offset: 0x002E5F9D
	private void UnregisterFromService()
	{
		if (!this.m_isRegisteredToManager)
		{
			return;
		}
		this.m_diamondRenderToTextureService.Unregister(this);
		this.m_isRegisteredToManager = false;
	}

	// Token: 0x06008F8D RID: 36749 RVA: 0x002E7DBC File Offset: 0x002E5FBC
	private void UpdatePlaneUVS(Rect atlasUV)
	{
		if (this.m_RenderToObject)
		{
			Mesh mesh = this.m_RenderToObject.GetComponent<MeshFilter>().mesh;
			Vector2[] uv = mesh.uv;
			Rect currentUVBounds = this.GetCurrentUVBounds(uv);
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

	// Token: 0x06008F8E RID: 36750 RVA: 0x002E7EAC File Offset: 0x002E60AC
	private Rect GetCurrentUVBounds(Vector2[] currentUv)
	{
		Vector2 one = Vector2.one;
		Vector2 zero = Vector2.zero;
		foreach (Vector2 vector in currentUv)
		{
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

	// Token: 0x06008F8F RID: 36751 RVA: 0x002E7F84 File Offset: 0x002E6184
	private void UpdateMaterial(RenderTexture atlasTexture)
	{
		if (!this.m_outputRenderer)
		{
			return;
		}
		if (this.m_RenderMaterial == DiamondRenderToTexture.RenderToTextureMaterial.Transparent)
		{
			this.TransparentMaterial.mainTexture = atlasTexture;
			this.m_outputRenderer.SetMaterial(this.TransparentMaterial);
			return;
		}
		this.m_outputRenderer.GetMaterial().mainTexture = atlasTexture;
	}

	// Token: 0x06008F90 RID: 36752 RVA: 0x002E7FD7 File Offset: 0x002E61D7
	public void UpdateMaterialBlend(bool inPlay)
	{
		this.TransparentMaterial.SetFloat("_LightingBlend", inPlay ? 1f : 0f);
	}

	// Token: 0x06008F91 RID: 36753 RVA: 0x002E7FF8 File Offset: 0x002E61F8
	private void CreateRenderCommands()
	{
		if (this.OpaqueRenderCommands == null)
		{
			this.OpaqueRenderCommands = new List<DiamondRenderToTexture.RenderCommand>(this.m_captureRenderers.Length);
		}
		else
		{
			this.OpaqueRenderCommands.Clear();
		}
		if (this.TransparentRenderCommands == null)
		{
			this.TransparentRenderCommands = new List<DiamondRenderToTexture.RenderCommand>(this.m_captureRenderers.Length);
		}
		else
		{
			this.TransparentRenderCommands.Clear();
		}
		Renderer[] captureRenderers = this.m_captureRenderers;
		int i = 0;
		while (i < captureRenderers.Length)
		{
			Renderer renderer = captureRenderers[i];
			List<Material> sharedMaterials = renderer.GetSharedMaterials();
			UnityEngine.Object exists = renderer as MeshRenderer;
			SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
			int num = 1;
			if (!exists)
			{
				goto IL_A3;
			}
			MeshFilter component = renderer.GetComponent<MeshFilter>();
			if (component)
			{
				num = component.sharedMesh.subMeshCount;
				goto IL_A3;
			}
			IL_15A:
			i++;
			continue;
			IL_A3:
			if (skinnedMeshRenderer)
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
				if (material.renderQueue < 3000)
				{
					this.OpaqueRenderCommands.Add(new DiamondRenderToTexture.RenderCommand
					{
						Renderer = renderer,
						Material = material,
						MeshIndex = j
					});
				}
				else
				{
					this.TransparentRenderCommands.Add(new DiamondRenderToTexture.RenderCommand
					{
						Renderer = renderer,
						Material = material,
						MeshIndex = j
					});
				}
			}
			goto IL_15A;
		}
	}

	// Token: 0x06008F92 RID: 36754 RVA: 0x002E816C File Offset: 0x002E636C
	private static void GizmosDrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20f)
	{
		Gizmos.color = color;
		Gizmos.DrawRay(pos, direction);
		Vector3 a = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f + arrowHeadAngle, 0f) * new Vector3(0f, 0f, 1f);
		Vector3 a2 = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f - arrowHeadAngle, 0f) * new Vector3(0f, 0f, 1f);
		Gizmos.DrawRay(pos + direction, a * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, a2 * arrowHeadLength);
	}

	// Token: 0x04007829 RID: 30761
	private static readonly Vector3 ALPHA_OBJECT_OFFSET = new Vector3(0f, 1000f, 0f);

	// Token: 0x0400782A RID: 30762
	private static readonly Color GIZMOS_COLOR = new Color(1f, 1f, 0f, 0.8f);

	// Token: 0x0400782B RID: 30763
	public GameObject m_ObjectToRender;

	// Token: 0x0400782C RID: 30764
	public GameObject m_AlphaObjectToRender;

	// Token: 0x0400782D RID: 30765
	public bool m_AllowRepetition;

	// Token: 0x0400782E RID: 30766
	public bool m_HideRenderObject = true;

	// Token: 0x0400782F RID: 30767
	public bool m_RealtimeRender;

	// Token: 0x04007830 RID: 30768
	public bool m_RealtimeTranslation;

	// Token: 0x04007831 RID: 30769
	public bool m_OpaqueObjectAlphaFill;

	// Token: 0x04007832 RID: 30770
	public DiamondRenderToTexture.RenderToTextureMaterial m_RenderMaterial;

	// Token: 0x04007833 RID: 30771
	public Material m_Material;

	// Token: 0x04007834 RID: 30772
	public bool m_CreateRenderPlane;

	// Token: 0x04007835 RID: 30773
	public Color m_ClearColor = Color.clear;

	// Token: 0x04007836 RID: 30774
	public GameObject m_RenderToObject;

	// Token: 0x04007837 RID: 30775
	[Range(1f, 2048f)]
	public int m_Resolution = 128;

	// Token: 0x04007838 RID: 30776
	public Vector3 m_bounds = Vector3.one;

	// Token: 0x04007839 RID: 30777
	public bool m_UniformWorldScale;

	// Token: 0x0400783A RID: 30778
	public Vector3 m_PositionOffset = Vector3.zero;

	// Token: 0x0400783B RID: 30779
	private const string TRANSPARENT_SHADER_NAME = "Hidden/R2TTransparent";

	// Token: 0x0400783C RID: 30780
	private Shader m_TransparentShader;

	// Token: 0x0400783D RID: 30781
	private Material m_TransparentMaterial;

	// Token: 0x04007844 RID: 30788
	private bool m_isRegisteredToManager;

	// Token: 0x04007845 RID: 30789
	private bool m_isDirty;

	// Token: 0x04007846 RID: 30790
	private DiamondRenderToTextureService m_diamondRenderToTextureService;

	// Token: 0x04007847 RID: 30791
	private Vector3 m_worldSize;

	// Token: 0x04007848 RID: 30792
	private Vector3 m_worldScale;

	// Token: 0x04007849 RID: 30793
	private DiamondRenderToTexture.TransformData m_transformSnapshot;

	// Token: 0x0400784A RID: 30794
	private Bounds m_renderBounds = new Bounds(Vector3.zero, Vector3.zero);

	// Token: 0x0400784B RID: 30795
	private Renderer m_outputRenderer;

	// Token: 0x0400784C RID: 30796
	private Renderer[] m_captureRenderers;

	// Token: 0x0400784D RID: 30797
	private DiamondRenderToTexture.TransformData m_atlasPositionSnapshot;

	// Token: 0x0400784E RID: 30798
	private Transform m_selfOriginalParent;

	// Token: 0x0400784F RID: 30799
	private Transform m_objectToRenderOriginalParent;

	// Token: 0x020026C7 RID: 9927
	public enum RenderToTextureMaterial
	{
		// Token: 0x0400F204 RID: 61956
		Custom,
		// Token: 0x0400F205 RID: 61957
		Transparent,
		// Token: 0x0400F206 RID: 61958
		Additive,
		// Token: 0x0400F207 RID: 61959
		Bloom,
		// Token: 0x0400F208 RID: 61960
		AlphaClip,
		// Token: 0x0400F209 RID: 61961
		AlphaClipBloom
	}

	// Token: 0x020026C8 RID: 9928
	public enum AlphaClipShader
	{
		// Token: 0x0400F20B RID: 61963
		Standard,
		// Token: 0x0400F20C RID: 61964
		ColorGradient
	}

	// Token: 0x020026C9 RID: 9929
	public struct TransformData
	{
		// Token: 0x0400F20D RID: 61965
		public Vector3 position;

		// Token: 0x0400F20E RID: 61966
		public Vector3 localScale;

		// Token: 0x0400F20F RID: 61967
		public Quaternion rotation;

		// Token: 0x0400F210 RID: 61968
		public Vector3 up;

		// Token: 0x0400F211 RID: 61969
		public Vector3 forward;

		// Token: 0x0400F212 RID: 61970
		public int layer;

		// Token: 0x0400F213 RID: 61971
		public Transform objectParent;

		// Token: 0x0400F214 RID: 61972
		public Transform atlasedComponentParent;
	}

	// Token: 0x020026CA RID: 9930
	public struct RenderCommand
	{
		// Token: 0x0400F215 RID: 61973
		public Renderer Renderer;

		// Token: 0x0400F216 RID: 61974
		public Material Material;

		// Token: 0x0400F217 RID: 61975
		public int MeshIndex;
	}
}
