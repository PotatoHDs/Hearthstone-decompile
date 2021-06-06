using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ModularBundleDbfRecord : DbfRecord
{
	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private string m_selectorPrefab;

	[SerializeField]
	private int m_selectorPackAmountBanner;

	[SerializeField]
	private string m_layoutButtonSize = "small";

	[SerializeField]
	private string m_background;

	[SerializeField]
	private string m_playlist;

	[SerializeField]
	private string m_logoTexture;

	[SerializeField]
	private string m_logoTextureGlow;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private bool m_showAfterPurchase;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("SELECTOR_PREFAB")]
	public string SelectorPrefab => m_selectorPrefab;

	[DbfField("SELECTOR_PACK_AMOUNT_BANNER")]
	public int SelectorPackAmountBanner => m_selectorPackAmountBanner;

	[DbfField("LAYOUT_BUTTON_SIZE")]
	public string LayoutButtonSize => m_layoutButtonSize;

	[DbfField("BACKGROUND")]
	public string Background => m_background;

	[DbfField("PLAYLIST")]
	public string Playlist => m_playlist;

	[DbfField("LOGO_TEXTURE")]
	public string LogoTexture => m_logoTexture;

	[DbfField("LOGO_TEXTURE_GLOW")]
	public string LogoTextureGlow => m_logoTextureGlow;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("SHOW_AFTER_PURCHASE")]
	public bool ShowAfterPurchase => m_showAfterPurchase;

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetSelectorPrefab(string v)
	{
		m_selectorPrefab = v;
	}

	public void SetSelectorPackAmountBanner(int v)
	{
		m_selectorPackAmountBanner = v;
	}

	public void SetLayoutButtonSize(string v)
	{
		m_layoutButtonSize = v;
	}

	public void SetBackground(string v)
	{
		m_background = v;
	}

	public void SetPlaylist(string v)
	{
		m_playlist = v;
	}

	public void SetLogoTexture(string v)
	{
		m_logoTexture = v;
	}

	public void SetLogoTextureGlow(string v)
	{
		m_logoTextureGlow = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetShowAfterPurchase(bool v)
	{
		m_showAfterPurchase = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NAME" => m_name, 
			"SELECTOR_PREFAB" => m_selectorPrefab, 
			"SELECTOR_PACK_AMOUNT_BANNER" => m_selectorPackAmountBanner, 
			"LAYOUT_BUTTON_SIZE" => m_layoutButtonSize, 
			"BACKGROUND" => m_background, 
			"PLAYLIST" => m_playlist, 
			"LOGO_TEXTURE" => m_logoTexture, 
			"LOGO_TEXTURE_GLOW" => m_logoTextureGlow, 
			"SORT_ORDER" => m_sortOrder, 
			"SHOW_AFTER_PURCHASE" => m_showAfterPurchase, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "SELECTOR_PREFAB":
			m_selectorPrefab = (string)val;
			break;
		case "SELECTOR_PACK_AMOUNT_BANNER":
			m_selectorPackAmountBanner = (int)val;
			break;
		case "LAYOUT_BUTTON_SIZE":
			m_layoutButtonSize = (string)val;
			break;
		case "BACKGROUND":
			m_background = (string)val;
			break;
		case "PLAYLIST":
			m_playlist = (string)val;
			break;
		case "LOGO_TEXTURE":
			m_logoTexture = (string)val;
			break;
		case "LOGO_TEXTURE_GLOW":
			m_logoTextureGlow = (string)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "SHOW_AFTER_PURCHASE":
			m_showAfterPurchase = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"SELECTOR_PREFAB" => typeof(string), 
			"SELECTOR_PACK_AMOUNT_BANNER" => typeof(int), 
			"LAYOUT_BUTTON_SIZE" => typeof(string), 
			"BACKGROUND" => typeof(string), 
			"PLAYLIST" => typeof(string), 
			"LOGO_TEXTURE" => typeof(string), 
			"LOGO_TEXTURE_GLOW" => typeof(string), 
			"SORT_ORDER" => typeof(int), 
			"SHOW_AFTER_PURCHASE" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleDbfRecords loadRecords = new LoadModularBundleDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleDbfAsset modularBundleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleDbfAsset)) as ModularBundleDbfAsset;
		if (modularBundleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ModularBundleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < modularBundleDbfAsset.Records.Count; i++)
		{
			modularBundleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = modularBundleDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
	}
}
