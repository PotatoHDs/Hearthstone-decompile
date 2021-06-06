using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct RepairProgress
	{
		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_progressDetails;

		[MarshalAs(UnmanagedType.U1)]
		public bool m_updateRequired;
	}
}
