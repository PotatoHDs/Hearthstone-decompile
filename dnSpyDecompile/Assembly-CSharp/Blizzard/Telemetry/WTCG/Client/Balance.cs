using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x0200119F RID: 4511
	public class Balance : IProtoBuf
	{
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600C75B RID: 51035 RVA: 0x003BEE31 File Offset: 0x003BD031
		// (set) Token: 0x0600C75C RID: 51036 RVA: 0x003BEE39 File Offset: 0x003BD039
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600C75D RID: 51037 RVA: 0x003BEE4C File Offset: 0x003BD04C
		// (set) Token: 0x0600C75E RID: 51038 RVA: 0x003BEE54 File Offset: 0x003BD054
		public double Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
				this.HasAmount = true;
			}
		}

		// Token: 0x0600C75F RID: 51039 RVA: 0x003BEE64 File Offset: 0x003BD064
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasAmount)
			{
				num ^= this.Amount.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C760 RID: 51040 RVA: 0x003BEEB0 File Offset: 0x003BD0B0
		public override bool Equals(object obj)
		{
			Balance balance = obj as Balance;
			return balance != null && this.HasName == balance.HasName && (!this.HasName || this.Name.Equals(balance.Name)) && this.HasAmount == balance.HasAmount && (!this.HasAmount || this.Amount.Equals(balance.Amount));
		}

		// Token: 0x0600C761 RID: 51041 RVA: 0x003BEF23 File Offset: 0x003BD123
		public void Deserialize(Stream stream)
		{
			Balance.Deserialize(stream, this);
		}

		// Token: 0x0600C762 RID: 51042 RVA: 0x003BEF2D File Offset: 0x003BD12D
		public static Balance Deserialize(Stream stream, Balance instance)
		{
			return Balance.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C763 RID: 51043 RVA: 0x003BEF38 File Offset: 0x003BD138
		public static Balance DeserializeLengthDelimited(Stream stream)
		{
			Balance balance = new Balance();
			Balance.DeserializeLengthDelimited(stream, balance);
			return balance;
		}

		// Token: 0x0600C764 RID: 51044 RVA: 0x003BEF54 File Offset: 0x003BD154
		public static Balance DeserializeLengthDelimited(Stream stream, Balance instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Balance.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C765 RID: 51045 RVA: 0x003BEF7C File Offset: 0x003BD17C
		public static Balance Deserialize(Stream stream, Balance instance, long limit)
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
				else if (num != 10)
				{
					if (num != 17)
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
						instance.Amount = binaryReader.ReadDouble();
					}
				}
				else
				{
					instance.Name = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C766 RID: 51046 RVA: 0x003BF01B File Offset: 0x003BD21B
		public void Serialize(Stream stream)
		{
			Balance.Serialize(stream, this);
		}

		// Token: 0x0600C767 RID: 51047 RVA: 0x003BF024 File Offset: 0x003BD224
		public static void Serialize(Stream stream, Balance instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Amount);
			}
		}

		// Token: 0x0600C768 RID: 51048 RVA: 0x003BF07C File Offset: 0x003BD27C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAmount)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x04009E80 RID: 40576
		public bool HasName;

		// Token: 0x04009E81 RID: 40577
		private string _Name;

		// Token: 0x04009E82 RID: 40578
		public bool HasAmount;

		// Token: 0x04009E83 RID: 40579
		private double _Amount;
	}
}
