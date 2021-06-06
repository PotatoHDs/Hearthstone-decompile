using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010E2 RID: 4322
	public class TextMessageContentDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600BDE1 RID: 48609 RVA: 0x0039E26A File Offset: 0x0039C46A
		public int DataModelId
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600BDE2 RID: 48610 RVA: 0x0039E271 File Offset: 0x0039C471
		public string DataModelDisplayName
		{
			get
			{
				return "text_message_content";
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600BDE4 RID: 48612 RVA: 0x0039E2A3 File Offset: 0x0039C4A3
		// (set) Token: 0x0600BDE3 RID: 48611 RVA: 0x0039E278 File Offset: 0x0039C478
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

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x0600BDE6 RID: 48614 RVA: 0x0039E2D6 File Offset: 0x0039C4D6
		// (set) Token: 0x0600BDE5 RID: 48613 RVA: 0x0039E2AB File Offset: 0x0039C4AB
		public string IconType
		{
			get
			{
				return this.m_IconType;
			}
			set
			{
				if (this.m_IconType == value)
				{
					return;
				}
				this.m_IconType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600BDE8 RID: 48616 RVA: 0x0039E309 File Offset: 0x0039C509
		// (set) Token: 0x0600BDE7 RID: 48615 RVA: 0x0039E2DE File Offset: 0x0039C4DE
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

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600BDE9 RID: 48617 RVA: 0x0039E311 File Offset: 0x0039C511
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BDEA RID: 48618 RVA: 0x0039E31C File Offset: 0x0039C51C
		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((this.m_Title != null) ? this.m_Title.GetHashCode() : 0)) * 31 + ((this.m_IconType != null) ? this.m_IconType.GetHashCode() : 0)) * 31 + ((this.m_BodyText != null) ? this.m_BodyText.GetHashCode() : 0);
		}

		// Token: 0x0600BDEB RID: 48619 RVA: 0x0039E379 File Offset: 0x0039C579
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Title;
				return true;
			case 1:
				value = this.m_IconType;
				return true;
			case 2:
				value = this.m_BodyText;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BDEC RID: 48620 RVA: 0x0039E3B4 File Offset: 0x0039C5B4
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Title = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.IconType = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.BodyText = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BDED RID: 48621 RVA: 0x0039E414 File Offset: 0x0039C614
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009AA6 RID: 39590
		public const int ModelId = 300;

		// Token: 0x04009AA7 RID: 39591
		private string m_Title;

		// Token: 0x04009AA8 RID: 39592
		private string m_IconType;

		// Token: 0x04009AA9 RID: 39593
		private string m_BodyText;

		// Token: 0x04009AAA RID: 39594
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
				PropertyDisplayName = "icon_type",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "body_text",
				Type = typeof(string)
			}
		};
	}
}
