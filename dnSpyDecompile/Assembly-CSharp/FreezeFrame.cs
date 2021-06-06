using System;
using UnityEngine;

// Token: 0x02000A2E RID: 2606
public class FreezeFrame : MonoBehaviour
{
	// Token: 0x170007C8 RID: 1992
	// (get) Token: 0x06008BF6 RID: 35830 RVA: 0x002CCE32 File Offset: 0x002CB032
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

	// Token: 0x170007C9 RID: 1993
	// (get) Token: 0x06008BF7 RID: 35831 RVA: 0x002CCE66 File Offset: 0x002CB066
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

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x06008BF8 RID: 35832 RVA: 0x002CCE9A File Offset: 0x002CB09A
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

	// Token: 0x06008BF9 RID: 35833 RVA: 0x002CCECE File Offset: 0x002CB0CE
	private void OnEnable()
	{
		if (this.m_IgnoreCamera != null)
		{
			this.m_IgnoreCamera.depthTextureMode = DepthTextureMode.None;
		}
	}

	// Token: 0x06008BFA RID: 35834 RVA: 0x002CCEEC File Offset: 0x002CB0EC
	protected void OnDisable()
	{
		if (this.m_FrozenState)
		{
			this.Unfreeze();
		}
		this.SetDefaults();
		if (this.m_BlurMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurMaterial);
		}
		if (this.m_BlurDesatMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlurDesatMaterial);
		}
		if (this.m_BlendMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlendMaterial);
		}
	}

	// Token: 0x06008BFB RID: 35835 RVA: 0x002CCF58 File Offset: 0x002CB158
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
			this.m_IgnoreCamera.depth = this.m_Camera.depth + 1f;
			this.m_IgnoreCamera.transform.localPosition = Vector3.zero;
			this.m_IgnoreCamera.transform.localRotation = Quaternion.identity;
			this.m_IgnoreCamera.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06008BFC RID: 35836 RVA: 0x002CD02C File Offset: 0x002CB22C
	protected void Start()
	{
		base.gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("FreezeFrame not supported");
			base.enabled = false;
			return;
		}
		if (this.m_BlurShader == null)
		{
			this.m_BlurShader = ShaderUtils.FindShader("Custom/FullScreen/Blur");
		}
		if (!this.m_BlurShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/Blur");
			base.enabled = false;
		}
		if (!this.m_BlurShader || !this.blurMaterial.shader.isSupported)
		{
			Debug.LogError("FreezeFrame Effect Shader not supported: Custom/FullScreen/Blur");
			base.enabled = false;
			return;
		}
		if (this.m_BlurDesaturationShader == null)
		{
			this.m_BlurDesaturationShader = ShaderUtils.FindShader("Custom/FullScreen/DesaturationBlur");
		}
		if (!this.m_BlurDesaturationShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/DesaturationBlur");
			base.enabled = false;
		}
		if (this.m_BlendShader == null)
		{
			this.m_BlendShader = ShaderUtils.FindShader("Custom/FullScreen/Blend");
		}
		if (!this.m_BlendShader)
		{
			Debug.LogError("FreezeFrame Effect Failed to load Shader: Custom/FullScreen/Blend");
			base.enabled = false;
		}
	}

	// Token: 0x170007CB RID: 1995
	// (get) Token: 0x06008BFD RID: 35837 RVA: 0x002CD148 File Offset: 0x002CB348
	// (set) Token: 0x06008BFE RID: 35838 RVA: 0x002CD150 File Offset: 0x002CB350
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
		}
	}

	// Token: 0x170007CC RID: 1996
	// (get) Token: 0x06008BFF RID: 35839 RVA: 0x002CD16D File Offset: 0x002CB36D
	// (set) Token: 0x06008C00 RID: 35840 RVA: 0x002CD175 File Offset: 0x002CB375
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
		}
	}

	// Token: 0x170007CD RID: 1997
	// (get) Token: 0x06008C01 RID: 35841 RVA: 0x002CD194 File Offset: 0x002CB394
	// (set) Token: 0x06008C02 RID: 35842 RVA: 0x002CD19C File Offset: 0x002CB39C
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
		}
	}

	// Token: 0x170007CE RID: 1998
	// (get) Token: 0x06008C03 RID: 35843 RVA: 0x002CD1BB File Offset: 0x002CB3BB
	// (set) Token: 0x06008C04 RID: 35844 RVA: 0x002CD1C3 File Offset: 0x002CB3C3
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
		}
	}

	// Token: 0x170007CF RID: 1999
	// (get) Token: 0x06008C05 RID: 35845 RVA: 0x002CD1E2 File Offset: 0x002CB3E2
	// (set) Token: 0x06008C06 RID: 35846 RVA: 0x002CD1EA File Offset: 0x002CB3EA
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
		}
	}

	// Token: 0x06008C07 RID: 35847 RVA: 0x002CD209 File Offset: 0x002CB409
	private void SetDefaults()
	{
		this.m_BlurEnabled = false;
		this.m_BlurBlend = 1f;
		this.m_BlurAmount = 2f;
		this.m_BlurDesaturation = 0f;
		this.m_BlurBrightness = 1f;
	}

	// Token: 0x06008C08 RID: 35848 RVA: 0x002CD240 File Offset: 0x002CB440
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

	// Token: 0x06008C09 RID: 35849 RVA: 0x002CD26C File Offset: 0x002CB46C
	[ContextMenu("Freeze")]
	public void Freeze()
	{
		base.enabled = true;
		if (this.m_FrozenState)
		{
			return;
		}
		this.m_BlurEnabled = true;
		this.m_BlurBlend = 1f;
		this.m_BlurAmount = 1.5f;
		this.m_BlurDesaturation = 0f;
		this.m_BlurBrightness = 1f;
		this.m_CaptureFrozenImage = true;
		int width = this.m_LowQualityFreezeBufferSize;
		int height = this.m_LowQualityFreezeBufferSize;
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			width = this.m_LowQualityFreezeBufferSize;
			height = this.m_LowQualityFreezeBufferSize;
		}
		else
		{
			width = Screen.currentResolution.width;
			height = Screen.currentResolution.height;
		}
		this.m_FrozenScreenTexture = new Texture2D(width, height, TextureFormat.RGB24, false, true);
		this.m_FrozenScreenTexture.filterMode = FilterMode.Point;
		this.m_FrozenScreenTexture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x06008C0A RID: 35850 RVA: 0x002CD334 File Offset: 0x002CB534
	[ContextMenu("Unfreeze")]
	public void Unfreeze()
	{
		this.m_BlurEnabled = false;
		this.m_BlurBlend = 0f;
		this.m_FrozenState = false;
		if (this.m_FrozenScreenTexture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_FrozenScreenTexture);
			this.m_FrozenScreenTexture = null;
		}
		this.Disable();
	}

	// Token: 0x06008C0B RID: 35851 RVA: 0x002CD380 File Offset: 0x002CB580
	public bool isActive()
	{
		return base.enabled && this.m_FrozenState;
	}

	// Token: 0x06008C0C RID: 35852 RVA: 0x002CD398 File Offset: 0x002CB598
	private void Blur(RenderTexture source, RenderTexture destination, Material blurMat)
	{
		float num = (float)source.width;
		float num2 = (float)source.height;
		this.CalcTextureSize(source.width, source.height, 512, out num, out num2);
		RenderTexture temporary = RenderTexture.GetTemporary((int)num, (int)num2, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)(num * 0.5f), (int)(num2 * 0.5f), 0);
		temporary.wrapMode = TextureWrapMode.Clamp;
		temporary2.wrapMode = TextureWrapMode.Clamp;
		blurMat.SetFloat("_BlurOffset", 1f);
		blurMat.SetFloat("_FirstPass", 1f);
		Graphics.Blit(source, temporary, blurMat);
		blurMat.SetFloat("_BlurOffset", 0.4f);
		blurMat.SetFloat("_FirstPass", 0f);
		Graphics.Blit(temporary, temporary2, blurMat);
		blurMat.SetFloat("_BlurOffset", -0.2f);
		blurMat.SetFloat("_FirstPass", 0f);
		Graphics.Blit(temporary2, destination, blurMat);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06008C0D RID: 35853 RVA: 0x002CD484 File Offset: 0x002CB684
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

	// Token: 0x06008C0E RID: 35854 RVA: 0x002CD4B8 File Offset: 0x002CB6B8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (source == null || source.width == 0 || source.height == 0)
		{
			return;
		}
		if (this.m_CaptureFrozenImage && !this.m_FrozenState)
		{
			Material material = this.blurMaterial;
			material.SetFloat("_Brightness", this.m_BlurBrightness);
			if (this.m_BlurDesaturation > 0f)
			{
				material = this.blurDesatMaterial;
				material.SetFloat("_Desaturation", this.m_BlurDesaturation);
			}
			int num = this.m_LowQualityFreezeBufferSize;
			int num2 = this.m_LowQualityFreezeBufferSize;
			if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
			{
				num = this.m_LowQualityFreezeBufferSize;
				num2 = this.m_LowQualityFreezeBufferSize;
			}
			else
			{
				num = Screen.currentResolution.width;
				num2 = Screen.currentResolution.height;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			this.Blur(source, temporary, material);
			RenderTexture.active = temporary;
			this.m_FrozenScreenTexture.ReadPixels(new Rect(0f, 0f, (float)num, (float)num2), 0, 0, false);
			this.m_FrozenScreenTexture.Apply();
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
			this.m_CaptureFrozenImage = false;
			this.m_FrozenState = true;
			this.m_DeactivateFrameCount = 0;
		}
		if (this.m_FrozenState)
		{
			if (this.m_FrozenScreenTexture)
			{
				Material blendMaterial = this.blendMaterial;
				blendMaterial.SetFloat("_Amount", 1f);
				blendMaterial.SetTexture("_BlendTex", this.m_FrozenScreenTexture);
				if (QualitySettings.antiAliasing > 0)
				{
					blendMaterial.SetFloat("_Flip", 1f);
				}
				else
				{
					blendMaterial.SetFloat("_Flip", 0f);
				}
				if (destination != null)
				{
					destination.DiscardContents();
				}
				Graphics.Blit(source, destination, blendMaterial);
				this.m_DeactivateFrameCount = 0;
				return;
			}
			Debug.LogWarning("m_FrozenScreenTexture is null. FreezeFrame disabled");
			this.m_FrozenState = false;
		}
		if (this.m_FrozenState)
		{
			return;
		}
		Material blendMaterial2 = this.blendMaterial;
		blendMaterial2.SetFloat("_Amount", 0f);
		blendMaterial2.SetTexture("_BlendTex", null);
		Graphics.Blit(source, destination, blendMaterial2);
		if (this.m_DeactivateFrameCount > 2)
		{
			this.m_DeactivateFrameCount = 0;
			this.Disable();
			return;
		}
		this.m_DeactivateFrameCount++;
	}

	// Token: 0x040074E4 RID: 29924
	private const int NO_WORK_FRAMES_BEFORE_DEACTIVATE = 2;

	// Token: 0x040074E5 RID: 29925
	private const string BLUR_SHADER_NAME = "Custom/FullScreen/Blur";

	// Token: 0x040074E6 RID: 29926
	private const string BLUR_DESATURATION_SHADER_NAME = "Custom/FullScreen/DesaturationBlur";

	// Token: 0x040074E7 RID: 29927
	private const string BLEND_SHADER_NAME = "Custom/FullScreen/Blend";

	// Token: 0x040074E8 RID: 29928
	private const int BLUR_BUFFER_SIZE = 512;

	// Token: 0x040074E9 RID: 29929
	private const float BLUR_SECOND_PASS_REDUCTION = 0.5f;

	// Token: 0x040074EA RID: 29930
	private const float BLUR_PASS_1_OFFSET = 1f;

	// Token: 0x040074EB RID: 29931
	private const float BLUR_PASS_2_OFFSET = 0.4f;

	// Token: 0x040074EC RID: 29932
	private const float BLUR_PASS_3_OFFSET = -0.2f;

	// Token: 0x040074ED RID: 29933
	public Camera m_IgnoreCamera;

	// Token: 0x040074EE RID: 29934
	private int m_LowQualityFreezeBufferSize = 512;

	// Token: 0x040074EF RID: 29935
	private bool m_BlurEnabled;

	// Token: 0x040074F0 RID: 29936
	public float m_BlurBlend = 1f;

	// Token: 0x040074F1 RID: 29937
	private float m_BlurAmount = 2f;

	// Token: 0x040074F2 RID: 29938
	private float m_BlurDesaturation;

	// Token: 0x040074F3 RID: 29939
	private float m_BlurBrightness = 1f;

	// Token: 0x040074F4 RID: 29940
	private int m_DeactivateFrameCount;

	// Token: 0x040074F5 RID: 29941
	private Shader m_BlurShader;

	// Token: 0x040074F6 RID: 29942
	private Shader m_BlurDesaturationShader;

	// Token: 0x040074F7 RID: 29943
	private Shader m_BlendShader;

	// Token: 0x040074F8 RID: 29944
	private bool m_FrozenState;

	// Token: 0x040074F9 RID: 29945
	private bool m_CaptureFrozenImage;

	// Token: 0x040074FA RID: 29946
	private Texture2D m_FrozenScreenTexture;

	// Token: 0x040074FB RID: 29947
	private UniversalInputManager m_UniversalInputManager;

	// Token: 0x040074FC RID: 29948
	private Camera m_Camera;

	// Token: 0x040074FD RID: 29949
	private Material m_BlurMaterial;

	// Token: 0x040074FE RID: 29950
	private Material m_BlurDesatMaterial;

	// Token: 0x040074FF RID: 29951
	private Material m_BlendMaterial;
}
