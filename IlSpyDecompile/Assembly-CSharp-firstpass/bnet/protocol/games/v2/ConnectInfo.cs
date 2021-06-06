using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v2
{
	public class ConnectInfo : IProtoBuf
	{
		public bool HasAddress;

		private Address _Address;

		public bool HasToken;

		private byte[] _Token;

		private List<Attribute> _Attribute = new List<Attribute>();

		public Address Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address = value;
				HasAddress = value != null;
			}
		}

		public byte[] Token
		{
			get
			{
				return _Token;
			}
			set
			{
				_Token = value;
				HasToken = value != null;
			}
		}

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public bool IsInitialized => true;

		public void SetAddress(Address val)
		{
			Address = val;
		}

		public void SetToken(byte[] val)
		{
			Token = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAddress)
			{
				num ^= Address.GetHashCode();
			}
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectInfo connectInfo = obj as ConnectInfo;
			if (connectInfo == null)
			{
				return false;
			}
			if (HasAddress != connectInfo.HasAddress || (HasAddress && !Address.Equals(connectInfo.Address)))
			{
				return false;
			}
			if (HasToken != connectInfo.HasToken || (HasToken && !Token.Equals(connectInfo.Token)))
			{
				return false;
			}
			if (Attribute.Count != connectInfo.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(connectInfo.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ConnectInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ConnectInfo DeserializeLengthDelimited(Stream stream)
		{
			ConnectInfo connectInfo = new ConnectInfo();
			DeserializeLengthDelimited(stream, connectInfo);
			return connectInfo;
		}

		public static ConnectInfo DeserializeLengthDelimited(Stream stream, ConnectInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ConnectInfo Deserialize(Stream stream, ConnectInfo instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.Address == null)
					{
						instance.Address = Address.DeserializeLengthDelimited(stream);
					}
					else
					{
						Address.DeserializeLengthDelimited(stream, instance.Address);
					}
					continue;
				case 18:
					instance.Token = ProtocolParser.ReadBytes(stream);
					continue;
				case 26:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

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
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAddress)
			{
				num++;
				uint serializedSize = Address.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasToken)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Token.Length) + Token.Length);
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
