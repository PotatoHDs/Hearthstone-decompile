using System.Collections;
using UnityEngine;

[CustomEditClass]
public class UIBInfoButton : PegUIElement
{
	private const float RAISE_TIME = 0.1f;

	private const float DEPRESS_TIME = 0.1f;

	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public GameObject m_RootObject;

	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public Transform m_UpBone;

	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public Transform m_DownBone;

	[CustomEditField(Sections = "Highlight")]
	[SerializeField]
	public GameObject m_Highlight;

	private UIBHighlight m_UIBHighlight;

	protected override void Awake()
	{
		base.Awake();
		UIBHighlight uIBHighlight = GetComponent<UIBHighlight>();
		if (uIBHighlight == null)
		{
			uIBHighlight = base.gameObject.AddComponent<UIBHighlight>();
		}
		m_UIBHighlight = uIBHighlight;
		if (m_UIBHighlight != null)
		{
			m_UIBHighlight.m_MouseOverHighlight = m_Highlight;
			m_UIBHighlight.m_HideMouseOverOnPress = false;
		}
	}

	public void Select()
	{
		Depress();
	}

	public void Deselect()
	{
		Raise();
	}

	private void Raise()
	{
		Hashtable args = iTween.Hash("position", m_UpBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}

	private void Depress()
	{
		Hashtable args = iTween.Hash("position", m_DownBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}
}
