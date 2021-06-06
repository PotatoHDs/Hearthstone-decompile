using System;

namespace PegasusShared
{
	// Token: 0x02000177 RID: 375
	public enum DatabaseResult
	{
		// Token: 0x04000874 RID: 2164
		DB_E_SQL_EX = -1,
		// Token: 0x04000875 RID: 2165
		DB_E_UNKNOWN,
		// Token: 0x04000876 RID: 2166
		DB_E_SUCCESS,
		// Token: 0x04000877 RID: 2167
		DB_E_NOT_OWNED,
		// Token: 0x04000878 RID: 2168
		DB_E_CONSTRAINT,
		// Token: 0x04000879 RID: 2169
		DB_E_NOT_FOUND,
		// Token: 0x0400087A RID: 2170
		DB_E_EXCEPTION = 9,
		// Token: 0x0400087B RID: 2171
		DB_E_BAD_PARAM = 11,
		// Token: 0x0400087C RID: 2172
		DB_E_DECK_IS_LOCKED,
		// Token: 0x0400087D RID: 2173
		DB_E_DISABLED
	}
}
