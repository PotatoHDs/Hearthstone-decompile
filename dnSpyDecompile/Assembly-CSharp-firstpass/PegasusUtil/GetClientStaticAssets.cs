using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000078 RID: 120
	public class GetClientStaticAssets : IProtoBuf
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001BDC3 File Offset: 0x00019FC3
		public override bool Equals(object obj)
		{
			return obj is GetClientStaticAssets;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public void Deserialize(Stream stream)
		{
			GetClientStaticAssets.Deserialize(stream, this);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001BDDA File Offset: 0x00019FDA
		public static GetClientStaticAssets Deserialize(Stream stream, GetClientStaticAssets instance)
		{
			return GetClientStaticAssets.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		public static GetClientStaticAssets DeserializeLengthDelimited(Stream stream)
		{
			GetClientStaticAssets getClientStaticAssets = new GetClientStaticAssets();
			GetClientStaticAssets.DeserializeLengthDelimited(stream, getClientStaticAssets);
			return getClientStaticAssets;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001BE04 File Offset: 0x0001A004
		public static GetClientStaticAssets DeserializeLengthDelimited(Stream stream, GetClientStaticAssets instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetClientStaticAssets.Deserialize(stream, instance, num);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public static GetClientStaticAssets Deserialize(Stream stream, GetClientStaticAssets instance, long limit)
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

		// Token: 0x06000797 RID: 1943 RVA: 0x0001BE99 File Offset: 0x0001A099
		public void Serialize(Stream stream)
		{
			GetClientStaticAssets.Serialize(stream, this);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetClientStaticAssets instance)
		{
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200058B RID: 1419
		public enum PacketID
		{
			// Token: 0x04001EFE RID: 7934
			ID = 340
		}
	}
}
