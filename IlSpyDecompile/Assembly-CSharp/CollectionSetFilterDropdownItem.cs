using UnityEngine;

public class CollectionSetFilterDropdownItem : PegUIElement
{
	public UberText m_dropdownText;

	public MeshRenderer m_iconRenderer;

	public GameObject m_mouseOverBar;

	public GameObject m_selectedBar;

	public Color m_mouseOverColor;

	public Color m_selectedColor;

	public Color m_unselectedColor;

	private bool m_selected;

	private Vector2? m_iconMaterialOffset;

	public Vector2? GetIconMaterialOffset()
	{
		return m_iconMaterialOffset;
	}

	public void Select(bool selection)
	{
		m_selected = selection;
		SetItemColors(m_selected ? m_selectedColor : m_unselectedColor);
		m_selectedBar.SetActive(selection);
		if (m_selected)
		{
			m_mouseOverBar.SetActive(value: false);
		}
	}

	public void SetName(string name)
	{
		m_dropdownText.Text = name;
	}

	public void SetIconMaterialOffset(Vector2 offset)
	{
		m_iconMaterialOffset = offset;
		m_iconRenderer.GetMaterial().SetTextureOffset("_MainTex", offset);
	}

	public void DisableIconMaterial()
	{
		m_iconMaterialOffset = null;
		m_iconRenderer.GetMaterial().SetTextureScale("_MainTex", Vector2.zero);
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (!m_selected)
		{
			SetItemColors(m_mouseOverColor);
			m_mouseOverBar.SetActive(value: true);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_mouseOverBar.SetActive(value: false);
		SetItemColors(m_selected ? m_selectedColor : m_unselectedColor);
	}

	private void SetItemColors(Color color)
	{
		m_iconRenderer.GetMaterial().SetColor("_Color", color);
		m_dropdownText.TextColor = color;
	}
}
