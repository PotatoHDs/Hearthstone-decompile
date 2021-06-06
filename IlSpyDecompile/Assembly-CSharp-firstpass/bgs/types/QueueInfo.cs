using System.Runtime.InteropServices;

namespace bgs.types
{
	public struct QueueInfo
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool changed;

		public int position;

		public long end;

		public long stdev;
	}
}
