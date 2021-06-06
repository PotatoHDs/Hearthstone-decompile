using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C4 RID: 4292
	public class PriceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600BB71 RID: 47985 RVA: 0x00393CEA File Offset: 0x00391EEA
		public int DataModelId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600BB72 RID: 47986 RVA: 0x00393CEE File Offset: 0x00391EEE
		public string DataModelDisplayName
		{
			get
			{
				return "price";
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600BB74 RID: 47988 RVA: 0x00393D1B File Offset: 0x00391F1B
		// (set) Token: 0x0600BB73 RID: 47987 RVA: 0x00393CF5 File Offset: 0x00391EF5
		public float Amount
		{
			get
			{
				return this.m_Amount;
			}
			set
			{
				if (this.m_Amount == value)
				{
					return;
				}
				this.m_Amount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600BB76 RID: 47990 RVA: 0x00393D49 File Offset: 0x00391F49
		// (set) Token: 0x0600BB75 RID: 47989 RVA: 0x00393D23 File Offset: 0x00391F23
		public CurrencyType Currency
		{
			get
			{
				return this.m_Currency;
			}
			set
			{
				if (this.m_Currency == value)
				{
					return;
				}
				this.m_Currency = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600BB78 RID: 47992 RVA: 0x00393D7C File Offset: 0x00391F7C
		// (set) Token: 0x0600BB77 RID: 47991 RVA: 0x00393D51 File Offset: 0x00391F51
		public string DisplayText
		{
			get
			{
				return this.m_DisplayText;
			}
			set
			{
				if (this.m_DisplayText == value)
				{
					return;
				}
				this.m_DisplayText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600BB79 RID: 47993 RVA: 0x00393D84 File Offset: 0x00391F84
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB7A RID: 47994 RVA: 0x00393D8C File Offset: 0x00391F8C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			float amount = this.m_Amount;
			int num2 = (num + this.m_Amount.GetHashCode()) * 31;
			CurrencyType currency = this.m_Currency;
			return (num2 + this.m_Currency.GetHashCode()) * 31 + ((this.m_DisplayText != null) ? this.m_DisplayText.GetHashCode() : 0);
		}

		// Token: 0x0600BB7B RID: 47995 RVA: 0x00393DE8 File Offset: 0x00391FE8
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Amount;
				return true;
			case 1:
				value = this.m_Currency;
				return true;
			case 2:
				value = this.m_DisplayText;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB7C RID: 47996 RVA: 0x00393E38 File Offset: 0x00392038
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Amount = ((value != null) ? ((float)value) : 0f);
				return true;
			case 1:
				this.Currency = ((value != null) ? ((CurrencyType)value) : CurrencyType.NONE);
				return true;
			case 2:
				this.DisplayText = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BB7D RID: 47997 RVA: 0x00393E9C File Offset: 0x0039209C
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

		// Token: 0x040099AA RID: 39338
		public const int ModelId = 29;

		// Token: 0x040099AB RID: 39339
		private float m_Amount;

		// Token: 0x040099AC RID: 39340
		private CurrencyType m_Currency;

		// Token: 0x040099AD RID: 39341
		private string m_DisplayText;

		// Token: 0x040099AE RID: 39342
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "amount",
				Type = typeof(float)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "currency",
				Type = typeof(CurrencyType)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "display_text",
				Type = typeof(string)
			}
		};
	}
}
