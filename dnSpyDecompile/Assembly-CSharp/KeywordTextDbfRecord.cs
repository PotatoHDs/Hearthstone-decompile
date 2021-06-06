using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001FF RID: 511
[Serializable]
public class KeywordTextDbfRecord : DbfRecord
{
	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0009262A File Offset: 0x0009082A
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00092632 File Offset: 0x00090832
	[DbfField("TAG")]
	public int Tag
	{
		get
		{
			return this.m_tag;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0009263A File Offset: 0x0009083A
	[DbfField("NAME")]
	public string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00092642 File Offset: 0x00090842
	[DbfField("TEXT")]
	public string Text
	{
		get
		{
			return this.m_text;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0009264A File Offset: 0x0009084A
	[DbfField("REF_TEXT")]
	public string RefText
	{
		get
		{
			return this.m_refText;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00092652 File Offset: 0x00090852
	[DbfField("COLLECTION_TEXT")]
	public string CollectionText
	{
		get
		{
			return this.m_collectionText;
		}
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x0009265A File Offset: 0x0009085A
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x00092663 File Offset: 0x00090863
	public void SetTag(int v)
	{
		this.m_tag = v;
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x0009266C File Offset: 0x0009086C
	public void SetName(string v)
	{
		this.m_name = v;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x00092675 File Offset: 0x00090875
	public void SetText(string v)
	{
		this.m_text = v;
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x0009267E File Offset: 0x0009087E
	public void SetRefText(string v)
	{
		this.m_refText = v;
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x00092687 File Offset: 0x00090887
	public void SetCollectionText(string v)
	{
		this.m_collectionText = v;
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x00092690 File Offset: 0x00090890
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num != 301355425U)
			{
				if (num != 406049971U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return this.m_name;
						}
					}
				}
				else if (name == "TAG")
				{
					return this.m_tag;
				}
			}
			else if (name == "COLLECTION_TEXT")
			{
				return this.m_collectionText;
			}
		}
		else if (num <= 2163098750U)
		{
			if (num != 1458105184U)
			{
				if (num == 2163098750U)
				{
					if (name == "TEXT")
					{
						return this.m_text;
					}
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num != 3022554311U)
		{
			if (num == 3417487296U)
			{
				if (name == "REF_TEXT")
				{
					return this.m_refText;
				}
			}
		}
		else if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		return null;
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x000927AC File Offset: 0x000909AC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num != 301355425U)
			{
				if (num != 406049971U)
				{
					if (num != 1387956774U)
					{
						return;
					}
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (string)val;
					return;
				}
				else
				{
					if (!(name == "TAG"))
					{
						return;
					}
					this.m_tag = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "COLLECTION_TEXT"))
				{
					return;
				}
				this.m_collectionText = (string)val;
				return;
			}
		}
		else if (num <= 2163098750U)
		{
			if (num != 1458105184U)
			{
				if (num != 2163098750U)
				{
					return;
				}
				if (!(name == "TEXT"))
				{
					return;
				}
				this.m_text = (string)val;
				return;
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num != 3022554311U)
		{
			if (num != 3417487296U)
			{
				return;
			}
			if (!(name == "REF_TEXT"))
			{
				return;
			}
			this.m_refText = (string)val;
			return;
		}
		else
		{
			if (!(name == "NOTE_DESC"))
			{
				return;
			}
			this.m_noteDesc = (string)val;
			return;
		}
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x000928CC File Offset: 0x00090ACC
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1387956774U)
		{
			if (num != 301355425U)
			{
				if (num != 406049971U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "TAG")
				{
					return typeof(int);
				}
			}
			else if (name == "COLLECTION_TEXT")
			{
				return typeof(string);
			}
		}
		else if (num <= 2163098750U)
		{
			if (num != 1458105184U)
			{
				if (num == 2163098750U)
				{
					if (name == "TEXT")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3022554311U)
		{
			if (num == 3417487296U)
			{
				if (name == "REF_TEXT")
				{
					return typeof(string);
				}
			}
		}
		else if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x000929FA File Offset: 0x00090BFA
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadKeywordTextDbfRecords loadRecords = new LoadKeywordTextDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x00092A10 File Offset: 0x00090C10
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		KeywordTextDbfAsset keywordTextDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(KeywordTextDbfAsset)) as KeywordTextDbfAsset;
		if (keywordTextDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("KeywordTextDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < keywordTextDbfAsset.Records.Count; i++)
		{
			keywordTextDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (keywordTextDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010DC RID: 4316
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x040010DD RID: 4317
	[SerializeField]
	private int m_tag;

	// Token: 0x040010DE RID: 4318
	[SerializeField]
	private string m_name;

	// Token: 0x040010DF RID: 4319
	[SerializeField]
	private string m_text;

	// Token: 0x040010E0 RID: 4320
	[SerializeField]
	private string m_refText;

	// Token: 0x040010E1 RID: 4321
	[SerializeField]
	private string m_collectionText;
}
