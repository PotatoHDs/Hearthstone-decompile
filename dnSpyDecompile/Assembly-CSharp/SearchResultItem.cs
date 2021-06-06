using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B43 RID: 2883
public class SearchResultItem : MonoBehaviour
{
	// Token: 0x1400009C RID: 156
	// (add) Token: 0x060098DA RID: 39130 RVA: 0x0031775C File Offset: 0x0031595C
	// (remove) Token: 0x060098DB RID: 39131 RVA: 0x00317794 File Offset: 0x00315994
	public event Action OnClose;

	// Token: 0x060098DC RID: 39132 RVA: 0x003177CC File Offset: 0x003159CC
	private void Start()
	{
		this.m_textElement = base.transform.Find("Text").GetComponent<Text>();
		this.m_textElement.text = this.m_text;
		this.m_closeButtonElement = base.transform.Find("CloseButton").GetComponent<Button>();
		if (this.m_showCloseButton)
		{
			this.m_closeButtonElement.gameObject.SetActive(true);
			this.m_closeButtonElement.onClick.AddListener(delegate()
			{
				this.OnClose();
			});
			return;
		}
		this.m_closeButtonElement.gameObject.SetActive(false);
	}

	// Token: 0x04007FD2 RID: 32722
	public string m_text;

	// Token: 0x04007FD3 RID: 32723
	public string m_card;

	// Token: 0x04007FD4 RID: 32724
	public bool m_showCloseButton;

	// Token: 0x04007FD6 RID: 32726
	private Text m_textElement;

	// Token: 0x04007FD7 RID: 32727
	private Button m_closeButtonElement;
}
