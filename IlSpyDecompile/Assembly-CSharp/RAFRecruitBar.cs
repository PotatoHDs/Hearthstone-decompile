using PegasusShared;
using UnityEngine;

public class RAFRecruitBar : PegUIElement
{
	public UberText m_battleTag;

	public UberText m_level;

	public ProgressBar m_progressBar;

	public MeshRenderer m_progressBarRenderer;

	public MaterialContainer m_progressCompleteMaterialContainer;

	private BnetId m_gameAccountId;

	private bool m_isLocked = true;

	private void Start()
	{
		AddEventListener(UIEventType.ROLLOVER, OnBarOver);
		AddEventListener(UIEventType.ROLLOUT, OnBarOut);
	}

	public void SetGameAccountId(BnetId gameAccountId)
	{
		m_gameAccountId = gameAccountId;
	}

	public BnetId GetGameAccountId()
	{
		return m_gameAccountId;
	}

	public void SetBattleTag(string battleTag)
	{
		m_battleTag.Text = battleTag;
	}

	public void SetLevel(int level)
	{
		m_level.Text = level.ToString();
		if (level >= 20)
		{
			m_progressBarRenderer.SetMaterial(m_progressCompleteMaterialContainer.m_material);
			m_progressBar.SetMaterial(m_progressBarRenderer.GetMaterial());
		}
		m_progressBar.SetProgressBar((float)level / 20f);
	}

	public void SetLocked(bool isLocked)
	{
		m_isLocked = isLocked;
		if (m_isLocked)
		{
			m_progressBar.gameObject.SetActive(value: false);
			SetBattleTag(GameStrings.Format("GLUE_RAF_PROGRESS_FRAME_LOCKED_BAR_NAME"));
		}
		else
		{
			m_progressBar.gameObject.SetActive(value: true);
		}
	}

	private void OnBarOver(UIEvent e)
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (!(component == null))
		{
			component.ShowLayerTooltip(GameStrings.Get("GLUE_RAF_RECRUIT_BAR_TOOLTIP_HEADLINE"), GameStrings.Get("GLUE_RAF_RECRUIT_BAR_TOOLTIP_DESC"));
		}
	}

	private void OnBarOut(UIEvent e)
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}
}
