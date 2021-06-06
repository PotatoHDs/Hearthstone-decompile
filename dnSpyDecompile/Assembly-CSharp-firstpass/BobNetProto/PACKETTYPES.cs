using System;
using System.IO;

namespace BobNetProto
{
	// Token: 0x020001DA RID: 474
	public class PACKETTYPES : IProtoBuf
	{
		// Token: 0x06001E30 RID: 7728 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0006A0A8 File Offset: 0x000682A8
		public override bool Equals(object obj)
		{
			return obj is PACKETTYPES;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0006A0B5 File Offset: 0x000682B5
		public void Deserialize(Stream stream)
		{
			PACKETTYPES.Deserialize(stream, this);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0006A0BF File Offset: 0x000682BF
		public static PACKETTYPES Deserialize(Stream stream, PACKETTYPES instance)
		{
			return PACKETTYPES.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0006A0CC File Offset: 0x000682CC
		public static PACKETTYPES DeserializeLengthDelimited(Stream stream)
		{
			PACKETTYPES packettypes = new PACKETTYPES();
			PACKETTYPES.DeserializeLengthDelimited(stream, packettypes);
			return packettypes;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public static PACKETTYPES DeserializeLengthDelimited(Stream stream, PACKETTYPES instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PACKETTYPES.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0006A110 File Offset: 0x00068310
		public static PACKETTYPES Deserialize(Stream stream, PACKETTYPES instance, long limit)
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

		// Token: 0x06001E37 RID: 7735 RVA: 0x0006A17D File Offset: 0x0006837D
		public void Serialize(Stream stream)
		{
			PACKETTYPES.Serialize(stream, this);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, PACKETTYPES instance)
		{
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000664 RID: 1636
		public enum BobNetCount
		{
			// Token: 0x04002162 RID: 8546
			COUNT = 700
		}
	}
}
