using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x02000368 RID: 872
	public class ConnectInfo : IProtoBuf
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000B5984 File Offset: 0x000B3B84
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x000B598C File Offset: 0x000B3B8C
		public Address Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this._Address = value;
				this.HasAddress = (value != null);
			}
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000B599F File Offset: 0x000B3B9F
		public void SetAddress(Address val)
		{
			this.Address = val;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000B59A8 File Offset: 0x000B3BA8
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x000B59B0 File Offset: 0x000B3BB0
		public byte[] Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = (value != null);
			}
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000B59C3 File Offset: 0x000B3BC3
		public void SetToken(byte[] val)
		{
			this.Token = val;
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000B59CC File Offset: 0x000B3BCC
		// (set) Token: 0x0600373A RID: 14138 RVA: 0x000B59D4 File Offset: 0x000B3BD4
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000B59CC File Offset: 0x000B3BCC
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000B59DD File Offset: 0x000B3BDD
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000B59EA File Offset: 0x000B3BEA
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000B59F8 File Offset: 0x000B3BF8
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000B5A05 File Offset: 0x000B3C05
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000B5A10 File Offset: 0x000B3C10
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAddress)
			{
				num ^= this.Address.GetHashCode();
			}
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000B5AA0 File Offset: 0x000B3CA0
		public override bool Equals(object obj)
		{
			ConnectInfo connectInfo = obj as ConnectInfo;
			if (connectInfo == null)
			{
				return false;
			}
			if (this.HasAddress != connectInfo.HasAddress || (this.HasAddress && !this.Address.Equals(connectInfo.Address)))
			{
				return false;
			}
			if (this.HasToken != connectInfo.HasToken || (this.HasToken && !this.Token.Equals(connectInfo.Token)))
			{
				return false;
			}
			if (this.Attribute.Count != connectInfo.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(connectInfo.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000B5B61 File Offset: 0x000B3D61
		public static ConnectInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectInfo>(bs, 0, -1);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000B5B6B File Offset: 0x000B3D6B
		public void Deserialize(Stream stream)
		{
			ConnectInfo.Deserialize(stream, this);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000B5B75 File Offset: 0x000B3D75
		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance)
		{
			return ConnectInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000B5B80 File Offset: 0x000B3D80
		public static ConnectInfo DeserializeLengthDelimited(Stream stream)
		{
			ConnectInfo connectInfo = new ConnectInfo();
			ConnectInfo.DeserializeLengthDelimited(stream, connectInfo);
			return connectInfo;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000B5B9C File Offset: 0x000B3D9C
		public static ConnectInfo DeserializeLengthDelimited(Stream stream, ConnectInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000B5BC4 File Offset: 0x000B3DC4
		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Token = ProtocolParser.ReadBytes(stream);
					}
				}
				else if (instance.Address == null)
				{
					instance.Address = Address.DeserializeLengthDelimited(stream);
				}
				else
				{
					Address.DeserializeLengthDelimited(stream, instance.Address);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000B5CAA File Offset: 0x000B3EAA
		public void Serialize(Stream stream)
		{
			ConnectInfo.Serialize(stream, this);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000B5CB4 File Offset: 0x000B3EB4
		public static void Serialize(Stream stream, ConnectInfo instance)
		{
			if (instance.HasAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				Address.Serialize(stream, instance.Address);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Token);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000B5D74 File Offset: 0x000B3F74
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAddress)
			{
				num += 1U;
				uint serializedSize = this.Address.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasToken)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Token.Length) + (uint)this.Token.Length;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040014B0 RID: 5296
		public bool HasAddress;

		// Token: 0x040014B1 RID: 5297
		private Address _Address;

		// Token: 0x040014B2 RID: 5298
		public bool HasToken;

		// Token: 0x040014B3 RID: 5299
		private byte[] _Token;

		// Token: 0x040014B4 RID: 5300
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
