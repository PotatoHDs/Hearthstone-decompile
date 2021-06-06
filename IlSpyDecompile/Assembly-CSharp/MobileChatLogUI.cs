using System.Collections;
using UnityEngine;

public class MobileChatLogUI : IChatLogUI
{
	private ChatFrames m_chatFrames;

	private Map<Renderer, int> m_chatLogOriginalLayers = new Map<Renderer, int>();

	public bool IsShowing => m_chatFrames != null;

	public GameObject GameObject
	{
		get
		{
			if (!(m_chatFrames == null))
			{
				return m_chatFrames.gameObject;
			}
			return null;
		}
	}

	public BnetPlayer Receiver
	{
		get
		{
			if (!(m_chatFrames == null))
			{
				return m_chatFrames.Receiver;
			}
			return null;
		}
	}

	public void ShowForPlayer(BnetPlayer player)
	{
		string text = (UniversalInputManager.UsePhoneUI ? "MobileChatFrames_phone.prefab:044c4b3ec33f4454c9a95d6a9ee52552" : "MobileChatFrames.prefab:1b0605e4925ea4424a53e7b000ad961f");
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject != null)
		{
			m_chatFrames = gameObject.GetComponent<ChatFrames>();
			m_chatFrames.Receiver = player;
		}
		m_chatFrames.chatLogFrame.Focus(focus: false);
		m_chatFrames.StartCoroutine(ShowChatWhenReady(player));
	}

	private IEnumerator ShowChatWhenReady(BnetPlayer player)
	{
		while (m_chatFrames == null || m_chatFrames.chatLogFrame == null || m_chatFrames.chatLogFrame.IsWaitingOnMedal)
		{
			if (m_chatFrames == null || m_chatFrames.chatLogFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		m_chatFrames.chatLogFrame.Focus(focus: true);
	}

	public void Hide()
	{
		if (IsShowing)
		{
			Object.Destroy(m_chatFrames.gameObject);
			m_chatFrames = null;
		}
	}

	public void GoBack()
	{
		if (IsShowing)
		{
			m_chatFrames.Back();
		}
	}
}
