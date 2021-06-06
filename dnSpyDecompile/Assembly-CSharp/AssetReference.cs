using System;
using System.IO;
using UnityEngine;

// Token: 0x0200085C RID: 2140
public class AssetReference
{
	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x060073A7 RID: 29607 RVA: 0x002520C0 File Offset: 0x002502C0
	public string FileName
	{
		get
		{
			return this.m_fileName;
		}
	}

	// Token: 0x060073A8 RID: 29608 RVA: 0x000052CE File Offset: 0x000034CE
	public AssetReference()
	{
	}

	// Token: 0x060073A9 RID: 29609 RVA: 0x002520C8 File Offset: 0x002502C8
	public AssetReference(string fileName, string guid)
	{
		this.m_fileName = fileName;
		this.guid = guid;
	}

	// Token: 0x060073AA RID: 29610 RVA: 0x002520DE File Offset: 0x002502DE
	public AssetReference(string assetString)
	{
		this.FromAssetString(assetString);
	}

	// Token: 0x060073AB RID: 29611 RVA: 0x002520ED File Offset: 0x002502ED
	public override string ToString()
	{
		if (string.IsNullOrEmpty(this.guid))
		{
			return string.Empty;
		}
		return string.Format("{0}:{1}", this.m_fileName, this.guid);
	}

	// Token: 0x060073AC RID: 29612 RVA: 0x00252118 File Offset: 0x00250318
	public void FromAssetString(string assetString)
	{
		if (string.IsNullOrEmpty(assetString))
		{
			return;
		}
		string[] array = assetString.Split(new char[]
		{
			':'
		});
		if (array.Length == 0 || array.Length > 2)
		{
			Debug.LogErrorFormat("Incorrectly formatted asset: {0}", new object[]
			{
				assetString
			});
			return;
		}
		if (array.Length == 2)
		{
			this.m_fileName = array[0];
			this.guid = array[1];
			return;
		}
		Debug.LogWarningFormat("Possibly incorrectly formatted or asset string missing name: {0}", new object[]
		{
			assetString
		});
		this.m_fileName = "<UNKNOWN_FILE>";
		this.guid = array[array.Length - 1];
	}

	// Token: 0x060073AD RID: 29613 RVA: 0x002521A4 File Offset: 0x002503A4
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

	// Token: 0x060073AE RID: 29614 RVA: 0x002521D0 File Offset: 0x002503D0
	public string GetLegacyAssetName()
	{
		bool flag = false;
		if (this.m_cachedLegacyName == null || flag)
		{
			try
			{
				this.m_cachedLegacyName = Path.GetFileNameWithoutExtension(this.m_fileName);
			}
			catch (Exception)
			{
				Debug.LogErrorFormat("Failed to get path for assetRef={0}", new object[]
				{
					this
				});
				this.m_cachedLegacyName = "";
			}
		}
		return this.m_cachedLegacyName;
	}

	// Token: 0x060073AF RID: 29615 RVA: 0x00252238 File Offset: 0x00250438
	public static implicit operator AssetReference(string input)
	{
		return AssetReference.CreateFromAssetString(input);
	}

	// Token: 0x060073B0 RID: 29616 RVA: 0x00252240 File Offset: 0x00250440
	public static implicit operator string(AssetReference assetRef)
	{
		if (assetRef == null)
		{
			return null;
		}
		return assetRef.ToString();
	}

	// Token: 0x04005BD3 RID: 23507
	public string guid;

	// Token: 0x04005BD4 RID: 23508
	private string m_fileName;

	// Token: 0x04005BD5 RID: 23509
	private string m_cachedLegacyName;
}
