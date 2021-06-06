using System;

// Token: 0x02000937 RID: 2359
public class TutorialNotification : Notification
{
	// Token: 0x0600824D RID: 33357 RVA: 0x002A58D1 File Offset: 0x002A3AD1
	public void SetWantedText(string txt)
	{
		if (this.m_WantedText != null)
		{
			this.m_WantedText.Text = txt;
			this.m_WantedText.gameObject.SetActive(true);
		}
	}

	// Token: 0x04006D25 RID: 27941
	public UIBButton m_ButtonStart;

	// Token: 0x04006D26 RID: 27942
	public UberText m_WantedText;
}
