namespace PegasusShared
{
	public enum ErrorCode
	{
		ERROR_OK = 0,
		ERROR_HEARTHSTONE_BEGIN = 1000000,
		ERROR_GLOBAL_INVALID_INPUT = 1000001,
		ERROR_GLOBAL_NO_DATA = 1000002,
		ERROR_GLOBAL_NOT_YET_IMPLEMENTED = 1000003,
		ERROR_GLOBAL_DATA_MODIFIED = 1000004,
		ERROR_GLOBAL_INTERNAL_DB_ERROR = 1000005,
		ERROR_GLOBAL_DECK_IS_LOCKED = 1000006,
		ERROR_GLOBAL_FEATURE_DISABLED = 1000007,
		ERROR_GLOBAL_HTTP_ERROR = 1000008,
		ERROR_GLOBAL_JSON_PARSE_ERROR = 1000009,
		ERROR_GLOBAL_INTERNAL_ERROR = 1000010,
		ERROR_GLOBAL_FSG_ID_INVALID = 1000011,
		ERROR_GLOBAL_HTTP_TIMEOUT_OR_ABORTED = 1000012,
		ERROR_GLOBAL_INVALID_HERO_SPECIFIED = 1000013,
		ERROR_GLOBAL_PARSE_ERROR_EXPECTED_NUMERIC_STRING = 1000014,
		ERROR_GLOBAL_BGS_FAILED = 1000015,
		ERROR_GLOBAL_DATABASE_FAILED = 1000016,
		ERROR_GLOBAL_UNKNOWN_PLAYER = 1000017,
		ERROR_SCENARIO_INCORRECT_NUM_PLAYERS = 1000500,
		ERROR_SCENARIO_NO_DECK_SPECIFIED = 1000501,
		ERROR_SCENARIO_MUST_BE_SERVER_ONLY = 1000502,
		ERROR_TAVERN_BRAWL_SEASON_INCREMENTED = 1001000,
		ERROR_TAVERN_BRAWL_NOT_ACTIVE = 1001001,
		ERROR_TAVERN_BRAWL_NO_TICKET = 1001002,
		ERROR_TAVERN_BRAWL_RETIRE_FIRST = 1001003,
		ERROR_TAVERN_BRAWL_NOT_IN_SESSION = 1001004,
		ERROR_TAVERN_BRAWL_NOT_IN_SESSION_BUT_COULD_BE = 1001005,
		ERROR_TAVERN_BRAWL_FEATURE_DISABLED = 1001006,
		ERROR_TAVERN_BRAWL_TICKET_NOT_REQUIRED = 1001007,
		ERROR_TAVERN_BRAWL_FRIENDLY_CHALLENGE_DISABLED = 1001008,
		ERROR_DECK_RULESET_RULE_UNKNOWN_TYPE = 1002000,
		ERROR_DECK_RULESET_RULE_DB_READ_ERROR = 1002001,
		ERROR_DECK_RULESET_RULE_VIOLATION = 1002002,
		ERROR_DECK_RULESET_DECK_CARD_ID_UNKNOWN = 1002003,
		ERROR_DECK_RULESET_HERO_CARD_GUID_UNKNOWN = 1002004,
		ERROR_DECK_RULESET_DECK_CARD_GUID_UNKNOWN = 1002005,
		ERROR_DECK_VALIDATION_DB_WRITE_ERROR = 1002006,
		ERROR_DECK_VALIDATION_WRONG_FORMAT = 1002007,
		ERROR_DECK_RULESET_MUST_BE_SERVER_ONLY = 1002008,
		ERROR_DECK_VALIDATION_LEAGUE_BANNED_CARD = 1002009,
		ERROR_DECK_RULESET_HERO_CARD_NOT_PLAYABLE = 1002010,
		ERROR_PLAY_GAME_DECK_MODIFIED = 1003000,
		ERROR_PLAY_GAME_FSG_ID_NOT_MATCHING = 1003002,
		ERROR_PLAY_GAME_FSG_PATRON_NOT_CHECKED_IN = 1003003,
		ERROR_PLAY_GAME_FSG_NOT_CURRENTLY_PLAYABLE = 1003004,
		ERROR_PLAY_GAME_SEASON_INCREMENTED = 1003005,
		ERROR_PLAY_GAME_INCORRECT_NUM_PLAYERS = 1003006,
		ERROR_PLAY_GAME_INVALID_ATTRIBUTE = 1003007,
		ERROR_PLAY_GAME_MISSING_ATTRIBUTE = 1003008,
		ERROR_PLAY_GAME_INVALID_SCENARIO = 1003009,
		ERROR_PLAY_GAME_INVALID_DECK = 1003010,
		ERROR_PLAY_GAME_LEVEL_REQUIREMENT = 1003011,
		ERROR_PLAY_GAME_INVALID_DECK_SHARING = 1003012,
		ERROR_PLAY_GAME_CURRENTLY_IN_GAME = 1003013,
		ERROR_PLAY_GAME_MISSING_LICENSE = 1003014,
		ERROR_PLAY_GAME_PARTY_NOT_ALLOWED = 1003015,
		ERROR_FSG_NO_PERMISSION = 1004000,
		ERROR_FSG_DUPLICATE_REQUEST_IGNORED = 1004001,
		ERROR_FSG_DUPLICATE_REQUEST_ORIGINAL_DROPPED = 1004002,
		ERROR_FSG_ALREADY_CHECKED_IN_FETCH_FSG_INFO = 1004003,
		ERROR_FSG_BSSIDS_NOT_VALID = 1004004,
		ERROR_GAME_SAVE_DATA_INVALID_KEY_REQUESTED = 1005000,
		ERROR_SET_GAME_SAVE_DATA_INVALID_DATA = 1005001,
		ERROR_SET_GAME_SAVE_DATA_INVALID_SUBKEY = 1005002,
		ERROR_SAVE_PROGRESS_RESTRICTED_NO_KEYS_UPDATED = 1005003,
		ERROR_DUELS_NO_ACTIVE_SESSION = 1006000,
		ERROR_DUELS_SESSION_MISMATCH_REQUESTED_GAME = 1006001
	}
}
