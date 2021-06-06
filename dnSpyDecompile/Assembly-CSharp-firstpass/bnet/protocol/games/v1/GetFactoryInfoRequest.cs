using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000383 RID: 899
	public class GetFactoryInfoRequest : IProtoBuf
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000BAB23 File Offset: 0x000B8D23
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000BAB2B File Offset: 0x000B8D2B
		public ulong FactoryId { get; set; }

		// Token: 0x06003953 RID: 14675 RVA: 0x000BAB34 File Offset: 0x000B8D34
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000BAB40 File Offset: 0x000B8D40
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FactoryId.GetHashCode();
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000BAB68 File Offset: 0x000B8D68
		public override bool Equals(object obj)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = obj as GetFactoryInfoRequest;
			return getFactoryInfoRequest != null && this.FactoryId.Equals(getFactoryInfoRequest.FactoryId);
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000BAB9A File Offset: 0x000B8D9A
		public static GetFactoryInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoRequest>(bs, 0, -1);
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000BABA4 File Offset: 0x000B8DA4
		public void Deserialize(Stream stream)
		{
			GetFactoryInfoRequest.Deserialize(stream, this);
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000BABAE File Offset: 0x000B8DAE
		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance)
		{
			return GetFactoryInfoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000BABBC File Offset: 0x000B8DBC
		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = new GetFactoryInfoRequest();
			GetFactoryInfoRequest.DeserializeLengthDelimited(stream, getFactoryInfoRequest);
			return getFactoryInfoRequest;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000BABD8 File Offset: 0x000B8DD8
		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream, GetFactoryInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFactoryInfoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000BAC00 File Offset: 0x000B8E00
		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num == 9)
				{
					instance.FactoryId = binaryReader.ReadUInt64();
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

		// Token: 0x0600395D RID: 14685 RVA: 0x000BAC87 File Offset: 0x000B8E87
		public void Serialize(Stream stream)
		{
			GetFactoryInfoRequest.Serialize(stream, this);
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000BAC90 File Offset: 0x000B8E90
		public static void Serialize(Stream stream, GetFactoryInfoRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000BACAB File Offset: 0x000B8EAB
		public uint GetSerializedSize()
		{
			return 0U + 8U + 1U;
		}
	}
}
