using System;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001159 RID: 4441
	public class MessageContentFeed
	{
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600C2AA RID: 49834 RVA: 0x003AFB2B File Offset: 0x003ADD2B
		// (set) Token: 0x0600C2AB RID: 49835 RVA: 0x003AFB33 File Offset: 0x003ADD33
		public string ContentId { get; set; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600C2AC RID: 49836 RVA: 0x003AFB3C File Offset: 0x003ADD3C
		// (set) Token: 0x0600C2AD RID: 49837 RVA: 0x003AFB44 File Offset: 0x003ADD44
		public IDataTranslator DataTranslator { get; set; }
	}
}
