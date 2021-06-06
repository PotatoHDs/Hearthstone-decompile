using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class DesktopChatLogUI : IChatLogUI
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600079A RID: 1946 RVA: 0x0002B3B5 File Offset: 0x000295B5
	public bool IsShowing
	{
		get
		{
			return this.m_quickChatFrame != null;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x0600079B RID: 1947 RVA: 0x0002B3C3 File Offset: 0x000295C3
	public GameObject GameObject
	{
		get
		{
			if (!(this.m_quickChatFrame == null))
			{
				return this.m_quickChatFrame.gameObject;
			}
			return null;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x0600079C RID: 1948 RVA: 0x0002B3E0 File Offset: 0x000295E0
	public BnetPlayer Receiver
	{
		get
		{
			if (!(this.m_quickChatFrame == null))
			{
				return this.m_quickChatFrame.GetReceiver();
			}
			return null;
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0002B400 File Offset: 0x00029600
	public void ShowForPlayer(BnetPlayer player)
	{
		if (this.m_quickChatFrame != null)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("QuickChatFrame.prefab:a8bbab56b6588e44a8f0d25fc30ae886", AssetLoadingOptions.None);
		if (gameObject != null)
		{
			this.m_quickChatFrame = gameObject.GetComponent<QuickChatFrame>();
			this.m_quickChatFrame.SetReceiver(player);
		}
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0002B453 File Offset: 0x00029653
	public void Hide()
	{
		if (this.m_quickChatFrame == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_quickChatFrame.gameObject);
		this.m_quickChatFrame = null;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void GoBack()
	{
	}

	// Token: 0x04000528 RID: 1320
	private QuickChatFrame m_quickChatFrame;
}
