using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B3 RID: 4275
	public class DeviceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600BA7B RID: 47739 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public int DataModelId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600BA7C RID: 47740 RVA: 0x00390B54 File Offset: 0x0038ED54
		public string DataModelDisplayName
		{
			get
			{
				return "device";
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600BA7E RID: 47742 RVA: 0x00390B81 File Offset: 0x0038ED81
		// (set) Token: 0x0600BA7D RID: 47741 RVA: 0x00390B5B File Offset: 0x0038ED5B
		public bool Mobile
		{
			get
			{
				return this.m_Mobile;
			}
			set
			{
				if (this.m_Mobile == value)
				{
					return;
				}
				this.m_Mobile = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600BA80 RID: 47744 RVA: 0x00390BAF File Offset: 0x0038EDAF
		// (set) Token: 0x0600BA7F RID: 47743 RVA: 0x00390B89 File Offset: 0x0038ED89
		public OSCategory Category
		{
			get
			{
				return this.m_Category;
			}
			set
			{
				if (this.m_Category == value)
				{
					return;
				}
				this.m_Category = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600BA82 RID: 47746 RVA: 0x00390BDD File Offset: 0x0038EDDD
		// (set) Token: 0x0600BA81 RID: 47745 RVA: 0x00390BB7 File Offset: 0x0038EDB7
		public bool Notch
		{
			get
			{
				return this.m_Notch;
			}
			set
			{
				if (this.m_Notch == value)
				{
					return;
				}
				this.m_Notch = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600BA84 RID: 47748 RVA: 0x00390C0B File Offset: 0x0038EE0B
		// (set) Token: 0x0600BA83 RID: 47747 RVA: 0x00390BE5 File Offset: 0x0038EDE5
		public ScreenCategory Screen
		{
			get
			{
				return this.m_Screen;
			}
			set
			{
				if (this.m_Screen == value)
				{
					return;
				}
				this.m_Screen = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600BA86 RID: 47750 RVA: 0x00390C39 File Offset: 0x0038EE39
		// (set) Token: 0x0600BA85 RID: 47749 RVA: 0x00390C13 File Offset: 0x0038EE13
		public AspectRatio AspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (this.m_AspectRatio == value)
				{
					return;
				}
				this.m_AspectRatio = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600BA87 RID: 47751 RVA: 0x00390C41 File Offset: 0x0038EE41
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA88 RID: 47752 RVA: 0x00390C4C File Offset: 0x0038EE4C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool mobile = this.m_Mobile;
			int num2 = (num + this.m_Mobile.GetHashCode()) * 31;
			OSCategory category = this.m_Category;
			int num3 = (num2 + this.m_Category.GetHashCode()) * 31;
			bool notch = this.m_Notch;
			int num4 = (num3 + this.m_Notch.GetHashCode()) * 31;
			ScreenCategory screen = this.m_Screen;
			int num5 = (num4 + this.m_Screen.GetHashCode()) * 31;
			AspectRatio aspectRatio = this.m_AspectRatio;
			return num5 + this.m_AspectRatio.GetHashCode();
		}

		// Token: 0x0600BA89 RID: 47753 RVA: 0x00390CDC File Offset: 0x0038EEDC
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Mobile;
				return true;
			case 1:
				value = this.m_Category;
				return true;
			case 2:
				value = this.m_Notch;
				return true;
			case 3:
				value = this.m_Screen;
				return true;
			case 4:
				value = this.m_AspectRatio;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BA8A RID: 47754 RVA: 0x00390D54 File Offset: 0x0038EF54
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Mobile = (value != null && (bool)value);
				return true;
			case 1:
				this.Category = ((value != null) ? ((OSCategory)value) : ((OSCategory)0));
				return true;
			case 2:
				this.Notch = (value != null && (bool)value);
				return true;
			case 3:
				this.Screen = ((value != null) ? ((ScreenCategory)value) : ((ScreenCategory)0));
				return true;
			case 4:
				this.AspectRatio = ((value != null) ? ((AspectRatio)value) : AspectRatio.Unknown);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BA8B RID: 47755 RVA: 0x00390DE4 File Offset: 0x0038EFE4
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009951 RID: 39249
		public const int ModelId = 0;

		// Token: 0x04009952 RID: 39250
		private bool m_Mobile;

		// Token: 0x04009953 RID: 39251
		private OSCategory m_Category;

		// Token: 0x04009954 RID: 39252
		private bool m_Notch;

		// Token: 0x04009955 RID: 39253
		private ScreenCategory m_Screen;

		// Token: 0x04009956 RID: 39254
		private AspectRatio m_AspectRatio;

		// Token: 0x04009957 RID: 39255
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "mobile",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "category",
				Type = typeof(OSCategory)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "notch",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "screen",
				Type = typeof(ScreenCategory)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "aspect_ratio",
				Type = typeof(AspectRatio)
			}
		};
	}
}
