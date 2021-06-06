using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000517 RID: 1303
	public class GetGameTimeRemainingInfoResponse : IProtoBuf
	{
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06005CDE RID: 23774 RVA: 0x0011A18E File Offset: 0x0011838E
		// (set) Token: 0x06005CDF RID: 23775 RVA: 0x0011A196 File Offset: 0x00118396
		public GameTimeRemainingInfo GameTimeRemainingInfo
		{
			get
			{
				return this._GameTimeRemainingInfo;
			}
			set
			{
				this._GameTimeRemainingInfo = value;
				this.HasGameTimeRemainingInfo = (value != null);
			}
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0011A1A9 File Offset: 0x001183A9
		public void SetGameTimeRemainingInfo(GameTimeRemainingInfo val)
		{
			this.GameTimeRemainingInfo = val;
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0011A1B4 File Offset: 0x001183B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameTimeRemainingInfo)
			{
				num ^= this.GameTimeRemainingInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0011A1E4 File Offset: 0x001183E4
		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = obj as GetGameTimeRemainingInfoResponse;
			return getGameTimeRemainingInfoResponse != null && this.HasGameTimeRemainingInfo == getGameTimeRemainingInfoResponse.HasGameTimeRemainingInfo && (!this.HasGameTimeRemainingInfo || this.GameTimeRemainingInfo.Equals(getGameTimeRemainingInfoResponse.GameTimeRemainingInfo));
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x0011A229 File Offset: 0x00118429
		public static GetGameTimeRemainingInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoResponse>(bs, 0, -1);
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0011A233 File Offset: 0x00118433
		public void Deserialize(Stream stream)
		{
			GetGameTimeRemainingInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x0011A23D File Offset: 0x0011843D
		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			return GetGameTimeRemainingInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x0011A248 File Offset: 0x00118448
		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = new GetGameTimeRemainingInfoResponse();
			GetGameTimeRemainingInfoResponse.DeserializeLengthDelimited(stream, getGameTimeRemainingInfoResponse);
			return getGameTimeRemainingInfoResponse;
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x0011A264 File Offset: 0x00118464
		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameTimeRemainingInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0011A28C File Offset: 0x0011848C
		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.GameTimeRemainingInfo == null)
					{
						instance.GameTimeRemainingInfo = GameTimeRemainingInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameTimeRemainingInfo.DeserializeLengthDelimited(stream, instance.GameTimeRemainingInfo);
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

		// Token: 0x06005CEA RID: 23786 RVA: 0x0011A326 File Offset: 0x00118526
		public void Serialize(Stream stream)
		{
			GetGameTimeRemainingInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x0011A32F File Offset: 0x0011852F
		public static void Serialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			if (instance.HasGameTimeRemainingInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeRemainingInfo.GetSerializedSize());
				GameTimeRemainingInfo.Serialize(stream, instance.GameTimeRemainingInfo);
			}
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0011A360 File Offset: 0x00118560
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameTimeRemainingInfo)
			{
				num += 1U;
				uint serializedSize = this.GameTimeRemainingInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001CB0 RID: 7344
		public bool HasGameTimeRemainingInfo;

		// Token: 0x04001CB1 RID: 7345
		private GameTimeRemainingInfo _GameTimeRemainingInfo;
	}
}
