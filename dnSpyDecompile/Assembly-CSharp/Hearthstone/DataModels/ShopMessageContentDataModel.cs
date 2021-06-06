using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010E0 RID: 4320
	public class ShopMessageContentDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600BDB3 RID: 48563 RVA: 0x0039D5F9 File Offset: 0x0039B7F9
		public int DataModelId
		{
			get
			{
				return 301;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600BDB4 RID: 48564 RVA: 0x0039D600 File Offset: 0x0039B800
		public string DataModelDisplayName
		{
			get
			{
				return "shop_message_content";
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x0600BDB6 RID: 48566 RVA: 0x0039D632 File Offset: 0x0039B832
		// (set) Token: 0x0600BDB5 RID: 48565 RVA: 0x0039D607 File Offset: 0x0039B807
		public string Title
		{
			get
			{
				return this.m_Title;
			}
			set
			{
				if (this.m_Title == value)
				{
					return;
				}
				this.m_Title = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x0600BDB8 RID: 48568 RVA: 0x0039D665 File Offset: 0x0039B865
		// (set) Token: 0x0600BDB7 RID: 48567 RVA: 0x0039D63A File Offset: 0x0039B83A
		public string BodyText
		{
			get
			{
				return this.m_BodyText;
			}
			set
			{
				if (this.m_BodyText == value)
				{
					return;
				}
				this.m_BodyText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x0600BDB9 RID: 48569 RVA: 0x0039D66D File Offset: 0x0039B86D
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BDBA RID: 48570 RVA: 0x0039D675 File Offset: 0x0039B875
		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((this.m_Title != null) ? this.m_Title.GetHashCode() : 0)) * 31 + ((this.m_BodyText != null) ? this.m_BodyText.GetHashCode() : 0);
		}

		// Token: 0x0600BDBB RID: 48571 RVA: 0x0039D6AD File Offset: 0x0039B8AD
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Title;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_BodyText;
			return true;
		}

		// Token: 0x0600BDBC RID: 48572 RVA: 0x0039D6D0 File Offset: 0x0039B8D0
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Title = ((value != null) ? ((string)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.BodyText = ((value != null) ? ((string)value) : null);
			return true;
		}

		// Token: 0x0600BDBD RID: 48573 RVA: 0x0039D704 File Offset: 0x0039B904
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 1)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x04009A93 RID: 39571
		public const int ModelId = 301;

		// Token: 0x04009A94 RID: 39572
		private string m_Title;

		// Token: 0x04009A95 RID: 39573
		private string m_BodyText;

		// Token: 0x04009A96 RID: 39574
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "title",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "body_text",
				Type = typeof(string)
			}
		};
	}
}
