using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A0 RID: 928
	public class ConnectInfo : IProtoBuf
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x000C128F File Offset: 0x000BF48F
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x000C1297 File Offset: 0x000BF497
		public EntityId GameAccountId { get; set; }

		// Token: 0x06003BCC RID: 15308 RVA: 0x000C12A0 File Offset: 0x000BF4A0
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000C12A9 File Offset: 0x000BF4A9
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x000C12B1 File Offset: 0x000BF4B1
		public string Host { get; set; }

		// Token: 0x06003BCF RID: 15311 RVA: 0x000C12BA File Offset: 0x000BF4BA
		public void SetHost(string val)
		{
			this.Host = val;
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000C12C3 File Offset: 0x000BF4C3
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000C12CB File Offset: 0x000BF4CB
		public int Port { get; set; }

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000C12D4 File Offset: 0x000BF4D4
		public void SetPort(int val)
		{
			this.Port = val;
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000C12DD File Offset: 0x000BF4DD
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000C12E5 File Offset: 0x000BF4E5
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

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000C12F8 File Offset: 0x000BF4F8
		public void SetToken(byte[] val)
		{
			this.Token = val;
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000C1301 File Offset: 0x000BF501
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x000C1309 File Offset: 0x000BF509
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

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000C1301 File Offset: 0x000BF501
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000C1312 File Offset: 0x000BF512
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000C131F File Offset: 0x000BF51F
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000C132D File Offset: 0x000BF52D
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000C133A File Offset: 0x000BF53A
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000C1344 File Offset: 0x000BF544
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccountId.GetHashCode();
			num ^= this.Host.GetHashCode();
			num ^= this.Port.GetHashCode();
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

		// Token: 0x06003BDE RID: 15326 RVA: 0x000C13EC File Offset: 0x000BF5EC
		public override bool Equals(object obj)
		{
			ConnectInfo connectInfo = obj as ConnectInfo;
			if (connectInfo == null)
			{
				return false;
			}
			if (!this.GameAccountId.Equals(connectInfo.GameAccountId))
			{
				return false;
			}
			if (!this.Host.Equals(connectInfo.Host))
			{
				return false;
			}
			if (!this.Port.Equals(connectInfo.Port))
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

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000C14C4 File Offset: 0x000BF6C4
		public static ConnectInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectInfo>(bs, 0, -1);
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000C14CE File Offset: 0x000BF6CE
		public void Deserialize(Stream stream)
		{
			ConnectInfo.Deserialize(stream, this);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000C14D8 File Offset: 0x000BF6D8
		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance)
		{
			return ConnectInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000C14E4 File Offset: 0x000BF6E4
		public static ConnectInfo DeserializeLengthDelimited(Stream stream)
		{
			ConnectInfo connectInfo = new ConnectInfo();
			ConnectInfo.DeserializeLengthDelimited(stream, connectInfo);
			return connectInfo;
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000C1500 File Offset: 0x000BF700
		public static ConnectInfo DeserializeLengthDelimited(Stream stream, ConnectInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000C1528 File Offset: 0x000BF728
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Host = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.GameAccountId == null)
							{
								instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Port = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Token = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000C1648 File Offset: 0x000BF848
		public void Serialize(Stream stream)
		{
			ConnectInfo.Serialize(stream, this);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000C1654 File Offset: 0x000BF854
		public static void Serialize(Stream stream, ConnectInfo instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.Host == null)
			{
				throw new ArgumentNullException("Host", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Host));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Port));
			if (instance.HasToken)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.Token);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000C1770 File Offset: 0x000BF970
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Host);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Port));
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
			num += 3U;
			return num;
		}

		// Token: 0x0400158E RID: 5518
		public bool HasToken;

		// Token: 0x0400158F RID: 5519
		private byte[] _Token;

		// Token: 0x04001590 RID: 5520
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
