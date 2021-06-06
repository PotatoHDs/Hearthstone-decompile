using System.Text;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.resources.v1;

namespace bgs
{
	public class ResourcesAPI : BattleNetAPI
	{
		public delegate void ResourceLookupCallback(ContentHandle contentHandle, object userContext);

		private ServiceDescriptor m_resourcesService = new ResourcesService();

		private Map<uint, ResourcesAPIPendingState> m_pendingLookups = new Map<uint, ResourcesAPIPendingState>();

		public ServiceDescriptor ResourcesService => m_resourcesService;

		public ResourcesAPI(BattleNetCSharp battlenet)
			: base(battlenet, "ResourcesAPI")
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			base.ApiLog.LogDebug("Initializing");
		}

		private void ResouceLookupTestCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("Lookup failed");
				return;
			}
			int num = (int)userContext;
			base.ApiLog.LogDebug("Lookup done i={0} Region={1} Usage={2} SHA256={3}", num, contentHandle.Region, contentHandle.Usage, contentHandle.Sha256Digest);
		}

		public void LookupResource(FourCC programId, FourCC streamId, FourCC locale, ResourceLookupCallback cb, object userContext)
		{
			ContentHandleRequest contentHandleRequest = new ContentHandleRequest();
			contentHandleRequest.SetProgram(programId.GetValue());
			contentHandleRequest.SetStream(streamId.GetValue());
			contentHandleRequest.SetVersion(locale.GetValue());
			if (contentHandleRequest == null || !contentHandleRequest.IsInitialized)
			{
				base.ApiLog.LogWarning("Unable to create request for RPC call.");
				return;
			}
			RPCContext rPCContext = m_rpcConnection.QueueRequest(m_resourcesService, 1u, contentHandleRequest, GetContentHandleCallback);
			ResourcesAPIPendingState resourcesAPIPendingState = new ResourcesAPIPendingState();
			resourcesAPIPendingState.Callback = cb;
			resourcesAPIPendingState.UserContext = userContext;
			m_pendingLookups.Add(rPCContext.Header.Token, resourcesAPIPendingState);
			base.ApiLog.LogDebug("Lookup request sent. PID={0} StreamID={1} Locale={2}", programId, streamId, locale);
		}

		private void GetContentHandleCallback(RPCContext context)
		{
			ResourcesAPIPendingState value = null;
			if (!m_pendingLookups.TryGetValue(context.Header.Token, out value))
			{
				base.ApiLog.LogWarning("Received unmatched lookup response");
				return;
			}
			m_pendingLookups.Remove(context.Header.Token);
			bnet.protocol.ContentHandle contentHandle = bnet.protocol.ContentHandle.ParseFrom(context.Payload);
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				base.ApiLog.LogWarning("Received invalid response");
				value.Callback(null, value.UserContext);
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				base.ApiLog.LogWarning("Battle.net Resources API C#: Failed lookup. Error={0}", status);
				value.Callback(null, value.UserContext);
			}
			else
			{
				ContentHandle contentHandle2 = ContentHandle.FromProtocol(contentHandle);
				value.Callback(contentHandle2, value.UserContext);
			}
		}

		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}
	}
}
