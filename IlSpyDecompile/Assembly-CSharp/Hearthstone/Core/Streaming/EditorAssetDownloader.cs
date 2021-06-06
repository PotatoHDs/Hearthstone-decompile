using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

namespace Hearthstone.Core.Streaming
{
	public class EditorAssetDownloader : IAssetDownloader
	{
		public enum DownloadMode
		{
			None,
			Local,
			Build
		}

		[Serializable]
		public class AssetBundleDownloadProgress
		{
			public string Name;

			public string Path;

			public string[] Tags = new string[0];

			public long BytesTotal;

			public long BytesDownloaded;
		}

		[Serializable]
		public class DownloadProgress
		{
			public List<AssetBundleDownloadProgress> AssetBundles = new List<AssetBundleDownloadProgress>();
		}

		private const string LIVE_SERVICE_STRING = "Live";

		private DownloadProgress m_downloadProgress = new DownloadProgress();

		private bool m_initializeCalled;

		private float m_secondsUntilReady;

		private string[] m_requestedTags = new string[0];

		private IAssetManifest m_assetManifest;

		private string[] m_disabledAdventuresForStreaming = new string[0];

		public static string SerializedPath => Path.Combine(FileUtils.PersistentDataPath, "EditorAssetDownloader_DownloadProgress.json");

		public AssetDownloaderState State { get; private set; }

		public string AgentLogPath => null;

		public bool IsReady { get; private set; }

		public bool IsNewMobileVersionReleased { get; private set; }

		public bool IsRunningNewerBinaryThanLive { get; private set; }

		public bool IsVersionChanged { get; private set; }

		public bool IsAssetManifestReady { get; private set; }

		public bool DownloadAllFinished { get; set; }

		public bool DisabledErrorMessageDialog { get; set; }

		public string PatchOverrideUrl { get; private set; }

		public string VersionOverrideUrl { get; private set; }

		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				return m_disabledAdventuresForStreaming;
			}
			private set
			{
				m_disabledAdventuresForStreaming = value;
			}
		}

		public double BytesPerSecond { get; private set; }

		public int MaxDownloadSpeed { get; set; }

		public int InGameStreamingDefaultSpeed { get; set; }

		public int DownloadSpeedInGame { get; set; }

		public static DownloadMode Mode => DownloadMode.None;

		public static int BuildNumber => 0;

		public static int FakeDownloadSpeedBytesPerSecond => 0;

		public bool Initialize()
		{
			PatchOverrideUrl = "Live";
			VersionOverrideUrl = "Live";
			State = AssetDownloaderState.UNINITIALIZED;
			m_assetManifest = null;
			switch (Mode)
			{
			case DownloadMode.Local:
				m_assetManifest = AssetManifest.Get();
				break;
			case DownloadMode.Build:
				m_assetManifest = AssetManifest.Get();
				break;
			}
			if (m_assetManifest == null)
			{
				Log.Downloader.PrintError("EditorAssetDownloader: Failed to load asset manifest");
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLOBAL_ERROR_ASSET_MANIFEST"), GameStrings.Get("GLOBAL_QUIT"), delegate
				{
					ShutdownApplication();
				});
				return false;
			}
			m_initializeCalled = true;
			m_secondsUntilReady = 1f;
			IsNewMobileVersionReleased = false;
			IsReady = true;
			IsAssetManifestReady = true;
			IsRunningNewerBinaryThanLive = false;
			DeserializeDownloadProgress();
			return true;
		}

		private void ShutdownApplication()
		{
			Log.Downloader.PrintInfo("ShutdownApplication");
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.Exit();
			}
			else
			{
				GeneralUtils.ExitApplication();
			}
		}

		public void Update(bool firstCall)
		{
			if (!m_initializeCalled)
			{
				return;
			}
			switch (State)
			{
			case AssetDownloaderState.UNINITIALIZED:
				m_secondsUntilReady -= Time.deltaTime;
				if (m_secondsUntilReady <= 0f)
				{
					m_secondsUntilReady = 0f;
					State = AssetDownloaderState.IDLE;
				}
				break;
			case AssetDownloaderState.IDLE:
				if (m_requestedTags != null && m_requestedTags.Length != 0)
				{
					State = AssetDownloaderState.DOWNLOADING;
				}
				break;
			case AssetDownloaderState.DOWNLOADING:
				if (m_requestedTags == null || m_requestedTags.Length == 0)
				{
					State = AssetDownloaderState.IDLE;
					break;
				}
				switch (Mode)
				{
				case DownloadMode.Local:
					UpdateLocal();
					break;
				case DownloadMode.Build:
					UpdateBuild();
					break;
				}
				break;
			}
			TrySerializeDownloadProgress();
		}

		private void UpdateLocal()
		{
			BytesPerSecond = 0.0;
			AssetBundleDownloadProgress[] allProgressForRequestedTags = GetAllProgressForRequestedTags();
			long num = 0L;
			long num2 = 0L;
			AssetBundleDownloadProgress[] array = allProgressForRequestedTags;
			foreach (AssetBundleDownloadProgress assetBundleDownloadProgress in array)
			{
				if (assetBundleDownloadProgress.BytesDownloaded < assetBundleDownloadProgress.BytesTotal)
				{
					assetBundleDownloadProgress.BytesDownloaded += (long)((float)FakeDownloadSpeedBytesPerSecond * Time.deltaTime);
					assetBundleDownloadProgress.BytesDownloaded = Math.Min(assetBundleDownloadProgress.BytesDownloaded, assetBundleDownloadProgress.BytesTotal);
					BytesPerSecond = FakeDownloadSpeedBytesPerSecond;
					num += assetBundleDownloadProgress.BytesDownloaded;
					num2 += assetBundleDownloadProgress.BytesTotal;
				}
			}
			if (num2 == num)
			{
				DownloadAllFinished = true;
			}
		}

		private void UpdateBuild()
		{
			AssetBundleDownloadProgress[] allProgressForRequestedTags = GetAllProgressForRequestedTags();
			for (int i = 0; i < allProgressForRequestedTags.Length; i++)
			{
				_ = allProgressForRequestedTags[i];
			}
		}

		private List<string> FindFirstIncompleteTagSet(List<string> parentTags, List<List<string>> requestedTagGroups, int tagGroupIndex)
		{
			if (tagGroupIndex == requestedTagGroups.Count)
			{
				if (!GetDownloadStatus(parentTags.ToArray()).Complete)
				{
					return parentTags;
				}
				return null;
			}
			foreach (string item in requestedTagGroups[tagGroupIndex])
			{
				List<string> parentTags2 = new List<string>(parentTags) { item };
				List<string> list = FindFirstIncompleteTagSet(parentTags2, requestedTagGroups, tagGroupIndex + 1);
				if (list != null)
				{
					return list;
				}
			}
			return null;
		}

		private AssetBundleDownloadProgress[] GetAllProgressForRequestedTags()
		{
			List<List<string>> list = new List<List<string>>();
			string[] tagGroups = m_assetManifest.GetTagGroups();
			foreach (string tagGroup in tagGroups)
			{
				List<string> list2 = new List<string>();
				string[] tagsInTagGroup = m_assetManifest.GetTagsInTagGroup(tagGroup);
				foreach (string text in tagsInTagGroup)
				{
					if (m_requestedTags.Contains(text))
					{
						list2.Add(text);
					}
				}
				if (list2.Count > 0)
				{
					list.Add(list2);
				}
			}
			list.Add(new List<string> { Localization.GetLocale().ToString() });
			List<string> requestedTags = FindFirstIncompleteTagSet(new List<string>(), list, 0);
			if (requestedTags == null)
			{
				return new AssetBundleDownloadProgress[0];
			}
			return m_downloadProgress.AssetBundles.Where((AssetBundleDownloadProgress bundleProgress) => QueryTags(bundleProgress.Tags, requestedTags)).ToArray();
		}

		private bool QueryTags(ICollection<string> targetTags, ICollection<string> queryTags)
		{
			if (targetTags == null || targetTags.Count == 0)
			{
				return false;
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string targetTag in targetTags)
			{
				hashSet.Add(m_assetManifest.GetTagGroupForTag(targetTag));
			}
			foreach (string queryTag in queryTags)
			{
				string tagGroupForTag = m_assetManifest.GetTagGroupForTag(queryTag);
				if (targetTags.Contains(queryTag))
				{
					hashSet.Remove(tagGroupForTag);
				}
			}
			return hashSet.Count == 0;
		}

		private void DeserializeDownloadProgress()
		{
			m_downloadProgress = DeserializeProgress();
			if (m_downloadProgress == null)
			{
				m_downloadProgress = new DownloadProgress();
			}
			DownloadMode mode = Mode;
			if ((uint)(mode - 1) > 1u)
			{
				return;
			}
			string[] assetBundleNames = m_assetManifest.GetAllAssetBundleNames();
			m_downloadProgress.AssetBundles = m_downloadProgress.AssetBundles.Where((AssetBundleDownloadProgress a) => assetBundleNames.Contains(a.Name)).ToList();
			string[] array = assetBundleNames;
			foreach (string text in array)
			{
				string bundlePath = AssetBundleInfo.GetAssetBundlePath(text);
				AssetBundleDownloadProgress assetBundleDownloadProgress = m_downloadProgress.AssetBundles.FirstOrDefault((AssetBundleDownloadProgress a) => a.Path == bundlePath);
				if (assetBundleDownloadProgress == null)
				{
					assetBundleDownloadProgress = new AssetBundleDownloadProgress();
					assetBundleDownloadProgress.Name = text;
					assetBundleDownloadProgress.Path = bundlePath;
					m_downloadProgress.AssetBundles.Add(assetBundleDownloadProgress);
					assetBundleDownloadProgress.BytesTotal = (File.Exists(bundlePath) ? new FileInfo(bundlePath).Length : (UnityEngine.Random.Range(100, 200) * 1024 * 1024));
				}
				List<string> list = new List<string>(m_assetManifest.GetTagsFromAssetBundle(assetBundleDownloadProgress.Name));
				string localeFromAssetBundle = UpdateUtils.GetLocaleFromAssetBundle(assetBundleDownloadProgress.Name);
				if (!string.IsNullOrEmpty(localeFromAssetBundle))
				{
					list.Add(localeFromAssetBundle);
				}
				assetBundleDownloadProgress.Tags = list.ToArray();
				assetBundleDownloadProgress.BytesDownloaded = Math.Min(assetBundleDownloadProgress.BytesDownloaded, assetBundleDownloadProgress.BytesTotal);
				if (text.StartsWith("local") && Mode == DownloadMode.Local)
				{
					assetBundleDownloadProgress.BytesDownloaded = assetBundleDownloadProgress.BytesTotal;
				}
			}
			m_downloadProgress.AssetBundles.Sort((AssetBundleDownloadProgress a, AssetBundleDownloadProgress b) => a.Name.CompareTo(b.Name));
		}

		public static DownloadProgress DeserializeProgress()
		{
			if (File.Exists(SerializedPath))
			{
				return JsonUtility.FromJson<DownloadProgress>(File.ReadAllText(SerializedPath));
			}
			return null;
		}

		public static void ResetDownloadProgress()
		{
			if (File.Exists(SerializedPath))
			{
				File.Delete(SerializedPath);
			}
		}

		private void TrySerializeDownloadProgress()
		{
			if (State != AssetDownloaderState.UNINITIALIZED && m_downloadProgress.AssetBundles.Any((AssetBundleDownloadProgress a) => a.BytesDownloaded < a.BytesTotal) && m_requestedTags != null && m_requestedTags.Length != 0)
			{
				string contents = JsonUtility.ToJson(m_downloadProgress, prettyPrint: true);
				File.WriteAllText(SerializedPath, contents);
			}
		}

		public void Shutdown()
		{
			m_requestedTags = new string[0];
			m_initializeCalled = false;
			State = AssetDownloaderState.UNINITIALIZED;
		}

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
			switch (Mode)
			{
			case DownloadMode.None:
				tagDownloadStatus.BytesTotal = (tagDownloadStatus.BytesDownloaded = 1L);
				break;
			case DownloadMode.Local:
			case DownloadMode.Build:
				foreach (AssetBundleDownloadProgress assetBundle in m_downloadProgress.AssetBundles)
				{
					if (QueryTags(assetBundle.Tags, tagDownloadStatus.Tags))
					{
						tagDownloadStatus.BytesTotal += assetBundle.BytesTotal;
						tagDownloadStatus.BytesDownloaded += assetBundle.BytesDownloaded;
					}
				}
				break;
			}
			if (tagDownloadStatus.BytesTotal == 0L)
			{
				tagDownloadStatus.Complete = true;
			}
			return tagDownloadStatus;
		}

		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			return GetDownloadStatus(m_requestedTags);
		}

		public void StartDownload(string[] tags, bool initialDownload, bool localeChanged)
		{
			m_requestedTags = tags;
			DownloadAllFinished = false;
		}

		public void PauseAllDownloads()
		{
			m_requestedTags = new string[0];
		}

		public void DeleteDownloadedData()
		{
			if (Directory.Exists("Final/Data"))
			{
				Directory.Delete("Final/Data", recursive: true);
			}
		}

		public bool IsFileDownloaded(string filePath)
		{
			switch (Mode)
			{
			case DownloadMode.None:
				return true;
			case DownloadMode.Local:
			case DownloadMode.Build:
			{
				AssetBundleDownloadProgress assetBundleDownloadProgress = m_downloadProgress.AssetBundles.FirstOrDefault((AssetBundleDownloadProgress a) => a.Path == filePath);
				if (assetBundleDownloadProgress != null)
				{
					return assetBundleDownloadProgress.BytesTotal == assetBundleDownloadProgress.BytesDownloaded;
				}
				return false;
			}
			default:
				return false;
			}
		}

		public bool IsBundleDownloaded(string bundleName)
		{
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(bundleName);
			return IsFileDownloaded(assetBundlePath);
		}

		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
		}

		public void PrepareRestart()
		{
		}

		public void DoPostTasksAfterInitialDownload()
		{
		}

		public void UnknownSourcesListener(string onOff)
		{
		}

		public void InstallAPKListener(string onOff)
		{
		}

		public void StopDownloads()
		{
			m_requestedTags = new string[0];
		}
	}
}
