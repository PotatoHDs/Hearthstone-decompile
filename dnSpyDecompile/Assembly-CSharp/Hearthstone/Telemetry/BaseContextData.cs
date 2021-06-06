using System;
using bgs;
using UnityEngine;

namespace Hearthstone.Telemetry
{
	// Token: 0x02001066 RID: 4198
	internal abstract class BaseContextData
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600B55A RID: 46426 RVA: 0x0037C10D File Offset: 0x0037A30D
		public string ProgramId
		{
			get
			{
				return "WTCG";
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600B55B RID: 46427 RVA: 0x0037C114 File Offset: 0x0037A314
		public string ProgramName
		{
			get
			{
				return "Hearthstone";
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600B55C RID: 46428 RVA: 0x0037C11B File Offset: 0x0037A31B
		public Uri IngestUri
		{
			get
			{
				return this.m_ingestUri;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600B55D RID: 46429 RVA: 0x0037C123 File Offset: 0x0037A323
		public bool TelemetryDisabled
		{
			get
			{
				return this.m_telemetryDisabled;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600B55E RID: 46430 RVA: 0x0037C12B File Offset: 0x0037A32B
		public int? BattleNetRegion
		{
			get
			{
				return this.m_battleNetRegion;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600B55F RID: 46431 RVA: 0x0037C133 File Offset: 0x0037A333
		public ulong? GameAccountId
		{
			get
			{
				return this.m_gameAccountId;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600B560 RID: 46432
		public abstract string ApplicationID { get; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600B561 RID: 46433 RVA: 0x0037C13B File Offset: 0x0037A33B
		public string ProgramVersion
		{
			get
			{
				return this.m_programVersion;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600B562 RID: 46434 RVA: 0x0037C143 File Offset: 0x0037A343
		public ulong? BattleNetId
		{
			get
			{
				return this.m_battleNetId;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x0600B563 RID: 46435 RVA: 0x0037C14B File Offset: 0x0037A34B
		public TelemetryConnectionType ConnectionType
		{
			get
			{
				return this.m_connectionType;
			}
		}

		// Token: 0x0600B564 RID: 46436 RVA: 0x0037C154 File Offset: 0x0037A354
		public BaseContextData()
		{
			if (Vars.Key("Telemetry.Host").GetStr("PRODUCTION").Equals("PRODUCTION", StringComparison.InvariantCultureIgnoreCase))
			{
				this.m_connectionType = TelemetryConnectionType.Production;
			}
			else
			{
				this.m_connectionType = TelemetryConnectionType.Internal;
			}
			this.m_programVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				20,
				4,
				0,
				84593
			});
			this.m_battleNetId = BnetUtils.TryGetBnetAccountId();
			constants.BnetRegion? bnetRegion = BnetUtils.TryGetGameRegion();
			this.m_battleNetRegion = ((bnetRegion != null) ? new int?((int)bnetRegion.GetValueOrDefault()) : null);
			this.m_telemetryDisabled = Vars.Key("Telemetry.Enabled").GetBool(true);
			this.PopulateIngestUrl();
		}

		// Token: 0x0600B565 RID: 46437 RVA: 0x0037C22C File Offset: 0x0037A42C
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
					Debug.LogErrorFormat("[Telemetry] Ingest host assignment had an error! (Host: '{0}')\nException: {1}", new object[]
					{
						str,
						ex
					});
					uriBuilder.Host = "telemetry-in.battle.net";
				}
			}
			uriBuilder.Port = Vars.Key("Telemetry.Port").GetInt(443);
			this.m_ingestUri = uriBuilder.Uri;
		}

		// Token: 0x0400973F RID: 38719
		private const string m_programId = "WTCG";

		// Token: 0x04009740 RID: 38720
		private const string m_programName = "Hearthstone";

		// Token: 0x04009741 RID: 38721
		private const string m_productionHostUrl = "telemetry-in.battle.net";

		// Token: 0x04009742 RID: 38722
		private const string m_qaHostUrl = "telemetry-in-dev.battle.net";

		// Token: 0x04009743 RID: 38723
		private const string m_localHostUrl = "localhost";

		// Token: 0x04009744 RID: 38724
		protected const string m_editorTag = "editor";

		// Token: 0x04009745 RID: 38725
		private string m_programVersion;

		// Token: 0x04009746 RID: 38726
		private ulong? m_battleNetId;

		// Token: 0x04009747 RID: 38727
		private ulong? m_gameAccountId;

		// Token: 0x04009748 RID: 38728
		private string m_countryCode;

		// Token: 0x04009749 RID: 38729
		private int? m_battleNetRegion;

		// Token: 0x0400974A RID: 38730
		private bool m_telemetryDisabled;

		// Token: 0x0400974B RID: 38731
		private Uri m_ingestUri;

		// Token: 0x0400974C RID: 38732
		private TelemetryConnectionType m_connectionType;
	}
}
