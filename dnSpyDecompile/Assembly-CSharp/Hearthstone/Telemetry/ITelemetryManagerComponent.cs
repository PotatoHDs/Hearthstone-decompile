using System;

namespace Hearthstone.Telemetry
{
	// Token: 0x0200106B RID: 4203
	public interface ITelemetryManagerComponent
	{
		// Token: 0x0600B56C RID: 46444
		void Initialize();

		// Token: 0x0600B56D RID: 46445
		void Update();

		// Token: 0x0600B56E RID: 46446
		void Shutdown();
	}
}
