using System;
using System.Text;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.resources.v1;

namespace bgs
{
	// Token: 0x0200020D RID: 525
	public class ResourcesAPI : BattleNetAPI
	{
		// Token: 0x06002096 RID: 8342 RVA: 0x00075B6D File Offset: 0x00073D6D
		public ResourcesAPI(BattleNetCSharp battlenet) : base(battlenet, "ResourcesAPI")
		{
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00075B91 File Offset: 0x00073D91
		public ServiceDescriptor ResourcesService
		{
			get
			{
				return this.m_resourcesService;
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00075B99 File Offset: 0x00073D99
		public override void Initialize()
		{
			base.Initialize();
			base.ApiLog.LogDebug("Initializing");
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00075BB4 File Offset: 0x00073DB4
		private void ResouceLookupTestCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("Lookup failed");
				return;
			}
			int num = (int)userContext;
			base.ApiLog.LogDebug("Lookup done i={0} Region={1} Usage={2} SHA256={3}", new object[]
			{
				num,
				contentHandle.Region,
				contentHandle.Usage,
				contentHandle.Sha256Digest
			});
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00075C18 File Offset: 0x00073E18
		public void LookupResource(FourCC programId, FourCC streamId, FourCC locale, ResourcesAPI.ResourceLookupCallback cb, object userContext)
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
			RPCContext rpccontext = this.m_rpcConnection.QueueRequest(this.m_resourcesService, 1U, contentHandleRequest, new RPCContextDelegate(this.GetContentHandleCallback), 0U);
			ResourcesAPIPendingState resourcesAPIPendingState = new ResourcesAPIPendingState();
			resourcesAPIPendingState.Callback = cb;
			resourcesAPIPendingState.UserContext = userContext;
			this.m_pendingLookups.Add(rpccontext.Header.Token, resourcesAPIPendingState);
			base.ApiLog.LogDebug("Lookup request sent. PID={0} StreamID={1} Locale={2}", new object[]
			{
				programId,
				streamId,
				locale
			});
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00075CDC File Offset: 0x00073EDC
		private void GetContentHandleCallback(RPCContext context)
		{
			ResourcesAPIPendingState resourcesAPIPendingState = null;
			if (!this.m_pendingLookups.TryGetValue(context.Header.Token, out resourcesAPIPendingState))
			{
				base.ApiLog.LogWarning("Received unmatched lookup response");
				return;
			}
			this.m_pendingLookups.Remove(context.Header.Token);
			ContentHandle contentHandle = ContentHandle.ParseFrom(context.Payload);
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				base.ApiLog.LogWarning("Received invalid response");
				resourcesAPIPendingState.Callback(null, resourcesAPIPendingState.UserContext);
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Resources API C#: Failed lookup. Error={0}", new object[]
				{
					status
				});
				resourcesAPIPendingState.Callback(null, resourcesAPIPendingState.UserContext);
				return;
			}
			ContentHandle contentHandle2 = ContentHandle.FromProtocol(contentHandle);
			resourcesAPIPendingState.Callback(contentHandle2, resourcesAPIPendingState.UserContext);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00075DC4 File Offset: 0x00073FC4
		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BB6 RID: 2998
		private ServiceDescriptor m_resourcesService = new ResourcesService();

		// Token: 0x04000BB7 RID: 2999
		private Map<uint, ResourcesAPIPendingState> m_pendingLookups = new Map<uint, ResourcesAPIPendingState>();

		// Token: 0x020006B6 RID: 1718
		// (Invoke) Token: 0x06006251 RID: 25169
		public delegate void ResourceLookupCallback(ContentHandle contentHandle, object userContext);
	}
}
