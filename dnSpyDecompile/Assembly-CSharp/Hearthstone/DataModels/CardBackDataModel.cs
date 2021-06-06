using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AC RID: 4268
	public class CardBackDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600BA0F RID: 47631 RVA: 0x0038F2E8 File Offset: 0x0038D4E8
		public int DataModelId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600BA10 RID: 47632 RVA: 0x0038F2EC File Offset: 0x0038D4EC
		public string DataModelDisplayName
		{
			get
			{
				return "cardback";
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600BA12 RID: 47634 RVA: 0x0038F319 File Offset: 0x0038D519
		// (set) Token: 0x0600BA11 RID: 47633 RVA: 0x0038F2F3 File Offset: 0x0038D4F3
		public int CardBackId
		{
			get
			{
				return this.m_CardBackId;
			}
			set
			{
				if (this.m_CardBackId == value)
				{
					return;
				}
				this.m_CardBackId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600BA13 RID: 47635 RVA: 0x0038F321 File Offset: 0x0038D521
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA14 RID: 47636 RVA: 0x0038F329 File Offset: 0x0038D529
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int cardBackId = this.m_CardBackId;
			return num + this.m_CardBackId.GetHashCode();
		}

		// Token: 0x0600BA15 RID: 47637 RVA: 0x0038F343 File Offset: 0x0038D543
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_CardBackId;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BA16 RID: 47638 RVA: 0x0038F35B File Offset: 0x0038D55B
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.CardBackId = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600BA17 RID: 47639 RVA: 0x0038F375 File Offset: 0x0038D575
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009929 RID: 39209
		public const int ModelId = 26;

		// Token: 0x0400992A RID: 39210
		private int m_CardBackId;

		// Token: 0x0400992B RID: 39211
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "id",
				Type = typeof(int)
			}
		};
	}
}
