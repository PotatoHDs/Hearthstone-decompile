using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C0 RID: 4288
	public class MiniSetDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BB3A RID: 47930 RVA: 0x00393164 File Offset: 0x00391364
		public MiniSetDetailsDataModel()
		{
			base.RegisterNestedDataModel(this.m_CardTiles);
			base.RegisterNestedDataModel(this.m_SelectedCard);
			base.RegisterNestedDataModel(this.m_Pack);
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600BB3B RID: 47931 RVA: 0x0039325D File Offset: 0x0039145D
		public int DataModelId
		{
			get
			{
				return 266;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600BB3C RID: 47932 RVA: 0x00393264 File Offset: 0x00391464
		public string DataModelDisplayName
		{
			get
			{
				return "mini_set_details";
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600BB3E RID: 47934 RVA: 0x003932A4 File Offset: 0x003914A4
		// (set) Token: 0x0600BB3D RID: 47933 RVA: 0x0039326B File Offset: 0x0039146B
		public DataModelList<CardTileDataModel> CardTiles
		{
			get
			{
				return this.m_CardTiles;
			}
			set
			{
				if (this.m_CardTiles == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_CardTiles);
				base.RegisterNestedDataModel(value);
				this.m_CardTiles = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600BB40 RID: 47936 RVA: 0x003932E5 File Offset: 0x003914E5
		// (set) Token: 0x0600BB3F RID: 47935 RVA: 0x003932AC File Offset: 0x003914AC
		public CardDataModel SelectedCard
		{
			get
			{
				return this.m_SelectedCard;
			}
			set
			{
				if (this.m_SelectedCard == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_SelectedCard);
				base.RegisterNestedDataModel(value);
				this.m_SelectedCard = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x0600BB42 RID: 47938 RVA: 0x00393326 File Offset: 0x00391526
		// (set) Token: 0x0600BB41 RID: 47937 RVA: 0x003932ED File Offset: 0x003914ED
		public PackDataModel Pack
		{
			get
			{
				return this.m_Pack;
			}
			set
			{
				if (this.m_Pack == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Pack);
				base.RegisterNestedDataModel(value);
				this.m_Pack = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600BB43 RID: 47939 RVA: 0x0039332E File Offset: 0x0039152E
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB44 RID: 47940 RVA: 0x00393338 File Offset: 0x00391538
		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((this.m_CardTiles != null) ? this.m_CardTiles.GetPropertiesHashCode() : 0)) * 31 + ((this.m_SelectedCard != null) ? this.m_SelectedCard.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Pack != null) ? this.m_Pack.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BB45 RID: 47941 RVA: 0x00393395 File Offset: 0x00391595
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 267)
			{
				value = this.m_CardTiles;
				return true;
			}
			if (id == 269)
			{
				value = this.m_SelectedCard;
				return true;
			}
			if (id != 273)
			{
				value = null;
				return false;
			}
			value = this.m_Pack;
			return true;
		}

		// Token: 0x0600BB46 RID: 47942 RVA: 0x003933D4 File Offset: 0x003915D4
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 267)
			{
				this.CardTiles = ((value != null) ? ((DataModelList<CardTileDataModel>)value) : null);
				return true;
			}
			if (id == 269)
			{
				this.SelectedCard = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			}
			if (id != 273)
			{
				return false;
			}
			this.Pack = ((value != null) ? ((PackDataModel)value) : null);
			return true;
		}

		// Token: 0x0600BB47 RID: 47943 RVA: 0x00393438 File Offset: 0x00391638
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 267)
			{
				info = this.Properties[0];
				return true;
			}
			if (id == 269)
			{
				info = this.Properties[1];
				return true;
			}
			if (id != 273)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[2];
			return true;
		}

		// Token: 0x04009997 RID: 39319
		public const int ModelId = 266;

		// Token: 0x04009998 RID: 39320
		private DataModelList<CardTileDataModel> m_CardTiles = new DataModelList<CardTileDataModel>();

		// Token: 0x04009999 RID: 39321
		private CardDataModel m_SelectedCard;

		// Token: 0x0400999A RID: 39322
		private PackDataModel m_Pack;

		// Token: 0x0400999B RID: 39323
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 267,
				PropertyDisplayName = "card_tiles",
				Type = typeof(DataModelList<CardTileDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 269,
				PropertyDisplayName = "selected_card",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 273,
				PropertyDisplayName = "pack",
				Type = typeof(PackDataModel)
			}
		};
	}
}
