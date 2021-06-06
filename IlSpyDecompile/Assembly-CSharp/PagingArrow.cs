using UnityEngine;

public class PagingArrow : MonoBehaviour
{
	public GameObject m_pagingArrowHighlight;

	public void ShowHighlight()
	{
		m_pagingArrowHighlight.SetActive(value: true);
	}

	public void HideHighlight()
	{
		m_pagingArrowHighlight.SetActive(value: false);
	}
}
