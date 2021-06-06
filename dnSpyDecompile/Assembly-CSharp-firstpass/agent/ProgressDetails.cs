using System;
using System.Runtime.InteropServices;

namespace agent
{
	// Token: 0x020001EA RID: 490
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ProgressDetails
	{
		// Token: 0x04000AFF RID: 2815
		public double m_progress;

		// Token: 0x04000B00 RID: 2816
		public long m_current;

		// Token: 0x04000B01 RID: 2817
		public long m_total;

		// Token: 0x04000B02 RID: 2818
		public int m_state;

		// Token: 0x04000B03 RID: 2819
		public int m_agentState;

		// Token: 0x04000B04 RID: 2820
		public int m_unitType;

		// Token: 0x04000B05 RID: 2821
		public int m_impediment;

		// Token: 0x04000B06 RID: 2822
		public int m_error;
	}
}
