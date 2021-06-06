using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DB RID: 987
	public class ConnectInfo : IProtoBuf
	{
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000CE77A File Offset: 0x000CC97A
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000CE782 File Offset: 0x000CC982
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

		// Token: 0x060040DA RID: 16602 RVA: 0x000CE795 File Offset: 0x000CC995
		public void SetAddress(Address val)
		{
			this.Address = val;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x000CE79E File Offset: 0x000CC99E
		// (set) Token: 0x060040DC RID: 16604 RVA: 0x000CE7A6 File Offset: 0x000CC9A6
		public List<bnet.protocol.v2.Attribute> Attribute
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

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000CE79E File Offset: 0x000CC99E
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x000CE7AF File Offset: 0x000CC9AF
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000CE7BC File Offset: 0x000CC9BC
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000CE7CA File Offset: 0x000CC9CA
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000CE7D7 File Offset: 0x000CC9D7
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000CE7E0 File Offset: 0x000CC9E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAddress)
			{
				num ^= this.Address.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000CE858 File Offset: 0x000CCA58
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

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000CE8EE File Offset: 0x000CCAEE
		public static ConnectInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectInfo>(bs, 0, -1);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000CE8F8 File Offset: 0x000CCAF8
		public void Deserialize(Stream stream)
		{
			ConnectInfo.Deserialize(stream, this);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000CE902 File Offset: 0x000CCB02
		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance)
		{
			return ConnectInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000CE910 File Offset: 0x000CCB10
		public static ConnectInfo DeserializeLengthDelimited(Stream stream)
		{
			ConnectInfo connectInfo = new ConnectInfo();
			ConnectInfo.DeserializeLengthDelimited(stream, connectInfo);
			return connectInfo;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000CE92C File Offset: 0x000CCB2C
		public static ConnectInfo DeserializeLengthDelimited(Stream stream, ConnectInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000CE954 File Offset: 0x000CCB54
		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x060040EB RID: 16619 RVA: 0x000CEA1E File Offset: 0x000CCC1E
		public void Serialize(Stream stream)
		{
			ConnectInfo.Serialize(stream, this);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000CEA28 File Offset: 0x000CCC28
		public static void Serialize(Stream stream, ConnectInfo instance)
		{
			if (instance.HasAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				Address.Serialize(stream, instance.Address);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000CEACC File Offset: 0x000CCCCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAddress)
			{
				num += 1U;
				uint serializedSize = this.Address.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001682 RID: 5762
		public bool HasAddress;

		// Token: 0x04001683 RID: 5763
		private Address _Address;

		// Token: 0x04001684 RID: 5764
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
