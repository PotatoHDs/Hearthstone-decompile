using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	// Token: 0x0200034C RID: 844
	public class Locale : IProtoBuf
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000AEEC3 File Offset: 0x000AD0C3
		// (set) Token: 0x060034B2 RID: 13490 RVA: 0x000AEECB File Offset: 0x000AD0CB
		public string Identifier { get; set; }

		// Token: 0x060034B3 RID: 13491 RVA: 0x000AEED4 File Offset: 0x000AD0D4
		public void SetIdentifier(string val)
		{
			this.Identifier = val;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000AEEDD File Offset: 0x000AD0DD
		// (set) Token: 0x060034B5 RID: 13493 RVA: 0x000AEEE5 File Offset: 0x000AD0E5
		public string Description { get; set; }

		// Token: 0x060034B6 RID: 13494 RVA: 0x000AEEEE File Offset: 0x000AD0EE
		public void SetDescription(string val)
		{
			this.Description = val;
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000AEEF7 File Offset: 0x000AD0F7
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x000AEEFF File Offset: 0x000AD0FF
		public List<string> Flag
		{
			get
			{
				return this._Flag;
			}
			set
			{
				this._Flag = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000AEEF7 File Offset: 0x000AD0F7
		public List<string> FlagList
		{
			get
			{
				return this._Flag;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000AEF08 File Offset: 0x000AD108
		public int FlagCount
		{
			get
			{
				return this._Flag.Count;
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000AEF15 File Offset: 0x000AD115
		public void AddFlag(string val)
		{
			this._Flag.Add(val);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000AEF23 File Offset: 0x000AD123
		public void ClearFlag()
		{
			this._Flag.Clear();
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000AEF30 File Offset: 0x000AD130
		public void SetFlag(List<string> val)
		{
			this.Flag = val;
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000AEF3C File Offset: 0x000AD13C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Identifier.GetHashCode();
			num ^= this.Description.GetHashCode();
			foreach (string text in this.Flag)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000AEFBC File Offset: 0x000AD1BC
		public override bool Equals(object obj)
		{
			Locale locale = obj as Locale;
			if (locale == null)
			{
				return false;
			}
			if (!this.Identifier.Equals(locale.Identifier))
			{
				return false;
			}
			if (!this.Description.Equals(locale.Description))
			{
				return false;
			}
			if (this.Flag.Count != locale.Flag.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Flag.Count; i++)
			{
				if (!this.Flag[i].Equals(locale.Flag[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000AF051 File Offset: 0x000AD251
		public static Locale ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Locale>(bs, 0, -1);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000AF05B File Offset: 0x000AD25B
		public void Deserialize(Stream stream)
		{
			Locale.Deserialize(stream, this);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000AF065 File Offset: 0x000AD265
		public static Locale Deserialize(Stream stream, Locale instance)
		{
			return Locale.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000AF070 File Offset: 0x000AD270
		public static Locale DeserializeLengthDelimited(Stream stream)
		{
			Locale locale = new Locale();
			Locale.DeserializeLengthDelimited(stream, locale);
			return locale;
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000AF08C File Offset: 0x000AD28C
		public static Locale DeserializeLengthDelimited(Stream stream, Locale instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Locale.Deserialize(stream, instance, num);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000AF0B4 File Offset: 0x000AD2B4
		public static Locale Deserialize(Stream stream, Locale instance, long limit)
		{
			if (instance.Flag == null)
			{
				instance.Flag = new List<string>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Flag.Add(ProtocolParser.ReadString(stream));
						}
					}
					else
					{
						instance.Description = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Identifier = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000AF17A File Offset: 0x000AD37A
		public void Serialize(Stream stream)
		{
			Locale.Serialize(stream, this);
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000AF184 File Offset: 0x000AD384
		public static void Serialize(Stream stream, Locale instance)
		{
			if (instance.Identifier == null)
			{
				throw new ArgumentNullException("Identifier", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identifier));
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			if (instance.Flag.Count > 0)
			{
				foreach (string s in instance.Flag)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000AF264 File Offset: 0x000AD464
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Identifier);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Description);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.Flag.Count > 0)
			{
				foreach (string s in this.Flag)
				{
					num += 1U;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x04001440 RID: 5184
		private List<string> _Flag = new List<string>();
	}
}
