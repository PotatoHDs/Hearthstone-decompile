using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using bgs;
using UnityEngine;

public class ChatLog : MonoBehaviour
{
	[Serializable]
	public class Prefabs
	{
		public MobileChatLogMessageFrame myMessage;

		public MobileChatLogMessageFrame theirMessage;

		public MobileChatLogMessageFrame systemMessage;
	}

	[Serializable]
	public class MessageInfo
	{
		public Color infoColor = Color.yellow;

		public Color errorColor = Color.red;

		public Color notificationColor = Color.cyan;
	}

	public TouchList messageFrames;

	public GameObject cameraTarget;

	public Prefabs prefabs;

	public MessageInfo messageInfo;

	public MobileChatNotification notifications;

	private const int maxMessageFrames = 500;

	private const GameLayer messageLayer = GameLayer.BattleNetChat;

	private BnetPlayer receiver;

	private Camera messagesCamera;

	public BnetPlayer Receiver
	{
		get
		{
			return receiver;
		}
		set
		{
			if (receiver == value)
			{
				return;
			}
			receiver = value;
			if (receiver != null)
			{
				UpdateMessages();
				if (!receiver.IsOnline())
				{
					AddReceiverOfflineMessage();
				}
				messageFrames.ScrollValue = 1f;
			}
		}
	}

	private void Awake()
	{
		CreateMessagesCamera();
		if (notifications != null)
		{
			notifications.Notified += OnNotified;
		}
		BnetWhisperMgr.Get().AddWhisperListener(OnWhisper);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		GraphicsManager.Get().OnResolutionChangedEvent += OnResizeAfterCurrentFrame;
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		BnetWhisperMgr.Get().RemoveWhisperListener(OnWhisper);
		if (GraphicsManager.Get() != null)
		{
			GraphicsManager.Get().OnResolutionChangedEvent -= OnResizeAfterCurrentFrame;
		}
		if (notifications != null)
		{
			notifications.Notified -= OnNotified;
		}
	}

	public void OnResize()
	{
		ResizeMessageFrames();
		UpdateMessagesCamera();
	}

	private void ResizeMessageFrames()
	{
		float scrollValue = messageFrames.ScrollValue;
		foreach (ITouchListItem renderedItem in messageFrames.RenderedItems)
		{
			MobileChatLogMessageFrame mobileChatLogMessageFrame = renderedItem as MobileChatLogMessageFrame;
			if (mobileChatLogMessageFrame != null)
			{
				mobileChatLogMessageFrame.Width = messageFrames.ClipSize.x - messageFrames.padding.x - 10f;
				mobileChatLogMessageFrame.RebuildUberText();
			}
		}
		messageFrames.RecalculateItemSizeAndOffsets(ignoreCurrentPosition: true);
		messageFrames.ScrollValue = scrollValue;
	}

	public void OnWhisperFailed()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer.IsAppearingOffline())
		{
			AddAppearOfflineMessage();
		}
		else if (!myPlayer.IsOnline())
		{
			AddSenderOfflineMessage();
		}
		else
		{
			AddReceiverOfflineMessage();
		}
	}

	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (receiver != null && WhisperUtil.IsSpeakerOrReceiver(receiver, whisper))
		{
			AddWhisperMessage(whisper);
			messageFrames.ScrollValue = 1f;
		}
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(receiver);
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
				AddOnlineMessage();
			}
			else
			{
				AddReceiverOfflineMessage();
			}
		}
	}

	private void OnNotified(string text)
	{
		AddSystemMessage(text, messageInfo.notificationColor);
	}

	private void UpdateMessages()
	{
		List<MobileChatLogMessageFrame> list = messageFrames.RenderedItems.Select((ITouchListItem i) => i.GetComponent<MobileChatLogMessageFrame>()).ToList();
		messageFrames.Clear();
		foreach (MobileChatLogMessageFrame item in list)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(receiver);
		if (whispersWithPlayer != null && whispersWithPlayer.Count > 0)
		{
			for (int j = Mathf.Max(whispersWithPlayer.Count - 500, 0); j < whispersWithPlayer.Count; j++)
			{
				BnetWhisper whisper = whispersWithPlayer[j];
				AddWhisperMessage(whisper);
			}
		}
		OnMessagesAdded();
	}

	private void AddWhisperMessage(BnetWhisper whisper)
	{
		string message = ChatUtils.GetMessage(whisper);
		MobileChatLogMessageFrame prefab = (WhisperUtil.IsSpeaker(receiver, whisper) ? prefabs.theirMessage : prefabs.myMessage);
		MobileChatLogMessageFrame item = CreateMessage(prefab, message);
		messageFrames.Add(item);
	}

	private void AddMyMessage(string message)
	{
		string message2 = ChatUtils.GetMessage(message);
		MobileChatLogMessageFrame item = CreateMessage(prefabs.myMessage, message2);
		messageFrames.Add(item);
		OnMessagesAdded();
	}

	private void AddSystemMessage(string message, Color color)
	{
		MobileChatLogMessageFrame item = CreateMessage(prefabs.systemMessage, message, color);
		messageFrames.Add(item);
		OnMessagesAdded();
	}

	private void AddOnlineMessage()
	{
		string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_ONLINE", receiver.GetBestName());
		AddSystemMessage(message, messageInfo.infoColor);
	}

	private void AddReceiverOfflineMessage()
	{
		string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_OFFLINE", receiver.GetBestName());
		AddSystemMessage(message, messageInfo.errorColor);
	}

	private void AddSenderOfflineMessage()
	{
		string message = GameStrings.Get("GLOBAL_CHAT_SENDER_OFFLINE");
		AddSystemMessage(message, messageInfo.errorColor);
	}

	private void AddAppearOfflineMessage()
	{
		string message = GameStrings.Get("GLOBAL_CHAT_SENDER_APPEAR_OFFLINE");
		AddSystemMessage(message, messageInfo.errorColor);
	}

	private void OnMessagesAdded()
	{
		if (messageFrames.RenderedItems.Count() > 500)
		{
			ITouchListItem touchListItem = messageFrames.RenderedItems.First();
			messageFrames.RemoveAt(0);
			UnityEngine.Object.Destroy(touchListItem.gameObject);
		}
		messageFrames.ScrollValue = 1f;
	}

	private MobileChatLogMessageFrame CreateMessage(MobileChatLogMessageFrame prefab, string message)
	{
		MobileChatLogMessageFrame mobileChatLogMessageFrame = UnityEngine.Object.Instantiate(prefab);
		mobileChatLogMessageFrame.Width = messageFrames.ClipSize.x - messageFrames.padding.x - 10f;
		mobileChatLogMessageFrame.Message = message;
		SceneUtils.SetLayer(mobileChatLogMessageFrame, GameLayer.BattleNetChat);
		return mobileChatLogMessageFrame;
	}

	private MobileChatLogMessageFrame CreateMessage(MobileChatLogMessageFrame prefab, string message, Color color)
	{
		MobileChatLogMessageFrame mobileChatLogMessageFrame = CreateMessage(prefab, message);
		mobileChatLogMessageFrame.Color = color;
		return mobileChatLogMessageFrame;
	}

	private void CreateMessagesCamera()
	{
		GameObject gameObject = new GameObject("MessagesCamera");
		gameObject.transform.parent = messageFrames.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, -100f);
		messagesCamera = gameObject.AddComponent<Camera>();
		messagesCamera.orthographic = true;
		messagesCamera.allowHDR = false;
		messagesCamera.depth = BnetBar.CameraDepth + 1;
		messagesCamera.clearFlags = CameraClearFlags.Depth;
		messagesCamera.cullingMask = GameLayer.BattleNetChat.LayerBit();
		UpdateMessagesCamera();
	}

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

	private void UpdateMessagesCamera()
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Bounds boundsFromGameObject = GetBoundsFromGameObject(cameraTarget);
		Vector3 vector = bnetCamera.WorldToScreenPoint(boundsFromGameObject.min);
		Vector3 vector2 = bnetCamera.WorldToScreenPoint(boundsFromGameObject.max);
		messagesCamera.pixelRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		messagesCamera.orthographicSize = messagesCamera.rect.height * bnetCamera.orthographicSize;
	}

	private void OnResizeAfterCurrentFrame(int width, int height)
	{
		StartCoroutine(UpdateMessagesCameraAfterCurrentFrame());
	}

	private IEnumerator UpdateMessagesCameraAfterCurrentFrame()
	{
		yield return null;
		UpdateMessagesCamera();
	}

	[Conditional("CHATLOG_DEBUG")]
	private void AssignMessageFrameNames()
	{
		int num = 0;
		foreach (ITouchListItem renderedItem in messageFrames.RenderedItems)
		{
			MobileChatLogMessageFrame component = renderedItem.GetComponent<MobileChatLogMessageFrame>();
			component.name = $"MessageFrame {num} ({component.Message})";
			num++;
		}
	}
}
