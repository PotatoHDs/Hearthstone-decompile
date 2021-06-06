using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BE RID: 4286
	public class MessageDebugContentDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600BB27 RID: 47911 RVA: 0x00392FA0 File Offset: 0x003911A0
		public int DataModelId
		{
			get
			{
				return 299;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600BB28 RID: 47912 RVA: 0x00392FA7 File Offset: 0x003911A7
		public string DataModelDisplayName
		{
			get
			{
				return "message_debug_content";
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600BB2A RID: 47914 RVA: 0x00392FD9 File Offset: 0x003911D9
		// (set) Token: 0x0600BB29 RID: 47913 RVA: 0x00392FAE File Offset: 0x003911AE
		public string TestString
		{
			get
			{
				return this.m_TestString;
			}
			set
			{
				if (this.m_TestString == value)
				{
					return;
				}
				this.m_TestString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600BB2B RID: 47915 RVA: 0x00392FE1 File Offset: 0x003911E1
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB2C RID: 47916 RVA: 0x00392FE9 File Offset: 0x003911E9
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_TestString != null) ? this.m_TestString.GetHashCode() : 0);
		}

		// Token: 0x0600BB2D RID: 47917 RVA: 0x00393007 File Offset: 0x00391207
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_TestString;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BB2E RID: 47918 RVA: 0x0039301A File Offset: 0x0039121A
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.TestString = ((value != null) ? ((string)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BB2F RID: 47919 RVA: 0x00393034 File Offset: 0x00391234
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

		// Token: 0x04009991 RID: 39313
		public const int ModelId = 299;

		// Token: 0x04009992 RID: 39314
		private string m_TestString;

		// Token: 0x04009993 RID: 39315
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "test_string",
				Type = typeof(string)
			}
		};
	}
}
