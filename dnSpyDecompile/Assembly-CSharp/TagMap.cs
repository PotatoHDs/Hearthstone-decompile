using System;
using System.Collections.Generic;

// Token: 0x0200092D RID: 2349
public class TagMap
{
	// Token: 0x060081A5 RID: 33189 RVA: 0x002A32AA File Offset: 0x002A14AA
	public TagMap()
	{
		this.m_values = new Map<int, int>();
	}

	// Token: 0x060081A6 RID: 33190 RVA: 0x002A32BD File Offset: 0x002A14BD
	public TagMap(int size)
	{
		this.m_values = new Map<int, int>(size);
	}

	// Token: 0x060081A7 RID: 33191 RVA: 0x002A32D1 File Offset: 0x002A14D1
	public void SetTag(int tag, int tagValue)
	{
		this.m_values[tag] = tagValue;
	}

	// Token: 0x060081A8 RID: 33192 RVA: 0x002A32E0 File Offset: 0x002A14E0
	public void SetTag(GAME_TAG tag, int tagValue)
	{
		this.SetTag((int)tag, tagValue);
	}

	// Token: 0x060081A9 RID: 33193 RVA: 0x002A32EA File Offset: 0x002A14EA
	public void SetTag<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		this.SetTag((int)tag, Convert.ToInt32(tagValue));
	}

	// Token: 0x060081AA RID: 33194 RVA: 0x002A3300 File Offset: 0x002A1500
	public void SetTags(Map<int, int> tagMap)
	{
		foreach (KeyValuePair<int, int> keyValuePair in tagMap)
		{
			this.SetTag(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x060081AB RID: 33195 RVA: 0x002A335C File Offset: 0x002A155C
	public void SetTags(Map<GAME_TAG, int> tagMap)
	{
		foreach (KeyValuePair<GAME_TAG, int> keyValuePair in tagMap)
		{
			this.SetTag(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x060081AC RID: 33196 RVA: 0x002A33B8 File Offset: 0x002A15B8
	public void SetTags(List<Network.Entity.Tag> tags)
	{
		foreach (Network.Entity.Tag tag in tags)
		{
			this.SetTag(tag.Name, tag.Value);
		}
	}

	// Token: 0x060081AD RID: 33197 RVA: 0x002A3414 File Offset: 0x002A1614
	public Map<int, int> GetMap()
	{
		return this.m_values;
	}

	// Token: 0x060081AE RID: 33198 RVA: 0x002A341C File Offset: 0x002A161C
	public int GetTag(int tag)
	{
		int result = 0;
		this.m_values.TryGetValue(tag, out result);
		return result;
	}

	// Token: 0x060081AF RID: 33199 RVA: 0x002A343C File Offset: 0x002A163C
	public TagEnum GetTag<TagEnum>(GAME_TAG enumTag)
	{
		int tag = Convert.ToInt32(enumTag);
		int tag2 = this.GetTag(tag);
		return (TagEnum)((object)Enum.ToObject(typeof(TagEnum), tag2));
	}

	// Token: 0x060081B0 RID: 33200 RVA: 0x002A3474 File Offset: 0x002A1674
	public int GetTag(GAME_TAG enumTag)
	{
		int tag = Convert.ToInt32(enumTag);
		return this.GetTag(tag);
	}

	// Token: 0x060081B1 RID: 33201 RVA: 0x002A3494 File Offset: 0x002A1694
	public bool HasTag(int tag)
	{
		int num = 0;
		return this.m_values.TryGetValue(tag, out num) && num > 0;
	}

	// Token: 0x060081B2 RID: 33202 RVA: 0x002A34B9 File Offset: 0x002A16B9
	public bool HasTag<TagEnum>(GAME_TAG tag)
	{
		return Convert.ToUInt32(this.GetTag<TagEnum>(tag)) > 0U;
	}

	// Token: 0x060081B3 RID: 33203 RVA: 0x002A34CF File Offset: 0x002A16CF
	public void Replace(TagMap tags)
	{
		this.Clear();
		this.SetTags(tags.m_values);
	}

	// Token: 0x060081B4 RID: 33204 RVA: 0x002A34E3 File Offset: 0x002A16E3
	public void Replace(List<Network.Entity.Tag> tags)
	{
		this.Clear();
		this.SetTags(tags);
	}

	// Token: 0x060081B5 RID: 33205 RVA: 0x002A34F2 File Offset: 0x002A16F2
	public void Clear()
	{
		this.m_values = new Map<int, int>();
	}

	// Token: 0x060081B6 RID: 33206 RVA: 0x002A3500 File Offset: 0x002A1700
	public TagDeltaList CreateDeltas(List<Network.Entity.Tag> comp)
	{
		TagDeltaList tagDeltaList = new TagDeltaList();
		foreach (Network.Entity.Tag tag in comp)
		{
			int name = tag.Name;
			int num = 0;
			this.m_values.TryGetValue(name, out num);
			int value = tag.Value;
			if (num != value)
			{
				tagDeltaList.Add(name, num, value);
			}
		}
		return tagDeltaList;
	}

	// Token: 0x060081B7 RID: 33207 RVA: 0x002A357C File Offset: 0x002A177C
	public bool TryGetValue(int tag, out int value)
	{
		return this.m_values.TryGetValue(tag, out value);
	}

	// Token: 0x04006CE1 RID: 27873
	private Map<int, int> m_values;
}
