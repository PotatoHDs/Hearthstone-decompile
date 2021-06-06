using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	public class RegisterGameServerRequest : IProtoBuf
	{
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasProgram;

		private uint _Program;

		public bool HasServerProperties;

		private GameServerProperties _ServerProperties;

		public bool HasPriority;

		private uint _Priority;

		public bool HasGameServerGuid;

		private ulong _GameServerGuid;

		public List<bnet.protocol.v2.Attribute> Attribute
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

		public List<bnet.protocol.v2.Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public GameServerProperties ServerProperties
		{
			get
			{
				return _ServerProperties;
			}
			set
			{
				_ServerProperties = value;
				HasServerProperties = value != null;
			}
		}

		public uint Priority
		{
			get
			{
				return _Priority;
			}
			set
			{
				_Priority = value;
				HasPriority = true;
			}
		}

		public ulong GameServerGuid
		{
			get
			{
				return _GameServerGuid;
			}
			set
			{
				_GameServerGuid = value;
				HasGameServerGuid = true;
			}
		}

		public bool IsInitialized => true;

		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			Attribute = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetServerProperties(GameServerProperties val)
		{
			ServerProperties = val;
		}

		public void SetPriority(uint val)
		{
			Priority = val;
		}

		public void SetGameServerGuid(ulong val)
		{
			GameServerGuid = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasServerProperties)
			{
				num ^= ServerProperties.GetHashCode();
			}
			if (HasPriority)
			{
				num ^= Priority.GetHashCode();
			}
			if (HasGameServerGuid)
			{
				num ^= GameServerGuid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegisterGameServerRequest registerGameServerRequest = obj as RegisterGameServerRequest;
			if (registerGameServerRequest == null)
			{
				return false;
			}
			if (Attribute.Count != registerGameServerRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(registerGameServerRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (HasProgram != registerGameServerRequest.HasProgram || (HasProgram && !Program.Equals(registerGameServerRequest.Program)))
			{
				return false;
			}
			if (HasServerProperties != registerGameServerRequest.HasServerProperties || (HasServerProperties && !ServerProperties.Equals(registerGameServerRequest.ServerProperties)))
			{
				return false;
			}
			if (HasPriority != registerGameServerRequest.HasPriority || (HasPriority && !Priority.Equals(registerGameServerRequest.Priority)))
			{
				return false;
			}
			if (HasGameServerGuid != registerGameServerRequest.HasGameServerGuid || (HasGameServerGuid && !GameServerGuid.Equals(registerGameServerRequest.GameServerGuid)))
			{
				return false;
			}
			return true;
		}

		public static RegisterGameServerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterGameServerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterGameServerRequest Deserialize(Stream stream, RegisterGameServerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterGameServerRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterGameServerRequest registerGameServerRequest = new RegisterGameServerRequest();
			DeserializeLengthDelimited(stream, registerGameServerRequest);
			return registerGameServerRequest;
		}

		public static RegisterGameServerRequest DeserializeLengthDelimited(Stream stream, RegisterGameServerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterGameServerRequest Deserialize(Stream stream, RegisterGameServerRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.Priority = 0u;
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
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 26:
					if (instance.ServerProperties == null)
					{
						instance.ServerProperties = GameServerProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameServerProperties.DeserializeLengthDelimited(stream, instance.ServerProperties);
					}
					continue;
				case 32:
					instance.Priority = ProtocolParser.ReadUInt32(stream);
					continue;
				case 41:
					instance.GameServerGuid = binaryReader.ReadUInt64();
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

		public static void Serialize(Stream stream, RegisterGameServerRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasServerProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ServerProperties.GetSerializedSize());
				GameServerProperties.Serialize(stream, instance.ServerProperties);
			}
			if (instance.HasPriority)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Priority);
			}
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasServerProperties)
			{
				num++;
				uint serializedSize2 = ServerProperties.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasPriority)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Priority);
			}
			if (HasGameServerGuid)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
