using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x0200014B RID: 331
	public class LocalizedString : IProtoBuf
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0004AFAB File Offset: 0x000491AB
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0004AFB3 File Offset: 0x000491B3
		public string Key { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0004AFBC File Offset: 0x000491BC
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0004AFC4 File Offset: 0x000491C4
		public string DeprecatedValue
		{
			get
			{
				return this._DeprecatedValue;
			}
			set
			{
				this._DeprecatedValue = value;
				this.HasDeprecatedValue = (value != null);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0004AFD7 File Offset: 0x000491D7
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x0004AFDF File Offset: 0x000491DF
		public int DeprecatedLocale
		{
			get
			{
				return this._DeprecatedLocale;
			}
			set
			{
				this._DeprecatedLocale = value;
				this.HasDeprecatedLocale = true;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0004AFEF File Offset: 0x000491EF
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x0004AFF7 File Offset: 0x000491F7
		public List<LocalizedStringValue> Values
		{
			get
			{
				return this._Values;
			}
			set
			{
				this._Values = value;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0004B000 File Offset: 0x00049200
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Key.GetHashCode();
			if (this.HasDeprecatedValue)
			{
				num ^= this.DeprecatedValue.GetHashCode();
			}
			if (this.HasDeprecatedLocale)
			{
				num ^= this.DeprecatedLocale.GetHashCode();
			}
			foreach (LocalizedStringValue localizedStringValue in this.Values)
			{
				num ^= localizedStringValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0004B0A0 File Offset: 0x000492A0
		public override bool Equals(object obj)
		{
			LocalizedString localizedString = obj as LocalizedString;
			if (localizedString == null)
			{
				return false;
			}
			if (!this.Key.Equals(localizedString.Key))
			{
				return false;
			}
			if (this.HasDeprecatedValue != localizedString.HasDeprecatedValue || (this.HasDeprecatedValue && !this.DeprecatedValue.Equals(localizedString.DeprecatedValue)))
			{
				return false;
			}
			if (this.HasDeprecatedLocale != localizedString.HasDeprecatedLocale || (this.HasDeprecatedLocale && !this.DeprecatedLocale.Equals(localizedString.DeprecatedLocale)))
			{
				return false;
			}
			if (this.Values.Count != localizedString.Values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (!this.Values[i].Equals(localizedString.Values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0004B179 File Offset: 0x00049379
		public void Deserialize(Stream stream)
		{
			LocalizedString.Deserialize(stream, this);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0004B183 File Offset: 0x00049383
		public static LocalizedString Deserialize(Stream stream, LocalizedString instance)
		{
			return LocalizedString.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0004B190 File Offset: 0x00049390
		public static LocalizedString DeserializeLengthDelimited(Stream stream)
		{
			LocalizedString localizedString = new LocalizedString();
			LocalizedString.DeserializeLengthDelimited(stream, localizedString);
			return localizedString;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0004B1AC File Offset: 0x000493AC
		public static LocalizedString DeserializeLengthDelimited(Stream stream, LocalizedString instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocalizedString.Deserialize(stream, instance, num);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0004B1D4 File Offset: 0x000493D4
		public static LocalizedString Deserialize(Stream stream, LocalizedString instance, long limit)
		{
			if (instance.Values == null)
			{
				instance.Values = new List<LocalizedStringValue>();
			}
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
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.Key = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.DeprecatedValue = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.DeprecatedLocale = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Values.Add(LocalizedStringValue.DeserializeLengthDelimited(stream));
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

		// Token: 0x060015F2 RID: 5618 RVA: 0x0004B2BE File Offset: 0x000494BE
		public void Serialize(Stream stream)
		{
			LocalizedString.Serialize(stream, this);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004B2C8 File Offset: 0x000494C8
		public static void Serialize(Stream stream, LocalizedString instance)
		{
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Key));
			if (instance.HasDeprecatedValue)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeprecatedValue));
			}
			if (instance.HasDeprecatedLocale)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedLocale));
			}
			if (instance.Values.Count > 0)
			{
				foreach (LocalizedStringValue localizedStringValue in instance.Values)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, localizedStringValue.GetSerializedSize());
					LocalizedStringValue.Serialize(stream, localizedStringValue);
				}
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0004B3B8 File Offset: 0x000495B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Key);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasDeprecatedValue)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DeprecatedValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasDeprecatedLocale)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedLocale));
			}
			if (this.Values.Count > 0)
			{
				foreach (LocalizedStringValue localizedStringValue in this.Values)
				{
					num += 1U;
					uint serializedSize = localizedStringValue.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040006B9 RID: 1721
		public bool HasDeprecatedValue;

		// Token: 0x040006BA RID: 1722
		private string _DeprecatedValue;

		// Token: 0x040006BB RID: 1723
		public bool HasDeprecatedLocale;

		// Token: 0x040006BC RID: 1724
		private int _DeprecatedLocale;

		// Token: 0x040006BD RID: 1725
		private List<LocalizedStringValue> _Values = new List<LocalizedStringValue>();
	}
}
