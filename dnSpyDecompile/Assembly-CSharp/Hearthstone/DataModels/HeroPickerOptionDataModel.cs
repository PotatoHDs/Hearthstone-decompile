using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010BB RID: 4283
	public class HeroPickerOptionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600BAF9 RID: 47865 RVA: 0x0039270D File Offset: 0x0039090D
		public int DataModelId
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600BAFA RID: 47866 RVA: 0x00392710 File Offset: 0x00390910
		public string DataModelDisplayName
		{
			get
			{
				return "hero_picker_option";
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600BAFC RID: 47868 RVA: 0x0039273D File Offset: 0x0039093D
		// (set) Token: 0x0600BAFB RID: 47867 RVA: 0x00392717 File Offset: 0x00390917
		public bool IsTimelocked
		{
			get
			{
				return this.m_IsTimelocked;
			}
			set
			{
				if (this.m_IsTimelocked == value)
				{
					return;
				}
				this.m_IsTimelocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600BAFE RID: 47870 RVA: 0x0039276B File Offset: 0x0039096B
		// (set) Token: 0x0600BAFD RID: 47869 RVA: 0x00392745 File Offset: 0x00390945
		public bool IsNewlyUnlocked
		{
			get
			{
				return this.m_IsNewlyUnlocked;
			}
			set
			{
				if (this.m_IsNewlyUnlocked == value)
				{
					return;
				}
				this.m_IsNewlyUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600BB00 RID: 47872 RVA: 0x0039279E File Offset: 0x0039099E
		// (set) Token: 0x0600BAFF RID: 47871 RVA: 0x00392773 File Offset: 0x00390973
		public string TimeLockInfoText
		{
			get
			{
				return this.m_TimeLockInfoText;
			}
			set
			{
				if (this.m_TimeLockInfoText == value)
				{
					return;
				}
				this.m_TimeLockInfoText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600BB02 RID: 47874 RVA: 0x003927CC File Offset: 0x003909CC
		// (set) Token: 0x0600BB01 RID: 47873 RVA: 0x003927A6 File Offset: 0x003909A6
		public bool IsUnowned
		{
			get
			{
				return this.m_IsUnowned;
			}
			set
			{
				if (this.m_IsUnowned == value)
				{
					return;
				}
				this.m_IsUnowned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600BB04 RID: 47876 RVA: 0x003927FF File Offset: 0x003909FF
		// (set) Token: 0x0600BB03 RID: 47875 RVA: 0x003927D4 File Offset: 0x003909D4
		public string UnlockCriteriaText
		{
			get
			{
				return this.m_UnlockCriteriaText;
			}
			set
			{
				if (this.m_UnlockCriteriaText == value)
				{
					return;
				}
				this.m_UnlockCriteriaText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600BB06 RID: 47878 RVA: 0x00392832 File Offset: 0x00390A32
		// (set) Token: 0x0600BB05 RID: 47877 RVA: 0x00392807 File Offset: 0x00390A07
		public string ComingSoonText
		{
			get
			{
				return this.m_ComingSoonText;
			}
			set
			{
				if (this.m_ComingSoonText == value)
				{
					return;
				}
				this.m_ComingSoonText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600BB07 RID: 47879 RVA: 0x0039283A File Offset: 0x00390A3A
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB08 RID: 47880 RVA: 0x00392844 File Offset: 0x00390A44
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool isTimelocked = this.m_IsTimelocked;
			int num2 = (num + this.m_IsTimelocked.GetHashCode()) * 31;
			bool isNewlyUnlocked = this.m_IsNewlyUnlocked;
			int num3 = ((num2 + this.m_IsNewlyUnlocked.GetHashCode()) * 31 + ((this.m_TimeLockInfoText != null) ? this.m_TimeLockInfoText.GetHashCode() : 0)) * 31;
			bool isUnowned = this.m_IsUnowned;
			return ((num3 + this.m_IsUnowned.GetHashCode()) * 31 + ((this.m_UnlockCriteriaText != null) ? this.m_UnlockCriteriaText.GetHashCode() : 0)) * 31 + ((this.m_ComingSoonText != null) ? this.m_ComingSoonText.GetHashCode() : 0);
		}

		// Token: 0x0600BB09 RID: 47881 RVA: 0x003928E4 File Offset: 0x00390AE4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_IsTimelocked;
				return true;
			case 1:
				value = this.m_IsNewlyUnlocked;
				return true;
			case 2:
				value = this.m_TimeLockInfoText;
				return true;
			case 3:
				value = this.m_IsUnowned;
				return true;
			case 4:
				value = this.m_UnlockCriteriaText;
				return true;
			case 5:
				value = this.m_ComingSoonText;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB0A RID: 47882 RVA: 0x00392960 File Offset: 0x00390B60
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.IsTimelocked = (value != null && (bool)value);
				return true;
			case 1:
				this.IsNewlyUnlocked = (value != null && (bool)value);
				return true;
			case 2:
				this.TimeLockInfoText = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.IsUnowned = (value != null && (bool)value);
				return true;
			case 4:
				this.UnlockCriteriaText = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				this.ComingSoonText = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BB0B RID: 47883 RVA: 0x00392A08 File Offset: 0x00390C08
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009980 RID: 39296
		public const int ModelId = 6;

		// Token: 0x04009981 RID: 39297
		private bool m_IsTimelocked;

		// Token: 0x04009982 RID: 39298
		private bool m_IsNewlyUnlocked;

		// Token: 0x04009983 RID: 39299
		private string m_TimeLockInfoText;

		// Token: 0x04009984 RID: 39300
		private bool m_IsUnowned;

		// Token: 0x04009985 RID: 39301
		private string m_UnlockCriteriaText;

		// Token: 0x04009986 RID: 39302
		private string m_ComingSoonText;

		// Token: 0x04009987 RID: 39303
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "is_timelocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "is_newly_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "time_lock_info_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_unowned",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "unlock_criteria_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "coming_soon_text",
				Type = typeof(string)
			}
		};
	}
}
