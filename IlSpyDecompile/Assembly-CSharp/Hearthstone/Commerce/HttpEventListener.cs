using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	public class HttpEventListener
	{
		public delegate void CommerceHttpEvent(blz_commerce_http_event_t httpEvent);

		public event CommerceHttpEvent CommerceHttpNeedsAuth;

		public void EventReceived(blz_commerce_http_event_t httpEvent)
		{
			blz_commerce_http_type_t http_type = httpEvent.http_type;
			if (http_type == blz_commerce_http_type_t.BLZ_COMMERCE_HTTP_NEEDS_AUTH)
			{
				this.CommerceHttpNeedsAuth?.Invoke(httpEvent);
				return;
			}
			Log.Store.PrintError("Http Event is not of a recognized type! ({0})", httpEvent.http_type.ToString());
		}
	}
}
