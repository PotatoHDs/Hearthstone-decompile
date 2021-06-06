using System;

// Token: 0x02000944 RID: 2372
public class DeviceAudioSettingsProviderMock : IDeviceAudioSettingsProvider
{
	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x06008308 RID: 33544 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	public float Volume
	{
		get
		{
			return 1f;
		}
	}

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x06008309 RID: 33545 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsMuted
	{
		get
		{
			return false;
		}
	}
}
