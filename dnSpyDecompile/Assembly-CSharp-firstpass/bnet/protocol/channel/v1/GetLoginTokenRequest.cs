using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DB RID: 1243
	public class GetLoginTokenRequest : IProtoBuf
	{
		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x0010D239 File Offset: 0x0010B439
		// (set) Token: 0x060057BC RID: 22460 RVA: 0x0010D241 File Offset: 0x0010B441
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x0010D254 File Offset: 0x0010B454
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x0010D260 File Offset: 0x0010B460
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x0010D290 File Offset: 0x0010B490
		public override bool Equals(object obj)
		{
			GetLoginTokenRequest getLoginTokenRequest = obj as GetLoginTokenRequest;
			return getLoginTokenRequest != null && this.HasMemberId == getLoginTokenRequest.HasMemberId && (!this.HasMemberId || this.MemberId.Equals(getLoginTokenRequest.MemberId));
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060057C0 RID: 22464 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x0010D2D5 File Offset: 0x0010B4D5
		public static GetLoginTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLoginTokenRequest>(bs, 0, -1);
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x0010D2DF File Offset: 0x0010B4DF
		public void Deserialize(Stream stream)
		{
			GetLoginTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x0010D2E9 File Offset: 0x0010B4E9
		public static GetLoginTokenRequest Deserialize(Stream stream, GetLoginTokenRequest instance)
		{
			return GetLoginTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x0010D2F4 File Offset: 0x0010B4F4
		public static GetLoginTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLoginTokenRequest getLoginTokenRequest = new GetLoginTokenRequest();
			GetLoginTokenRequest.DeserializeLengthDelimited(stream, getLoginTokenRequest);
			return getLoginTokenRequest;
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0010D310 File Offset: 0x0010B510
		public static GetLoginTokenRequest DeserializeLengthDelimited(Stream stream, GetLoginTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLoginTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x0010D338 File Offset: 0x0010B538
		public static GetLoginTokenRequest Deserialize(Stream stream, GetLoginTokenRequest instance, long limit)
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
				else if (num == 26)
				{
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
					}
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x0010D3D2 File Offset: 0x0010B5D2
		public void Serialize(Stream stream)
		{
			GetLoginTokenRequest.Serialize(stream, this);
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x0010D3DB File Offset: 0x0010B5DB
		public static void Serialize(Stream stream, GetLoginTokenRequest instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x0010D40C File Offset: 0x0010B60C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001B8B RID: 7051
		public bool HasMemberId;

		// Token: 0x04001B8C RID: 7052
		private GameAccountHandle _MemberId;
	}
}
