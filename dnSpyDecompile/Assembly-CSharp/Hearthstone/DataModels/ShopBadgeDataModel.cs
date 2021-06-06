using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DD RID: 4317
	public class ShopBadgeDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600BD71 RID: 48497 RVA: 0x0039C3EF File Offset: 0x0039A5EF
		public int DataModelId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600BD72 RID: 48498 RVA: 0x0039C3F3 File Offset: 0x0039A5F3
		public string DataModelDisplayName
		{
			get
			{
				return "shop_badge";
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600BD74 RID: 48500 RVA: 0x0039C420 File Offset: 0x0039A620
		// (set) Token: 0x0600BD73 RID: 48499 RVA: 0x0039C3FA File Offset: 0x0039A5FA
		public ShopButtonDisplay.DisplayType BadgeType
		{
			get
			{
				return this.m_BadgeType;
			}
			set
			{
				if (this.m_BadgeType == value)
				{
					return;
				}
				this.m_BadgeType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x0600BD76 RID: 48502 RVA: 0x0039C44E File Offset: 0x0039A64E
		// (set) Token: 0x0600BD75 RID: 48501 RVA: 0x0039C428 File Offset: 0x0039A628
		public int AssetId
		{
			get
			{
				return this.m_AssetId;
			}
			set
			{
				if (this.m_AssetId == value)
				{
					return;
				}
				this.m_AssetId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600BD78 RID: 48504 RVA: 0x0039C47C File Offset: 0x0039A67C
		// (set) Token: 0x0600BD77 RID: 48503 RVA: 0x0039C456 File Offset: 0x0039A656
		public int Quantity
		{
			get
			{
				return this.m_Quantity;
			}
			set
			{
				if (this.m_Quantity == value)
				{
					return;
				}
				this.m_Quantity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600BD7A RID: 48506 RVA: 0x0039C4AA File Offset: 0x0039A6AA
		// (set) Token: 0x0600BD79 RID: 48505 RVA: 0x0039C484 File Offset: 0x0039A684
		public float Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				if (this.m_Scale == value)
				{
					return;
				}
				this.m_Scale = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600BD7B RID: 48507 RVA: 0x0039C4B2 File Offset: 0x0039A6B2
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD7C RID: 48508 RVA: 0x0039C4BC File Offset: 0x0039A6BC
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			ShopButtonDisplay.DisplayType badgeType = this.m_BadgeType;
			int num2 = (num + this.m_BadgeType.GetHashCode()) * 31;
			int assetId = this.m_AssetId;
			int num3 = (num2 + this.m_AssetId.GetHashCode()) * 31;
			int quantity = this.m_Quantity;
			int num4 = (num3 + this.m_Quantity.GetHashCode()) * 31;
			float scale = this.m_Scale;
			return num4 + this.m_Scale.GetHashCode();
		}

		// Token: 0x0600BD7D RID: 48509 RVA: 0x0039C52C File Offset: 0x0039A72C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_BadgeType;
				return true;
			case 1:
				value = this.m_AssetId;
				return true;
			case 2:
				value = this.m_Quantity;
				return true;
			case 3:
				value = this.m_Scale;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BD7E RID: 48510 RVA: 0x0039C594 File Offset: 0x0039A794
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.BadgeType = ((value != null) ? ((ShopButtonDisplay.DisplayType)value) : ShopButtonDisplay.DisplayType.BOOSTER);
				return true;
			case 1:
				this.AssetId = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.Scale = ((value != null) ? ((float)value) : 0f);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BD7F RID: 48511 RVA: 0x0039C610 File Offset: 0x0039A810
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

		// Token: 0x04009A78 RID: 39544
		public const int ModelId = 18;

		// Token: 0x04009A79 RID: 39545
		private ShopButtonDisplay.DisplayType m_BadgeType;

		// Token: 0x04009A7A RID: 39546
		private int m_AssetId;

		// Token: 0x04009A7B RID: 39547
		private int m_Quantity;

		// Token: 0x04009A7C RID: 39548
		private float m_Scale;

		// Token: 0x04009A7D RID: 39549
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "badge_type",
				Type = typeof(ShopButtonDisplay.DisplayType)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "asset_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "scale",
				Type = typeof(float)
			}
		};
	}
}
