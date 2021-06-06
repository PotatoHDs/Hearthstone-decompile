using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F8 RID: 248
	public class BattlegroundsPlayerInfo : IProtoBuf
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0003A486 File Offset: 0x00038686
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0003A48E File Offset: 0x0003868E
		public int Rating { get; set; }

		// Token: 0x06001080 RID: 4224 RVA: 0x0003A498 File Offset: 0x00038698
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Rating.GetHashCode();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003A4C0 File Offset: 0x000386C0
		public override bool Equals(object obj)
		{
			BattlegroundsPlayerInfo battlegroundsPlayerInfo = obj as BattlegroundsPlayerInfo;
			return battlegroundsPlayerInfo != null && this.Rating.Equals(battlegroundsPlayerInfo.Rating);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003A4F2 File Offset: 0x000386F2
		public void Deserialize(Stream stream)
		{
			BattlegroundsPlayerInfo.Deserialize(stream, this);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003A4FC File Offset: 0x000386FC
		public static BattlegroundsPlayerInfo Deserialize(Stream stream, BattlegroundsPlayerInfo instance)
		{
			return BattlegroundsPlayerInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003A508 File Offset: 0x00038708
		public static BattlegroundsPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsPlayerInfo battlegroundsPlayerInfo = new BattlegroundsPlayerInfo();
			BattlegroundsPlayerInfo.DeserializeLengthDelimited(stream, battlegroundsPlayerInfo);
			return battlegroundsPlayerInfo;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003A524 File Offset: 0x00038724
		public static BattlegroundsPlayerInfo DeserializeLengthDelimited(Stream stream, BattlegroundsPlayerInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsPlayerInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003A54C File Offset: 0x0003874C
		public static BattlegroundsPlayerInfo Deserialize(Stream stream, BattlegroundsPlayerInfo instance, long limit)
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
					instance.Rating = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001087 RID: 4231 RVA: 0x0003A5CC File Offset: 0x000387CC
		public void Serialize(Stream stream)
		{
			BattlegroundsPlayerInfo.Serialize(stream, this);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0003A5D5 File Offset: 0x000387D5
		public static void Serialize(Stream stream, BattlegroundsPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Rating));
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003A5EB File Offset: 0x000387EB
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Rating)) + 1U;
		}
	}
}
