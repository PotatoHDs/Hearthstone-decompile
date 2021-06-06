using System;
using UnityEngine;

// Token: 0x02000A84 RID: 2692
[ExecuteAlways]
public class ScreenEffectsRender : MonoBehaviour
{
	// Token: 0x1700082D RID: 2093
	// (get) Token: 0x06009042 RID: 36930 RVA: 0x002ED518 File Offset: 0x002EB718
	protected Material GlowMaterial
	{
		get
		{
			if (this.m_GlowMaterial == null)
			{
				if (this.m_GlowShader == null)
				{
					this.m_GlowShader = Shader.Find("Hidden/ScreenEffectsGlow");
					if (!this.m_GlowShader)
					{
						Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectsGlow");
					}
				}
				this.m_GlowMaterial = new Material(this.m_GlowShader);
				SceneUtils.SetHideFlags(this.m_GlowMaterial, HideFlags.HideAndDontSave);
			}
			return this.m_GlowMaterial;
		}
	}

	// Token: 0x1700082E RID: 2094
	// (get) Token: 0x06009043 RID: 36931 RVA: 0x002ED58C File Offset: 0x002EB78C
	protected Material DistortionMaterial
	{
		get
		{
			if (this.m_DistortionMaterial == null)
			{
				if (this.m_DistortionShader == null)
				{
					this.m_DistortionShader = Shader.Find("Hidden/ScreenEffectDistortion");
					if (!this.m_DistortionShader)
					{
						Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectDistortion");
					}
				}
				this.m_DistortionMaterial = new Material(this.m_DistortionShader);
				SceneUtils.SetHideFlags(this.m_DistortionMaterial, HideFlags.HideAndDontSave);
			}
			return this.m_DistortionMaterial;
		}
	}

	// Token: 0x06009044 RID: 36932 RVA: 0x002ED600 File Offset: 0x002EB800
	private void Awake()
	{
		if (ScreenEffectsMgr.Get() == null)
		{
			base.enabled = false;
		}
		if (this.m_MaskShader == null)
		{
			this.m_MaskShader = Shader.Find("Hidden/ScreenEffectsMask");
			if (!this.m_MaskShader)
			{
				Debug.LogError("Failed to load ScreenEffectsRender Shader: Hidden/ScreenEffectsMask");
			}
		}
	}

	// Token: 0x06009045 RID: 36933 RVA: 0x002ED650 File Offset: 0x002EB850
	private void Update()
	{
		this.RenderEffectsObjects();
	}

	// Token: 0x06009046 RID: 36934 RVA: 0x002ED658 File Offset: 0x002EB858
	private void OnDisable()
	{
		UnityEngine.Object.DestroyImmediate(this.GlowMaterial);
		UnityEngine.Object.DestroyImmediate(this.DistortionMaterial);
		if (this.m_MaskRenderTexture != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_MaskRenderTexture);
			this.m_MaskRenderTexture = null;
		}
		if (this.m_GlowMaterial != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_GlowMaterial);
		}
		if (this.m_DistortionMaterial != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_DistortionMaterial);
		}
	}

	// Token: 0x06009047 RID: 36935 RVA: 0x002ED6D2 File Offset: 0x002EB8D2
	private void OnEnable()
	{
		this.SetupEffect();
	}

	// Token: 0x06009048 RID: 36936 RVA: 0x002ED6DA File Offset: 0x002EB8DA
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.RenderGlow(source, destination);
	}

	// Token: 0x06009049 RID: 36937 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void SetupEffect()
	{
	}

	// Token: 0x0600904A RID: 36938 RVA: 0x002ED6E4 File Offset: 0x002EB8E4
	private void RenderEffectsObjects()
	{
		if (this.m_EffectsObjectsCamera == null)
		{
			base.enabled = false;
			return;
		}
		float num = (float)Screen.width / (float)Screen.height;
		int num2 = (int)(512f * num);
		int num3 = 512;
		if (num2 != this.m_previousWidth || num3 != this.m_previousHeight)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_MaskRenderTexture);
			this.m_MaskRenderTexture = null;
		}
		if (this.m_MaskRenderTexture == null)
		{
			this.m_MaskRenderTexture = RenderTextureTracker.Get().CreateNewTexture(num2, num3, 32, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
			this.m_MaskRenderTexture.filterMode = FilterMode.Bilinear;
			this.m_previousWidth = num2;
			this.m_previousHeight = num3;
		}
		if (this.m_EffectsObjectsCamera.targetTexture == null && this.m_MaskRenderTexture != null)
		{
			this.m_EffectsObjectsCamera.targetTexture = this.m_MaskRenderTexture;
		}
		this.m_MaskRenderTexture.DiscardContents();
		this.m_EffectsObjectsCamera.RenderWithShader(this.m_MaskShader, "RenderType");
	}

	// Token: 0x0600904B RID: 36939 RVA: 0x002ED7E0 File Offset: 0x002EB9E0
	private void RenderGlow(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_MaskRenderTexture)
		{
			Graphics.Blit(source, destination);
			return;
		}
		int width = this.m_MaskRenderTexture.width;
		int height = this.m_MaskRenderTexture.height;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
		temporary.filterMode = FilterMode.Bilinear;
		this.GlowMaterial.SetFloat("_BlurOffset", 1f);
		Graphics.Blit(this.m_MaskRenderTexture, temporary, this.GlowMaterial, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0, source.format);
		temporary2.filterMode = FilterMode.Bilinear;
		this.GlowMaterial.SetFloat("_BlurOffset", 2f);
		Graphics.Blit(temporary, temporary2, this.GlowMaterial, 0);
		this.GlowMaterial.SetFloat("_BlurOffset", 3f);
		temporary.DiscardContents();
		Graphics.Blit(temporary2, temporary, this.GlowMaterial, 0);
		this.GlowMaterial.SetFloat("_BlurOffset", 5f);
		temporary2.DiscardContents();
		Graphics.Blit(temporary, temporary2, this.GlowMaterial, 0);
		this.GlowMaterial.SetTexture("_BlurTex", temporary2);
		if (!this.m_Debug)
		{
			Graphics.Blit(source, destination, this.GlowMaterial, 1);
		}
		else
		{
			Graphics.Blit(source, destination, this.GlowMaterial, 2);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x0600904C RID: 36940 RVA: 0x002ED924 File Offset: 0x002EBB24
	private void SetupDistortion()
	{
		if (this.m_EffectsTexture == null)
		{
			return;
		}
		this.DistortionMaterial.SetTexture("_EffectTex", this.m_EffectsTexture);
	}

	// Token: 0x0600904D RID: 36941 RVA: 0x002ED94B File Offset: 0x002EBB4B
	private Material DistortionMaterialRender(RenderTexture source)
	{
		return this.DistortionMaterial;
	}

	// Token: 0x0600904E RID: 36942 RVA: 0x002ED953 File Offset: 0x002EBB53
	private void RenderDistortion(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, this.DistortionMaterialRender(source));
	}

	// Token: 0x04007921 RID: 31009
	private const int GLOW_RANDER_BUFFER_RESOLUTION = 512;

	// Token: 0x04007922 RID: 31010
	private const string BLOOM_SHADER_NAME = "Hidden/ScreenEffectsGlow";

	// Token: 0x04007923 RID: 31011
	private const string DISTORTION_SHADER_NAME = "Hidden/ScreenEffectDistortion";

	// Token: 0x04007924 RID: 31012
	private const string GLOW_MASK_SHADER = "Hidden/ScreenEffectsMask";

	// Token: 0x04007925 RID: 31013
	[HideInInspector]
	public RenderTexture m_EffectsTexture;

	// Token: 0x04007926 RID: 31014
	[HideInInspector]
	public Camera m_EffectsObjectsCamera;

	// Token: 0x04007927 RID: 31015
	public bool m_Debug;

	// Token: 0x04007928 RID: 31016
	private int m_width;

	// Token: 0x04007929 RID: 31017
	private int m_height;

	// Token: 0x0400792A RID: 31018
	private int m_previousWidth;

	// Token: 0x0400792B RID: 31019
	private int m_previousHeight;

	// Token: 0x0400792C RID: 31020
	private RenderTexture m_MaskRenderTexture;

	// Token: 0x0400792D RID: 31021
	private Shader m_MaskShader;

	// Token: 0x0400792E RID: 31022
	private Shader m_GlowShader;

	// Token: 0x0400792F RID: 31023
	private Material m_GlowMaterial;

	// Token: 0x04007930 RID: 31024
	private Shader m_DistortionShader;

	// Token: 0x04007931 RID: 31025
	private Material m_DistortionMaterial;
}
