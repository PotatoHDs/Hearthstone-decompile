using System;
using Hearthstone.Progression;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109A RID: 4250
	public class AchievementDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B84E RID: 47182 RVA: 0x00387290 File Offset: 0x00385490
		public AchievementDataModel()
		{
			base.RegisterNestedDataModel(this.m_RewardList);
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600B84F RID: 47183 RVA: 0x0038772D File Offset: 0x0038592D
		public int DataModelId
		{
			get
			{
				return 222;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600B850 RID: 47184 RVA: 0x00387734 File Offset: 0x00385934
		public string DataModelDisplayName
		{
			get
			{
				return "achievement";
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600B852 RID: 47186 RVA: 0x00387766 File Offset: 0x00385966
		// (set) Token: 0x0600B851 RID: 47185 RVA: 0x0038773B File Offset: 0x0038593B
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name == value)
				{
					return;
				}
				this.m_Name = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600B854 RID: 47188 RVA: 0x00387794 File Offset: 0x00385994
		// (set) Token: 0x0600B853 RID: 47187 RVA: 0x0038776E File Offset: 0x0038596E
		public int Progress
		{
			get
			{
				return this.m_Progress;
			}
			set
			{
				if (this.m_Progress == value)
				{
					return;
				}
				this.m_Progress = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600B856 RID: 47190 RVA: 0x003877C2 File Offset: 0x003859C2
		// (set) Token: 0x0600B855 RID: 47189 RVA: 0x0038779C File Offset: 0x0038599C
		public int Quota
		{
			get
			{
				return this.m_Quota;
			}
			set
			{
				if (this.m_Quota == value)
				{
					return;
				}
				this.m_Quota = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600B858 RID: 47192 RVA: 0x003877F5 File Offset: 0x003859F5
		// (set) Token: 0x0600B857 RID: 47191 RVA: 0x003877CA File Offset: 0x003859CA
		public string Description
		{
			get
			{
				return this.m_Description;
			}
			set
			{
				if (this.m_Description == value)
				{
					return;
				}
				this.m_Description = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600B85A RID: 47194 RVA: 0x00387828 File Offset: 0x00385A28
		// (set) Token: 0x0600B859 RID: 47193 RVA: 0x003877FD File Offset: 0x003859FD
		public string StyleName
		{
			get
			{
				return this.m_StyleName;
			}
			set
			{
				if (this.m_StyleName == value)
				{
					return;
				}
				this.m_StyleName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600B85C RID: 47196 RVA: 0x00387856 File Offset: 0x00385A56
		// (set) Token: 0x0600B85B RID: 47195 RVA: 0x00387830 File Offset: 0x00385A30
		public int Points
		{
			get
			{
				return this.m_Points;
			}
			set
			{
				if (this.m_Points == value)
				{
					return;
				}
				this.m_Points = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600B85E RID: 47198 RVA: 0x00387884 File Offset: 0x00385A84
		// (set) Token: 0x0600B85D RID: 47197 RVA: 0x0038785E File Offset: 0x00385A5E
		public AchievementManager.AchievementStatus Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				if (this.m_Status == value)
				{
					return;
				}
				this.m_Status = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600B860 RID: 47200 RVA: 0x003878B7 File Offset: 0x00385AB7
		// (set) Token: 0x0600B85F RID: 47199 RVA: 0x0038788C File Offset: 0x00385A8C
		public string CompletionDate
		{
			get
			{
				return this.m_CompletionDate;
			}
			set
			{
				if (this.m_CompletionDate == value)
				{
					return;
				}
				this.m_CompletionDate = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600B862 RID: 47202 RVA: 0x003878F8 File Offset: 0x00385AF8
		// (set) Token: 0x0600B861 RID: 47201 RVA: 0x003878BF File Offset: 0x00385ABF
		public RewardListDataModel RewardList
		{
			get
			{
				return this.m_RewardList;
			}
			set
			{
				if (this.m_RewardList == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_RewardList);
				base.RegisterNestedDataModel(value);
				this.m_RewardList = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600B864 RID: 47204 RVA: 0x0038792B File Offset: 0x00385B2B
		// (set) Token: 0x0600B863 RID: 47203 RVA: 0x00387900 File Offset: 0x00385B00
		public string ProgressMessage
		{
			get
			{
				return this.m_ProgressMessage;
			}
			set
			{
				if (this.m_ProgressMessage == value)
				{
					return;
				}
				this.m_ProgressMessage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600B866 RID: 47206 RVA: 0x00387959 File Offset: 0x00385B59
		// (set) Token: 0x0600B865 RID: 47205 RVA: 0x00387933 File Offset: 0x00385B33
		public int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				if (this.m_ID == value)
				{
					return;
				}
				this.m_ID = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600B868 RID: 47208 RVA: 0x00387987 File Offset: 0x00385B87
		// (set) Token: 0x0600B867 RID: 47207 RVA: 0x00387961 File Offset: 0x00385B61
		public int NextTierID
		{
			get
			{
				return this.m_NextTierID;
			}
			set
			{
				if (this.m_NextTierID == value)
				{
					return;
				}
				this.m_NextTierID = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600B86A RID: 47210 RVA: 0x003879B5 File Offset: 0x00385BB5
		// (set) Token: 0x0600B869 RID: 47209 RVA: 0x0038798F File Offset: 0x00385B8F
		public int Tier
		{
			get
			{
				return this.m_Tier;
			}
			set
			{
				if (this.m_Tier == value)
				{
					return;
				}
				this.m_Tier = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600B86C RID: 47212 RVA: 0x003879E3 File Offset: 0x00385BE3
		// (set) Token: 0x0600B86B RID: 47211 RVA: 0x003879BD File Offset: 0x00385BBD
		public int MaxTier
		{
			get
			{
				return this.m_MaxTier;
			}
			set
			{
				if (this.m_MaxTier == value)
				{
					return;
				}
				this.m_MaxTier = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600B86E RID: 47214 RVA: 0x00387A11 File Offset: 0x00385C11
		// (set) Token: 0x0600B86D RID: 47213 RVA: 0x003879EB File Offset: 0x00385BEB
		public int SortOrder
		{
			get
			{
				return this.m_SortOrder;
			}
			set
			{
				if (this.m_SortOrder == value)
				{
					return;
				}
				this.m_SortOrder = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600B870 RID: 47216 RVA: 0x00387A44 File Offset: 0x00385C44
		// (set) Token: 0x0600B86F RID: 47215 RVA: 0x00387A19 File Offset: 0x00385C19
		public string RewardSummary
		{
			get
			{
				return this.m_RewardSummary;
			}
			set
			{
				if (this.m_RewardSummary == value)
				{
					return;
				}
				this.m_RewardSummary = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600B872 RID: 47218 RVA: 0x00387A77 File Offset: 0x00385C77
		// (set) Token: 0x0600B871 RID: 47217 RVA: 0x00387A4C File Offset: 0x00385C4C
		public string TierMessage
		{
			get
			{
				return this.m_TierMessage;
			}
			set
			{
				if (this.m_TierMessage == value)
				{
					return;
				}
				this.m_TierMessage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600B874 RID: 47220 RVA: 0x00387AA5 File Offset: 0x00385CA5
		// (set) Token: 0x0600B873 RID: 47219 RVA: 0x00387A7F File Offset: 0x00385C7F
		public int RewardTrackXp
		{
			get
			{
				return this.m_RewardTrackXp;
			}
			set
			{
				if (this.m_RewardTrackXp == value)
				{
					return;
				}
				this.m_RewardTrackXp = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600B876 RID: 47222 RVA: 0x00387AD3 File Offset: 0x00385CD3
		// (set) Token: 0x0600B875 RID: 47221 RVA: 0x00387AAD File Offset: 0x00385CAD
		public int RewardTrackXpBonusPercent
		{
			get
			{
				return this.m_RewardTrackXpBonusPercent;
			}
			set
			{
				if (this.m_RewardTrackXpBonusPercent == value)
				{
					return;
				}
				this.m_RewardTrackXpBonusPercent = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600B878 RID: 47224 RVA: 0x00387B01 File Offset: 0x00385D01
		// (set) Token: 0x0600B877 RID: 47223 RVA: 0x00387ADB File Offset: 0x00385CDB
		public int RewardTrackXpBonusAdjusted
		{
			get
			{
				return this.m_RewardTrackXpBonusAdjusted;
			}
			set
			{
				if (this.m_RewardTrackXpBonusAdjusted == value)
				{
					return;
				}
				this.m_RewardTrackXpBonusAdjusted = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600B87A RID: 47226 RVA: 0x00387B2F File Offset: 0x00385D2F
		// (set) Token: 0x0600B879 RID: 47225 RVA: 0x00387B09 File Offset: 0x00385D09
		public bool AllowExceedQuota
		{
			get
			{
				return this.m_AllowExceedQuota;
			}
			set
			{
				if (this.m_AllowExceedQuota == value)
				{
					return;
				}
				this.m_AllowExceedQuota = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600B87B RID: 47227 RVA: 0x00387B37 File Offset: 0x00385D37
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B87C RID: 47228 RVA: 0x00387B40 File Offset: 0x00385D40
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31;
			int progress = this.m_Progress;
			int num2 = (num + this.m_Progress.GetHashCode()) * 31;
			int quota = this.m_Quota;
			int num3 = (((num2 + this.m_Quota.GetHashCode()) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_StyleName != null) ? this.m_StyleName.GetHashCode() : 0)) * 31;
			int points = this.m_Points;
			int num4 = (num3 + this.m_Points.GetHashCode()) * 31;
			AchievementManager.AchievementStatus status = this.m_Status;
			int num5 = ((((num4 + this.m_Status.GetHashCode()) * 31 + ((this.m_CompletionDate != null) ? this.m_CompletionDate.GetHashCode() : 0)) * 31 + ((this.m_RewardList != null) ? this.m_RewardList.GetPropertiesHashCode() : 0)) * 31 + ((this.m_ProgressMessage != null) ? this.m_ProgressMessage.GetHashCode() : 0)) * 31;
			int id = this.m_ID;
			int num6 = (num5 + this.m_ID.GetHashCode()) * 31;
			int nextTierID = this.m_NextTierID;
			int num7 = (num6 + this.m_NextTierID.GetHashCode()) * 31;
			int tier = this.m_Tier;
			int num8 = (num7 + this.m_Tier.GetHashCode()) * 31;
			int maxTier = this.m_MaxTier;
			int num9 = (num8 + this.m_MaxTier.GetHashCode()) * 31;
			int sortOrder = this.m_SortOrder;
			int num10 = (((num9 + this.m_SortOrder.GetHashCode()) * 31 + ((this.m_RewardSummary != null) ? this.m_RewardSummary.GetHashCode() : 0)) * 31 + ((this.m_TierMessage != null) ? this.m_TierMessage.GetHashCode() : 0)) * 31;
			int rewardTrackXp = this.m_RewardTrackXp;
			int num11 = (num10 + this.m_RewardTrackXp.GetHashCode()) * 31;
			int rewardTrackXpBonusPercent = this.m_RewardTrackXpBonusPercent;
			int num12 = (num11 + this.m_RewardTrackXpBonusPercent.GetHashCode()) * 31;
			int rewardTrackXpBonusAdjusted = this.m_RewardTrackXpBonusAdjusted;
			int num13 = (num12 + this.m_RewardTrackXpBonusAdjusted.GetHashCode()) * 31;
			bool allowExceedQuota = this.m_AllowExceedQuota;
			return num13 + this.m_AllowExceedQuota.GetHashCode();
		}

		// Token: 0x0600B87D RID: 47229 RVA: 0x00387D44 File Offset: 0x00385F44
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_Progress;
				return true;
			case 2:
				value = this.m_Quota;
				return true;
			case 3:
				value = this.m_Description;
				return true;
			case 4:
				value = this.m_StyleName;
				return true;
			case 5:
				value = this.m_Points;
				return true;
			case 6:
				value = this.m_Status;
				return true;
			case 7:
				value = this.m_CompletionDate;
				return true;
			case 8:
				value = this.m_RewardList;
				return true;
			case 9:
				value = this.m_ProgressMessage;
				return true;
			case 10:
				value = this.m_ID;
				return true;
			case 11:
				value = this.m_NextTierID;
				return true;
			case 12:
				value = this.m_Tier;
				return true;
			case 13:
				value = this.m_MaxTier;
				return true;
			case 14:
				value = this.m_SortOrder;
				return true;
			case 15:
				value = this.m_RewardSummary;
				return true;
			case 16:
				value = this.m_TierMessage;
				return true;
			case 17:
				value = this.m_RewardTrackXp;
				return true;
			case 18:
				value = this.m_RewardTrackXpBonusPercent;
				return true;
			case 19:
				value = this.m_RewardTrackXpBonusAdjusted;
				return true;
			case 20:
				value = this.m_AllowExceedQuota;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B87E RID: 47230 RVA: 0x00387EC8 File Offset: 0x003860C8
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Progress = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Quota = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			case 4:
				this.StyleName = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				this.Points = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				this.Status = ((value != null) ? ((AchievementManager.AchievementStatus)value) : AchievementManager.AchievementStatus.UNKNOWN);
				return true;
			case 7:
				this.CompletionDate = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				this.RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 9:
				this.ProgressMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				this.ID = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				this.NextTierID = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				this.Tier = ((value != null) ? ((int)value) : 0);
				return true;
			case 13:
				this.MaxTier = ((value != null) ? ((int)value) : 0);
				return true;
			case 14:
				this.SortOrder = ((value != null) ? ((int)value) : 0);
				return true;
			case 15:
				this.RewardSummary = ((value != null) ? ((string)value) : null);
				return true;
			case 16:
				this.TierMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 17:
				this.RewardTrackXp = ((value != null) ? ((int)value) : 0);
				return true;
			case 18:
				this.RewardTrackXpBonusPercent = ((value != null) ? ((int)value) : 0);
				return true;
			case 19:
				this.RewardTrackXpBonusAdjusted = ((value != null) ? ((int)value) : 0);
				return true;
			case 20:
				this.AllowExceedQuota = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B87F RID: 47231 RVA: 0x003880DC File Offset: 0x003862DC
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = this.Properties[0];
				return true;
			case 1:
				info = this.Properties[1];
				return true;
			case 2:
				info = this.Properties[2];
				return true;
			case 3:
				info = this.Properties[3];
				return true;
			case 4:
				info = this.Properties[4];
				return true;
			case 5:
				info = this.Properties[5];
				return true;
			case 6:
				info = this.Properties[6];
				return true;
			case 7:
				info = this.Properties[7];
				return true;
			case 8:
				info = this.Properties[8];
				return true;
			case 9:
				info = this.Properties[9];
				return true;
			case 10:
				info = this.Properties[10];
				return true;
			case 11:
				info = this.Properties[11];
				return true;
			case 12:
				info = this.Properties[12];
				return true;
			case 13:
				info = this.Properties[13];
				return true;
			case 14:
				info = this.Properties[14];
				return true;
			case 15:
				info = this.Properties[15];
				return true;
			case 16:
				info = this.Properties[16];
				return true;
			case 17:
				info = this.Properties[17];
				return true;
			case 18:
				info = this.Properties[18];
				return true;
			case 19:
				info = this.Properties[19];
				return true;
			case 20:
				info = this.Properties[20];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400986D RID: 39021
		public const int ModelId = 222;

		// Token: 0x0400986E RID: 39022
		private string m_Name;

		// Token: 0x0400986F RID: 39023
		private int m_Progress;

		// Token: 0x04009870 RID: 39024
		private int m_Quota;

		// Token: 0x04009871 RID: 39025
		private string m_Description;

		// Token: 0x04009872 RID: 39026
		private string m_StyleName;

		// Token: 0x04009873 RID: 39027
		private int m_Points;

		// Token: 0x04009874 RID: 39028
		private AchievementManager.AchievementStatus m_Status;

		// Token: 0x04009875 RID: 39029
		private string m_CompletionDate;

		// Token: 0x04009876 RID: 39030
		private RewardListDataModel m_RewardList;

		// Token: 0x04009877 RID: 39031
		private string m_ProgressMessage;

		// Token: 0x04009878 RID: 39032
		private int m_ID;

		// Token: 0x04009879 RID: 39033
		private int m_NextTierID;

		// Token: 0x0400987A RID: 39034
		private int m_Tier;

		// Token: 0x0400987B RID: 39035
		private int m_MaxTier;

		// Token: 0x0400987C RID: 39036
		private int m_SortOrder;

		// Token: 0x0400987D RID: 39037
		private string m_RewardSummary;

		// Token: 0x0400987E RID: 39038
		private string m_TierMessage;

		// Token: 0x0400987F RID: 39039
		private int m_RewardTrackXp;

		// Token: 0x04009880 RID: 39040
		private int m_RewardTrackXpBonusPercent;

		// Token: 0x04009881 RID: 39041
		private int m_RewardTrackXpBonusAdjusted;

		// Token: 0x04009882 RID: 39042
		private bool m_AllowExceedQuota;

		// Token: 0x04009883 RID: 39043
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "progress",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quota",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "style_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "points",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "status",
				Type = typeof(AchievementManager.AchievementStatus)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "completion_date",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "progress_message",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "next_tier_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "tier",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "max_tier",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "sort_order",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "reward_summary",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "tier_message",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "reward_track_xp",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "reward_track_xp_bonus_percent",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "reward_track_xp_bonus_adjusted",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "allow_exceed_quota",
				Type = typeof(bool)
			}
		};
	}
}
