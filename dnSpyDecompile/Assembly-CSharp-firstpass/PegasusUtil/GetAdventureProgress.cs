using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000073 RID: 115
	public class GetAdventureProgress : IProtoBuf
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001AE73 File Offset: 0x00019073
		public override bool Equals(object obj)
		{
			return obj is GetAdventureProgress;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001AE80 File Offset: 0x00019080
		public void Deserialize(Stream stream)
		{
			GetAdventureProgress.Deserialize(stream, this);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001AE8A File Offset: 0x0001908A
		public static GetAdventureProgress Deserialize(Stream stream, GetAdventureProgress instance)
		{
			return GetAdventureProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001AE98 File Offset: 0x00019098
		public static GetAdventureProgress DeserializeLengthDelimited(Stream stream)
		{
			GetAdventureProgress getAdventureProgress = new GetAdventureProgress();
			GetAdventureProgress.DeserializeLengthDelimited(stream, getAdventureProgress);
			return getAdventureProgress;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001AEB4 File Offset: 0x000190B4
		public static GetAdventureProgress DeserializeLengthDelimited(Stream stream, GetAdventureProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAdventureProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001AEDC File Offset: 0x000190DC
		public static GetAdventureProgress Deserialize(Stream stream, GetAdventureProgress instance, long limit)
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

		// Token: 0x06000748 RID: 1864 RVA: 0x0001AF49 File Offset: 0x00019149
		public void Serialize(Stream stream)
		{
			GetAdventureProgress.Serialize(stream, this);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetAdventureProgress instance)
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000586 RID: 1414
		public enum PacketID
		{
			// Token: 0x04001EEF RID: 7919
			ID = 305,
			// Token: 0x04001EF0 RID: 7920
			System = 0
		}
	}
}
