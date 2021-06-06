using UnityEngine;

public class CheatHighlight : MonoBehaviour
{
	[SerializeField]
	public GameObject m_highlight;

	private void OnMouseEnter()
	{
		m_highlight.SetActive(value: true);
	}

	private void OnMouseExit()
	{
		m_highlight.SetActive(value: false);
	}
}
