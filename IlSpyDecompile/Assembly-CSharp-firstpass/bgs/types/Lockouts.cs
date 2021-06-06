using System;
using System.Runtime.InteropServices;

namespace bgs.types
{
	public struct Lockouts
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool loaded;

		[MarshalAs(UnmanagedType.I1)]
		public bool loading;

		[MarshalAs(UnmanagedType.I1)]
		public bool readingPCI;

		[MarshalAs(UnmanagedType.I1)]
		public bool readingGTRI;

		[MarshalAs(UnmanagedType.I1)]
		public bool readingCAISI;

		[MarshalAs(UnmanagedType.I1)]
		public bool readingGSI;

		[MarshalAs(UnmanagedType.I1)]
		public bool parentalControls;

		[MarshalAs(UnmanagedType.I1)]
		public bool parentalTimedAccount;

		public int parentalMinutesRemaining;

		public IntPtr day1;

		public IntPtr day2;

		public IntPtr day3;

		public IntPtr day4;

		public IntPtr day5;

		public IntPtr day6;

		public IntPtr day7;

		[MarshalAs(UnmanagedType.I1)]
		public bool timedAccount;

		public int minutesRemaining;

		public ulong sessionStartTime;

		[MarshalAs(UnmanagedType.I1)]
		public bool CAISactive;

		public int CAISplayed;

		public int CAISrested;
	}
}
