using System.Collections;
using UnityEngine;

public class MassDisenchantTab : PegUIElement
{
	public GameObject m_root;

	public GameObject m_highlight;

	public UberText m_amount;

	private static readonly Vector3 SELECTED_SCALE = new Vector3(0.6f, 0.6f, 0.6f);

	private static readonly float SELECTED_LOCAL_Y_OFFSET = 0.3822131f;

	private bool m_isSelected;

	private Vector3 m_originalLocalPos;

	private Vector3 m_originalScale;

	private bool m_isVisible;

	protected override void Awake()
	{
		base.Awake();
		m_highlight.SetActive(value: false);
		m_originalLocalPos = base.transform.localPosition;
		m_originalScale = base.transform.localScale;
	}

	private void Start()
	{
		AddEventListener(UIEventType.ROLLOVER, OnRollover);
		AddEventListener(UIEventType.ROLLOUT, OnRollout);
	}

	public void Show()
	{
		m_isVisible = true;
		m_root.SetActive(value: true);
		SetEnabled(enabled: true);
	}

	public void Hide()
	{
		m_isVisible = false;
		m_root.SetActive(value: false);
		SetEnabled(enabled: false);
	}

	public bool IsVisible()
	{
		return m_isVisible;
	}

	public void SetAmount(int amount)
	{
		m_amount.Text = amount.ToString();
	}

	public void Select()
	{
		if (!m_isSelected)
		{
			m_isSelected = true;
			Hashtable args = iTween.Hash("scale", SELECTED_SCALE, "time", CollectionPageManager.SELECT_TAB_ANIM_TIME, "name", "scale");
			iTween.StopByName(base.gameObject, "scale");
			iTween.ScaleTo(base.gameObject, args);
			Vector3 originalLocalPos = m_originalLocalPos;
			originalLocalPos.y += SELECTED_LOCAL_Y_OFFSET;
			Hashtable args2 = iTween.Hash("position", originalLocalPos, "isLocal", true, "time", CollectionPageManager.SELECT_TAB_ANIM_TIME, "name", "position");
			iTween.StopByName(base.gameObject, "position");
			iTween.MoveTo(base.gameObject, args2);
		}
	}

	public void Deselect()
	{
		if (m_isSelected)
		{
			m_isSelected = false;
			Hashtable args = iTween.Hash("scale", m_originalScale, "time", CollectionPageManager.SELECT_TAB_ANIM_TIME, "name", "scale");
			iTween.StopByName(base.gameObject, "scale");
			iTween.ScaleTo(base.gameObject, args);
			Hashtable args2 = iTween.Hash("position", m_originalLocalPos, "isLocal", true, "time", CollectionPageManager.SELECT_TAB_ANIM_TIME, "name", "position");
			iTween.StopByName(base.gameObject, "position");
			iTween.MoveTo(base.gameObject, args2);
		}
	}

	private void OnRollover(UIEvent e)
	{
		m_highlight.SetActive(value: true);
	}

	private void OnRollout(UIEvent e)
	{
		m_highlight.SetActive(value: false);
	}
}
