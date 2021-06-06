using System;
using UnityEngine;

namespace Hearthstone.Telemetry
{
	internal abstract class BaseContextData
	{
		private const string m_programId = "WTCG";

		private const string m_programName = "Hearthstone";

		private const string m_productionHostUrl = "telemetry-in.battle.net";

		private const string m_qaHostUrl = "telemetry-in-dev.battle.net";

		private const string m_localHostUrl = "localhost";

		protected const string m_editorTag = "editor";

		private string m_programVersion;

		private ulong? m_battleNetId;

		private ulong? m_gameAccountId;

		private string m_countryCode;

		private int? m_battleNetRegion;

		private bool m_telemetryDisabled;

		private Uri m_ingestUri;

		private TelemetryConnectionType m_connectionType;

		public string ProgramId => "WTCG";

		public string ProgramName => "Hearthstone";

		public Uri IngestUri => m_ingestUri;

		public bool TelemetryDisabled => m_telemetryDisabled;

		public int? BattleNetRegion => m_battleNetRegion;

		public ulong? GameAccountId => m_gameAccountId;

		public abstract string ApplicationID { get; }

		public string ProgramVersion => m_programVersion;

		public ulong? BattleNetId => m_battleNetId;

		public TelemetryConnectionType ConnectionType => m_connectionType;

		public BaseContextData()
		{
			if (Vars.Key("Telemetry.Host").GetStr("PRODUCTION").Equals("PRODUCTION", StringComparison.InvariantCultureIgnoreCase))
			{
				m_connectionType = TelemetryConnectionType.Production;
			}
			else
			{
				m_connectionType = TelemetryConnectionType.Internal;
			}
			m_programVersion = $"{20}.{4}.{0}.{84593}";
			m_battleNetId = BnetUtils.TryGetBnetAccountId();
			m_battleNetRegion = (int?)BnetUtils.TryGetGameRegion();
			m_telemetryDisabled = Vars.Key("Telemetry.Enabled").GetBool(def: true);
			PopulateIngestUrl();
		}

		private void PopulateIngestUrl()
		{
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Scheme = "https";
			string str = Vars.Key("Telemetry.Host").GetStr("PRODUCTION");
			if (str.Equals("PRODUCTION", StringComparison.InvariantCultureIgnoreCase))
			{
				uriBuilder.Host = "telemetry-in.battle.net";
			}
			else
			{
				try
				{
					uriBuilder.Host = new Uri(str).Host;
				}
				catch (Exception ex)
				{
					Debug.LogErrorFormat("[Telemetry] Ingest host assignment had an error! (Host: '{0}')\nException: {1}", str, ex);
					uriBuilder.Host = "telemetry-in.battle.net";
				}
			}
			uriBuilder.Port = Vars.Key("Telemetry.Port").GetInt(443);
			m_ingestUri = uriBuilder.Uri;
		}
	}
}
