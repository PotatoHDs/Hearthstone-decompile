using System;
using UnityEngine;
using UnityEngine.UI;

public class SearchResultItem : MonoBehaviour
{
	public string m_text;

	public string m_card;

	public bool m_showCloseButton;

	private Text m_textElement;

	private Button m_closeButtonElement;

	public event Action OnClose;

	private void Start()
	{
		m_textElement = base.transform.Find("Text").GetComponent<Text>();
		m_textElement.text = m_text;
		m_closeButtonElement = base.transform.Find("CloseButton").GetComponent<Button>();
		if (m_showCloseButton)
		{
			m_closeButtonElement.gameObject.SetActive(value: true);
			m_closeButtonElement.onClick.AddListener(delegate
			{
				this.OnClose();
			});
		}
		else
		{
			m_closeButtonElement.gameObject.SetActive(value: false);
		}
	}
}
