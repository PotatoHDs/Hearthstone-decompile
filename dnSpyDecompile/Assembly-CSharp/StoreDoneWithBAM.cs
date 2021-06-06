using System;
using System.Collections.Generic;

// Token: 0x02000723 RID: 1827
public class StoreDoneWithBAM : UIBPopup
{
	// Token: 0x060065D5 RID: 26069 RVA: 0x00211D79 File Offset: 0x0020FF79
	protected override void Awake()
	{
		base.Awake();
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOkayPressed));
	}

	// Token: 0x060065D6 RID: 26070 RVA: 0x00211D9A File Offset: 0x0020FF9A
	public void RegisterOkayListener(StoreDoneWithBAM.ButtonPressedListener listener)
	{
		if (this.m_okayListeners.Contains(listener))
		{
			return;
		}
		this.m_okayListeners.Add(listener);
	}

	// Token: 0x060065D7 RID: 26071 RVA: 0x00211DB7 File Offset: 0x0020FFB7
	public void RemoveOkayListener(StoreDoneWithBAM.ButtonPressedListener listener)
	{
		this.m_okayListeners.Remove(listener);
	}

	// Token: 0x060065D8 RID: 26072 RVA: 0x00211DC8 File Offset: 0x0020FFC8
	private void OnOkayPressed(UIEvent e)
	{
		this.Hide(true);
		StoreDoneWithBAM.ButtonPressedListener[] array = this.m_okayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x04005470 RID: 21616
	public UIBButton m_okayButton;

	// Token: 0x04005471 RID: 21617
	public UberText m_headlineText;

	// Token: 0x04005472 RID: 21618
	public UberText m_messageText;

	// Token: 0x04005473 RID: 21619
	private List<StoreDoneWithBAM.ButtonPressedListener> m_okayListeners = new List<StoreDoneWithBAM.ButtonPressedListener>();

	// Token: 0x020022B8 RID: 8888
	// (Invoke) Token: 0x06012852 RID: 75858
	public delegate void ButtonPressedListener();
}
