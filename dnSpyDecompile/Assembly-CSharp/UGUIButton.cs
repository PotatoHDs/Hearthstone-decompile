using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B33 RID: 2867
public class UGUIButton : MonoBehaviour
{
	// Token: 0x0600985D RID: 39005 RVA: 0x00315830 File Offset: 0x00313A30
	public void SetupButton(string text, Action callback, Action closeAction)
	{
		if (this.m_buttonText != null)
		{
			this.m_buttonText.text = text;
		}
		if (this.m_button != null)
		{
			this.m_button.onClick.RemoveAllListeners();
			this.m_button.onClick.AddListener(delegate()
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

	// Token: 0x04007F68 RID: 32616
	[SerializeField]
	private Text m_buttonText;

	// Token: 0x04007F69 RID: 32617
	[SerializeField]
	private Button m_button;
}
