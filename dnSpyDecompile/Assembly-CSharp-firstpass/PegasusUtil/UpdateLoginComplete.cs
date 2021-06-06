using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000083 RID: 131
	public class UpdateLoginComplete : IProtoBuf
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001CD6E File Offset: 0x0001AF6E
		public override bool Equals(object obj)
		{
			return obj is UpdateLoginComplete;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001CD7B File Offset: 0x0001AF7B
		public void Deserialize(Stream stream)
		{
			UpdateLoginComplete.Deserialize(stream, this);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001CD85 File Offset: 0x0001AF85
		public static UpdateLoginComplete Deserialize(Stream stream, UpdateLoginComplete instance)
		{
			return UpdateLoginComplete.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001CD90 File Offset: 0x0001AF90
		public static UpdateLoginComplete DeserializeLengthDelimited(Stream stream)
		{
			UpdateLoginComplete updateLoginComplete = new UpdateLoginComplete();
			UpdateLoginComplete.DeserializeLengthDelimited(stream, updateLoginComplete);
			return updateLoginComplete;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public static UpdateLoginComplete DeserializeLengthDelimited(Stream stream, UpdateLoginComplete instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateLoginComplete.Deserialize(stream, instance, num);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		public static UpdateLoginComplete Deserialize(Stream stream, UpdateLoginComplete instance, long limit)
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

		// Token: 0x0600081A RID: 2074 RVA: 0x0001CE41 File Offset: 0x0001B041
		public void Serialize(Stream stream)
		{
			UpdateLoginComplete.Serialize(stream, this);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, UpdateLoginComplete instance)
		{
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000596 RID: 1430
		public enum PacketID
		{
			// Token: 0x04001F1E RID: 7966
			ID = 307
		}
	}
}
