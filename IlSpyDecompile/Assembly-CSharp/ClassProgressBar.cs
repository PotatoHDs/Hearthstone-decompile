using UnityEngine;

public class ClassProgressBar : PegUIElement
{
	public TAG_CLASS m_class;

	public UberText m_classNameText;

	public UberText m_levelText;

	public GameObject m_classLockedGO;

	public ProgressBar m_progressBar;

	public GameObject m_classIcon;

	public GameObject m_levelFrame;

	public GameObject m_tooltipRoot;

	public UberText m_tooltipTitle;

	public UberText m_tooltipDesc;

	public UberText m_tooltipLevelText;

	private string m_rewardText;

	private bool m_tooltipAvailable;

	private bool m_locked;

	protected override void Awake()
	{
		base.Awake();
		AddEventListener(UIEventType.ROLLOVER, OnProgressBarOver);
		AddEventListener(UIEventType.ROLLOUT, OnProgressBarOut);
	}

	public void Init()
	{
		if (m_classNameText != null)
		{
			m_classNameText.Text = GameStrings.GetClassName(m_class);
		}
	}

	public void SetTooltipText(string title, string desc, string currentLevel)
	{
		if (m_tooltipRoot != null)
		{
			m_tooltipAvailable = true;
			m_tooltipRoot.GetComponent<TooltipPanel>().Initialize(title, desc);
			m_tooltipLevelText.Text = currentLevel.ToString();
			Transform[] componentsInChildren = m_tooltipRoot.GetComponentsInChildren<Transform>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = base.gameObject.layer;
			}
			m_tooltipRoot.SetActive(value: false);
		}
	}

	public void SetPremium(bool isPremium)
	{
		if (!isPremium)
		{
			GetComponent<Renderer>().GetMaterial().SetTexture("_MaskTex", null);
		}
	}

	public void Lock()
	{
		m_locked = true;
		m_classLockedGO.SetActive(value: true);
		m_levelFrame.SetActive(value: false);
	}

	private void OnProgressBarOver(UIEvent e)
	{
		if (!m_locked && m_tooltipAvailable)
		{
			m_tooltipRoot.SetActive(value: true);
		}
	}

	private void OnProgressBarOut(UIEvent e)
	{
		if (!m_locked)
		{
			m_tooltipRoot.SetActive(value: false);
		}
	}
}
