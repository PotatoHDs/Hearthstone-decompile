using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class MobileChatLogUI : IChatLogUI
{
	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00038326 File Offset: 0x00036526
	public bool IsShowing
	{
		get
		{
			return this.m_chatFrames != null;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00038334 File Offset: 0x00036534
	public GameObject GameObject
	{
		get
		{
			if (!(this.m_chatFrames == null))
			{
				return this.m_chatFrames.gameObject;
			}
			return null;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00038351 File Offset: 0x00036551
	public BnetPlayer Receiver
	{
		get
		{
			if (!(this.m_chatFrames == null))
			{
				return this.m_chatFrames.Receiver;
			}
			return null;
		}
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00038370 File Offset: 0x00036570
	public void ShowForPlayer(BnetPlayer player)
	{
		string input = UniversalInputManager.UsePhoneUI ? "MobileChatFrames_phone.prefab:044c4b3ec33f4454c9a95d6a9ee52552" : "MobileChatFrames.prefab:1b0605e4925ea4424a53e7b000ad961f";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None);
		if (gameObject != null)
		{
			this.m_chatFrames = gameObject.GetComponent<ChatFrames>();
			this.m_chatFrames.Receiver = player;
		}
		this.m_chatFrames.chatLogFrame.Focus(false);
		this.m_chatFrames.StartCoroutine(this.ShowChatWhenReady(player));
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000383ED File Offset: 0x000365ED
	private IEnumerator ShowChatWhenReady(BnetPlayer player)
	{
		while (this.m_chatFrames == null || this.m_chatFrames.chatLogFrame == null || this.m_chatFrames.chatLogFrame.IsWaitingOnMedal)
		{
			if (this.m_chatFrames == null || this.m_chatFrames.chatLogFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		this.m_chatFrames.chatLogFrame.Focus(true);
		yield break;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000383FC File Offset: 0x000365FC
	public void Hide()
	{
		if (!this.IsShowing)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_chatFrames.gameObject);
		this.m_chatFrames = null;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003841E File Offset: 0x0003661E
	public void GoBack()
	{
		if (!this.IsShowing)
		{
			return;
		}
		this.m_chatFrames.Back();
	}

	// Token: 0x04000672 RID: 1650
	private ChatFrames m_chatFrames;

	// Token: 0x04000673 RID: 1651
	private Map<Renderer, int> m_chatLogOriginalLayers = new Map<Renderer, int>();
}
