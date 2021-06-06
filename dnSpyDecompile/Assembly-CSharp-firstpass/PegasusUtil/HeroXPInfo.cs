using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200004A RID: 74
	public class HeroXPInfo : IProtoBuf
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00013837 File Offset: 0x00011A37
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001383F File Offset: 0x00011A3F
		public int ClassId { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00013848 File Offset: 0x00011A48
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00013850 File Offset: 0x00011A50
		public int Level { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00013859 File Offset: 0x00011A59
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00013861 File Offset: 0x00011A61
		public long CurrXp { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001386A File Offset: 0x00011A6A
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00013872 File Offset: 0x00011A72
		public long MaxXp { get; set; }

		// Token: 0x060004AC RID: 1196 RVA: 0x0001387C File Offset: 0x00011A7C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ClassId.GetHashCode() ^ this.Level.GetHashCode() ^ this.CurrXp.GetHashCode() ^ this.MaxXp.GetHashCode();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000138D0 File Offset: 0x00011AD0
		public override bool Equals(object obj)
		{
			HeroXPInfo heroXPInfo = obj as HeroXPInfo;
			return heroXPInfo != null && this.ClassId.Equals(heroXPInfo.ClassId) && this.Level.Equals(heroXPInfo.Level) && this.CurrXp.Equals(heroXPInfo.CurrXp) && this.MaxXp.Equals(heroXPInfo.MaxXp);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001394A File Offset: 0x00011B4A
		public void Deserialize(Stream stream)
		{
			HeroXPInfo.Deserialize(stream, this);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00013954 File Offset: 0x00011B54
		public static HeroXPInfo Deserialize(Stream stream, HeroXPInfo instance)
		{
			return HeroXPInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013960 File Offset: 0x00011B60
		public static HeroXPInfo DeserializeLengthDelimited(Stream stream)
		{
			HeroXPInfo heroXPInfo = new HeroXPInfo();
			HeroXPInfo.DeserializeLengthDelimited(stream, heroXPInfo);
			return heroXPInfo;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001397C File Offset: 0x00011B7C
		public static HeroXPInfo DeserializeLengthDelimited(Stream stream, HeroXPInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HeroXPInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000139A4 File Offset: 0x00011BA4
		public static HeroXPInfo Deserialize(Stream stream, HeroXPInfo instance, long limit)
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
							instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Level = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.CurrXp = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.MaxXp = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060004B3 RID: 1203 RVA: 0x00013A76 File Offset: 0x00011C76
		public void Serialize(Stream stream)
		{
			HeroXPInfo.Serialize(stream, this);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013A80 File Offset: 0x00011C80
		public static void Serialize(Stream stream, HeroXPInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrXp);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxXp);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013ADE File Offset: 0x00011CDE
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Level)) + ProtocolParser.SizeOfUInt64((ulong)this.CurrXp) + ProtocolParser.SizeOfUInt64((ulong)this.MaxXp) + 4U;
		}
	}
}
