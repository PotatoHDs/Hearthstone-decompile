using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	public class WhisperAPI : BattleNetAPI
	{
		private BattleNetCSharp m_battleNetCSharp;

		private List<BnetWhisper> m_whispers = new List<BnetWhisper>();

		public WhisperAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Whisper")
		{
			m_battleNetCSharp = battlenet;
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public void OnWhisper(Notification notification)
		{
			BnetWhisper bnetWhisper = CreateWhisperFromNotification(notification);
			if (bnetWhisper != null && !string.IsNullOrEmpty(bnetWhisper.GetMessage()))
			{
				m_whispers.Add(bnetWhisper);
			}
		}

		public void OnWhisperEcho(Notification notification)
		{
			BnetWhisper bnetWhisper = CreateWhisperFromNotification(notification);
			if (bnetWhisper != null && !string.IsNullOrEmpty(bnetWhisper.GetMessage()))
			{
				BnetGameAccountId speakerId = BnetGameAccountId.CreateFromProtocol(m_battleNetCSharp.GameAccountId);
				bnetWhisper.SetSpeakerId(speakerId);
				m_whispers.Add(bnetWhisper);
			}
		}

		public BnetWhisper CreateWhisperFromNotification(Notification notification)
		{
			if (!notification.HasSenderId)
			{
				return null;
			}
			if (notification.AttributeCount <= 0)
			{
				return null;
			}
			BnetWhisper bnetWhisper = new BnetWhisper();
			bnetWhisper.SetSpeakerId(BnetGameAccountId.CreateFromProtocol(notification.SenderId));
			bnetWhisper.SetReceiverId(BnetGameAccountId.CreateFromProtocol(notification.TargetId));
			for (int i = 0; i < notification.AttributeCount; i++)
			{
				Attribute attribute2 = notification.Attribute[i];
				if (attribute2.Name == "whisper")
				{
					bnetWhisper.SetMessage(attribute2.Value.StringValue);
				}
			}
			bnetWhisper.SetTimestampMilliseconds(TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds);
			return bnetWhisper;
		}

		public void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				Notification notification = new Notification();
				notification.SetType("WHISPER");
				bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
				entityId.SetLow(gameAccount.GetLo());
				entityId.SetHigh(gameAccount.GetHi());
				notification.SetTargetId(entityId);
				Attribute attribute2 = new Attribute();
				attribute2.SetName("whisper");
				Variant variant = new Variant();
				variant.SetStringValue(message);
				attribute2.SetValue(variant);
				notification.AddAttribute(attribute2);
				m_rpcConnection.QueueRequest(m_battleNet.NotificationService, 1u, notification, WhisperSentCallback);
				BnetGameAccountId speakerId = BnetGameAccountId.CreateFromEntityId(BattleNet.GetMyGameAccountId());
				BnetWhisper bnetWhisper = new BnetWhisper();
				bnetWhisper.SetSpeakerId(speakerId);
				bnetWhisper.SetReceiverId(gameAccount);
				bnetWhisper.SetMessage(message);
				bnetWhisper.SetTimestampMilliseconds(TimeUtils.GetElapsedTimeSinceEpoch().TotalMilliseconds);
				m_whispers.Add(bnetWhisper);
			}
		}

		public void GetWhisperInfo(ref WhisperInfo info)
		{
			info.whisperSize = m_whispers.Count;
		}

		public void GetWhispers([Out] BnetWhisper[] whispers)
		{
			m_whispers.CopyTo(whispers, 0);
		}

		public void ClearWhispers()
		{
			m_whispers.Clear();
		}

		private void WhisperSentCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				base.ApiLog.LogWarning("Battle.net Whisper API C#: Failed to SendWhisper. " + status);
				m_battleNet.EnqueueErrorInfo(BnetFeature.Whisper, BnetFeatureEvent.Whisper_OnSend, status, context);
			}
		}
	}
}
