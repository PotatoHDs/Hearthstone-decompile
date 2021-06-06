using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B2 RID: 4274
	public class DeckPouchDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BA68 RID: 47720 RVA: 0x003905A4 File Offset: 0x0038E7A4
		public DeckPouchDataModel()
		{
			base.RegisterNestedDataModel(this.m_Pouch);
			base.RegisterNestedDataModel(this.m_Details);
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600BA69 RID: 47721 RVA: 0x003906E4 File Offset: 0x0038E8E4
		public int DataModelId
		{
			get
			{
				return 289;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600BA6A RID: 47722 RVA: 0x003906EB File Offset: 0x0038E8EB
		public string DataModelDisplayName
		{
			get
			{
				return "deck_pouch";
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600BA6C RID: 47724 RVA: 0x0039072B File Offset: 0x0038E92B
		// (set) Token: 0x0600BA6B RID: 47723 RVA: 0x003906F2 File Offset: 0x0038E8F2
		public AdventureLoadoutOptionDataModel Pouch
		{
			get
			{
				return this.m_Pouch;
			}
			set
			{
				if (this.m_Pouch == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Pouch);
				base.RegisterNestedDataModel(value);
				this.m_Pouch = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600BA6E RID: 47726 RVA: 0x00390759 File Offset: 0x0038E959
		// (set) Token: 0x0600BA6D RID: 47725 RVA: 0x00390733 File Offset: 0x0038E933
		public int RemainingDust
		{
			get
			{
				return this.m_RemainingDust;
			}
			set
			{
				if (this.m_RemainingDust == value)
				{
					return;
				}
				this.m_RemainingDust = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600BA70 RID: 47728 RVA: 0x00390787 File Offset: 0x0038E987
		// (set) Token: 0x0600BA6F RID: 47727 RVA: 0x00390761 File Offset: 0x0038E961
		public int TotalDust
		{
			get
			{
				return this.m_TotalDust;
			}
			set
			{
				if (this.m_TotalDust == value)
				{
					return;
				}
				this.m_TotalDust = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600BA72 RID: 47730 RVA: 0x003907C8 File Offset: 0x0038E9C8
		// (set) Token: 0x0600BA71 RID: 47729 RVA: 0x0039078F File Offset: 0x0038E98F
		public DeckDetailsDataModel Details
		{
			get
			{
				return this.m_Details;
			}
			set
			{
				if (this.m_Details == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Details);
				base.RegisterNestedDataModel(value);
				this.m_Details = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600BA74 RID: 47732 RVA: 0x003907F6 File Offset: 0x0038E9F6
		// (set) Token: 0x0600BA73 RID: 47731 RVA: 0x003907D0 File Offset: 0x0038E9D0
		public TAG_CLASS Class
		{
			get
			{
				return this.m_Class;
			}
			set
			{
				if (this.m_Class == value)
				{
					return;
				}
				this.m_Class = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600BA75 RID: 47733 RVA: 0x003907FE File Offset: 0x0038E9FE
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA76 RID: 47734 RVA: 0x00390808 File Offset: 0x0038EA08
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Pouch != null) ? this.m_Pouch.GetPropertiesHashCode() : 0)) * 31;
			int remainingDust = this.m_RemainingDust;
			int num2 = (num + this.m_RemainingDust.GetHashCode()) * 31;
			int totalDust = this.m_TotalDust;
			int num3 = ((num2 + this.m_TotalDust.GetHashCode()) * 31 + ((this.m_Details != null) ? this.m_Details.GetPropertiesHashCode() : 0)) * 31;
			TAG_CLASS @class = this.m_Class;
			return num3 + this.m_Class.GetHashCode();
		}

		// Token: 0x0600BA77 RID: 47735 RVA: 0x00390894 File Offset: 0x0038EA94
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Pouch;
				return true;
			case 1:
				value = this.m_RemainingDust;
				return true;
			case 2:
				value = this.m_TotalDust;
				return true;
			case 3:
				value = this.m_Details;
				return true;
			case 4:
				value = this.m_Class;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BA78 RID: 47736 RVA: 0x00390904 File Offset: 0x0038EB04
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Pouch = ((value != null) ? ((AdventureLoadoutOptionDataModel)value) : null);
				return true;
			case 1:
				this.RemainingDust = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.TotalDust = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.Details = ((value != null) ? ((DeckDetailsDataModel)value) : null);
				return true;
			case 4:
				this.Class = ((value != null) ? ((TAG_CLASS)value) : TAG_CLASS.INVALID);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BA79 RID: 47737 RVA: 0x00390994 File Offset: 0x0038EB94
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400994A RID: 39242
		public const int ModelId = 289;

		// Token: 0x0400994B RID: 39243
		private AdventureLoadoutOptionDataModel m_Pouch;

		// Token: 0x0400994C RID: 39244
		private int m_RemainingDust;

		// Token: 0x0400994D RID: 39245
		private int m_TotalDust;

		// Token: 0x0400994E RID: 39246
		private DeckDetailsDataModel m_Details;

		// Token: 0x0400994F RID: 39247
		private TAG_CLASS m_Class;

		// Token: 0x04009950 RID: 39248
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pouch",
				Type = typeof(AdventureLoadoutOptionDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "remaining_dust",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "total_dust",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "details",
				Type = typeof(DeckDetailsDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "class",
				Type = typeof(TAG_CLASS)
			}
		};
	}
}
