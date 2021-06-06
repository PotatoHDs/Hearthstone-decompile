using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class ClientRequest : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasHost;

		private ProcessId _Host;

		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasProgram;

		private uint _Program;

		public bool HasClientInfo;

		private ClientInfo _ClientInfo;

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

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public EntityId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public EntityId GameAccountId
		{
			get
			{
				return _GameAccountId;
			}
			set
			{
				_GameAccountId = value;
				HasGameAccountId = value != null;
			}
		}

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

		public ClientInfo ClientInfo
		{
			get
			{
				return _ClientInfo;
			}
			set
			{
				_ClientInfo = value;
				HasClientInfo = value != null;
			}
		}

		public bool IsInitialized => true;

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

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void SetAccountId(EntityId val)
		{
			AccountId = val;
		}

		public void SetGameAccountId(EntityId val)
		{
			GameAccountId = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetClientInfo(ClientInfo val)
		{
			ClientInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasGameAccountId)
			{
				num ^= GameAccountId.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasClientInfo)
			{
				num ^= ClientInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientRequest clientRequest = obj as ClientRequest;
			if (clientRequest == null)
			{
				return false;
			}
			if (Attribute.Count != clientRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(clientRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (HasHost != clientRequest.HasHost || (HasHost && !Host.Equals(clientRequest.Host)))
			{
				return false;
			}
			if (HasAccountId != clientRequest.HasAccountId || (HasAccountId && !AccountId.Equals(clientRequest.AccountId)))
			{
				return false;
			}
			if (HasGameAccountId != clientRequest.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(clientRequest.GameAccountId)))
			{
				return false;
			}
			if (HasProgram != clientRequest.HasProgram || (HasProgram && !Program.Equals(clientRequest.Program)))
			{
				return false;
			}
			if (HasClientInfo != clientRequest.HasClientInfo || (HasClientInfo && !ClientInfo.Equals(clientRequest.ClientInfo)))
			{
				return false;
			}
			return true;
		}

		public static ClientRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientRequest Deserialize(Stream stream, ClientRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientRequest DeserializeLengthDelimited(Stream stream)
		{
			ClientRequest clientRequest = new ClientRequest();
			DeserializeLengthDelimited(stream, clientRequest);
			return clientRequest;
		}

		public static ClientRequest DeserializeLengthDelimited(Stream stream, ClientRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientRequest Deserialize(Stream stream, ClientRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 26:
					if (instance.AccountId == null)
					{
						instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 34:
					if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
					continue;
				case 45:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 50:
					if (instance.ClientInfo == null)
					{
						instance.ClientInfo = ClientInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ClientInfo.DeserializeLengthDelimited(stream, instance.ClientInfo);
					}
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

		public static void Serialize(Stream stream, ClientRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasClientInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.ClientInfo.GetSerializedSize());
				ClientInfo.Serialize(stream, instance.ClientInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasHost)
			{
				num++;
				uint serializedSize2 = Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasAccountId)
			{
				num++;
				uint serializedSize3 = AccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasGameAccountId)
			{
				num++;
				uint serializedSize4 = GameAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasClientInfo)
			{
				num++;
				uint serializedSize5 = ClientInfo.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num;
		}
	}
}
