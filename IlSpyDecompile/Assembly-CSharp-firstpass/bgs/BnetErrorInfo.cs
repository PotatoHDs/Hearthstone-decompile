using System;

namespace bgs
{
	public class BnetErrorInfo
	{
		private BnetFeature m_feature;

		private BnetFeatureEvent m_featureEvent;

		private BattleNetErrors m_error;

		private int m_clientContext;

		private BnetErrorInfo()
		{
		}

		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error)
		{
			m_feature = feature;
			m_featureEvent = featureEvent;
			m_error = error;
			m_clientContext = 0;
		}

		public BnetErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error, int context)
		{
			m_feature = feature;
			m_featureEvent = featureEvent;
			m_error = error;
			m_clientContext = context;
		}

		public BnetFeature GetFeature()
		{
			return m_feature;
		}

		public void SetFeature(BnetFeature feature)
		{
			m_feature = feature;
		}

		public BnetFeatureEvent GetFeatureEvent()
		{
			return m_featureEvent;
		}

		public void SetFeatureEvent(BnetFeatureEvent featureEvent)
		{
			m_featureEvent = featureEvent;
		}

		public BattleNetErrors GetError()
		{
			return m_error;
		}

		public void SetError(BattleNetErrors error)
		{
			m_error = error;
		}

		public string GetName()
		{
			return m_error.ToString();
		}

		public int GetContext()
		{
			return m_clientContext;
		}

		public override string ToString()
		{
			if (Enum.IsDefined(typeof(BattleNetErrors), m_error))
			{
				return $"[event={m_featureEvent} error={(int)m_error} {m_error.ToString()}]";
			}
			return $"[event={m_featureEvent} code={(int)m_error} name={m_error.ToString()}]";
		}
	}
}
