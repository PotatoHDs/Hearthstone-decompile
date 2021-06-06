using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000537 RID: 1335
	public class GameSessionLocation : IProtoBuf
	{
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x001233EF File Offset: 0x001215EF
		// (set) Token: 0x06006030 RID: 24624 RVA: 0x001233F7 File Offset: 0x001215F7
		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				this._IpAddress = value;
				this.HasIpAddress = (value != null);
			}
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x0012340A File Offset: 0x0012160A
		public void SetIpAddress(string val)
		{
			this.IpAddress = val;
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06006032 RID: 24626 RVA: 0x00123413 File Offset: 0x00121613
		// (set) Token: 0x06006033 RID: 24627 RVA: 0x0012341B File Offset: 0x0012161B
		public uint Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
				this.HasCountry = true;
			}
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x0012342B File Offset: 0x0012162B
		public void SetCountry(uint val)
		{
			this.Country = val;
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06006035 RID: 24629 RVA: 0x00123434 File Offset: 0x00121634
		// (set) Token: 0x06006036 RID: 24630 RVA: 0x0012343C File Offset: 0x0012163C
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
				this.HasCity = (value != null);
			}
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x0012344F File Offset: 0x0012164F
		public void SetCity(string val)
		{
			this.City = val;
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x00123458 File Offset: 0x00121658
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIpAddress)
			{
				num ^= this.IpAddress.GetHashCode();
			}
			if (this.HasCountry)
			{
				num ^= this.Country.GetHashCode();
			}
			if (this.HasCity)
			{
				num ^= this.City.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x001234B8 File Offset: 0x001216B8
		public override bool Equals(object obj)
		{
			GameSessionLocation gameSessionLocation = obj as GameSessionLocation;
			return gameSessionLocation != null && this.HasIpAddress == gameSessionLocation.HasIpAddress && (!this.HasIpAddress || this.IpAddress.Equals(gameSessionLocation.IpAddress)) && this.HasCountry == gameSessionLocation.HasCountry && (!this.HasCountry || this.Country.Equals(gameSessionLocation.Country)) && this.HasCity == gameSessionLocation.HasCity && (!this.HasCity || this.City.Equals(gameSessionLocation.City));
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x00123556 File Offset: 0x00121756
		public static GameSessionLocation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionLocation>(bs, 0, -1);
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x00123560 File Offset: 0x00121760
		public void Deserialize(Stream stream)
		{
			GameSessionLocation.Deserialize(stream, this);
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x0012356A File Offset: 0x0012176A
		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance)
		{
			return GameSessionLocation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x00123578 File Offset: 0x00121778
		public static GameSessionLocation DeserializeLengthDelimited(Stream stream)
		{
			GameSessionLocation gameSessionLocation = new GameSessionLocation();
			GameSessionLocation.DeserializeLengthDelimited(stream, gameSessionLocation);
			return gameSessionLocation;
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x00123594 File Offset: 0x00121794
		public static GameSessionLocation DeserializeLengthDelimited(Stream stream, GameSessionLocation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionLocation.Deserialize(stream, instance, num);
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x001235BC File Offset: 0x001217BC
		public static GameSessionLocation Deserialize(Stream stream, GameSessionLocation instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
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
							instance.City = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Country = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.IpAddress = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0012366A File Offset: 0x0012186A
		public void Serialize(Stream stream)
		{
			GameSessionLocation.Serialize(stream, this);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x00123674 File Offset: 0x00121874
		public static void Serialize(Stream stream, GameSessionLocation instance)
		{
			if (instance.HasIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IpAddress));
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Country);
			}
			if (instance.HasCity)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.City));
			}
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x001236EC File Offset: 0x001218EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIpAddress)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.IpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCountry)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Country);
			}
			if (this.HasCity)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.City);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04001DA7 RID: 7591
		public bool HasIpAddress;

		// Token: 0x04001DA8 RID: 7592
		private string _IpAddress;

		// Token: 0x04001DA9 RID: 7593
		public bool HasCountry;

		// Token: 0x04001DAA RID: 7594
		private uint _Country;

		// Token: 0x04001DAB RID: 7595
		public bool HasCity;

		// Token: 0x04001DAC RID: 7596
		private string _City;
	}
}
