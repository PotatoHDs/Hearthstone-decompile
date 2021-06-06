using UnityEngine;

public class HeroChoiceActor : Actor
{
	public UberText m_nameText;

	public void SetNameText(string text)
	{
		if (m_nameText != null)
		{
			m_nameText.Text = text;
		}
	}

	public void SetNameTextActive(bool active)
	{
		if (m_nameText != null)
		{
			m_nameText.gameObject.SetActive(active);
		}
	}

	protected override void ShowImpl(bool ignoreSpells)
	{
		base.ShowImpl(ignoreSpells);
		if (m_nameTextMesh != null)
		{
			m_nameTextMesh.gameObject.SetActive(value: false);
			if ((bool)m_nameTextMesh.RenderOnObject)
			{
				m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
