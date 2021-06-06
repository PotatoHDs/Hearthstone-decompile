using System;
using System.IO;
using UnityEngine;

public class AssetReference
{
	public string guid;

	private string m_fileName;

	private string m_cachedLegacyName;

	public string FileName => m_fileName;

	public AssetReference()
	{
	}

	public AssetReference(string fileName, string guid)
	{
		m_fileName = fileName;
		this.guid = guid;
	}

	public AssetReference(string assetString)
	{
		FromAssetString(assetString);
	}

	public override string ToString()
	{
		if (string.IsNullOrEmpty(guid))
		{
			return string.Empty;
		}
		return $"{m_fileName}:{guid}";
	}

	public void FromAssetString(string assetString)
	{
		if (!string.IsNullOrEmpty(assetString))
		{
			string[] array = assetString.Split(':');
			if (array.Length == 0 || array.Length > 2)
			{
				Debug.LogErrorFormat("Incorrectly formatted asset: {0}", assetString);
				return;
			}
			if (array.Length == 2)
			{
				m_fileName = array[0];
				guid = array[1];
				return;
			}
			Debug.LogWarningFormat("Possibly incorrectly formatted or asset string missing name: {0}", assetString);
			m_fileName = "<UNKNOWN_FILE>";
			guid = array[array.Length - 1];
		}
	}

	public static AssetReference CreateFromAssetString(string assetString)
	{
		if (string.IsNullOrEmpty(assetString))
		{
			return null;
		}
		AssetReference assetReference = new AssetReference(assetString);
		if (assetReference.guid == null)
		{
			return null;
		}
		return assetReference;
	}

	public string GetLegacyAssetName()
	{
		bool flag = false;
		if (m_cachedLegacyName == null || flag)
		{
			try
			{
				m_cachedLegacyName = Path.GetFileNameWithoutExtension(m_fileName);
			}
			catch (Exception)
			{
				Debug.LogErrorFormat("Failed to get path for assetRef={0}", this);
				m_cachedLegacyName = "";
			}
		}
		return m_cachedLegacyName;
	}

	public static implicit operator AssetReference(string input)
	{
		return CreateFromAssetString(input);
	}

	public static implicit operator string(AssetReference assetRef)
	{
		return assetRef?.ToString();
	}
}
