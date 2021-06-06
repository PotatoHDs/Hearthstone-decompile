using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C1 RID: 449
	public class PowerHistoryTagChange : IProtoBuf
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000646AC File Offset: 0x000628AC
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x000646B4 File Offset: 0x000628B4
		public int Entity { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000646BD File Offset: 0x000628BD
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x000646C5 File Offset: 0x000628C5
		public int Tag { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000646CE File Offset: 0x000628CE
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x000646D6 File Offset: 0x000628D6
		public int Value { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000646DF File Offset: 0x000628DF
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x000646E7 File Offset: 0x000628E7
		public bool ChangeDef
		{
			get
			{
				return this._ChangeDef;
			}
			set
			{
				this._ChangeDef = value;
				this.HasChangeDef = true;
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000646F8 File Offset: 0x000628F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Entity.GetHashCode();
			num ^= this.Tag.GetHashCode();
			num ^= this.Value.GetHashCode();
			if (this.HasChangeDef)
			{
				num ^= this.ChangeDef.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00064760 File Offset: 0x00062960
		public override bool Equals(object obj)
		{
			PowerHistoryTagChange powerHistoryTagChange = obj as PowerHistoryTagChange;
			return powerHistoryTagChange != null && this.Entity.Equals(powerHistoryTagChange.Entity) && this.Tag.Equals(powerHistoryTagChange.Tag) && this.Value.Equals(powerHistoryTagChange.Value) && this.HasChangeDef == powerHistoryTagChange.HasChangeDef && (!this.HasChangeDef || this.ChangeDef.Equals(powerHistoryTagChange.ChangeDef));
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000647F0 File Offset: 0x000629F0
		public void Deserialize(Stream stream)
		{
			PowerHistoryTagChange.Deserialize(stream, this);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000647FA File Offset: 0x000629FA
		public static PowerHistoryTagChange Deserialize(Stream stream, PowerHistoryTagChange instance)
		{
			return PowerHistoryTagChange.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00064808 File Offset: 0x00062A08
		public static PowerHistoryTagChange DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryTagChange powerHistoryTagChange = new PowerHistoryTagChange();
			PowerHistoryTagChange.DeserializeLengthDelimited(stream, powerHistoryTagChange);
			return powerHistoryTagChange;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00064824 File Offset: 0x00062A24
		public static PowerHistoryTagChange DeserializeLengthDelimited(Stream stream, PowerHistoryTagChange instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryTagChange.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0006484C File Offset: 0x00062A4C
		public static PowerHistoryTagChange Deserialize(Stream stream, PowerHistoryTagChange instance, long limit)
		{
			instance.ChangeDef = false;
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
							instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Tag = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Value = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ChangeDef = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06001C8F RID: 7311 RVA: 0x00064926 File Offset: 0x00062B26
		public void Serialize(Stream stream)
		{
			PowerHistoryTagChange.Serialize(stream, this);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00064930 File Offset: 0x00062B30
		public static void Serialize(Stream stream, PowerHistoryTagChange instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Entity));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Tag));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Value));
			if (instance.HasChangeDef)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.ChangeDef);
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00064998 File Offset: 0x00062B98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Entity));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Tag));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Value));
			if (this.HasChangeDef)
			{
				num += 1U;
				num += 1U;
			}
			return num + 3U;
		}

		// Token: 0x04000A60 RID: 2656
		public bool HasChangeDef;

		// Token: 0x04000A61 RID: 2657
		private bool _ChangeDef;
	}
}
