using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class BaconStatsPageDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 122;

		private int m_TriplesCreated;

		private int m_Top4Finishes;

		private int m_MinionsDestroyed;

		private int m_FirstPlaceFinishes;

		private int m_TavernUpgrades;

		private int m_PlayersEliminated;

		private int m_DamageInOneTurn;

		private CardDataModel m_BiggestMinionId;

		private int m_BiggestMinionAttack;

		private int m_BiggestMinionHealth;

		private int m_SecondsPlayed;

		private int m_LongestWinStreak;

		private DataModelList<CardDataModel> m_MostBoughtMinionsCardIds = new DataModelList<CardDataModel>();

		private DataModelList<int> m_MostBoughtMinionsCount = new DataModelList<int>();

		private DataModelList<CardDataModel> m_TopHeroesByWinCardIds = new DataModelList<CardDataModel>();

		private DataModelList<int> m_TopHeroesByWinCount = new DataModelList<int>();

		private DataModelList<CardDataModel> m_TopHeroesByGamesPlayedCardIds = new DataModelList<CardDataModel>();

		private DataModelList<int> m_TopHeroesByGamesPlayedCount = new DataModelList<int>();

		private string m_TimePlayedString;

		private string m_BiggestMinionString;

		private DataModelList<BaconPastGameStatsDataModel> m_PastGames = new DataModelList<BaconPastGameStatsDataModel>();

		private DataModelProperty[] m_properties;

		public int DataModelId => 122;

		public string DataModelDisplayName => "baconstatspage";

		public int TriplesCreated
		{
			get
			{
				return m_TriplesCreated;
			}
			set
			{
				if (m_TriplesCreated != value)
				{
					m_TriplesCreated = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Top4Finishes
		{
			get
			{
				return m_Top4Finishes;
			}
			set
			{
				if (m_Top4Finishes != value)
				{
					m_Top4Finishes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MinionsDestroyed
		{
			get
			{
				return m_MinionsDestroyed;
			}
			set
			{
				if (m_MinionsDestroyed != value)
				{
					m_MinionsDestroyed = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int FirstPlaceFinishes
		{
			get
			{
				return m_FirstPlaceFinishes;
			}
			set
			{
				if (m_FirstPlaceFinishes != value)
				{
					m_FirstPlaceFinishes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TavernUpgrades
		{
			get
			{
				return m_TavernUpgrades;
			}
			set
			{
				if (m_TavernUpgrades != value)
				{
					m_TavernUpgrades = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PlayersEliminated
		{
			get
			{
				return m_PlayersEliminated;
			}
			set
			{
				if (m_PlayersEliminated != value)
				{
					m_PlayersEliminated = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int DamageInOneTurn
		{
			get
			{
				return m_DamageInOneTurn;
			}
			set
			{
				if (m_DamageInOneTurn != value)
				{
					m_DamageInOneTurn = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CardDataModel BiggestMinionId
		{
			get
			{
				return m_BiggestMinionId;
			}
			set
			{
				if (m_BiggestMinionId != value)
				{
					RemoveNestedDataModel(m_BiggestMinionId);
					RegisterNestedDataModel(value);
					m_BiggestMinionId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int BiggestMinionAttack
		{
			get
			{
				return m_BiggestMinionAttack;
			}
			set
			{
				if (m_BiggestMinionAttack != value)
				{
					m_BiggestMinionAttack = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int BiggestMinionHealth
		{
			get
			{
				return m_BiggestMinionHealth;
			}
			set
			{
				if (m_BiggestMinionHealth != value)
				{
					m_BiggestMinionHealth = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int SecondsPlayed
		{
			get
			{
				return m_SecondsPlayed;
			}
			set
			{
				if (m_SecondsPlayed != value)
				{
					m_SecondsPlayed = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LongestWinStreak
		{
			get
			{
				return m_LongestWinStreak;
			}
			set
			{
				if (m_LongestWinStreak != value)
				{
					m_LongestWinStreak = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<CardDataModel> MostBoughtMinionsCardIds
		{
			get
			{
				return m_MostBoughtMinionsCardIds;
			}
			set
			{
				if (m_MostBoughtMinionsCardIds != value)
				{
					RemoveNestedDataModel(m_MostBoughtMinionsCardIds);
					RegisterNestedDataModel(value);
					m_MostBoughtMinionsCardIds = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<int> MostBoughtMinionsCount
		{
			get
			{
				return m_MostBoughtMinionsCount;
			}
			set
			{
				if (m_MostBoughtMinionsCount != value)
				{
					RemoveNestedDataModel(m_MostBoughtMinionsCount);
					RegisterNestedDataModel(value);
					m_MostBoughtMinionsCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<CardDataModel> TopHeroesByWinCardIds
		{
			get
			{
				return m_TopHeroesByWinCardIds;
			}
			set
			{
				if (m_TopHeroesByWinCardIds != value)
				{
					RemoveNestedDataModel(m_TopHeroesByWinCardIds);
					RegisterNestedDataModel(value);
					m_TopHeroesByWinCardIds = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<int> TopHeroesByWinCount
		{
			get
			{
				return m_TopHeroesByWinCount;
			}
			set
			{
				if (m_TopHeroesByWinCount != value)
				{
					RemoveNestedDataModel(m_TopHeroesByWinCount);
					RegisterNestedDataModel(value);
					m_TopHeroesByWinCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<CardDataModel> TopHeroesByGamesPlayedCardIds
		{
			get
			{
				return m_TopHeroesByGamesPlayedCardIds;
			}
			set
			{
				if (m_TopHeroesByGamesPlayedCardIds != value)
				{
					RemoveNestedDataModel(m_TopHeroesByGamesPlayedCardIds);
					RegisterNestedDataModel(value);
					m_TopHeroesByGamesPlayedCardIds = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<int> TopHeroesByGamesPlayedCount
		{
			get
			{
				return m_TopHeroesByGamesPlayedCount;
			}
			set
			{
				if (m_TopHeroesByGamesPlayedCount != value)
				{
					RemoveNestedDataModel(m_TopHeroesByGamesPlayedCount);
					RegisterNestedDataModel(value);
					m_TopHeroesByGamesPlayedCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TimePlayedString
		{
			get
			{
				return m_TimePlayedString;
			}
			set
			{
				if (!(m_TimePlayedString == value))
				{
					m_TimePlayedString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string BiggestMinionString
		{
			get
			{
				return m_BiggestMinionString;
			}
			set
			{
				if (!(m_BiggestMinionString == value))
				{
					m_BiggestMinionString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<BaconPastGameStatsDataModel> PastGames
		{
			get
			{
				return m_PastGames;
			}
			set
			{
				if (m_PastGames != value)
				{
					RemoveNestedDataModel(m_PastGames);
					RegisterNestedDataModel(value);
					m_PastGames = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public BaconStatsPageDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[21];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "triples_created",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "top_4_finishes",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "minions_destroyed",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "first_place_finishes",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "tavern_upgrades",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "players_eliminated",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "damage_in_one_turn",
				Type = typeof(int)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "biggest_minion_id",
				Type = typeof(CardDataModel)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "biggest_minion_attack",
				Type = typeof(int)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "biggest_minion_health",
				Type = typeof(int)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "seconds_played",
				Type = typeof(int)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "longest_win_streak",
				Type = typeof(int)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "most_bought_minions_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "most_bought_minions_count",
				Type = typeof(DataModelList<int>)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "top_heroes_by_win_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			};
			array[14] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "top_heroes_by_win_count",
				Type = typeof(DataModelList<int>)
			};
			array[15] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "top_heroes_by_games_played_card_ids",
				Type = typeof(DataModelList<CardDataModel>)
			};
			array[16] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "top_heroes_by_games_played_count",
				Type = typeof(DataModelList<int>)
			};
			array[17] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "time_played_string",
				Type = typeof(string)
			};
			array[18] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "biggest_minion_string",
				Type = typeof(string)
			};
			array[19] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "past_games",
				Type = typeof(DataModelList<BaconPastGameStatsDataModel>)
			};
			array[20] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_BiggestMinionId);
			RegisterNestedDataModel(m_MostBoughtMinionsCardIds);
			RegisterNestedDataModel(m_MostBoughtMinionsCount);
			RegisterNestedDataModel(m_TopHeroesByWinCardIds);
			RegisterNestedDataModel(m_TopHeroesByWinCount);
			RegisterNestedDataModel(m_TopHeroesByGamesPlayedCardIds);
			RegisterNestedDataModel(m_TopHeroesByGamesPlayedCount);
			RegisterNestedDataModel(m_PastGames);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_TriplesCreated;
			int num2 = (num + m_TriplesCreated.GetHashCode()) * 31;
			_ = m_Top4Finishes;
			int num3 = (num2 + m_Top4Finishes.GetHashCode()) * 31;
			_ = m_MinionsDestroyed;
			int num4 = (num3 + m_MinionsDestroyed.GetHashCode()) * 31;
			_ = m_FirstPlaceFinishes;
			int num5 = (num4 + m_FirstPlaceFinishes.GetHashCode()) * 31;
			_ = m_TavernUpgrades;
			int num6 = (num5 + m_TavernUpgrades.GetHashCode()) * 31;
			_ = m_PlayersEliminated;
			int num7 = (num6 + m_PlayersEliminated.GetHashCode()) * 31;
			_ = m_DamageInOneTurn;
			int num8 = ((num7 + m_DamageInOneTurn.GetHashCode()) * 31 + ((m_BiggestMinionId != null) ? m_BiggestMinionId.GetPropertiesHashCode() : 0)) * 31;
			_ = m_BiggestMinionAttack;
			int num9 = (num8 + m_BiggestMinionAttack.GetHashCode()) * 31;
			_ = m_BiggestMinionHealth;
			int num10 = (num9 + m_BiggestMinionHealth.GetHashCode()) * 31;
			_ = m_SecondsPlayed;
			int num11 = (num10 + m_SecondsPlayed.GetHashCode()) * 31;
			_ = m_LongestWinStreak;
			return (((((((((num11 + m_LongestWinStreak.GetHashCode()) * 31 + ((m_MostBoughtMinionsCardIds != null) ? m_MostBoughtMinionsCardIds.GetPropertiesHashCode() : 0)) * 31 + ((m_MostBoughtMinionsCount != null) ? m_MostBoughtMinionsCount.GetPropertiesHashCode() : 0)) * 31 + ((m_TopHeroesByWinCardIds != null) ? m_TopHeroesByWinCardIds.GetPropertiesHashCode() : 0)) * 31 + ((m_TopHeroesByWinCount != null) ? m_TopHeroesByWinCount.GetPropertiesHashCode() : 0)) * 31 + ((m_TopHeroesByGamesPlayedCardIds != null) ? m_TopHeroesByGamesPlayedCardIds.GetPropertiesHashCode() : 0)) * 31 + ((m_TopHeroesByGamesPlayedCount != null) ? m_TopHeroesByGamesPlayedCount.GetPropertiesHashCode() : 0)) * 31 + ((m_TimePlayedString != null) ? m_TimePlayedString.GetHashCode() : 0)) * 31 + ((m_BiggestMinionString != null) ? m_BiggestMinionString.GetHashCode() : 0)) * 31 + ((m_PastGames != null) ? m_PastGames.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_TriplesCreated;
				return true;
			case 1:
				value = m_Top4Finishes;
				return true;
			case 2:
				value = m_MinionsDestroyed;
				return true;
			case 3:
				value = m_FirstPlaceFinishes;
				return true;
			case 4:
				value = m_TavernUpgrades;
				return true;
			case 5:
				value = m_PlayersEliminated;
				return true;
			case 6:
				value = m_DamageInOneTurn;
				return true;
			case 7:
				value = m_BiggestMinionId;
				return true;
			case 8:
				value = m_BiggestMinionAttack;
				return true;
			case 9:
				value = m_BiggestMinionHealth;
				return true;
			case 10:
				value = m_SecondsPlayed;
				return true;
			case 11:
				value = m_LongestWinStreak;
				return true;
			case 12:
				value = m_MostBoughtMinionsCardIds;
				return true;
			case 13:
				value = m_MostBoughtMinionsCount;
				return true;
			case 14:
				value = m_TopHeroesByWinCardIds;
				return true;
			case 15:
				value = m_TopHeroesByWinCount;
				return true;
			case 16:
				value = m_TopHeroesByGamesPlayedCardIds;
				return true;
			case 17:
				value = m_TopHeroesByGamesPlayedCount;
				return true;
			case 18:
				value = m_TimePlayedString;
				return true;
			case 19:
				value = m_BiggestMinionString;
				return true;
			case 20:
				value = m_PastGames;
				return true;
			default:
				value = null;
				return false;
			}
		}

		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				TriplesCreated = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				Top4Finishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				MinionsDestroyed = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				FirstPlaceFinishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				TavernUpgrades = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				PlayersEliminated = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				DamageInOneTurn = ((value != null) ? ((int)value) : 0);
				return true;
			case 7:
				BiggestMinionId = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 8:
				BiggestMinionAttack = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				BiggestMinionHealth = ((value != null) ? ((int)value) : 0);
				return true;
			case 10:
				SecondsPlayed = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				LongestWinStreak = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				MostBoughtMinionsCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 13:
				MostBoughtMinionsCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 14:
				TopHeroesByWinCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 15:
				TopHeroesByWinCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 16:
				TopHeroesByGamesPlayedCardIds = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 17:
				TopHeroesByGamesPlayedCount = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 18:
				TimePlayedString = ((value != null) ? ((string)value) : null);
				return true;
			case 19:
				BiggestMinionString = ((value != null) ? ((string)value) : null);
				return true;
			case 20:
				PastGames = ((value != null) ? ((DataModelList<BaconPastGameStatsDataModel>)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			case 4:
				info = Properties[4];
				return true;
			case 5:
				info = Properties[5];
				return true;
			case 6:
				info = Properties[6];
				return true;
			case 7:
				info = Properties[7];
				return true;
			case 8:
				info = Properties[8];
				return true;
			case 9:
				info = Properties[9];
				return true;
			case 10:
				info = Properties[10];
				return true;
			case 11:
				info = Properties[11];
				return true;
			case 12:
				info = Properties[12];
				return true;
			case 13:
				info = Properties[13];
				return true;
			case 14:
				info = Properties[14];
				return true;
			case 15:
				info = Properties[15];
				return true;
			case 16:
				info = Properties[16];
				return true;
			case 17:
				info = Properties[17];
				return true;
			case 18:
				info = Properties[18];
				return true;
			case 19:
				info = Properties[19];
				return true;
			case 20:
				info = Properties[20];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
