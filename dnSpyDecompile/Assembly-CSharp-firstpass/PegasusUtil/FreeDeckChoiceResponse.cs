using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CF RID: 207
	public class FreeDeckChoiceResponse : IProtoBuf
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00033C2D File Offset: 0x00031E2D
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00033C35 File Offset: 0x00031E35
		public bool Success { get; set; }

		// Token: 0x06000E21 RID: 3617 RVA: 0x00033C40 File Offset: 0x00031E40
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Success.GetHashCode();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00033C68 File Offset: 0x00031E68
		public override bool Equals(object obj)
		{
			FreeDeckChoiceResponse freeDeckChoiceResponse = obj as FreeDeckChoiceResponse;
			return freeDeckChoiceResponse != null && this.Success.Equals(freeDeckChoiceResponse.Success);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00033C9A File Offset: 0x00031E9A
		public void Deserialize(Stream stream)
		{
			FreeDeckChoiceResponse.Deserialize(stream, this);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00033CA4 File Offset: 0x00031EA4
		public static FreeDeckChoiceResponse Deserialize(Stream stream, FreeDeckChoiceResponse instance)
		{
			return FreeDeckChoiceResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00033CB0 File Offset: 0x00031EB0
		public static FreeDeckChoiceResponse DeserializeLengthDelimited(Stream stream)
		{
			FreeDeckChoiceResponse freeDeckChoiceResponse = new FreeDeckChoiceResponse();
			FreeDeckChoiceResponse.DeserializeLengthDelimited(stream, freeDeckChoiceResponse);
			return freeDeckChoiceResponse;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00033CCC File Offset: 0x00031ECC
		public static FreeDeckChoiceResponse DeserializeLengthDelimited(Stream stream, FreeDeckChoiceResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FreeDeckChoiceResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00033CF4 File Offset: 0x00031EF4
		public static FreeDeckChoiceResponse Deserialize(Stream stream, FreeDeckChoiceResponse instance, long limit)
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
					instance.Success = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06000E28 RID: 3624 RVA: 0x00033D73 File Offset: 0x00031F73
		public void Serialize(Stream stream)
		{
			FreeDeckChoiceResponse.Serialize(stream, this);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00033D7C File Offset: 0x00031F7C
		public static void Serialize(Stream stream, FreeDeckChoiceResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00033D91 File Offset: 0x00031F91
		public uint GetSerializedSize()
		{
			return 0U + 1U + 1U;
		}

		// Token: 0x020005DE RID: 1502
		public enum PacketID
		{
			// Token: 0x04001FE4 RID: 8164
			ID = 334,
			// Token: 0x04001FE5 RID: 8165
			System = 0
		}
	}
}
