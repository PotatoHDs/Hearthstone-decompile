using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A3 RID: 4259
	public class AdventureChooserDescriptionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B93C RID: 47420 RVA: 0x0038B798 File Offset: 0x00389998
		public AdventureChooserDescriptionDataModel()
		{
			base.RegisterNestedDataModel(this.m_Heroes);
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600B93D RID: 47421 RVA: 0x0038B835 File Offset: 0x00389A35
		public int DataModelId
		{
			get
			{
				return 202;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600B93E RID: 47422 RVA: 0x0038B83C File Offset: 0x00389A3C
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_chooser_description";
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600B940 RID: 47424 RVA: 0x0038B87C File Offset: 0x00389A7C
		// (set) Token: 0x0600B93F RID: 47423 RVA: 0x0038B843 File Offset: 0x00389A43
		public CardListDataModel Heroes
		{
			get
			{
				return this.m_Heroes;
			}
			set
			{
				if (this.m_Heroes == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Heroes);
				base.RegisterNestedDataModel(value);
				this.m_Heroes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600B942 RID: 47426 RVA: 0x0038B8AA File Offset: 0x00389AAA
		// (set) Token: 0x0600B941 RID: 47425 RVA: 0x0038B884 File Offset: 0x00389A84
		public bool HasNewHero
		{
			get
			{
				return this.m_HasNewHero;
			}
			set
			{
				if (this.m_HasNewHero == value)
				{
					return;
				}
				this.m_HasNewHero = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600B943 RID: 47427 RVA: 0x0038B8B2 File Offset: 0x00389AB2
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B944 RID: 47428 RVA: 0x0038B8BA File Offset: 0x00389ABA
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Heroes != null) ? this.m_Heroes.GetPropertiesHashCode() : 0)) * 31;
			bool hasNewHero = this.m_HasNewHero;
			return num + this.m_HasNewHero.GetHashCode();
		}

		// Token: 0x0600B945 RID: 47429 RVA: 0x0038B8EE File Offset: 0x00389AEE
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 203)
			{
				value = this.m_Heroes;
				return true;
			}
			if (id != 204)
			{
				value = null;
				return false;
			}
			value = this.m_HasNewHero;
			return true;
		}

		// Token: 0x0600B946 RID: 47430 RVA: 0x0038B91F File Offset: 0x00389B1F
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 203)
			{
				this.Heroes = ((value != null) ? ((CardListDataModel)value) : null);
				return true;
			}
			if (id != 204)
			{
				return false;
			}
			this.HasNewHero = (value != null && (bool)value);
			return true;
		}

		// Token: 0x0600B947 RID: 47431 RVA: 0x0038B95C File Offset: 0x00389B5C
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 203)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 204)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x040098D2 RID: 39122
		public const int ModelId = 202;

		// Token: 0x040098D3 RID: 39123
		private CardListDataModel m_Heroes;

		// Token: 0x040098D4 RID: 39124
		private bool m_HasNewHero;

		// Token: 0x040098D5 RID: 39125
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 203,
				PropertyDisplayName = "heroes",
				Type = typeof(CardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 204,
				PropertyDisplayName = "has_new_hero",
				Type = typeof(bool)
			}
		};
	}
}
