using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B0 RID: 4272
	public class DeckChoiceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600BA4D RID: 47693 RVA: 0x003900FF File Offset: 0x0038E2FF
		public int DataModelId
		{
			get
			{
				return 157;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600BA4E RID: 47694 RVA: 0x00390106 File Offset: 0x0038E306
		public string DataModelDisplayName
		{
			get
			{
				return "deck_choice";
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600BA50 RID: 47696 RVA: 0x00390133 File Offset: 0x0038E333
		// (set) Token: 0x0600BA4F RID: 47695 RVA: 0x0039010D File Offset: 0x0038E30D
		public int ChoiceClassID
		{
			get
			{
				return this.m_ChoiceClassID;
			}
			set
			{
				if (this.m_ChoiceClassID == value)
				{
					return;
				}
				this.m_ChoiceClassID = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600BA52 RID: 47698 RVA: 0x00390166 File Offset: 0x0038E366
		// (set) Token: 0x0600BA51 RID: 47697 RVA: 0x0039013B File Offset: 0x0038E33B
		public string ChoiceClassName
		{
			get
			{
				return this.m_ChoiceClassName;
			}
			set
			{
				if (this.m_ChoiceClassName == value)
				{
					return;
				}
				this.m_ChoiceClassName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600BA54 RID: 47700 RVA: 0x00390199 File Offset: 0x0038E399
		// (set) Token: 0x0600BA53 RID: 47699 RVA: 0x0039016E File Offset: 0x0038E36E
		public string DeckDescription
		{
			get
			{
				return this.m_DeckDescription;
			}
			set
			{
				if (this.m_DeckDescription == value)
				{
					return;
				}
				this.m_DeckDescription = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600BA56 RID: 47702 RVA: 0x003901CC File Offset: 0x0038E3CC
		// (set) Token: 0x0600BA55 RID: 47701 RVA: 0x003901A1 File Offset: 0x0038E3A1
		public string ButtonClass
		{
			get
			{
				return this.m_ButtonClass;
			}
			set
			{
				if (this.m_ButtonClass == value)
				{
					return;
				}
				this.m_ButtonClass = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600BA57 RID: 47703 RVA: 0x003901D4 File Offset: 0x0038E3D4
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA58 RID: 47704 RVA: 0x003901DC File Offset: 0x0038E3DC
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int choiceClassID = this.m_ChoiceClassID;
			return (((num + this.m_ChoiceClassID.GetHashCode()) * 31 + ((this.m_ChoiceClassName != null) ? this.m_ChoiceClassName.GetHashCode() : 0)) * 31 + ((this.m_DeckDescription != null) ? this.m_DeckDescription.GetHashCode() : 0)) * 31 + ((this.m_ButtonClass != null) ? this.m_ButtonClass.GetHashCode() : 0);
		}

		// Token: 0x0600BA59 RID: 47705 RVA: 0x00390250 File Offset: 0x0038E450
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_ChoiceClassID;
				return true;
			case 1:
				value = this.m_ChoiceClassName;
				return true;
			case 2:
				value = this.m_DeckDescription;
				return true;
			case 3:
				value = this.m_ButtonClass;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BA5A RID: 47706 RVA: 0x003902A8 File Offset: 0x0038E4A8
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.ChoiceClassID = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.ChoiceClassName = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.DeckDescription = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.ButtonClass = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BA5B RID: 47707 RVA: 0x00390320 File Offset: 0x0038E520
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009940 RID: 39232
		public const int ModelId = 157;

		// Token: 0x04009941 RID: 39233
		private int m_ChoiceClassID;

		// Token: 0x04009942 RID: 39234
		private string m_ChoiceClassName;

		// Token: 0x04009943 RID: 39235
		private string m_DeckDescription;

		// Token: 0x04009944 RID: 39236
		private string m_ButtonClass;

		// Token: 0x04009945 RID: 39237
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "choice_class_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "choice_class_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "deck_description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "button_class_name",
				Type = typeof(string)
			}
		};
	}
}
