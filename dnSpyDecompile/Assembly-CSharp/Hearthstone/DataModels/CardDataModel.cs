using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AD RID: 4269
	public class CardDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BA18 RID: 47640 RVA: 0x0038F398 File Offset: 0x0038D598
		public CardDataModel()
		{
			base.RegisterNestedDataModel(this.m_SpellTypes);
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600BA19 RID: 47641 RVA: 0x0038F5AC File Offset: 0x0038D7AC
		public int DataModelId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600BA1A RID: 47642 RVA: 0x0038F5B0 File Offset: 0x0038D7B0
		public string DataModelDisplayName
		{
			get
			{
				return "card";
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600BA1C RID: 47644 RVA: 0x0038F5E2 File Offset: 0x0038D7E2
		// (set) Token: 0x0600BA1B RID: 47643 RVA: 0x0038F5B7 File Offset: 0x0038D7B7
		public string CardId
		{
			get
			{
				return this.m_CardId;
			}
			set
			{
				if (this.m_CardId == value)
				{
					return;
				}
				this.m_CardId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600BA1E RID: 47646 RVA: 0x0038F610 File Offset: 0x0038D810
		// (set) Token: 0x0600BA1D RID: 47645 RVA: 0x0038F5EA File Offset: 0x0038D7EA
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

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600BA20 RID: 47648 RVA: 0x0038F643 File Offset: 0x0038D843
		// (set) Token: 0x0600BA1F RID: 47647 RVA: 0x0038F618 File Offset: 0x0038D818
		public string FlavorText
		{
			get
			{
				return this.m_FlavorText;
			}
			set
			{
				if (this.m_FlavorText == value)
				{
					return;
				}
				this.m_FlavorText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600BA22 RID: 47650 RVA: 0x0038F671 File Offset: 0x0038D871
		// (set) Token: 0x0600BA21 RID: 47649 RVA: 0x0038F64B File Offset: 0x0038D84B
		public int Attack
		{
			get
			{
				return this.m_Attack;
			}
			set
			{
				if (this.m_Attack == value)
				{
					return;
				}
				this.m_Attack = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600BA24 RID: 47652 RVA: 0x0038F69F File Offset: 0x0038D89F
		// (set) Token: 0x0600BA23 RID: 47651 RVA: 0x0038F679 File Offset: 0x0038D879
		public int Health
		{
			get
			{
				return this.m_Health;
			}
			set
			{
				if (this.m_Health == value)
				{
					return;
				}
				this.m_Health = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600BA26 RID: 47654 RVA: 0x0038F6CD File Offset: 0x0038D8CD
		// (set) Token: 0x0600BA25 RID: 47653 RVA: 0x0038F6A7 File Offset: 0x0038D8A7
		public int Mana
		{
			get
			{
				return this.m_Mana;
			}
			set
			{
				if (this.m_Mana == value)
				{
					return;
				}
				this.m_Mana = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600BA28 RID: 47656 RVA: 0x0038F70E File Offset: 0x0038D90E
		// (set) Token: 0x0600BA27 RID: 47655 RVA: 0x0038F6D5 File Offset: 0x0038D8D5
		public DataModelList<SpellType> SpellTypes
		{
			get
			{
				return this.m_SpellTypes;
			}
			set
			{
				if (this.m_SpellTypes == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_SpellTypes);
				base.RegisterNestedDataModel(value);
				this.m_SpellTypes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600BA2A RID: 47658 RVA: 0x0038F741 File Offset: 0x0038D941
		// (set) Token: 0x0600BA29 RID: 47657 RVA: 0x0038F716 File Offset: 0x0038D916
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name == value)
				{
					return;
				}
				this.m_Name = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600BA2C RID: 47660 RVA: 0x0038F76F File Offset: 0x0038D96F
		// (set) Token: 0x0600BA2B RID: 47659 RVA: 0x0038F749 File Offset: 0x0038D949
		public bool Owned
		{
			get
			{
				return this.m_Owned;
			}
			set
			{
				if (this.m_Owned == value)
				{
					return;
				}
				this.m_Owned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600BA2D RID: 47661 RVA: 0x0038F777 File Offset: 0x0038D977
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA2E RID: 47662 RVA: 0x0038F780 File Offset: 0x0038D980
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_CardId != null) ? this.m_CardId.GetHashCode() : 0)) * 31;
			TAG_PREMIUM premium = this.m_Premium;
			int num2 = ((num + this.m_Premium.GetHashCode()) * 31 + ((this.m_FlavorText != null) ? this.m_FlavorText.GetHashCode() : 0)) * 31;
			int attack = this.m_Attack;
			int num3 = (num2 + this.m_Attack.GetHashCode()) * 31;
			int health = this.m_Health;
			int num4 = (num3 + this.m_Health.GetHashCode()) * 31;
			int mana = this.m_Mana;
			int num5 = (((num4 + this.m_Mana.GetHashCode()) * 31 + ((this.m_SpellTypes != null) ? this.m_SpellTypes.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31;
			bool owned = this.m_Owned;
			return num5 + this.m_Owned.GetHashCode();
		}

		// Token: 0x0600BA2F RID: 47663 RVA: 0x0038F86C File Offset: 0x0038DA6C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_CardId;
				return true;
			case 1:
				value = this.m_Premium;
				return true;
			case 2:
				value = this.m_FlavorText;
				return true;
			case 3:
				value = this.m_Attack;
				return true;
			case 4:
				value = this.m_Health;
				return true;
			case 5:
				value = this.m_Mana;
				return true;
			case 6:
				value = this.m_SpellTypes;
				return true;
			case 7:
				value = this.m_Name;
				return true;
			case 8:
				value = this.m_Owned;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BA30 RID: 47664 RVA: 0x0038F91C File Offset: 0x0038DB1C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.CardId = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 2:
				this.FlavorText = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.Attack = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.Health = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.Mana = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				this.SpellTypes = ((value != null) ? ((DataModelList<SpellType>)value) : null);
				return true;
			case 7:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				this.Owned = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BA31 RID: 47665 RVA: 0x0038FA10 File Offset: 0x0038DC10
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
			case 4:
				info = this.Properties[4];
				return true;
			case 5:
				info = this.Properties[5];
				return true;
			case 6:
				info = this.Properties[6];
				return true;
			case 7:
				info = this.Properties[7];
				return true;
			case 8:
				info = this.Properties[8];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400992C RID: 39212
		public const int ModelId = 27;

		// Token: 0x0400992D RID: 39213
		private string m_CardId;

		// Token: 0x0400992E RID: 39214
		private TAG_PREMIUM m_Premium;

		// Token: 0x0400992F RID: 39215
		private string m_FlavorText;

		// Token: 0x04009930 RID: 39216
		private int m_Attack;

		// Token: 0x04009931 RID: 39217
		private int m_Health;

		// Token: 0x04009932 RID: 39218
		private int m_Mana;

		// Token: 0x04009933 RID: 39219
		private DataModelList<SpellType> m_SpellTypes = new DataModelList<SpellType>();

		// Token: 0x04009934 RID: 39220
		private string m_Name;

		// Token: 0x04009935 RID: 39221
		private bool m_Owned;

		// Token: 0x04009936 RID: 39222
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "id",
				Type = typeof(string)
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
				PropertyDisplayName = "flavor_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "attack",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "health",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "mana",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "spell_types",
				Type = typeof(DataModelList<SpellType>)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "owned",
				Type = typeof(bool)
			}
		};
	}
}
