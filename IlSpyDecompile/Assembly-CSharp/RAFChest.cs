using UnityEngine;

public class RAFChest : PegUIElement
{
	public Renderer m_chestQuad;

	public GameObject m_tooltipBone;

	private bool m_isChestOpen;

	public void SetOpen(bool isChestOpen)
	{
		if (m_isChestOpen != isChestOpen)
		{
			m_isChestOpen = isChestOpen;
			m_chestQuad.GetMaterial().SetTextureOffset("_MainTex", new Vector2(m_isChestOpen ? 0.5f : 0f, 0.5f));
			base.gameObject.GetComponent<UIBHighlight>().EnableResponse = !m_isChestOpen;
		}
	}

	public bool IsOpen()
	{
		return m_isChestOpen;
	}
}
