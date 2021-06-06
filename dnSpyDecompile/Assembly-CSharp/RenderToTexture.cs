using System;
using System.Collections.Generic;
using Hearthstone.Extensions;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000A6E RID: 2670
public class RenderToTexture : MonoBehaviour, IPopupRendering
{
	// Token: 0x17000800 RID: 2048
	// (get) Token: 0x06008F1E RID: 36638 RVA: 0x002E4098 File Offset: 0x002E2298
	protected Vector3 Offset
	{
		get
		{
			if (this.m_Offset == Vector3.zero)
			{
				RenderToTexture.s_offset.x = RenderToTexture.s_offset.x - 300f;
				if (RenderToTexture.s_offset.x < -90000f)
				{
					RenderToTexture.s_offset.x = -4000f;
					RenderToTexture.s_offset.y = RenderToTexture.s_offset.y - 300f;
					if (RenderToTexture.s_offset.y < -90000f)
					{
						RenderToTexture.s_offset.y = -4000f;
						RenderToTexture.s_offset.z = RenderToTexture.s_offset.z - 300f;
						if (RenderToTexture.s_offset.z < -90000f)
						{
							RenderToTexture.s_offset.z = -4000f;
						}
					}
				}
				this.m_Offset = RenderToTexture.s_offset;
			}
			return this.m_Offset;
		}
	}

	// Token: 0x17000801 RID: 2049
	// (get) Token: 0x06008F1F RID: 36639 RVA: 0x002E4164 File Offset: 0x002E2364
	protected Material AlphaBlendMaterial
	{
		get
		{
			if (this.m_AlphaBlendMaterial == null)
			{
				if (this.m_AlphaBlendShader == null)
				{
					this.m_AlphaBlendShader = ShaderUtils.FindShader("Hidden/R2TColorAlphaCombine");
					if (!this.m_AlphaBlendShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TColorAlphaCombine");
					}
				}
				this.m_AlphaBlendMaterial = new Material(this.m_AlphaBlendShader);
				SceneUtils.SetHideFlags(this.m_AlphaBlendMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaBlendMaterial;
		}
	}

	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x06008F20 RID: 36640 RVA: 0x002E41D8 File Offset: 0x002E23D8
	protected Material AlphaBlendAddMaterial
	{
		get
		{
			if (this.m_AlphaBlendAddMaterial == null)
			{
				if (this.m_AlphaBlendAddShader == null)
				{
					this.m_AlphaBlendAddShader = ShaderUtils.FindShader("Hidden/R2TColorAlphaCombineAdd");
					if (!this.m_AlphaBlendAddShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TColorAlphaCombineAdd");
					}
				}
				this.m_AlphaBlendAddMaterial = new Material(this.m_AlphaBlendAddShader);
				SceneUtils.SetHideFlags(this.m_AlphaBlendAddMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaBlendAddMaterial;
		}
	}

	// Token: 0x17000803 RID: 2051
	// (get) Token: 0x06008F21 RID: 36641 RVA: 0x002E424C File Offset: 0x002E244C
	protected Material AdditiveMaterial
	{
		get
		{
			if (this.m_AdditiveMaterial == null)
			{
				if (this.m_AdditiveShader == null)
				{
					this.m_AdditiveShader = ShaderUtils.FindShader("Hidden/R2TAdditive");
					if (!this.m_AdditiveShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAdditive");
					}
				}
				this.m_AdditiveMaterial = new Material(this.m_AdditiveShader);
				SceneUtils.SetHideFlags(this.m_AdditiveMaterial, HideFlags.DontSave);
			}
			return this.m_AdditiveMaterial;
		}
	}

	// Token: 0x17000804 RID: 2052
	// (get) Token: 0x06008F22 RID: 36642 RVA: 0x002E42C0 File Offset: 0x002E24C0
	protected Material BloomMaterial
	{
		get
		{
			if (this.m_BloomMaterial == null)
			{
				if (this.m_BloomShader == null)
				{
					this.m_BloomShader = ShaderUtils.FindShader("Hidden/R2TBloom");
					if (!this.m_BloomShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBloom");
					}
				}
				this.m_BloomMaterial = new Material(this.m_BloomShader);
				SceneUtils.SetHideFlags(this.m_BloomMaterial, HideFlags.DontSave);
			}
			return this.m_BloomMaterial;
		}
	}

	// Token: 0x17000805 RID: 2053
	// (get) Token: 0x06008F23 RID: 36643 RVA: 0x002E4334 File Offset: 0x002E2534
	protected Material BloomMaterialAlpha
	{
		get
		{
			if (this.m_BloomMaterialAlpha == null)
			{
				if (this.m_BloomShaderAlpha == null)
				{
					this.m_BloomShaderAlpha = ShaderUtils.FindShader("Hidden/R2TBloomAlpha");
					if (!this.m_BloomShaderAlpha)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBloomAlpha");
					}
				}
				this.m_BloomMaterialAlpha = new Material(this.m_BloomShaderAlpha);
				SceneUtils.SetHideFlags(this.m_BloomMaterialAlpha, HideFlags.DontSave);
			}
			return this.m_BloomMaterialAlpha;
		}
	}

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x06008F24 RID: 36644 RVA: 0x002E43A8 File Offset: 0x002E25A8
	protected Material BlurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				if (this.m_BlurShader == null)
				{
					this.m_BlurShader = ShaderUtils.FindShader("Hidden/R2TBlur");
					if (!this.m_BlurShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TBlur");
					}
				}
				this.m_BlurMaterial = new Material(this.m_BlurShader);
				SceneUtils.SetHideFlags(this.m_BlurMaterial, HideFlags.DontSave);
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x06008F25 RID: 36645 RVA: 0x002E441C File Offset: 0x002E261C
	protected Material AlphaBlurMaterial
	{
		get
		{
			if (this.m_AlphaBlurMaterial == null)
			{
				if (this.m_AlphaBlurShader == null)
				{
					this.m_AlphaBlurShader = ShaderUtils.FindShader("Hidden/R2TAlphaBlur");
					if (!this.m_AlphaBlurShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaBlur");
					}
				}
				this.m_AlphaBlurMaterial = new Material(this.m_AlphaBlurShader);
				SceneUtils.SetHideFlags(this.m_AlphaBlurMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaBlurMaterial;
		}
	}

	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x06008F26 RID: 36646 RVA: 0x002E4490 File Offset: 0x002E2690
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

	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x06008F27 RID: 36647 RVA: 0x002E4504 File Offset: 0x002E2704
	protected Material AlphaClipMaterial
	{
		get
		{
			if (this.m_AlphaClipMaterial == null)
			{
				if (this.m_AlphaClipShader == null)
				{
					this.m_AlphaClipShader = ShaderUtils.FindShader("Hidden/R2TAlphaClip");
					if (!this.m_AlphaClipShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClip");
					}
				}
				this.m_AlphaClipMaterial = new Material(this.m_AlphaClipShader);
				SceneUtils.SetHideFlags(this.m_AlphaClipMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaClipMaterial;
		}
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x06008F28 RID: 36648 RVA: 0x002E4578 File Offset: 0x002E2778
	protected Material AlphaClipBloomMaterial
	{
		get
		{
			if (this.m_AlphaClipBloomMaterial == null)
			{
				if (this.m_AlphaClipBloomShader == null)
				{
					this.m_AlphaClipBloomShader = ShaderUtils.FindShader("Hidden/R2TAlphaClipBloom");
					if (!this.m_AlphaClipBloomShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClipBloom");
					}
				}
				this.m_AlphaClipBloomMaterial = new Material(this.m_AlphaClipBloomShader);
				SceneUtils.SetHideFlags(this.m_AlphaClipBloomMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaClipBloomMaterial;
		}
	}

	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x06008F29 RID: 36649 RVA: 0x002E45EC File Offset: 0x002E27EC
	protected Material AlphaClipGradientMaterial
	{
		get
		{
			if (this.m_AlphaClipGradientMaterial == null)
			{
				if (this.m_AlphaClipGradientShader == null)
				{
					this.m_AlphaClipGradientShader = ShaderUtils.FindShader("Hidden/R2TAlphaClipGradient");
					if (!this.m_AlphaClipGradientShader)
					{
						Debug.LogError("Failed to load RenderToTexture Shader: Hidden/R2TAlphaClipGradient");
					}
				}
				this.m_AlphaClipGradientMaterial = new Material(this.m_AlphaClipGradientShader);
				SceneUtils.SetHideFlags(this.m_AlphaClipGradientMaterial, HideFlags.DontSave);
			}
			return this.m_AlphaClipGradientMaterial;
		}
	}

	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06008F2B RID: 36651 RVA: 0x002E4669 File Offset: 0x002E2869
	// (set) Token: 0x06008F2A RID: 36650 RVA: 0x002E4660 File Offset: 0x002E2860
	public bool DontRefreshOnFocus { get; set; }

	// Token: 0x06008F2C RID: 36652 RVA: 0x002E4674 File Offset: 0x002E2874
	private void Awake()
	{
		this.m_AlphaFillShader = ShaderUtils.FindShader("Custom/AlphaFillOpaque");
		if (!this.m_AlphaFillShader)
		{
			Debug.LogError("Failed to load RenderToTexture Shader: Custom/AlphaFillOpaque");
		}
		this.m_OffscreenPos = this.Offset;
		if (this.m_Material != null)
		{
			this.m_Material = UnityEngine.Object.Instantiate<Material>(this.m_Material);
			this.m_hasMaterialInstance = true;
			RenderToTexture.GetMaterialManagerService().IgnoreMaterial(this.m_Material);
		}
	}

	// Token: 0x06008F2D RID: 36653 RVA: 0x002E46EA File Offset: 0x002E28EA
	private void Start()
	{
		if (this.m_RenderOnStart)
		{
			this.m_isDirty = true;
		}
		this.Init();
	}

	// Token: 0x06008F2E RID: 36654 RVA: 0x002E4704 File Offset: 0x002E2904
	private void Update()
	{
		if (!this.m_renderEnabled)
		{
			return;
		}
		if (this.m_RenderTexture && !this.m_RenderTexture.IsCreated())
		{
			Log.Graphics.Print("RenderToTexture Texture lost. Render Called", Array.Empty<object>());
			this.m_isDirty = true;
			this.RenderTex();
			return;
		}
		if (this.m_LateUpdate)
		{
			return;
		}
		if (this.m_HideRenderObject && this.m_ObjectToRender)
		{
			this.PositionHiddenObjectsAndCameras();
		}
		if (this.m_RealtimeRender || this.m_isDirty)
		{
			this.RenderTex();
		}
	}

	// Token: 0x06008F2F RID: 36655 RVA: 0x002E4790 File Offset: 0x002E2990
	private void LateUpdate()
	{
		if (!this.m_renderEnabled)
		{
			return;
		}
		if (this.m_LateUpdate)
		{
			if (this.m_HideRenderObject && this.m_ObjectToRender)
			{
				this.PositionHiddenObjectsAndCameras();
			}
			if (this.m_RealtimeRender || this.m_isDirty)
			{
				this.RenderTex();
				return;
			}
		}
		else
		{
			if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClipBloom || this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.Bloom)
			{
				this.RenderBloom();
				return;
			}
			if (this.m_BloomPlaneGameObject)
			{
				UnityEngine.Object.DestroyImmediate(this.m_BloomPlaneGameObject);
			}
		}
	}

	// Token: 0x06008F30 RID: 36656 RVA: 0x002E4811 File Offset: 0x002E2A11
	private void OnApplicationFocus(bool state)
	{
		if (this.DontRefreshOnFocus)
		{
			return;
		}
		if (this.m_RenderTexture && state)
		{
			this.m_isDirty = true;
			this.RenderTex();
		}
	}

	// Token: 0x06008F31 RID: 36657 RVA: 0x002E4838 File Offset: 0x002E2A38
	private void OnDrawGizmos()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.m_FarClip < 0f)
		{
			this.m_FarClip = 0f;
		}
		if (this.m_NearClip > 0f)
		{
			this.m_NearClip = 0f;
		}
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Vector3 a = new Vector3(0f, -this.m_NearClip * 0.5f, 0f);
		Gizmos.color = new Color(0.1f, 0.5f, 0.7f, 0.8f);
		Gizmos.DrawWireCube(a + this.m_PositionOffset, new Vector3(this.m_Width, -this.m_NearClip, this.m_Height));
		Gizmos.color = new Color(0.2f, 0.2f, 0.9f, 0.8f);
		Gizmos.DrawWireCube(new Vector3(0f, -this.m_FarClip * 0.5f, 0f) + this.m_PositionOffset, new Vector3(this.m_Width, -this.m_FarClip, this.m_Height));
		Gizmos.color = new Color(0.8f, 0.8f, 1f, 1f);
		Gizmos.DrawWireCube(this.m_PositionOffset, new Vector3(this.m_Width, 0f, this.m_Height));
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x06008F32 RID: 36658 RVA: 0x002E499C File Offset: 0x002E2B9C
	private void OnDisable()
	{
		this.RestoreAfterRender();
		if (this.m_ObjectToRender)
		{
			if (this.m_ObjectToRenderOrgParent != null)
			{
				this.m_ObjectToRender.transform.parent = this.m_ObjectToRenderOrgParent;
			}
			this.m_ObjectToRender.transform.localPosition = this.m_ObjectToRenderOrgPosition;
		}
		if (this.m_PlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_PlaneGameObject);
		}
		if (this.m_BloomPlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomPlaneGameObject);
		}
		if (this.m_BloomCapturePlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomCapturePlaneGameObject);
		}
		if (this.m_BloomCaptureCameraGO)
		{
			UnityEngine.Object.Destroy(this.m_BloomCaptureCameraGO);
		}
		this.ReleaseTexture();
		if (this.m_Camera)
		{
			this.m_Camera.enabled = false;
		}
		if (this.m_AlphaCamera)
		{
			this.m_AlphaCamera.enabled = false;
		}
		this.m_init = false;
		this.m_isDirty = true;
	}

	// Token: 0x06008F33 RID: 36659 RVA: 0x002E4A9C File Offset: 0x002E2C9C
	private void OnDestroy()
	{
		this.CleanUp();
	}

	// Token: 0x06008F34 RID: 36660 RVA: 0x002E4AA4 File Offset: 0x002E2CA4
	private void OnEnable()
	{
		if (this.m_RenderOnEnable)
		{
			this.RenderTex();
		}
	}

	// Token: 0x06008F35 RID: 36661 RVA: 0x002E4AB4 File Offset: 0x002E2CB4
	public RenderTexture Render()
	{
		this.m_isDirty = true;
		return this.m_RenderTexture;
	}

	// Token: 0x06008F36 RID: 36662 RVA: 0x002E4AC3 File Offset: 0x002E2CC3
	public RenderTexture RenderNow()
	{
		this.RenderTex();
		return this.m_RenderTexture;
	}

	// Token: 0x06008F37 RID: 36663 RVA: 0x002E4AD1 File Offset: 0x002E2CD1
	public void ForceTextureRebuild()
	{
		if (!base.enabled)
		{
			return;
		}
		this.ReleaseTexture();
		this.m_isDirty = true;
		this.RenderTex();
	}

	// Token: 0x06008F38 RID: 36664 RVA: 0x002E4AEF File Offset: 0x002E2CEF
	public void Show()
	{
		this.Show(false);
	}

	// Token: 0x06008F39 RID: 36665 RVA: 0x002E4AF8 File Offset: 0x002E2CF8
	public void Show(bool render)
	{
		this.m_renderEnabled = true;
		if (this.m_RenderToObject)
		{
			this.m_RenderToObject.GetComponent<Renderer>().enabled = true;
		}
		else if (this.m_PlaneGameObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
		if (render)
		{
			this.Render();
		}
	}

	// Token: 0x06008F3A RID: 36666 RVA: 0x002E4B74 File Offset: 0x002E2D74
	public void Hide()
	{
		this.m_renderEnabled = false;
		if (this.m_RenderToObject)
		{
			this.m_RenderToObject.GetComponent<Renderer>().enabled = false;
			return;
		}
		if (this.m_PlaneGameObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	// Token: 0x06008F3B RID: 36667 RVA: 0x002E4BE3 File Offset: 0x002E2DE3
	public void SetDirty()
	{
		this.m_init = false;
		this.m_isDirty = true;
	}

	// Token: 0x06008F3C RID: 36668 RVA: 0x002E4BF4 File Offset: 0x002E2DF4
	public Material GetRenderMaterial()
	{
		if (this.m_RenderToObject)
		{
			return this.m_RenderToObject.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_PlaneGameObject)
		{
			return this.m_PlaneGameObject.GetComponent<Renderer>().GetMaterial();
		}
		return this.m_Material;
	}

	// Token: 0x06008F3D RID: 36669 RVA: 0x002E4C43 File Offset: 0x002E2E43
	public GameObject GetRenderToObject()
	{
		if (this.m_RenderToObject)
		{
			return this.m_RenderToObject;
		}
		return this.m_PlaneGameObject;
	}

	// Token: 0x06008F3E RID: 36670 RVA: 0x002E4C5F File Offset: 0x002E2E5F
	public RenderTexture GetRenderTexture()
	{
		return this.m_RenderTexture;
	}

	// Token: 0x06008F3F RID: 36671 RVA: 0x002E4C67 File Offset: 0x002E2E67
	public Vector3 GetOffscreenPosition()
	{
		return this.m_OffscreenPos;
	}

	// Token: 0x06008F40 RID: 36672 RVA: 0x002E4C6F File Offset: 0x002E2E6F
	public Vector3 GetOffscreenPositionOffset()
	{
		return this.m_OffscreenPos - base.transform.position;
	}

	// Token: 0x06008F41 RID: 36673 RVA: 0x002E4C88 File Offset: 0x002E2E88
	private void Init()
	{
		if (this.m_init)
		{
			return;
		}
		if (this.m_RealtimeTranslation)
		{
			this.m_OffscreenGameObject = new GameObject();
			this.m_OffscreenGameObject.name = string.Format("R2TOffsetRenderRoot_{0}", base.name);
			this.m_OffscreenGameObject.transform.position = base.transform.position;
		}
		if (this.m_ObjectToRender)
		{
			if (!this.m_ObjectToRenderOrgPositionStored)
			{
				this.m_ObjectToRenderOrgParent = this.m_ObjectToRender.transform.parent;
				this.m_ObjectToRenderOrgPosition = this.m_ObjectToRender.transform.localPosition;
				this.m_ObjectToRenderOrgPositionStored = true;
			}
			if (this.m_HideRenderObject)
			{
				if (this.m_RealtimeTranslation)
				{
					this.m_ObjectToRender.transform.parent = this.m_OffscreenGameObject.transform;
					if (this.m_AlphaObjectToRender)
					{
						this.m_AlphaObjectToRender.transform.parent = this.m_OffscreenGameObject.transform;
					}
				}
				if (this.m_RenderToObject)
				{
					this.m_OriginalRenderPosition = this.m_RenderToObject.transform.position;
				}
				else
				{
					this.m_OriginalRenderPosition = base.transform.position;
				}
				if (this.m_ObjectToRender && this.m_ObjectToRenderOffset == Vector3.zero)
				{
					this.m_ObjectToRenderOffset = base.transform.position - this.m_ObjectToRender.transform.position;
				}
				if (this.m_AlphaObjectToRender && this.m_AlphaObjectToRenderOffset == Vector3.zero)
				{
					this.m_AlphaObjectToRenderOffset = base.transform.position - this.m_AlphaObjectToRender.transform.position;
				}
			}
		}
		else if (!this.m_ObjectToRenderOrgPositionStored)
		{
			this.m_ObjectToRenderOrgPosition = base.transform.localPosition;
			if (this.m_OffscreenGameObject != null)
			{
				this.m_OffscreenGameObject.transform.position = base.transform.position;
			}
			this.m_ObjectToRenderOrgPositionStored = true;
		}
		if (this.m_HideRenderObject)
		{
			if (this.m_RealtimeTranslation)
			{
				if (this.m_OffscreenGameObject != null)
				{
					this.m_OffscreenGameObject.transform.position = this.m_OffscreenPos;
				}
			}
			else if (this.m_ObjectToRender)
			{
				this.m_ObjectToRender.transform.position = this.m_OffscreenPos;
			}
			else
			{
				base.transform.position = this.m_OffscreenPos;
			}
		}
		if (this.m_ObjectToRender == null)
		{
			this.m_ObjectToRender = base.gameObject;
		}
		this.CalcWorldWidthHeightScale();
		this.CreateTexture();
		this.CreateCamera();
		if (this.m_OpaqueObjectAlphaFill || this.m_RenderMeshAsAlpha || this.m_AlphaObjectToRender != null)
		{
			this.CreateAlphaCamera();
		}
		if (!this.m_RenderToObject && this.m_CreateRenderPlane)
		{
			this.CreateRenderPlane();
		}
		if (this.m_RenderToObject)
		{
			this.m_RenderToObject.GetComponent<Renderer>().GetMaterial().renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
		}
		this.SetupMaterial();
		this.m_init = true;
	}

	// Token: 0x06008F42 RID: 36674 RVA: 0x002E4FAC File Offset: 0x002E31AC
	private void RenderTex()
	{
		if (!this.m_renderEnabled)
		{
			return;
		}
		this.Init();
		if (!this.m_init)
		{
			return;
		}
		if (this.m_Camera == null)
		{
			return;
		}
		this.SetupForRender();
		if (this.m_RenderMaterial != this.m_PreviousRenderMaterial || this.m_RenderQueue != this.m_previousRenderQueue)
		{
			this.SetupMaterial();
		}
		if (this.m_HideRenderObject && this.m_ObjectToRender)
		{
			this.PositionHiddenObjectsAndCameras();
		}
		if (this.m_OpaqueObjectAlphaFill || this.m_RenderMeshAsAlpha || this.m_AlphaObjectToRender != null)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, this.m_RenderTexture.format);
			this.m_Camera.targetTexture = temporary;
			this.CameraRender();
			RenderTexture temporary2 = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, 16, RenderTextureFormat.R8);
			this.AlphaCameraRender(temporary2);
			if (this.m_OpaqueObjectAlphaFill)
			{
				this.AlphaBlendAddMaterial.SetTexture("_AlphaTex", temporary2);
			}
			else
			{
				this.AlphaBlendMaterial.SetTexture("_AlphaTex", temporary2);
			}
			if (this.m_BlurAmount > 0f)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, this.m_RenderTexture.format);
				if (this.m_OpaqueObjectAlphaFill)
				{
					Graphics.Blit(temporary, temporary3, this.AlphaBlendAddMaterial);
				}
				else
				{
					Graphics.Blit(temporary, temporary3, this.AlphaBlendMaterial);
				}
				this.CameraRender();
				Material sampleMat = this.BlurMaterial;
				if (this.m_BlurAlphaOnly)
				{
					sampleMat = this.AlphaBlurMaterial;
				}
				this.m_RenderTexture.DiscardContents();
				this.Sample(temporary3, this.m_RenderTexture, sampleMat, this.m_BlurAmount);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				this.m_RenderTexture.DiscardContents();
				if (this.m_OpaqueObjectAlphaFill)
				{
					Graphics.Blit(temporary, this.m_RenderTexture, this.AlphaBlendAddMaterial);
				}
				else
				{
					Graphics.Blit(temporary, this.m_RenderTexture, this.AlphaBlendMaterial);
				}
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else if (this.m_BlurAmount > 0f)
		{
			RenderTexture temporary4 = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, this.m_RenderTexture.format);
			this.m_Camera.targetTexture = temporary4;
			this.CameraRender();
			Material sampleMat2 = this.BlurMaterial;
			if (this.m_BlurAlphaOnly)
			{
				sampleMat2 = this.m_AlphaBlurMaterial;
			}
			this.m_RenderTexture.DiscardContents();
			this.Sample(temporary4, this.m_RenderTexture, sampleMat2, this.m_BlurAmount);
			RenderTexture.ReleaseTemporary(temporary4);
		}
		else
		{
			this.m_Camera.targetTexture = this.m_RenderTexture;
			this.CameraRender();
		}
		if (this.m_RenderToObject)
		{
			Renderer renderer = this.m_RenderToObject.GetComponent<Renderer>();
			if (renderer == null)
			{
				renderer = this.m_RenderToObject.GetComponentInChildren<Renderer>();
			}
			if (this.m_ShaderTextureName != string.Empty)
			{
				renderer.GetMaterial().SetTexture(this.m_ShaderTextureName, this.m_RenderTexture);
			}
			else
			{
				renderer.GetMaterial().mainTexture = this.m_RenderTexture;
			}
		}
		else if (this.m_PlaneGameObject)
		{
			if (this.m_ShaderTextureName != string.Empty)
			{
				this.m_PlaneGameObject.GetComponent<Renderer>().GetMaterial().SetTexture(this.m_ShaderTextureName, this.m_RenderTexture);
			}
			else
			{
				this.m_PlaneGameObject.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_RenderTexture;
			}
		}
		if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClip || this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClipBloom)
		{
			GameObject gameObject = this.m_PlaneGameObject;
			if (this.m_RenderToObject)
			{
				gameObject = this.m_RenderToObject;
			}
			Material material = gameObject.GetComponent<Renderer>().GetMaterial();
			material.SetFloat("_Cutoff", this.m_AlphaClip);
			material.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture.AlphaClipShader.ColorGradient)
			{
				material.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
		}
		if (!this.m_RealtimeRender)
		{
			this.RestoreAfterRender();
		}
		if (this.m_popupRoot != null)
		{
			if (this.m_PlaneGameObject != null && this.m_PlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				this.m_PlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(this.m_popupRoot);
			}
			if (this.m_BloomPlaneGameObject != null && this.m_BloomPlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				this.m_BloomPlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(this.m_popupRoot);
			}
			if (this.m_BloomCapturePlaneGameObject != null && this.m_BloomCapturePlaneGameObject.GetComponent<PopupRenderer>() == null)
			{
				this.m_BloomCapturePlaneGameObject.AddComponent<PopupRenderer>().EnablePopupRendering(this.m_popupRoot);
			}
		}
		this.m_isDirty = false;
	}

	// Token: 0x06008F43 RID: 36675 RVA: 0x002E54A4 File Offset: 0x002E36A4
	private void RenderBloom()
	{
		if (this.m_BloomIntensity == 0f)
		{
			if (this.m_BloomPlaneGameObject)
			{
				UnityEngine.Object.DestroyImmediate(this.m_BloomPlaneGameObject);
			}
			return;
		}
		if (this.m_BloomIntensity == 0f)
		{
			if (this.m_BloomPlaneGameObject)
			{
				UnityEngine.Object.DestroyImmediate(this.m_BloomPlaneGameObject);
			}
			return;
		}
		int num = (int)((float)this.m_RenderTexture.width * Mathf.Clamp01(this.m_BloomResolutionRatio));
		int num2 = (int)((float)this.m_RenderTexture.height * Mathf.Clamp01(this.m_BloomResolutionRatio));
		RenderTexture renderTexture = this.m_RenderTexture;
		if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClipBloom)
		{
			if (!this.m_BloomPlaneGameObject)
			{
				this.CreateBloomPlane();
			}
			if (!this.m_BloomRenderTexture)
			{
				this.m_BloomRenderTexture = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			}
		}
		if (!this.m_BloomRenderBuffer1)
		{
			this.m_BloomRenderBuffer1 = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
		}
		if (!this.m_BloomRenderBuffer2)
		{
			this.m_BloomRenderBuffer2 = RenderTextureTracker.Get().CreateNewTexture(num, num2, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
		}
		Material material = this.BloomMaterial;
		if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClipBloom)
		{
			material = this.AlphaClipBloomMaterial;
			renderTexture = this.m_BloomRenderTexture;
			if (!this.m_BloomCaptureCameraGO)
			{
				this.CreateBloomCaptureCamera();
			}
			this.m_BloomCaptureCamera.targetTexture = this.m_BloomRenderTexture;
			material.SetFloat("_Cutoff", this.m_AlphaClip);
			material.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			this.m_BloomCaptureCamera.Render();
		}
		if (this.m_BloomRenderType == RenderToTexture.BloomRenderType.Alpha)
		{
			material = this.BloomMaterialAlpha;
			material.SetFloat("_AlphaIntensity", this.m_BloomAlphaIntensity);
		}
		float num3 = 1f / (float)num;
		float num4 = 1f / (float)num2;
		material.SetFloat("_Threshold", this.m_BloomThreshold);
		material.SetFloat("_Intensity", this.m_BloomIntensity / (1f - this.m_BloomThreshold));
		material.SetVector("_OffsetA", new Vector4(1.5f * num3, 1.5f * num4, -1.5f * num3, 1.5f * num4));
		material.SetVector("_OffsetB", new Vector4(-1.5f * num3, -1.5f * num4, 1.5f * num3, -1.5f * num4));
		this.m_BloomRenderBuffer2.DiscardContents();
		Graphics.Blit(renderTexture, this.m_BloomRenderBuffer2, material, 1);
		num3 *= 4f * this.m_BloomBlur;
		num4 *= 4f * this.m_BloomBlur;
		material.SetVector("_OffsetA", new Vector4(1.5f * num3, 0f, -1.5f * num3, 0f));
		material.SetVector("_OffsetB", new Vector4(0.5f * num3, 0f, -0.5f * num3, 0f));
		this.m_BloomRenderBuffer1.DiscardContents();
		Graphics.Blit(this.m_BloomRenderBuffer2, this.m_BloomRenderBuffer1, material, 2);
		material.SetVector("_OffsetA", new Vector4(0f, 1.5f * num4, 0f, -1.5f * num4));
		material.SetVector("_OffsetB", new Vector4(0f, 0.5f * num4, 0f, -0.5f * num4));
		renderTexture.DiscardContents();
		Graphics.Blit(this.m_BloomRenderBuffer1, renderTexture, material, 2);
		Material material2 = this.m_PlaneGameObject.GetComponent<Renderer>().GetMaterial();
		if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.AlphaClipBloom)
		{
			Material material3 = this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial();
			material3.color = this.m_BloomColor;
			material3.mainTexture = renderTexture;
			if (this.m_PlaneGameObject)
			{
				material3.renderQueue = material2.renderQueue + 1;
				return;
			}
		}
		else
		{
			if (this.m_RenderToObject)
			{
				Material material4 = this.m_RenderToObject.GetComponent<Renderer>().GetMaterial();
				material4.color = this.m_BloomColor;
				material4.mainTexture = renderTexture;
				return;
			}
			material2.color = this.m_BloomColor;
			material2.mainTexture = renderTexture;
		}
	}

	// Token: 0x06008F44 RID: 36676 RVA: 0x002E58BC File Offset: 0x002E3ABC
	private void SetupForRender()
	{
		this.CalcWorldWidthHeightScale();
		if (!this.m_RenderTexture)
		{
			this.CreateTexture();
		}
		if (!this.m_HideRenderObject)
		{
			return;
		}
		if (this.m_PlaneGameObject)
		{
			this.m_PlaneGameObject.transform.localPosition = this.m_PositionOffset;
			this.m_PlaneGameObject.layer = base.gameObject.layer;
		}
		if (this.m_Camera != null)
		{
			this.m_Camera.backgroundColor = this.m_ClearColor;
		}
	}

	// Token: 0x06008F45 RID: 36677 RVA: 0x002E5944 File Offset: 0x002E3B44
	private void SetupMaterial()
	{
		GameObject gameObject = this.m_PlaneGameObject;
		if (this.m_RenderToObject)
		{
			gameObject = this.m_RenderToObject;
			if (this.m_RenderMaterial == RenderToTexture.RenderToTextureMaterial.Custom)
			{
				return;
			}
		}
		if (gameObject == null)
		{
			return;
		}
		Renderer component = gameObject.GetComponent<Renderer>();
		switch (this.m_RenderMaterial)
		{
		case RenderToTexture.RenderToTextureMaterial.Transparent:
			component.SetMaterial(this.TransparentMaterial);
			break;
		case RenderToTexture.RenderToTextureMaterial.Additive:
			component.SetMaterial(this.AdditiveMaterial);
			break;
		case RenderToTexture.RenderToTextureMaterial.Bloom:
			if (this.m_BloomBlend == RenderToTexture.BloomBlendType.Additive)
			{
				component.SetMaterial(this.AdditiveMaterial);
			}
			else if (this.m_BloomBlend == RenderToTexture.BloomBlendType.Transparent)
			{
				component.SetMaterial(this.TransparentMaterial);
			}
			break;
		case RenderToTexture.RenderToTextureMaterial.AlphaClip:
		{
			Material material;
			if (this.m_AlphaClipRenderStyle == RenderToTexture.AlphaClipShader.Standard)
			{
				material = this.AlphaClipMaterial;
			}
			else
			{
				material = this.AlphaClipGradientMaterial;
			}
			component.SetMaterial(material);
			material.SetFloat("_Cutoff", this.m_AlphaClip);
			material.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture.AlphaClipShader.ColorGradient)
			{
				material.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
			break;
		}
		case RenderToTexture.RenderToTextureMaterial.AlphaClipBloom:
		{
			Material material2;
			if (this.m_AlphaClipRenderStyle == RenderToTexture.AlphaClipShader.Standard)
			{
				material2 = this.AlphaClipMaterial;
			}
			else
			{
				material2 = this.AlphaClipGradientMaterial;
			}
			component.SetMaterial(material2);
			material2.SetFloat("_Cutoff", this.m_AlphaClip);
			material2.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material2.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture.AlphaClipShader.ColorGradient)
			{
				material2.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
			break;
		}
		default:
			if (this.m_Material != null)
			{
				component.SetMaterial(this.m_Material);
			}
			break;
		}
		Material material3 = component.GetMaterial();
		if (material3 != null)
		{
			material3.color *= this.m_TintColor;
		}
		if (this.m_BloomIntensity > 0f && this.m_BloomPlaneGameObject)
		{
			this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial().color = this.m_BloomColor;
		}
		component.sortingOrder = this.m_RenderQueue;
		material3.renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
		if (this.m_BloomPlaneGameObject)
		{
			Renderer component2 = this.m_BloomPlaneGameObject.GetComponent<Renderer>();
			component2.sortingOrder = this.m_RenderQueue + 1;
			component2.GetMaterial().renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue + 1;
		}
		this.m_PreviousRenderMaterial = this.m_RenderMaterial;
		this.m_previousRenderQueue = this.m_RenderQueue;
	}

	// Token: 0x06008F46 RID: 36678 RVA: 0x002E5BDC File Offset: 0x002E3DDC
	private void PositionHiddenObjectsAndCameras()
	{
		Vector3 b = Vector3.zero;
		if (this.m_RenderToObject)
		{
			b = this.m_RenderToObject.transform.position - this.m_OriginalRenderPosition;
		}
		else
		{
			b = base.transform.position - this.m_OriginalRenderPosition;
		}
		if (this.m_RealtimeTranslation)
		{
			this.m_OffscreenGameObject.transform.position = this.m_OffscreenPos + b;
			this.m_OffscreenGameObject.transform.rotation = base.transform.rotation;
			if (this.m_AlphaObjectToRender)
			{
				this.m_AlphaObjectToRender.transform.position = this.m_OffscreenPos - this.ALPHA_OBJECT_OFFSET - this.m_AlphaObjectToRenderOffset + b;
				this.m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
				return;
			}
		}
		else
		{
			if (this.m_ObjectToRender)
			{
				this.m_ObjectToRender.transform.rotation = Quaternion.identity;
				this.m_ObjectToRender.transform.position = this.m_OffscreenPos - this.m_ObjectToRenderOffset + this.m_PositionOffset;
				this.m_ObjectToRender.transform.rotation = base.transform.rotation;
			}
			if (this.m_AlphaObjectToRender)
			{
				this.m_AlphaObjectToRender.transform.position = this.m_OffscreenPos - this.ALPHA_OBJECT_OFFSET;
				this.m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
			}
			if (this.m_CameraGO == null)
			{
				return;
			}
			this.m_CameraGO.transform.rotation = Quaternion.identity;
			if (this.m_ObjectToRender)
			{
				this.m_CameraGO.transform.position = this.m_ObjectToRender.transform.position + this.m_CameraOffset;
			}
			else
			{
				this.m_CameraGO.transform.position = this.m_OffscreenPos + this.m_PositionOffset + this.m_CameraOffset;
			}
			this.m_CameraGO.transform.rotation = this.m_ObjectToRender.transform.rotation;
			this.m_CameraGO.transform.Rotate(90f, 0f, 0f);
		}
	}

	// Token: 0x06008F47 RID: 36679 RVA: 0x002E5E4C File Offset: 0x002E404C
	private void RestoreAfterRender()
	{
		if (this.m_HideRenderObject)
		{
			return;
		}
		if (this.m_ObjectToRender)
		{
			if (this.m_ObjectToRenderOrgParent != null)
			{
				this.m_ObjectToRender.transform.parent = this.m_ObjectToRenderOrgParent;
			}
			this.m_ObjectToRender.transform.localPosition = this.m_ObjectToRenderOrgPosition;
			return;
		}
		base.transform.localPosition = this.m_ObjectToRenderOrgPosition;
	}

	// Token: 0x06008F48 RID: 36680 RVA: 0x002E5EBC File Offset: 0x002E40BC
	private void CreateTexture()
	{
		if (this.m_RenderTexture != null)
		{
			return;
		}
		Vector2 vector = this.CalcTextureSize();
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
		this.m_RenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, this.m_RenderTextureFormat, RenderTextureReadWrite.Default);
		this.m_RenderTexture.Create();
		if (this.m_Camera)
		{
			this.m_Camera.targetTexture = this.m_RenderTexture;
		}
	}

	// Token: 0x06008F49 RID: 36681 RVA: 0x002E5F7C File Offset: 0x002E417C
	private void ReleaseTexture()
	{
		if (RenderTexture.active == this.m_RenderTexture)
		{
			RenderTexture.active = null;
		}
		if (RenderTexture.active == this.m_BloomRenderTexture)
		{
			RenderTexture.active = null;
		}
		if (this.m_RenderTexture != null)
		{
			if (this.m_Camera)
			{
				this.m_Camera.targetTexture = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_RenderTexture);
			this.m_RenderTexture = null;
		}
		if (this.m_BloomRenderTexture != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_BloomRenderTexture);
			this.m_BloomRenderTexture = null;
		}
		if (this.m_BloomRenderBuffer1 != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_BloomRenderBuffer1);
			this.m_BloomRenderBuffer1 = null;
		}
		if (this.m_BloomRenderBuffer2 != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_BloomRenderBuffer2);
			this.m_BloomRenderBuffer2 = null;
		}
	}

	// Token: 0x06008F4A RID: 36682 RVA: 0x002E6068 File Offset: 0x002E4268
	private void CreateCamera()
	{
		if (this.m_Camera != null)
		{
			return;
		}
		this.m_CameraGO = new GameObject();
		this.m_Camera = this.m_CameraGO.AddComponent<Camera>();
		this.m_CameraGO.name = base.name + "_R2TRenderCamera";
		this.m_CameraGO.AddComponent<RenderToTextureCamera>();
		this.m_Camera.orthographic = true;
		if (this.m_HideRenderObject)
		{
			if (this.m_RealtimeTranslation)
			{
				this.m_OffscreenGameObject.transform.position = base.transform.position;
				this.m_CameraGO.transform.parent = this.m_OffscreenGameObject.transform;
				this.m_CameraGO.transform.localPosition = Vector3.zero + this.m_PositionOffset + this.m_CameraOffset;
				this.m_CameraGO.transform.rotation = base.transform.rotation;
				this.m_OffscreenGameObject.transform.position = this.m_OffscreenPos;
			}
			else
			{
				this.m_CameraGO.transform.parent = null;
				this.m_CameraGO.transform.position = this.m_OffscreenPos + this.m_PositionOffset + this.m_CameraOffset;
				this.m_CameraGO.transform.rotation = base.transform.rotation;
			}
		}
		else
		{
			this.m_CameraGO.transform.parent = base.transform;
			this.m_CameraGO.transform.position = base.transform.position + this.m_PositionOffset + this.m_CameraOffset;
			this.m_CameraGO.transform.rotation = base.transform.rotation;
		}
		this.m_CameraGO.transform.Rotate(90f, 0f, 0f);
		if (this.m_FarClip < 0f)
		{
			this.m_FarClip = 0f;
		}
		if (this.m_NearClip > 0f)
		{
			this.m_NearClip = 0f;
		}
		this.m_Camera.nearClipPlane = this.m_NearClip * this.m_WorldScale.y;
		this.m_Camera.farClipPlane = this.m_FarClip * this.m_WorldScale.y;
		Camera main = Camera.main;
		if (main != null)
		{
			this.m_Camera.depth = main.depth - 2f;
		}
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.backgroundColor = this.m_ClearColor;
		this.m_Camera.depthTextureMode = DepthTextureMode.None;
		this.m_Camera.renderingPath = RenderingPath.Forward;
		this.m_Camera.cullingMask = this.m_LayerMask;
		this.m_Camera.allowHDR = false;
		this.m_Camera.targetTexture = this.m_RenderTexture;
		this.m_Camera.enabled = false;
	}

	// Token: 0x06008F4B RID: 36683 RVA: 0x002E6360 File Offset: 0x002E4560
	private float OrthoSize()
	{
		if (this.m_OverrideCameraSize > 0f)
		{
			return this.m_OverrideCameraSize;
		}
		float result;
		if (this.m_WorldWidth > this.m_WorldHeight)
		{
			result = Mathf.Min(this.m_WorldWidth * 0.5f, this.m_WorldHeight * 0.5f);
		}
		else
		{
			result = this.m_WorldHeight * 0.5f;
		}
		return result;
	}

	// Token: 0x06008F4C RID: 36684 RVA: 0x002E63C4 File Offset: 0x002E45C4
	private void CameraRender()
	{
		this.m_Camera.orthographicSize = this.OrthoSize();
		this.m_Camera.farClipPlane = this.m_FarClip * this.m_WorldScale.z;
		this.m_Camera.nearClipPlane = this.m_NearClip * this.m_WorldScale.z;
		if (this.m_PlaneGameObject && !this.m_HideRenderObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if (this.m_ReplacmentShader)
		{
			this.m_Camera.RenderWithShader(this.m_ReplacmentShader, this.m_ReplacmentTag);
		}
		else
		{
			this.m_Camera.Render();
		}
		if (this.m_PlaneGameObject && !this.m_HideRenderObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}

	// Token: 0x06008F4D RID: 36685 RVA: 0x002E64D8 File Offset: 0x002E46D8
	private void CreateAlphaCamera()
	{
		if (this.m_AlphaCamera != null)
		{
			return;
		}
		this.m_AlphaCameraGO = new GameObject();
		this.m_AlphaCamera = this.m_AlphaCameraGO.AddComponent<Camera>();
		this.m_AlphaCameraGO.AddComponent<RenderToTextureCamera>();
		this.m_AlphaCameraGO.name = base.name + "_R2TAlphaRenderCamera";
		this.m_AlphaCamera.CopyFrom(this.m_Camera);
		this.m_AlphaCamera.enabled = false;
		this.m_AlphaCamera.backgroundColor = Color.clear;
		this.m_AlphaCamera.allowHDR = false;
		this.m_AlphaCameraGO.transform.parent = this.m_CameraGO.transform;
		if (this.m_AlphaObjectToRender)
		{
			this.m_AlphaCameraGO.transform.position = this.m_CameraGO.transform.position - this.ALPHA_OBJECT_OFFSET;
		}
		else
		{
			this.m_AlphaCameraGO.transform.position = this.m_CameraGO.transform.position;
		}
		this.m_AlphaCameraGO.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06008F4E RID: 36686 RVA: 0x002E65FC File Offset: 0x002E47FC
	private void AlphaCameraRender(RenderTexture targetTexture)
	{
		this.m_AlphaCamera.targetTexture = targetTexture;
		this.m_AlphaCamera.orthographicSize = this.OrthoSize();
		this.m_AlphaCamera.farClipPlane = this.m_FarClip * this.m_WorldScale.z;
		this.m_AlphaCamera.nearClipPlane = this.m_NearClip * this.m_WorldScale.z;
		if (this.m_PlaneGameObject && !this.m_HideRenderObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = false;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if (this.m_OpaqueObjectAlphaFill)
		{
			this.m_AlphaCamera.RenderWithShader(this.m_AlphaFillShader, "RenderType");
		}
		else if (this.m_AlphaObjectToRender == null)
		{
			string text = this.m_AlphaReplacementTag;
			if (text == string.Empty)
			{
				text = this.m_ReplacmentTag;
			}
			this.m_AlphaCamera.RenderWithShader(this.m_AlphaFillShader, text);
		}
		else
		{
			this.m_AlphaCamera.Render();
		}
		if (this.m_PlaneGameObject && !this.m_HideRenderObject)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().enabled = true;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
		this.m_AlphaCamera.targetTexture = null;
	}

	// Token: 0x06008F4F RID: 36687 RVA: 0x002E675C File Offset: 0x002E495C
	private void CreateBloomCaptureCamera()
	{
		if (this.m_BloomCaptureCamera != null)
		{
			return;
		}
		this.m_BloomCaptureCameraGO = new GameObject();
		this.m_BloomCaptureCamera = this.m_BloomCaptureCameraGO.AddComponent<Camera>();
		this.m_BloomCaptureCameraGO.name = base.name + "_R2TBloomRenderCamera";
		this.m_BloomCaptureCamera.CopyFrom(this.m_Camera);
		this.m_BloomCaptureCamera.enabled = false;
		this.m_BloomCaptureCamera.depth = this.m_Camera.depth + 1f;
		this.m_BloomCaptureCameraGO.transform.parent = this.m_Camera.transform;
		this.m_BloomCaptureCameraGO.transform.localPosition = Vector3.zero;
		this.m_BloomCaptureCameraGO.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06008F50 RID: 36688 RVA: 0x002E6830 File Offset: 0x002E4A30
	private Vector2 CalcTextureSize()
	{
		Vector2 result = new Vector2((float)this.m_Resolution, (float)this.m_Resolution);
		if (this.m_WorldWidth > this.m_WorldHeight)
		{
			result.x = (float)this.m_Resolution;
			result.y = (float)this.m_Resolution * (this.m_WorldHeight / this.m_WorldWidth);
		}
		else
		{
			result.x = (float)this.m_Resolution * (this.m_WorldWidth / this.m_WorldHeight);
			result.y = (float)this.m_Resolution;
		}
		return result;
	}

	// Token: 0x06008F51 RID: 36689 RVA: 0x002E68B8 File Offset: 0x002E4AB8
	private void CreateRenderPlane()
	{
		if (this.m_PlaneGameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject);
		}
		this.m_PlaneGameObject = this.CreateMeshPlane(string.Format("{0}_RenderPlane", base.name), this.m_Material);
		SceneUtils.SetHideFlags(this.m_PlaneGameObject, HideFlags.DontSave);
	}

	// Token: 0x06008F52 RID: 36690 RVA: 0x002E6910 File Offset: 0x002E4B10
	private void CreateBloomPlane()
	{
		if (this.m_BloomPlaneGameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_BloomPlaneGameObject);
		}
		Material material = this.AdditiveMaterial;
		if (this.m_BloomBlend == RenderToTexture.BloomBlendType.Transparent)
		{
			material = this.TransparentMaterial;
		}
		this.m_BloomPlaneGameObject = this.CreateMeshPlane(string.Format("{0}_BloomRenderPlane", base.name), material);
		this.m_BloomPlaneGameObject.transform.parent = this.m_PlaneGameObject.transform;
		this.m_BloomPlaneGameObject.transform.localPosition = new Vector3(0f, 0.15f, 0f);
		this.m_BloomPlaneGameObject.transform.localRotation = Quaternion.identity;
		this.m_BloomPlaneGameObject.transform.localScale = Vector3.one;
		this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetMaterial().color = this.m_BloomColor;
	}

	// Token: 0x06008F53 RID: 36691 RVA: 0x002E69F0 File Offset: 0x002E4BF0
	private void CreateBloomCapturePlane()
	{
		if (this.m_BloomCapturePlaneGameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_BloomCapturePlaneGameObject);
		}
		Material material = this.AdditiveMaterial;
		if (this.m_BloomBlend == RenderToTexture.BloomBlendType.Transparent)
		{
			material = this.TransparentMaterial;
		}
		this.m_BloomCapturePlaneGameObject = this.CreateMeshPlane(string.Format("{0}_BloomCaptureRenderPlane", base.name), material);
		this.m_BloomCapturePlaneGameObject.transform.parent = this.m_BloomCaptureCameraGO.transform;
		this.m_BloomCapturePlaneGameObject.transform.localPosition = Vector3.zero;
		this.m_BloomCapturePlaneGameObject.transform.localRotation = Quaternion.identity;
		this.m_BloomCapturePlaneGameObject.transform.Rotate(-90f, 0f, 0f);
		this.m_BloomCapturePlaneGameObject.transform.localScale = this.m_WorldScale;
		if (this.m_Material)
		{
			this.m_BloomCapturePlaneGameObject.GetComponent<Renderer>().SetMaterial(this.m_PlaneGameObject.GetComponent<Renderer>().GetMaterial());
		}
		if (this.m_RenderTexture)
		{
			this.m_BloomCapturePlaneGameObject.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_RenderTexture;
		}
	}

	// Token: 0x06008F54 RID: 36692 RVA: 0x002E6B1C File Offset: 0x002E4D1C
	private GameObject CreateMeshPlane(string name, Material material)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = name;
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = this.m_PositionOffset;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		float num = this.m_Width * 0.5f;
		float num2 = this.m_Height * 0.5f;
		mesh.vertices = new Vector3[]
		{
			new Vector3(-num, 0f, -num2),
			new Vector3(num, 0f, -num2),
			new Vector3(-num, 0f, num2),
			new Vector3(num, 0f, num2)
		};
		mesh.uv = this.PLANE_UVS;
		mesh.normals = this.PLANE_NORMALS;
		mesh.triangles = this.PLANE_TRIANGLES;
		(gameObject.GetComponent<MeshFilter>().mesh = mesh).RecalculateBounds();
		Renderer component = gameObject.GetComponent<Renderer>();
		if (material)
		{
			component.SetMaterial(material);
		}
		component.sortingOrder = this.m_RenderQueue;
		component.GetMaterial().renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
		this.m_previousRenderQueue = this.m_RenderQueue;
		return gameObject;
	}

	// Token: 0x06008F55 RID: 36693 RVA: 0x002E6C87 File Offset: 0x002E4E87
	public void EnablePopupRendering(PopupRoot popupRoot)
	{
		this.m_popupRoot = popupRoot;
	}

	// Token: 0x06008F56 RID: 36694 RVA: 0x002E6C90 File Offset: 0x002E4E90
	public void DisablePopupRendering()
	{
		this.m_popupRoot = null;
	}

	// Token: 0x06008F57 RID: 36695 RVA: 0x000052EC File Offset: 0x000034EC
	public bool ShouldPropagatePopupRendering()
	{
		return true;
	}

	// Token: 0x06008F58 RID: 36696 RVA: 0x002E6C9C File Offset: 0x002E4E9C
	private void Sample(RenderTexture source, RenderTexture dest, Material sampleMat, float offset)
	{
		Graphics.BlitMultiTap(source, dest, sampleMat, new Vector2[]
		{
			new Vector2(-offset, -offset),
			new Vector2(-offset, offset),
			new Vector2(offset, offset),
			new Vector2(offset, -offset)
		});
	}

	// Token: 0x06008F59 RID: 36697 RVA: 0x002E6CFC File Offset: 0x002E4EFC
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
		if (this.m_UniformWorldScale)
		{
			float num = Mathf.Max(new float[]
			{
				base.transform.lossyScale.x,
				base.transform.lossyScale.y,
				base.transform.lossyScale.z
			});
			this.m_WorldScale = new Vector3(num, num, num);
		}
		else
		{
			this.m_WorldScale = base.transform.lossyScale;
		}
		this.m_WorldWidth = this.m_Width * this.m_WorldScale.x;
		this.m_WorldHeight = this.m_Height * this.m_WorldScale.y;
		if (flag)
		{
			base.transform.parent = parent;
			base.transform.localScale = localScale;
		}
		base.transform.rotation = rotation;
		if (this.m_WorldWidth == 0f || this.m_WorldHeight == 0f)
		{
			Debug.LogError(string.Format(" \"{0}\": RenderToTexture has a world scale of zero. \nm_WorldWidth: {1},   m_WorldHeight: {2}", this.m_WorldWidth, this.m_WorldHeight));
		}
	}

	// Token: 0x06008F5A RID: 36698 RVA: 0x002E6E80 File Offset: 0x002E5080
	private void CleanUp()
	{
		this.ReleaseTexture();
		if (this.m_hasMaterialInstance)
		{
			if (!RenderToTexture.GetMaterialManagerService().CanIgnoreMaterial(this.m_Material))
			{
				UnityEngine.Object.Destroy(this.m_Material);
			}
			this.m_hasMaterialInstance = false;
		}
		if (this.m_CameraGO)
		{
			UnityEngine.Object.Destroy(this.m_CameraGO);
		}
		if (this.m_AlphaCameraGO)
		{
			UnityEngine.Object.Destroy(this.m_AlphaCameraGO);
		}
		if (this.m_PlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_PlaneGameObject);
		}
		if (this.m_BloomPlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomPlaneGameObject);
		}
		if (this.m_BloomCaptureCameraGO)
		{
			UnityEngine.Object.Destroy(this.m_BloomCaptureCameraGO);
		}
		if (this.m_BloomCapturePlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomCapturePlaneGameObject);
		}
		if (this.m_OffscreenGameObject)
		{
			UnityEngine.Object.Destroy(this.m_OffscreenGameObject);
		}
		if (this.m_ObjectToRender != null)
		{
			if (this.m_ObjectToRenderOrgParent != null)
			{
				this.m_ObjectToRender.transform.parent = this.m_ObjectToRenderOrgParent;
			}
			this.m_ObjectToRender.transform.localPosition = this.m_ObjectToRenderOrgPosition;
		}
		this.m_init = false;
		this.m_isDirty = true;
	}

	// Token: 0x06008F5B RID: 36699 RVA: 0x002E6FBD File Offset: 0x002E51BD
	private static MaterialManagerService GetMaterialManagerService()
	{
		if (RenderToTexture.s_MaterialManagerService == null)
		{
			RenderToTexture.s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return RenderToTexture.s_MaterialManagerService;
	}

	// Token: 0x040077A6 RID: 30630
	private const string BLUR_SHADER_NAME = "Hidden/R2TBlur";

	// Token: 0x040077A7 RID: 30631
	private const string BLUR_ALPHA_SHADER_NAME = "Hidden/R2TAlphaBlur";

	// Token: 0x040077A8 RID: 30632
	private const string ALPHA_BLEND_SHADER_NAME = "Hidden/R2TColorAlphaCombine";

	// Token: 0x040077A9 RID: 30633
	private const string ALPHA_BLEND_ADD_SHADER_NAME = "Hidden/R2TColorAlphaCombineAdd";

	// Token: 0x040077AA RID: 30634
	private const string ALPHA_FILL_SHADER_NAME = "Custom/AlphaFillOpaque";

	// Token: 0x040077AB RID: 30635
	private const string BLOOM_SHADER_NAME = "Hidden/R2TBloom";

	// Token: 0x040077AC RID: 30636
	private const string BLOOM_ALPHA_SHADER_NAME = "Hidden/R2TBloomAlpha";

	// Token: 0x040077AD RID: 30637
	private const string ADDITIVE_SHADER_NAME = "Hidden/R2TAdditive";

	// Token: 0x040077AE RID: 30638
	private const string TRANSPARENT_SHADER_NAME = "Hidden/R2TTransparent";

	// Token: 0x040077AF RID: 30639
	private const string ALPHA_CLIP_SHADER_NAME = "Hidden/R2TAlphaClip";

	// Token: 0x040077B0 RID: 30640
	private const string ALPHA_CLIP_BLOOM_SHADER_NAME = "Hidden/R2TAlphaClipBloom";

	// Token: 0x040077B1 RID: 30641
	private const string ALPHA_CLIP_GRADIENT_SHADER_NAME = "Hidden/R2TAlphaClipGradient";

	// Token: 0x040077B2 RID: 30642
	private const RenderTextureFormat ALPHA_TEXTURE_FORMAT = RenderTextureFormat.R8;

	// Token: 0x040077B3 RID: 30643
	private const float OFFSET_DISTANCE = 300f;

	// Token: 0x040077B4 RID: 30644
	private const float MIN_OFFSET_DISTANCE = -4000f;

	// Token: 0x040077B5 RID: 30645
	private const float MAX_OFFSET_DISTANCE = -90000f;

	// Token: 0x040077B6 RID: 30646
	private readonly Vector3 ALPHA_OBJECT_OFFSET = new Vector3(0f, 1000f, 0f);

	// Token: 0x040077B7 RID: 30647
	private const float RENDER_SIZE_QUALITY_LOW = 0.75f;

	// Token: 0x040077B8 RID: 30648
	private const float RENDER_SIZE_QUALITY_MEDIUM = 1f;

	// Token: 0x040077B9 RID: 30649
	private const float RENDER_SIZE_QUALITY_HIGH = 1.5f;

	// Token: 0x040077BA RID: 30650
	private readonly Vector2[] PLANE_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x040077BB RID: 30651
	private readonly Vector3[] PLANE_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x040077BC RID: 30652
	private readonly int[] PLANE_TRIANGLES = new int[]
	{
		3,
		1,
		2,
		2,
		1,
		0
	};

	// Token: 0x040077BD RID: 30653
	public GameObject m_ObjectToRender;

	// Token: 0x040077BE RID: 30654
	public GameObject m_AlphaObjectToRender;

	// Token: 0x040077BF RID: 30655
	public bool m_HideRenderObject = true;

	// Token: 0x040077C0 RID: 30656
	public bool m_RealtimeRender;

	// Token: 0x040077C1 RID: 30657
	public bool m_RealtimeTranslation;

	// Token: 0x040077C2 RID: 30658
	public bool m_RenderMeshAsAlpha;

	// Token: 0x040077C3 RID: 30659
	public bool m_OpaqueObjectAlphaFill;

	// Token: 0x040077C4 RID: 30660
	public RenderToTexture.RenderToTextureMaterial m_RenderMaterial;

	// Token: 0x040077C5 RID: 30661
	public Material m_Material;

	// Token: 0x040077C6 RID: 30662
	public bool m_CreateRenderPlane = true;

	// Token: 0x040077C7 RID: 30663
	public GameObject m_RenderToObject;

	// Token: 0x040077C8 RID: 30664
	public string m_ShaderTextureName = string.Empty;

	// Token: 0x040077C9 RID: 30665
	public int m_Resolution = 128;

	// Token: 0x040077CA RID: 30666
	public float m_Width = 1f;

	// Token: 0x040077CB RID: 30667
	public float m_Height = 1f;

	// Token: 0x040077CC RID: 30668
	public float m_NearClip = -0.1f;

	// Token: 0x040077CD RID: 30669
	public float m_FarClip = 0.5f;

	// Token: 0x040077CE RID: 30670
	public float m_BloomIntensity;

	// Token: 0x040077CF RID: 30671
	public float m_BloomThreshold = 0.7f;

	// Token: 0x040077D0 RID: 30672
	public float m_BloomBlur = 0.3f;

	// Token: 0x040077D1 RID: 30673
	public float m_BloomResolutionRatio = 0.5f;

	// Token: 0x040077D2 RID: 30674
	public RenderToTexture.BloomRenderType m_BloomRenderType;

	// Token: 0x040077D3 RID: 30675
	public float m_BloomAlphaIntensity = 1f;

	// Token: 0x040077D4 RID: 30676
	public RenderToTexture.BloomBlendType m_BloomBlend;

	// Token: 0x040077D5 RID: 30677
	public Color m_BloomColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x040077D6 RID: 30678
	public RenderToTexture.AlphaClipShader m_AlphaClipRenderStyle;

	// Token: 0x040077D7 RID: 30679
	public float m_AlphaClip = 15f;

	// Token: 0x040077D8 RID: 30680
	public float m_AlphaClipIntensity = 1.5f;

	// Token: 0x040077D9 RID: 30681
	public float m_AlphaClipAlphaIntensity = 1f;

	// Token: 0x040077DA RID: 30682
	public Texture2D m_AlphaClipGradientMap;

	// Token: 0x040077DB RID: 30683
	public float m_BlurAmount;

	// Token: 0x040077DC RID: 30684
	public bool m_BlurAlphaOnly;

	// Token: 0x040077DD RID: 30685
	public Color m_TintColor = Color.white;

	// Token: 0x040077DE RID: 30686
	public int m_RenderQueueOffset = 3000;

	// Token: 0x040077DF RID: 30687
	public int m_RenderQueue;

	// Token: 0x040077E0 RID: 30688
	public Color m_ClearColor = Color.clear;

	// Token: 0x040077E1 RID: 30689
	public Shader m_ReplacmentShader;

	// Token: 0x040077E2 RID: 30690
	public string m_ReplacmentTag;

	// Token: 0x040077E3 RID: 30691
	public string m_AlphaReplacementTag;

	// Token: 0x040077E4 RID: 30692
	public RenderTextureFormat m_RenderTextureFormat = RenderTextureFormat.Default;

	// Token: 0x040077E5 RID: 30693
	public Vector3 m_PositionOffset = Vector3.zero;

	// Token: 0x040077E6 RID: 30694
	public Vector3 m_CameraOffset = Vector3.zero;

	// Token: 0x040077E7 RID: 30695
	public LayerMask m_LayerMask = -1;

	// Token: 0x040077E8 RID: 30696
	public bool m_UniformWorldScale;

	// Token: 0x040077E9 RID: 30697
	public float m_OverrideCameraSize;

	// Token: 0x040077EA RID: 30698
	public bool m_LateUpdate;

	// Token: 0x040077EB RID: 30699
	public bool m_RenderOnStart = true;

	// Token: 0x040077EC RID: 30700
	public bool m_RenderOnEnable = true;

	// Token: 0x040077ED RID: 30701
	private bool m_renderEnabled = true;

	// Token: 0x040077EE RID: 30702
	private bool m_init;

	// Token: 0x040077EF RID: 30703
	private float m_WorldWidth;

	// Token: 0x040077F0 RID: 30704
	private float m_WorldHeight;

	// Token: 0x040077F1 RID: 30705
	private Vector3 m_WorldScale;

	// Token: 0x040077F2 RID: 30706
	private GameObject m_OffscreenGameObject;

	// Token: 0x040077F3 RID: 30707
	private GameObject m_CameraGO;

	// Token: 0x040077F4 RID: 30708
	private Camera m_Camera;

	// Token: 0x040077F5 RID: 30709
	private GameObject m_AlphaCameraGO;

	// Token: 0x040077F6 RID: 30710
	private Camera m_AlphaCamera;

	// Token: 0x040077F7 RID: 30711
	private GameObject m_BloomCaptureCameraGO;

	// Token: 0x040077F8 RID: 30712
	private Camera m_BloomCaptureCamera;

	// Token: 0x040077F9 RID: 30713
	private RenderTexture m_RenderTexture;

	// Token: 0x040077FA RID: 30714
	private RenderTexture m_BloomRenderTexture;

	// Token: 0x040077FB RID: 30715
	private RenderTexture m_BloomRenderBuffer1;

	// Token: 0x040077FC RID: 30716
	private RenderTexture m_BloomRenderBuffer2;

	// Token: 0x040077FD RID: 30717
	private GameObject m_PlaneGameObject;

	// Token: 0x040077FE RID: 30718
	private GameObject m_BloomPlaneGameObject;

	// Token: 0x040077FF RID: 30719
	private GameObject m_BloomCapturePlaneGameObject;

	// Token: 0x04007800 RID: 30720
	private bool m_ObjectToRenderOrgPositionStored;

	// Token: 0x04007801 RID: 30721
	private Transform m_ObjectToRenderOrgParent;

	// Token: 0x04007802 RID: 30722
	private Vector3 m_ObjectToRenderOrgPosition = Vector3.zero;

	// Token: 0x04007803 RID: 30723
	private Vector3 m_OriginalRenderPosition = Vector3.zero;

	// Token: 0x04007804 RID: 30724
	private bool m_isDirty;

	// Token: 0x04007805 RID: 30725
	private Shader m_AlphaFillShader;

	// Token: 0x04007806 RID: 30726
	private Vector3 m_OffscreenPos;

	// Token: 0x04007807 RID: 30727
	private Vector3 m_ObjectToRenderOffset = Vector3.zero;

	// Token: 0x04007808 RID: 30728
	private Vector3 m_AlphaObjectToRenderOffset = Vector3.zero;

	// Token: 0x04007809 RID: 30729
	private RenderToTexture.RenderToTextureMaterial m_PreviousRenderMaterial;

	// Token: 0x0400780A RID: 30730
	private int m_previousRenderQueue;

	// Token: 0x0400780B RID: 30731
	private List<Renderer> m_OpaqueObjectAlphaFillTransparent;

	// Token: 0x0400780C RID: 30732
	private List<UberText> m_OpaqueObjectAlphaFillUberText;

	// Token: 0x0400780D RID: 30733
	private bool m_hasMaterialInstance;

	// Token: 0x0400780E RID: 30734
	private static MaterialManagerService s_MaterialManagerService;

	// Token: 0x0400780F RID: 30735
	private Vector3 m_Offset = Vector3.zero;

	// Token: 0x04007810 RID: 30736
	private static Vector3 s_offset = new Vector3(-4000f, -4000f, -4000f);

	// Token: 0x04007811 RID: 30737
	private Shader m_AlphaBlendShader;

	// Token: 0x04007812 RID: 30738
	private Material m_AlphaBlendMaterial;

	// Token: 0x04007813 RID: 30739
	private Shader m_AlphaBlendAddShader;

	// Token: 0x04007814 RID: 30740
	private Material m_AlphaBlendAddMaterial;

	// Token: 0x04007815 RID: 30741
	private Shader m_AdditiveShader;

	// Token: 0x04007816 RID: 30742
	private Material m_AdditiveMaterial;

	// Token: 0x04007817 RID: 30743
	private Shader m_BloomShader;

	// Token: 0x04007818 RID: 30744
	private Material m_BloomMaterial;

	// Token: 0x04007819 RID: 30745
	private Shader m_BloomShaderAlpha;

	// Token: 0x0400781A RID: 30746
	private Material m_BloomMaterialAlpha;

	// Token: 0x0400781B RID: 30747
	private Shader m_BlurShader;

	// Token: 0x0400781C RID: 30748
	private Material m_BlurMaterial;

	// Token: 0x0400781D RID: 30749
	private Shader m_AlphaBlurShader;

	// Token: 0x0400781E RID: 30750
	private Material m_AlphaBlurMaterial;

	// Token: 0x0400781F RID: 30751
	private Shader m_TransparentShader;

	// Token: 0x04007820 RID: 30752
	private Material m_TransparentMaterial;

	// Token: 0x04007821 RID: 30753
	private Shader m_AlphaClipShader;

	// Token: 0x04007822 RID: 30754
	private Material m_AlphaClipMaterial;

	// Token: 0x04007823 RID: 30755
	private Shader m_AlphaClipBloomShader;

	// Token: 0x04007824 RID: 30756
	private Material m_AlphaClipBloomMaterial;

	// Token: 0x04007825 RID: 30757
	private Shader m_AlphaClipGradientShader;

	// Token: 0x04007826 RID: 30758
	private Material m_AlphaClipGradientMaterial;

	// Token: 0x04007828 RID: 30760
	private PopupRoot m_popupRoot;

	// Token: 0x020026C3 RID: 9923
	public enum RenderToTextureMaterial
	{
		// Token: 0x0400F1F4 RID: 61940
		Custom,
		// Token: 0x0400F1F5 RID: 61941
		Transparent,
		// Token: 0x0400F1F6 RID: 61942
		Additive,
		// Token: 0x0400F1F7 RID: 61943
		Bloom,
		// Token: 0x0400F1F8 RID: 61944
		AlphaClip,
		// Token: 0x0400F1F9 RID: 61945
		AlphaClipBloom
	}

	// Token: 0x020026C4 RID: 9924
	public enum AlphaClipShader
	{
		// Token: 0x0400F1FB RID: 61947
		Standard,
		// Token: 0x0400F1FC RID: 61948
		ColorGradient
	}

	// Token: 0x020026C5 RID: 9925
	public enum BloomRenderType
	{
		// Token: 0x0400F1FE RID: 61950
		Color,
		// Token: 0x0400F1FF RID: 61951
		Alpha
	}

	// Token: 0x020026C6 RID: 9926
	public enum BloomBlendType
	{
		// Token: 0x0400F201 RID: 61953
		Additive,
		// Token: 0x0400F202 RID: 61954
		Transparent
	}
}
