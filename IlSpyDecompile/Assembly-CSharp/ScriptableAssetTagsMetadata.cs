using System;
using System.Collections.Generic;
using Hearthstone.Core.Streaming;
using UnityEngine;

public class ScriptableAssetTagsMetadata : ScriptableObject
{
	[SerializeField]
	private List<string> m_tags = new List<string>();

	[SerializeField]
	private List<string> m_tagGroups = new List<string>();

	[SerializeField]
	private List<int> m_tagIdToTagGroupId = new List<int>();

	[SerializeField]
	private List<int> m_overrideId = new List<int>();

	public void Clear()
	{
		m_tags.Clear();
		m_tagGroups.Clear();
		m_tagIdToTagGroupId.Clear();
		m_overrideId.Clear();
	}

	public void AddTag(string tag, string tagGroup, string overrideTag)
	{
		if (!m_tags.Contains(tag))
		{
			int num = m_tagGroups.IndexOf(tagGroup);
			if (num == -1)
			{
				m_tagGroups.Add(tagGroup);
				num = m_tagGroups.Count - 1;
			}
			m_tags.Add(tag);
			m_tagIdToTagGroupId.Add(num);
			int num2 = m_tags.IndexOf(overrideTag);
			if (num2 == -1)
			{
				throw new Exception($"The override tag '{overrideTag}' must added before tag '{tag}'.");
			}
			m_overrideId.Add(num2);
		}
	}

	public string[] GetTagGroups()
	{
		return m_tagGroups.ToArray();
	}

	public string[] GetTagsInTagGroup(string tagGroup)
	{
		return GetTagsInTagGroup(m_tagGroups.IndexOf(tagGroup));
	}

	public string[] GetTagsInTagGroup(int tagGroupId)
	{
		if (tagGroupId == -1)
		{
			return new string[0];
		}
		List<string> list = new List<string>();
		for (int i = 0; i < m_tagIdToTagGroupId.Count; i++)
		{
			if (tagGroupId == m_tagIdToTagGroupId[i])
			{
				list.Add(m_tags[i]);
			}
		}
		return list.ToArray();
	}

	public string ConvertToOverrideTag(string tag, string tagGroup)
	{
		return ConvertToOverrideTag(tag, m_tagGroups.IndexOf(tagGroup));
	}

	public string ConvertToOverrideTag(string tag, int tagGroupId)
	{
		if (tagGroupId == -1)
		{
			return tag;
		}
		int num = m_tags.IndexOf(tag);
		if (num == -1)
		{
			return tag;
		}
		return m_tags[m_overrideId[num]];
	}

	public string GetTagGroupForTag(string tag)
	{
		int num = m_tags.IndexOf(tag);
		if (num >= 0)
		{
			return m_tagGroups[m_tagIdToTagGroupId[num]];
		}
		return string.Empty;
	}

	public string[] GetTagsFromAssetBundle(string assetBundleName)
	{
		List<string> list = new List<string>();
		int tagGroupId = m_tagGroups.IndexOf(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
		string[] tagsInTagGroup = GetTagsInTagGroup(tagGroupId);
		foreach (string text in tagsInTagGroup)
		{
			if (assetBundleName.IndexOf(text) >= 0)
			{
				list.Add(ConvertToOverrideTag(text, tagGroupId));
			}
		}
		tagsInTagGroup = GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content));
		foreach (string text2 in tagsInTagGroup)
		{
			if (assetBundleName.IndexOf(text2) >= 0)
			{
				list.Add(ConvertToOverrideTag(text2, tagGroupId));
			}
		}
		return list.ToArray();
	}

	public string[] GetAllTags(string tagGroup, bool excludeOverridenTag)
	{
		List<string> list = new List<string>();
		string[] tagsInTagGroup = GetTagsInTagGroup(tagGroup);
		foreach (string item in tagsInTagGroup)
		{
			if (!excludeOverridenTag || m_overrideId[m_tags.IndexOf(item)] == m_tags.IndexOf(item))
			{
				list.Add(item);
			}
		}
		return list.ToArray();
	}
}
