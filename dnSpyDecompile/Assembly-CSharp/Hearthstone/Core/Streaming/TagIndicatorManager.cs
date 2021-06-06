using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blizzard.T5.Core;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001093 RID: 4243
	public class TagIndicatorManager
	{
		// Token: 0x0600B7F2 RID: 47090 RVA: 0x00385A69 File Offset: 0x00383C69
		public void Initialize(string dataPath)
		{
			this.m_dataPath = dataPath;
			this.m_indicatorChecker = new IndicatorChecker();
			this.m_tagCombinatorHelper = new TagCombinatorHelper();
			this.Reset(false);
		}

		// Token: 0x0600B7F3 RID: 47091 RVA: 0x00385A8F File Offset: 0x00383C8F
		public void SetExistsIndicators(IIndicatorChecker indicatorChecker)
		{
			this.m_indicatorChecker = indicatorChecker;
		}

		// Token: 0x0600B7F4 RID: 47092 RVA: 0x00385A98 File Offset: 0x00383C98
		public void Check()
		{
			this.Reset(false);
			foreach (string text in this.m_lastQualityTags.Keys.ToList<string>())
			{
				while (this.m_lastQualityTags[text] < this.m_allExpectedQualityTags.Count<string>() - 1)
				{
					int num = this.m_lastQualityTags[text] + 1;
					string text2 = this.m_allExpectedQualityTags[num];
					bool flag = text2 == DownloadTags.GetTagString(DownloadTags.Quality.Manifest);
					if ((!flag || text == DownloadTags.GetTagString(DownloadTags.Content.Base)) && !this.m_indicatorChecker.Exists(this.FindIndicators(new string[]
					{
						text,
						text2
					}, true)))
					{
						break;
					}
					Log.Downloader.PrintInfo("Found the indicator for {0} of {1}", new object[]
					{
						text2,
						text
					});
					this.m_lastQualityTags[text] = num;
					this.RecordAvailBundles(new string[]
					{
						text,
						text2
					}, flag);
				}
			}
		}

		// Token: 0x0600B7F5 RID: 47093 RVA: 0x00385BBC File Offset: 0x00383DBC
		public bool IsReady(string[] tags)
		{
			bool flag;
			if (this.m_ready)
			{
				if (this.m_isDownloadedDelegate == null)
				{
					this.m_isDownloadedDelegate = new Func<string, string, bool>(this.IsAlreadyDownloaded);
				}
				flag = this.m_tagCombinatorHelper.ForEachCombination(tags, this.m_allQualityTags, this.m_allContentTags, this.m_isDownloadedDelegate);
			}
			else
			{
				flag = this.m_indicatorChecker.Exists(this.FindIndicators(tags, true));
			}
			if (flag)
			{
				Log.Downloader.PrintInfo("ready = {0}, tags = {1}", new object[]
				{
					flag,
					string.Join(" ", tags)
				});
			}
			return flag;
		}

		// Token: 0x0600B7F6 RID: 47094 RVA: 0x00385C52 File Offset: 0x00383E52
		public bool IsReady(string bundlename)
		{
			return this.m_ready && this.m_availableBundles.Contains(bundlename);
		}

		// Token: 0x0600B7F7 RID: 47095 RVA: 0x00385C6C File Offset: 0x00383E6C
		public void ClearAllIndicators()
		{
			string[] files;
			try
			{
				files = Directory.GetFiles(this.m_dataPath, "tag_*");
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Failed to get the file list from {0}: {1}", new object[]
				{
					this.m_dataPath,
					ex.Message
				});
				return;
			}
			foreach (string text in files)
			{
				Log.Downloader.PrintInfo("Delete the indicator - " + text, Array.Empty<object>());
				try
				{
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					Log.Downloader.PrintError("Failed to delete the indicator({0}): {1}", new object[]
					{
						text,
						ex2.Message
					});
				}
			}
			this.Reset(true);
		}

		// Token: 0x0600B7F8 RID: 47096 RVA: 0x00385D3C File Offset: 0x00383F3C
		public void SetSkipInitialDataCheck()
		{
			this.m_skipInitialDataCheck = true;
		}

		// Token: 0x0600B7F9 RID: 47097 RVA: 0x00385D48 File Offset: 0x00383F48
		private void RecordAvailBundles(string[] availableTags, bool loadLocaleManifest)
		{
			Log.Downloader.PrintDebug("RecordAvailBundles with '{0}', load locale manifest '{1}'", new object[]
			{
				string.Join(" ", availableTags),
				loadLocaleManifest
			});
			if (loadLocaleManifest)
			{
				Log.Downloader.PrintDebug("Load locale asset manifest for {0}", new object[]
				{
					Localization.GetLocale().ToString()
				});
				this.GetAssetManifest().ReadLocaleCatalogs();
			}
			Func<string, bool> <>9__2;
			this.GetAssetManifest().GetAllAssetBundleNames(Localization.GetLocale()).Where(delegate(string b)
			{
				IEnumerable<string> tagsFromAssetBundle = this.GetAssetManifest().GetTagsFromAssetBundle(b);
				Func<string, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = ((string t) => availableTags.Contains(t)));
				}
				return tagsFromAssetBundle.All(predicate);
			}).ToArray<string>().ForEach(delegate(string b)
			{
				if (File.Exists(AssetBundleInfo.GetAssetBundlePath(b)))
				{
					this.m_availableBundles.Add(b);
					Log.Downloader.PrintDebug("available: {0}", new object[]
					{
						b
					});
					return;
				}
				Log.Downloader.PrintError("unavailable still(file not found): {0}", new object[]
				{
					b
				});
			});
		}

		// Token: 0x0600B7FA RID: 47098 RVA: 0x00385E0C File Offset: 0x0038400C
		private bool IsAlreadyDownloaded(string quality, string content)
		{
			this.Reset(false);
			if (!this.m_ready)
			{
				return false;
			}
			int num;
			if (!this.m_lastQualityTags.TryGetValue(content, out num))
			{
				Log.Downloader.PrintInfo("Ignored the content key which does not exist: '{0}'", new object[]
				{
					content
				});
				return false;
			}
			int num2 = this.m_allExpectedQualityTags.IndexOf(quality);
			if (num2 == -1)
			{
				Log.Downloader.PrintInfo("Ignored the quality key which does not exist: '{0}'", new object[]
				{
					quality
				});
				return false;
			}
			return num >= num2;
		}

		// Token: 0x0600B7FB RID: 47099 RVA: 0x00385E88 File Offset: 0x00384088
		protected void Reset(bool forcibly)
		{
			if (this.GetAssetManifest() == null || (this.m_ready && !forcibly))
			{
				return;
			}
			this.m_allExpectedQualityTags.Clear();
			this.m_lastQualityTags.Clear();
			this.m_availableBundles.Clear();
			this.m_allQualityTags.Clear();
			this.m_allContentTags.Clear();
			this.GetAssetManifest().GetAllTags(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content), true).ForEach(delegate(string t)
			{
				this.m_lastQualityTags[t] = -1;
			});
			string[] allTags = this.GetAssetManifest().GetAllTags(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality), true);
			int minSize = allTags.Length;
			UpdateUtils.ResizeListIfNeeded(this.m_allExpectedQualityTags, minSize);
			this.m_allExpectedQualityTags.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
			foreach (string text in allTags)
			{
				if (!string.IsNullOrEmpty(text) && (!(text == DownloadTags.GetTagString(DownloadTags.Quality.PortHigh)) || !PlatformSettings.ShouldFallbackToLowRes))
				{
					this.m_allExpectedQualityTags.Add(text);
				}
			}
			string[] tagsInTagGroup = this.GetAssetManifest().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
			int num = tagsInTagGroup.Length;
			UpdateUtils.ResizeListIfNeeded(this.m_allQualityTags, num);
			this.m_allQualityTags.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
			foreach (string item in tagsInTagGroup)
			{
				if (!this.m_allQualityTags.Contains(item))
				{
					this.m_allQualityTags.Add(item);
				}
			}
			string[] tagsInTagGroup2 = this.GetAssetManifest().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content));
			int num2 = tagsInTagGroup2.Length;
			UpdateUtils.ResizeListIfNeeded(this.m_allContentTags, num2);
			foreach (string item2 in tagsInTagGroup2)
			{
				if (!this.m_allContentTags.Contains(item2))
				{
					this.m_allContentTags.Add(item2);
				}
			}
			UpdateUtils.ResizeListIfNeeded(this.m_tempIndicators, num2 * num);
			this.m_lastQualityTags.ForEach(delegate(KeyValuePair<string, int> p)
			{
				Log.Downloader.PrintDebug("{0} = {1}", new object[]
				{
					p.Key,
					p.Value
				});
			});
			Log.Downloader.PrintDebug("Expected qualities = {0}", new object[]
			{
				string.Join(" ", this.m_allExpectedQualityTags)
			});
			Log.Downloader.PrintDebug("All qualities = {0}", new object[]
			{
				string.Join(" ", this.m_allQualityTags)
			});
			Log.Downloader.PrintDebug("All contents = {0}", new object[]
			{
				string.Join(" ", this.m_allContentTags)
			});
			if (this.m_skipInitialDataCheck && this.m_lastQualityTags.ContainsKey(DownloadTags.GetTagString(DownloadTags.Content.Base)))
			{
				string tagString = DownloadTags.GetTagString(DownloadTags.Quality.Initial);
				int value = this.m_allQualityTags.IndexOf(tagString);
				this.m_lastQualityTags[DownloadTags.GetTagString(DownloadTags.Content.Base)] = value;
				this.RecordAvailBundles(new string[]
				{
					DownloadTags.GetTagString(DownloadTags.Content.Base),
					DownloadTags.GetTagString(DownloadTags.Quality.Initial)
				}, false);
			}
			this.m_ready = true;
			this.Check();
		}

		// Token: 0x0600B7FC RID: 47100 RVA: 0x0038615C File Offset: 0x0038435C
		protected string[] FindIndicators(string[] tags, bool fullPath)
		{
			if (!this.m_ready)
			{
				string indicatorName = this.GetIndicatorName(tags);
				return new string[]
				{
					fullPath ? Path.Combine(this.m_dataPath, indicatorName) : indicatorName
				};
			}
			this.m_tempIndicators.Clear();
			this.m_tagCombinatorHelper.ForEachCombination(tags, this.m_allQualityTags, this.m_allContentTags, delegate(string quality, string content)
			{
				string text = "tag_" + quality + "_" + content;
				this.m_tempIndicators.Add(fullPath ? Path.Combine(this.m_dataPath, text) : text);
				return true;
			});
			return this.m_tempIndicators.ToArray();
		}

		// Token: 0x0600B7FD RID: 47101 RVA: 0x003861EC File Offset: 0x003843EC
		protected string GetIndicatorName(string[] tags)
		{
			string text = "tag";
			foreach (string str in tags)
			{
				text = text + "_" + str;
			}
			return text;
		}

		// Token: 0x0600B7FE RID: 47102 RVA: 0x00386221 File Offset: 0x00384421
		private IAssetManifest GetAssetManifest()
		{
			if (this.m_assetManifest == null)
			{
				this.m_assetManifest = AssetManifest.Get();
			}
			return this.m_assetManifest;
		}

		// Token: 0x04009839 RID: 38969
		private Dictionary<string, int> m_lastQualityTags = new Dictionary<string, int>();

		// Token: 0x0400983A RID: 38970
		private List<string> m_allQualityTags = new List<string>();

		// Token: 0x0400983B RID: 38971
		private List<string> m_allContentTags = new List<string>();

		// Token: 0x0400983C RID: 38972
		private List<string> m_allExpectedQualityTags = new List<string>();

		// Token: 0x0400983D RID: 38973
		private string m_dataPath;

		// Token: 0x0400983E RID: 38974
		private IIndicatorChecker m_indicatorChecker;

		// Token: 0x0400983F RID: 38975
		private bool m_ready;

		// Token: 0x04009840 RID: 38976
		private HashSet<string> m_availableBundles = new HashSet<string>();

		// Token: 0x04009841 RID: 38977
		private bool m_skipInitialDataCheck;

		// Token: 0x04009842 RID: 38978
		private IAssetManifest m_assetManifest;

		// Token: 0x04009843 RID: 38979
		private TagCombinatorHelper m_tagCombinatorHelper;

		// Token: 0x04009844 RID: 38980
		private List<string> m_tempIndicators = new List<string>();

		// Token: 0x04009845 RID: 38981
		private Func<string, string, bool> m_isDownloadedDelegate;
	}
}
