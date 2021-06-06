using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	public class ViewCountController
	{
		[Serializable]
		private struct ViewCountStructSave
		{
			public string m_uid;

			public int m_viewCount;

			public ulong m_lastUpdate;
		}

		[Serializable]
		private class ViewCountClassSave
		{
			public List<ViewCountStructSave> m_items = new List<ViewCountStructSave>();
		}

		private struct ViewCountStruct
		{
			public int m_viewCount;

			public ulong m_lastUpdate;
		}

		private const string VIEW_COUNT_FILENAME = "ViewCountsPath.json";

		private const double IGM_VALID_VIEWCOUNTS_SECONDS = 4838400.0;

		private Dictionary<string, ViewCountStruct> m_viewCountDict = new Dictionary<string, ViewCountStruct>();

		private static string ViewCountsPath => Path.Combine(FileUtils.PersistentDataPath, "ViewCountsPath.json");

		public ViewCountController()
		{
			Deserialize();
		}

		public int GetViewCount(string uid)
		{
			if (m_viewCountDict.TryGetValue(uid, out var value))
			{
				return value.m_viewCount;
			}
			return 0;
		}

		public void IncreaseViewCount(string uid)
		{
			if (m_viewCountDict.TryGetValue(uid, out var value))
			{
				value.m_viewCount++;
				value.m_lastUpdate = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
				m_viewCountDict[uid] = value;
			}
			else
			{
				ViewCountStruct viewCountStruct = default(ViewCountStruct);
				viewCountStruct.m_viewCount = 1;
				viewCountStruct.m_lastUpdate = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
				value = viewCountStruct;
				m_viewCountDict.Add(uid, value);
			}
			Serialize();
		}

		public void ClearViewCounts()
		{
			m_viewCountDict.Clear();
			Serialize();
		}

		private void Deserialize()
		{
			if (!File.Exists(ViewCountsPath))
			{
				return;
			}
			ViewCountClassSave viewCountClassSave;
			try
			{
				viewCountClassSave = JsonUtility.FromJson<ViewCountClassSave>(File.ReadAllText(ViewCountsPath));
			}
			catch (Exception ex)
			{
				Log.InGameMessage.PrintError("Unable to deserialize {0}: {1}", "ViewCountsPath.json", ex);
				return;
			}
			viewCountClassSave.m_items.ForEach(delegate(ViewCountStructSave x)
			{
				if ((double)(TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now) - x.m_lastUpdate) < 4838400.0)
				{
					m_viewCountDict.Add(x.m_uid, new ViewCountStruct
					{
						m_viewCount = x.m_viewCount,
						m_lastUpdate = x.m_lastUpdate
					});
				}
			});
		}

		private void Serialize()
		{
			ViewCountClassSave viewCountClassSave = new ViewCountClassSave();
			foreach (KeyValuePair<string, ViewCountStruct> item in m_viewCountDict)
			{
				viewCountClassSave.m_items.Add(new ViewCountStructSave
				{
					m_uid = item.Key,
					m_viewCount = item.Value.m_viewCount,
					m_lastUpdate = item.Value.m_lastUpdate
				});
			}
			try
			{
				string contents = JsonUtility.ToJson(viewCountClassSave, !HearthstoneApplication.IsPublic());
				File.WriteAllText(ViewCountsPath, contents);
			}
			catch (Exception ex)
			{
				Log.InGameMessage.PrintError("Unable to serialize {0}: {1}", "ViewCountsPath.json", ex);
			}
		}
	}
}
