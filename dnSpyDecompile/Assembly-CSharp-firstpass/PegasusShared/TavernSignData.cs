using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000161 RID: 353
	public class TavernSignData : IProtoBuf
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000547DB File Offset: 0x000529DB
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x000547E3 File Offset: 0x000529E3
		public int Sign { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000547EC File Offset: 0x000529EC
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x000547F4 File Offset: 0x000529F4
		public int Background { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000547FD File Offset: 0x000529FD
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x00054805 File Offset: 0x00052A05
		public int Major { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005480E File Offset: 0x00052A0E
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x00054816 File Offset: 0x00052A16
		public int Minor { get; set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005481F File Offset: 0x00052A1F
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00054827 File Offset: 0x00052A27
		public TavernSignType SignType { get; set; }

		// Token: 0x06001827 RID: 6183 RVA: 0x00054830 File Offset: 0x00052A30
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Sign.GetHashCode() ^ this.Background.GetHashCode() ^ this.Major.GetHashCode() ^ this.Minor.GetHashCode() ^ this.SignType.GetHashCode();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005489C File Offset: 0x00052A9C
		public override bool Equals(object obj)
		{
			TavernSignData tavernSignData = obj as TavernSignData;
			return tavernSignData != null && this.Sign.Equals(tavernSignData.Sign) && this.Background.Equals(tavernSignData.Background) && this.Major.Equals(tavernSignData.Major) && this.Minor.Equals(tavernSignData.Minor) && this.SignType.Equals(tavernSignData.SignType);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00054939 File Offset: 0x00052B39
		public void Deserialize(Stream stream)
		{
			TavernSignData.Deserialize(stream, this);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00054943 File Offset: 0x00052B43
		public static TavernSignData Deserialize(Stream stream, TavernSignData instance)
		{
			return TavernSignData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00054950 File Offset: 0x00052B50
		public static TavernSignData DeserializeLengthDelimited(Stream stream)
		{
			TavernSignData tavernSignData = new TavernSignData();
			TavernSignData.DeserializeLengthDelimited(stream, tavernSignData);
			return tavernSignData;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0005496C File Offset: 0x00052B6C
		public static TavernSignData DeserializeLengthDelimited(Stream stream, TavernSignData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernSignData.Deserialize(stream, instance, num);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00054994 File Offset: 0x00052B94
		public static TavernSignData Deserialize(Stream stream, TavernSignData instance, long limit)
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
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Sign = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Background = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Major = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Minor = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.SignType = (TavernSignType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600182E RID: 6190 RVA: 0x00054A7F File Offset: 0x00052C7F
		public void Serialize(Stream stream)
		{
			TavernSignData.Serialize(stream, this);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00054A88 File Offset: 0x00052C88
		public static void Serialize(Stream stream, TavernSignData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Sign));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Background));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Major));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Minor));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SignType));
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00054B00 File Offset: 0x00052D00
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Sign)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Background)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Major)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Minor)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.SignType)) + 5U;
		}
	}
}
