using System;
using System.Collections.Generic;

public class TagCombinatorHelper
{
	private List<string> m_tempQualityTags = new List<string>();

	private List<string> m_tempContentTags = new List<string>();

	public bool ForEachCombination(string[] inputTags, List<string> qualityTags, List<string> contentTags, Func<string, string, bool> action)
	{
		m_tempContentTags.Clear();
		m_tempQualityTags.Clear();
		UpdateUtils.ResizeListIfNeeded(m_tempQualityTags, qualityTags.Count);
		UpdateUtils.ResizeListIfNeeded(m_tempContentTags, contentTags.Count);
		foreach (string item in inputTags)
		{
			if (qualityTags.Contains(item))
			{
				m_tempQualityTags.Add(item);
			}
			if (contentTags.Contains(item))
			{
				m_tempContentTags.Add(item);
			}
		}
		bool flag = false;
		bool flag2 = true;
		foreach (string tempQualityTag in m_tempQualityTags)
		{
			foreach (string tempContentTag in m_tempContentTags)
			{
				flag = true;
				if (!action(tempQualityTag, tempContentTag))
				{
					flag2 = false;
					break;
				}
			}
		}
		return flag && flag2;
	}
}
