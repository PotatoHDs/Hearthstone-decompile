using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	// Token: 0x02001076 RID: 4214
	public class HttpEventListener
	{
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x0600B608 RID: 46600 RVA: 0x0037DA9C File Offset: 0x0037BC9C
		// (remove) Token: 0x0600B609 RID: 46601 RVA: 0x0037DAD4 File Offset: 0x0037BCD4
		public event HttpEventListener.CommerceHttpEvent CommerceHttpNeedsAuth;

		// Token: 0x0600B60A RID: 46602 RVA: 0x0037DB0C File Offset: 0x0037BD0C
		public void EventReceived(blz_commerce_http_event_t httpEvent)
		{
			blz_commerce_http_type_t http_type = httpEvent.http_type;
			if (http_type != blz_commerce_http_type_t.BLZ_COMMERCE_HTTP_NEEDS_AUTH)
			{
				Log.Store.PrintError("Http Event is not of a recognized type! ({0})", new object[]
				{
					httpEvent.http_type.ToString()
				});
				return;
			}
			HttpEventListener.CommerceHttpEvent commerceHttpNeedsAuth = this.CommerceHttpNeedsAuth;
			if (commerceHttpNeedsAuth == null)
			{
				return;
			}
			commerceHttpNeedsAuth(httpEvent);
		}

		// Token: 0x02002876 RID: 10358
		// (Invoke) Token: 0x06013BF1 RID: 80881
		public delegate void CommerceHttpEvent(blz_commerce_http_event_t httpEvent);
	}
}
