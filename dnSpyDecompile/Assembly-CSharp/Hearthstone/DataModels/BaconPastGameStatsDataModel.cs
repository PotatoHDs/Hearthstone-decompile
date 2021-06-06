using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AA RID: 4266
	public class BaconPastGameStatsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B9CA RID: 47562 RVA: 0x0038DC68 File Offset: 0x0038BE68
		public BaconPastGameStatsDataModel()
		{
			base.RegisterNestedDataModel(this.m_Hero);
			base.RegisterNestedDataModel(this.m_HeroPower);
			base.RegisterNestedDataModel(this.m_Minions);
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600B9CB RID: 47563 RVA: 0x0038DDBF File Offset: 0x0038BFBF
		public int DataModelId
		{
			get
			{
				return 140;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600B9CC RID: 47564 RVA: 0x0038DDC6 File Offset: 0x0038BFC6
		public string DataModelDisplayName
		{
			get
			{
				return "baconpastgamestats";
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600B9CE RID: 47566 RVA: 0x0038DE06 File Offset: 0x0038C006
		// (set) Token: 0x0600B9CD RID: 47565 RVA: 0x0038DDCD File Offset: 0x0038BFCD
		public CardDataModel Hero
		{
			get
			{
				return this.m_Hero;
			}
			set
			{
				if (this.m_Hero == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Hero);
				base.RegisterNestedDataModel(value);
				this.m_Hero = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600B9D0 RID: 47568 RVA: 0x0038DE47 File Offset: 0x0038C047
		// (set) Token: 0x0600B9CF RID: 47567 RVA: 0x0038DE0E File Offset: 0x0038C00E
		public CardDataModel HeroPower
		{
			get
			{
				return this.m_HeroPower;
			}
			set
			{
				if (this.m_HeroPower == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_HeroPower);
				base.RegisterNestedDataModel(value);
				this.m_HeroPower = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600B9D2 RID: 47570 RVA: 0x0038DE75 File Offset: 0x0038C075
		// (set) Token: 0x0600B9D1 RID: 47569 RVA: 0x0038DE4F File Offset: 0x0038C04F
		public int Place
		{
			get
			{
				return this.m_Place;
			}
			set
			{
				if (this.m_Place == value)
				{
					return;
				}
				this.m_Place = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600B9D4 RID: 47572 RVA: 0x0038DEB6 File Offset: 0x0038C0B6
		// (set) Token: 0x0600B9D3 RID: 47571 RVA: 0x0038DE7D File Offset: 0x0038C07D
		public DataModelList<CardDataModel> Minions
		{
			get
			{
				return this.m_Minions;
			}
			set
			{
				if (this.m_Minions == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Minions);
				base.RegisterNestedDataModel(value);
				this.m_Minions = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600B9D6 RID: 47574 RVA: 0x0038DEE9 File Offset: 0x0038C0E9
		// (set) Token: 0x0600B9D5 RID: 47573 RVA: 0x0038DEBE File Offset: 0x0038C0BE
		public string HeroName
		{
			get
			{
				return this.m_HeroName;
			}
			set
			{
				if (this.m_HeroName == value)
				{
					return;
				}
				this.m_HeroName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600B9D7 RID: 47575 RVA: 0x0038DEF1 File Offset: 0x0038C0F1
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B9D8 RID: 47576 RVA: 0x0038DEFC File Offset: 0x0038C0FC
		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((this.m_Hero != null) ? this.m_Hero.GetPropertiesHashCode() : 0)) * 31 + ((this.m_HeroPower != null) ? this.m_HeroPower.GetPropertiesHashCode() : 0)) * 31;
			int place = this.m_Place;
			return ((num + this.m_Place.GetHashCode()) * 31 + ((this.m_Minions != null) ? this.m_Minions.GetPropertiesHashCode() : 0)) * 31 + ((this.m_HeroName != null) ? this.m_HeroName.GetHashCode() : 0);
		}

		// Token: 0x0600B9D9 RID: 47577 RVA: 0x0038DF8C File Offset: 0x0038C18C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 1:
				value = this.m_Hero;
				return true;
			case 2:
				value = this.m_HeroPower;
				return true;
			case 3:
				value = this.m_Place;
				return true;
			case 4:
				value = this.m_Minions;
				return true;
			case 5:
				value = this.m_HeroName;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B9DA RID: 47578 RVA: 0x0038DFF4 File Offset: 0x0038C1F4
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 1:
				this.Hero = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 2:
				this.HeroPower = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 3:
				this.Place = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.Minions = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 5:
				this.HeroName = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B9DB RID: 47579 RVA: 0x0038E084 File Offset: 0x0038C284
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 1:
				info = this.Properties[0];
				return true;
			case 2:
				info = this.Properties[1];
				return true;
			case 3:
				info = this.Properties[2];
				return true;
			case 4:
				info = this.Properties[3];
				return true;
			case 5:
				info = this.Properties[4];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400990B RID: 39179
		public const int ModelId = 140;

		// Token: 0x0400990C RID: 39180
		private CardDataModel m_Hero;

		// Token: 0x0400990D RID: 39181
		private CardDataModel m_HeroPower;

		// Token: 0x0400990E RID: 39182
		private int m_Place;

		// Token: 0x0400990F RID: 39183
		private DataModelList<CardDataModel> m_Minions = new DataModelList<CardDataModel>();

		// Token: 0x04009910 RID: 39184
		private string m_HeroName;

		// Token: 0x04009911 RID: 39185
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "hero",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "hero_power",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "place",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "minions",
				Type = typeof(DataModelList<CardDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "hero_name",
				Type = typeof(string)
			}
		};
	}
}
