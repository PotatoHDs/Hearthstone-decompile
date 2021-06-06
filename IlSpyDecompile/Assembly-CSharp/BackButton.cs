using UnityEngine;

public class BackButton : PegUIElement
{
	public GameObject m_highlight;

	public UberText m_backText;

	public static KeyCode backKey;

	protected override void Awake()
	{
		base.Awake();
		SetOriginalLocalPosition();
		m_highlight.SetActive(value: false);
		if ((bool)m_backText)
		{
			m_backText.Text = GameStrings.Get("GLOBAL_BACK");
		}
	}

	protected override void OnPress()
	{
		Vector3 originalLocalPosition = GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y - 0.3f, originalLocalPosition.z);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", vector, "isLocal", true, "time", 0.15f));
	}

	protected override void OnRelease()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", GetOriginalLocalPosition(), "isLocal", true, "time", 0.15f));
	}

	protected override void OnOver(InteractionState oldState)
	{
		Vector3 originalLocalPosition = GetOriginalLocalPosition();
		Vector3 vector = new Vector3(originalLocalPosition.x, originalLocalPosition.y + 0.5f, originalLocalPosition.z);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", vector, "isLocal", true, "time", 0.15f));
		m_highlight.SetActive(value: true);
	}

	protected override void OnOut(InteractionState oldState)
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", GetOriginalLocalPosition(), "isLocal", true, "time", 0.15f));
		m_highlight.SetActive(value: false);
	}
}
