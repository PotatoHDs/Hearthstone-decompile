using System;
using UnityEngine;

// Token: 0x02000864 RID: 2148
public class BoardCameras : MonoBehaviour
{
	// Token: 0x060073FD RID: 29693 RVA: 0x002537D4 File Offset: 0x002519D4
	private void Awake()
	{
		BoardCameras.s_instance = this;
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	// Token: 0x060073FE RID: 29694 RVA: 0x002537F9 File Offset: 0x002519F9
	private void OnDestroy()
	{
		BoardCameras.s_instance = null;
	}

	// Token: 0x060073FF RID: 29695 RVA: 0x00253801 File Offset: 0x00251A01
	public static BoardCameras Get()
	{
		return BoardCameras.s_instance;
	}

	// Token: 0x06007400 RID: 29696 RVA: 0x00253808 File Offset: 0x00251A08
	public AudioListener GetAudioListener()
	{
		return this.m_AudioListener;
	}

	// Token: 0x04005C27 RID: 23591
	public AudioListener m_AudioListener;

	// Token: 0x04005C28 RID: 23592
	private static BoardCameras s_instance;
}
