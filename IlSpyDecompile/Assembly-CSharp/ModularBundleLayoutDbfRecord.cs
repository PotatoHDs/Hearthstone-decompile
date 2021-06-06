using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ModularBundleLayoutDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_modularBundleId;

	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_hiddenLicenseId;

	[SerializeField]
	private string m_accentTexture;

	[SerializeField]
	private string m_regions = "*";

	[SerializeField]
	private string m_abValue = "A";

	[SerializeField]
	private string m_prefab;

	[SerializeField]
	private DbfLocValue m_descriptionHeadline;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_orderSummaryName;

	[SerializeField]
	private bool m_animateAfterPurchase;

	[SerializeField]
	private double m_storeShakeDelay = 1.0;

	[DbfField("MODULAR_BUNDLE_ID")]
	public int ModularBundleId => m_modularBundleId;

	public ModularBundleDbfRecord ModularBundleRecord => GameDbf.ModularBundle.GetRecord(m_modularBundleId);

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("HIDDEN_LICENSE_ID")]
	public int HiddenLicenseId => m_hiddenLicenseId;

	public HiddenLicenseDbfRecord HiddenLicenseRecord => GameDbf.HiddenLicense.GetRecord(m_hiddenLicenseId);

	[DbfField("ACCENT_TEXTURE")]
	public string AccentTexture => m_accentTexture;

	[DbfField("REGIONS")]
	public string Regions => m_regions;

	[DbfField("AB_VALUE")]
	public string AbValue => m_abValue;

	[DbfField("PREFAB")]
	public string Prefab => m_prefab;

	[DbfField("DESCRIPTION_HEADLINE")]
	public DbfLocValue DescriptionHeadline => m_descriptionHeadline;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("ORDER_SUMMARY_NAME")]
	public DbfLocValue OrderSummaryName => m_orderSummaryName;

	[DbfField("ANIMATE_AFTER_PURCHASE")]
	public bool AnimateAfterPurchase => m_animateAfterPurchase;

	[DbfField("STORE_SHAKE_DELAY")]
	public double StoreShakeDelay => m_storeShakeDelay;

	public List<ModularBundleLayoutNodeDbfRecord> Nodes => GameDbf.ModularBundleLayoutNode.GetRecords((ModularBundleLayoutNodeDbfRecord r) => r.NodeLayoutId == base.ID);

	public void SetModularBundleId(int v)
	{
		m_modularBundleId = v;
	}

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetHiddenLicenseId(int v)
	{
		m_hiddenLicenseId = v;
	}

	public void SetAccentTexture(string v)
	{
		m_accentTexture = v;
	}

	public void SetRegions(string v)
	{
		m_regions = v;
	}

	public void SetAbValue(string v)
	{
		m_abValue = v;
	}

	public void SetPrefab(string v)
	{
		m_prefab = v;
	}

	public void SetDescriptionHeadline(DbfLocValue v)
	{
		m_descriptionHeadline = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION_HEADLINE");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetOrderSummaryName(DbfLocValue v)
	{
		m_orderSummaryName = v;
		v.SetDebugInfo(base.ID, "ORDER_SUMMARY_NAME");
	}

	public void SetAnimateAfterPurchase(bool v)
	{
		m_animateAfterPurchase = v;
	}

	public void SetStoreShakeDelay(double v)
	{
		m_storeShakeDelay = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"MODULAR_BUNDLE_ID" => m_modularBundleId, 
			"NOTE_DESC" => m_noteDesc, 
			"HIDDEN_LICENSE_ID" => m_hiddenLicenseId, 
			"ACCENT_TEXTURE" => m_accentTexture, 
			"REGIONS" => m_regions, 
			"AB_VALUE" => m_abValue, 
			"PREFAB" => m_prefab, 
			"DESCRIPTION_HEADLINE" => m_descriptionHeadline, 
			"DESCRIPTION" => m_description, 
			"ORDER_SUMMARY_NAME" => m_orderSummaryName, 
			"ANIMATE_AFTER_PURCHASE" => m_animateAfterPurchase, 
			"STORE_SHAKE_DELAY" => m_storeShakeDelay, 
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
		case "MODULAR_BUNDLE_ID":
			m_modularBundleId = (int)val;
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "HIDDEN_LICENSE_ID":
			m_hiddenLicenseId = (int)val;
			break;
		case "ACCENT_TEXTURE":
			m_accentTexture = (string)val;
			break;
		case "REGIONS":
			m_regions = (string)val;
			break;
		case "AB_VALUE":
			m_abValue = (string)val;
			break;
		case "PREFAB":
			m_prefab = (string)val;
			break;
		case "DESCRIPTION_HEADLINE":
			m_descriptionHeadline = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "ORDER_SUMMARY_NAME":
			m_orderSummaryName = (DbfLocValue)val;
			break;
		case "ANIMATE_AFTER_PURCHASE":
			m_animateAfterPurchase = (bool)val;
			break;
		case "STORE_SHAKE_DELAY":
			m_storeShakeDelay = (double)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"MODULAR_BUNDLE_ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"HIDDEN_LICENSE_ID" => typeof(int), 
			"ACCENT_TEXTURE" => typeof(string), 
			"REGIONS" => typeof(string), 
			"AB_VALUE" => typeof(string), 
			"PREFAB" => typeof(string), 
			"DESCRIPTION_HEADLINE" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"ORDER_SUMMARY_NAME" => typeof(DbfLocValue), 
			"ANIMATE_AFTER_PURCHASE" => typeof(bool), 
			"STORE_SHAKE_DELAY" => typeof(double), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleLayoutDbfRecords loadRecords = new LoadModularBundleLayoutDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleLayoutDbfAsset modularBundleLayoutDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleLayoutDbfAsset)) as ModularBundleLayoutDbfAsset;
		if (modularBundleLayoutDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ModularBundleLayoutDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < modularBundleLayoutDbfAsset.Records.Count; i++)
		{
			modularBundleLayoutDbfAsset.Records[i].StripUnusedLocales();
		}
		records = modularBundleLayoutDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_descriptionHeadline.StripUnusedLocales();
		m_description.StripUnusedLocales();
		m_orderSummaryName.StripUnusedLocales();
	}
}
