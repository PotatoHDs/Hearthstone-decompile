using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000A30 RID: 2608
[RequireComponent(typeof(Camera))]
public class FullScreenEffects : MonoBehaviour
{
	// Token: 0x170007D1 RID: 2001
	// (get) Token: 0x06008C17 RID: 35863 RVA: 0x002CD7C7 File Offset: 0x002CB9C7
	protected Material blurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.m_BlurShader);
				SceneUtils.SetHideFlags(this.m_BlurMaterial, HideFlags.DontSave);
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x170007D2 RID: 2002
	// (get) Token: 0x06008C18 RID: 35864 RVA: 0x002CD7FB File Offset: 0x002CB9FB
	protected Material blurVignettingMaterial
	{
		get
		{
			if (this.m_BlurVignettingMaterial == null)
			{
				this.m_BlurVignettingMaterial = new Material(this.m_BlurVignettingShader);
				SceneUtils.SetHideFlags(this.m_BlurVignettingMaterial, HideFlags.DontSave);
			}
			return this.m_BlurVignettingMaterial;
		}
	}

	// Token: 0x170007D3 RID: 2003
	// (get) Token: 0x06008C19 RID: 35865 RVA: 0x002CD82F File Offset: 0x002CBA2F
	protected Material blurDesatMaterial
	{
		get
		{
			if (this.m_BlurDesatMaterial == null)
			{
				this.m_BlurDesatMaterial = new Material(this.m_BlurDesaturationShader);
				SceneUtils.SetHideFlags(this.m_BlurDesatMaterial, HideFlags.DontSave);
			}
			return this.m_BlurDesatMaterial;
		}
	}

	// Token: 0x170007D4 RID: 2004
	// (get) Token: 0x06008C1A RID: 35866 RVA: 0x002CD863 File Offset: 0x002CBA63
	protected Material blendMaterial
	{
		get
		{
			if (this.m_BlendMaterial == null)
			{
				this.m_BlendMaterial = new Material(this.m_BlendShader);
				SceneUtils.SetHideFlags(this.m_BlendMaterial, HideFlags.DontSave);
			}
			return this.m_BlendMaterial;
		}
	}

	// Token: 0x170007D5 RID: 2005
	// (get) Token: 0x06008C1B RID: 35867 RVA: 0x002CD897 File Offset: 0x002CBA97
	protected Material VignettingMaterial
	{
		get
		{
			if (this.m_VignettingMaterial == null)
			{
				this.m_VignettingMaterial = new Material(this.m_VignettingShader);
				SceneUtils.SetHideFlags(this.m_VignettingMaterial, HideFlags.DontSave);
			}
			return this.m_VignettingMaterial;
		}
	}

	// Token: 0x170007D6 RID: 2006
	// (get) Token: 0x06008C1C RID: 35868 RVA: 0x002CD8CB File Offset: 0x002CBACB
	protected Material BlendToColorMaterial
	{
		get
		{
			if (this.m_BlendToColorMaterial == null)
			{
				this.m_BlendToColorMaterial = new Material(this.m_BlendToColorShader);
				SceneUtils.SetHideFlags(this.m_BlendToColorMaterial, HideFlags.DontSave);
			}
			return this.m_BlendToColorMaterial;
		}
	}

	// Token: 0x170007D7 RID: 2007
	// (get) Token: 0x06008C1D RID: 35869 RVA: 0x002CD8FF File Offset: 0x002CBAFF
	protected Material DesaturationMaterial
	{
		get
		{
			if (this.m_DesaturationMaterial == null)
			{
				this.m_DesaturationMaterial = new Material(this.m_DesaturationShader);
				SceneUtils.SetHideFlags(this.m_DesaturationMaterial, HideFlags.DontSave);
			}
			return this.m_DesaturationMaterial;
		}
	}

	// Token: 0x170007D8 RID: 2008
	// (get) Token: 0x06008C1E RID: 35870 RVA: 0x002CD933 File Offset: 0x002CBB33
	protected Material DesaturationVignettingMaterial
	{
		get
		{
			if (this.m_DesaturationVignettingMaterial == null)
			{
				this.m_DesaturationVignettingMaterial = new Material(this.m_DesaturationVignettingShader);
				SceneUtils.SetHideFlags(this.m_DesaturationVignettingMaterial, HideFlags.DontSave);
			}
			return this.m_DesaturationVignettingMaterial;
		}
	}

	// Token: 0x170007D9 RID: 2009
	// (get) Token: 0x06008C1F RID: 35871 RVA: 0x002CD967 File Offset: 0x002CBB67
	protected Material BlurDesaturationVignettingMaterial
	{
		get
		{
			if (this.m_BlurDesaturationVignettingMaterial == null)
			{
				this.m_BlurDesaturationVignettingMaterial = new Material(this.m_BlurDesaturationVignettingShader);
				SceneUtils.SetHideFlags(this.m_BlurDesaturationVignettingMaterial, HideFlags.DontSave);
			}
			return this.m_BlurDesaturationVignettingMaterial;
		}
	}

	// Token: 0x06008C20 RID: 35872 RVA: 0x002CD99B File Offset: 0x002CBB9B
	private void OnEnable()
	{
		if (this.m_IgnoreCamera != null)
		{
			this.m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
		}
		this.m_Camera.forceIntoRenderTexture = true;
	}

	// Token: 0x06008C21 RID: 35873 RVA: 0x002CD9C4 File Offset: 0x002CBBC4
	protected void OnDisable()
	{
		this.SetDefaults();
		if (this.m_BlurMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurMaterial);
		}
		if (this.m_BlurVignettingMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurVignettingMaterial);
		}
		if (this.m_BlurDesatMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurDesatMaterial);
		}
		if (this.m_BlendMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlendMaterial);
		}
		if (this.m_VignettingMaterial)
		{
			UnityEngine.Object.Destroy(this.m_VignettingMaterial);
		}
		if (this.m_BlendToColorMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlendToColorMaterial);
		}
		if (this.m_DesaturationMaterial)
		{
			UnityEngine.Object.Destroy(this.m_DesaturationMaterial);
		}
		if (this.m_DesaturationVignettingMaterial)
		{
			UnityEngine.Object.Destroy(this.m_DesaturationVignettingMaterial);
		}
		if (this.m_BlurDesaturationVignettingMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurDesaturationVignettingMaterial);
		}
	}

	// Token: 0x06008C22 RID: 35874 RVA: 0x002CDAB0 File Offset: 0x002CBCB0
	protected void OnDestroy()
	{
		CheatMgr cheatMgr = CheatMgr.Get();
		if (cheatMgr != null)
		{
			cheatMgr.UnregisterCheatHandler("wireframe", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_RenderWireframe));
		}
	}

	// Token: 0x06008C23 RID: 35875 RVA: 0x002CDAE0 File Offset: 0x002CBCE0
	protected void Awake()
	{
		this.m_Camera = base.GetComponent<Camera>();
		if (this.m_IgnoreCamera != null)
		{
			int cullingMask = this.m_IgnoreCamera.cullingMask;
			CameraClearFlags clearFlags = this.m_IgnoreCamera.clearFlags;
			this.m_IgnoreCamera.CopyFrom(this.m_Camera);
			this.m_IgnoreCamera.cullingMask = cullingMask;
			this.m_IgnoreCamera.clearFlags = clearFlags;
			this.m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
			this.m_IgnoreCamera.depth = this.m_Camera.depth + 10f;
			this.m_IgnoreCamera.transform.localPosition = Vector3.zero;
			this.m_IgnoreCamera.transform.localRotation = Quaternion.identity;
			this.m_IgnoreCamera.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06008C24 RID: 35876 RVA: 0x002CDBB4 File Offset: 0x002CBDB4
	protected void Start()
	{
		CheatMgr cheatMgr = CheatMgr.Get();
		if (cheatMgr != null)
		{
			cheatMgr.RegisterCheatHandler("wireframe", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_RenderWireframe), null, null, null);
		}
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("Fullscreen Effects not supported");
			base.enabled = false;
			return;
		}
		if (this.m_BlurShader == null)
		{
			this.m_BlurShader = ShaderUtils.FindShader("Custom/FullScreen/Blur");
		}
		if (!this.m_BlurShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blur");
			base.enabled = false;
		}
		if (!this.m_BlurShader || !this.blurMaterial.shader.isSupported)
		{
			Debug.LogError("Fullscreen Effect Shader not supported: Custom/FullScreen/Blur");
			base.enabled = false;
			return;
		}
		if (this.m_BlurVignettingShader == null)
		{
			this.m_BlurVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurVignetting");
		}
		if (!this.m_BlurVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurVignetting");
			base.enabled = false;
		}
		if (this.m_BlurDesaturationShader == null)
		{
			this.m_BlurDesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationBlur");
		}
		if (!this.m_BlurDesaturationShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationBlur");
			base.enabled = false;
		}
		if (this.m_BlendShader == null)
		{
			this.m_BlendShader = ShaderUtils.FindShader("Custom/FullScreen/Blend");
		}
		if (!this.m_BlendShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Blend");
			base.enabled = false;
		}
		if (this.m_VignettingShader == null)
		{
			this.m_VignettingShader = ShaderUtils.FindShader("Custom/FullScreen/Vignetting");
		}
		if (!this.m_VignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Vignetting");
			base.enabled = false;
		}
		if (this.m_BlendToColorShader == null)
		{
			this.m_BlendToColorShader = ShaderUtils.FindShader("Custom/FullScreen/BlendToColor");
		}
		if (!this.m_BlendToColorShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlendToColor");
			base.enabled = false;
		}
		if (this.m_DesaturationShader == null)
		{
			this.m_DesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/Desaturation");
		}
		if (!this.m_DesaturationShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/Desaturation");
			base.enabled = false;
		}
		if (this.m_DesaturationVignettingShader == null)
		{
			this.m_DesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationVignetting");
		}
		if (!this.m_DesaturationVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/DesaturationVignetting");
			base.enabled = false;
		}
		if (this.m_BlurDesaturationVignettingShader == null)
		{
			this.m_BlurDesaturationVignettingShader = ShaderUtils.FindShader("Custom/FullScreen/BlurDesaturationVignetting");
		}
		if (!this.m_BlurDesaturationVignettingShader)
		{
			Debug.LogError("Fullscreen Effect Failed to load Shader: Custom/FullScreen/BlurDesaturationVignetting");
			base.enabled = false;
		}
		this.m_InitComplete = true;
		this.UpdateEffectBuffers();
	}

	// Token: 0x06008C25 RID: 35877 RVA: 0x002CDE57 File Offset: 0x002CC057
	private void Update()
	{
		if (!this.IsActive)
		{
			if (this.m_DeactivateFrameCount > 2)
			{
				this.m_DeactivateFrameCount = 0;
				this.Disable();
				return;
			}
			this.m_DeactivateFrameCount++;
		}
	}

	// Token: 0x170007DA RID: 2010
	// (get) Token: 0x06008C26 RID: 35878 RVA: 0x002CDE86 File Offset: 0x002CC086
	public Camera Camera
	{
		get
		{
			return this.m_Camera;
		}
	}

	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x06008C27 RID: 35879 RVA: 0x002CDE8E File Offset: 0x002CC08E
	// (set) Token: 0x06008C28 RID: 35880 RVA: 0x002CDE96 File Offset: 0x002CC096
	public bool BlurEnabled
	{
		get
		{
			return this.m_BlurEnabled;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			this.m_BlurEnabled = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x06008C29 RID: 35881 RVA: 0x002CDEB9 File Offset: 0x002CC0B9
	// (set) Token: 0x06008C2A RID: 35882 RVA: 0x002CDEC1 File Offset: 0x002CC0C1
	public float BlurBlend
	{
		get
		{
			return this.m_BlurBlend;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlurEnabled = true;
			this.m_BlurBlend = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x06008C2B RID: 35883 RVA: 0x002CDEE6 File Offset: 0x002CC0E6
	// (set) Token: 0x06008C2C RID: 35884 RVA: 0x002CDEEE File Offset: 0x002CC0EE
	public float BlurAmount
	{
		get
		{
			return this.m_BlurAmount;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlurEnabled = true;
			this.m_BlurAmount = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x06008C2D RID: 35885 RVA: 0x002CDF13 File Offset: 0x002CC113
	// (set) Token: 0x06008C2E RID: 35886 RVA: 0x002CDF1B File Offset: 0x002CC11B
	public float BlurDesaturation
	{
		get
		{
			return this.m_BlurDesaturation;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlurEnabled = true;
			this.m_BlurDesaturation = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x06008C2F RID: 35887 RVA: 0x002CDF40 File Offset: 0x002CC140
	// (set) Token: 0x06008C30 RID: 35888 RVA: 0x002CDF48 File Offset: 0x002CC148
	public float BlurBrightness
	{
		get
		{
			return this.m_BlurBrightness;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlurEnabled = true;
			this.m_BlurBrightness = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x06008C31 RID: 35889 RVA: 0x002CDF6D File Offset: 0x002CC16D
	// (set) Token: 0x06008C32 RID: 35890 RVA: 0x002CDF75 File Offset: 0x002CC175
	public bool VignettingEnable
	{
		get
		{
			return this.m_VignettingEnable;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			this.m_VignettingEnable = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x06008C33 RID: 35891 RVA: 0x002CDF98 File Offset: 0x002CC198
	// (set) Token: 0x06008C34 RID: 35892 RVA: 0x002CDFA0 File Offset: 0x002CC1A0
	public float VignettingIntensity
	{
		get
		{
			return this.m_VignettingIntensity;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_VignettingEnable = true;
			this.m_VignettingIntensity = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x06008C35 RID: 35893 RVA: 0x002CDFC5 File Offset: 0x002CC1C5
	// (set) Token: 0x06008C36 RID: 35894 RVA: 0x002CDFCD File Offset: 0x002CC1CD
	public bool BlendToColorEnable
	{
		get
		{
			return this.m_BlendToColorEnable;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			this.m_BlendToColorEnable = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x06008C37 RID: 35895 RVA: 0x002CDFF0 File Offset: 0x002CC1F0
	// (set) Token: 0x06008C38 RID: 35896 RVA: 0x002CDFF8 File Offset: 0x002CC1F8
	public Color BlendColor
	{
		get
		{
			return this.m_BlendColor;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlendToColorEnable = true;
			this.m_BlendColor = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x06008C39 RID: 35897 RVA: 0x002CE01D File Offset: 0x002CC21D
	// (set) Token: 0x06008C3A RID: 35898 RVA: 0x002CE025 File Offset: 0x002CC225
	public float BlendToColorAmount
	{
		get
		{
			return this.m_BlendToColorAmount;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_BlendToColorEnable = true;
			this.m_BlendToColorAmount = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E5 RID: 2021
	// (get) Token: 0x06008C3B RID: 35899 RVA: 0x002CE04A File Offset: 0x002CC24A
	// (set) Token: 0x06008C3C RID: 35900 RVA: 0x002CE052 File Offset: 0x002CC252
	public bool DesaturationEnabled
	{
		get
		{
			return this.m_DesaturationEnabled;
		}
		set
		{
			if (!base.enabled && value)
			{
				base.enabled = true;
			}
			this.m_DesaturationEnabled = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x06008C3D RID: 35901 RVA: 0x002CE075 File Offset: 0x002CC275
	// (set) Token: 0x06008C3E RID: 35902 RVA: 0x002CE07D File Offset: 0x002CC27D
	public float Desaturation
	{
		get
		{
			return this.m_Desaturation;
		}
		set
		{
			if (!base.enabled)
			{
				base.enabled = true;
			}
			this.m_DesaturationEnabled = true;
			this.m_Desaturation = value;
			this.UpdateEffectBuffers();
		}
	}

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x06008C3F RID: 35903 RVA: 0x002CE0A2 File Offset: 0x002CC2A2
	public bool IsActive
	{
		get
		{
			return base.gameObject.activeInHierarchy && base.enabled && this.HasActiveEffects;
		}
	}

	// Token: 0x170007E8 RID: 2024
	// (get) Token: 0x06008C40 RID: 35904 RVA: 0x002CE0C4 File Offset: 0x002CC2C4
	public bool HasActiveEffects
	{
		get
		{
			return (this.m_BlurEnabled && this.m_BlurBlend > 0f) || this.m_VignettingEnable || this.m_BlendToColorEnable || this.m_DesaturationEnabled || this.m_WireframeRender;
		}
	}

	// Token: 0x06008C41 RID: 35905 RVA: 0x002CE114 File Offset: 0x002CC314
	private void SetDefaults()
	{
		this.m_BlurEnabled = false;
		this.m_BlurBlend = 1f;
		this.m_BlurAmount = 2f;
		this.m_BlurDesaturation = 0f;
		this.m_BlurBrightness = 1f;
		this.m_VignettingEnable = false;
		this.m_VignettingIntensity = 0f;
		this.m_BlendToColorEnable = false;
		this.m_BlendColor = Color.white;
		this.m_BlendToColorAmount = 0f;
		this.m_DesaturationEnabled = false;
		this.m_Desaturation = 0f;
		this.UpdateEffectBuffers();
	}

	// Token: 0x06008C42 RID: 35906 RVA: 0x002CE19C File Offset: 0x002CC39C
	public void Disable()
	{
		base.enabled = false;
		this.SetDefaults();
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.WillReset();
		}
	}

	// Token: 0x06008C43 RID: 35907 RVA: 0x002CE1C8 File Offset: 0x002CC3C8
	private void CalcTextureSize(int currentWidth, int currentHeight, int resolution, out float sizeX, out float sizeY)
	{
		float num = (float)currentWidth;
		float num2 = (float)currentHeight;
		float num3 = (float)resolution;
		if (num > num2)
		{
			sizeX = num3;
			sizeY = num3 * (num2 / num);
			return;
		}
		sizeX = num3 * (num / num2);
		sizeY = num3;
	}

	// Token: 0x06008C44 RID: 35908 RVA: 0x002CE1FB File Offset: 0x002CC3FB
	private void OnPreRender()
	{
		if (this.m_WireframeRender)
		{
			GL.wireframe = true;
		}
	}

	// Token: 0x06008C45 RID: 35909 RVA: 0x002CC6F4 File Offset: 0x002CA8F4
	private void OnPostRender()
	{
		GL.wireframe = false;
	}

	// Token: 0x06008C46 RID: 35910 RVA: 0x002CE20B File Offset: 0x002CC40B
	private bool OnProcessCheat_RenderWireframe(string func, string[] args, string rawArgs)
	{
		if (this.m_WireframeRender)
		{
			this.m_WireframeRender = false;
			return true;
		}
		this.m_WireframeRender = true;
		base.enabled = true;
		return true;
	}

	// Token: 0x06008C47 RID: 35911 RVA: 0x002CE230 File Offset: 0x002CC430
	private void UpdateEffectBuffers()
	{
		if (!this.m_InitComplete)
		{
			return;
		}
		this.m_Camera.RemoveCommandBuffers(CameraEvent.AfterImageEffects);
		this.m_EffectBuffer = null;
		bool flag = false;
		if (this.m_BlurEnabled && this.m_BlurBlend > 0f)
		{
			Material material = this.blurMaterial;
			material.SetFloat("_Brightness", this.m_BlurBrightness);
			if (this.m_BlurDesaturation > 0f && !this.m_VignettingEnable)
			{
				material = this.blurDesatMaterial;
				material.SetFloat("_Desaturation", this.m_BlurDesaturation);
			}
			else if (this.m_VignettingEnable && this.m_BlurDesaturation == 0f)
			{
				material = this.blurVignettingMaterial;
				material.SetFloat("_Amount", this.m_VignettingIntensity);
				material.SetTexture("_MaskTex", this.m_VignettingMask);
			}
			else if (this.m_VignettingEnable && this.m_BlurDesaturation > 0f)
			{
				material = this.BlurDesaturationVignettingMaterial;
				material.SetFloat("_Amount", this.m_VignettingIntensity);
				material.SetTexture("_MaskTex", this.m_VignettingMask);
				material.SetFloat("_Desaturation", this.m_BlurDesaturation);
			}
			this.m_EffectBuffer = this.InitBlurBuffer(material);
			flag = true;
		}
		if (this.m_DesaturationEnabled && !flag)
		{
			Material material2 = this.DesaturationMaterial;
			if (this.m_VignettingEnable)
			{
				material2 = this.DesaturationVignettingMaterial;
				material2.SetFloat("_Amount", this.m_VignettingIntensity);
				material2.SetTexture("_MaskTex", this.m_VignettingMask);
			}
			material2.SetFloat("_Desaturation", this.m_Desaturation);
			this.m_EffectBuffer = this.InitEffectBuffer(material2, null);
			flag = true;
		}
		if (this.m_VignettingEnable && !flag)
		{
			Material vignettingMaterial = this.VignettingMaterial;
			vignettingMaterial.SetFloat("_Amount", this.m_VignettingIntensity);
			vignettingMaterial.SetTexture("_MaskTex", this.m_VignettingMask);
			this.m_EffectBuffer = this.InitEffectBuffer(vignettingMaterial, null);
			flag = true;
		}
		if (this.m_BlendToColorEnable && !flag)
		{
			Material blendToColorMaterial = this.BlendToColorMaterial;
			blendToColorMaterial.SetFloat("_Amount", this.m_BlendToColorAmount);
			blendToColorMaterial.SetColor("_Color", this.m_BlendColor);
			this.m_EffectBuffer = this.InitEffectBuffer(blendToColorMaterial, null);
		}
		if (this.m_EffectBuffer != null)
		{
			this.m_Camera.AddCommandBuffer(CameraEvent.AfterImageEffects, this.m_EffectBuffer);
		}
	}

	// Token: 0x06008C48 RID: 35912 RVA: 0x002CE46C File Offset: 0x002CC66C
	private CommandBuffer InitEffectBuffer(Material material, CommandBuffer cb = null)
	{
		if (material == null)
		{
			return null;
		}
		if (cb == null)
		{
			cb = new CommandBuffer();
		}
		cb.name = "FullScreenEffectBuffer";
		int nameID = Shader.PropertyToID("_ScreenCopyTexture");
		cb.GetTemporaryRT(nameID, -1, -1, 0, FilterMode.Bilinear);
		cb.SetRenderTarget(nameID, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
		cb.Blit(BuiltinRenderTextureType.CameraTarget, nameID);
		cb.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
		cb.Blit(nameID, BuiltinRenderTextureType.CurrentActive, material);
		cb.ReleaseTemporaryRT(nameID);
		return cb;
	}

	// Token: 0x06008C49 RID: 35913 RVA: 0x002CE4FC File Offset: 0x002CC6FC
	private CommandBuffer InitBlurBuffer(Material blurMat)
	{
		if (blurMat == null)
		{
			return null;
		}
		CommandBuffer commandBuffer = new CommandBuffer();
		commandBuffer.name = "FullScreenEffectBuffer";
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		this.CalcTextureSize(Screen.width, Screen.height, 512, out num, out num2);
		int nameID = Shader.PropertyToID("_Blur1");
		int nameID2 = Shader.PropertyToID("_Blur2");
		int nameID3 = Shader.PropertyToID("_ScreenCopyTexture");
		commandBuffer.GetTemporaryRT(nameID3, -1, -1, 0, FilterMode.Bilinear);
		commandBuffer.GetTemporaryRT(nameID, (int)num, (int)num2, 0, FilterMode.Bilinear);
		commandBuffer.GetTemporaryRT(nameID2, (int)(num * 0.5f), (int)(num2 * 0.5f), 0, FilterMode.Bilinear);
		commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, nameID3);
		commandBuffer.SetGlobalFloat("_BlurOffset", 1f);
		commandBuffer.SetGlobalFloat("_FirstPass", 1f);
		commandBuffer.Blit(nameID3, nameID, blurMat);
		commandBuffer.SetGlobalFloat("_BlurOffset", 0.4f);
		commandBuffer.SetGlobalFloat("_FirstPass", 0f);
		commandBuffer.Blit(nameID, nameID2, blurMat);
		commandBuffer.ReleaseTemporaryRT(nameID);
		commandBuffer.SetGlobalFloat("_BlurOffset", -0.2f);
		commandBuffer.SetGlobalFloat("_FirstPass", 0f);
		if (this.m_BlurBlend >= 1f)
		{
			commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			commandBuffer.Blit(nameID2, BuiltinRenderTextureType.CurrentActive, blurMat);
			commandBuffer.ReleaseTemporaryRT(nameID2);
		}
		else
		{
			int nameID4 = Shader.PropertyToID("_Blend");
			commandBuffer.GetTemporaryRT(nameID4, 512, 512, 0, FilterMode.Bilinear);
			commandBuffer.Blit(nameID2, nameID4, blurMat);
			commandBuffer.ReleaseTemporaryRT(nameID2);
			commandBuffer.SetGlobalFloat("_Amount", this.m_BlurBlend);
			commandBuffer.SetGlobalTexture("_BlendTex", nameID4);
			commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			commandBuffer.Blit(nameID3, BuiltinRenderTextureType.CurrentActive, this.m_BlendMaterial);
			commandBuffer.ReleaseTemporaryRT(nameID4);
		}
		commandBuffer.ReleaseTemporaryRT(nameID3);
		return commandBuffer;
	}

	// Token: 0x04007502 RID: 29954
	private const int NO_WORK_FRAMES_BEFORE_DEACTIVATE = 2;

	// Token: 0x04007503 RID: 29955
	private const string BLUR_SHADER_NAME = "Custom/FullScreen/Blur";

	// Token: 0x04007504 RID: 29956
	private const string BLUR_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurVignetting";

	// Token: 0x04007505 RID: 29957
	private const string BLUR_DESATURATION_SHADER_NAME = "Custom/FullScreen/DesaturationBlur";

	// Token: 0x04007506 RID: 29958
	private const string BLEND_SHADER_NAME = "Custom/FullScreen/Blend";

	// Token: 0x04007507 RID: 29959
	private const string VIGNETTING_SHADER_NAME = "Custom/FullScreen/Vignetting";

	// Token: 0x04007508 RID: 29960
	private const string BLEND_TO_COLOR_SHADER_NAME = "Custom/FullScreen/BlendToColor";

	// Token: 0x04007509 RID: 29961
	private const string DESATURATION_SHADER_NAME = "Custom/FullScreen/Desaturation";

	// Token: 0x0400750A RID: 29962
	private const string DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/DesaturationVignetting";

	// Token: 0x0400750B RID: 29963
	private const string BLUR_DESATURATION_VIGNETTING_SHADER_NAME = "Custom/FullScreen/BlurDesaturationVignetting";

	// Token: 0x0400750C RID: 29964
	private const int BLUR_BUFFER_SIZE = 512;

	// Token: 0x0400750D RID: 29965
	private const float BLUR_SECOND_PASS_REDUCTION = 0.5f;

	// Token: 0x0400750E RID: 29966
	private const float BLUR_PASS_1_OFFSET = 1f;

	// Token: 0x0400750F RID: 29967
	private const float BLUR_PASS_2_OFFSET = 0.4f;

	// Token: 0x04007510 RID: 29968
	private const float BLUR_PASS_3_OFFSET = -0.2f;

	// Token: 0x04007511 RID: 29969
	public Texture2D m_VignettingMask;

	// Token: 0x04007512 RID: 29970
	public Camera m_IgnoreCamera;

	// Token: 0x04007513 RID: 29971
	private bool m_BlurEnabled;

	// Token: 0x04007514 RID: 29972
	public float m_BlurBlend = 1f;

	// Token: 0x04007515 RID: 29973
	private float m_BlurAmount = 2f;

	// Token: 0x04007516 RID: 29974
	private float m_BlurDesaturation;

	// Token: 0x04007517 RID: 29975
	private float m_BlurBrightness = 1f;

	// Token: 0x04007518 RID: 29976
	private bool m_VignettingEnable;

	// Token: 0x04007519 RID: 29977
	private float m_VignettingIntensity;

	// Token: 0x0400751A RID: 29978
	private bool m_BlendToColorEnable;

	// Token: 0x0400751B RID: 29979
	private Color m_BlendColor = Color.white;

	// Token: 0x0400751C RID: 29980
	private float m_BlendToColorAmount;

	// Token: 0x0400751D RID: 29981
	private bool m_DesaturationEnabled;

	// Token: 0x0400751E RID: 29982
	private float m_Desaturation;

	// Token: 0x0400751F RID: 29983
	private bool m_WireframeRender;

	// Token: 0x04007520 RID: 29984
	private int m_DeactivateFrameCount;

	// Token: 0x04007521 RID: 29985
	private Shader m_BlurShader;

	// Token: 0x04007522 RID: 29986
	private Shader m_BlurVignettingShader;

	// Token: 0x04007523 RID: 29987
	private Shader m_BlurDesaturationShader;

	// Token: 0x04007524 RID: 29988
	private Shader m_BlendShader;

	// Token: 0x04007525 RID: 29989
	private Shader m_VignettingShader;

	// Token: 0x04007526 RID: 29990
	private Shader m_BlendToColorShader;

	// Token: 0x04007527 RID: 29991
	private Shader m_DesaturationShader;

	// Token: 0x04007528 RID: 29992
	private Shader m_DesaturationVignettingShader;

	// Token: 0x04007529 RID: 29993
	private Shader m_BlurDesaturationVignettingShader;

	// Token: 0x0400752A RID: 29994
	private Camera m_Camera;

	// Token: 0x0400752B RID: 29995
	private CommandBuffer m_EffectBuffer;

	// Token: 0x0400752C RID: 29996
	private bool m_InitComplete;

	// Token: 0x0400752D RID: 29997
	private Material m_BlurMaterial;

	// Token: 0x0400752E RID: 29998
	private Material m_BlurVignettingMaterial;

	// Token: 0x0400752F RID: 29999
	private Material m_BlurDesatMaterial;

	// Token: 0x04007530 RID: 30000
	private Material m_BlendMaterial;

	// Token: 0x04007531 RID: 30001
	private Material m_VignettingMaterial;

	// Token: 0x04007532 RID: 30002
	private Material m_BlendToColorMaterial;

	// Token: 0x04007533 RID: 30003
	private Material m_DesaturationMaterial;

	// Token: 0x04007534 RID: 30004
	private Material m_DesaturationVignettingMaterial;

	// Token: 0x04007535 RID: 30005
	private Material m_BlurDesaturationVignettingMaterial;
}
