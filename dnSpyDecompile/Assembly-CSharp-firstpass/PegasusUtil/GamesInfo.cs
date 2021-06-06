using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000AA RID: 170
	public class GamesInfo : IProtoBuf
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002B65F File Offset: 0x0002985F
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0002B667 File Offset: 0x00029867
		public int GamesStarted { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002B670 File Offset: 0x00029870
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0002B678 File Offset: 0x00029878
		public int GamesWon { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002B681 File Offset: 0x00029881
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0002B689 File Offset: 0x00029889
		public int GamesLost { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002B692 File Offset: 0x00029892
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0002B69A File Offset: 0x0002989A
		public int FreeRewardProgress { get; set; }

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GamesStarted.GetHashCode() ^ this.GamesWon.GetHashCode() ^ this.GamesLost.GetHashCode() ^ this.FreeRewardProgress.GetHashCode();
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002B6F8 File Offset: 0x000298F8
		public override bool Equals(object obj)
		{
			GamesInfo gamesInfo = obj as GamesInfo;
			return gamesInfo != null && this.GamesStarted.Equals(gamesInfo.GamesStarted) && this.GamesWon.Equals(gamesInfo.GamesWon) && this.GamesLost.Equals(gamesInfo.GamesLost) && this.FreeRewardProgress.Equals(gamesInfo.FreeRewardProgress);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002B772 File Offset: 0x00029972
		public void Deserialize(Stream stream)
		{
			GamesInfo.Deserialize(stream, this);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002B77C File Offset: 0x0002997C
		public static GamesInfo Deserialize(Stream stream, GamesInfo instance)
		{
			return GamesInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002B788 File Offset: 0x00029988
		public static GamesInfo DeserializeLengthDelimited(Stream stream)
		{
			GamesInfo gamesInfo = new GamesInfo();
			GamesInfo.DeserializeLengthDelimited(stream, gamesInfo);
			return gamesInfo;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002B7A4 File Offset: 0x000299A4
		public static GamesInfo DeserializeLengthDelimited(Stream stream, GamesInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GamesInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002B7CC File Offset: 0x000299CC
		public static GamesInfo Deserialize(Stream stream, GamesInfo instance, long limit)
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
							instance.GamesStarted = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.GamesWon = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.GamesLost = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.FreeRewardProgress = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002B8A0 File Offset: 0x00029AA0
		public void Serialize(Stream stream)
		{
			GamesInfo.Serialize(stream, this);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public static void Serialize(Stream stream, GamesInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GamesStarted));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GamesWon));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GamesLost));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FreeRewardProgress));
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002B90C File Offset: 0x00029B0C
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.GamesStarted)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.GamesWon)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.GamesLost)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.FreeRewardProgress)) + 4U;
		}

		// Token: 0x020005B2 RID: 1458
		public enum PacketID
		{
			// Token: 0x04001F6A RID: 8042
			ID = 208
		}
	}
}
