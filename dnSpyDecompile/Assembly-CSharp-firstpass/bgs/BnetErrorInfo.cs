using System;

namespace bgs
{
	// Token: 0x0200023C RID: 572
	public class BnetErrorInfo
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x00002654 File Offset: 0x00000854
		private BnetErrorInfo()
		{
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0007ECDB File Offset: 0x0007CEDB
		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error)
		{
			this.m_feature = feature;
			this.m_featureEvent = featureEvent;
			this.m_error = error;
			this.m_clientContext = 0;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0007ECFF File Offset: 0x0007CEFF
		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error, int context)
		{
			this.m_feature = feature;
			this.m_featureEvent = featureEvent;
			this.m_error = error;
			this.m_clientContext = context;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0007ED24 File Offset: 0x0007CF24
		public BnetFeature GetFeature()
		{
			return this.m_feature;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0007ED2C File Offset: 0x0007CF2C
		public void SetFeature(BnetFeature feature)
		{
			this.m_feature = feature;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0007ED35 File Offset: 0x0007CF35
		public BnetFeatureEvent GetFeatureEvent()
		{
			return this.m_featureEvent;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0007ED3D File Offset: 0x0007CF3D
		public void SetFeatureEvent(BnetFeatureEvent featureEvent)
		{
			this.m_featureEvent = featureEvent;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x0007ED46 File Offset: 0x0007CF46
		public BattleNetErrors GetError()
		{
			return this.m_error;
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0007ED4E File Offset: 0x0007CF4E
		public void SetError(BattleNetErrors error)
		{
			this.m_error = error;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0007ED57 File Offset: 0x0007CF57
		public string GetName()
		{
			return this.m_error.ToString();
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0007ED6A File Offset: 0x0007CF6A
		public int GetContext()
		{
			return this.m_clientContext;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0007ED74 File Offset: 0x0007CF74
		public override string ToString()
		{
			string result;
			if (Enum.IsDefined(typeof(BattleNetErrors), this.m_error))
			{
				result = string.Format("[event={0} error={1} {2}]", this.m_featureEvent, (int)this.m_error, this.m_error.ToString());
			}
			else
			{
				result = string.Format("[event={0} code={1} name={2}]", this.m_featureEvent, (int)this.m_error, this.m_error.ToString());
			}
			return result;
		}

		// Token: 0x04000EAB RID: 3755
		private BnetFeature m_feature;

		// Token: 0x04000EAC RID: 3756
		private BnetFeatureEvent m_featureEvent;

		// Token: 0x04000EAD RID: 3757
		private BattleNetErrors m_error;

		// Token: 0x04000EAE RID: 3758
		private int m_clientContext;
	}
}
