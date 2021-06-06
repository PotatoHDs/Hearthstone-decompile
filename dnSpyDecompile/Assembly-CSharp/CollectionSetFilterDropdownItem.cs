using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class CollectionSetFilterDropdownItem : PegUIElement
{
	// Token: 0x06001287 RID: 4743 RVA: 0x00069ABE File Offset: 0x00067CBE
	public Vector2? GetIconMaterialOffset()
	{
		return this.m_iconMaterialOffset;
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x00069AC8 File Offset: 0x00067CC8
	public void Select(bool selection)
	{
		this.m_selected = selection;
		this.SetItemColors(this.m_selected ? this.m_selectedColor : this.m_unselectedColor);
		this.m_selectedBar.SetActive(selection);
		if (this.m_selected)
		{
			this.m_mouseOverBar.SetActive(false);
		}
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x00069B18 File Offset: 0x00067D18
	public void SetName(string name)
	{
		this.m_dropdownText.Text = name;
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00069B26 File Offset: 0x00067D26
	public void SetIconMaterialOffset(Vector2 offset)
	{
		this.m_iconMaterialOffset = new Vector2?(offset);
		this.m_iconRenderer.GetMaterial().SetTextureOffset("_MainTex", offset);
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x00069B4A File Offset: 0x00067D4A
	public void DisableIconMaterial()
	{
		this.m_iconMaterialOffset = null;
		this.m_iconRenderer.GetMaterial().SetTextureScale("_MainTex", Vector2.zero);
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x00069B72 File Offset: 0x00067D72
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_selected)
		{
			return;
		}
		this.SetItemColors(this.m_mouseOverColor);
		this.m_mouseOverBar.SetActive(true);
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x00069B95 File Offset: 0x00067D95
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_mouseOverBar.SetActive(false);
		this.SetItemColors(this.m_selected ? this.m_selectedColor : this.m_unselectedColor);
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x00069BBF File Offset: 0x00067DBF
	private void SetItemColors(Color color)
	{
		this.m_iconRenderer.GetMaterial().SetColor("_Color", color);
		this.m_dropdownText.TextColor = color;
	}

	// Token: 0x04000BD1 RID: 3025
	public UberText m_dropdownText;

	// Token: 0x04000BD2 RID: 3026
	public MeshRenderer m_iconRenderer;

	// Token: 0x04000BD3 RID: 3027
	public GameObject m_mouseOverBar;

	// Token: 0x04000BD4 RID: 3028
	public GameObject m_selectedBar;

	// Token: 0x04000BD5 RID: 3029
	public Color m_mouseOverColor;

	// Token: 0x04000BD6 RID: 3030
	public Color m_selectedColor;

	// Token: 0x04000BD7 RID: 3031
	public Color m_unselectedColor;

	// Token: 0x04000BD8 RID: 3032
	private bool m_selected;

	// Token: 0x04000BD9 RID: 3033
	private Vector2? m_iconMaterialOffset;
}
