namespace PegasusShared
{
	public enum DatabaseResult
	{
		DB_E_SQL_EX = -1,
		DB_E_UNKNOWN = 0,
		DB_E_SUCCESS = 1,
		DB_E_NOT_OWNED = 2,
		DB_E_CONSTRAINT = 3,
		DB_E_NOT_FOUND = 4,
		DB_E_EXCEPTION = 9,
		DB_E_BAD_PARAM = 11,
		DB_E_DECK_IS_LOCKED = 12,
		DB_E_DISABLED = 13
	}
}
