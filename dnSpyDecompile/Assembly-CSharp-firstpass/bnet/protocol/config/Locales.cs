using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	// Token: 0x0200034D RID: 845
	public class Locales : IProtoBuf
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000AF333 File Offset: 0x000AD533
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x000AF33B File Offset: 0x000AD53B
		public List<Locale> Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000AF333 File Offset: 0x000AD533
		public List<Locale> LocaleList
		{
			get
			{
				return this._Locale;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000AF344 File Offset: 0x000AD544
		public int LocaleCount
		{
			get
			{
				return this._Locale.Count;
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000AF351 File Offset: 0x000AD551
		public void AddLocale(Locale val)
		{
			this._Locale.Add(val);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000AF35F File Offset: 0x000AD55F
		public void ClearLocale()
		{
			this._Locale.Clear();
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000AF36C File Offset: 0x000AD56C
		public void SetLocale(List<Locale> val)
		{
			this.Locale = val;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000AF378 File Offset: 0x000AD578
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Locale locale in this.Locale)
			{
				num ^= locale.GetHashCode();
			}
			return num;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000AF3DC File Offset: 0x000AD5DC
		public override bool Equals(object obj)
		{
			Locales locales = obj as Locales;
			if (locales == null)
			{
				return false;
			}
			if (this.Locale.Count != locales.Locale.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Locale.Count; i++)
			{
				if (!this.Locale[i].Equals(locales.Locale[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000AF447 File Offset: 0x000AD647
		public static Locales ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Locales>(bs, 0, -1);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000AF451 File Offset: 0x000AD651
		public void Deserialize(Stream stream)
		{
			Locales.Deserialize(stream, this);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000AF45B File Offset: 0x000AD65B
		public static Locales Deserialize(Stream stream, Locales instance)
		{
			return Locales.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000AF468 File Offset: 0x000AD668
		public static Locales DeserializeLengthDelimited(Stream stream)
		{
			Locales locales = new Locales();
			Locales.DeserializeLengthDelimited(stream, locales);
			return locales;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000AF484 File Offset: 0x000AD684
		public static Locales DeserializeLengthDelimited(Stream stream, Locales instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Locales.Deserialize(stream, instance, num);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000AF4AC File Offset: 0x000AD6AC
		public static Locales Deserialize(Stream stream, Locales instance, long limit)
		{
			if (instance.Locale == null)
			{
				instance.Locale = new List<Locale>();
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
				else if (num == 10)
				{
					instance.Locale.Add(bnet.protocol.config.Locale.DeserializeLengthDelimited(stream));
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

		// Token: 0x060034DB RID: 13531 RVA: 0x000AF544 File Offset: 0x000AD744
		public void Serialize(Stream stream)
		{
			Locales.Serialize(stream, this);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000AF550 File Offset: 0x000AD750
		public static void Serialize(Stream stream, Locales instance)
		{
			if (instance.Locale.Count > 0)
			{
				foreach (Locale locale in instance.Locale)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, locale.GetSerializedSize());
					bnet.protocol.config.Locale.Serialize(stream, locale);
				}
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000AF5C8 File Offset: 0x000AD7C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Locale.Count > 0)
			{
				foreach (Locale locale in this.Locale)
				{
					num += 1U;
					uint serializedSize = locale.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001441 RID: 5185
		private List<Locale> _Locale = new List<Locale>();
	}
}
