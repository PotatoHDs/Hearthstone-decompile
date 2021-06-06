using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A6 RID: 4262
	public class AdventureMissionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B984 RID: 47492 RVA: 0x0038CB90 File Offset: 0x0038AD90
		public AdventureMissionDataModel()
		{
			base.RegisterNestedDataModel(this.m_Rewards);
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600B985 RID: 47493 RVA: 0x0038CD3C File Offset: 0x0038AF3C
		public int DataModelId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600B986 RID: 47494 RVA: 0x0038CD40 File Offset: 0x0038AF40
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_mission";
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600B988 RID: 47496 RVA: 0x0038CD6D File Offset: 0x0038AF6D
		// (set) Token: 0x0600B987 RID: 47495 RVA: 0x0038CD47 File Offset: 0x0038AF47
		public ScenarioDbId ScenarioId
		{
			get
			{
				return this.m_ScenarioId;
			}
			set
			{
				if (this.m_ScenarioId == value)
				{
					return;
				}
				this.m_ScenarioId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600B98A RID: 47498 RVA: 0x0038CD9B File Offset: 0x0038AF9B
		// (set) Token: 0x0600B989 RID: 47497 RVA: 0x0038CD75 File Offset: 0x0038AF75
		public AdventureMissionState MissionState
		{
			get
			{
				return this.m_MissionState;
			}
			set
			{
				if (this.m_MissionState == value)
				{
					return;
				}
				this.m_MissionState = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600B98C RID: 47500 RVA: 0x0038CDCE File Offset: 0x0038AFCE
		// (set) Token: 0x0600B98B RID: 47499 RVA: 0x0038CDA3 File Offset: 0x0038AFA3
		public Material CoinPortraitMaterial
		{
			get
			{
				return this.m_CoinPortraitMaterial;
			}
			set
			{
				if (this.m_CoinPortraitMaterial == value)
				{
					return;
				}
				this.m_CoinPortraitMaterial = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600B98E RID: 47502 RVA: 0x0038CDFC File Offset: 0x0038AFFC
		// (set) Token: 0x0600B98D RID: 47501 RVA: 0x0038CDD6 File Offset: 0x0038AFD6
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

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600B990 RID: 47504 RVA: 0x0038CE3D File Offset: 0x0038B03D
		// (set) Token: 0x0600B98F RID: 47503 RVA: 0x0038CE04 File Offset: 0x0038B004
		public RewardListDataModel Rewards
		{
			get
			{
				return this.m_Rewards;
			}
			set
			{
				if (this.m_Rewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Rewards);
				base.RegisterNestedDataModel(value);
				this.m_Rewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600B992 RID: 47506 RVA: 0x0038CE6B File Offset: 0x0038B06B
		// (set) Token: 0x0600B991 RID: 47505 RVA: 0x0038CE45 File Offset: 0x0038B045
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

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600B994 RID: 47508 RVA: 0x0038CE99 File Offset: 0x0038B099
		// (set) Token: 0x0600B993 RID: 47507 RVA: 0x0038CE73 File Offset: 0x0038B073
		public bool NewlyCompleted
		{
			get
			{
				return this.m_NewlyCompleted;
			}
			set
			{
				if (this.m_NewlyCompleted == value)
				{
					return;
				}
				this.m_NewlyCompleted = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600B995 RID: 47509 RVA: 0x0038CEA1 File Offset: 0x0038B0A1
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B996 RID: 47510 RVA: 0x0038CEAC File Offset: 0x0038B0AC
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			ScenarioDbId scenarioId = this.m_ScenarioId;
			int num2 = (num + this.m_ScenarioId.GetHashCode()) * 31;
			AdventureMissionState missionState = this.m_MissionState;
			int num3 = ((num2 + this.m_MissionState.GetHashCode()) * 31 + ((this.m_CoinPortraitMaterial != null) ? this.m_CoinPortraitMaterial.GetHashCode() : 0)) * 31;
			bool selected = this.m_Selected;
			int num4 = ((num3 + this.m_Selected.GetHashCode()) * 31 + ((this.m_Rewards != null) ? this.m_Rewards.GetPropertiesHashCode() : 0)) * 31;
			bool newlyUnlocked = this.m_NewlyUnlocked;
			int num5 = (num4 + this.m_NewlyUnlocked.GetHashCode()) * 31;
			bool newlyCompleted = this.m_NewlyCompleted;
			return num5 + this.m_NewlyCompleted.GetHashCode();
		}

		// Token: 0x0600B997 RID: 47511 RVA: 0x0038CF70 File Offset: 0x0038B170
		public bool GetPropertyValue(int id, out object value)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					value = this.m_ScenarioId;
					return true;
				}
				if (id == 1)
				{
					value = this.m_MissionState;
					return true;
				}
			}
			else
			{
				if (id == 115)
				{
					value = this.m_CoinPortraitMaterial;
					return true;
				}
				if (id == 118)
				{
					value = this.m_Selected;
					return true;
				}
				switch (id)
				{
				case 136:
					value = this.m_Rewards;
					return true;
				case 137:
					value = this.m_NewlyUnlocked;
					return true;
				case 138:
					value = this.m_NewlyCompleted;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600B998 RID: 47512 RVA: 0x0038D014 File Offset: 0x0038B214
		public bool SetPropertyValue(int id, object value)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					this.ScenarioId = ((value != null) ? ((ScenarioDbId)value) : ScenarioDbId.INVALID);
					return true;
				}
				if (id == 1)
				{
					this.MissionState = ((value != null) ? ((AdventureMissionState)value) : AdventureMissionState.LOCKED);
					return true;
				}
			}
			else
			{
				if (id == 115)
				{
					this.CoinPortraitMaterial = ((value != null) ? ((Material)value) : null);
					return true;
				}
				if (id == 118)
				{
					this.Selected = (value != null && (bool)value);
					return true;
				}
				switch (id)
				{
				case 136:
					this.Rewards = ((value != null) ? ((RewardListDataModel)value) : null);
					return true;
				case 137:
					this.NewlyUnlocked = (value != null && (bool)value);
					return true;
				case 138:
					this.NewlyCompleted = (value != null && (bool)value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x0038D0E8 File Offset: 0x0038B2E8
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					info = this.Properties[0];
					return true;
				}
				if (id == 1)
				{
					info = this.Properties[1];
					return true;
				}
			}
			else
			{
				if (id == 115)
				{
					info = this.Properties[2];
					return true;
				}
				if (id == 118)
				{
					info = this.Properties[3];
					return true;
				}
				switch (id)
				{
				case 136:
					info = this.Properties[4];
					return true;
				case 137:
					info = this.Properties[5];
					return true;
				case 138:
					info = this.Properties[6];
					return true;
				}
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x040098F0 RID: 39152
		public const int ModelId = 14;

		// Token: 0x040098F1 RID: 39153
		private ScenarioDbId m_ScenarioId;

		// Token: 0x040098F2 RID: 39154
		private AdventureMissionState m_MissionState;

		// Token: 0x040098F3 RID: 39155
		private Material m_CoinPortraitMaterial;

		// Token: 0x040098F4 RID: 39156
		private bool m_Selected;

		// Token: 0x040098F5 RID: 39157
		private RewardListDataModel m_Rewards;

		// Token: 0x040098F6 RID: 39158
		private bool m_NewlyUnlocked;

		// Token: 0x040098F7 RID: 39159
		private bool m_NewlyCompleted;

		// Token: 0x040098F8 RID: 39160
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "scenario_id",
				Type = typeof(ScenarioDbId)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mission_state",
				Type = typeof(AdventureMissionState)
			},
			new DataModelProperty
			{
				PropertyId = 115,
				PropertyDisplayName = "coin_portrait_material",
				Type = typeof(Material)
			},
			new DataModelProperty
			{
				PropertyId = 118,
				PropertyDisplayName = "selected",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 136,
				PropertyDisplayName = "rewards",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 137,
				PropertyDisplayName = "newly_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 138,
				PropertyDisplayName = "newly_completed",
				Type = typeof(bool)
			}
		};
	}
}
