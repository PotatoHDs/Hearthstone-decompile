using UnityEngine;

public class PackOpeningButton : BoxMenuButton
{
	public UberText m_count;

	public GameObject m_countFrame;

	public string GetGetPackCount()
	{
		return m_count.Text;
	}

	public void SetPackCount(int packs)
	{
		if (packs < 0)
		{
			m_count.Text = "";
			return;
		}
		m_count.Text = GameStrings.Format("GLUE_PACK_OPENING_BOOSTER_COUNT", packs);
	}
}
