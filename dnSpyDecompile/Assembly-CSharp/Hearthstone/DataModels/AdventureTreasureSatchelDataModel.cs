using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A7 RID: 4263
	public class AdventureTreasureSatchelDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B99A RID: 47514 RVA: 0x0038D1C0 File Offset: 0x0038B3C0
		public AdventureTreasureSatchelDataModel()
		{
			base.RegisterNestedDataModel(this.m_Cards);
			base.RegisterNestedDataModel(this.m_LoadoutOptions);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600B99B RID: 47515 RVA: 0x0038D277 File Offset: 0x0038B477
		public int DataModelId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600B99C RID: 47516 RVA: 0x0038D27B File Offset: 0x0038B47B
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_treasure_satchel";
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600B99E RID: 47518 RVA: 0x0038D2BB File Offset: 0x0038B4BB
		// (set) Token: 0x0600B99D RID: 47517 RVA: 0x0038D282 File Offset: 0x0038B482
		public DataModelList<CardDataModel> Cards
		{
			get
			{
				return this.m_Cards;
			}
			set
			{
				if (this.m_Cards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Cards);
				base.RegisterNestedDataModel(value);
				this.m_Cards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600B9A0 RID: 47520 RVA: 0x0038D2FC File Offset: 0x0038B4FC
		// (set) Token: 0x0600B99F RID: 47519 RVA: 0x0038D2C3 File Offset: 0x0038B4C3
		public DataModelList<AdventureLoadoutOptionDataModel> LoadoutOptions
		{
			get
			{
				return this.m_LoadoutOptions;
			}
			set
			{
				if (this.m_LoadoutOptions == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_LoadoutOptions);
				base.RegisterNestedDataModel(value);
				this.m_LoadoutOptions = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600B9A1 RID: 47521 RVA: 0x0038D304 File Offset: 0x0038B504
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B9A2 RID: 47522 RVA: 0x0038D30C File Offset: 0x0038B50C
		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((this.m_Cards != null) ? this.m_Cards.GetPropertiesHashCode() : 0)) * 31 + ((this.m_LoadoutOptions != null) ? this.m_LoadoutOptions.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B9A3 RID: 47523 RVA: 0x0038D344 File Offset: 0x0038B544
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Cards;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_LoadoutOptions;
			return true;
		}

		// Token: 0x0600B9A4 RID: 47524 RVA: 0x0038D367 File Offset: 0x0038B567
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Cards = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.LoadoutOptions = ((value != null) ? ((DataModelList<AdventureLoadoutOptionDataModel>)value) : null);
			return true;
		}

		// Token: 0x0600B9A5 RID: 47525 RVA: 0x0038D39B File Offset: 0x0038B59B
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

		// Token: 0x040098F9 RID: 39161
		public const int ModelId = 32;

		// Token: 0x040098FA RID: 39162
		private DataModelList<CardDataModel> m_Cards = new DataModelList<CardDataModel>();

		// Token: 0x040098FB RID: 39163
		private DataModelList<AdventureLoadoutOptionDataModel> m_LoadoutOptions = new DataModelList<AdventureLoadoutOptionDataModel>();

		// Token: 0x040098FC RID: 39164
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "cards",
				Type = typeof(DataModelList<CardDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "loadout_options",
				Type = typeof(DataModelList<AdventureLoadoutOptionDataModel>)
			}
		};
	}
}
