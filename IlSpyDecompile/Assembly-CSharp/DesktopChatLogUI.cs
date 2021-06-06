using UnityEngine;

public class DesktopChatLogUI : IChatLogUI
{
	private QuickChatFrame m_quickChatFrame;

	public bool IsShowing => m_quickChatFrame != null;

	public GameObject GameObject
	{
		get
		{
			if (!(m_quickChatFrame == null))
			{
				return m_quickChatFrame.gameObject;
			}
			return null;
		}
	}

	public BnetPlayer Receiver
	{
		get
		{
			if (!(m_quickChatFrame == null))
			{
				return m_quickChatFrame.GetReceiver();
			}
			return null;
		}
	}

	public void ShowForPlayer(BnetPlayer player)
	{
		if (!(m_quickChatFrame != null))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("QuickChatFrame.prefab:a8bbab56b6588e44a8f0d25fc30ae886");
			if (gameObject != null)
			{
				m_quickChatFrame = gameObject.GetComponent<QuickChatFrame>();
				m_quickChatFrame.SetReceiver(player);
			}
		}
	}

	public void Hide()
	{
		if (!(m_quickChatFrame == null))
		{
			Object.Destroy(m_quickChatFrame.gameObject);
			m_quickChatFrame = null;
		}
	}

	public void GoBack()
	{
	}
}
