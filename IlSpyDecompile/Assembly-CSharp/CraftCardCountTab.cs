using UnityEngine;

public class CraftCardCountTab : MonoBehaviour
{
	public UberText m_count;

	public UberText m_x;

	public UberText m_plus;

	public GameObject m_shadow;

	public Color m_normalColor;

	public Color m_goldenColor;

	public Material m_normalMaterial;

	public Material m_goldenMaterial;

	public MeshRenderer m_countTab;

	public void UpdateText(int numCopies, TAG_PREMIUM premium)
	{
		if (premium == TAG_PREMIUM.DIAMOND)
		{
			base.gameObject.SetActive(value: false);
		}
		if (numCopies > 9)
		{
			m_count.Text = "9";
			m_plus.gameObject.SetActive(value: true);
			return;
		}
		if (numCopies >= 2)
		{
			m_shadow.SetActive(value: true);
			m_shadow.GetComponent<Animation>().Play("Crafting2ndCardShadow");
		}
		else
		{
			m_shadow.SetActive(value: false);
		}
		m_count.TextColor = m_normalColor;
		m_plus.TextColor = m_normalColor;
		m_x.TextColor = m_normalColor;
		m_countTab.SetMaterial(m_normalMaterial);
		m_count.Text = numCopies.ToString();
		m_plus.gameObject.SetActive(value: false);
	}
}
