using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using bgs;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class ChatLog : MonoBehaviour
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060006FA RID: 1786 RVA: 0x000282E3 File Offset: 0x000264E3
	// (set) Token: 0x060006FB RID: 1787 RVA: 0x000282EC File Offset: 0x000264EC
	public BnetPlayer Receiver
	{
		get
		{
			return this.receiver;
		}
		set
		{
			if (this.receiver == value)
			{
				return;
			}
			this.receiver = value;
			if (this.receiver != null)
			{
				this.UpdateMessages();
				if (!this.receiver.IsOnline())
				{
					this.AddReceiverOfflineMessage();
				}
				this.messageFrames.ScrollValue = 1f;
			}
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002833C File Offset: 0x0002653C
	private void Awake()
	{
		this.CreateMessagesCamera();
		if (this.notifications != null)
		{
			this.notifications.Notified += this.OnNotified;
		}
		BnetWhisperMgr.Get().AddWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		GraphicsManager.Get().OnResolutionChangedEvent += this.OnResizeAfterCurrentFrame;
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x000283B8 File Offset: 0x000265B8
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetWhisperMgr.Get().RemoveWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		if (GraphicsManager.Get() != null)
		{
			GraphicsManager.Get().OnResolutionChangedEvent -= this.OnResizeAfterCurrentFrame;
		}
		if (this.notifications != null)
		{
			this.notifications.Notified -= this.OnNotified;
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00028435 File Offset: 0x00026635
	public void OnResize()
	{
		this.ResizeMessageFrames();
		this.UpdateMessagesCamera();
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00028444 File Offset: 0x00026644
	private void ResizeMessageFrames()
	{
		float scrollValue = this.messageFrames.ScrollValue;
		foreach (ITouchListItem touchListItem in this.messageFrames.RenderedItems)
		{
			MobileChatLogMessageFrame mobileChatLogMessageFrame = touchListItem as MobileChatLogMessageFrame;
			if (mobileChatLogMessageFrame != null)
			{
				mobileChatLogMessageFrame.Width = this.messageFrames.ClipSize.x - this.messageFrames.padding.x - 10f;
				mobileChatLogMessageFrame.RebuildUberText();
			}
		}
		this.messageFrames.RecalculateItemSizeAndOffsets(true);
		this.messageFrames.ScrollValue = scrollValue;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x000284F4 File Offset: 0x000266F4
	public void OnWhisperFailed()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer.IsAppearingOffline())
		{
			this.AddAppearOfflineMessage();
			return;
		}
		if (!myPlayer.IsOnline())
		{
			this.AddSenderOfflineMessage();
			return;
		}
		this.AddReceiverOfflineMessage();
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00028530 File Offset: 0x00026730
	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (this.receiver == null || !WhisperUtil.IsSpeakerOrReceiver(this.receiver, whisper))
		{
			return;
		}
		this.AddWhisperMessage(whisper);
		this.messageFrames.ScrollValue = 1f;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00028560 File Offset: 0x00026760
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(this.receiver);
		if (bnetPlayerChange == null)
		{
			return;
		}
		BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
		BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
		if (oldPlayer == null || oldPlayer.IsOnline() != newPlayer.IsOnline())
		{
			if (newPlayer.IsOnline())
			{
				this.AddOnlineMessage();
				return;
			}
			this.AddReceiverOfflineMessage();
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x000285B2 File Offset: 0x000267B2
	private void OnNotified(string text)
	{
		this.AddSystemMessage(text, this.messageInfo.notificationColor);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x000285C8 File Offset: 0x000267C8
	private void UpdateMessages()
	{
		List<MobileChatLogMessageFrame> list = (from i in this.messageFrames.RenderedItems
		select i.GetComponent<MobileChatLogMessageFrame>()).ToList<MobileChatLogMessageFrame>();
		this.messageFrames.Clear();
		foreach (MobileChatLogMessageFrame mobileChatLogMessageFrame in list)
		{
			UnityEngine.Object.Destroy(mobileChatLogMessageFrame.gameObject);
		}
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(this.receiver);
		if (whispersWithPlayer != null && whispersWithPlayer.Count > 0)
		{
			for (int j = Mathf.Max(whispersWithPlayer.Count - 500, 0); j < whispersWithPlayer.Count; j++)
			{
				BnetWhisper whisper = whispersWithPlayer[j];
				this.AddWhisperMessage(whisper);
			}
		}
		this.OnMessagesAdded();
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x000286AC File Offset: 0x000268AC
	private void AddWhisperMessage(BnetWhisper whisper)
	{
		string message = ChatUtils.GetMessage(whisper);
		MobileChatLogMessageFrame prefab = WhisperUtil.IsSpeaker(this.receiver, whisper) ? this.prefabs.theirMessage : this.prefabs.myMessage;
		MobileChatLogMessageFrame item = this.CreateMessage(prefab, message);
		this.messageFrames.Add(item);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x000286FC File Offset: 0x000268FC
	private void AddMyMessage(string message)
	{
		string message2 = ChatUtils.GetMessage(message);
		MobileChatLogMessageFrame item = this.CreateMessage(this.prefabs.myMessage, message2);
		this.messageFrames.Add(item);
		this.OnMessagesAdded();
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00028738 File Offset: 0x00026938
	private void AddSystemMessage(string message, Color color)
	{
		MobileChatLogMessageFrame item = this.CreateMessage(this.prefabs.systemMessage, message, color);
		this.messageFrames.Add(item);
		this.OnMessagesAdded();
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0002876C File Offset: 0x0002696C
	private void AddOnlineMessage()
	{
		string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_ONLINE", new object[]
		{
			this.receiver.GetBestName()
		});
		this.AddSystemMessage(message, this.messageInfo.infoColor);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x000287AC File Offset: 0x000269AC
	private void AddReceiverOfflineMessage()
	{
		string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_OFFLINE", new object[]
		{
			this.receiver.GetBestName()
		});
		this.AddSystemMessage(message, this.messageInfo.errorColor);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x000287EC File Offset: 0x000269EC
	private void AddSenderOfflineMessage()
	{
		string message = GameStrings.Get("GLOBAL_CHAT_SENDER_OFFLINE");
		this.AddSystemMessage(message, this.messageInfo.errorColor);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00028818 File Offset: 0x00026A18
	private void AddAppearOfflineMessage()
	{
		string message = GameStrings.Get("GLOBAL_CHAT_SENDER_APPEAR_OFFLINE");
		this.AddSystemMessage(message, this.messageInfo.errorColor);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00028844 File Offset: 0x00026A44
	private void OnMessagesAdded()
	{
		if (this.messageFrames.RenderedItems.Count<ITouchListItem>() > 500)
		{
			ITouchListItem touchListItem = this.messageFrames.RenderedItems.First<ITouchListItem>();
			this.messageFrames.RemoveAt(0);
			UnityEngine.Object.Destroy(touchListItem.gameObject);
		}
		this.messageFrames.ScrollValue = 1f;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x000288A0 File Offset: 0x00026AA0
	private MobileChatLogMessageFrame CreateMessage(MobileChatLogMessageFrame prefab, string message)
	{
		MobileChatLogMessageFrame mobileChatLogMessageFrame = UnityEngine.Object.Instantiate<MobileChatLogMessageFrame>(prefab);
		mobileChatLogMessageFrame.Width = this.messageFrames.ClipSize.x - this.messageFrames.padding.x - 10f;
		mobileChatLogMessageFrame.Message = message;
		SceneUtils.SetLayer(mobileChatLogMessageFrame, GameLayer.BattleNetChat);
		return mobileChatLogMessageFrame;
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x000288EF File Offset: 0x00026AEF
	private MobileChatLogMessageFrame CreateMessage(MobileChatLogMessageFrame prefab, string message, Color color)
	{
		MobileChatLogMessageFrame mobileChatLogMessageFrame = this.CreateMessage(prefab, message);
		mobileChatLogMessageFrame.Color = color;
		return mobileChatLogMessageFrame;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00028900 File Offset: 0x00026B00
	private void CreateMessagesCamera()
	{
		this.messagesCamera = new GameObject("MessagesCamera")
		{
			transform = 
			{
				parent = this.messageFrames.transform,
				localPosition = new Vector3(0f, 0f, -100f)
			}
		}.AddComponent<Camera>();
		this.messagesCamera.orthographic = true;
		this.messagesCamera.allowHDR = false;
		this.messagesCamera.depth = (float)(BnetBar.CameraDepth + 1);
		this.messagesCamera.clearFlags = CameraClearFlags.Depth;
		this.messagesCamera.cullingMask = GameLayer.BattleNetChat.LayerBit();
		this.UpdateMessagesCamera();
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000289A8 File Offset: 0x00026BA8
	private Bounds GetBoundsFromGameObject(GameObject go)
	{
		Renderer component = go.GetComponent<Renderer>();
		if (component != null)
		{
			return component.bounds;
		}
		Collider component2 = go.GetComponent<Collider>();
		if (component2 != null)
		{
			return component2.bounds;
		}
		return default(Bounds);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x000289EC File Offset: 0x00026BEC
	private void UpdateMessagesCamera()
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Bounds boundsFromGameObject = this.GetBoundsFromGameObject(this.cameraTarget);
		Vector3 vector = bnetCamera.WorldToScreenPoint(boundsFromGameObject.min);
		Vector3 vector2 = bnetCamera.WorldToScreenPoint(boundsFromGameObject.max);
		this.messagesCamera.pixelRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		this.messagesCamera.orthographicSize = this.messagesCamera.rect.height * bnetCamera.orthographicSize;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00028A89 File Offset: 0x00026C89
	private void OnResizeAfterCurrentFrame(int width, int height)
	{
		base.StartCoroutine(this.UpdateMessagesCameraAfterCurrentFrame());
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00028A98 File Offset: 0x00026C98
	private IEnumerator UpdateMessagesCameraAfterCurrentFrame()
	{
		yield return null;
		this.UpdateMessagesCamera();
		yield break;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00028AA8 File Offset: 0x00026CA8
	[Conditional("CHATLOG_DEBUG")]
	private void AssignMessageFrameNames()
	{
		int num = 0;
		foreach (ITouchListItem touchListItem in this.messageFrames.RenderedItems)
		{
			MobileChatLogMessageFrame component = touchListItem.GetComponent<MobileChatLogMessageFrame>();
			component.name = string.Format("MessageFrame {0} ({1})", num, component.Message);
			num++;
		}
	}

	// Token: 0x040004D5 RID: 1237
	public TouchList messageFrames;

	// Token: 0x040004D6 RID: 1238
	public GameObject cameraTarget;

	// Token: 0x040004D7 RID: 1239
	public ChatLog.Prefabs prefabs;

	// Token: 0x040004D8 RID: 1240
	public ChatLog.MessageInfo messageInfo;

	// Token: 0x040004D9 RID: 1241
	public MobileChatNotification notifications;

	// Token: 0x040004DA RID: 1242
	private const int maxMessageFrames = 500;

	// Token: 0x040004DB RID: 1243
	private const GameLayer messageLayer = GameLayer.BattleNetChat;

	// Token: 0x040004DC RID: 1244
	private BnetPlayer receiver;

	// Token: 0x040004DD RID: 1245
	private Camera messagesCamera;

	// Token: 0x02001376 RID: 4982
	[Serializable]
	public class Prefabs
	{
		// Token: 0x0400A6D4 RID: 42708
		public MobileChatLogMessageFrame myMessage;

		// Token: 0x0400A6D5 RID: 42709
		public MobileChatLogMessageFrame theirMessage;

		// Token: 0x0400A6D6 RID: 42710
		public MobileChatLogMessageFrame systemMessage;
	}

	// Token: 0x02001377 RID: 4983
	[Serializable]
	public class MessageInfo
	{
		// Token: 0x0400A6D7 RID: 42711
		public Color infoColor = Color.yellow;

		// Token: 0x0400A6D8 RID: 42712
		public Color errorColor = Color.red;

		// Token: 0x0400A6D9 RID: 42713
		public Color notificationColor = Color.cyan;
	}
}
