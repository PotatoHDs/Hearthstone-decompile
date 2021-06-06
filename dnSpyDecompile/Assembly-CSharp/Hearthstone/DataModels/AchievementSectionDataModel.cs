using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x0200109C RID: 4252
	public class AchievementSectionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B88A RID: 47242 RVA: 0x00388430 File Offset: 0x00386630
		public AchievementSectionDataModel()
		{
			base.RegisterNestedDataModel(this.m_Achievements);
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600B88B RID: 47243 RVA: 0x00388599 File Offset: 0x00386799
		public int DataModelId
		{
			get
			{
				return 226;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600B88C RID: 47244 RVA: 0x003885A0 File Offset: 0x003867A0
		public string DataModelDisplayName
		{
			get
			{
				return "achievement_section";
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600B88E RID: 47246 RVA: 0x003885D2 File Offset: 0x003867D2
		// (set) Token: 0x0600B88D RID: 47245 RVA: 0x003885A7 File Offset: 0x003867A7
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

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600B890 RID: 47248 RVA: 0x00388613 File Offset: 0x00386813
		// (set) Token: 0x0600B88F RID: 47247 RVA: 0x003885DA File Offset: 0x003867DA
		public AchievementListDataModel Achievements
		{
			get
			{
				return this.m_Achievements;
			}
			set
			{
				if (this.m_Achievements == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Achievements);
				base.RegisterNestedDataModel(value);
				this.m_Achievements = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600B892 RID: 47250 RVA: 0x00388641 File Offset: 0x00386841
		// (set) Token: 0x0600B891 RID: 47249 RVA: 0x0038861B File Offset: 0x0038681B
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

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600B894 RID: 47252 RVA: 0x0038866F File Offset: 0x0038686F
		// (set) Token: 0x0600B893 RID: 47251 RVA: 0x00388649 File Offset: 0x00386849
		public int Index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				if (this.m_Index == value)
				{
					return;
				}
				this.m_Index = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600B896 RID: 47254 RVA: 0x0038869D File Offset: 0x0038689D
		// (set) Token: 0x0600B895 RID: 47253 RVA: 0x00388677 File Offset: 0x00386877
		public int TileCount
		{
			get
			{
				return this.m_TileCount;
			}
			set
			{
				if (this.m_TileCount == value)
				{
					return;
				}
				this.m_TileCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600B898 RID: 47256 RVA: 0x003886CB File Offset: 0x003868CB
		// (set) Token: 0x0600B897 RID: 47255 RVA: 0x003886A5 File Offset: 0x003868A5
		public int PreviousTileCount
		{
			get
			{
				return this.m_PreviousTileCount;
			}
			set
			{
				if (this.m_PreviousTileCount == value)
				{
					return;
				}
				this.m_PreviousTileCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600B899 RID: 47257 RVA: 0x003886D3 File Offset: 0x003868D3
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B89A RID: 47258 RVA: 0x003886DC File Offset: 0x003868DC
		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Achievements != null) ? this.m_Achievements.GetPropertiesHashCode() : 0)) * 31;
			int id = this.m_ID;
			int num2 = (num + this.m_ID.GetHashCode()) * 31;
			int index = this.m_Index;
			int num3 = (num2 + this.m_Index.GetHashCode()) * 31;
			int tileCount = this.m_TileCount;
			int num4 = (num3 + this.m_TileCount.GetHashCode()) * 31;
			int previousTileCount = this.m_PreviousTileCount;
			return num4 + this.m_PreviousTileCount.GetHashCode();
		}

		// Token: 0x0600B89B RID: 47259 RVA: 0x00388778 File Offset: 0x00386978
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_Achievements;
				return true;
			case 3:
				value = this.m_ID;
				return true;
			case 4:
				value = this.m_Index;
				return true;
			case 5:
				value = this.m_TileCount;
				return true;
			case 6:
				value = this.m_PreviousTileCount;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600B89C RID: 47260 RVA: 0x00388800 File Offset: 0x00386A00
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Achievements = ((value != null) ? ((AchievementListDataModel)value) : null);
				return true;
			case 3:
				this.ID = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.Index = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.TileCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				this.PreviousTileCount = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600B89D RID: 47261 RVA: 0x003888AC File Offset: 0x00386AAC
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
			case 3:
				info = this.Properties[2];
				return true;
			case 4:
				info = this.Properties[3];
				return true;
			case 5:
				info = this.Properties[4];
				return true;
			case 6:
				info = this.Properties[5];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009887 RID: 39047
		public const int ModelId = 226;

		// Token: 0x04009888 RID: 39048
		private string m_Name;

		// Token: 0x04009889 RID: 39049
		private AchievementListDataModel m_Achievements;

		// Token: 0x0400988A RID: 39050
		private int m_ID;

		// Token: 0x0400988B RID: 39051
		private int m_Index;

		// Token: 0x0400988C RID: 39052
		private int m_TileCount;

		// Token: 0x0400988D RID: 39053
		private int m_PreviousTileCount;

		// Token: 0x0400988E RID: 39054
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
				PropertyDisplayName = "achievements",
				Type = typeof(AchievementListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "index",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "tile_count",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "previous_tile_count",
				Type = typeof(int)
			}
		};
	}
}
