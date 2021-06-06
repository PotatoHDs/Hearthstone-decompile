using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B8 RID: 4280
	public class HeroClassIconsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BAD4 RID: 47828 RVA: 0x00391F94 File Offset: 0x00390194
		public HeroClassIconsDataModel()
		{
			base.RegisterNestedDataModel(this.m_Classes);
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600BAD5 RID: 47829 RVA: 0x00391FFF File Offset: 0x003901FF
		public int DataModelId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600BAD6 RID: 47830 RVA: 0x00392003 File Offset: 0x00390203
		public string DataModelDisplayName
		{
			get
			{
				return "hero_class_icons";
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600BAD8 RID: 47832 RVA: 0x00392043 File Offset: 0x00390243
		// (set) Token: 0x0600BAD7 RID: 47831 RVA: 0x0039200A File Offset: 0x0039020A
		public DataModelList<TAG_CLASS> Classes
		{
			get
			{
				return this.m_Classes;
			}
			set
			{
				if (this.m_Classes == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Classes);
				base.RegisterNestedDataModel(value);
				this.m_Classes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600BAD9 RID: 47833 RVA: 0x0039204B File Offset: 0x0039024B
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BADA RID: 47834 RVA: 0x00392053 File Offset: 0x00390253
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Classes != null) ? this.m_Classes.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BADB RID: 47835 RVA: 0x00392071 File Offset: 0x00390271
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Classes;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BADC RID: 47836 RVA: 0x00392084 File Offset: 0x00390284
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Classes = ((value != null) ? ((DataModelList<TAG_CLASS>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BADD RID: 47837 RVA: 0x0039209E File Offset: 0x0039029E
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

		// Token: 0x04009974 RID: 39284
		public const int ModelId = 33;

		// Token: 0x04009975 RID: 39285
		private DataModelList<TAG_CLASS> m_Classes = new DataModelList<TAG_CLASS>();

		// Token: 0x04009976 RID: 39286
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "classes",
				Type = typeof(DataModelList<TAG_CLASS>)
			}
		};
	}
}
