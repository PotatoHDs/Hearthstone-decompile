using Blizzard.T5.Jobs;

public class WaitForCheckoutConfiguration : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service))
		{
			if (service.HasProductCatalog && service.HasClientID)
			{
				return service.HasCurrencyCode;
			}
			return false;
		}
		return false;
	}
}
