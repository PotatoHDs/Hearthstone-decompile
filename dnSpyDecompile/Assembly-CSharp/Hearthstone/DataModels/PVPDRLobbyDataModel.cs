using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D0 RID: 4304
	public class PVPDRLobbyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600BC47 RID: 48199 RVA: 0x0039754F File Offset: 0x0039574F
		public int DataModelId
		{
			get
			{
				return 181;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600BC48 RID: 48200 RVA: 0x00397556 File Offset: 0x00395756
		public string DataModelDisplayName
		{
			get
			{
				return "pvpdrlobby";
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600BC4A RID: 48202 RVA: 0x00397583 File Offset: 0x00395783
		// (set) Token: 0x0600BC49 RID: 48201 RVA: 0x0039755D File Offset: 0x0039575D
		public bool HasSession
		{
			get
			{
				return this.m_HasSession;
			}
			set
			{
				if (this.m_HasSession == value)
				{
					return;
				}
				this.m_HasSession = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600BC4C RID: 48204 RVA: 0x003975B1 File Offset: 0x003957B1
		// (set) Token: 0x0600BC4B RID: 48203 RVA: 0x0039758B File Offset: 0x0039578B
		public int Season
		{
			get
			{
				return this.m_Season;
			}
			set
			{
				if (this.m_Season == value)
				{
					return;
				}
				this.m_Season = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600BC4E RID: 48206 RVA: 0x003975DF File Offset: 0x003957DF
		// (set) Token: 0x0600BC4D RID: 48205 RVA: 0x003975B9 File Offset: 0x003957B9
		public int Wins
		{
			get
			{
				return this.m_Wins;
			}
			set
			{
				if (this.m_Wins == value)
				{
					return;
				}
				this.m_Wins = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600BC50 RID: 48208 RVA: 0x0039760D File Offset: 0x0039580D
		// (set) Token: 0x0600BC4F RID: 48207 RVA: 0x003975E7 File Offset: 0x003957E7
		public int Losses
		{
			get
			{
				return this.m_Losses;
			}
			set
			{
				if (this.m_Losses == value)
				{
					return;
				}
				this.m_Losses = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600BC52 RID: 48210 RVA: 0x0039763B File Offset: 0x0039583B
		// (set) Token: 0x0600BC51 RID: 48209 RVA: 0x00397615 File Offset: 0x00395815
		public int Rating
		{
			get
			{
				return this.m_Rating;
			}
			set
			{
				if (this.m_Rating == value)
				{
					return;
				}
				this.m_Rating = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600BC54 RID: 48212 RVA: 0x0039766E File Offset: 0x0039586E
		// (set) Token: 0x0600BC53 RID: 48211 RVA: 0x00397643 File Offset: 0x00395843
		public string TimeRemainingString
		{
			get
			{
				return this.m_TimeRemainingString;
			}
			set
			{
				if (this.m_TimeRemainingString == value)
				{
					return;
				}
				this.m_TimeRemainingString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600BC56 RID: 48214 RVA: 0x0039769C File Offset: 0x0039589C
		// (set) Token: 0x0600BC55 RID: 48213 RVA: 0x00397676 File Offset: 0x00395876
		public int PaidRating
		{
			get
			{
				return this.m_PaidRating;
			}
			set
			{
				if (this.m_PaidRating == value)
				{
					return;
				}
				this.m_PaidRating = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600BC58 RID: 48216 RVA: 0x003976CA File Offset: 0x003958CA
		// (set) Token: 0x0600BC57 RID: 48215 RVA: 0x003976A4 File Offset: 0x003958A4
		public int HighWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
			set
			{
				if (this.m_HighWatermark == value)
				{
					return;
				}
				this.m_HighWatermark = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600BC5A RID: 48218 RVA: 0x003976FD File Offset: 0x003958FD
		// (set) Token: 0x0600BC59 RID: 48217 RVA: 0x003976D2 File Offset: 0x003958D2
		public string Unlocks
		{
			get
			{
				return this.m_Unlocks;
			}
			set
			{
				if (this.m_Unlocks == value)
				{
					return;
				}
				this.m_Unlocks = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600BC5C RID: 48220 RVA: 0x0039772B File Offset: 0x0039592B
		// (set) Token: 0x0600BC5B RID: 48219 RVA: 0x00397705 File Offset: 0x00395905
		public bool RecentWin
		{
			get
			{
				return this.m_RecentWin;
			}
			set
			{
				if (this.m_RecentWin == value)
				{
					return;
				}
				this.m_RecentWin = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600BC5E RID: 48222 RVA: 0x00397759 File Offset: 0x00395959
		// (set) Token: 0x0600BC5D RID: 48221 RVA: 0x00397733 File Offset: 0x00395933
		public bool RecentLoss
		{
			get
			{
				return this.m_RecentLoss;
			}
			set
			{
				if (this.m_RecentLoss == value)
				{
					return;
				}
				this.m_RecentLoss = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600BC60 RID: 48224 RVA: 0x00397787 File Offset: 0x00395987
		// (set) Token: 0x0600BC5F RID: 48223 RVA: 0x00397761 File Offset: 0x00395961
		public bool IsEarlyAccess
		{
			get
			{
				return this.m_IsEarlyAccess;
			}
			set
			{
				if (this.m_IsEarlyAccess == value)
				{
					return;
				}
				this.m_IsEarlyAccess = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600BC62 RID: 48226 RVA: 0x003977B5 File Offset: 0x003959B5
		// (set) Token: 0x0600BC61 RID: 48225 RVA: 0x0039778F File Offset: 0x0039598F
		public bool IsPaidEntry
		{
			get
			{
				return this.m_IsPaidEntry;
			}
			set
			{
				if (this.m_IsPaidEntry == value)
				{
					return;
				}
				this.m_IsPaidEntry = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600BC64 RID: 48228 RVA: 0x003977E3 File Offset: 0x003959E3
		// (set) Token: 0x0600BC63 RID: 48227 RVA: 0x003977BD File Offset: 0x003959BD
		public bool IsSessionActive
		{
			get
			{
				return this.m_IsSessionActive;
			}
			set
			{
				if (this.m_IsSessionActive == value)
				{
					return;
				}
				this.m_IsSessionActive = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600BC66 RID: 48230 RVA: 0x00397811 File Offset: 0x00395A11
		// (set) Token: 0x0600BC65 RID: 48229 RVA: 0x003977EB File Offset: 0x003959EB
		public int LastRatingChange
		{
			get
			{
				return this.m_LastRatingChange;
			}
			set
			{
				if (this.m_LastRatingChange == value)
				{
					return;
				}
				this.m_LastRatingChange = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600BC68 RID: 48232 RVA: 0x0039783F File Offset: 0x00395A3F
		// (set) Token: 0x0600BC67 RID: 48231 RVA: 0x00397819 File Offset: 0x00395A19
		public bool IsPaidUnlocked
		{
			get
			{
				return this.m_IsPaidUnlocked;
			}
			set
			{
				if (this.m_IsPaidUnlocked == value)
				{
					return;
				}
				this.m_IsPaidUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600BC6A RID: 48234 RVA: 0x0039786D File Offset: 0x00395A6D
		// (set) Token: 0x0600BC69 RID: 48233 RVA: 0x00397847 File Offset: 0x00395A47
		public bool IsFreeUnlocked
		{
			get
			{
				return this.m_IsFreeUnlocked;
			}
			set
			{
				if (this.m_IsFreeUnlocked == value)
				{
					return;
				}
				this.m_IsFreeUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600BC6C RID: 48236 RVA: 0x003978A0 File Offset: 0x00395AA0
		// (set) Token: 0x0600BC6B RID: 48235 RVA: 0x00397875 File Offset: 0x00395A75
		public string NoticeHeaderString
		{
			get
			{
				return this.m_NoticeHeaderString;
			}
			set
			{
				if (this.m_NoticeHeaderString == value)
				{
					return;
				}
				this.m_NoticeHeaderString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600BC6E RID: 48238 RVA: 0x003978D3 File Offset: 0x00395AD3
		// (set) Token: 0x0600BC6D RID: 48237 RVA: 0x003978A8 File Offset: 0x00395AA8
		public string NoticeDescString
		{
			get
			{
				return this.m_NoticeDescString;
			}
			set
			{
				if (this.m_NoticeDescString == value)
				{
					return;
				}
				this.m_NoticeDescString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600BC70 RID: 48240 RVA: 0x00397901 File Offset: 0x00395B01
		// (set) Token: 0x0600BC6F RID: 48239 RVA: 0x003978DB File Offset: 0x00395ADB
		public int LastPlayedMode
		{
			get
			{
				return this.m_LastPlayedMode;
			}
			set
			{
				if (this.m_LastPlayedMode == value)
				{
					return;
				}
				this.m_LastPlayedMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600BC72 RID: 48242 RVA: 0x0039792F File Offset: 0x00395B2F
		// (set) Token: 0x0600BC71 RID: 48241 RVA: 0x00397909 File Offset: 0x00395B09
		public bool IsRatingNotice
		{
			get
			{
				return this.m_IsRatingNotice;
			}
			set
			{
				if (this.m_IsRatingNotice == value)
				{
					return;
				}
				this.m_IsRatingNotice = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600BC74 RID: 48244 RVA: 0x00397962 File Offset: 0x00395B62
		// (set) Token: 0x0600BC73 RID: 48243 RVA: 0x00397937 File Offset: 0x00395B37
		public string NoticeRatingString
		{
			get
			{
				return this.m_NoticeRatingString;
			}
			set
			{
				if (this.m_NoticeRatingString == value)
				{
					return;
				}
				this.m_NoticeRatingString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600BC75 RID: 48245 RVA: 0x0039796A File Offset: 0x00395B6A
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC76 RID: 48246 RVA: 0x00397974 File Offset: 0x00395B74
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool hasSession = this.m_HasSession;
			int num2 = (num + this.m_HasSession.GetHashCode()) * 31;
			int season = this.m_Season;
			int num3 = (num2 + this.m_Season.GetHashCode()) * 31;
			int wins = this.m_Wins;
			int num4 = (num3 + this.m_Wins.GetHashCode()) * 31;
			int losses = this.m_Losses;
			int num5 = (num4 + this.m_Losses.GetHashCode()) * 31;
			int rating = this.m_Rating;
			int num6 = ((num5 + this.m_Rating.GetHashCode()) * 31 + ((this.m_TimeRemainingString != null) ? this.m_TimeRemainingString.GetHashCode() : 0)) * 31;
			int paidRating = this.m_PaidRating;
			int num7 = (num6 + this.m_PaidRating.GetHashCode()) * 31;
			int highWatermark = this.m_HighWatermark;
			int num8 = ((num7 + this.m_HighWatermark.GetHashCode()) * 31 + ((this.m_Unlocks != null) ? this.m_Unlocks.GetHashCode() : 0)) * 31;
			bool recentWin = this.m_RecentWin;
			int num9 = (num8 + this.m_RecentWin.GetHashCode()) * 31;
			bool recentLoss = this.m_RecentLoss;
			int num10 = (num9 + this.m_RecentLoss.GetHashCode()) * 31;
			bool isEarlyAccess = this.m_IsEarlyAccess;
			int num11 = (num10 + this.m_IsEarlyAccess.GetHashCode()) * 31;
			bool isPaidEntry = this.m_IsPaidEntry;
			int num12 = (num11 + this.m_IsPaidEntry.GetHashCode()) * 31;
			bool isSessionActive = this.m_IsSessionActive;
			int num13 = (num12 + this.m_IsSessionActive.GetHashCode()) * 31;
			int lastRatingChange = this.m_LastRatingChange;
			int num14 = (num13 + this.m_LastRatingChange.GetHashCode()) * 31;
			bool isPaidUnlocked = this.m_IsPaidUnlocked;
			int num15 = (num14 + this.m_IsPaidUnlocked.GetHashCode()) * 31;
			bool isFreeUnlocked = this.m_IsFreeUnlocked;
			int num16 = (((num15 + this.m_IsFreeUnlocked.GetHashCode()) * 31 + ((this.m_NoticeHeaderString != null) ? this.m_NoticeHeaderString.GetHashCode() : 0)) * 31 + ((this.m_NoticeDescString != null) ? this.m_NoticeDescString.GetHashCode() : 0)) * 31;
			int lastPlayedMode = this.m_LastPlayedMode;
			int num17 = (num16 + this.m_LastPlayedMode.GetHashCode()) * 31;
			bool isRatingNotice = this.m_IsRatingNotice;
			return (num17 + this.m_IsRatingNotice.GetHashCode()) * 31 + ((this.m_NoticeRatingString != null) ? this.m_NoticeRatingString.GetHashCode() : 0);
		}

		// Token: 0x0600BC77 RID: 48247 RVA: 0x00397B7C File Offset: 0x00395D7C
		public bool GetPropertyValue(int id, out object value)
		{
			if (id <= 212)
			{
				switch (id)
				{
				case 182:
					value = this.m_HasSession;
					return true;
				case 183:
					value = this.m_Season;
					return true;
				case 184:
					value = this.m_Wins;
					return true;
				case 185:
					value = this.m_Losses;
					return true;
				case 186:
					value = this.m_Rating;
					return true;
				case 187:
					value = this.m_TimeRemainingString;
					return true;
				case 188:
					value = this.m_PaidRating;
					return true;
				default:
					if (id == 211)
					{
						value = this.m_HighWatermark;
						return true;
					}
					if (id == 212)
					{
						value = this.m_Unlocks;
						return true;
					}
					break;
				}
			}
			else if (id <= 220)
			{
				if (id == 219)
				{
					value = this.m_RecentWin;
					return true;
				}
				if (id == 220)
				{
					value = this.m_RecentLoss;
					return true;
				}
			}
			else
			{
				switch (id)
				{
				case 235:
					value = this.m_IsEarlyAccess;
					return true;
				case 236:
				case 238:
				case 239:
				case 240:
				case 242:
					break;
				case 237:
					value = this.m_IsPaidEntry;
					return true;
				case 241:
					value = this.m_IsSessionActive;
					return true;
				case 243:
					value = this.m_LastRatingChange;
					return true;
				case 244:
					value = this.m_IsPaidUnlocked;
					return true;
				case 245:
					value = this.m_IsFreeUnlocked;
					return true;
				default:
					switch (id)
					{
					case 257:
						value = this.m_NoticeHeaderString;
						return true;
					case 258:
						value = this.m_NoticeDescString;
						return true;
					case 260:
						value = this.m_LastPlayedMode;
						return true;
					case 261:
						value = this.m_IsRatingNotice;
						return true;
					case 262:
						value = this.m_NoticeRatingString;
						return true;
					}
					break;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600BC78 RID: 48248 RVA: 0x00397D90 File Offset: 0x00395F90
		public bool SetPropertyValue(int id, object value)
		{
			if (id <= 212)
			{
				switch (id)
				{
				case 182:
					this.HasSession = (value != null && (bool)value);
					return true;
				case 183:
					this.Season = ((value != null) ? ((int)value) : 0);
					return true;
				case 184:
					this.Wins = ((value != null) ? ((int)value) : 0);
					return true;
				case 185:
					this.Losses = ((value != null) ? ((int)value) : 0);
					return true;
				case 186:
					this.Rating = ((value != null) ? ((int)value) : 0);
					return true;
				case 187:
					this.TimeRemainingString = ((value != null) ? ((string)value) : null);
					return true;
				case 188:
					this.PaidRating = ((value != null) ? ((int)value) : 0);
					return true;
				default:
					if (id == 211)
					{
						this.HighWatermark = ((value != null) ? ((int)value) : 0);
						return true;
					}
					if (id == 212)
					{
						this.Unlocks = ((value != null) ? ((string)value) : null);
						return true;
					}
					break;
				}
			}
			else if (id <= 220)
			{
				if (id == 219)
				{
					this.RecentWin = (value != null && (bool)value);
					return true;
				}
				if (id == 220)
				{
					this.RecentLoss = (value != null && (bool)value);
					return true;
				}
			}
			else
			{
				switch (id)
				{
				case 235:
					this.IsEarlyAccess = (value != null && (bool)value);
					return true;
				case 236:
				case 238:
				case 239:
				case 240:
				case 242:
					break;
				case 237:
					this.IsPaidEntry = (value != null && (bool)value);
					return true;
				case 241:
					this.IsSessionActive = (value != null && (bool)value);
					return true;
				case 243:
					this.LastRatingChange = ((value != null) ? ((int)value) : 0);
					return true;
				case 244:
					this.IsPaidUnlocked = (value != null && (bool)value);
					return true;
				case 245:
					this.IsFreeUnlocked = (value != null && (bool)value);
					return true;
				default:
					switch (id)
					{
					case 257:
						this.NoticeHeaderString = ((value != null) ? ((string)value) : null);
						return true;
					case 258:
						this.NoticeDescString = ((value != null) ? ((string)value) : null);
						return true;
					case 260:
						this.LastPlayedMode = ((value != null) ? ((int)value) : 0);
						return true;
					case 261:
						this.IsRatingNotice = (value != null && (bool)value);
						return true;
					case 262:
						this.NoticeRatingString = ((value != null) ? ((string)value) : null);
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600BC79 RID: 48249 RVA: 0x00398028 File Offset: 0x00396228
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id <= 212)
			{
				switch (id)
				{
				case 182:
					info = this.Properties[0];
					return true;
				case 183:
					info = this.Properties[1];
					return true;
				case 184:
					info = this.Properties[2];
					return true;
				case 185:
					info = this.Properties[3];
					return true;
				case 186:
					info = this.Properties[4];
					return true;
				case 187:
					info = this.Properties[5];
					return true;
				case 188:
					info = this.Properties[6];
					return true;
				default:
					if (id == 211)
					{
						info = this.Properties[7];
						return true;
					}
					if (id == 212)
					{
						info = this.Properties[8];
						return true;
					}
					break;
				}
			}
			else if (id <= 220)
			{
				if (id == 219)
				{
					info = this.Properties[9];
					return true;
				}
				if (id == 220)
				{
					info = this.Properties[10];
					return true;
				}
			}
			else
			{
				switch (id)
				{
				case 235:
					info = this.Properties[11];
					return true;
				case 236:
				case 238:
				case 239:
				case 240:
				case 242:
					break;
				case 237:
					info = this.Properties[12];
					return true;
				case 241:
					info = this.Properties[13];
					return true;
				case 243:
					info = this.Properties[14];
					return true;
				case 244:
					info = this.Properties[15];
					return true;
				case 245:
					info = this.Properties[16];
					return true;
				default:
					switch (id)
					{
					case 257:
						info = this.Properties[17];
						return true;
					case 258:
						info = this.Properties[18];
						return true;
					case 260:
						info = this.Properties[19];
						return true;
					case 261:
						info = this.Properties[20];
						return true;
					case 262:
						info = this.Properties[21];
						return true;
					}
					break;
				}
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x040099FD RID: 39421
		public const int ModelId = 181;

		// Token: 0x040099FE RID: 39422
		private bool m_HasSession;

		// Token: 0x040099FF RID: 39423
		private int m_Season;

		// Token: 0x04009A00 RID: 39424
		private int m_Wins;

		// Token: 0x04009A01 RID: 39425
		private int m_Losses;

		// Token: 0x04009A02 RID: 39426
		private int m_Rating;

		// Token: 0x04009A03 RID: 39427
		private string m_TimeRemainingString;

		// Token: 0x04009A04 RID: 39428
		private int m_PaidRating;

		// Token: 0x04009A05 RID: 39429
		private int m_HighWatermark;

		// Token: 0x04009A06 RID: 39430
		private string m_Unlocks;

		// Token: 0x04009A07 RID: 39431
		private bool m_RecentWin;

		// Token: 0x04009A08 RID: 39432
		private bool m_RecentLoss;

		// Token: 0x04009A09 RID: 39433
		private bool m_IsEarlyAccess;

		// Token: 0x04009A0A RID: 39434
		private bool m_IsPaidEntry;

		// Token: 0x04009A0B RID: 39435
		private bool m_IsSessionActive;

		// Token: 0x04009A0C RID: 39436
		private int m_LastRatingChange;

		// Token: 0x04009A0D RID: 39437
		private bool m_IsPaidUnlocked;

		// Token: 0x04009A0E RID: 39438
		private bool m_IsFreeUnlocked;

		// Token: 0x04009A0F RID: 39439
		private string m_NoticeHeaderString;

		// Token: 0x04009A10 RID: 39440
		private string m_NoticeDescString;

		// Token: 0x04009A11 RID: 39441
		private int m_LastPlayedMode;

		// Token: 0x04009A12 RID: 39442
		private bool m_IsRatingNotice;

		// Token: 0x04009A13 RID: 39443
		private string m_NoticeRatingString;

		// Token: 0x04009A14 RID: 39444
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 182,
				PropertyDisplayName = "has_session",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 183,
				PropertyDisplayName = "season",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 184,
				PropertyDisplayName = "wins",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 185,
				PropertyDisplayName = "losses",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 186,
				PropertyDisplayName = "rating",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 187,
				PropertyDisplayName = "time_remaining_string",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 188,
				PropertyDisplayName = "paid_rating",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 211,
				PropertyDisplayName = "high_watermark",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 212,
				PropertyDisplayName = "unlocks",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 219,
				PropertyDisplayName = "recent_win",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 220,
				PropertyDisplayName = "recent_loss",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 235,
				PropertyDisplayName = "is_early_access",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 237,
				PropertyDisplayName = "is_paid_entry",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 241,
				PropertyDisplayName = "is_session_active",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 243,
				PropertyDisplayName = "last_rating_change",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 244,
				PropertyDisplayName = "is_paid_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 245,
				PropertyDisplayName = "is_free_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 257,
				PropertyDisplayName = "notice_header_string",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 258,
				PropertyDisplayName = "notice_desc_string",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 260,
				PropertyDisplayName = "last_played_mode",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 261,
				PropertyDisplayName = "is_rating_notice",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 262,
				PropertyDisplayName = "notice_rating_string",
				Type = typeof(string)
			}
		};
	}
}
