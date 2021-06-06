using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001AC RID: 428
	public class MouseInfo : IProtoBuf
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0005FC27 File Offset: 0x0005DE27
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x0005FC2F File Offset: 0x0005DE2F
		public int ArrowOrigin { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0005FC38 File Offset: 0x0005DE38
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0005FC40 File Offset: 0x0005DE40
		public int HeldCard { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0005FC49 File Offset: 0x0005DE49
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0005FC51 File Offset: 0x0005DE51
		public int OverCard { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0005FC5A File Offset: 0x0005DE5A
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0005FC62 File Offset: 0x0005DE62
		public int X { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0005FC6B File Offset: 0x0005DE6B
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0005FC73 File Offset: 0x0005DE73
		public int Y { get; set; }

		// Token: 0x06001B17 RID: 6935 RVA: 0x0005FC7C File Offset: 0x0005DE7C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ArrowOrigin.GetHashCode() ^ this.HeldCard.GetHashCode() ^ this.OverCard.GetHashCode() ^ this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0005FCE0 File Offset: 0x0005DEE0
		public override bool Equals(object obj)
		{
			MouseInfo mouseInfo = obj as MouseInfo;
			return mouseInfo != null && this.ArrowOrigin.Equals(mouseInfo.ArrowOrigin) && this.HeldCard.Equals(mouseInfo.HeldCard) && this.OverCard.Equals(mouseInfo.OverCard) && this.X.Equals(mouseInfo.X) && this.Y.Equals(mouseInfo.Y);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0005FD72 File Offset: 0x0005DF72
		public void Deserialize(Stream stream)
		{
			MouseInfo.Deserialize(stream, this);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0005FD7C File Offset: 0x0005DF7C
		public static MouseInfo Deserialize(Stream stream, MouseInfo instance)
		{
			return MouseInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005FD88 File Offset: 0x0005DF88
		public static MouseInfo DeserializeLengthDelimited(Stream stream)
		{
			MouseInfo mouseInfo = new MouseInfo();
			MouseInfo.DeserializeLengthDelimited(stream, mouseInfo);
			return mouseInfo;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		public static MouseInfo DeserializeLengthDelimited(Stream stream, MouseInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MouseInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0005FDCC File Offset: 0x0005DFCC
		public static MouseInfo Deserialize(Stream stream, MouseInfo instance, long limit)
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
							instance.ArrowOrigin = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.HeldCard = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.OverCard = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.X = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Y = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001B1E RID: 6942 RVA: 0x0005FEB7 File Offset: 0x0005E0B7
		public void Serialize(Stream stream)
		{
			MouseInfo.Serialize(stream, this);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0005FEC0 File Offset: 0x0005E0C0
		public static void Serialize(Stream stream, MouseInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ArrowOrigin));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeldCard));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.OverCard));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.X));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Y));
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0005FF38 File Offset: 0x0005E138
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ArrowOrigin)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.HeldCard)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.OverCard)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.X)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Y)) + 5U;
		}
	}
}
