using System.Runtime.InteropServices;

namespace bgs.types
{
	public struct ChallengeInfo
	{
		public ulong challengeId;

		[MarshalAs(UnmanagedType.I1)]
		public bool isRetry;

		public int type;
	}
}
