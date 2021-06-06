using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B9 RID: 4281
	public class HeroDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BADE RID: 47838 RVA: 0x003920C0 File Offset: 0x003902C0
		public HeroDataModel()
		{
			base.RegisterNestedDataModel(this.m_HeroCard);
			base.RegisterNestedDataModel(this.m_HeroPowerCard);
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600BADF RID: 47839 RVA: 0x003921D2 File Offset: 0x003903D2
		public int DataModelId
		{
			get
			{
				return 111;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600BAE0 RID: 47840 RVA: 0x003921D6 File Offset: 0x003903D6
		public string DataModelDisplayName
		{
			get
			{
				return "hero";
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600BAE2 RID: 47842 RVA: 0x00392216 File Offset: 0x00390416
		// (set) Token: 0x0600BAE1 RID: 47841 RVA: 0x003921DD File Offset: 0x003903DD
		public CardDataModel HeroCard
		{
			get
			{
				return this.m_HeroCard;
			}
			set
			{
				if (this.m_HeroCard == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_HeroCard);
				base.RegisterNestedDataModel(value);
				this.m_HeroCard = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600BAE4 RID: 47844 RVA: 0x00392257 File Offset: 0x00390457
		// (set) Token: 0x0600BAE3 RID: 47843 RVA: 0x0039221E File Offset: 0x0039041E
		public CardDataModel HeroPowerCard
		{
			get
			{
				return this.m_HeroPowerCard;
			}
			set
			{
				if (this.m_HeroPowerCard == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_HeroPowerCard);
				base.RegisterNestedDataModel(value);
				this.m_HeroPowerCard = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600BAE6 RID: 47846 RVA: 0x0039228A File Offset: 0x0039048A
		// (set) Token: 0x0600BAE5 RID: 47845 RVA: 0x0039225F File Offset: 0x0039045F
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

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600BAE8 RID: 47848 RVA: 0x003922BD File Offset: 0x003904BD
		// (set) Token: 0x0600BAE7 RID: 47847 RVA: 0x00392292 File Offset: 0x00390492
		public string Description
		{
			get
			{
				return this.m_Description;
			}
			set
			{
				if (this.m_Description == value)
				{
					return;
				}
				this.m_Description = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600BAE9 RID: 47849 RVA: 0x003922C5 File Offset: 0x003904C5
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BAEA RID: 47850 RVA: 0x003922D0 File Offset: 0x003904D0
		public int GetPropertiesHashCode()
		{
			return (((17 * 31 + ((this.m_HeroCard != null) ? this.m_HeroCard.GetPropertiesHashCode() : 0)) * 31 + ((this.m_HeroPowerCard != null) ? this.m_HeroPowerCard.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0);
		}

		// Token: 0x0600BAEB RID: 47851 RVA: 0x00392348 File Offset: 0x00390548
		public bool GetPropertyValue(int id, out object value)
		{
			if (id <= 113)
			{
				if (id == 112)
				{
					value = this.m_HeroCard;
					return true;
				}
				if (id == 113)
				{
					value = this.m_HeroPowerCard;
					return true;
				}
			}
			else
			{
				if (id == 121)
				{
					value = this.m_Description;
					return true;
				}
				if (id == 141)
				{
					value = this.m_Name;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600BAEC RID: 47852 RVA: 0x003923A4 File Offset: 0x003905A4
		public bool SetPropertyValue(int id, object value)
		{
			if (id <= 113)
			{
				if (id == 112)
				{
					this.HeroCard = ((value != null) ? ((CardDataModel)value) : null);
					return true;
				}
				if (id == 113)
				{
					this.HeroPowerCard = ((value != null) ? ((CardDataModel)value) : null);
					return true;
				}
			}
			else
			{
				if (id == 121)
				{
					this.Description = ((value != null) ? ((string)value) : null);
					return true;
				}
				if (id == 141)
				{
					this.Name = ((value != null) ? ((string)value) : null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BAED RID: 47853 RVA: 0x00392424 File Offset: 0x00390624
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id <= 113)
			{
				if (id == 112)
				{
					info = this.Properties[0];
					return true;
				}
				if (id == 113)
				{
					info = this.Properties[1];
					return true;
				}
			}
			else
			{
				if (id == 121)
				{
					info = this.Properties[3];
					return true;
				}
				if (id == 141)
				{
					info = this.Properties[2];
					return true;
				}
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009977 RID: 39287
		public const int ModelId = 111;

		// Token: 0x04009978 RID: 39288
		private CardDataModel m_HeroCard;

		// Token: 0x04009979 RID: 39289
		private CardDataModel m_HeroPowerCard;

		// Token: 0x0400997A RID: 39290
		private string m_Name;

		// Token: 0x0400997B RID: 39291
		private string m_Description;

		// Token: 0x0400997C RID: 39292
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 112,
				PropertyDisplayName = "hero_card",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 113,
				PropertyDisplayName = "hero_power_card",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 141,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 121,
				PropertyDisplayName = "description",
				Type = typeof(string)
			}
		};
	}
}
