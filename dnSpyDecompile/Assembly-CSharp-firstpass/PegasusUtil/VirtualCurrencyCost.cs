using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000046 RID: 70
	public class VirtualCurrencyCost : IProtoBuf
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00011A38 File Offset: 0x0000FC38
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00011A40 File Offset: 0x0000FC40
		public long Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				this._Cost = value;
				this.HasCost = true;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00011A50 File Offset: 0x0000FC50
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00011A58 File Offset: 0x0000FC58
		public string CurrencyCode
		{
			get
			{
				return this._CurrencyCode;
			}
			set
			{
				this._CurrencyCode = value;
				this.HasCurrencyCode = (value != null);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCost)
			{
				num ^= this.Cost.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public override bool Equals(object obj)
		{
			VirtualCurrencyCost virtualCurrencyCost = obj as VirtualCurrencyCost;
			return virtualCurrencyCost != null && this.HasCost == virtualCurrencyCost.HasCost && (!this.HasCost || this.Cost.Equals(virtualCurrencyCost.Cost)) && this.HasCurrencyCode == virtualCurrencyCost.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(virtualCurrencyCost.CurrencyCode));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011B2B File Offset: 0x0000FD2B
		public void Deserialize(Stream stream)
		{
			VirtualCurrencyCost.Deserialize(stream, this);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011B35 File Offset: 0x0000FD35
		public static VirtualCurrencyCost Deserialize(Stream stream, VirtualCurrencyCost instance)
		{
			return VirtualCurrencyCost.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011B40 File Offset: 0x0000FD40
		public static VirtualCurrencyCost DeserializeLengthDelimited(Stream stream)
		{
			VirtualCurrencyCost virtualCurrencyCost = new VirtualCurrencyCost();
			VirtualCurrencyCost.DeserializeLengthDelimited(stream, virtualCurrencyCost);
			return virtualCurrencyCost;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public static VirtualCurrencyCost DeserializeLengthDelimited(Stream stream, VirtualCurrencyCost instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VirtualCurrencyCost.Deserialize(stream, instance, num);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011B84 File Offset: 0x0000FD84
		public static VirtualCurrencyCost Deserialize(Stream stream, VirtualCurrencyCost instance, long limit)
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
					if (num != 18)
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
						instance.CurrencyCode = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Cost = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011C1B File Offset: 0x0000FE1B
		public void Serialize(Stream stream)
		{
			VirtualCurrencyCost.Serialize(stream, this);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011C24 File Offset: 0x0000FE24
		public static void Serialize(Stream stream, VirtualCurrencyCost instance)
		{
			if (instance.HasCost)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Cost);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00011C74 File Offset: 0x0000FE74
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCost)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.Cost);
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04000186 RID: 390
		public bool HasCost;

		// Token: 0x04000187 RID: 391
		private long _Cost;

		// Token: 0x04000188 RID: 392
		public bool HasCurrencyCode;

		// Token: 0x04000189 RID: 393
		private string _CurrencyCode;
	}
}
