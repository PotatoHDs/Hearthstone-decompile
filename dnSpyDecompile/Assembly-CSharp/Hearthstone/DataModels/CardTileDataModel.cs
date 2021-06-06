using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AF RID: 4271
	public class CardTileDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600BA3D RID: 47677 RVA: 0x0038FD4F File Offset: 0x0038DF4F
		public int DataModelId
		{
			get
			{
				return 262;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600BA3E RID: 47678 RVA: 0x0038FD56 File Offset: 0x0038DF56
		public string DataModelDisplayName
		{
			get
			{
				return "card_tile";
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600BA40 RID: 47680 RVA: 0x0038FD88 File Offset: 0x0038DF88
		// (set) Token: 0x0600BA3F RID: 47679 RVA: 0x0038FD5D File Offset: 0x0038DF5D
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600BA42 RID: 47682 RVA: 0x0038FDB6 File Offset: 0x0038DFB6
		// (set) Token: 0x0600BA41 RID: 47681 RVA: 0x0038FD90 File Offset: 0x0038DF90
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

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600BA44 RID: 47684 RVA: 0x0038FDE4 File Offset: 0x0038DFE4
		// (set) Token: 0x0600BA43 RID: 47683 RVA: 0x0038FDBE File Offset: 0x0038DFBE
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

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600BA46 RID: 47686 RVA: 0x0038FE12 File Offset: 0x0038E012
		// (set) Token: 0x0600BA45 RID: 47685 RVA: 0x0038FDEC File Offset: 0x0038DFEC
		public bool Selected
		{
			get
			{
				return this.m_Selected;
			}
			set
			{
				if (this.m_Selected == value)
				{
					return;
				}
				this.m_Selected = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600BA47 RID: 47687 RVA: 0x0038FE1A File Offset: 0x0038E01A
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA48 RID: 47688 RVA: 0x0038FE24 File Offset: 0x0038E024
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_CardId != null) ? this.m_CardId.GetHashCode() : 0)) * 31;
			TAG_PREMIUM premium = this.m_Premium;
			int num2 = (num + this.m_Premium.GetHashCode()) * 31;
			int count = this.m_Count;
			int num3 = (num2 + this.m_Count.GetHashCode()) * 31;
			bool selected = this.m_Selected;
			return num3 + this.m_Selected.GetHashCode();
		}

		// Token: 0x0600BA49 RID: 47689 RVA: 0x0038FE98 File Offset: 0x0038E098
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 263:
				value = this.m_CardId;
				return true;
			case 264:
				value = this.m_Premium;
				return true;
			case 265:
				value = this.m_Count;
				return true;
			default:
				if (id != 277)
				{
					value = null;
					return false;
				}
				value = this.m_Selected;
				return true;
			}
		}

		// Token: 0x0600BA4A RID: 47690 RVA: 0x0038FF04 File Offset: 0x0038E104
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 263:
				this.CardId = ((value != null) ? ((string)value) : null);
				return true;
			case 264:
				this.Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 265:
				this.Count = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				if (id != 277)
				{
					return false;
				}
				this.Selected = (value != null && (bool)value);
				return true;
			}
		}

		// Token: 0x0600BA4B RID: 47691 RVA: 0x0038FF84 File Offset: 0x0038E184
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 263:
				info = this.Properties[0];
				return true;
			case 264:
				info = this.Properties[1];
				return true;
			case 265:
				info = this.Properties[2];
				return true;
			default:
				if (id != 277)
				{
					info = default(DataModelProperty);
					return false;
				}
				info = this.Properties[3];
				return true;
			}
		}

		// Token: 0x0400993A RID: 39226
		public const int ModelId = 262;

		// Token: 0x0400993B RID: 39227
		private string m_CardId;

		// Token: 0x0400993C RID: 39228
		private TAG_PREMIUM m_Premium;

		// Token: 0x0400993D RID: 39229
		private int m_Count;

		// Token: 0x0400993E RID: 39230
		private bool m_Selected;

		// Token: 0x0400993F RID: 39231
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 263,
				PropertyDisplayName = "id",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 264,
				PropertyDisplayName = "premium",
				Type = typeof(TAG_PREMIUM)
			},
			new DataModelProperty
			{
				PropertyId = 265,
				PropertyDisplayName = "count",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 277,
				PropertyDisplayName = "selected",
				Type = typeof(bool)
			}
		};
	}
}
