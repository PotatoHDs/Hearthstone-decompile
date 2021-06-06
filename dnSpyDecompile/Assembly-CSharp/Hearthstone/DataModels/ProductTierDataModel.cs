using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C7 RID: 4295
	public class ProductTierDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BBAE RID: 48046 RVA: 0x00394D24 File Offset: 0x00392F24
		public ProductTierDataModel()
		{
			base.RegisterNestedDataModel(this.m_Tags);
			base.RegisterNestedDataModel(this.m_BrowserButtons);
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600BBAF RID: 48047 RVA: 0x00394E45 File Offset: 0x00393045
		public int DataModelId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600BBB0 RID: 48048 RVA: 0x00394E49 File Offset: 0x00393049
		public string DataModelDisplayName
		{
			get
			{
				return "product_tier";
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600BBB2 RID: 48050 RVA: 0x00394E7B File Offset: 0x0039307B
		// (set) Token: 0x0600BBB1 RID: 48049 RVA: 0x00394E50 File Offset: 0x00393050
		public string Header
		{
			get
			{
				return this.m_Header;
			}
			set
			{
				if (this.m_Header == value)
				{
					return;
				}
				this.m_Header = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600BBB4 RID: 48052 RVA: 0x00394EBC File Offset: 0x003930BC
		// (set) Token: 0x0600BBB3 RID: 48051 RVA: 0x00394E83 File Offset: 0x00393083
		public DataModelList<string> Tags
		{
			get
			{
				return this.m_Tags;
			}
			set
			{
				if (this.m_Tags == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Tags);
				base.RegisterNestedDataModel(value);
				this.m_Tags = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600BBB6 RID: 48054 RVA: 0x00394EEF File Offset: 0x003930EF
		// (set) Token: 0x0600BBB5 RID: 48053 RVA: 0x00394EC4 File Offset: 0x003930C4
		public string Style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				if (this.m_Style == value)
				{
					return;
				}
				this.m_Style = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600BBB8 RID: 48056 RVA: 0x00394F30 File Offset: 0x00393130
		// (set) Token: 0x0600BBB7 RID: 48055 RVA: 0x00394EF7 File Offset: 0x003930F7
		public DataModelList<ShopBrowserButtonDataModel> BrowserButtons
		{
			get
			{
				return this.m_BrowserButtons;
			}
			set
			{
				if (this.m_BrowserButtons == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_BrowserButtons);
				base.RegisterNestedDataModel(value);
				this.m_BrowserButtons = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600BBB9 RID: 48057 RVA: 0x00394F38 File Offset: 0x00393138
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BBBA RID: 48058 RVA: 0x00394F40 File Offset: 0x00393140
		public int GetPropertiesHashCode()
		{
			return (((17 * 31 + ((this.m_Header != null) ? this.m_Header.GetHashCode() : 0)) * 31 + ((this.m_Tags != null) ? this.m_Tags.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Style != null) ? this.m_Style.GetHashCode() : 0)) * 31 + ((this.m_BrowserButtons != null) ? this.m_BrowserButtons.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BBBB RID: 48059 RVA: 0x00394FB8 File Offset: 0x003931B8
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 1:
				value = this.m_Header;
				return true;
			case 2:
				value = this.m_Tags;
				return true;
			case 3:
				value = this.m_Style;
				return true;
			case 4:
				value = this.m_BrowserButtons;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BBBC RID: 48060 RVA: 0x0039500C File Offset: 0x0039320C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 1:
				this.Header = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.Tags = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 3:
				this.Style = ((value != null) ? ((string)value) : null);
				return true;
			case 4:
				this.BrowserButtons = ((value != null) ? ((DataModelList<ShopBrowserButtonDataModel>)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BBBD RID: 48061 RVA: 0x00395084 File Offset: 0x00393284
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 1:
				info = this.Properties[0];
				return true;
			case 2:
				info = this.Properties[1];
				return true;
			case 3:
				info = this.Properties[2];
				return true;
			case 4:
				info = this.Properties[3];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099C3 RID: 39363
		public const int ModelId = 23;

		// Token: 0x040099C4 RID: 39364
		private string m_Header;

		// Token: 0x040099C5 RID: 39365
		private DataModelList<string> m_Tags = new DataModelList<string>();

		// Token: 0x040099C6 RID: 39366
		private string m_Style;

		// Token: 0x040099C7 RID: 39367
		private DataModelList<ShopBrowserButtonDataModel> m_BrowserButtons = new DataModelList<ShopBrowserButtonDataModel>();

		// Token: 0x040099C8 RID: 39368
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "header",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "tags",
				Type = typeof(DataModelList<string>)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "style",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "browser_buttons",
				Type = typeof(DataModelList<ShopBrowserButtonDataModel>)
			}
		};
	}
}
