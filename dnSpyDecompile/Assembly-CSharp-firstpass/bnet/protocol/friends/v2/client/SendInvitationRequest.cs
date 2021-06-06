using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000404 RID: 1028
	public class SendInvitationRequest : IProtoBuf
	{
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x000D738F File Offset: 0x000D558F
		// (set) Token: 0x06004446 RID: 17478 RVA: 0x000D7397 File Offset: 0x000D5597
		public SendInvitationTarget Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
				this.HasTarget = (value != null);
			}
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000D73AA File Offset: 0x000D55AA
		public void SetTarget(SendInvitationTarget val)
		{
			this.Target = val;
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x000D73B3 File Offset: 0x000D55B3
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x000D73BB File Offset: 0x000D55BB
		public SendInvitationOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x000D73CE File Offset: 0x000D55CE
		public void SetOptions(SendInvitationOptions val)
		{
			this.Options = val;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x000D73D8 File Offset: 0x000D55D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000D7420 File Offset: 0x000D5620
		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			return sendInvitationRequest != null && this.HasTarget == sendInvitationRequest.HasTarget && (!this.HasTarget || this.Target.Equals(sendInvitationRequest.Target)) && this.HasOptions == sendInvitationRequest.HasOptions && (!this.HasOptions || this.Options.Equals(sendInvitationRequest.Options));
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000D7490 File Offset: 0x000D5690
		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x000D749A File Offset: 0x000D569A
		public void Deserialize(Stream stream)
		{
			SendInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000D74A4 File Offset: 0x000D56A4
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return SendInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x000D74B0 File Offset: 0x000D56B0
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationRequest.DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x000D74CC File Offset: 0x000D56CC
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x000D74F4 File Offset: 0x000D56F4
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 18)
				{
					if (num != 26)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Options == null)
					{
						instance.Options = SendInvitationOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SendInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.Target == null)
				{
					instance.Target = SendInvitationTarget.DeserializeLengthDelimited(stream);
				}
				else
				{
					SendInvitationTarget.DeserializeLengthDelimited(stream, instance.Target);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x000D75C6 File Offset: 0x000D57C6
		public void Serialize(Stream stream)
		{
			SendInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x000D75D0 File Offset: 0x000D57D0
		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				SendInvitationTarget.Serialize(stream, instance.Target);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SendInvitationOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x000D7638 File Offset: 0x000D5838
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget)
			{
				num += 1U;
				uint serializedSize = this.Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400171E RID: 5918
		public bool HasTarget;

		// Token: 0x0400171F RID: 5919
		private SendInvitationTarget _Target;

		// Token: 0x04001720 RID: 5920
		public bool HasOptions;

		// Token: 0x04001721 RID: 5921
		private SendInvitationOptions _Options;
	}
}
