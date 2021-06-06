using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C3 RID: 4291
	public class PageInfoDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600BB61 RID: 47969 RVA: 0x001B1A7E File Offset: 0x001AFC7E
		public int DataModelId
		{
			get
			{
				return 255;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600BB62 RID: 47970 RVA: 0x0039399B File Offset: 0x00391B9B
		public string DataModelDisplayName
		{
			get
			{
				return "page_info";
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x0600BB64 RID: 47972 RVA: 0x003939C8 File Offset: 0x00391BC8
		// (set) Token: 0x0600BB63 RID: 47971 RVA: 0x003939A2 File Offset: 0x00391BA2
		public int PageNumber
		{
			get
			{
				return this.m_PageNumber;
			}
			set
			{
				if (this.m_PageNumber == value)
				{
					return;
				}
				this.m_PageNumber = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600BB66 RID: 47974 RVA: 0x003939F6 File Offset: 0x00391BF6
		// (set) Token: 0x0600BB65 RID: 47973 RVA: 0x003939D0 File Offset: 0x00391BD0
		public int TotalPages
		{
			get
			{
				return this.m_TotalPages;
			}
			set
			{
				if (this.m_TotalPages == value)
				{
					return;
				}
				this.m_TotalPages = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600BB68 RID: 47976 RVA: 0x00393A24 File Offset: 0x00391C24
		// (set) Token: 0x0600BB67 RID: 47975 RVA: 0x003939FE File Offset: 0x00391BFE
		public int ItemsPerPage
		{
			get
			{
				return this.m_ItemsPerPage;
			}
			set
			{
				if (this.m_ItemsPerPage == value)
				{
					return;
				}
				this.m_ItemsPerPage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600BB6A RID: 47978 RVA: 0x00393A57 File Offset: 0x00391C57
		// (set) Token: 0x0600BB69 RID: 47977 RVA: 0x00393A2C File Offset: 0x00391C2C
		public string InfoText
		{
			get
			{
				return this.m_InfoText;
			}
			set
			{
				if (this.m_InfoText == value)
				{
					return;
				}
				this.m_InfoText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600BB6B RID: 47979 RVA: 0x00393A5F File Offset: 0x00391C5F
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB6C RID: 47980 RVA: 0x00393A68 File Offset: 0x00391C68
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int pageNumber = this.m_PageNumber;
			int num2 = (num + this.m_PageNumber.GetHashCode()) * 31;
			int totalPages = this.m_TotalPages;
			int num3 = (num2 + this.m_TotalPages.GetHashCode()) * 31;
			int itemsPerPage = this.m_ItemsPerPage;
			return (num3 + this.m_ItemsPerPage.GetHashCode()) * 31 + ((this.m_InfoText != null) ? this.m_InfoText.GetHashCode() : 0);
		}

		// Token: 0x0600BB6D RID: 47981 RVA: 0x00393AD4 File Offset: 0x00391CD4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_PageNumber;
				return true;
			case 1:
				value = this.m_TotalPages;
				return true;
			case 2:
				value = this.m_ItemsPerPage;
				return true;
			case 3:
				value = this.m_InfoText;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB6E RID: 47982 RVA: 0x00393B34 File Offset: 0x00391D34
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.PageNumber = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.TotalPages = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.ItemsPerPage = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.InfoText = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BB6F RID: 47983 RVA: 0x00393BAC File Offset: 0x00391DAC
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

		// Token: 0x040099A4 RID: 39332
		public const int ModelId = 255;

		// Token: 0x040099A5 RID: 39333
		private int m_PageNumber;

		// Token: 0x040099A6 RID: 39334
		private int m_TotalPages;

		// Token: 0x040099A7 RID: 39335
		private int m_ItemsPerPage;

		// Token: 0x040099A8 RID: 39336
		private string m_InfoText;

		// Token: 0x040099A9 RID: 39337
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "page_number",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "total_pages",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "items_per_page",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "info_text",
				Type = typeof(string)
			}
		};
	}
}
