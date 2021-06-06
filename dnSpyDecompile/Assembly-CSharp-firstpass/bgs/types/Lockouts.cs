using System;
using System.Runtime.InteropServices;

namespace bgs.types
{
	// Token: 0x0200027D RID: 637
	public struct Lockouts
	{
		// Token: 0x0400101A RID: 4122
		[MarshalAs(UnmanagedType.I1)]
		public bool loaded;

		// Token: 0x0400101B RID: 4123
		[MarshalAs(UnmanagedType.I1)]
		public bool loading;

		// Token: 0x0400101C RID: 4124
		[MarshalAs(UnmanagedType.I1)]
		public bool readingPCI;

		// Token: 0x0400101D RID: 4125
		[MarshalAs(UnmanagedType.I1)]
		public bool readingGTRI;

		// Token: 0x0400101E RID: 4126
		[MarshalAs(UnmanagedType.I1)]
		public bool readingCAISI;

		// Token: 0x0400101F RID: 4127
		[MarshalAs(UnmanagedType.I1)]
		public bool readingGSI;

		// Token: 0x04001020 RID: 4128
		[MarshalAs(UnmanagedType.I1)]
		public bool parentalControls;

		// Token: 0x04001021 RID: 4129
		[MarshalAs(UnmanagedType.I1)]
		public bool parentalTimedAccount;

		// Token: 0x04001022 RID: 4130
		public int parentalMinutesRemaining;

		// Token: 0x04001023 RID: 4131
		public IntPtr day1;

		// Token: 0x04001024 RID: 4132
		public IntPtr day2;

		// Token: 0x04001025 RID: 4133
		public IntPtr day3;

		// Token: 0x04001026 RID: 4134
		public IntPtr day4;

		// Token: 0x04001027 RID: 4135
		public IntPtr day5;

		// Token: 0x04001028 RID: 4136
		public IntPtr day6;

		// Token: 0x04001029 RID: 4137
		public IntPtr day7;

		// Token: 0x0400102A RID: 4138
		[MarshalAs(UnmanagedType.I1)]
		public bool timedAccount;

		// Token: 0x0400102B RID: 4139
		public int minutesRemaining;

		// Token: 0x0400102C RID: 4140
		public ulong sessionStartTime;

		// Token: 0x0400102D RID: 4141
		[MarshalAs(UnmanagedType.I1)]
		public bool CAISactive;

		// Token: 0x0400102E RID: 4142
		public int CAISplayed;

		// Token: 0x0400102F RID: 4143
		public int CAISrested;
	}
}
