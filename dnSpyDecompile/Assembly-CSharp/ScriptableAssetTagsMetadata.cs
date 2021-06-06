using System;
using System.Collections.Generic;
using Hearthstone.Core.Streaming;
using UnityEngine;

// Token: 0x0200090B RID: 2315
public class ScriptableAssetTagsMetadata : ScriptableObject
{
	// Token: 0x060080D6 RID: 32982 RVA: 0x0029DA33 File Offset: 0x0029BC33
	public void Clear()
	{
		this.m_tags.Clear();
		this.m_tagGroups.Clear();
		this.m_tagIdToTagGroupId.Clear();
		this.m_overrideId.Clear();
	}

	// Token: 0x060080D7 RID: 32983 RVA: 0x0029DA64 File Offset: 0x0029BC64
	public void AddTag(string tag, string tagGroup, string overrideTag)
	{
		if (this.m_tags.Contains(tag))
		{
			return;
		}
		int num = this.m_tagGroups.IndexOf(tagGroup);
		if (num == -1)
		{
			this.m_tagGroups.Add(tagGroup);
			num = this.m_tagGroups.Count - 1;
		}
		this.m_tags.Add(tag);
		this.m_tagIdToTagGroupId.Add(num);
		int num2 = this.m_tags.IndexOf(overrideTag);
		if (num2 == -1)
		{
			throw new Exception(string.Format("The override tag '{0}' must added before tag '{1}'.", overrideTag, tag));
		}
		this.m_overrideId.Add(num2);
	}

	// Token: 0x060080D8 RID: 32984 RVA: 0x0029DAF2 File Offset: 0x0029BCF2
	public string[] GetTagGroups()
	{
		return this.m_tagGroups.ToArray();
	}

	// Token: 0x060080D9 RID: 32985 RVA: 0x0029DAFF File Offset: 0x0029BCFF
	public string[] GetTagsInTagGroup(string tagGroup)
	{
		return this.GetTagsInTagGroup(this.m_tagGroups.IndexOf(tagGroup));
	}

	// Token: 0x060080DA RID: 32986 RVA: 0x0029DB14 File Offset: 0x0029BD14
	public string[] GetTagsInTagGroup(int tagGroupId)
	{
		if (tagGroupId == -1)
		{
			return new string[0];
		}
		List<string> list = new List<string>();
		for (int i = 0; i < this.m_tagIdToTagGroupId.Count; i++)
		{
			if (tagGroupId == this.m_tagIdToTagGroupId[i])
			{
				list.Add(this.m_tags[i]);
			}
		}
		return list.ToArray();
	}

	// Token: 0x060080DB RID: 32987 RVA: 0x0029DB6F File Offset: 0x0029BD6F
	public string ConvertToOverrideTag(string tag, string tagGroup)
	{
		return this.ConvertToOverrideTag(tag, this.m_tagGroups.IndexOf(tagGroup));
	}

	// Token: 0x060080DC RID: 32988 RVA: 0x0029DB84 File Offset: 0x0029BD84
	public string ConvertToOverrideTag(string tag, int tagGroupId)
	{
		if (tagGroupId == -1)
		{
			return tag;
		}
		int num = this.m_tags.IndexOf(tag);
		if (num == -1)
		{
			return tag;
		}
		return this.m_tags[this.m_overrideId[num]];
	}

	// Token: 0x060080DD RID: 32989 RVA: 0x0029DBC4 File Offset: 0x0029BDC4
	public string GetTagGroupForTag(string tag)
	{
		int num = this.m_tags.IndexOf(tag);
		if (num >= 0)
		{
			return this.m_tagGroups[this.m_tagIdToTagGroupId[num]];
		}
		return string.Empty;
	}

	// Token: 0x060080DE RID: 32990 RVA: 0x0029DC00 File Offset: 0x0029BE00
	public string[] GetTagsFromAssetBundle(string assetBundleName)
	{
		List<string> list = new List<string>();
		int tagGroupId = this.m_tagGroups.IndexOf(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
		foreach (string text in this.GetTagsInTagGroup(tagGroupId))
		{
			if (assetBundleName.IndexOf(text) >= 0)
			{
				list.Add(this.ConvertToOverrideTag(text, tagGroupId));
			}
		}
		foreach (string text2 in this.GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content)))
		{
			if (assetBundleName.IndexOf(text2) >= 0)
			{
				list.Add(this.ConvertToOverrideTag(text2, tagGroupId));
			}
		}
		return list.ToArray();
	}

	// Token: 0x060080DF RID: 32991 RVA: 0x0029DC9C File Offset: 0x0029BE9C
	public string[] GetAllTags(string tagGroup, bool excludeOverridenTag)
	{
		List<string> list = new List<string>();
		foreach (string item in this.GetTagsInTagGroup(tagGroup))
		{
			if (!excludeOverridenTag || this.m_overrideId[this.m_tags.IndexOf(item)] == this.m_tags.IndexOf(item))
			{
				list.Add(item);
			}
		}
		return list.ToArray();
	}

	// Token: 0x04006996 RID: 27030
	[SerializeField]
	private List<string> m_tags = new List<string>();

	// Token: 0x04006997 RID: 27031
	[SerializeField]
	private List<string> m_tagGroups = new List<string>();

	// Token: 0x04006998 RID: 27032
	[SerializeField]
	private List<int> m_tagIdToTagGroupId = new List<int>();

	// Token: 0x04006999 RID: 27033
	[SerializeField]
	private List<int> m_overrideId = new List<int>();
}
