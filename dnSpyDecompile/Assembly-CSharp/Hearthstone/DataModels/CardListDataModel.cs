using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AE RID: 4270
	public class CardListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BA32 RID: 47666 RVA: 0x0038FB08 File Offset: 0x0038DD08
		public CardListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Cards);
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600BA33 RID: 47667 RVA: 0x0038FB77 File Offset: 0x0038DD77
		public int DataModelId
		{
			get
			{
				return 205;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600BA34 RID: 47668 RVA: 0x0038FB7E File Offset: 0x0038DD7E
		public string DataModelDisplayName
		{
			get
			{
				return "card_list";
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600BA36 RID: 47670 RVA: 0x0038FBBE File Offset: 0x0038DDBE
		// (set) Token: 0x0600BA35 RID: 47669 RVA: 0x0038FB85 File Offset: 0x0038DD85
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600BA37 RID: 47671 RVA: 0x0038FBC6 File Offset: 0x0038DDC6
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA38 RID: 47672 RVA: 0x0038FBCE File Offset: 0x0038DDCE
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Cards != null) ? this.m_Cards.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BA39 RID: 47673 RVA: 0x0038FBEC File Offset: 0x0038DDEC
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 206)
			{
				value = this.m_Cards;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BA3A RID: 47674 RVA: 0x0038FC04 File Offset: 0x0038DE04
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 206)
			{
				this.Cards = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BA3B RID: 47675 RVA: 0x0038FC23 File Offset: 0x0038DE23
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 206)
			{
				info = this.Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009937 RID: 39223
		public const int ModelId = 205;

		// Token: 0x04009938 RID: 39224
		private DataModelList<CardDataModel> m_Cards = new DataModelList<CardDataModel>();

		// Token: 0x04009939 RID: 39225
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 206,
				PropertyDisplayName = "cards",
				Type = typeof(DataModelList<CardDataModel>)
			}
		};
	}
}
