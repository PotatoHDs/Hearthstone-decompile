using System;
using System.Collections.Generic;

// Token: 0x020007B1 RID: 1969
public class TagCombinatorHelper
{
	// Token: 0x06006D37 RID: 27959 RVA: 0x00233BA8 File Offset: 0x00231DA8
	public bool ForEachCombination(string[] inputTags, List<string> qualityTags, List<string> contentTags, Func<string, string, bool> action)
	{
		this.m_tempContentTags.Clear();
		this.m_tempQualityTags.Clear();
		UpdateUtils.ResizeListIfNeeded(this.m_tempQualityTags, qualityTags.Count);
		UpdateUtils.ResizeListIfNeeded(this.m_tempContentTags, contentTags.Count);
		foreach (string item in inputTags)
		{
			if (qualityTags.Contains(item))
			{
				this.m_tempQualityTags.Add(item);
			}
			if (contentTags.Contains(item))
			{
				this.m_tempContentTags.Add(item);
			}
		}
		bool flag = false;
		bool flag2 = true;
		foreach (string arg in this.m_tempQualityTags)
		{
			foreach (string arg2 in this.m_tempContentTags)
			{
				flag = true;
				if (!action(arg, arg2))
				{
					flag2 = false;
					break;
				}
			}
		}
		return flag && flag2;
	}

	// Token: 0x040057E4 RID: 22500
	private List<string> m_tempQualityTags = new List<string>();

	// Token: 0x040057E5 RID: 22501
	private List<string> m_tempContentTags = new List<string>();
}
