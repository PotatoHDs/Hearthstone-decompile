using System;

namespace Hearthstone.InGameMessage.UI
{
	// Token: 0x02001163 RID: 4451
	public class MessageUIData
	{
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x0600C2E1 RID: 49889 RVA: 0x003B0452 File Offset: 0x003AE652
		// (set) Token: 0x0600C2E2 RID: 49890 RVA: 0x003B045A File Offset: 0x003AE65A
		public string UID { get; set; }

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600C2E3 RID: 49891 RVA: 0x003B0463 File Offset: 0x003AE663
		// (set) Token: 0x0600C2E4 RID: 49892 RVA: 0x003B046B File Offset: 0x003AE66B
		public MessageContentType ContentType { get; set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600C2E5 RID: 49893 RVA: 0x003B0474 File Offset: 0x003AE674
		public UIMessageCallbacks Callbacks { get; } = new UIMessageCallbacks();

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600C2E6 RID: 49894 RVA: 0x003B047C File Offset: 0x003AE67C
		// (set) Token: 0x0600C2E7 RID: 49895 RVA: 0x003B0484 File Offset: 0x003AE684
		public object MessageData { get; set; }

		// Token: 0x0600C2E8 RID: 49896 RVA: 0x003B0490 File Offset: 0x003AE690
		public void CopyValues(MessageUIData newData)
		{
			this.ContentType = newData.ContentType;
			this.Callbacks.OnShown = newData.Callbacks.OnShown;
			this.Callbacks.OnClosed = newData.Callbacks.OnClosed;
			this.Callbacks.OnStoreOpened = newData.Callbacks.OnStoreOpened;
			this.MessageData = newData.MessageData;
		}
	}
}
