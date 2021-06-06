using System.Runtime.InteropServices;

namespace agent
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ProgressDetails
	{
		public double m_progress;

		public long m_current;

		public long m_total;

		public int m_state;

		public int m_agentState;

		public int m_unitType;

		public int m_impediment;

		public int m_error;
	}
}
