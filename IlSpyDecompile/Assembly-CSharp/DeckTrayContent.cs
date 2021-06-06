using UnityEngine;

public class DeckTrayContent : MonoBehaviour
{
	private bool m_isModeActive;

	private bool m_isModeTrying;

	protected virtual void Awake()
	{
	}

	protected virtual void OnDestroy()
	{
	}

	public virtual void Show(bool showAll = false)
	{
	}

	public virtual void Hide(bool hideAll = false)
	{
	}

	public virtual void OnContentLoaded()
	{
	}

	public virtual bool IsContentLoaded()
	{
		return true;
	}

	public virtual bool PreAnimateContentEntrance()
	{
		return true;
	}

	public virtual bool PostAnimateContentEntrance()
	{
		return true;
	}

	public virtual bool AnimateContentEntranceStart()
	{
		return true;
	}

	public virtual bool AnimateContentEntranceEnd()
	{
		return true;
	}

	public virtual bool AnimateContentExitStart()
	{
		return true;
	}

	public virtual bool AnimateContentExitEnd()
	{
		return true;
	}

	public virtual bool PreAnimateContentExit()
	{
		return true;
	}

	public virtual bool PostAnimateContentExit()
	{
		return true;
	}

	public virtual void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
	}

	public bool IsModeActive()
	{
		return m_isModeActive;
	}

	public bool IsModeTryingOrActive()
	{
		if (!m_isModeTrying)
		{
			return m_isModeActive;
		}
		return true;
	}

	public void SetModeActive(bool active)
	{
		m_isModeActive = active;
	}

	public void SetModeTrying(bool trying)
	{
		m_isModeTrying = trying;
	}
}
