using System;
using UnityEngine;

// Token: 0x02000943 RID: 2371
public class DeviceAudioSettingsProviderEditor : MonoBehaviour, IDeviceAudioSettingsProvider
{
	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x06008304 RID: 33540 RVA: 0x002A7F6F File Offset: 0x002A616F
	public float Volume
	{
		get
		{
			return this.m_volume;
		}
	}

	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x06008305 RID: 33541 RVA: 0x002A7F77 File Offset: 0x002A6177
	public bool IsMuted
	{
		get
		{
			return this.m_isMuted;
		}
	}

	// Token: 0x06008306 RID: 33542 RVA: 0x002A7F7F File Offset: 0x002A617F
	private void Awake()
	{
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x04006DA9 RID: 28073
	[SerializeField]
	private float m_volume = 1f;

	// Token: 0x04006DAA RID: 28074
	[SerializeField]
	private bool m_isMuted;
}
