using System;
using UnityEngine;

// Token: 0x02000B37 RID: 2871
public class AdventureUITestScene : MonoBehaviour
{
	// Token: 0x06009872 RID: 39026 RVA: 0x00315BFC File Offset: 0x00313DFC
	private void Start()
	{
		PegUI.Get().AddInputCamera(Box.Get().m_Camera.GetComponent<Camera>());
	}

	// Token: 0x06009873 RID: 39027 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06009874 RID: 39028 RVA: 0x00315C17 File Offset: 0x00313E17
	private void OnDestroy()
	{
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(Box.Get().m_Camera.GetComponent<Camera>());
		}
	}
}
