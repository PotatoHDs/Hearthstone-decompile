using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CachedState
	{
		[MarshalAs(UnmanagedType.Struct)]
		public BaseState m_baseState;

		[MarshalAs(UnmanagedType.Struct)]
		public UpdateProgress m_updateProgress;

		[MarshalAs(UnmanagedType.Struct)]
		public RepairProgress m_repairProgress;

		[MarshalAs(UnmanagedType.Struct)]
		public BackfillProgress m_backfillProgress;

		[MarshalAs(UnmanagedType.Struct)]
		public ProgressDetails m_versionProgress;
	}
}
