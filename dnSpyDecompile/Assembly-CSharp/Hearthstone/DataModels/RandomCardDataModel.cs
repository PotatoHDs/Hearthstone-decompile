using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D3 RID: 4307
	public class RandomCardDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600BCAB RID: 48299 RVA: 0x003990F2 File Offset: 0x003972F2
		public int DataModelId
		{
			get
			{
				return 125;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600BCAC RID: 48300 RVA: 0x003990F6 File Offset: 0x003972F6
		public string DataModelDisplayName
		{
			get
			{
				return "random_card";
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600BCAE RID: 48302 RVA: 0x00399123 File Offset: 0x00397323
		// (set) Token: 0x0600BCAD RID: 48301 RVA: 0x003990FD File Offset: 0x003972FD
		public TAG_RARITY Rarity
		{
			get
			{
				return this.m_Rarity;
			}
			set
			{
				if (this.m_Rarity == value)
				{
					return;
				}
				this.m_Rarity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600BCB0 RID: 48304 RVA: 0x00399151 File Offset: 0x00397351
		// (set) Token: 0x0600BCAF RID: 48303 RVA: 0x0039912B File Offset: 0x0039732B
		public TAG_PREMIUM Premium
		{
			get
			{
				return this.m_Premium;
			}
			set
			{
				if (this.m_Premium == value)
				{
					return;
				}
				this.m_Premium = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600BCB2 RID: 48306 RVA: 0x0039917F File Offset: 0x0039737F
		// (set) Token: 0x0600BCB1 RID: 48305 RVA: 0x00399159 File Offset: 0x00397359
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				if (this.m_Count == value)
				{
					return;
				}
				this.m_Count = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600BCB3 RID: 48307 RVA: 0x00399187 File Offset: 0x00397387
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BCB4 RID: 48308 RVA: 0x00399190 File Offset: 0x00397390
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			TAG_RARITY rarity = this.m_Rarity;
			int num2 = (num + this.m_Rarity.GetHashCode()) * 31;
			TAG_PREMIUM premium = this.m_Premium;
			int num3 = (num2 + this.m_Premium.GetHashCode()) * 31;
			int count = this.m_Count;
			return num3 + this.m_Count.GetHashCode();
		}

		// Token: 0x0600BCB5 RID: 48309 RVA: 0x003991F0 File Offset: 0x003973F0
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Rarity;
				return true;
			case 1:
				value = this.m_Premium;
				return true;
			case 2:
				value = this.m_Count;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BCB6 RID: 48310 RVA: 0x00399244 File Offset: 0x00397444
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Rarity = ((value != null) ? ((TAG_RARITY)value) : TAG_RARITY.INVALID);
				return true;
			case 1:
				this.Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 2:
				this.Count = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BCB7 RID: 48311 RVA: 0x003992A4 File Offset: 0x003974A4
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

		// Token: 0x04009A29 RID: 39465
		public const int ModelId = 125;

		// Token: 0x04009A2A RID: 39466
		private TAG_RARITY m_Rarity;

		// Token: 0x04009A2B RID: 39467
		private TAG_PREMIUM m_Premium;

		// Token: 0x04009A2C RID: 39468
		private int m_Count;

		// Token: 0x04009A2D RID: 39469
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "rarity",
				Type = typeof(TAG_RARITY)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "premium",
				Type = typeof(TAG_PREMIUM)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "count",
				Type = typeof(int)
			}
		};
	}
}
