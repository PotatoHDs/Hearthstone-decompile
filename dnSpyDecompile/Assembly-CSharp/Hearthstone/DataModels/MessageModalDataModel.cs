using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BF RID: 4287
	public class MessageModalDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600BB31 RID: 47921 RVA: 0x003930AC File Offset: 0x003912AC
		public int DataModelId
		{
			get
			{
				return 298;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600BB32 RID: 47922 RVA: 0x003930B3 File Offset: 0x003912B3
		public string DataModelDisplayName
		{
			get
			{
				return "message_modal";
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600BB34 RID: 47924 RVA: 0x003930E5 File Offset: 0x003912E5
		// (set) Token: 0x0600BB33 RID: 47923 RVA: 0x003930BA File Offset: 0x003912BA
		public string ContentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (this.m_ContentType == value)
				{
					return;
				}
				this.m_ContentType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600BB35 RID: 47925 RVA: 0x003930ED File Offset: 0x003912ED
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB36 RID: 47926 RVA: 0x003930F5 File Offset: 0x003912F5
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_ContentType != null) ? this.m_ContentType.GetHashCode() : 0);
		}

		// Token: 0x0600BB37 RID: 47927 RVA: 0x00393113 File Offset: 0x00391313
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_ContentType;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BB38 RID: 47928 RVA: 0x00393126 File Offset: 0x00391326
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.ContentType = ((value != null) ? ((string)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BB39 RID: 47929 RVA: 0x00393140 File Offset: 0x00391340
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009994 RID: 39316
		public const int ModelId = 298;

		// Token: 0x04009995 RID: 39317
		private string m_ContentType;

		// Token: 0x04009996 RID: 39318
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "content_type",
				Type = typeof(string)
			}
		};
	}
}
