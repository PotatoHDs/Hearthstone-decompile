using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BA RID: 4282
	public class HeroPickerDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600BAEF RID: 47855 RVA: 0x00392500 File Offset: 0x00390700
		public int DataModelId
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600BAF0 RID: 47856 RVA: 0x00392504 File Offset: 0x00390704
		public string DataModelDisplayName
		{
			get
			{
				return "hero_picker";
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600BAF2 RID: 47858 RVA: 0x00392531 File Offset: 0x00390731
		// (set) Token: 0x0600BAF1 RID: 47857 RVA: 0x0039250B File Offset: 0x0039070B
		public bool HasGuestHeroes
		{
			get
			{
				return this.m_HasGuestHeroes;
			}
			set
			{
				if (this.m_HasGuestHeroes == value)
				{
					return;
				}
				this.m_HasGuestHeroes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600BAF3 RID: 47859 RVA: 0x00392539 File Offset: 0x00390739
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BAF4 RID: 47860 RVA: 0x00392541 File Offset: 0x00390741
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool hasGuestHeroes = this.m_HasGuestHeroes;
			return num + this.m_HasGuestHeroes.GetHashCode();
		}

		// Token: 0x0600BAF5 RID: 47861 RVA: 0x0039255B File Offset: 0x0039075B
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_HasGuestHeroes;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BAF6 RID: 47862 RVA: 0x00392573 File Offset: 0x00390773
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.HasGuestHeroes = (value != null && (bool)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BAF7 RID: 47863 RVA: 0x0039258D File Offset: 0x0039078D
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

		// Token: 0x0400997D RID: 39293
		public const int ModelId = 13;

		// Token: 0x0400997E RID: 39294
		private bool m_HasGuestHeroes;

		// Token: 0x0400997F RID: 39295
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "has_guest_heroes",
				Type = typeof(bool)
			}
		};
	}
}
