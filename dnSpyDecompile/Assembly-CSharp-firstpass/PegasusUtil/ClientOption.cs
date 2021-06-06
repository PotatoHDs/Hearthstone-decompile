using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000033 RID: 51
	public class ClientOption : IProtoBuf
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B102 File Offset: 0x00009302
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000B10A File Offset: 0x0000930A
		public int Index { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B113 File Offset: 0x00009313
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000B11B File Offset: 0x0000931B
		public bool AsBool
		{
			get
			{
				return this._AsBool;
			}
			set
			{
				this._AsBool = value;
				this.HasAsBool = true;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B12B File Offset: 0x0000932B
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000B133 File Offset: 0x00009333
		public int AsInt32
		{
			get
			{
				return this._AsInt32;
			}
			set
			{
				this._AsInt32 = value;
				this.HasAsInt32 = true;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B143 File Offset: 0x00009343
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000B14B File Offset: 0x0000934B
		public long AsInt64
		{
			get
			{
				return this._AsInt64;
			}
			set
			{
				this._AsInt64 = value;
				this.HasAsInt64 = true;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B15B File Offset: 0x0000935B
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000B163 File Offset: 0x00009363
		public float AsFloat
		{
			get
			{
				return this._AsFloat;
			}
			set
			{
				this._AsFloat = value;
				this.HasAsFloat = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B173 File Offset: 0x00009373
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000B17B File Offset: 0x0000937B
		public ulong AsUint64
		{
			get
			{
				return this._AsUint64;
			}
			set
			{
				this._AsUint64 = value;
				this.HasAsUint64 = true;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B18C File Offset: 0x0000938C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Index.GetHashCode();
			if (this.HasAsBool)
			{
				num ^= this.AsBool.GetHashCode();
			}
			if (this.HasAsInt32)
			{
				num ^= this.AsInt32.GetHashCode();
			}
			if (this.HasAsInt64)
			{
				num ^= this.AsInt64.GetHashCode();
			}
			if (this.HasAsFloat)
			{
				num ^= this.AsFloat.GetHashCode();
			}
			if (this.HasAsUint64)
			{
				num ^= this.AsUint64.GetHashCode();
			}
			return num;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B238 File Offset: 0x00009438
		public override bool Equals(object obj)
		{
			ClientOption clientOption = obj as ClientOption;
			return clientOption != null && this.Index.Equals(clientOption.Index) && this.HasAsBool == clientOption.HasAsBool && (!this.HasAsBool || this.AsBool.Equals(clientOption.AsBool)) && this.HasAsInt32 == clientOption.HasAsInt32 && (!this.HasAsInt32 || this.AsInt32.Equals(clientOption.AsInt32)) && this.HasAsInt64 == clientOption.HasAsInt64 && (!this.HasAsInt64 || this.AsInt64.Equals(clientOption.AsInt64)) && this.HasAsFloat == clientOption.HasAsFloat && (!this.HasAsFloat || this.AsFloat.Equals(clientOption.AsFloat)) && this.HasAsUint64 == clientOption.HasAsUint64 && (!this.HasAsUint64 || this.AsUint64.Equals(clientOption.AsUint64));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B352 File Offset: 0x00009552
		public void Deserialize(Stream stream)
		{
			ClientOption.Deserialize(stream, this);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B35C File Offset: 0x0000955C
		public static ClientOption Deserialize(Stream stream, ClientOption instance)
		{
			return ClientOption.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B368 File Offset: 0x00009568
		public static ClientOption DeserializeLengthDelimited(Stream stream)
		{
			ClientOption clientOption = new ClientOption();
			ClientOption.DeserializeLengthDelimited(stream, clientOption);
			return clientOption;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B384 File Offset: 0x00009584
		public static ClientOption DeserializeLengthDelimited(Stream stream, ClientOption instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientOption.Deserialize(stream, instance, num);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B3AC File Offset: 0x000095AC
		public static ClientOption Deserialize(Stream stream, ClientOption instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.Index = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.AsBool = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 24)
						{
							instance.AsInt32 = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.AsInt64 = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 45)
						{
							instance.AsFloat = binaryReader.ReadSingle();
							continue;
						}
						if (num == 48)
						{
							instance.AsUint64 = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public void Serialize(Stream stream)
		{
			ClientOption.Serialize(stream, this);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public static void Serialize(Stream stream, ClientOption instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Index));
			if (instance.HasAsBool)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.AsBool);
			}
			if (instance.HasAsInt32)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AsInt32));
			}
			if (instance.HasAsInt64)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AsInt64);
			}
			if (instance.HasAsFloat)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.AsFloat);
			}
			if (instance.HasAsUint64)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.AsUint64);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B578 File Offset: 0x00009778
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Index));
			if (this.HasAsBool)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasAsInt32)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AsInt32));
			}
			if (this.HasAsInt64)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.AsInt64);
			}
			if (this.HasAsFloat)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasAsUint64)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AsUint64);
			}
			return num + 1U;
		}

		// Token: 0x040000CC RID: 204
		public bool HasAsBool;

		// Token: 0x040000CD RID: 205
		private bool _AsBool;

		// Token: 0x040000CE RID: 206
		public bool HasAsInt32;

		// Token: 0x040000CF RID: 207
		private int _AsInt32;

		// Token: 0x040000D0 RID: 208
		public bool HasAsInt64;

		// Token: 0x040000D1 RID: 209
		private long _AsInt64;

		// Token: 0x040000D2 RID: 210
		public bool HasAsFloat;

		// Token: 0x040000D3 RID: 211
		private float _AsFloat;

		// Token: 0x040000D4 RID: 212
		public bool HasAsUint64;

		// Token: 0x040000D5 RID: 213
		private ulong _AsUint64;
	}
}
