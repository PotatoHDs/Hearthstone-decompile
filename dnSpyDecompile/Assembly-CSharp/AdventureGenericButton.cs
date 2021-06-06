using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x0200003F RID: 63
[CustomEditClass]
public class AdventureGenericButton : PegUIElement
{
	// Token: 0x0600032B RID: 811 RVA: 0x00013EA4 File Offset: 0x000120A4
	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_portraitTexture);
		base.OnDestroy();
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00013EB7 File Offset: 0x000120B7
	public bool IsPortraitLoaded()
	{
		return this.m_PortraitLoaded;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00013EC0 File Offset: 0x000120C0
	public void SetDesaturate(bool desaturate)
	{
		if (!this.CheckValidMaterialProperties(this.m_MaterialProperties))
		{
			return;
		}
		if (!this.CheckValidMaterialProperties(this.m_BorderMaterialProperties))
		{
			return;
		}
		this.m_PortraitRenderer.GetMaterial(this.m_MaterialProperties.m_MaterialIndex).SetFloat("_Desaturate", desaturate ? 1f : 0f);
		this.m_BorderRenderer.GetMaterial(this.m_BorderMaterialProperties.m_MaterialIndex).SetFloat("_Desaturate", desaturate ? 1f : 0f);
		this.m_ButtonTextObject.TextColor = (desaturate ? this.m_DisabledTextColor : this.m_NormalTextColor);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00013F68 File Offset: 0x00012168
	public void SetContrast(float contrast)
	{
		if (!this.CheckValidMaterialProperties(this.m_MaterialProperties))
		{
			return;
		}
		if (!this.CheckValidMaterialProperties(this.m_BorderMaterialProperties))
		{
			return;
		}
		this.m_PortraitRenderer.GetMaterial(this.m_MaterialProperties.m_MaterialIndex).SetFloat("_Contrast", contrast);
		this.m_BorderRenderer.GetMaterial(this.m_BorderMaterialProperties.m_MaterialIndex).SetFloat("_Contrast", contrast);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00013FD5 File Offset: 0x000121D5
	public void SetButtonText(string str)
	{
		if (this.m_ButtonTextObject == null)
		{
			return;
		}
		this.m_ButtonTextObject.Text = str;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00013FF2 File Offset: 0x000121F2
	public void SetPortraitTexture(string textureAssetPath)
	{
		this.SetPortraitTexture(textureAssetPath, this.m_MaterialProperties.m_MaterialIndex, this.m_MaterialProperties.m_MaterialPropertyName);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00014014 File Offset: 0x00012214
	public void SetPortraitTexture(string textureAssetPath, int index, string mattexprop)
	{
		if (textureAssetPath == null || textureAssetPath.Length == 0)
		{
			return;
		}
		AdventureGenericButton.MaterialProperties materialProperties = new AdventureGenericButton.MaterialProperties
		{
			m_MaterialIndex = index,
			m_MaterialPropertyName = mattexprop
		};
		if (!this.CheckValidMaterialProperties(materialProperties))
		{
			return;
		}
		this.m_PortraitLoaded = false;
		AssetLoader.Get().LoadAsset<Texture>(textureAssetPath, new AssetHandleCallback<Texture>(this.ApplyPortraitTexture), materialProperties, AssetLoadingOptions.None);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00014071 File Offset: 0x00012271
	public void SetPortraitTiling(Vector2 tiling)
	{
		this.SetPortraitTiling(tiling, this.m_MaterialProperties.m_MaterialIndex, this.m_MaterialProperties.m_MaterialPropertyName);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00014090 File Offset: 0x00012290
	public void SetPortraitTiling(Vector2 tiling, int index, string mattexprop)
	{
		AdventureGenericButton.MaterialProperties materialProperties = new AdventureGenericButton.MaterialProperties
		{
			m_MaterialIndex = index,
			m_MaterialPropertyName = mattexprop
		};
		if (!this.CheckValidMaterialProperties(materialProperties))
		{
			return;
		}
		this.m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTextureScale(materialProperties.m_MaterialPropertyName, tiling);
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000140D8 File Offset: 0x000122D8
	public void SetPortraitOffset(Vector2 offset)
	{
		this.SetPortraitOffset(offset, this.m_MaterialProperties.m_MaterialIndex, this.m_MaterialProperties.m_MaterialPropertyName);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000140F8 File Offset: 0x000122F8
	public void SetPortraitOffset(Vector2 offset, int index, string mattexprop)
	{
		AdventureGenericButton.MaterialProperties materialProperties = new AdventureGenericButton.MaterialProperties
		{
			m_MaterialIndex = index,
			m_MaterialPropertyName = mattexprop
		};
		if (!this.CheckValidMaterialProperties(materialProperties))
		{
			return;
		}
		this.m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTextureOffset(materialProperties.m_MaterialPropertyName, offset);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00014140 File Offset: 0x00012340
	private void ApplyPortraitTexture(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object userdata)
	{
		try
		{
			this.m_PortraitLoaded = true;
			AdventureGenericButton.MaterialProperties materialProperties = userdata as AdventureGenericButton.MaterialProperties;
			if (!loadedTexture)
			{
				Debug.LogError(string.Format("Unable to load portrait texture {0}.", assetRef.ToString()), this);
			}
			else
			{
				AssetHandle.Set<Texture>(ref this.m_portraitTexture, loadedTexture);
				this.m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTexture(materialProperties.m_MaterialPropertyName, this.m_portraitTexture);
			}
		}
		finally
		{
			if (loadedTexture != null)
			{
				((IDisposable)loadedTexture).Dispose();
			}
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x000141CC File Offset: 0x000123CC
	private bool CheckValidMaterialProperties(AdventureGenericButton.MaterialProperties matprop)
	{
		if (this.m_PortraitRenderer == null)
		{
			Debug.LogError("No portrait mesh renderer set.");
			return false;
		}
		if (matprop.m_MaterialIndex >= this.m_PortraitRenderer.GetMaterials().Count)
		{
			Debug.LogError(string.Format("Unable to find material index {0}", matprop.m_MaterialIndex));
			return false;
		}
		return true;
	}

	// Token: 0x04000247 RID: 583
	private const string s_DefaultPortraitMaterialtextureAssetPath = "_MainTex";

	// Token: 0x04000248 RID: 584
	private const int s_DefaultPortraitMaterialIndex = 1;

	// Token: 0x04000249 RID: 585
	[CustomEditField(Sections = "Portrait Settings")]
	public MeshRenderer m_PortraitRenderer;

	// Token: 0x0400024A RID: 586
	[CustomEditField(Sections = "Portrait Settings")]
	public AdventureGenericButton.MaterialProperties m_MaterialProperties = new AdventureGenericButton.MaterialProperties();

	// Token: 0x0400024B RID: 587
	[CustomEditField(Sections = "Border Settings")]
	public MeshRenderer m_BorderRenderer;

	// Token: 0x0400024C RID: 588
	[CustomEditField(Sections = "Border Settings")]
	public AdventureGenericButton.MaterialProperties m_BorderMaterialProperties = new AdventureGenericButton.MaterialProperties();

	// Token: 0x0400024D RID: 589
	[CustomEditField(Sections = "Text Settings")]
	public UberText m_ButtonTextObject;

	// Token: 0x0400024E RID: 590
	[CustomEditField(Sections = "Text Settings")]
	public Color m_NormalTextColor;

	// Token: 0x0400024F RID: 591
	public Color m_DisabledTextColor;

	// Token: 0x04000250 RID: 592
	private bool m_PortraitLoaded = true;

	// Token: 0x04000251 RID: 593
	private AssetHandle<Texture> m_portraitTexture;

	// Token: 0x020012ED RID: 4845
	[Serializable]
	public class MaterialProperties
	{
		// Token: 0x0400A51B RID: 42267
		public int m_MaterialIndex = 1;

		// Token: 0x0400A51C RID: 42268
		public string m_MaterialPropertyName = "_MainTex";
	}
}
