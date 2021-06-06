using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109E RID: 4254
	public class AchievementStatsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x0600B8A9 RID: 47273 RVA: 0x00388BED File Offset: 0x00386DED
		public int DataModelId
		{
			get
			{
				return 240;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x0600B8AA RID: 47274 RVA: 0x00388BF4 File Offset: 0x00386DF4
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_stats";
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600B8AC RID: 47276 RVA: 0x00388C21 File Offset: 0x00386E21
		// (set) Token: 0x0600B8AB RID: 47275 RVA: 0x00388BFB File Offset: 0x00386DFB
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

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600B8AE RID: 47278 RVA: 0x00388C4F File Offset: 0x00386E4F
		// (set) Token: 0x0600B8AD RID: 47277 RVA: 0x00388C29 File Offset: 0x00386E29
		public int AvailablePoints
		{
			get
			{
				return this.m_AvailablePoints;
			}
			set
			{
				if (this.m_AvailablePoints == value)
				{
					return;
				}
				this.m_AvailablePoints = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600B8B0 RID: 47280 RVA: 0x00388C7D File Offset: 0x00386E7D
		// (set) Token: 0x0600B8AF RID: 47279 RVA: 0x00388C57 File Offset: 0x00386E57
		public int Unclaimed
		{
			get
			{
				return this.m_Unclaimed;
			}
			set
			{
				if (this.m_Unclaimed == value)
				{
					return;
				}
				this.m_Unclaimed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600B8B2 RID: 47282 RVA: 0x00388CAB File Offset: 0x00386EAB
		// (set) Token: 0x0600B8B1 RID: 47281 RVA: 0x00388C85 File Offset: 0x00386E85
		public int CompletedAchievements
		{
			get
			{
				return this.m_CompletedAchievements;
			}
			set
			{
				if (this.m_CompletedAchievements == value)
				{
					return;
				}
				this.m_CompletedAchievements = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600B8B4 RID: 47284 RVA: 0x00388CD9 File Offset: 0x00386ED9
		// (set) Token: 0x0600B8B3 RID: 47283 RVA: 0x00388CB3 File Offset: 0x00386EB3
		public int TotalAchievements
		{
			get
			{
				return this.m_TotalAchievements;
			}
			set
			{
				if (this.m_TotalAchievements == value)
				{
					return;
				}
				this.m_TotalAchievements = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600B8B6 RID: 47286 RVA: 0x00388D0C File Offset: 0x00386F0C
		// (set) Token: 0x0600B8B5 RID: 47285 RVA: 0x00388CE1 File Offset: 0x00386EE1
		public string CompletionPercentage
		{
			get
			{
				return this.m_CompletionPercentage;
			}
			set
			{
				if (this.m_CompletionPercentage == value)
				{
					return;
				}
				this.m_CompletionPercentage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600B8B7 RID: 47287 RVA: 0x00388D14 File Offset: 0x00386F14
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B8B8 RID: 47288 RVA: 0x00388D1C File Offset: 0x00386F1C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int points = this.m_Points;
			int num2 = (num + this.m_Points.GetHashCode()) * 31;
			int availablePoints = this.m_AvailablePoints;
			int num3 = (num2 + this.m_AvailablePoints.GetHashCode()) * 31;
			int unclaimed = this.m_Unclaimed;
			int num4 = (num3 + this.m_Unclaimed.GetHashCode()) * 31;
			int completedAchievements = this.m_CompletedAchievements;
			int num5 = (num4 + this.m_CompletedAchievements.GetHashCode()) * 31;
			int totalAchievements = this.m_TotalAchievements;
			return (num5 + this.m_TotalAchievements.GetHashCode()) * 31 + ((this.m_CompletionPercentage != null) ? this.m_CompletionPercentage.GetHashCode() : 0);
		}

		// Token: 0x0600B8B9 RID: 47289 RVA: 0x00388DB4 File Offset: 0x00386FB4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Points;
				return true;
			case 1:
				value = this.m_AvailablePoints;
				return true;
			case 2:
				value = this.m_Unclaimed;
				return true;
			case 3:
				value = this.m_CompletedAchievements;
				return true;
			case 4:
				value = this.m_TotalAchievements;
				return true;
			case 5:
				value = this.m_CompletionPercentage;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B8BA RID: 47290 RVA: 0x00388E3C File Offset: 0x0038703C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Points = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.AvailablePoints = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Unclaimed = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.CompletedAchievements = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.TotalAchievements = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.CompletionPercentage = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B8BB RID: 47291 RVA: 0x00388EE4 File Offset: 0x003870E4
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009892 RID: 39058
		public const int ModelId = 240;

		// Token: 0x04009893 RID: 39059
		private int m_Points;

		// Token: 0x04009894 RID: 39060
		private int m_AvailablePoints;

		// Token: 0x04009895 RID: 39061
		private int m_Unclaimed;

		// Token: 0x04009896 RID: 39062
		private int m_CompletedAchievements;

		// Token: 0x04009897 RID: 39063
		private int m_TotalAchievements;

		// Token: 0x04009898 RID: 39064
		private string m_CompletionPercentage;

		// Token: 0x04009899 RID: 39065
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "points",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "available_points",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "unclaimed",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "completed_achievements",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "total_achievements",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "completion_percentage",
				Type = typeof(string)
			}
		};
	}
}
