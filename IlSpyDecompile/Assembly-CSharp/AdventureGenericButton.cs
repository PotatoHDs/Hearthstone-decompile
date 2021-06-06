using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class AdventureGenericButton : PegUIElement
{
	[Serializable]
	public class MaterialProperties
	{
		public int m_MaterialIndex = 1;

		public string m_MaterialPropertyName = "_MainTex";
	}

	private const string s_DefaultPortraitMaterialtextureAssetPath = "_MainTex";

	private const int s_DefaultPortraitMaterialIndex = 1;

	[CustomEditField(Sections = "Portrait Settings")]
	public MeshRenderer m_PortraitRenderer;

	[CustomEditField(Sections = "Portrait Settings")]
	public MaterialProperties m_MaterialProperties = new MaterialProperties();

	[CustomEditField(Sections = "Border Settings")]
	public MeshRenderer m_BorderRenderer;

	[CustomEditField(Sections = "Border Settings")]
	public MaterialProperties m_BorderMaterialProperties = new MaterialProperties();

	[CustomEditField(Sections = "Text Settings")]
	public UberText m_ButtonTextObject;

	[CustomEditField(Sections = "Text Settings")]
	public Color m_NormalTextColor;

	public Color m_DisabledTextColor;

	private bool m_PortraitLoaded = true;

	private AssetHandle<Texture> m_portraitTexture;

	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_portraitTexture);
		base.OnDestroy();
	}

	public bool IsPortraitLoaded()
	{
		return m_PortraitLoaded;
	}

	public void SetDesaturate(bool desaturate)
	{
		if (CheckValidMaterialProperties(m_MaterialProperties) && CheckValidMaterialProperties(m_BorderMaterialProperties))
		{
			m_PortraitRenderer.GetMaterial(m_MaterialProperties.m_MaterialIndex).SetFloat("_Desaturate", desaturate ? 1f : 0f);
			m_BorderRenderer.GetMaterial(m_BorderMaterialProperties.m_MaterialIndex).SetFloat("_Desaturate", desaturate ? 1f : 0f);
			m_ButtonTextObject.TextColor = (desaturate ? m_DisabledTextColor : m_NormalTextColor);
		}
	}

	public void SetContrast(float contrast)
	{
		if (CheckValidMaterialProperties(m_MaterialProperties) && CheckValidMaterialProperties(m_BorderMaterialProperties))
		{
			m_PortraitRenderer.GetMaterial(m_MaterialProperties.m_MaterialIndex).SetFloat("_Contrast", contrast);
			m_BorderRenderer.GetMaterial(m_BorderMaterialProperties.m_MaterialIndex).SetFloat("_Contrast", contrast);
		}
	}

	public void SetButtonText(string str)
	{
		if (!(m_ButtonTextObject == null))
		{
			m_ButtonTextObject.Text = str;
		}
	}

	public void SetPortraitTexture(string textureAssetPath)
	{
		SetPortraitTexture(textureAssetPath, m_MaterialProperties.m_MaterialIndex, m_MaterialProperties.m_MaterialPropertyName);
	}

	public void SetPortraitTexture(string textureAssetPath, int index, string mattexprop)
	{
		if (textureAssetPath != null && textureAssetPath.Length != 0)
		{
			MaterialProperties materialProperties = new MaterialProperties
			{
				m_MaterialIndex = index,
				m_MaterialPropertyName = mattexprop
			};
			if (CheckValidMaterialProperties(materialProperties))
			{
				m_PortraitLoaded = false;
				AssetLoader.Get().LoadAsset<Texture>(textureAssetPath, ApplyPortraitTexture, materialProperties);
			}
		}
	}

	public void SetPortraitTiling(Vector2 tiling)
	{
		SetPortraitTiling(tiling, m_MaterialProperties.m_MaterialIndex, m_MaterialProperties.m_MaterialPropertyName);
	}

	public void SetPortraitTiling(Vector2 tiling, int index, string mattexprop)
	{
		MaterialProperties materialProperties = new MaterialProperties
		{
			m_MaterialIndex = index,
			m_MaterialPropertyName = mattexprop
		};
		if (CheckValidMaterialProperties(materialProperties))
		{
			m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTextureScale(materialProperties.m_MaterialPropertyName, tiling);
		}
	}

	public void SetPortraitOffset(Vector2 offset)
	{
		SetPortraitOffset(offset, m_MaterialProperties.m_MaterialIndex, m_MaterialProperties.m_MaterialPropertyName);
	}

	public void SetPortraitOffset(Vector2 offset, int index, string mattexprop)
	{
		MaterialProperties materialProperties = new MaterialProperties
		{
			m_MaterialIndex = index,
			m_MaterialPropertyName = mattexprop
		};
		if (CheckValidMaterialProperties(materialProperties))
		{
			m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTextureOffset(materialProperties.m_MaterialPropertyName, offset);
		}
	}

	private void ApplyPortraitTexture(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object userdata)
	{
		using (loadedTexture)
		{
			m_PortraitLoaded = true;
			MaterialProperties materialProperties = userdata as MaterialProperties;
			if (!loadedTexture)
			{
				Debug.LogError($"Unable to load portrait texture {assetRef.ToString()}.", this);
				return;
			}
			AssetHandle.Set(ref m_portraitTexture, loadedTexture);
			m_PortraitRenderer.GetMaterial(materialProperties.m_MaterialIndex).SetTexture(materialProperties.m_MaterialPropertyName, m_portraitTexture);
		}
	}

	private bool CheckValidMaterialProperties(MaterialProperties matprop)
	{
		if (m_PortraitRenderer == null)
		{
			Debug.LogError("No portrait mesh renderer set.");
			return false;
		}
		if (matprop.m_MaterialIndex >= m_PortraitRenderer.GetMaterials().Count)
		{
			Debug.LogError($"Unable to find material index {matprop.m_MaterialIndex}");
			return false;
		}
		return true;
	}
}
