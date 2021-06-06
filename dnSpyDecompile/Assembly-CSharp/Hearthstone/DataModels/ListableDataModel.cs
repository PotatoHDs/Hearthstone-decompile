using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BD RID: 4285
	public class ListableDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600BB17 RID: 47895 RVA: 0x00392CB3 File Offset: 0x00390EB3
		public int DataModelId
		{
			get
			{
				return 258;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600BB18 RID: 47896 RVA: 0x00392CBA File Offset: 0x00390EBA
		public string DataModelDisplayName
		{
			get
			{
				return "listable";
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600BB1A RID: 47898 RVA: 0x00392CE7 File Offset: 0x00390EE7
		// (set) Token: 0x0600BB19 RID: 47897 RVA: 0x00392CC1 File Offset: 0x00390EC1
		public int ItemIndex
		{
			get
			{
				return this.m_ItemIndex;
			}
			set
			{
				if (this.m_ItemIndex == value)
				{
					return;
				}
				this.m_ItemIndex = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600BB1C RID: 47900 RVA: 0x00392D15 File Offset: 0x00390F15
		// (set) Token: 0x0600BB1B RID: 47899 RVA: 0x00392CEF File Offset: 0x00390EEF
		public int ListSize
		{
			get
			{
				return this.m_ListSize;
			}
			set
			{
				if (this.m_ListSize == value)
				{
					return;
				}
				this.m_ListSize = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600BB1E RID: 47902 RVA: 0x00392D43 File Offset: 0x00390F43
		// (set) Token: 0x0600BB1D RID: 47901 RVA: 0x00392D1D File Offset: 0x00390F1D
		public bool IsFirstItem
		{
			get
			{
				return this.m_IsFirstItem;
			}
			set
			{
				if (this.m_IsFirstItem == value)
				{
					return;
				}
				this.m_IsFirstItem = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600BB20 RID: 47904 RVA: 0x00392D71 File Offset: 0x00390F71
		// (set) Token: 0x0600BB1F RID: 47903 RVA: 0x00392D4B File Offset: 0x00390F4B
		public bool IsLastItem
		{
			get
			{
				return this.m_IsLastItem;
			}
			set
			{
				if (this.m_IsLastItem == value)
				{
					return;
				}
				this.m_IsLastItem = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600BB21 RID: 47905 RVA: 0x00392D79 File Offset: 0x00390F79
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB22 RID: 47906 RVA: 0x00392D84 File Offset: 0x00390F84
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int itemIndex = this.m_ItemIndex;
			int num2 = (num + this.m_ItemIndex.GetHashCode()) * 31;
			int listSize = this.m_ListSize;
			int num3 = (num2 + this.m_ListSize.GetHashCode()) * 31;
			bool isFirstItem = this.m_IsFirstItem;
			int num4 = (num3 + this.m_IsFirstItem.GetHashCode()) * 31;
			bool isLastItem = this.m_IsLastItem;
			return num4 + this.m_IsLastItem.GetHashCode();
		}

		// Token: 0x0600BB23 RID: 47907 RVA: 0x00392DEC File Offset: 0x00390FEC
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_ItemIndex;
				return true;
			case 1:
				value = this.m_ListSize;
				return true;
			case 2:
				value = this.m_IsFirstItem;
				return true;
			case 3:
				value = this.m_IsLastItem;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB24 RID: 47908 RVA: 0x00392E54 File Offset: 0x00391054
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.ItemIndex = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.ListSize = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.IsFirstItem = (value != null && (bool)value);
				return true;
			case 3:
				this.IsLastItem = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BB25 RID: 47909 RVA: 0x00392ECC File Offset: 0x003910CC
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400998B RID: 39307
		public const int ModelId = 258;

		// Token: 0x0400998C RID: 39308
		private int m_ItemIndex;

		// Token: 0x0400998D RID: 39309
		private int m_ListSize;

		// Token: 0x0400998E RID: 39310
		private bool m_IsFirstItem;

		// Token: 0x0400998F RID: 39311
		private bool m_IsLastItem;

		// Token: 0x04009990 RID: 39312
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "item_index",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "list_size",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_first_item",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_last_item",
				Type = typeof(bool)
			}
		};
	}
}
