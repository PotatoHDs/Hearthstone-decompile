using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B4 RID: 4276
	public class EventDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600BA8D RID: 47757 RVA: 0x00390F05 File Offset: 0x0038F105
		public int DataModelId
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600BA8E RID: 47758 RVA: 0x00390F09 File Offset: 0x0038F109
		public string DataModelDisplayName
		{
			get
			{
				return "event";
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600BA90 RID: 47760 RVA: 0x00390F3B File Offset: 0x0038F13B
		// (set) Token: 0x0600BA8F RID: 47759 RVA: 0x00390F10 File Offset: 0x0038F110
		public string SourceName
		{
			get
			{
				return this.m_SourceName;
			}
			set
			{
				if (this.m_SourceName == value)
				{
					return;
				}
				this.m_SourceName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600BA92 RID: 47762 RVA: 0x00390F69 File Offset: 0x0038F169
		// (set) Token: 0x0600BA91 RID: 47761 RVA: 0x00390F43 File Offset: 0x0038F143
		public object Payload
		{
			get
			{
				return this.m_Payload;
			}
			set
			{
				if (this.m_Payload == value)
				{
					return;
				}
				this.m_Payload = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600BA93 RID: 47763 RVA: 0x00390F71 File Offset: 0x0038F171
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA94 RID: 47764 RVA: 0x00390F79 File Offset: 0x0038F179
		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((this.m_SourceName != null) ? this.m_SourceName.GetHashCode() : 0)) * 31 + ((this.m_Payload != null) ? this.m_Payload.GetHashCode() : 0);
		}

		// Token: 0x0600BA95 RID: 47765 RVA: 0x00390FB1 File Offset: 0x0038F1B1
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_SourceName;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_Payload;
			return true;
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x00390FD4 File Offset: 0x0038F1D4
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.SourceName = ((value != null) ? ((string)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.Payload = ((value != null) ? value : null);
			return true;
		}

		// Token: 0x0600BA97 RID: 47767 RVA: 0x00391003 File Offset: 0x0038F203
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

		// Token: 0x04009958 RID: 39256
		public const int ModelId = 120;

		// Token: 0x04009959 RID: 39257
		private string m_SourceName;

		// Token: 0x0400995A RID: 39258
		private object m_Payload;

		// Token: 0x0400995B RID: 39259
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "source_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "payload",
				Type = typeof(object)
			}
		};
	}
}
