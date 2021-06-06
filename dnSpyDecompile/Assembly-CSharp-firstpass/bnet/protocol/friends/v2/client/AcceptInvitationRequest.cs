using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000407 RID: 1031
	public class AcceptInvitationRequest : IProtoBuf
	{
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x000D794A File Offset: 0x000D5B4A
		// (set) Token: 0x06004476 RID: 17526 RVA: 0x000D7952 File Offset: 0x000D5B52
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000D7962 File Offset: 0x000D5B62
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x000D796B File Offset: 0x000D5B6B
		// (set) Token: 0x06004479 RID: 17529 RVA: 0x000D7973 File Offset: 0x000D5B73
		public AcceptInvitationOptions Options
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

		// Token: 0x0600447A RID: 17530 RVA: 0x000D7986 File Offset: 0x000D5B86
		public void SetOptions(AcceptInvitationOptions val)
		{
			this.Options = val;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000D7990 File Offset: 0x000D5B90
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000D79DC File Offset: 0x000D5BDC
		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			return acceptInvitationRequest != null && this.HasInvitationId == acceptInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(acceptInvitationRequest.InvitationId)) && this.HasOptions == acceptInvitationRequest.HasOptions && (!this.HasOptions || this.Options.Equals(acceptInvitationRequest.Options));
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000D7A4F File Offset: 0x000D5C4F
		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000D7A59 File Offset: 0x000D5C59
		public void Deserialize(Stream stream)
		{
			AcceptInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000D7A63 File Offset: 0x000D5C63
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return AcceptInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000D7A70 File Offset: 0x000D5C70
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			AcceptInvitationRequest.DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000D7A8C File Offset: 0x000D5C8C
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000D7AB4 File Offset: 0x000D5CB4
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
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
				else if (num != 16)
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
						instance.Options = AcceptInvitationOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						AcceptInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else
				{
					instance.InvitationId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000D7B66 File Offset: 0x000D5D66
		public void Serialize(Stream stream)
		{
			AcceptInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000D7B70 File Offset: 0x000D5D70
		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AcceptInvitationOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000D7BC8 File Offset: 0x000D5DC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInvitationId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.InvitationId);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001724 RID: 5924
		public bool HasInvitationId;

		// Token: 0x04001725 RID: 5925
		private ulong _InvitationId;

		// Token: 0x04001726 RID: 5926
		public bool HasOptions;

		// Token: 0x04001727 RID: 5927
		private AcceptInvitationOptions _Options;
	}
}
