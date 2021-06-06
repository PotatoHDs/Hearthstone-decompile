using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs
{
	// Token: 0x020001FA RID: 506
	public class AuthenticationAPI : BattleNetAPI
	{
		// Token: 0x06001F03 RID: 7939 RVA: 0x0006C817 File Offset: 0x0006AA17
		public AuthenticationAPI(BattleNetCSharp battlenet) : base(battlenet, "Authentication")
		{
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0006C851 File Offset: 0x0006AA51
		public ServiceDescriptor AuthServerService
		{
			get
			{
				return this.m_authServerService;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0006C859 File Offset: 0x0006AA59
		public ServiceDescriptor AuthClientService
		{
			get
			{
				return this.m_authClientService;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0006C861 File Offset: 0x0006AA61
		public bnet.protocol.EntityId GameAccountId
		{
			get
			{
				return this.m_gameAccount;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0006C869 File Offset: 0x0006AA69
		public bnet.protocol.EntityId AccountId
		{
			get
			{
				return this.m_accountEntity;
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0006C874 File Offset: 0x0006AA74
		public void GetQueueInfo(ref QueueInfo queueInfo)
		{
			queueInfo.position = this.m_queueInfo.position;
			queueInfo.end = this.m_queueInfo.end;
			queueInfo.stdev = this.m_queueInfo.stdev;
			queueInfo.changed = this.m_queueInfo.changed;
			this.m_queueInfo.changed = false;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0006C8D1 File Offset: 0x0006AAD1
		public AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest()
		{
			if (this.m_memModuleRequests.Count > 0)
			{
				return this.m_memModuleRequests.Dequeue();
			}
			return null;
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0006C8F0 File Offset: 0x0006AAF0
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 5U, new RPCContextDelegate(this.HandleLogonCompleteRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 10U, new RPCContextDelegate(this.HandleLogonUpdateRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 1U, new RPCContextDelegate(this.HandleLoadModuleRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 6U, new RPCContextDelegate(this.HandleLoadMemModuleRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 12U, new RPCContextDelegate(this.HandleLogonQueueUpdate));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 13U, new RPCContextDelegate(this.HandleLogonQueueEnd));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 14U, new RPCContextDelegate(this.HandleGameAccountSelected));
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0006CA05 File Offset: 0x0006AC05
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_memModuleRequests.Clear();
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0006CA18 File Offset: 0x0006AC18
		public bool AuthenticationFailure()
		{
			return this.m_authenticationFailure;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0006CA20 File Offset: 0x0006AC20
		public void VerifyWebCredentials(string token, RPCContextDelegate callback = null)
		{
			if (this.m_rpcConnection == null)
			{
				return;
			}
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = new VerifyWebCredentialsRequest();
			byte[] bytes = Encoding.UTF8.GetBytes(token);
			verifyWebCredentialsRequest.SetWebCredentials(bytes);
			this.m_rpcConnection.BeginAuth();
			this.m_rpcConnection.QueueRequest(this.AuthServerService, 7U, verifyWebCredentialsRequest, callback, 0U);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0006CA70 File Offset: 0x0006AC70
		private void HandleLogonCompleteRequest(RPCContext context)
		{
			LogonResult logonResult = LogonResult.ParseFrom(context.Payload);
			BattleNetErrors errorCode = (BattleNetErrors)logonResult.ErrorCode;
			if (errorCode != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_OnFinish, errorCode, context);
				return;
			}
			this.m_accountEntity = logonResult.AccountId;
			this.m_battleNet.Presence.PresenceSubscribe(this.m_accountEntity);
			this.m_gameAccounts = new List<bnet.protocol.EntityId>();
			foreach (bnet.protocol.EntityId entityId in logonResult.GameAccountIdList)
			{
				this.m_gameAccounts.Add(entityId);
				this.m_battleNet.Presence.PresenceSubscribe(entityId);
			}
			if (this.m_gameAccounts.Count > 0)
			{
				this.m_gameAccount = logonResult.GameAccountIdList[0];
			}
			this.m_battleNet.IssueSelectGameAccountRequest();
			this.m_battleNet.SetConnectedRegion(logonResult.ConnectedRegion);
			base.ApiLog.LogDebug("LogonComplete {0}", new object[]
			{
				logonResult
			});
			base.ApiLog.LogDebug("Region (connected): {0}", new object[]
			{
				logonResult.ConnectedRegion
			});
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0006CBA8 File Offset: 0x0006ADA8
		private void HandleLogonUpdateRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: LogonUpdate");
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0006CBBA File Offset: 0x0006ADBA
		private void HandleLoadModuleRequest(RPCContext context)
		{
			base.ApiLog.LogWarning("RPC Called: LoadModule");
			this.m_authenticationFailure = true;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0006CBD4 File Offset: 0x0006ADD4
		private void HandleLoadMemModuleRequest(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_MemModuleLoad, status, null);
				return;
			}
			AuthenticationAPI.MemModuleLoadRequest item = new AuthenticationAPI.MemModuleLoadRequest(context);
			this.m_memModuleRequests.Enqueue(item);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0006CC14 File Offset: 0x0006AE14
		public void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			MemModuleLoadResponse memModuleLoadResponse = new MemModuleLoadResponse();
			memModuleLoadResponse.SetData(memModuleResponseBytes);
			this.m_rpcConnection.QueueResponse(request.RpcContext, memModuleLoadResponse);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0006CC40 File Offset: 0x0006AE40
		private void HandleLogonQueueUpdate(RPCContext context)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = LogonQueueUpdateRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleLogonQueueUpdate : " + logonQueueUpdateRequest.ToString());
			long end = (long)(logonQueueUpdateRequest.EstimatedTime / 1000000UL - (ulong)this.m_battleNet.CurrentUTCServerTimeSeconds);
			this.SaveQueuePosition((int)logonQueueUpdateRequest.Position, end, (long)logonQueueUpdateRequest.EtaDeviationInSec, false);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0006CCA2 File Offset: 0x0006AEA2
		private void HandleLogonQueueEnd(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleLogonQueueEnd : ");
			this.SaveQueuePosition(0, 0L, 0L, true);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0006CCC0 File Offset: 0x0006AEC0
		private void HandleGameAccountSelected(RPCContext context)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = GameAccountSelectedRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleGameAccountSelected : " + gameAccountSelectedRequest.ToString());
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0006CCF4 File Offset: 0x0006AEF4
		public void SaveQueuePosition(int position, long end, long stdev, bool ended)
		{
			this.m_queueInfo.changed = (ended || position != this.m_queueInfo.position || end != this.m_queueInfo.end || stdev != this.m_queueInfo.stdev);
			this.m_queueInfo.position = position;
			this.m_queueInfo.end = end;
			this.m_queueInfo.stdev = stdev;
			base.ApiLog.LogDebug("LogonQueue changed={0} position={1} secondsTilEnd={2} minutes={3} stdev={4}", new object[]
			{
				this.m_queueInfo.changed,
				this.m_queueInfo.position,
				this.m_queueInfo.end,
				this.m_queueInfo.end / 60L,
				stdev
			});
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0006CDD4 File Offset: 0x0006AFD4
		public void GenerateSSOToken(Action<bool, string> callback)
		{
			if (callback == null)
			{
				return;
			}
			if (this.m_rpcConnection != null)
			{
				GenerateSSOTokenRequest generateSSOTokenRequest = new GenerateSSOTokenRequest();
				generateSSOTokenRequest.Program = 1465140039U;
				if (this.m_rpcConnection.QueueRequest(this.AuthServerService, 5U, generateSSOTokenRequest, delegate(RPCContext receivedContext)
				{
					GenerateSSOTokenResponse generateSSOTokenResponse = GenerateSSOTokenResponse.ParseFrom(receivedContext.Payload);
					string text = null;
					if (generateSSOTokenResponse.HasSsoId)
					{
						text = this.ParseSSOToken(generateSSOTokenResponse.SsoId);
					}
					this.ApiLog.LogDebug("[GenerateSSOToken] SSO Token Response - {0}", new object[]
					{
						text ?? "NULL"
					});
					if (callback != null)
					{
						callback(generateSSOTokenResponse.HasSsoSecret, text);
					}
				}, 0U) != null)
				{
					return;
				}
			}
			callback(false, null);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0006CE48 File Offset: 0x0006B048
		public void GenerateWebCredentials(Action<bool, string> callback, uint programId)
		{
			if (callback == null)
			{
				return;
			}
			if (this.m_rpcConnection != null)
			{
				GenerateWebCredentialsRequest generateWebCredentialsRequest = new GenerateWebCredentialsRequest();
				generateWebCredentialsRequest.Program = programId;
				if (this.m_rpcConnection.QueueRequest(this.AuthServerService, 8U, generateWebCredentialsRequest, delegate(RPCContext receivedContext)
				{
					GenerateWebCredentialsResponse generateWebCredentialsResponse = GenerateWebCredentialsResponse.ParseFrom(receivedContext.Payload);
					string text = null;
					if (generateWebCredentialsResponse.HasWebCredentials)
					{
						text = Encoding.UTF8.GetString(generateWebCredentialsResponse.WebCredentials);
					}
					this.ApiLog.LogDebug("[GenerateWebCredentials] Web Credentials Response - {0}", new object[]
					{
						text ?? "NULL"
					});
					if (callback != null)
					{
						callback(generateWebCredentialsResponse.HasWebCredentials, text);
					}
				}, 0U) != null)
				{
					return;
				}
			}
			callback(false, null);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0006CEB7 File Offset: 0x0006B0B7
		public void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			this.GenerateWebCredentials(callback, 1465140039U);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006CEC5 File Offset: 0x0006B0C5
		public void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			this.GenerateWebCredentials(callback, 4288624U);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0006CED4 File Offset: 0x0006B0D4
		private string ParseSSOToken(byte[] bytes)
		{
			this.tokenBuilder.Length = 0;
			AuthenticationAPI.SSOTokenParseSection ssotokenParseSection = AuthenticationAPI.SSOTokenParseSection.Initial;
			int num = 0;
			int num2 = 0;
			while (num2 < bytes.Length && ssotokenParseSection != AuthenticationAPI.SSOTokenParseSection.Finished)
			{
				char c = (char)bytes[num2];
				switch (ssotokenParseSection)
				{
				case AuthenticationAPI.SSOTokenParseSection.Initial:
					if (c == '\t')
					{
						num2++;
						ssotokenParseSection = AuthenticationAPI.SSOTokenParseSection.Region;
					}
					break;
				case AuthenticationAPI.SSOTokenParseSection.Region:
				case AuthenticationAPI.SSOTokenParseSection.Token:
					this.tokenBuilder.Append(c);
					if (c == '-')
					{
						ssotokenParseSection++;
					}
					break;
				case AuthenticationAPI.SSOTokenParseSection.PlayerID:
					this.tokenBuilder.Append(c);
					num++;
					if (num == 9)
					{
						ssotokenParseSection = AuthenticationAPI.SSOTokenParseSection.Finished;
					}
					break;
				}
				num2++;
			}
			return this.tokenBuilder.ToString();
		}

		// Token: 0x04000B4E RID: 2894
		private ServiceDescriptor m_authServerService = new AuthServerService();

		// Token: 0x04000B4F RID: 2895
		private ServiceDescriptor m_authClientService = new AuthClientService();

		// Token: 0x04000B50 RID: 2896
		private QueueInfo m_queueInfo;

		// Token: 0x04000B51 RID: 2897
		private StringBuilder tokenBuilder = new StringBuilder();

		// Token: 0x04000B52 RID: 2898
		private List<bnet.protocol.EntityId> m_gameAccounts;

		// Token: 0x04000B53 RID: 2899
		private bnet.protocol.EntityId m_accountEntity;

		// Token: 0x04000B54 RID: 2900
		private bnet.protocol.EntityId m_gameAccount;

		// Token: 0x04000B55 RID: 2901
		private bool m_authenticationFailure;

		// Token: 0x04000B56 RID: 2902
		private Queue<AuthenticationAPI.MemModuleLoadRequest> m_memModuleRequests = new Queue<AuthenticationAPI.MemModuleLoadRequest>();

		// Token: 0x04000B57 RID: 2903
		private const uint WTCGProgramID = 1465140039U;

		// Token: 0x04000B58 RID: 2904
		private const uint AppProgramID = 4288624U;

		// Token: 0x0200067D RID: 1661
		public class MemModuleLoadRequest
		{
			// Token: 0x1700128A RID: 4746
			// (get) Token: 0x060061E4 RID: 25060 RVA: 0x00127EE0 File Offset: 0x001260E0
			public RPCContext RpcContext
			{
				get
				{
					return this.m_rpcContext;
				}
			}

			// Token: 0x060061E5 RID: 25061 RVA: 0x00127EE8 File Offset: 0x001260E8
			public MemModuleLoadRequest(RPCContext rpcContext)
			{
				bnet.protocol.authentication.v1.MemModuleLoadRequest memModuleLoadRequest = bnet.protocol.authentication.v1.MemModuleLoadRequest.ParseFrom(rpcContext.Payload);
				this.m_rpcContext = rpcContext;
				this.m_contentHandle = ContentHandle.FromProtocol(memModuleLoadRequest.Handle);
				this.m_key = memModuleLoadRequest.Key;
				this.m_input = memModuleLoadRequest.Input;
			}

			// Token: 0x04002192 RID: 8594
			public ContentHandle m_contentHandle;

			// Token: 0x04002193 RID: 8595
			public byte[] m_key;

			// Token: 0x04002194 RID: 8596
			public byte[] m_input;

			// Token: 0x04002195 RID: 8597
			private RPCContext m_rpcContext;
		}

		// Token: 0x0200067E RID: 1662
		private enum SSOTokenParseSection
		{
			// Token: 0x04002197 RID: 8599
			Initial,
			// Token: 0x04002198 RID: 8600
			Region,
			// Token: 0x04002199 RID: 8601
			Token,
			// Token: 0x0400219A RID: 8602
			PlayerID,
			// Token: 0x0400219B RID: 8603
			Finished
		}
	}
}
