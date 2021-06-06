using System;
using UnityEngine;

// Token: 0x02000610 RID: 1552
public class OptionsMenuPhone : MonoBehaviour
{
	// Token: 0x060056DA RID: 22234 RVA: 0x001C74BE File Offset: 0x001C56BE
	private void Start()
	{
		this.m_doneButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_optionsMenu.Hide(true);
		});
	}

	// Token: 0x04004ACA RID: 19146
	public OptionsMenu m_optionsMenu;

	// Token: 0x04004ACB RID: 19147
	public UIBButton m_doneButton;

	// Token: 0x04004ACC RID: 19148
	public GameObject m_mainContentsPanel;
}
