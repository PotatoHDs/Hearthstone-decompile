using System;
using UnityEngine;
using UnityEngine.UI;

public class UGUIButton : MonoBehaviour
{
	[SerializeField]
	private Text m_buttonText;

	[SerializeField]
	private Button m_button;

	public void SetupButton(string text, Action callback, Action closeAction)
	{
		if (m_buttonText != null)
		{
			m_buttonText.text = text;
		}
		if (!(m_button != null))
		{
			return;
		}
		m_button.onClick.RemoveAllListeners();
		m_button.onClick.AddListener(delegate
		{
			if (closeAction != null)
			{
				closeAction();
			}
			if (callback != null)
			{
				callback();
			}
		});
	}
}
