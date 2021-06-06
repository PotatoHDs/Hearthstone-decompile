using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	public class ConnectInfo : IProtoBuf
	{
		public bool HasToken;

		private byte[] _Token;

		private List<Attribute> _Attribute = new List<Attribute>();

		public EntityId GameAccountId { get; set; }

		public string Host { get; set; }

		public int Port { get; set; }

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

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetHost(string val)
		{
			Host = val;
		}

		public void SetPort(int val)
		{
			Port = val;
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
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameAccountId.GetHashCode();
			hashCode ^= Host.GetHashCode();
			hashCode ^= Port.GetHashCode();
			if (HasToken)
			{
				hashCode ^= Token.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ConnectInfo connectInfo = obj as ConnectInfo;
			if (connectInfo == null)
			{
				return false;
			}
			if (!GameAccountId.Equals(connectInfo.GameAccountId))
			{
				return false;
			}
			if (!Host.Equals(connectInfo.Host))
			{
				return false;
			}
			if (!Port.Equals(connectInfo.Port))
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
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 18:
					instance.Host = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.Port = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Token = ProtocolParser.ReadBytes(stream);
					continue;
				case 42:
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
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Port);
			if (instance.HasToken)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.Token);
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Host);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)Port);
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
			}
			return num + 3;
		}
	}
}
