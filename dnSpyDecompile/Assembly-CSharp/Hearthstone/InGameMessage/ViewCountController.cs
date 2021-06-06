using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001157 RID: 4439
	public class ViewCountController
	{
		// Token: 0x0600C2A1 RID: 49825 RVA: 0x003AF881 File Offset: 0x003ADA81
		public ViewCountController()
		{
			this.Deserialize();
		}

		// Token: 0x0600C2A2 RID: 49826 RVA: 0x003AF89C File Offset: 0x003ADA9C
		public int GetViewCount(string uid)
		{
			ViewCountController.ViewCountStruct viewCountStruct;
			if (this.m_viewCountDict.TryGetValue(uid, out viewCountStruct))
			{
				return viewCountStruct.m_viewCount;
			}
			return 0;
		}

		// Token: 0x0600C2A3 RID: 49827 RVA: 0x003AF8C4 File Offset: 0x003ADAC4
		public void IncreaseViewCount(string uid)
		{
			ViewCountController.ViewCountStruct value;
			if (this.m_viewCountDict.TryGetValue(uid, out value))
			{
				value.m_viewCount++;
				value.m_lastUpdate = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
				this.m_viewCountDict[uid] = value;
			}
			else
			{
				value = new ViewCountController.ViewCountStruct
				{
					m_viewCount = 1,
					m_lastUpdate = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now)
				};
				this.m_viewCountDict.Add(uid, value);
			}
			this.Serialize();
		}

		// Token: 0x0600C2A4 RID: 49828 RVA: 0x003AF943 File Offset: 0x003ADB43
		public void ClearViewCounts()
		{
			this.m_viewCountDict.Clear();
			this.Serialize();
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600C2A5 RID: 49829 RVA: 0x003AF956 File Offset: 0x003ADB56
		private static string ViewCountsPath
		{
			get
			{
				return Path.Combine(FileUtils.PersistentDataPath, "ViewCountsPath.json");
			}
		}

		// Token: 0x0600C2A6 RID: 49830 RVA: 0x003AF968 File Offset: 0x003ADB68
		private void Deserialize()
		{
			if (File.Exists(ViewCountController.ViewCountsPath))
			{
				ViewCountController.ViewCountClassSave viewCountClassSave;
				try
				{
					viewCountClassSave = JsonUtility.FromJson<ViewCountController.ViewCountClassSave>(File.ReadAllText(ViewCountController.ViewCountsPath));
				}
				catch (Exception ex)
				{
					Log.InGameMessage.PrintError("Unable to deserialize {0}: {1}", new object[]
					{
						"ViewCountsPath.json",
						ex
					});
					return;
				}
				viewCountClassSave.m_items.ForEach(delegate(ViewCountController.ViewCountStructSave x)
				{
					if (TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now) - x.m_lastUpdate < 4838400.0)
					{
						this.m_viewCountDict.Add(x.m_uid, new ViewCountController.ViewCountStruct
						{
							m_viewCount = x.m_viewCount,
							m_lastUpdate = x.m_lastUpdate
						});
					}
				});
			}
		}

		// Token: 0x0600C2A7 RID: 49831 RVA: 0x003AF9E0 File Offset: 0x003ADBE0
		private void Serialize()
		{
			ViewCountController.ViewCountClassSave viewCountClassSave = new ViewCountController.ViewCountClassSave();
			foreach (KeyValuePair<string, ViewCountController.ViewCountStruct> keyValuePair in this.m_viewCountDict)
			{
				viewCountClassSave.m_items.Add(new ViewCountController.ViewCountStructSave
				{
					m_uid = keyValuePair.Key,
					m_viewCount = keyValuePair.Value.m_viewCount,
					m_lastUpdate = keyValuePair.Value.m_lastUpdate
				});
			}
			try
			{
				string contents = JsonUtility.ToJson(viewCountClassSave, !HearthstoneApplication.IsPublic());
				File.WriteAllText(ViewCountController.ViewCountsPath, contents);
			}
			catch (Exception ex)
			{
				Log.InGameMessage.PrintError("Unable to serialize {0}: {1}", new object[]
				{
					"ViewCountsPath.json",
					ex
				});
			}
		}

		// Token: 0x04009CB1 RID: 40113
		private const string VIEW_COUNT_FILENAME = "ViewCountsPath.json";

		// Token: 0x04009CB2 RID: 40114
		private const double IGM_VALID_VIEWCOUNTS_SECONDS = 4838400.0;

		// Token: 0x04009CB3 RID: 40115
		private Dictionary<string, ViewCountController.ViewCountStruct> m_viewCountDict = new Dictionary<string, ViewCountController.ViewCountStruct>();

		// Token: 0x02002922 RID: 10530
		[Serializable]
		private struct ViewCountStructSave
		{
			// Token: 0x0400FBDC RID: 64476
			public string m_uid;

			// Token: 0x0400FBDD RID: 64477
			public int m_viewCount;

			// Token: 0x0400FBDE RID: 64478
			public ulong m_lastUpdate;
		}

		// Token: 0x02002923 RID: 10531
		[Serializable]
		private class ViewCountClassSave
		{
			// Token: 0x0400FBDF RID: 64479
			public List<ViewCountController.ViewCountStructSave> m_items = new List<ViewCountController.ViewCountStructSave>();
		}

		// Token: 0x02002924 RID: 10532
		private struct ViewCountStruct
		{
			// Token: 0x0400FBE0 RID: 64480
			public int m_viewCount;

			// Token: 0x0400FBE1 RID: 64481
			public ulong m_lastUpdate;
		}
	}
}
