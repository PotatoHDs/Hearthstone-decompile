using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000035 RID: 53
	public class NearbyPlayer : IProtoBuf
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B93B File Offset: 0x00009B3B
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B943 File Offset: 0x00009B43
		public ulong BnetIdHi { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B94C File Offset: 0x00009B4C
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000B954 File Offset: 0x00009B54
		public ulong BnetIdLo { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B95D File Offset: 0x00009B5D
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000B965 File Offset: 0x00009B65
		public ulong SessionStartTime { get; set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B970 File Offset: 0x00009B70
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.BnetIdHi.GetHashCode() ^ this.BnetIdLo.GetHashCode() ^ this.SessionStartTime.GetHashCode();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public override bool Equals(object obj)
		{
			NearbyPlayer nearbyPlayer = obj as NearbyPlayer;
			return nearbyPlayer != null && this.BnetIdHi.Equals(nearbyPlayer.BnetIdHi) && this.BnetIdLo.Equals(nearbyPlayer.BnetIdLo) && this.SessionStartTime.Equals(nearbyPlayer.SessionStartTime);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BA1A File Offset: 0x00009C1A
		public void Deserialize(Stream stream)
		{
			NearbyPlayer.Deserialize(stream, this);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BA24 File Offset: 0x00009C24
		public static NearbyPlayer Deserialize(Stream stream, NearbyPlayer instance)
		{
			return NearbyPlayer.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BA30 File Offset: 0x00009C30
		public static NearbyPlayer DeserializeLengthDelimited(Stream stream)
		{
			NearbyPlayer nearbyPlayer = new NearbyPlayer();
			NearbyPlayer.DeserializeLengthDelimited(stream, nearbyPlayer);
			return nearbyPlayer;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public static NearbyPlayer DeserializeLengthDelimited(Stream stream, NearbyPlayer instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NearbyPlayer.Deserialize(stream, instance, num);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000BA74 File Offset: 0x00009C74
		public static NearbyPlayer Deserialize(Stream stream, NearbyPlayer instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.SessionStartTime = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.BnetIdLo = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.BnetIdHi = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000BB21 File Offset: 0x00009D21
		public void Serialize(Stream stream)
		{
			NearbyPlayer.Serialize(stream, this);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000BB2A File Offset: 0x00009D2A
		public static void Serialize(Stream stream, NearbyPlayer instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.BnetIdHi);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.BnetIdLo);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.SessionStartTime);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000BB67 File Offset: 0x00009D67
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64(this.BnetIdHi) + ProtocolParser.SizeOfUInt64(this.BnetIdLo) + ProtocolParser.SizeOfUInt64(this.SessionStartTime) + 3U;
		}
	}
}
