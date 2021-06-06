using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000A78 RID: 2680
[ExecuteInEditMode]
public class RenderToTexture2 : MonoBehaviour
{
	// Token: 0x17000821 RID: 2081
	// (get) Token: 0x06008FDB RID: 36827 RVA: 0x002E9B44 File Offset: 0x002E7D44
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

	// Token: 0x17000822 RID: 2082
	// (get) Token: 0x06008FDC RID: 36828 RVA: 0x002E9BB8 File Offset: 0x002E7DB8
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

	// Token: 0x17000823 RID: 2083
	// (get) Token: 0x06008FDD RID: 36829 RVA: 0x002E9C2C File Offset: 0x002E7E2C
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

	// Token: 0x17000824 RID: 2084
	// (get) Token: 0x06008FDE RID: 36830 RVA: 0x002E9CA0 File Offset: 0x002E7EA0
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

	// Token: 0x17000825 RID: 2085
	// (get) Token: 0x06008FDF RID: 36831 RVA: 0x002E9D14 File Offset: 0x002E7F14
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

	// Token: 0x17000826 RID: 2086
	// (get) Token: 0x06008FE0 RID: 36832 RVA: 0x002E9D88 File Offset: 0x002E7F88
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

	// Token: 0x17000827 RID: 2087
	// (get) Token: 0x06008FE1 RID: 36833 RVA: 0x002E9DFC File Offset: 0x002E7FFC
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

	// Token: 0x17000828 RID: 2088
	// (get) Token: 0x06008FE2 RID: 36834 RVA: 0x002E9E70 File Offset: 0x002E8070
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

	// Token: 0x17000829 RID: 2089
	// (get) Token: 0x06008FE3 RID: 36835 RVA: 0x002E9EE4 File Offset: 0x002E80E4
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

	// Token: 0x1700082A RID: 2090
	// (get) Token: 0x06008FE4 RID: 36836 RVA: 0x002E9F58 File Offset: 0x002E8158
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

	// Token: 0x1700082B RID: 2091
	// (get) Token: 0x06008FE5 RID: 36837 RVA: 0x002E9FCC File Offset: 0x002E81CC
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

	// Token: 0x06008FE6 RID: 36838 RVA: 0x002EA040 File Offset: 0x002E8240
	private void Awake()
	{
		this.m_AlphaFillShader = ShaderUtils.FindShader("Custom/AlphaFillOpaque");
		if (!this.m_AlphaFillShader)
		{
			Debug.LogError("Failed to load RenderToTexture Shader: Custom/AlphaFillOpaque");
		}
		if (this.m_Material != null)
		{
			this.m_Material = UnityEngine.Object.Instantiate<Material>(this.m_Material);
		}
	}

	// Token: 0x06008FE7 RID: 36839 RVA: 0x002EA093 File Offset: 0x002E8293
	private void Start()
	{
		if (this.m_RenderOnStart)
		{
			this.m_isDirty = true;
		}
		this.Init();
	}

	// Token: 0x06008FE8 RID: 36840 RVA: 0x002EA0AC File Offset: 0x002E82AC
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
			this.m_RenderTexture.name = "Rendered Texture";
			this.RenderTex();
			return;
		}
		if (this.m_LateUpdate)
		{
			return;
		}
		if (this.m_RealtimeRender || this.m_isDirty)
		{
			this.RenderTex();
		}
	}

	// Token: 0x06008FE9 RID: 36841 RVA: 0x002EA130 File Offset: 0x002E8330
	private void LateUpdate()
	{
		if (!this.m_renderEnabled)
		{
			return;
		}
		if (this.m_LateUpdate)
		{
			if (this.m_RealtimeRender || this.m_isDirty)
			{
				this.RenderTex();
				return;
			}
		}
		else
		{
			if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom || this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.Bloom)
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

	// Token: 0x06008FEA RID: 36842 RVA: 0x002EA196 File Offset: 0x002E8396
	private void OnApplicationFocus(bool state)
	{
		if (this.m_RenderTexture && state)
		{
			this.m_isDirty = true;
			this.RenderTex();
		}
	}

	// Token: 0x06008FEB RID: 36843 RVA: 0x002EA1B4 File Offset: 0x002E83B4
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

	// Token: 0x06008FEC RID: 36844 RVA: 0x002EA318 File Offset: 0x002E8518
	private void OnDisable()
	{
		this.CleanUp();
		if (this.m_ObjectToRender)
		{
			this.m_ObjectToRender.transform.localPosition = this.m_ObjectToRenderOrgPosition;
		}
		if (this.m_CreateRenderPlane)
		{
			UnityEngine.Object.Destroy(this.m_GameObject);
		}
		if (this.m_BloomPlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomPlaneGameObject);
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

	// Token: 0x06008FED RID: 36845 RVA: 0x002EA3D7 File Offset: 0x002E85D7
	private void OnDestroy()
	{
		this.CleanUp();
	}

	// Token: 0x06008FEE RID: 36846 RVA: 0x002EA3DF File Offset: 0x002E85DF
	private void OnEnable()
	{
		if (this.m_RenderOnEnable)
		{
			this.RenderTex();
		}
	}

	// Token: 0x06008FEF RID: 36847 RVA: 0x002EA3EF File Offset: 0x002E85EF
	public RenderTexture Render()
	{
		this.m_isDirty = true;
		return this.m_RenderTexture;
	}

	// Token: 0x06008FF0 RID: 36848 RVA: 0x002EA3FE File Offset: 0x002E85FE
	public RenderTexture RenderNow()
	{
		this.RenderTex();
		return this.m_RenderTexture;
	}

	// Token: 0x06008FF1 RID: 36849 RVA: 0x002EA40C File Offset: 0x002E860C
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

	// Token: 0x06008FF2 RID: 36850 RVA: 0x002EA42A File Offset: 0x002E862A
	public void Show()
	{
		this.Show(false);
	}

	// Token: 0x06008FF3 RID: 36851 RVA: 0x002EA434 File Offset: 0x002E8634
	public void Show(bool render)
	{
		this.m_renderEnabled = true;
		this.m_GameObject.GetComponent<Renderer>().enabled = true;
		if (this.m_BloomPlaneGameObject)
		{
			this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
		}
		if (render)
		{
			this.Render();
		}
	}

	// Token: 0x06008FF4 RID: 36852 RVA: 0x002EA481 File Offset: 0x002E8681
	public void Hide()
	{
		this.m_renderEnabled = false;
		this.m_GameObject.GetComponent<Renderer>().enabled = false;
		if (this.m_BloomPlaneGameObject)
		{
			this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x06008FF5 RID: 36853 RVA: 0x002EA4B9 File Offset: 0x002E86B9
	public void SetDirty()
	{
		this.m_init = false;
		this.m_isDirty = true;
	}

	// Token: 0x06008FF6 RID: 36854 RVA: 0x002EA4C9 File Offset: 0x002E86C9
	public Material GetRenderMaterial()
	{
		if (this.m_GameObject)
		{
			return this.m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
		}
		return this.m_Material;
	}

	// Token: 0x06008FF7 RID: 36855 RVA: 0x002EA4EF File Offset: 0x002E86EF
	public GameObject GetRenderToObject()
	{
		return this.m_GameObject;
	}

	// Token: 0x06008FF8 RID: 36856 RVA: 0x002EA4F7 File Offset: 0x002E86F7
	public RenderTexture GetRenderTexture()
	{
		return this.m_RenderTexture;
	}

	// Token: 0x06008FF9 RID: 36857 RVA: 0x002EA500 File Offset: 0x002E8700
	private void Init()
	{
		if (this.m_init)
		{
			return;
		}
		if (this.m_ObjectToRender && this.m_AlphaObjectToRender)
		{
			this.m_AlphaObjectToRender.transform.parent = this.m_ObjectToRender.transform;
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
			this.m_RenderToObject.GetComponent<Renderer>().GetSharedMaterial().renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
			this.m_GameObject = this.m_RenderToObject;
		}
		this.SetupMaterial();
		this.m_init = true;
	}

	// Token: 0x06008FFA RID: 36858 RVA: 0x002EA600 File Offset: 0x002E8800
	private void RenderTex()
	{
		if (!this.m_renderEnabled)
		{
			return;
		}
		if (this.m_RenderTexture)
		{
			RenderTexture.active = this.m_RenderTexture;
			GL.Clear(true, false, Color.white);
			RenderTexture.active = null;
		}
		if (this.m_AlphaRenderTexture)
		{
			RenderTexture.active = this.m_AlphaRenderTexture;
			GL.Clear(true, false, new Color(0f, 0f, 0f, 0f));
			RenderTexture.active = null;
		}
		if (this.m_BloomRenderTexture)
		{
			RenderTexture.active = this.m_BloomRenderTexture;
			GL.Clear(true, false, new Color(0f, 0f, 0f, 0f));
			RenderTexture.active = null;
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
		if (this.m_RenderMaterial != this.m_PreviousRenderMaterial)
		{
			this.SetupMaterial();
		}
		if (this.m_ObjectToRender)
		{
			this.PositionObjectsAndCameras();
		}
		if (this.m_OpaqueObjectAlphaFill || this.m_RenderMeshAsAlpha || this.m_AlphaObjectToRender != null)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, this.m_RenderTexture.format);
			this.m_Camera.targetTexture = temporary;
			this.CameraRender();
			RenderTexture temporary2 = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, RenderTextureFormat.ARGB32);
			this.m_AlphaCamera.targetTexture = temporary2;
			this.AlphaCameraRender();
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
		Renderer renderer = this.m_GameObject.GetComponent<Renderer>();
		if (renderer == null)
		{
			renderer = this.m_GameObject.GetComponentInChildren<Renderer>();
		}
		Material sharedMaterial = renderer.GetSharedMaterial();
		if (this.m_ShaderTextureName != string.Empty)
		{
			sharedMaterial.SetTexture(this.m_ShaderTextureName, this.m_RenderTexture);
		}
		else if (sharedMaterial.HasProperty("_MainTex"))
		{
			sharedMaterial.SetTexture("_MainTex", this.m_RenderTexture);
		}
		else
		{
			sharedMaterial.mainTexture = this.m_RenderTexture;
		}
		if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClip || this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom)
		{
			Material sharedMaterial2 = this.m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.SetFloat("_Cutoff", this.m_AlphaClip);
			sharedMaterial2.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			sharedMaterial2.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture2.AlphaClipShader.ColorGradient)
			{
				sharedMaterial2.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
		}
		this.m_isDirty = false;
	}

	// Token: 0x06008FFB RID: 36859 RVA: 0x002EAA74 File Offset: 0x002E8C74
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
		if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom)
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
		if (this.m_BloomRenderTexture)
		{
			SceneUtils.SetHideFlags(this.m_BloomRenderTexture, this.m_DevFlag);
		}
		SceneUtils.SetHideFlags(this.m_BloomRenderBuffer1, this.m_DevFlag);
		SceneUtils.SetHideFlags(this.m_BloomRenderBuffer2, this.m_DevFlag);
		Material material = this.BloomMaterial;
		if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom)
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
		if (this.m_BloomRenderType == RenderToTexture2.BloomRenderType.Alpha)
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
		Material sharedMaterial = this.m_GameObject.GetComponent<Renderer>().GetSharedMaterial();
		if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom)
		{
			Material sharedMaterial2 = this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.color = this.m_BloomColor;
			sharedMaterial2.mainTexture = renderTexture;
			if (this.m_CreateRenderPlane)
			{
				sharedMaterial2.renderQueue = sharedMaterial.renderQueue + 1;
				return;
			}
		}
		else
		{
			sharedMaterial.color = this.m_BloomColor;
			sharedMaterial.mainTexture = renderTexture;
		}
	}

	// Token: 0x06008FFC RID: 36860 RVA: 0x002EAE98 File Offset: 0x002E9098
	private void SetupForRender()
	{
		this.CalcWorldWidthHeightScale();
		if (!this.m_RenderTexture)
		{
			this.CreateTexture();
		}
		if (this.m_CreateRenderPlane)
		{
			this.m_GameObject.layer = base.gameObject.layer;
		}
		if (this.m_Camera != null)
		{
			this.m_Camera.backgroundColor = this.m_ClearColor;
		}
	}

	// Token: 0x06008FFD RID: 36861 RVA: 0x002EAEFC File Offset: 0x002E90FC
	private void PositionObjectsAndCameras()
	{
		if (!this.m_CreateRenderPlane)
		{
			this.m_ObjectToRender.transform.rotation = Quaternion.identity;
			this.m_ObjectToRender.transform.rotation = base.transform.rotation;
		}
		if (this.m_AlphaObjectToRender)
		{
			this.m_AlphaObjectToRender.transform.rotation = base.transform.rotation;
		}
		if (this.m_CameraGO == null)
		{
			return;
		}
		this.m_CameraGO.transform.rotation = Quaternion.identity;
		if (!this.m_CreateRenderPlane)
		{
			this.m_CameraGO.transform.position = this.m_ObjectToRender.transform.position + this.m_CameraOffset;
		}
		this.m_CameraGO.transform.rotation = this.m_ObjectToRender.transform.rotation;
		this.m_CameraGO.transform.Rotate(90f, 0f, 0f);
	}

	// Token: 0x06008FFE RID: 36862 RVA: 0x002EB000 File Offset: 0x002E9200
	private void SetupMaterial()
	{
		if (this.m_RenderMaterial == RenderToTexture2.RenderToTextureMaterial.Custom)
		{
			return;
		}
		if (this.m_GameObject == null)
		{
			return;
		}
		Renderer component = this.m_GameObject.GetComponent<Renderer>();
		switch (this.m_RenderMaterial)
		{
		case RenderToTexture2.RenderToTextureMaterial.Transparent:
			component.SetSharedMaterial(this.TransparentMaterial);
			break;
		case RenderToTexture2.RenderToTextureMaterial.Additive:
			component.SetSharedMaterial(this.AdditiveMaterial);
			break;
		case RenderToTexture2.RenderToTextureMaterial.Bloom:
			if (this.m_BloomBlend == RenderToTexture2.BloomBlendType.Additive)
			{
				component.SetSharedMaterial(this.AdditiveMaterial);
			}
			else if (this.m_BloomBlend == RenderToTexture2.BloomBlendType.Transparent)
			{
				component.SetSharedMaterial(this.TransparentMaterial);
			}
			break;
		case RenderToTexture2.RenderToTextureMaterial.AlphaClip:
		{
			Material material;
			if (this.m_AlphaClipRenderStyle == RenderToTexture2.AlphaClipShader.Standard)
			{
				material = this.AlphaClipMaterial;
			}
			else
			{
				material = this.AlphaClipGradientMaterial;
			}
			component.SetSharedMaterial(material);
			material.SetFloat("_Cutoff", this.m_AlphaClip);
			material.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture2.AlphaClipShader.ColorGradient)
			{
				material.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
			break;
		}
		case RenderToTexture2.RenderToTextureMaterial.AlphaClipBloom:
		{
			Material material2;
			if (this.m_AlphaClipRenderStyle == RenderToTexture2.AlphaClipShader.Standard)
			{
				material2 = this.AlphaClipMaterial;
			}
			else
			{
				material2 = this.AlphaClipGradientMaterial;
			}
			component.SetSharedMaterial(material2);
			material2.SetFloat("_Cutoff", this.m_AlphaClip);
			material2.SetFloat("_Intensity", this.m_AlphaClipIntensity);
			material2.SetFloat("_AlphaIntensity", this.m_AlphaClipAlphaIntensity);
			if (this.m_AlphaClipRenderStyle == RenderToTexture2.AlphaClipShader.ColorGradient)
			{
				material2.SetTexture("_GradientTex", this.m_AlphaClipGradientMap);
			}
			break;
		}
		default:
			if (this.m_Material != null)
			{
				component.SetSharedMaterial(this.m_Material);
			}
			break;
		}
		Material sharedMaterial = component.GetSharedMaterial();
		sharedMaterial.color *= this.m_TintColor;
		if (this.m_BloomIntensity > 0f && this.m_BloomPlaneGameObject)
		{
			this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().color = this.m_BloomColor;
		}
		sharedMaterial.renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
		if (this.m_BloomPlaneGameObject)
		{
			this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue + 1;
		}
		this.m_PreviousRenderMaterial = this.m_RenderMaterial;
	}

	// Token: 0x06008FFF RID: 36863 RVA: 0x002EB248 File Offset: 0x002E9448
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
		SceneUtils.SetHideFlags(this.m_RenderTexture, this.m_DevFlag);
		this.m_RenderTexture.Create();
		if (this.m_RenderMeshAsAlpha)
		{
			this.m_AlphaRenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, this.m_RenderTextureFormat, RenderTextureReadWrite.Default);
			SceneUtils.SetHideFlags(this.m_AlphaRenderTexture, this.m_DevFlag);
			this.m_AlphaRenderTexture.Create();
		}
		if (this.m_Camera)
		{
			this.m_Camera.targetTexture = this.m_RenderTexture;
		}
		if (this.m_AlphaCamera)
		{
			this.m_AlphaCamera.targetTexture = this.m_AlphaRenderTexture;
		}
	}

	// Token: 0x06009000 RID: 36864 RVA: 0x002EB384 File Offset: 0x002E9584
	private void ReleaseTexture()
	{
		if (RenderTexture.active == this.m_RenderTexture)
		{
			RenderTexture.active = null;
		}
		if (RenderTexture.active == this.m_AlphaRenderTexture)
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
		if (this.m_AlphaRenderTexture != null)
		{
			if (this.m_AlphaCamera)
			{
				this.m_AlphaCamera.targetTexture = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_AlphaRenderTexture);
			this.m_AlphaRenderTexture = null;
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
		}
		this.m_BloomRenderBuffer2 = null;
	}

	// Token: 0x06009001 RID: 36865 RVA: 0x002EB4C4 File Offset: 0x002E96C4
	private void CreateCamera()
	{
		if (this.m_Camera != null)
		{
			return;
		}
		this.m_CameraGO = new GameObject();
		this.m_Camera = this.m_CameraGO.AddComponent<Camera>();
		this.m_CameraGO.name = base.name + "_R2TRenderCamera";
		this.m_Camera.cullingMask = this.m_LayerMask;
		SceneUtils.SetHideFlags(this.m_CameraGO, this.m_DevFlag);
		this.m_Camera.orthographic = true;
		this.m_CameraGO.transform.parent = base.transform;
		this.m_CameraGO.transform.position = base.transform.position + this.m_PositionOffset + this.m_CameraOffset;
		this.m_CameraGO.transform.rotation = base.transform.rotation;
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
		this.m_Camera.clearFlags = this.m_ClearFlags;
		this.m_Camera.backgroundColor = this.m_ClearColor;
		this.m_Camera.depthTextureMode = DepthTextureMode.None;
		this.m_Camera.renderingPath = RenderingPath.Forward;
		this.m_Camera.allowHDR = false;
		this.m_Camera.targetTexture = this.m_RenderTexture;
		this.m_Camera.enabled = false;
	}

	// Token: 0x06009002 RID: 36866 RVA: 0x002EB6C0 File Offset: 0x002E98C0
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

	// Token: 0x06009003 RID: 36867 RVA: 0x002EB724 File Offset: 0x002E9924
	private void CameraRender()
	{
		this.m_Camera.orthographicSize = this.OrthoSize();
		this.m_Camera.farClipPlane = this.m_FarClip * this.m_WorldScale.z;
		this.m_Camera.nearClipPlane = this.m_NearClip * this.m_WorldScale.z;
		if (this.m_CreateRenderPlane)
		{
			this.m_GameObject.GetComponent<Renderer>().enabled = false;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = false;
			}
		}
		if (this.m_ReplacementShader)
		{
			this.m_Camera.RenderWithShader(this.m_ReplacementShader, this.m_ReplacementTag);
		}
		else
		{
			this.m_Camera.Render();
		}
		if (this.m_CreateRenderPlane)
		{
			this.m_GameObject.GetComponent<Renderer>().enabled = true;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}

	// Token: 0x06009004 RID: 36868 RVA: 0x002EB81C File Offset: 0x002E9A1C
	private void CreateAlphaCamera()
	{
		if (this.m_AlphaCamera != null)
		{
			return;
		}
		this.m_AlphaCameraGO = new GameObject();
		this.m_AlphaCamera = this.m_AlphaCameraGO.AddComponent<Camera>();
		this.m_AlphaCameraGO.name = base.name + "_R2TAlphaRenderCamera";
		SceneUtils.SetHideFlags(this.m_AlphaCameraGO, this.m_DevFlag);
		this.m_AlphaCamera.CopyFrom(this.m_Camera);
		this.m_AlphaCamera.enabled = false;
		this.m_AlphaCamera.cullingMask = this.m_LayerMask;
		this.m_AlphaCamera.backgroundColor = Color.clear;
		this.m_AlphaCamera.allowHDR = false;
		this.m_AlphaCameraGO.transform.parent = this.m_CameraGO.transform;
		this.m_AlphaCameraGO.transform.position = this.m_CameraGO.transform.position;
		this.m_AlphaCameraGO.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06009005 RID: 36869 RVA: 0x002EB920 File Offset: 0x002E9B20
	private void AlphaCameraRender()
	{
		this.m_AlphaCamera.orthographicSize = this.OrthoSize();
		this.m_AlphaCamera.farClipPlane = this.m_FarClip * this.m_WorldScale.z;
		this.m_AlphaCamera.nearClipPlane = this.m_NearClip * this.m_WorldScale.z;
		if (this.m_CreateRenderPlane)
		{
			this.m_GameObject.GetComponent<Renderer>().enabled = false;
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
				text = this.m_ReplacementTag;
			}
			this.m_AlphaCamera.RenderWithShader(this.m_AlphaFillShader, text);
		}
		else
		{
			this.m_AlphaCamera.Render();
		}
		if (this.m_CreateRenderPlane)
		{
			this.m_GameObject.GetComponent<Renderer>().enabled = true;
			if (this.m_BloomPlaneGameObject)
			{
				this.m_BloomPlaneGameObject.GetComponent<Renderer>().enabled = true;
			}
		}
	}

	// Token: 0x06009006 RID: 36870 RVA: 0x002EBA50 File Offset: 0x002E9C50
	private void CreateBloomCaptureCamera()
	{
		if (this.m_BloomCaptureCamera != null)
		{
			return;
		}
		this.m_BloomCaptureCameraGO = new GameObject();
		this.m_BloomCaptureCamera = this.m_BloomCaptureCameraGO.AddComponent<Camera>();
		this.m_BloomCaptureCameraGO.name = base.name + "_R2TBloomRenderCamera";
		SceneUtils.SetHideFlags(this.m_BloomCaptureCameraGO, this.m_DevFlag);
		this.m_BloomCaptureCamera.CopyFrom(this.m_Camera);
		this.m_BloomCaptureCamera.enabled = false;
		this.m_BloomCaptureCamera.cullingMask = this.m_LayerMask;
		this.m_BloomCaptureCamera.depth = this.m_Camera.depth + 1f;
		this.m_BloomCaptureCameraGO.transform.parent = this.m_Camera.transform;
		this.m_BloomCaptureCameraGO.transform.localPosition = Vector3.zero;
		this.m_BloomCaptureCameraGO.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06009007 RID: 36871 RVA: 0x002EBB48 File Offset: 0x002E9D48
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

	// Token: 0x06009008 RID: 36872 RVA: 0x002EBBD0 File Offset: 0x002E9DD0
	private void CreateRenderPlane()
	{
		if (this.m_CreateRenderPlane && this.m_GameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_GameObject);
		}
		this.m_GameObject = this.CreateMeshPlane(string.Format("{0}_RenderPlane", base.name), this.m_Material);
		SceneUtils.SetHideFlags(this.m_GameObject, this.m_DevFlag);
	}

	// Token: 0x06009009 RID: 36873 RVA: 0x002EBC34 File Offset: 0x002E9E34
	private void CreateBloomPlane()
	{
		if (this.m_BloomPlaneGameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_BloomPlaneGameObject);
		}
		Material material = this.AdditiveMaterial;
		if (this.m_BloomBlend == RenderToTexture2.BloomBlendType.Transparent)
		{
			material = this.TransparentMaterial;
		}
		this.m_BloomPlaneGameObject = this.CreateMeshPlane(string.Format("{0}_BloomRenderPlane", base.name), material);
		this.m_BloomPlaneGameObject.transform.parent = this.m_GameObject.transform;
		this.m_BloomPlaneGameObject.transform.localPosition = new Vector3(0f, 0.15f, 0f);
		this.m_BloomPlaneGameObject.transform.localRotation = Quaternion.identity;
		this.m_BloomPlaneGameObject.transform.localScale = Vector3.one;
		this.m_BloomPlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().color = this.m_BloomColor;
		SceneUtils.SetHideFlags(this.m_BloomPlaneGameObject, this.m_DevFlag);
	}

	// Token: 0x0600900A RID: 36874 RVA: 0x002EBD24 File Offset: 0x002E9F24
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
		gameObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
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
		Mesh mesh2 = gameObject.GetComponent<MeshFilter>().mesh = mesh;
		SceneUtils.SetHideFlags(mesh2, this.m_DevFlag);
		mesh2.RecalculateBounds();
		if (material)
		{
			material.renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
			gameObject.GetComponent<Renderer>().SetSharedMaterial(material);
		}
		return gameObject;
	}

	// Token: 0x0600900B RID: 36875 RVA: 0x002EBE84 File Offset: 0x002EA084
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

	// Token: 0x0600900C RID: 36876 RVA: 0x002EBEE4 File Offset: 0x002EA0E4
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
			Debug.LogError(string.Format(" RenderToTexture has a world scale of zero. \nm_WorldWidth: {0},   m_WorldHeight: {1}", this.m_WorldWidth, this.m_WorldHeight));
		}
	}

	// Token: 0x0600900D RID: 36877 RVA: 0x002EC068 File Offset: 0x002EA268
	private void CleanUp()
	{
		this.ReleaseTexture();
		if (this.m_CameraGO)
		{
			UnityEngine.Object.Destroy(this.m_CameraGO);
		}
		if (this.m_AlphaCameraGO)
		{
			UnityEngine.Object.Destroy(this.m_AlphaCameraGO);
		}
		if (this.m_BloomPlaneGameObject)
		{
			UnityEngine.Object.Destroy(this.m_BloomPlaneGameObject);
		}
		if (this.m_BloomCaptureCameraGO)
		{
			UnityEngine.Object.Destroy(this.m_BloomCaptureCameraGO);
		}
		if (this.m_ObjectToRender != null)
		{
			this.m_ObjectToRender.transform.localPosition = this.m_ObjectToRenderOrgPosition;
		}
		this.m_init = false;
		this.m_isDirty = true;
	}

	// Token: 0x0400787F RID: 30847
	private const string BLUR_SHADER_NAME = "Hidden/R2TBlur";

	// Token: 0x04007880 RID: 30848
	private const string BLUR_ALPHA_SHADER_NAME = "Hidden/R2TAlphaBlur";

	// Token: 0x04007881 RID: 30849
	private const string ALPHA_BLEND_SHADER_NAME = "Hidden/R2TColorAlphaCombine";

	// Token: 0x04007882 RID: 30850
	private const string ALPHA_BLEND_ADD_SHADER_NAME = "Hidden/R2TColorAlphaCombineAdd";

	// Token: 0x04007883 RID: 30851
	private const string ALPHA_FILL_SHADER_NAME = "Custom/AlphaFillOpaque";

	// Token: 0x04007884 RID: 30852
	private const string BLOOM_SHADER_NAME = "Hidden/R2TBloom";

	// Token: 0x04007885 RID: 30853
	private const string BLOOM_ALPHA_SHADER_NAME = "Hidden/R2TBloomAlpha";

	// Token: 0x04007886 RID: 30854
	private const string ADDITIVE_SHADER_NAME = "Hidden/R2TAdditive";

	// Token: 0x04007887 RID: 30855
	private const string TRANSPARENT_SHADER_NAME = "Hidden/R2TTransparent";

	// Token: 0x04007888 RID: 30856
	private const string ALPHA_CLIP_SHADER_NAME = "Hidden/R2TAlphaClip";

	// Token: 0x04007889 RID: 30857
	private const string ALPHA_CLIP_BLOOM_SHADER_NAME = "Hidden/R2TAlphaClipBloom";

	// Token: 0x0400788A RID: 30858
	private const string ALPHA_CLIP_GRADIENT_SHADER_NAME = "Hidden/R2TAlphaClipGradient";

	// Token: 0x0400788B RID: 30859
	private const RenderTextureFormat ALPHA_TEXTURE_FORMAT = RenderTextureFormat.ARGB32;

	// Token: 0x0400788C RID: 30860
	private const float RENDER_SIZE_QUALITY_LOW = 0.75f;

	// Token: 0x0400788D RID: 30861
	private const float RENDER_SIZE_QUALITY_MEDIUM = 1f;

	// Token: 0x0400788E RID: 30862
	private const float RENDER_SIZE_QUALITY_HIGH = 1.5f;

	// Token: 0x0400788F RID: 30863
	private readonly Vector2[] PLANE_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04007890 RID: 30864
	private readonly Vector3[] PLANE_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x04007891 RID: 30865
	private readonly int[] PLANE_TRIANGLES = new int[]
	{
		3,
		1,
		2,
		2,
		1,
		0
	};

	// Token: 0x04007892 RID: 30866
	[Tooltip("The highest level FX object that you want to render onto a texture.")]
	public GameObject m_ObjectToRender;

	// Token: 0x04007893 RID: 30867
	[Tooltip("An alpha object to render. If an object is provided, alpha passes will happen regardless of other settings. You still must provide a normal ObjectToRender as well.")]
	public GameObject m_AlphaObjectToRender;

	// Token: 0x04007894 RID: 30868
	[Tooltip("This will ensure that the texture is rendered every update, otherwise the render texture will only be rendered on generation and then when rendering or updates are manually called.")]
	public bool m_RealtimeRender = true;

	// Token: 0x04007895 RID: 30869
	[Tooltip("Render the texture with an alpha pass. You do not have to supply an \"Alpha Object to Render\"")]
	public bool m_RenderMeshAsAlpha;

	// Token: 0x04007896 RID: 30870
	[Tooltip("Render the object with an alpha pass and an alpha blend shader. You do not have to supply an \"Alpha object to Render\"")]
	public bool m_OpaqueObjectAlphaFill;

	// Token: 0x04007897 RID: 30871
	[Tooltip("Determines what types of default materials are created. To get a better idea, search the project for RT2 shaders. Only use \"Custom\" if you are supplying a custom material below, as Custom materials will ignore most alpha and bloom settings below.")]
	public RenderToTexture2.RenderToTextureMaterial m_RenderMaterial = RenderToTexture2.RenderToTextureMaterial.Transparent;

	// Token: 0x04007898 RID: 30872
	[Tooltip("The format of the RenderTexture that will be created.")]
	public RenderTextureFormat m_RenderTextureFormat;

	// Token: 0x04007899 RID: 30873
	[Tooltip("A custom material. If none is supplied, a default material will be created. This is an easy way to provide custom material settings and shaders.")]
	public Material m_Material;

	// Token: 0x0400789A RID: 30874
	[Space(10f)]
	[Tooltip("Check this true if you are not providing a custom mesh to apply the RenderTexture to. This will create a simple quad the texture will be projected upon.")]
	public bool m_CreateRenderPlane = true;

	// Token: 0x0400789B RID: 30875
	[Tooltip("A custom game object that the RenderTexture will render to instead of a default mesh plane. If a game object is provided, this will OVERRIDE \"Create Render Plane\". If you see issues with Bloom settings, try using a render plane.")]
	public GameObject m_RenderToObject;

	// Token: 0x0400789C RID: 30876
	[Tooltip("The name of a custom shader to be used for the material of the RenderTexture.")]
	public string m_ShaderTextureName = string.Empty;

	// Token: 0x0400789D RID: 30877
	[Space(10f)]
	[Tooltip("The maximum dimension that the render texture will be on any side. Keep this to a power of 2.")]
	public int m_Resolution = 128;

	// Token: 0x0400789E RID: 30878
	[Tooltip("The width of the render texture in pixels adjusted by resolution.")]
	public float m_Width = 1f;

	// Token: 0x0400789F RID: 30879
	[Tooltip("The height of the render texture in pixels adjusted by resolution.")]
	public float m_Height = 1f;

	// Token: 0x040078A0 RID: 30880
	[Tooltip("A setting to use a uniform world scale instead of a lossy scale when calculating the size for textures and generated quads. You will need to disable and re-enable to see changes in this setting.")]
	public bool m_UniformWorldScale;

	// Token: 0x040078A1 RID: 30881
	[Space(10f)]
	[Tooltip("The intensity level of the bloom.")]
	public float m_BloomIntensity;

	// Token: 0x040078A2 RID: 30882
	[Tooltip("The minimum value at which bloom will start being applied.")]
	public float m_BloomThreshold = 0.7f;

	// Token: 0x040078A3 RID: 30883
	[Tooltip("Sets the amount of blur that will be applied to the texture in bloom processes.")]
	public float m_BloomBlur = 0.3f;

	// Token: 0x040078A4 RID: 30884
	[Tooltip("The size of the bloom in comparison to the RenderTexture's size. 1.0 is 100% of the size of the RenderTexture.")]
	public float m_BloomResolutionRatio = 0.5f;

	// Token: 0x040078A5 RID: 30885
	[Tooltip("Whether or not the bloom renders with alpha or a solid color.")]
	public RenderToTexture2.BloomRenderType m_BloomRenderType;

	// Token: 0x040078A6 RID: 30886
	[Tooltip("A decimal value for the alpha intensity of the bloom shader. Only works if Bloom Render Type is set to \"Alpha\".")]
	public float m_BloomAlphaIntensity = 1f;

	// Token: 0x040078A7 RID: 30887
	[Tooltip("The blend type for the bloom. Use Additive for a whiter, intense, more blown out look, and Transparent for a gentler bloom.")]
	public RenderToTexture2.BloomBlendType m_BloomBlend;

	// Token: 0x040078A8 RID: 30888
	[Tooltip("A color that will be applied to the bloom.")]
	public Color m_BloomColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x040078A9 RID: 30889
	[Space(10f)]
	[Tooltip("Set the style of the Alpha clip shader. If you use gradient, you must provide an \"Alpha Clip Gradient Map\" below. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public RenderToTexture2.AlphaClipShader m_AlphaClipRenderStyle;

	// Token: 0x040078AA RID: 30890
	[Tooltip("A gradient map to be used if the \"Alpha Clip Render Style\" is set to \"Color Gradient\". \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public Texture2D m_AlphaClipGradientMap;

	// Token: 0x040078AB RID: 30891
	[Tooltip("The Cutoff for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClip = 15f;

	// Token: 0x040078AC RID: 30892
	[Tooltip("The Intensity for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClipIntensity = 1.5f;

	// Token: 0x040078AD RID: 30893
	[Tooltip("The Alpha Intensity for the Alpha clip, Bloom, and Alpha Clip Bloom materials, depending on what is being used. \"Render Material\" must be set to \"Alpha Clip\" or \"Alpha Clip Bloom\".")]
	public float m_AlphaClipAlphaIntensity = 1f;

	// Token: 0x040078AE RID: 30894
	[Tooltip("The amount of blur to use when sampling. \"Opaque Object Alpha Fill\" or \"Render Mesh as Alpha\" must be turned on, or an \"Alpha Object to Render\" must be provided.")]
	public float m_BlurAmount;

	// Token: 0x040078AF RID: 30895
	[Tooltip("Apply a shader material that will blur only alpha settings. You do not need any other settings turned on for this to take affect.")]
	public bool m_BlurAlphaOnly;

	// Token: 0x040078B0 RID: 30896
	[Tooltip("A tint that will be applied to the RenderTexture mesh plane material.")]
	public Color m_TintColor = Color.white;

	// Token: 0x040078B1 RID: 30897
	[Space(10f)]
	[Tooltip("An offset for setting where the materials of the RenderTexture should fall in the rendering order. '3000' is the value for the 'Transparent' queue level.")]
	public int m_RenderQueueOffset = 3000;

	// Token: 0x040078B2 RID: 30898
	[Tooltip("A setting for the render order that comes after the Render Queue Offset. If working with multiple RenderToTexture2's, you can use this to manually adjust their render order.")]
	public int m_RenderQueue;

	// Token: 0x040078B3 RID: 30899
	[Space(10f)]
	[Tooltip("The closest point to the camera at which drawing will occur.")]
	public float m_NearClip = -0.1f;

	// Token: 0x040078B4 RID: 30900
	[Tooltip("The furthest distance from the camera at which drawing will occur.")]
	public float m_FarClip = 0.5f;

	// Token: 0x040078B5 RID: 30901
	[Tooltip("Set the Clear Flags for the camera that will be used to render the texture. Recommended to keep this on Depth.")]
	public CameraClearFlags m_ClearFlags = CameraClearFlags.Depth;

	// Token: 0x040078B6 RID: 30902
	[Tooltip("Set the background color for the camera that will be used to render the texture.")]
	public Color m_ClearColor = Color.clear;

	// Token: 0x040078B7 RID: 30903
	[Tooltip("A replacement shader that will be applied to the main camera projecting onto the RenderTexture.")]
	public Shader m_ReplacementShader;

	// Token: 0x040078B8 RID: 30904
	[Tooltip("A replacement tag filter that the replacement shader will use when applied to the main camera. See Unity Docs for Replacement Tag info.")]
	public string m_ReplacementTag;

	// Token: 0x040078B9 RID: 30905
	[Tooltip("Replacement tag filters for the Alpha Camera replacement shader. If OpaqueObjectAlphaFill is checked, this will be overwritten as \"RenderType\". See Unity Docs for Replacement Tag info.")]
	public string m_AlphaReplacementTag;

	// Token: 0x040078BA RID: 30906
	[Tooltip("Allows a custom value for the camera's Orthographics size. See Unity docs for camera.orthographicSize.")]
	public float m_OverrideCameraSize;

	// Token: 0x040078BB RID: 30907
	[Tooltip("The render camera's transform's base position offset.")]
	public Vector3 m_PositionOffset = Vector3.zero;

	// Token: 0x040078BC RID: 30908
	[Tooltip("The additive offset between the camera and the object it's rendering. This is added after the above position offset.")]
	public Vector3 m_CameraOffset = Vector3.zero;

	// Token: 0x040078BD RID: 30909
	[Tooltip("The layer mask that will be used as the culling mask for the camera. Recommended: InvisibleRender.")]
	public LayerMask m_LayerMask = -1;

	// Token: 0x040078BE RID: 30910
	[Space(10f)]
	[Tooltip("Defers rendering until the LateUpdate function instead of Update. Also will skip some Bloom process effects.")]
	public bool m_LateUpdate;

	// Token: 0x040078BF RID: 30911
	[Tooltip("Do a rendering pass when this component runs its Start() function.")]
	public bool m_RenderOnStart = true;

	// Token: 0x040078C0 RID: 30912
	[Tooltip("Do a rendering pass when this component is enabled, either in Editor or in Game.")]
	public bool m_RenderOnEnable = true;

	// Token: 0x040078C1 RID: 30913
	private bool m_renderEnabled = true;

	// Token: 0x040078C2 RID: 30914
	private bool m_init;

	// Token: 0x040078C3 RID: 30915
	private float m_WorldWidth;

	// Token: 0x040078C4 RID: 30916
	private float m_WorldHeight;

	// Token: 0x040078C5 RID: 30917
	private Vector3 m_WorldScale;

	// Token: 0x040078C6 RID: 30918
	private GameObject m_CameraGO;

	// Token: 0x040078C7 RID: 30919
	private Camera m_Camera;

	// Token: 0x040078C8 RID: 30920
	private GameObject m_AlphaCameraGO;

	// Token: 0x040078C9 RID: 30921
	private Camera m_AlphaCamera;

	// Token: 0x040078CA RID: 30922
	private GameObject m_BloomCaptureCameraGO;

	// Token: 0x040078CB RID: 30923
	private Camera m_BloomCaptureCamera;

	// Token: 0x040078CC RID: 30924
	private RenderTexture m_RenderTexture;

	// Token: 0x040078CD RID: 30925
	private RenderTexture m_AlphaRenderTexture;

	// Token: 0x040078CE RID: 30926
	private RenderTexture m_BloomRenderTexture;

	// Token: 0x040078CF RID: 30927
	private RenderTexture m_BloomRenderBuffer1;

	// Token: 0x040078D0 RID: 30928
	private RenderTexture m_BloomRenderBuffer2;

	// Token: 0x040078D1 RID: 30929
	private GameObject m_GameObject;

	// Token: 0x040078D2 RID: 30930
	private GameObject m_BloomPlaneGameObject;

	// Token: 0x040078D3 RID: 30931
	private Vector3 m_ObjectToRenderOrgPosition = Vector3.zero;

	// Token: 0x040078D4 RID: 30932
	private bool m_isDirty;

	// Token: 0x040078D5 RID: 30933
	private Shader m_AlphaFillShader;

	// Token: 0x040078D6 RID: 30934
	private RenderToTexture2.RenderToTextureMaterial m_PreviousRenderMaterial;

	// Token: 0x040078D7 RID: 30935
	private List<Renderer> m_OpaqueObjectAlphaFillTransparent;

	// Token: 0x040078D8 RID: 30936
	private List<UberText> m_OpaqueObjectAlphaFillUberText;

	// Token: 0x040078D9 RID: 30937
	private HideFlags m_DevFlag = HideFlags.HideAndDontSave;

	// Token: 0x040078DA RID: 30938
	private Shader m_AlphaBlendShader;

	// Token: 0x040078DB RID: 30939
	private Material m_AlphaBlendMaterial;

	// Token: 0x040078DC RID: 30940
	private Shader m_AlphaBlendAddShader;

	// Token: 0x040078DD RID: 30941
	private Material m_AlphaBlendAddMaterial;

	// Token: 0x040078DE RID: 30942
	private Shader m_AdditiveShader;

	// Token: 0x040078DF RID: 30943
	private Material m_AdditiveMaterial;

	// Token: 0x040078E0 RID: 30944
	private Shader m_BloomShader;

	// Token: 0x040078E1 RID: 30945
	private Material m_BloomMaterial;

	// Token: 0x040078E2 RID: 30946
	private Shader m_BloomShaderAlpha;

	// Token: 0x040078E3 RID: 30947
	private Material m_BloomMaterialAlpha;

	// Token: 0x040078E4 RID: 30948
	private Shader m_BlurShader;

	// Token: 0x040078E5 RID: 30949
	private Material m_BlurMaterial;

	// Token: 0x040078E6 RID: 30950
	private Shader m_AlphaBlurShader;

	// Token: 0x040078E7 RID: 30951
	private Material m_AlphaBlurMaterial;

	// Token: 0x040078E8 RID: 30952
	private Shader m_TransparentShader;

	// Token: 0x040078E9 RID: 30953
	private Material m_TransparentMaterial;

	// Token: 0x040078EA RID: 30954
	private Shader m_AlphaClipShader;

	// Token: 0x040078EB RID: 30955
	private Material m_AlphaClipMaterial;

	// Token: 0x040078EC RID: 30956
	private Shader m_AlphaClipBloomShader;

	// Token: 0x040078ED RID: 30957
	private Material m_AlphaClipBloomMaterial;

	// Token: 0x040078EE RID: 30958
	private Shader m_AlphaClipGradientShader;

	// Token: 0x040078EF RID: 30959
	private Material m_AlphaClipGradientMaterial;

	// Token: 0x020026CF RID: 9935
	public enum RenderToTextureMaterial
	{
		// Token: 0x0400F225 RID: 61989
		Custom,
		// Token: 0x0400F226 RID: 61990
		Transparent,
		// Token: 0x0400F227 RID: 61991
		Additive,
		// Token: 0x0400F228 RID: 61992
		Bloom,
		// Token: 0x0400F229 RID: 61993
		AlphaClip,
		// Token: 0x0400F22A RID: 61994
		AlphaClipBloom
	}

	// Token: 0x020026D0 RID: 9936
	public enum AlphaClipShader
	{
		// Token: 0x0400F22C RID: 61996
		Standard,
		// Token: 0x0400F22D RID: 61997
		ColorGradient
	}

	// Token: 0x020026D1 RID: 9937
	public enum BloomRenderType
	{
		// Token: 0x0400F22F RID: 61999
		Color,
		// Token: 0x0400F230 RID: 62000
		Alpha
	}

	// Token: 0x020026D2 RID: 9938
	public enum BloomBlendType
	{
		// Token: 0x0400F232 RID: 62002
		Additive,
		// Token: 0x0400F233 RID: 62003
		Transparent
	}
}
