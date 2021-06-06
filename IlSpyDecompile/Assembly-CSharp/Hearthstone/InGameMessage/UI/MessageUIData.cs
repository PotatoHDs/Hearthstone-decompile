namespace Hearthstone.InGameMessage.UI
{
	public class MessageUIData
	{
		public string UID { get; set; }

		public MessageContentType ContentType { get; set; }

		public UIMessageCallbacks Callbacks { get; } = new UIMessageCallbacks();


		public object MessageData { get; set; }

		public void CopyValues(MessageUIData newData)
		{
			ContentType = newData.ContentType;
			Callbacks.OnShown = newData.Callbacks.OnShown;
			Callbacks.OnClosed = newData.Callbacks.OnClosed;
			Callbacks.OnStoreOpened = newData.Callbacks.OnStoreOpened;
			MessageData = newData.MessageData;
		}
	}
}
