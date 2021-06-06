using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F1 RID: 241
	public class GetServerTimeResponse : IProtoBuf
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00039672 File Offset: 0x00037872
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x0003967A File Offset: 0x0003787A
		public long ServerUnixTime { get; set; }

		// Token: 0x0600101F RID: 4127 RVA: 0x00039684 File Offset: 0x00037884
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ServerUnixTime.GetHashCode();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000396AC File Offset: 0x000378AC
		public override bool Equals(object obj)
		{
			GetServerTimeResponse getServerTimeResponse = obj as GetServerTimeResponse;
			return getServerTimeResponse != null && this.ServerUnixTime.Equals(getServerTimeResponse.ServerUnixTime);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000396DE File Offset: 0x000378DE
		public void Deserialize(Stream stream)
		{
			GetServerTimeResponse.Deserialize(stream, this);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000396E8 File Offset: 0x000378E8
		public static GetServerTimeResponse Deserialize(Stream stream, GetServerTimeResponse instance)
		{
			return GetServerTimeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000396F4 File Offset: 0x000378F4
		public static GetServerTimeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetServerTimeResponse getServerTimeResponse = new GetServerTimeResponse();
			GetServerTimeResponse.DeserializeLengthDelimited(stream, getServerTimeResponse);
			return getServerTimeResponse;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00039710 File Offset: 0x00037910
		public static GetServerTimeResponse DeserializeLengthDelimited(Stream stream, GetServerTimeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetServerTimeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00039738 File Offset: 0x00037938
		public static GetServerTimeResponse Deserialize(Stream stream, GetServerTimeResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.ServerUnixTime = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001026 RID: 4134 RVA: 0x000397B7 File Offset: 0x000379B7
		public void Serialize(Stream stream)
		{
			GetServerTimeResponse.Serialize(stream, this);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000397C0 File Offset: 0x000379C0
		public static void Serialize(Stream stream, GetServerTimeResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ServerUnixTime);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000397D5 File Offset: 0x000379D5
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.ServerUnixTime) + 1U;
		}

		// Token: 0x020005F5 RID: 1525
		public enum PacketID
		{
			// Token: 0x0400201F RID: 8223
			ID = 365
		}
	}
}
