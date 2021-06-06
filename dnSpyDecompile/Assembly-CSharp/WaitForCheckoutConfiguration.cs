using System;
using Blizzard.T5.Jobs;

// Token: 0x020008D4 RID: 2260
public class WaitForCheckoutConfiguration : IJobDependency, IAsyncJobResult
{
	// Token: 0x06007D6A RID: 32106 RVA: 0x0028B95C File Offset: 0x00289B5C
	public bool IsReady()
	{
		HearthstoneCheckout hearthstoneCheckout;
		return HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && (hearthstoneCheckout.HasProductCatalog && hearthstoneCheckout.HasClientID) && hearthstoneCheckout.HasCurrencyCode;
	}
}
