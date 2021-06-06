using UnityEngine;

public class QuestProgressToast : GameToast
{
	public UberText m_questTitle;

	public UberText m_questDescription;

	public UberText m_questProgressCount;

	public GameObject m_questProgressCountBg;

	public GameObject m_background;

	private void Awake()
	{
		m_intensityMaterials.Add(m_questProgressCountBg.GetComponent<Renderer>().GetMaterial());
		m_intensityMaterials.Add(m_background.GetComponent<Renderer>().GetMaterial());
	}

	public void UpdateDisplay(string title, string description, int progress, int maxProgress)
	{
		if (maxProgress > 1)
		{
			m_questProgressCountBg.SetActive(value: true);
			m_questProgressCount.Text = GameStrings.Format("GLOBAL_QUEST_PROGRESS_COUNT", progress, maxProgress);
		}
		else
		{
			m_questProgressCountBg.SetActive(value: false);
		}
		m_questTitle.Text = title;
		m_questDescription.Text = description;
	}
}
