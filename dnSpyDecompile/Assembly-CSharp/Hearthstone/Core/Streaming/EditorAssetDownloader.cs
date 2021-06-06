using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x0200108B RID: 4235
	public class EditorAssetDownloader : IAssetDownloader
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600B6EC RID: 46828 RVA: 0x00380B90 File Offset: 0x0037ED90
		public static string SerializedPath
		{
			get
			{
				return Path.Combine(FileUtils.PersistentDataPath, "EditorAssetDownloader_DownloadProgress.json");
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600B6ED RID: 46829 RVA: 0x00380BA1 File Offset: 0x0037EDA1
		// (set) Token: 0x0600B6EE RID: 46830 RVA: 0x00380BA9 File Offset: 0x0037EDA9
		public AssetDownloaderState State { get; private set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600B6EF RID: 46831 RVA: 0x00090064 File Offset: 0x0008E264
		public string AgentLogPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600B6F0 RID: 46832 RVA: 0x00380BB2 File Offset: 0x0037EDB2
		// (set) Token: 0x0600B6F1 RID: 46833 RVA: 0x00380BBA File Offset: 0x0037EDBA
		public bool IsReady { get; private set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600B6F2 RID: 46834 RVA: 0x00380BC3 File Offset: 0x0037EDC3
		// (set) Token: 0x0600B6F3 RID: 46835 RVA: 0x00380BCB File Offset: 0x0037EDCB
		public bool IsNewMobileVersionReleased { get; private set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600B6F4 RID: 46836 RVA: 0x00380BD4 File Offset: 0x0037EDD4
		// (set) Token: 0x0600B6F5 RID: 46837 RVA: 0x00380BDC File Offset: 0x0037EDDC
		public bool IsRunningNewerBinaryThanLive { get; private set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600B6F6 RID: 46838 RVA: 0x00380BE5 File Offset: 0x0037EDE5
		// (set) Token: 0x0600B6F7 RID: 46839 RVA: 0x00380BED File Offset: 0x0037EDED
		public bool IsVersionChanged { get; private set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600B6F8 RID: 46840 RVA: 0x00380BF6 File Offset: 0x0037EDF6
		// (set) Token: 0x0600B6F9 RID: 46841 RVA: 0x00380BFE File Offset: 0x0037EDFE
		public bool IsAssetManifestReady { get; private set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600B6FA RID: 46842 RVA: 0x00380C07 File Offset: 0x0037EE07
		// (set) Token: 0x0600B6FB RID: 46843 RVA: 0x00380C0F File Offset: 0x0037EE0F
		public bool DownloadAllFinished { get; set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600B6FC RID: 46844 RVA: 0x00380C18 File Offset: 0x0037EE18
		// (set) Token: 0x0600B6FD RID: 46845 RVA: 0x00380C20 File Offset: 0x0037EE20
		public bool DisabledErrorMessageDialog { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600B6FE RID: 46846 RVA: 0x00380C29 File Offset: 0x0037EE29
		// (set) Token: 0x0600B6FF RID: 46847 RVA: 0x00380C31 File Offset: 0x0037EE31
		public string PatchOverrideUrl { get; private set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x0600B700 RID: 46848 RVA: 0x00380C3A File Offset: 0x0037EE3A
		// (set) Token: 0x0600B701 RID: 46849 RVA: 0x00380C42 File Offset: 0x0037EE42
		public string VersionOverrideUrl { get; private set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600B702 RID: 46850 RVA: 0x00380C4B File Offset: 0x0037EE4B
		// (set) Token: 0x0600B703 RID: 46851 RVA: 0x00380C53 File Offset: 0x0037EE53
		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				return this.m_disabledAdventuresForStreaming;
			}
			private set
			{
				this.m_disabledAdventuresForStreaming = value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600B704 RID: 46852 RVA: 0x00380C5C File Offset: 0x0037EE5C
		// (set) Token: 0x0600B705 RID: 46853 RVA: 0x00380C64 File Offset: 0x0037EE64
		public double BytesPerSecond { get; private set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600B706 RID: 46854 RVA: 0x00380C6D File Offset: 0x0037EE6D
		// (set) Token: 0x0600B707 RID: 46855 RVA: 0x00380C75 File Offset: 0x0037EE75
		public int MaxDownloadSpeed { get; set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600B708 RID: 46856 RVA: 0x00380C7E File Offset: 0x0037EE7E
		// (set) Token: 0x0600B709 RID: 46857 RVA: 0x00380C86 File Offset: 0x0037EE86
		public int InGameStreamingDefaultSpeed { get; set; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600B70A RID: 46858 RVA: 0x00380C8F File Offset: 0x0037EE8F
		// (set) Token: 0x0600B70B RID: 46859 RVA: 0x00380C97 File Offset: 0x0037EE97
		public int DownloadSpeedInGame { get; set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600B70C RID: 46860 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public static EditorAssetDownloader.DownloadMode Mode
		{
			get
			{
				return EditorAssetDownloader.DownloadMode.None;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600B70D RID: 46861 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public static int BuildNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600B70E RID: 46862 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public static int FakeDownloadSpeedBytesPerSecond
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600B70F RID: 46863 RVA: 0x00380CA0 File Offset: 0x0037EEA0
		public bool Initialize()
		{
			this.PatchOverrideUrl = "Live";
			this.VersionOverrideUrl = "Live";
			this.State = AssetDownloaderState.UNINITIALIZED;
			this.m_assetManifest = null;
			EditorAssetDownloader.DownloadMode mode = EditorAssetDownloader.Mode;
			if (mode != EditorAssetDownloader.DownloadMode.Local)
			{
				if (mode == EditorAssetDownloader.DownloadMode.Build)
				{
					this.m_assetManifest = AssetManifest.Get();
				}
			}
			else
			{
				this.m_assetManifest = AssetManifest.Get();
			}
			if (this.m_assetManifest == null)
			{
				Log.Downloader.PrintError("EditorAssetDownloader: Failed to load asset manifest", Array.Empty<object>());
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLOBAL_ERROR_ASSET_MANIFEST"), GameStrings.Get("GLOBAL_QUIT"), delegate()
				{
					this.ShutdownApplication();
				});
				return false;
			}
			this.m_initializeCalled = true;
			this.m_secondsUntilReady = 1f;
			this.IsNewMobileVersionReleased = false;
			this.IsReady = true;
			this.IsAssetManifestReady = true;
			this.IsRunningNewerBinaryThanLive = false;
			this.DeserializeDownloadProgress();
			return true;
		}

		// Token: 0x0600B710 RID: 46864 RVA: 0x00380D7C File Offset: 0x0037EF7C
		private void ShutdownApplication()
		{
			Log.Downloader.PrintInfo("ShutdownApplication", Array.Empty<object>());
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.Exit();
				return;
			}
			GeneralUtils.ExitApplication();
		}

		// Token: 0x0600B711 RID: 46865 RVA: 0x00380DB8 File Offset: 0x0037EFB8
		public void Update(bool firstCall)
		{
			if (!this.m_initializeCalled)
			{
				return;
			}
			switch (this.State)
			{
			case AssetDownloaderState.UNINITIALIZED:
				this.m_secondsUntilReady -= Time.deltaTime;
				if (this.m_secondsUntilReady <= 0f)
				{
					this.m_secondsUntilReady = 0f;
					this.State = AssetDownloaderState.IDLE;
				}
				break;
			case AssetDownloaderState.IDLE:
				if (this.m_requestedTags != null && this.m_requestedTags.Length != 0)
				{
					this.State = AssetDownloaderState.DOWNLOADING;
				}
				break;
			case AssetDownloaderState.DOWNLOADING:
				if (this.m_requestedTags == null || this.m_requestedTags.Length == 0)
				{
					this.State = AssetDownloaderState.IDLE;
				}
				else
				{
					EditorAssetDownloader.DownloadMode mode = EditorAssetDownloader.Mode;
					if (mode != EditorAssetDownloader.DownloadMode.Local)
					{
						if (mode == EditorAssetDownloader.DownloadMode.Build)
						{
							this.UpdateBuild();
						}
					}
					else
					{
						this.UpdateLocal();
					}
				}
				break;
			}
			this.TrySerializeDownloadProgress();
		}

		// Token: 0x0600B712 RID: 46866 RVA: 0x00380E80 File Offset: 0x0037F080
		private void UpdateLocal()
		{
			this.BytesPerSecond = 0.0;
			EditorAssetDownloader.AssetBundleDownloadProgress[] allProgressForRequestedTags = this.GetAllProgressForRequestedTags();
			long num = 0L;
			long num2 = 0L;
			foreach (EditorAssetDownloader.AssetBundleDownloadProgress assetBundleDownloadProgress in allProgressForRequestedTags)
			{
				if (assetBundleDownloadProgress.BytesDownloaded < assetBundleDownloadProgress.BytesTotal)
				{
					assetBundleDownloadProgress.BytesDownloaded += (long)((float)EditorAssetDownloader.FakeDownloadSpeedBytesPerSecond * Time.deltaTime);
					assetBundleDownloadProgress.BytesDownloaded = Math.Min(assetBundleDownloadProgress.BytesDownloaded, assetBundleDownloadProgress.BytesTotal);
					this.BytesPerSecond = (double)EditorAssetDownloader.FakeDownloadSpeedBytesPerSecond;
					num += assetBundleDownloadProgress.BytesDownloaded;
					num2 += assetBundleDownloadProgress.BytesTotal;
				}
			}
			if (num2 == num)
			{
				this.DownloadAllFinished = true;
			}
		}

		// Token: 0x0600B713 RID: 46867 RVA: 0x00380F2C File Offset: 0x0037F12C
		private void UpdateBuild()
		{
			foreach (EditorAssetDownloader.AssetBundleDownloadProgress assetBundleDownloadProgress in this.GetAllProgressForRequestedTags())
			{
			}
		}

		// Token: 0x0600B714 RID: 46868 RVA: 0x00380F54 File Offset: 0x0037F154
		private List<string> FindFirstIncompleteTagSet(List<string> parentTags, List<List<string>> requestedTagGroups, int tagGroupIndex)
		{
			if (tagGroupIndex != requestedTagGroups.Count)
			{
				foreach (string item in requestedTagGroups[tagGroupIndex])
				{
					List<string> parentTags2 = new List<string>(parentTags)
					{
						item
					};
					List<string> list = this.FindFirstIncompleteTagSet(parentTags2, requestedTagGroups, tagGroupIndex + 1);
					if (list != null)
					{
						return list;
					}
				}
				return null;
			}
			if (!this.GetDownloadStatus(parentTags.ToArray()).Complete)
			{
				return parentTags;
			}
			return null;
		}

		// Token: 0x0600B715 RID: 46869 RVA: 0x00380FE8 File Offset: 0x0037F1E8
		private EditorAssetDownloader.AssetBundleDownloadProgress[] GetAllProgressForRequestedTags()
		{
			List<List<string>> list = new List<List<string>>();
			foreach (string tagGroup in this.m_assetManifest.GetTagGroups())
			{
				List<string> list2 = new List<string>();
				foreach (string text in this.m_assetManifest.GetTagsInTagGroup(tagGroup))
				{
					if (this.m_requestedTags.Contains(text))
					{
						list2.Add(text);
					}
				}
				if (list2.Count > 0)
				{
					list.Add(list2);
				}
			}
			list.Add(new List<string>
			{
				Localization.GetLocale().ToString()
			});
			List<string> requestedTags = this.FindFirstIncompleteTagSet(new List<string>(), list, 0);
			if (requestedTags == null)
			{
				return new EditorAssetDownloader.AssetBundleDownloadProgress[0];
			}
			return (from bundleProgress in this.m_downloadProgress.AssetBundles
			where this.QueryTags(bundleProgress.Tags, requestedTags)
			select bundleProgress).ToArray<EditorAssetDownloader.AssetBundleDownloadProgress>();
		}

		// Token: 0x0600B716 RID: 46870 RVA: 0x003810EC File Offset: 0x0037F2EC
		private bool QueryTags(ICollection<string> targetTags, ICollection<string> queryTags)
		{
			if (targetTags == null || targetTags.Count == 0)
			{
				return false;
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string tag in targetTags)
			{
				hashSet.Add(this.m_assetManifest.GetTagGroupForTag(tag));
			}
			foreach (string text in queryTags)
			{
				string tagGroupForTag = this.m_assetManifest.GetTagGroupForTag(text);
				if (targetTags.Contains(text))
				{
					hashSet.Remove(tagGroupForTag);
				}
			}
			return hashSet.Count == 0;
		}

		// Token: 0x0600B717 RID: 46871 RVA: 0x003811AC File Offset: 0x0037F3AC
		private void DeserializeDownloadProgress()
		{
			this.m_downloadProgress = EditorAssetDownloader.DeserializeProgress();
			if (this.m_downloadProgress == null)
			{
				this.m_downloadProgress = new EditorAssetDownloader.DownloadProgress();
			}
			EditorAssetDownloader.DownloadMode mode = EditorAssetDownloader.Mode;
			if (mode - EditorAssetDownloader.DownloadMode.Local <= 1)
			{
				string[] assetBundleNames = this.m_assetManifest.GetAllAssetBundleNames(Locale.UNKNOWN);
				this.m_downloadProgress.AssetBundles = (from a in this.m_downloadProgress.AssetBundles
				where assetBundleNames.Contains(a.Name)
				select a).ToList<EditorAssetDownloader.AssetBundleDownloadProgress>();
				string[] assetBundleNames2 = assetBundleNames;
				for (int i = 0; i < assetBundleNames2.Length; i++)
				{
					string text = assetBundleNames2[i];
					string bundlePath = AssetBundleInfo.GetAssetBundlePath(text);
					EditorAssetDownloader.AssetBundleDownloadProgress assetBundleDownloadProgress = this.m_downloadProgress.AssetBundles.FirstOrDefault((EditorAssetDownloader.AssetBundleDownloadProgress a) => a.Path == bundlePath);
					if (assetBundleDownloadProgress == null)
					{
						assetBundleDownloadProgress = new EditorAssetDownloader.AssetBundleDownloadProgress();
						assetBundleDownloadProgress.Name = text;
						assetBundleDownloadProgress.Path = bundlePath;
						this.m_downloadProgress.AssetBundles.Add(assetBundleDownloadProgress);
						assetBundleDownloadProgress.BytesTotal = (File.Exists(bundlePath) ? new FileInfo(bundlePath).Length : ((long)(UnityEngine.Random.Range(100, 200) * 1024 * 1024)));
					}
					List<string> list = new List<string>(this.m_assetManifest.GetTagsFromAssetBundle(assetBundleDownloadProgress.Name));
					string localeFromAssetBundle = UpdateUtils.GetLocaleFromAssetBundle(assetBundleDownloadProgress.Name);
					if (!string.IsNullOrEmpty(localeFromAssetBundle))
					{
						list.Add(localeFromAssetBundle);
					}
					assetBundleDownloadProgress.Tags = list.ToArray();
					assetBundleDownloadProgress.BytesDownloaded = Math.Min(assetBundleDownloadProgress.BytesDownloaded, assetBundleDownloadProgress.BytesTotal);
					if (text.StartsWith("local") && EditorAssetDownloader.Mode == EditorAssetDownloader.DownloadMode.Local)
					{
						assetBundleDownloadProgress.BytesDownloaded = assetBundleDownloadProgress.BytesTotal;
					}
				}
				this.m_downloadProgress.AssetBundles.Sort((EditorAssetDownloader.AssetBundleDownloadProgress a, EditorAssetDownloader.AssetBundleDownloadProgress b) => a.Name.CompareTo(b.Name));
			}
		}

		// Token: 0x0600B718 RID: 46872 RVA: 0x003813A7 File Offset: 0x0037F5A7
		public static EditorAssetDownloader.DownloadProgress DeserializeProgress()
		{
			if (File.Exists(EditorAssetDownloader.SerializedPath))
			{
				return JsonUtility.FromJson<EditorAssetDownloader.DownloadProgress>(File.ReadAllText(EditorAssetDownloader.SerializedPath));
			}
			return null;
		}

		// Token: 0x0600B719 RID: 46873 RVA: 0x003813C6 File Offset: 0x0037F5C6
		public static void ResetDownloadProgress()
		{
			if (File.Exists(EditorAssetDownloader.SerializedPath))
			{
				File.Delete(EditorAssetDownloader.SerializedPath);
			}
		}

		// Token: 0x0600B71A RID: 46874 RVA: 0x003813E0 File Offset: 0x0037F5E0
		private void TrySerializeDownloadProgress()
		{
			if (this.State != AssetDownloaderState.UNINITIALIZED)
			{
				if (this.m_downloadProgress.AssetBundles.Any((EditorAssetDownloader.AssetBundleDownloadProgress a) => a.BytesDownloaded < a.BytesTotal) && this.m_requestedTags != null && this.m_requestedTags.Length != 0)
				{
					string contents = JsonUtility.ToJson(this.m_downloadProgress, true);
					File.WriteAllText(EditorAssetDownloader.SerializedPath, contents);
				}
			}
		}

		// Token: 0x0600B71B RID: 46875 RVA: 0x00381450 File Offset: 0x0037F650
		public void Shutdown()
		{
			this.m_requestedTags = new string[0];
			this.m_initializeCalled = false;
			this.State = AssetDownloaderState.UNINITIALIZED;
		}

		// Token: 0x0600B71C RID: 46876 RVA: 0x0038146C File Offset: 0x0037F66C
		public TagDownloadStatus GetDownloadStatus(string[] tags)
		{
			TagDownloadStatus tagDownloadStatus = new TagDownloadStatus();
			if (!tags.Contains(Localization.GetLocale().ToString()))
			{
				tagDownloadStatus.Tags = new string[tags.Length + 1];
				tags.CopyTo(tagDownloadStatus.Tags, 0);
				tagDownloadStatus.Tags[tags.Length] = Localization.GetLocale().ToString();
			}
			else
			{
				tagDownloadStatus.Tags = (string[])tags.Clone();
			}
			EditorAssetDownloader.DownloadMode mode = EditorAssetDownloader.Mode;
			if (mode != EditorAssetDownloader.DownloadMode.None)
			{
				if (mode - EditorAssetDownloader.DownloadMode.Local <= 1)
				{
					foreach (EditorAssetDownloader.AssetBundleDownloadProgress assetBundleDownloadProgress in this.m_downloadProgress.AssetBundles)
					{
						if (this.QueryTags(assetBundleDownloadProgress.Tags, tagDownloadStatus.Tags))
						{
							tagDownloadStatus.BytesTotal += assetBundleDownloadProgress.BytesTotal;
							tagDownloadStatus.BytesDownloaded += assetBundleDownloadProgress.BytesDownloaded;
						}
					}
				}
			}
			else
			{
				tagDownloadStatus.BytesTotal = (tagDownloadStatus.BytesDownloaded = 1L);
			}
			if (tagDownloadStatus.BytesTotal == 0L)
			{
				tagDownloadStatus.Complete = true;
			}
			return tagDownloadStatus;
		}

		// Token: 0x0600B71D RID: 46877 RVA: 0x003815A4 File Offset: 0x0037F7A4
		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			return this.GetDownloadStatus(this.m_requestedTags);
		}

		// Token: 0x0600B71E RID: 46878 RVA: 0x003815B2 File Offset: 0x0037F7B2
		public void StartDownload(string[] tags, bool initialDownload, bool localeChanged)
		{
			this.m_requestedTags = tags;
			this.DownloadAllFinished = false;
		}

		// Token: 0x0600B71F RID: 46879 RVA: 0x003815C2 File Offset: 0x0037F7C2
		public void PauseAllDownloads()
		{
			this.m_requestedTags = new string[0];
		}

		// Token: 0x0600B720 RID: 46880 RVA: 0x003815D0 File Offset: 0x0037F7D0
		public void DeleteDownloadedData()
		{
			if (Directory.Exists("Final/Data"))
			{
				Directory.Delete("Final/Data", true);
			}
		}

		// Token: 0x0600B721 RID: 46881 RVA: 0x003815EC File Offset: 0x0037F7EC
		public bool IsFileDownloaded(string filePath)
		{
			EditorAssetDownloader.DownloadMode mode = EditorAssetDownloader.Mode;
			if (mode == EditorAssetDownloader.DownloadMode.None)
			{
				return true;
			}
			if (mode - EditorAssetDownloader.DownloadMode.Local > 1)
			{
				return false;
			}
			EditorAssetDownloader.AssetBundleDownloadProgress assetBundleDownloadProgress = this.m_downloadProgress.AssetBundles.FirstOrDefault((EditorAssetDownloader.AssetBundleDownloadProgress a) => a.Path == filePath);
			return assetBundleDownloadProgress != null && assetBundleDownloadProgress.BytesTotal == assetBundleDownloadProgress.BytesDownloaded;
		}

		// Token: 0x0600B722 RID: 46882 RVA: 0x0038164C File Offset: 0x0037F84C
		public bool IsBundleDownloaded(string bundleName)
		{
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(bundleName);
			return this.IsFileDownloaded(assetBundlePath);
		}

		// Token: 0x0600B723 RID: 46883 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
		}

		// Token: 0x0600B724 RID: 46884 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void PrepareRestart()
		{
		}

		// Token: 0x0600B725 RID: 46885 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void DoPostTasksAfterInitialDownload()
		{
		}

		// Token: 0x0600B726 RID: 46886 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void UnknownSourcesListener(string onOff)
		{
		}

		// Token: 0x0600B727 RID: 46887 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void InstallAPKListener(string onOff)
		{
		}

		// Token: 0x0600B728 RID: 46888 RVA: 0x003815C2 File Offset: 0x0037F7C2
		public void StopDownloads()
		{
			this.m_requestedTags = new string[0];
		}

		// Token: 0x040097D3 RID: 38867
		private const string LIVE_SERVICE_STRING = "Live";

		// Token: 0x040097E2 RID: 38882
		private EditorAssetDownloader.DownloadProgress m_downloadProgress = new EditorAssetDownloader.DownloadProgress();

		// Token: 0x040097E3 RID: 38883
		private bool m_initializeCalled;

		// Token: 0x040097E4 RID: 38884
		private float m_secondsUntilReady;

		// Token: 0x040097E5 RID: 38885
		private string[] m_requestedTags = new string[0];

		// Token: 0x040097E6 RID: 38886
		private IAssetManifest m_assetManifest;

		// Token: 0x040097E7 RID: 38887
		private string[] m_disabledAdventuresForStreaming = new string[0];

		// Token: 0x0200288E RID: 10382
		public enum DownloadMode
		{
			// Token: 0x0400FA22 RID: 64034
			None,
			// Token: 0x0400FA23 RID: 64035
			Local,
			// Token: 0x0400FA24 RID: 64036
			Build
		}

		// Token: 0x0200288F RID: 10383
		[Serializable]
		public class AssetBundleDownloadProgress
		{
			// Token: 0x0400FA25 RID: 64037
			public string Name;

			// Token: 0x0400FA26 RID: 64038
			public string Path;

			// Token: 0x0400FA27 RID: 64039
			public string[] Tags = new string[0];

			// Token: 0x0400FA28 RID: 64040
			public long BytesTotal;

			// Token: 0x0400FA29 RID: 64041
			public long BytesDownloaded;
		}

		// Token: 0x02002890 RID: 10384
		[Serializable]
		public class DownloadProgress
		{
			// Token: 0x0400FA2A RID: 64042
			public List<EditorAssetDownloader.AssetBundleDownloadProgress> AssetBundles = new List<EditorAssetDownloader.AssetBundleDownloadProgress>();
		}
	}
}
