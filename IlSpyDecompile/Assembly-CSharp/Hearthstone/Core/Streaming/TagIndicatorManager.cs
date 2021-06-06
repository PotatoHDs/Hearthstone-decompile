using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blizzard.T5.Core;

namespace Hearthstone.Core.Streaming
{
	public class TagIndicatorManager
	{
		private Dictionary<string, int> m_lastQualityTags = new Dictionary<string, int>();

		private List<string> m_allQualityTags = new List<string>();

		private List<string> m_allContentTags = new List<string>();

		private List<string> m_allExpectedQualityTags = new List<string>();

		private string m_dataPath;

		private IIndicatorChecker m_indicatorChecker;

		private bool m_ready;

		private HashSet<string> m_availableBundles = new HashSet<string>();

		private bool m_skipInitialDataCheck;

		private IAssetManifest m_assetManifest;

		private TagCombinatorHelper m_tagCombinatorHelper;

		private List<string> m_tempIndicators = new List<string>();

		private Func<string, string, bool> m_isDownloadedDelegate;

		public void Initialize(string dataPath)
		{
			m_dataPath = dataPath;
			m_indicatorChecker = new IndicatorChecker();
			m_tagCombinatorHelper = new TagCombinatorHelper();
			Reset(forcibly: false);
		}

		public void SetExistsIndicators(IIndicatorChecker indicatorChecker)
		{
			m_indicatorChecker = indicatorChecker;
		}

		public void Check()
		{
			Reset(forcibly: false);
			foreach (string item in m_lastQualityTags.Keys.ToList())
			{
				while (m_lastQualityTags[item] < m_allExpectedQualityTags.Count() - 1)
				{
					int num = m_lastQualityTags[item] + 1;
					string text = m_allExpectedQualityTags[num];
					bool flag = text == DownloadTags.GetTagString(DownloadTags.Quality.Manifest);
					if ((!flag || item == DownloadTags.GetTagString(DownloadTags.Content.Base)) && !m_indicatorChecker.Exists(FindIndicators(new string[2] { item, text }, fullPath: true)))
					{
						break;
					}
					Log.Downloader.PrintInfo("Found the indicator for {0} of {1}", text, item);
					m_lastQualityTags[item] = num;
					RecordAvailBundles(new string[2] { item, text }, flag);
				}
			}
		}

		public bool IsReady(string[] tags)
		{
			bool flag = false;
			if (m_ready)
			{
				if (m_isDownloadedDelegate == null)
				{
					m_isDownloadedDelegate = IsAlreadyDownloaded;
				}
				flag = m_tagCombinatorHelper.ForEachCombination(tags, m_allQualityTags, m_allContentTags, m_isDownloadedDelegate);
			}
			else
			{
				flag = m_indicatorChecker.Exists(FindIndicators(tags, fullPath: true));
			}
			if (flag)
			{
				Log.Downloader.PrintInfo("ready = {0}, tags = {1}", flag, string.Join(" ", tags));
			}
			return flag;
		}

		public bool IsReady(string bundlename)
		{
			if (m_ready)
			{
				return m_availableBundles.Contains(bundlename);
			}
			return false;
		}

		public void ClearAllIndicators()
		{
			string[] files;
			try
			{
				files = Directory.GetFiles(m_dataPath, "tag_*");
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Failed to get the file list from {0}: {1}", m_dataPath, ex.Message);
				return;
			}
			string[] array = files;
			foreach (string text in array)
			{
				Log.Downloader.PrintInfo("Delete the indicator - " + text);
				try
				{
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					Log.Downloader.PrintError("Failed to delete the indicator({0}): {1}", text, ex2.Message);
				}
			}
			Reset(forcibly: true);
		}

		public void SetSkipInitialDataCheck()
		{
			m_skipInitialDataCheck = true;
		}

		private void RecordAvailBundles(string[] availableTags, bool loadLocaleManifest)
		{
			Log.Downloader.PrintDebug("RecordAvailBundles with '{0}', load locale manifest '{1}'", string.Join(" ", availableTags), loadLocaleManifest);
			if (loadLocaleManifest)
			{
				Log.Downloader.PrintDebug("Load locale asset manifest for {0}", Localization.GetLocale().ToString());
				GetAssetManifest().ReadLocaleCatalogs();
			}
			(from b in GetAssetManifest().GetAllAssetBundleNames(Localization.GetLocale())
				where GetAssetManifest().GetTagsFromAssetBundle(b).All((string t) => availableTags.Contains(t))
				select b).ToArray().ForEach(delegate(string b)
			{
				if (File.Exists(AssetBundleInfo.GetAssetBundlePath(b)))
				{
					m_availableBundles.Add(b);
					Log.Downloader.PrintDebug("available: {0}", b);
				}
				else
				{
					Log.Downloader.PrintError("unavailable still(file not found): {0}", b);
				}
			});
		}

		private bool IsAlreadyDownloaded(string quality, string content)
		{
			Reset(forcibly: false);
			if (!m_ready)
			{
				return false;
			}
			if (!m_lastQualityTags.TryGetValue(content, out var value))
			{
				Log.Downloader.PrintInfo("Ignored the content key which does not exist: '{0}'", content);
				return false;
			}
			int num = m_allExpectedQualityTags.IndexOf(quality);
			if (num == -1)
			{
				Log.Downloader.PrintInfo("Ignored the quality key which does not exist: '{0}'", quality);
				return false;
			}
			return value >= num;
		}

		protected void Reset(bool forcibly)
		{
			if (GetAssetManifest() == null || (m_ready && !forcibly))
			{
				return;
			}
			m_allExpectedQualityTags.Clear();
			m_lastQualityTags.Clear();
			m_availableBundles.Clear();
			m_allQualityTags.Clear();
			m_allContentTags.Clear();
			GetAssetManifest().GetAllTags(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content), excludeOverridenTag: true).ForEach(delegate(string t)
			{
				m_lastQualityTags[t] = -1;
			});
			string[] allTags = GetAssetManifest().GetAllTags(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality), excludeOverridenTag: true);
			UpdateUtils.ResizeListIfNeeded(minSize: allTags.Length, list: m_allExpectedQualityTags);
			m_allExpectedQualityTags.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
			string[] array = allTags;
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text) && (!(text == DownloadTags.GetTagString(DownloadTags.Quality.PortHigh)) || !PlatformSettings.ShouldFallbackToLowRes))
				{
					m_allExpectedQualityTags.Add(text);
				}
			}
			string[] tagsInTagGroup = GetAssetManifest().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
			int num = tagsInTagGroup.Length;
			UpdateUtils.ResizeListIfNeeded(m_allQualityTags, num);
			m_allQualityTags.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
			array = tagsInTagGroup;
			foreach (string item in array)
			{
				if (!m_allQualityTags.Contains(item))
				{
					m_allQualityTags.Add(item);
				}
			}
			string[] tagsInTagGroup2 = GetAssetManifest().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content));
			int num2 = tagsInTagGroup2.Length;
			UpdateUtils.ResizeListIfNeeded(m_allContentTags, num2);
			array = tagsInTagGroup2;
			foreach (string item2 in array)
			{
				if (!m_allContentTags.Contains(item2))
				{
					m_allContentTags.Add(item2);
				}
			}
			UpdateUtils.ResizeListIfNeeded(m_tempIndicators, num2 * num);
			m_lastQualityTags.ForEach(delegate(KeyValuePair<string, int> p)
			{
				Log.Downloader.PrintDebug("{0} = {1}", p.Key, p.Value);
			});
			Log.Downloader.PrintDebug("Expected qualities = {0}", string.Join(" ", m_allExpectedQualityTags));
			Log.Downloader.PrintDebug("All qualities = {0}", string.Join(" ", m_allQualityTags));
			Log.Downloader.PrintDebug("All contents = {0}", string.Join(" ", m_allContentTags));
			if (m_skipInitialDataCheck && m_lastQualityTags.ContainsKey(DownloadTags.GetTagString(DownloadTags.Content.Base)))
			{
				string tagString = DownloadTags.GetTagString(DownloadTags.Quality.Initial);
				int value = m_allQualityTags.IndexOf(tagString);
				m_lastQualityTags[DownloadTags.GetTagString(DownloadTags.Content.Base)] = value;
				RecordAvailBundles(new string[2]
				{
					DownloadTags.GetTagString(DownloadTags.Content.Base),
					DownloadTags.GetTagString(DownloadTags.Quality.Initial)
				}, loadLocaleManifest: false);
			}
			m_ready = true;
			Check();
		}

		protected string[] FindIndicators(string[] tags, bool fullPath)
		{
			if (!m_ready)
			{
				string indicatorName = GetIndicatorName(tags);
				return new string[1] { fullPath ? Path.Combine(m_dataPath, indicatorName) : indicatorName };
			}
			m_tempIndicators.Clear();
			m_tagCombinatorHelper.ForEachCombination(tags, m_allQualityTags, m_allContentTags, delegate(string quality, string content)
			{
				string text = "tag_" + quality + "_" + content;
				m_tempIndicators.Add(fullPath ? Path.Combine(m_dataPath, text) : text);
				return true;
			});
			return m_tempIndicators.ToArray();
		}

		protected string GetIndicatorName(string[] tags)
		{
			string text = "tag";
			foreach (string text2 in tags)
			{
				text = text + "_" + text2;
			}
			return text;
		}

		private IAssetManifest GetAssetManifest()
		{
			if (m_assetManifest == null)
			{
				m_assetManifest = AssetManifest.Get();
			}
			return m_assetManifest;
		}
	}
}
