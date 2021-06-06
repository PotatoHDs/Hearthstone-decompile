using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010AB RID: 4267
	public class BaconStatsPageDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B9DC RID: 47580 RVA: 0x0038E11C File Offset: 0x0038C31C
		public BaconStatsPageDataModel()
		{
			base.RegisterNestedDataModel(this.m_BiggestMinionId);
			base.RegisterNestedDataModel(this.m_MostBoughtMinionsCardIds);
			base.RegisterNestedDataModel(this.m_MostBoughtMinionsCount);
			base.RegisterNestedDataModel(this.m_TopHeroesByWinCardIds);
			base.RegisterNestedDataModel(this.m_TopHeroesByWinCount);
			base.RegisterNestedDataModel(this.m_TopHeroesByGamesPlayedCardIds);
			base.RegisterNestedDataModel(this.m_TopHeroesByGamesPlayedCount);
			base.RegisterNestedDataModel(this.m_PastGames);
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600B9DD RID: 47581 RVA: 0x0038E65A File Offset: 0x0038C85A
		public int DataModelId
		{
			get
			{
				return 122;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600B9DE RID: 47582 RVA: 0x0038E65E File Offset: 0x0038C85E
		public string DataModelDisplayName
		{
			get
			{
				return "baconstatspage";
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600B9E0 RID: 47584 RVA: 0x0038E68B File Offset: 0x0038C88B
		// (set) Token: 0x0600B9DF RID: 47583 RVA: 0x0038E665 File Offset: 0x0038C865
		public int TriplesCreated
		{
			get
			{
				return this.m_TriplesCreated;
			}
			set
			{
				if (this.m_TriplesCreated == value)
				{
					return;
				}
				this.m_TriplesCreated = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600B9E2 RID: 47586 RVA: 0x0038E6B9 File Offset: 0x0038C8B9
		// (set) Token: 0x0600B9E1 RID: 47585 RVA: 0x0038E693 File Offset: 0x0038C893
		public int Top4Finishes
		{
			get
			{
				return this.m_Top4Finishes;
			}
			set
			{
				if (this.m_Top4Finishes == value)
				{
					return;
				}
				this.m_Top4Finishes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600B9E4 RID: 47588 RVA: 0x0038E6E7 File Offset: 0x0038C8E7
		// (set) Token: 0x0600B9E3 RID: 47587 RVA: 0x0038E6C1 File Offset: 0x0038C8C1
		public int MinionsDestroyed
		{
			get
			{
				return this.m_MinionsDestroyed;
			}
			set
			{
				if (this.m_MinionsDestroyed == value)
				{
					return;
				}
				this.m_MinionsDestroyed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600B9E6 RID: 47590 RVA: 0x0038E715 File Offset: 0x0038C915
		// (set) Token: 0x0600B9E5 RID: 47589 RVA: 0x0038E6EF File Offset: 0x0038C8EF
		public int FirstPlaceFinishes
		{
			get
			{
				return this.m_FirstPlaceFinishes;
			}
			set
			{
				if (this.m_FirstPlaceFinishes == value)
				{
					return;
				}
				this.m_FirstPlaceFinishes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600B9E8 RID: 47592 RVA: 0x0038E743 File Offset: 0x0038C943
		// (set) Token: 0x0600B9E7 RID: 47591 RVA: 0x0038E71D File Offset: 0x0038C91D
		public int TavernUpgrades
		{
			get
			{
				return this.m_TavernUpgrades;
			}
			set
			{
				if (this.m_TavernUpgrades == value)
				{
					return;
				}
				this.m_TavernUpgrades = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600B9EA RID: 47594 RVA: 0x0038E771 File Offset: 0x0038C971
		// (set) Token: 0x0600B9E9 RID: 47593 RVA: 0x0038E74B File Offset: 0x0038C94B
		public int PlayersEliminated
		{
			get
			{
				return this.m_PlayersEliminated;
			}
			set
			{
				if (this.m_PlayersEliminated == value)
				{
					return;
				}
				this.m_PlayersEliminated = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600B9EC RID: 47596 RVA: 0x0038E79F File Offset: 0x0038C99F
		// (set) Token: 0x0600B9EB RID: 47595 RVA: 0x0038E779 File Offset: 0x0038C979
		public int DamageInOneTurn
		{
			get
			{
				return this.m_DamageInOneTurn;
			}
			set
			{
				if (this.m_DamageInOneTurn == value)
				{
					return;
				}
				this.m_DamageInOneTurn = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600B9EE RID: 47598 RVA: 0x0038E7E0 File Offset: 0x0038C9E0
		// (set) Token: 0x0600B9ED RID: 47597 RVA: 0x0038E7A7 File Offset: 0x0038C9A7
		public CardDataModel BiggestMinionId
		{
			get
			{
				return this.m_BiggestMinionId;
			}
			set
			{
				if (this.m_BiggestMinionId == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_BiggestMinionId);
				base.RegisterNestedDataModel(value);
				this.m_BiggestMinionId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600B9F0 RID: 47600 RVA: 0x0038E80E File Offset: 0x0038CA0E
		// (set) Token: 0x0600B9EF RID: 47599 RVA: 0x0038E7E8 File Offset: 0x0038C9E8
		public int BiggestMinionAttack
		{
			get
			{
				return this.m_BiggestMinionAttack;
			}
			set
			{
				if (this.m_BiggestMinionAttack == value)
				{
					return;
				}
				this.m_BiggestMinionAttack = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600B9F2 RID: 47602 RVA: 0x0038E83C File Offset: 0x0038CA3C
		// (set) Token: 0x0600B9F1 RID: 47601 RVA: 0x0038E816 File Offset: 0x0038CA16
		public int BiggestMinionHealth
		{
			get
			{
				return this.m_BiggestMinionHealth;
			}
			set
			{
				if (this.m_BiggestMinionHealth == value)
				{
					return;
				}
				this.m_BiggestMinionHealth = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600B9F4 RID: 47604 RVA: 0x0038E86A File Offset: 0x0038CA6A
		// (set) Token: 0x0600B9F3 RID: 47603 RVA: 0x0038E844 File Offset: 0x0038CA44
		public int SecondsPlayed
		{
			get
			{
				return this.m_SecondsPlayed;
			}
			set
			{
				if (this.m_SecondsPlayed == value)
				{
					return;
				}
				this.m_SecondsPlayed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600B9F6 RID: 47606 RVA: 0x0038E898 File Offset: 0x0038CA98
		// (set) Token: 0x0600B9F5 RID: 47605 RVA: 0x0038E872 File Offset: 0x0038CA72
		public int LongestWinStreak
		{
			get
			{
				return this.m_LongestWinStreak;
			}
			set
			{
				if (this.m_LongestWinStreak == value)
				{
					return;
				}
				this.m_LongestWinStreak = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600B9F8 RID: 47608 RVA: 0x0038E8D9 File Offset: 0x0038CAD9
		// (set) Token: 0x0600B9F7 RID: 47607 RVA: 0x0038E8A0 File Offset: 0x0038CAA0
		public DataModelList<CardDataModel> MostBoughtMinionsCardIds
		{
			get
			{
				return this.m_MostBoughtMinionsCardIds;
			}
			set
			{
				if (this.m_MostBoughtMinionsCardIds == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_MostBoughtMinionsCardIds);
				base.RegisterNestedDataModel(value);
				this.m_MostBoughtMinionsCardIds = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600B9FA RID: 47610 RVA: 0x0038E91A File Offset: 0x0038CB1A
		// (set) Token: 0x0600B9F9 RID: 47609 RVA: 0x0038E8E1 File Offset: 0x0038CAE1
		public DataModelList<int> MostBoughtMinionsCount
		{
			get
			{
				return this.m_MostBoughtMinionsCount;
			}
			set
			{
				if (this.m_MostBoughtMinionsCount == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_MostBoughtMinionsCount);
				base.RegisterNestedDataModel(value);
				this.m_MostBoughtMinionsCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600B9FC RID: 47612 RVA: 0x0038E95B File Offset: 0x0038CB5B
		// (set) Token: 0x0600B9FB RID: 47611 RVA: 0x0038E922 File Offset: 0x0038CB22
		public DataModelList<CardDataModel> TopHeroesByWinCardIds
		{
			get
			{
				return this.m_TopHeroesByWinCardIds;
			}
			set
			{
				if (this.m_TopHeroesByWinCardIds == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_TopHeroesByWinCardIds);
				base.RegisterNestedDataModel(value);
				this.m_TopHeroesByWinCardIds = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600B9FE RID: 47614 RVA: 0x0038E99C File Offset: 0x0038CB9C
		// (set) Token: 0x0600B9FD RID: 47613 RVA: 0x0038E963 File Offset: 0x0038CB63
		public DataModelList<int> TopHeroesByWinCount
		{
			get
			{
				return this.m_TopHeroesByWinCount;
			}
			set
			{
				if (this.m_TopHeroesByWinCount == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_TopHeroesByWinCount);
				base.RegisterNestedDataModel(value);
				this.m_TopHeroesByWinCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600BA00 RID: 47616 RVA: 0x0038E9DD File Offset: 0x0038CBDD
		// (set) Token: 0x0600B9FF RID: 47615 RVA: 0x0038E9A4 File Offset: 0x0038CBA4
		public DataModelList<CardDataModel> TopHeroesByGamesPlayedCardIds
		{
			get
			{
				return this.m_TopHeroesByGamesPlayedCardIds;
			}
			set
			{
				if (this.m_TopHeroesByGamesPlayedCardIds == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_TopHeroesByGamesPlayedCardIds);
				base.RegisterNestedDataModel(value);
				this.m_TopHeroesByGamesPlayedCardIds = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600BA02 RID: 47618 RVA: 0x0038EA1E File Offset: 0x0038CC1E
		// (set) Token: 0x0600BA01 RID: 47617 RVA: 0x0038E9E5 File Offset: 0x0038CBE5
		public DataModelList<int> TopHeroesByGamesPlayedCount
		{
			get
			{
				return this.m_TopHeroesByGamesPlayedCount;
			}
			set
			{
				if (this.m_TopHeroesByGamesPlayedCount == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_TopHeroesByGamesPlayedCount);
				base.RegisterNestedDataModel(value);
				this.m_TopHeroesByGamesPlayedCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600BA04 RID: 47620 RVA: 0x0038EA51 File Offset: 0x0038CC51
		// (set) Token: 0x0600BA03 RID: 47619 RVA: 0x0038EA26 File Offset: 0x0038CC26
		public string TimePlayedString
		{
			get
			{
				return this.m_TimePlayedString;
			}
			set
			{
				if (this.m_TimePlayedString == value)
				{
					return;
				}
				this.m_TimePlayedString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600BA06 RID: 47622 RVA: 0x0038EA84 File Offset: 0x0038CC84
		// (set) Token: 0x0600BA05 RID: 47621 RVA: 0x0038EA59 File Offset: 0x0038CC59
		public string BiggestMinionString
		{
			get
			{
				return this.m_BiggestMinionString;
			}
			set
			{
				if (this.m_BiggestMinionString == value)
				{
					return;
				}
				this.m_BiggestMinionString = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600BA08 RID: 47624 RVA: 0x0038EAC5 File Offset: 0x0038CCC5
		// (set) Token: 0x0600BA07 RID: 47623 RVA: 0x0038EA8C File Offset: 0x0038CC8C
		public DataModelList<BaconPastGameStatsDataModel> PastGames
		{
			get
			{
				return this.m_PastGames;
			}
			set
			{
				if (this.m_PastGames == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_PastGames);
				base.RegisterNestedDataModel(value);
				this.m_PastGames = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600BA09 RID: 47625 RVA: 0x0038EACD File Offset: 0x0038CCCD
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA0A RID: 47626 RVA: 0x0038EAD8 File Offset: 0x0038CCD8
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int triplesCreated = this.m_TriplesCreated;
			int num2 = (num + this.m_TriplesCreated.GetHashCode()) * 31;
			int top4Finishes = this.m_Top4Finishes;
			int num3 = (num2 + this.m_Top4Finishes.GetHashCode()) * 31;
			int minionsDestroyed = this.m_MinionsDestroyed;
			int num4 = (num3 + this.m_MinionsDestroyed.GetHashCode()) * 31;
			int firstPlaceFinishes = this.m_FirstPlaceFinishes;
			int num5 = (num4 + this.m_FirstPlaceFinishes.GetHashCode()) * 31;
			int tavernUpgrades = this.m_TavernUpgrades;
			int num6 = (num5 + this.m_TavernUpgrades.GetHashCode()) * 31;
			int playersEliminated = this.m_PlayersEliminated;
			int num7 = (num6 + this.m_PlayersEliminated.GetHashCode()) * 31;
			int damageInOneTurn = this.m_DamageInOneTurn;
			int num8 = ((num7 + this.m_DamageInOneTurn.GetHashCode()) * 31 + ((this.m_BiggestMinionId != null) ? this.m_BiggestMinionId.GetPropertiesHashCode() : 0)) * 31;
			int biggestMinionAttack = this.m_BiggestMinionAttack;
			int num9 = (num8 + this.m_BiggestMinionAttack.GetHashCode()) * 31;
			int biggestMinionHealth = this.m_BiggestMinionHealth;
			int num10 = (num9 + this.m_BiggestMinionHealth.GetHashCode()) * 31;
			int secondsPlayed = this.m_SecondsPlayed;
			int num11 = (num10 + this.m_SecondsPlayed.GetHashCode()) * 31;
			int longestWinStreak = this.m_LongestWinStreak;
			return (((((((((num11 + this.m_LongestWinStreak.GetHashCode()) * 31 + ((this.m_MostBoughtMinionsCardIds != null) ? this.m_MostBoughtMinionsCardIds.GetPropertiesHashCode() : 0)) * 31 + ((this.m_MostBoughtMinionsCount != null) ? this.m_MostBoughtMinionsCount.GetPropertiesHashCode() : 0)) * 31 + ((this.m_TopHeroesByWinCardIds != null) ? this.m_TopHeroesByWinCardIds.GetPropertiesHashCode() : 0)) * 31 + ((this.m_TopHeroesByWinCount != null) ? this.m_TopHeroesByWinCount.GetPropertiesHashCode() : 0)) * 31 + ((this.m_TopHeroesByGamesPlayedCardIds != null) ? this.m_TopHeroesByGamesPlayedCardIds.GetPropertiesHashCode() : 0)) * 31 + ((this.m_TopHeroesByGamesPlayedCount != null) ? this.m_TopHeroesByGamesPlayedCount.GetPropertiesHashCode() : 0)) * 31 + ((this.m_TimePlayedString != null) ? this.m_TimePlayedString.GetHashCode() : 0)) * 31 + ((this.m_BiggestMinionString != null) ? this.m_BiggestMinionString.GetHashCode() : 0)) * 31 + ((this.m_PastGames != null) ? this.m_PastGames.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BA0B RID: 47627 RVA: 0x0038ECE0 File Offset: 0x0038CEE0
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_TriplesCreated;
				return true;
			case 1:
				value = this.m_Top4Finishes;
				return true;
			case 2:
				value = this.m_MinionsDestroyed;
				return true;
			case 3:
				value = this.m_FirstPlaceFinishes;
				return true;
			case 4:
				value = this.m_TavernUpgrades;
				return true;
			case 5:
				value = this.m_PlayersEliminated;
				return true;
			case 6:
				value = this.m_DamageInOneTurn;
				return true;
			case 7:
				value = this.m_BiggestMinionId;
				return true;
			case 8:
				value = this.m_BiggestMinionAttack;
				return true;
			case 9:
				value = this.m_BiggestMinionHealth;
				return true;
			case 10:
				value = this.m_SecondsPlayed;
				return true;
			case 11:
				value = this.m_LongestWinStreak;
				return true;
			case 12:
				value = this.m_MostBoughtMinionsCardIds;
				return true;
			case 13:
				value = this.m_MostBoughtMinionsCount;
				return true;
			case 14:
				value = this.m_TopHeroesByWinCardIds;
				return true;
			case 15:
				value = this.m_TopHeroesByWinCount;
				return true;
			case 16:
				value = this.m_TopHeroesByGamesPlayedCardIds;
				return true;
			case 17:
				value = this.m_TopHeroesByGamesPlayedCount;
				return true;
			case 18:
				value = this.m_TimePlayedString;
				return true;
			case 19:
				value = this.m_BiggestMinionString;
				return true;
			case 20:
				value = this.m_PastGames;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BA0C RID: 47628 RVA: 0x0038EE5C File Offset: 0x0038D05C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.TriplesCreated = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.Top4Finishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.MinionsDestroyed = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.FirstPlaceFinishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.TavernUpgrades = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.PlayersEliminated = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				this.DamageInOneTurn = ((value != null) ? ((int)value) : 0);
				return true;
			case 7:
				this.BiggestMinionId = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 8:
				this.BiggestMinionAttack = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				this.BiggestMinionHealth = ((value != null) ? ((int)value) : 0);
				return true;
			case 10:
				this.SecondsPlayed = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				this.LongestWinStreak = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				this.MostBoughtMinionsCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 13:
				this.MostBoughtMinionsCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 14:
				this.TopHeroesByWinCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 15:
				this.TopHeroesByWinCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 16:
				this.TopHeroesByGamesPlayedCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 17:
				this.TopHeroesByGamesPlayedCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 18:
				this.TimePlayedString = ((value != null) ? ((string)value) : null);
				return true;
			case 19:
				this.BiggestMinionString = ((value != null) ? ((string)value) : null);
				return true;
			case 20:
				this.PastGames = ((value != null) ? ((DataModelList<BaconPastGameStatsDataModel>)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BA0D RID: 47629 RVA: 0x0038F070 File Offset: 0x0038D270
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
			case 9:
				info = this.Properties[9];
				return true;
			case 10:
				info = this.Properties[10];
				return true;
			case 11:
				info = this.Properties[11];
				return true;
			case 12:
				info = this.Properties[12];
				return true;
			case 13:
				info = this.Properties[13];
				return true;
			case 14:
				info = this.Properties[14];
				return true;
			case 15:
				info = this.Properties[15];
				return true;
			case 16:
				info = this.Properties[16];
				return true;
			case 17:
				info = this.Properties[17];
				return true;
			case 18:
				info = this.Properties[18];
				return true;
			case 19:
				info = this.Properties[19];
				return true;
			case 20:
				info = this.Properties[20];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009912 RID: 39186
		public const int ModelId = 122;

		// Token: 0x04009913 RID: 39187
		private int m_TriplesCreated;

		// Token: 0x04009914 RID: 39188
		private int m_Top4Finishes;

		// Token: 0x04009915 RID: 39189
		private int m_MinionsDestroyed;

		// Token: 0x04009916 RID: 39190
		private int m_FirstPlaceFinishes;

		// Token: 0x04009917 RID: 39191
		private int m_TavernUpgrades;

		// Token: 0x04009918 RID: 39192
		private int m_PlayersEliminated;

		// Token: 0x04009919 RID: 39193
		private int m_DamageInOneTurn;

		// Token: 0x0400991A RID: 39194
		private CardDataModel m_BiggestMinionId;

		// Token: 0x0400991B RID: 39195
		private int m_BiggestMinionAttack;

		// Token: 0x0400991C RID: 39196
		private int m_BiggestMinionHealth;

		// Token: 0x0400991D RID: 39197
		private int m_SecondsPlayed;

		// Token: 0x0400991E RID: 39198
		private int m_LongestWinStreak;

		// Token: 0x0400991F RID: 39199
		private DataModelList<CardDataModel> m_MostBoughtMinionsCardIds = new DataModelList<CardDataModel>();

		// Token: 0x04009920 RID: 39200
		private DataModelList<int> m_MostBoughtMinionsCount = new DataModelList<int>();

		// Token: 0x04009921 RID: 39201
		private DataModelList<CardDataModel> m_TopHeroesByWinCardIds = new DataModelList<CardDataModel>();

		// Token: 0x04009922 RID: 39202
		private DataModelList<int> m_TopHeroesByWinCount = new DataModelList<int>();

		// Token: 0x04009923 RID: 39203
		private DataModelList<CardDataModel> m_TopHeroesByGamesPlayedCardIds = new DataModelList<CardDataModel>();

		// Token: 0x04009924 RID: 39204
		private DataModelList<int> m_TopHeroesByGamesPlayedCount = new DataModelList<int>();

		// Token: 0x04009925 RID: 39205
		private string m_TimePlayedString;

		// Token: 0x04009926 RID: 39206
		private string m_BiggestMinionString;

		// Token: 0x04009927 RID: 39207
		private DataModelList<BaconPastGameStatsDataModel> m_PastGames = new DataModelList<BaconPastGameStatsDataModel>();

		// Token: 0x04009928 RID: 39208
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "triples_created",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "top_4_finishes",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "minions_destroyed",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "first_place_finishes",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "tavern_upgrades",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "players_eliminated",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "damage_in_one_turn",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "biggest_minion_id",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "biggest_minion_attack",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "biggest_minion_health",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "seconds_played",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "longest_win_streak",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "most_bought_minions_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "most_bought_minions_count",
				Type = typeof(DataModelList<int>)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "top_heroes_by_win_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "top_heroes_by_win_count",
				Type = typeof(DataModelList<int>)
			},
			new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "top_heroes_by_games_played_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "top_heroes_by_games_played_count",
				Type = typeof(DataModelList<int>)
			},
			new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "time_played_string",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "biggest_minion_string",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "past_games",
				Type = typeof(DataModelList<BaconPastGameStatsDataModel>)
			}
		};
	}
}
