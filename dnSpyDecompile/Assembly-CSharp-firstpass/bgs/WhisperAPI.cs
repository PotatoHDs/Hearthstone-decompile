using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	// Token: 0x0200020F RID: 527
	public class WhisperAPI : BattleNetAPI
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x00076127 File Offset: 0x00074327
		public WhisperAPI(BattleNetCSharp battlenet) : base(battlenet, "Whisper")
		{
			this.m_battleNetCSharp = battlenet;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00076148 File Offset: 0x00074348
		public void OnWhisper(Notification notification)
		{
			BnetWhisper bnetWhisper = this.CreateWhisperFromNotification(notification);
			if (bnetWhisper == null || string.IsNullOrEmpty(bnetWhisper.GetMessage()))
			{
				return;
			}
			this.m_whispers.Add(bnetWhisper);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0007617C File Offset: 0x0007437C
		public void OnWhisperEcho(Notification notification)
		{
			BnetWhisper bnetWhisper = this.CreateWhisperFromNotification(notification);
			if (bnetWhisper == null || string.IsNullOrEmpty(bnetWhisper.GetMessage()))
			{
				return;
			}
			BnetGameAccountId speakerId = BnetGameAccountId.CreateFromProtocol(this.m_battleNetCSharp.GameAccountId);
			bnetWhisper.SetSpeakerId(speakerId);
			this.m_whispers.Add(bnetWhisper);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000761C8 File Offset: 0x000743C8
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
				bnet.protocol.Attribute attribute = notification.Attribute[i];
				if (attribute.Name == "whisper")
				{
					bnetWhisper.SetMessage(attribute.Value.StringValue);
				}
			}
			bnetWhisper.SetTimestampMilliseconds(TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds);
			return bnetWhisper;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00076274 File Offset: 0x00074474
		public void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			Notification notification = new Notification();
			notification.SetType("WHISPER");
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(gameAccount.GetLo());
			entityId.SetHigh(gameAccount.GetHi());
			notification.SetTargetId(entityId);
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			attribute.SetName("whisper");
			Variant variant = new Variant();
			variant.SetStringValue(message);
			attribute.SetValue(variant);
			notification.AddAttribute(attribute);
			this.m_rpcConnection.QueueRequest(this.m_battleNet.NotificationService, 1U, notification, new RPCContextDelegate(this.WhisperSentCallback), 0U);
			BnetGameAccountId speakerId = BnetGameAccountId.CreateFromEntityId(BattleNet.GetMyGameAccountId());
			BnetWhisper bnetWhisper = new BnetWhisper();
			bnetWhisper.SetSpeakerId(speakerId);
			bnetWhisper.SetReceiverId(gameAccount);
			bnetWhisper.SetMessage(message);
			bnetWhisper.SetTimestampMilliseconds(TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds);
			this.m_whispers.Add(bnetWhisper);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0007636A File Offset: 0x0007456A
		public void GetWhisperInfo(ref WhisperInfo info)
		{
			info.whisperSize = this.m_whispers.Count;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0007637D File Offset: 0x0007457D
		public void GetWhispers([Out] BnetWhisper[] whispers)
		{
			this.m_whispers.CopyTo(whispers, 0);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0007638C File Offset: 0x0007458C
		public void ClearWhispers()
		{
			this.m_whispers.Clear();
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0007639C File Offset: 0x0007459C
		private void WhisperSentCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Whisper API C#: Failed to SendWhisper. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Whisper, BnetFeatureEvent.Whisper_OnSend, status, context);
			}
		}

		// Token: 0x04000BBC RID: 3004
		private BattleNetCSharp m_battleNetCSharp;

		// Token: 0x04000BBD RID: 3005
		private List<BnetWhisper> m_whispers = new List<BnetWhisper>();
	}
}
