using System;
using System.Collections.Generic;

public class TagMap
{
	private Map<int, int> m_values;

	public TagMap()
	{
		m_values = new Map<int, int>();
	}

	public TagMap(int size)
	{
		m_values = new Map<int, int>(size);
	}

	public void SetTag(int tag, int tagValue)
	{
		m_values[tag] = tagValue;
	}

	public void SetTag(GAME_TAG tag, int tagValue)
	{
		SetTag((int)tag, tagValue);
	}

	public void SetTag<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		SetTag((int)tag, Convert.ToInt32(tagValue));
	}

	public void SetTags(Map<int, int> tagMap)
	{
		foreach (KeyValuePair<int, int> item in tagMap)
		{
			SetTag(item.Key, item.Value);
		}
	}

	public void SetTags(Map<GAME_TAG, int> tagMap)
	{
		foreach (KeyValuePair<GAME_TAG, int> item in tagMap)
		{
			SetTag(item.Key, item.Value);
		}
	}

	public void SetTags(List<Network.Entity.Tag> tags)
	{
		foreach (Network.Entity.Tag tag in tags)
		{
			SetTag(tag.Name, tag.Value);
		}
	}

	public Map<int, int> GetMap()
	{
		return m_values;
	}

	public int GetTag(int tag)
	{
		int value = 0;
		m_values.TryGetValue(tag, out value);
		return value;
	}

	public TagEnum GetTag<TagEnum>(GAME_TAG enumTag)
	{
		int tag = Convert.ToInt32(enumTag);
		int tag2 = GetTag(tag);
		return (TagEnum)Enum.ToObject(typeof(TagEnum), tag2);
	}

	public int GetTag(GAME_TAG enumTag)
	{
		int tag = Convert.ToInt32(enumTag);
		return GetTag(tag);
	}

	public bool HasTag(int tag)
	{
		int value = 0;
		if (!m_values.TryGetValue(tag, out value))
		{
			return false;
		}
		return value > 0;
	}

	public bool HasTag<TagEnum>(GAME_TAG tag)
	{
		return Convert.ToUInt32(GetTag<TagEnum>(tag)) != 0;
	}

	public void Replace(TagMap tags)
	{
		Clear();
		SetTags(tags.m_values);
	}

	public void Replace(List<Network.Entity.Tag> tags)
	{
		Clear();
		SetTags(tags);
	}

	public void Clear()
	{
		m_values = new Map<int, int>();
	}

	public TagDeltaList CreateDeltas(List<Network.Entity.Tag> comp)
	{
		TagDeltaList tagDeltaList = new TagDeltaList();
		foreach (Network.Entity.Tag item in comp)
		{
			int name = item.Name;
			int value = 0;
			m_values.TryGetValue(name, out value);
			int value2 = item.Value;
			if (value != value2)
			{
				tagDeltaList.Add(name, value, value2);
			}
		}
		return tagDeltaList;
	}

	public bool TryGetValue(int tag, out int value)
	{
		return m_values.TryGetValue(tag, out value);
	}
}
