using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A5 RID: 4261
	public class AdventureLoadoutOptionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600B96B RID: 47467 RVA: 0x001A35D7 File Offset: 0x001A17D7
		public int DataModelId
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600B96C RID: 47468 RVA: 0x0038C631 File Offset: 0x0038A831
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_loadout_option";
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600B96E RID: 47470 RVA: 0x0038C65E File Offset: 0x0038A85E
		// (set) Token: 0x0600B96D RID: 47469 RVA: 0x0038C638 File Offset: 0x0038A838
		public bool Locked
		{
			get
			{
				return this.m_Locked;
			}
			set
			{
				if (this.m_Locked == value)
				{
					return;
				}
				this.m_Locked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600B970 RID: 47472 RVA: 0x0038C68C File Offset: 0x0038A88C
		// (set) Token: 0x0600B96F RID: 47471 RVA: 0x0038C666 File Offset: 0x0038A866
		public bool Completed
		{
			get
			{
				return this.m_Completed;
			}
			set
			{
				if (this.m_Completed == value)
				{
					return;
				}
				this.m_Completed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600B972 RID: 47474 RVA: 0x0038C6BA File Offset: 0x0038A8BA
		// (set) Token: 0x0600B971 RID: 47473 RVA: 0x0038C694 File Offset: 0x0038A894
		public bool NewlyUnlocked
		{
			get
			{
				return this.m_NewlyUnlocked;
			}
			set
			{
				if (this.m_NewlyUnlocked == value)
				{
					return;
				}
				this.m_NewlyUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600B974 RID: 47476 RVA: 0x0038C6ED File Offset: 0x0038A8ED
		// (set) Token: 0x0600B973 RID: 47475 RVA: 0x0038C6C2 File Offset: 0x0038A8C2
		public string LockedText
		{
			get
			{
				return this.m_LockedText;
			}
			set
			{
				if (this.m_LockedText == value)
				{
					return;
				}
				this.m_LockedText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600B976 RID: 47478 RVA: 0x0038C720 File Offset: 0x0038A920
		// (set) Token: 0x0600B975 RID: 47477 RVA: 0x0038C6F5 File Offset: 0x0038A8F5
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

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600B978 RID: 47480 RVA: 0x0038C74E File Offset: 0x0038A94E
		// (set) Token: 0x0600B977 RID: 47479 RVA: 0x0038C728 File Offset: 0x0038A928
		public bool IsSelectedOption
		{
			get
			{
				return this.m_IsSelectedOption;
			}
			set
			{
				if (this.m_IsSelectedOption == value)
				{
					return;
				}
				this.m_IsSelectedOption = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600B97A RID: 47482 RVA: 0x0038C781 File Offset: 0x0038A981
		// (set) Token: 0x0600B979 RID: 47481 RVA: 0x0038C756 File Offset: 0x0038A956
		public Material DisplayTexture
		{
			get
			{
				return this.m_DisplayTexture;
			}
			set
			{
				if (this.m_DisplayTexture == value)
				{
					return;
				}
				this.m_DisplayTexture = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600B97C RID: 47484 RVA: 0x0038C7B4 File Offset: 0x0038A9B4
		// (set) Token: 0x0600B97B RID: 47483 RVA: 0x0038C789 File Offset: 0x0038A989
		public Color DisplayColor
		{
			get
			{
				return this.m_DisplayColor;
			}
			set
			{
				if (this.m_DisplayColor == value)
				{
					return;
				}
				this.m_DisplayColor = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600B97E RID: 47486 RVA: 0x0038C7E2 File Offset: 0x0038A9E2
		// (set) Token: 0x0600B97D RID: 47485 RVA: 0x0038C7BC File Offset: 0x0038A9BC
		public bool IsUpgraded
		{
			get
			{
				return this.m_IsUpgraded;
			}
			set
			{
				if (this.m_IsUpgraded == value)
				{
					return;
				}
				this.m_IsUpgraded = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600B97F RID: 47487 RVA: 0x0038C7EA File Offset: 0x0038A9EA
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B980 RID: 47488 RVA: 0x0038C7F4 File Offset: 0x0038A9F4
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool locked = this.m_Locked;
			int num2 = (num + this.m_Locked.GetHashCode()) * 31;
			bool completed = this.m_Completed;
			int num3 = (num2 + this.m_Completed.GetHashCode()) * 31;
			bool newlyUnlocked = this.m_NewlyUnlocked;
			int num4 = (((num3 + this.m_NewlyUnlocked.GetHashCode()) * 31 + ((this.m_LockedText != null) ? this.m_LockedText.GetHashCode() : 0)) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31;
			bool isSelectedOption = this.m_IsSelectedOption;
			int num5 = ((num4 + this.m_IsSelectedOption.GetHashCode()) * 31 + ((this.m_DisplayTexture != null) ? this.m_DisplayTexture.GetHashCode() : 0)) * 31;
			Color displayColor = this.m_DisplayColor;
			int num6 = (num5 + this.m_DisplayColor.GetHashCode()) * 31;
			bool isUpgraded = this.m_IsUpgraded;
			return num6 + this.m_IsUpgraded.GetHashCode();
		}

		// Token: 0x0600B981 RID: 47489 RVA: 0x0038C8E4 File Offset: 0x0038AAE4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Locked;
				return true;
			case 1:
				value = this.m_Completed;
				return true;
			case 2:
				value = this.m_NewlyUnlocked;
				return true;
			case 3:
				value = this.m_LockedText;
				return true;
			case 4:
				value = this.m_Name;
				return true;
			case 5:
				value = this.m_IsSelectedOption;
				return true;
			case 6:
				value = this.m_DisplayTexture;
				return true;
			case 7:
				value = this.m_DisplayColor;
				return true;
			case 8:
				value = this.m_IsUpgraded;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x0038C99C File Offset: 0x0038AB9C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Locked = (value != null && (bool)value);
				return true;
			case 1:
				this.Completed = (value != null && (bool)value);
				return true;
			case 2:
				this.NewlyUnlocked = (value != null && (bool)value);
				return true;
			case 3:
				this.LockedText = ((value != null) ? ((string)value) : null);
				return true;
			case 4:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				this.IsSelectedOption = (value != null && (bool)value);
				return true;
			case 6:
				this.DisplayTexture = ((value != null) ? ((Material)value) : null);
				return true;
			case 7:
				this.DisplayColor = ((value != null) ? ((Color)value) : default(Color));
				return true;
			case 8:
				this.IsUpgraded = (value != null && (bool)value);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600B983 RID: 47491 RVA: 0x0038CA98 File Offset: 0x0038AC98
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

		// Token: 0x040098E5 RID: 39141
		public const int ModelId = 4;

		// Token: 0x040098E6 RID: 39142
		private bool m_Locked;

		// Token: 0x040098E7 RID: 39143
		private bool m_Completed;

		// Token: 0x040098E8 RID: 39144
		private bool m_NewlyUnlocked;

		// Token: 0x040098E9 RID: 39145
		private string m_LockedText;

		// Token: 0x040098EA RID: 39146
		private string m_Name;

		// Token: 0x040098EB RID: 39147
		private bool m_IsSelectedOption;

		// Token: 0x040098EC RID: 39148
		private Material m_DisplayTexture;

		// Token: 0x040098ED RID: 39149
		private Color m_DisplayColor;

		// Token: 0x040098EE RID: 39150
		private bool m_IsUpgraded;

		// Token: 0x040098EF RID: 39151
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "locked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "completed",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "newly_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "locked_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "is_selected_option",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "display_texture",
				Type = typeof(Material)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "display_color",
				Type = typeof(Color)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "is_upgraded",
				Type = typeof(bool)
			}
		};
	}
}
