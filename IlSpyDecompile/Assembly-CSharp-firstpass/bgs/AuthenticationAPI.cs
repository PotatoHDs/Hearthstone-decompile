using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.authentication.v1;

namespace bgs
{
	public class AuthenticationAPI : BattleNetAPI
	{
		public class MemModuleLoadRequest
		{
			public ContentHandle m_contentHandle;

			public byte[] m_key;

			public byte[] m_input;

			private RPCContext m_rpcContext;

			public RPCContext RpcContext => m_rpcContext;

			public MemModuleLoadRequest(RPCContext rpcContext)
			{
				bnet.protocol.authentication.v1.MemModuleLoadRequest memModuleLoadRequest = bnet.protocol.authentication.v1.MemModuleLoadRequest.ParseFrom(rpcContext.Payload);
				m_rpcContext = rpcContext;
				m_contentHandle = ContentHandle.FromProtocol(memModuleLoadRequest.Handle);
				m_key = memModuleLoadRequest.Key;
				m_input = memModuleLoadRequest.Input;
			}
		}

		private enum SSOTokenParseSection
		{
			Initial,
			Region,
			Token,
			PlayerID,
			Finished
		}

		private ServiceDescriptor m_authServerService = new AuthServerService();

		private ServiceDescriptor m_authClientService = new AuthClientService();

		private QueueInfo m_queueInfo;

		private StringBuilder tokenBuilder = new StringBuilder();

		private List<bnet.protocol.EntityId> m_gameAccounts;

		private bnet.protocol.EntityId m_accountEntity;

		private bnet.protocol.EntityId m_gameAccount;

		private bool m_authenticationFailure;

		private Queue<MemModuleLoadRequest> m_memModuleRequests = new Queue<MemModuleLoadRequest>();

		private const uint WTCGProgramID = 1465140039u;

		private const uint AppProgramID = 4288624u;

		public ServiceDescriptor AuthServerService => m_authServerService;

		public ServiceDescriptor AuthClientService => m_authClientService;

		public bnet.protocol.EntityId GameAccountId => m_gameAccount;

		public bnet.protocol.EntityId AccountId => m_accountEntity;

		public AuthenticationAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Authentication")
		{
		}

		public void GetQueueInfo(ref QueueInfo queueInfo)
		{
			queueInfo.position = m_queueInfo.position;
			queueInfo.end = m_queueInfo.end;
			queueInfo.stdev = m_queueInfo.stdev;
			queueInfo.changed = m_queueInfo.changed;
			m_queueInfo.changed = false;
		}

		public MemModuleLoadRequest NextMemModuleRequest()
		{
			if (m_memModuleRequests.Count > 0)
			{
				return m_memModuleRequests.Dequeue();
			}
			return null;
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 5u, HandleLogonCompleteRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 10u, HandleLogonUpdateRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 1u, HandleLoadModuleRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 6u, HandleLoadMemModuleRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 12u, HandleLogonQueueUpdate);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 13u, HandleLogonQueueEnd);
			m_rpcConnection.RegisterServiceMethodListener(m_authClientService.Id, 14u, HandleGameAccountSelected);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			m_memModuleRequests.Clear();
		}

		public bool AuthenticationFailure()
		{
			return m_authenticationFailure;
		}

		public void VerifyWebCredentials(string token, RPCContextDelegate callback = null)
		{
			if (m_rpcConnection != null)
			{
				VerifyWebCredentialsRequest verifyWebCredentialsRequest = new VerifyWebCredentialsRequest();
				byte[] bytes = Encoding.UTF8.GetBytes(token);
				verifyWebCredentialsRequest.SetWebCredentials(bytes);
				m_rpcConnection.BeginAuth();
				m_rpcConnection.QueueRequest(AuthServerService, 7u, verifyWebCredentialsRequest, callback);
			}
		}

		private void HandleLogonCompleteRequest(RPCContext context)
		{
			LogonResult logonResult = LogonResult.ParseFrom(context.Payload);
			BattleNetErrors errorCode = (BattleNetErrors)logonResult.ErrorCode;
			if (errorCode != 0)
			{
				m_battleNet.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_OnFinish, errorCode, context);
				return;
			}
			m_accountEntity = logonResult.AccountId;
			m_battleNet.Presence.PresenceSubscribe(m_accountEntity);
			m_gameAccounts = new List<bnet.protocol.EntityId>();
			foreach (bnet.protocol.EntityId gameAccountId in logonResult.GameAccountIdList)
			{
				m_gameAccounts.Add(gameAccountId);
				m_battleNet.Presence.PresenceSubscribe(gameAccountId);
			}
			if (m_gameAccounts.Count > 0)
			{
				m_gameAccount = logonResult.GameAccountIdList[0];
			}
			m_battleNet.IssueSelectGameAccountRequest();
			m_battleNet.SetConnectedRegion(logonResult.ConnectedRegion);
			base.ApiLog.LogDebug("LogonComplete {0}", logonResult);
			base.ApiLog.LogDebug("Region (connected): {0}", logonResult.ConnectedRegion);
		}

		private void HandleLogonUpdateRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: LogonUpdate");
		}

		private void HandleLoadModuleRequest(RPCContext context)
		{
			base.ApiLog.LogWarning("RPC Called: LoadModule");
			m_authenticationFailure = true;
		}

		private void HandleLoadMemModuleRequest(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				m_battleNet.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_MemModuleLoad, status);
				return;
			}
			MemModuleLoadRequest item = new MemModuleLoadRequest(context);
			m_memModuleRequests.Enqueue(item);
		}

		public void SendMemModuleResponse(MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			MemModuleLoadResponse memModuleLoadResponse = new MemModuleLoadResponse();
			memModuleLoadResponse.SetData(memModuleResponseBytes);
			m_rpcConnection.QueueResponse(request.RpcContext, memModuleLoadResponse);
		}

		private void HandleLogonQueueUpdate(RPCContext context)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = LogonQueueUpdateRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleLogonQueueUpdate : " + logonQueueUpdateRequest.ToString());
			long end = (long)logonQueueUpdateRequest.EstimatedTime / 1000000L - m_battleNet.CurrentUTCServerTimeSeconds;
			SaveQueuePosition((int)logonQueueUpdateRequest.Position, end, (long)logonQueueUpdateRequest.EtaDeviationInSec, ended: false);
		}

		private void HandleLogonQueueEnd(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleLogonQueueEnd : ");
			SaveQueuePosition(0, 0L, 0L, ended: true);
		}

		private void HandleGameAccountSelected(RPCContext context)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = GameAccountSelectedRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleGameAccountSelected : " + gameAccountSelectedRequest.ToString());
		}

		public void SaveQueuePosition(int position, long end, long stdev, bool ended)
		{
			m_queueInfo.changed = ended || position != m_queueInfo.position || end != m_queueInfo.end || stdev != m_queueInfo.stdev;
			m_queueInfo.position = position;
			m_queueInfo.end = end;
			m_queueInfo.stdev = stdev;
			base.ApiLog.LogDebug("LogonQueue changed={0} position={1} secondsTilEnd={2} minutes={3} stdev={4}", m_queueInfo.changed, m_queueInfo.position, m_queueInfo.end, m_queueInfo.end / 60, stdev);
		}

		public void GenerateSSOToken(Action<bool, string> callback)
		{
			if (callback == null)
			{
				return;
			}
			if (m_rpcConnection != null)
			{
				GenerateSSOTokenRequest generateSSOTokenRequest = new GenerateSSOTokenRequest();
				generateSSOTokenRequest.Program = 1465140039u;
				if (m_rpcConnection.QueueRequest(AuthServerService, 5u, generateSSOTokenRequest, delegate(RPCContext receivedContext)
				{
					GenerateSSOTokenResponse generateSSOTokenResponse = GenerateSSOTokenResponse.ParseFrom(receivedContext.Payload);
					string text = null;
					if (generateSSOTokenResponse.HasSsoId)
					{
						text = ParseSSOToken(generateSSOTokenResponse.SsoId);
					}
					base.ApiLog.LogDebug("[GenerateSSOToken] SSO Token Response - {0}", text ?? "NULL");
					if (callback != null)
					{
						callback(generateSSOTokenResponse.HasSsoSecret, text);
					}
				}) != null)
				{
					return;
				}
			}
			callback(arg1: false, null);
		}

		public void GenerateWebCredentials(Action<bool, string> callback, uint programId)
		{
			if (callback == null)
			{
				return;
			}
			if (m_rpcConnection != null)
			{
				GenerateWebCredentialsRequest generateWebCredentialsRequest = new GenerateWebCredentialsRequest();
				generateWebCredentialsRequest.Program = programId;
				if (m_rpcConnection.QueueRequest(AuthServerService, 8u, generateWebCredentialsRequest, delegate(RPCContext receivedContext)
				{
					GenerateWebCredentialsResponse generateWebCredentialsResponse = GenerateWebCredentialsResponse.ParseFrom(receivedContext.Payload);
					string text = null;
					if (generateWebCredentialsResponse.HasWebCredentials)
					{
						text = Encoding.UTF8.GetString(generateWebCredentialsResponse.WebCredentials);
					}
					base.ApiLog.LogDebug("[GenerateWebCredentials] Web Credentials Response - {0}", text ?? "NULL");
					if (callback != null)
					{
						callback(generateWebCredentialsResponse.HasWebCredentials, text);
					}
				}) != null)
				{
					return;
				}
			}
			callback(arg1: false, null);
		}

		public void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			GenerateWebCredentials(callback, 1465140039u);
		}

		public void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			GenerateWebCredentials(callback, 4288624u);
		}

		private string ParseSSOToken(byte[] bytes)
		{
			tokenBuilder.Length = 0;
			SSOTokenParseSection sSOTokenParseSection = SSOTokenParseSection.Initial;
			int num = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				if (sSOTokenParseSection == SSOTokenParseSection.Finished)
				{
					break;
				}
				char c = (char)bytes[i];
				switch (sSOTokenParseSection)
				{
				case SSOTokenParseSection.Initial:
					if (c == '\t')
					{
						i++;
						sSOTokenParseSection = SSOTokenParseSection.Region;
					}
					break;
				case SSOTokenParseSection.Region:
				case SSOTokenParseSection.Token:
					tokenBuilder.Append(c);
					if (c == '-')
					{
						sSOTokenParseSection++;
					}
					break;
				case SSOTokenParseSection.PlayerID:
					tokenBuilder.Append(c);
					num++;
					if (num == 9)
					{
						sSOTokenParseSection = SSOTokenParseSection.Finished;
					}
					break;
				}
			}
			return tokenBuilder.ToString();
		}
	}
}
