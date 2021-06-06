using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class KafkaHeader : IProtoBuf
	{
		public bool HasServiceHash;

		private uint _ServiceHash;

		public bool HasMethodId;

		private uint _MethodId;

		public bool HasToken;

		private uint _Token;

		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasSize;

		private uint _Size;

		public bool HasStatus;

		private uint _Status;

		public bool HasTimeout;

		private ulong _Timeout;

		public bool HasForwardTarget;

		private ProcessId _ForwardTarget;

		public bool HasReturnTopic;

		private string _ReturnTopic;

		public bool HasClientId;

		private string _ClientId;

		public uint ServiceHash
		{
			get
			{
				return _ServiceHash;
			}
			set
			{
				_ServiceHash = value;
				HasServiceHash = true;
			}
		}

		public uint MethodId
		{
			get
			{
				return _MethodId;
			}
			set
			{
				_MethodId = value;
				HasMethodId = true;
			}
		}

		public uint Token
		{
			get
			{
				return _Token;
			}
			set
			{
				_Token = value;
				HasToken = true;
			}
		}

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public uint Size
		{
			get
			{
				return _Size;
			}
			set
			{
				_Size = value;
				HasSize = true;
			}
		}

		public uint Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status = value;
				HasStatus = true;
			}
		}

		public ulong Timeout
		{
			get
			{
				return _Timeout;
			}
			set
			{
				_Timeout = value;
				HasTimeout = true;
			}
		}

		public ProcessId ForwardTarget
		{
			get
			{
				return _ForwardTarget;
			}
			set
			{
				_ForwardTarget = value;
				HasForwardTarget = value != null;
			}
		}

		public string ReturnTopic
		{
			get
			{
				return _ReturnTopic;
			}
			set
			{
				_ReturnTopic = value;
				HasReturnTopic = value != null;
			}
		}

		public string ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetServiceHash(uint val)
		{
			ServiceHash = val;
		}

		public void SetMethodId(uint val)
		{
			MethodId = val;
		}

		public void SetToken(uint val)
		{
			Token = val;
		}

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetSize(uint val)
		{
			Size = val;
		}

		public void SetStatus(uint val)
		{
			Status = val;
		}

		public void SetTimeout(ulong val)
		{
			Timeout = val;
		}

		public void SetForwardTarget(ProcessId val)
		{
			ForwardTarget = val;
		}

		public void SetReturnTopic(string val)
		{
			ReturnTopic = val;
		}

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServiceHash)
			{
				num ^= ServiceHash.GetHashCode();
			}
			if (HasMethodId)
			{
				num ^= MethodId.GetHashCode();
			}
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			if (HasSize)
			{
				num ^= Size.GetHashCode();
			}
			if (HasStatus)
			{
				num ^= Status.GetHashCode();
			}
			if (HasTimeout)
			{
				num ^= Timeout.GetHashCode();
			}
			if (HasForwardTarget)
			{
				num ^= ForwardTarget.GetHashCode();
			}
			if (HasReturnTopic)
			{
				num ^= ReturnTopic.GetHashCode();
			}
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			KafkaHeader kafkaHeader = obj as KafkaHeader;
			if (kafkaHeader == null)
			{
				return false;
			}
			if (HasServiceHash != kafkaHeader.HasServiceHash || (HasServiceHash && !ServiceHash.Equals(kafkaHeader.ServiceHash)))
			{
				return false;
			}
			if (HasMethodId != kafkaHeader.HasMethodId || (HasMethodId && !MethodId.Equals(kafkaHeader.MethodId)))
			{
				return false;
			}
			if (HasToken != kafkaHeader.HasToken || (HasToken && !Token.Equals(kafkaHeader.Token)))
			{
				return false;
			}
			if (HasObjectId != kafkaHeader.HasObjectId || (HasObjectId && !ObjectId.Equals(kafkaHeader.ObjectId)))
			{
				return false;
			}
			if (HasSize != kafkaHeader.HasSize || (HasSize && !Size.Equals(kafkaHeader.Size)))
			{
				return false;
			}
			if (HasStatus != kafkaHeader.HasStatus || (HasStatus && !Status.Equals(kafkaHeader.Status)))
			{
				return false;
			}
			if (HasTimeout != kafkaHeader.HasTimeout || (HasTimeout && !Timeout.Equals(kafkaHeader.Timeout)))
			{
				return false;
			}
			if (HasForwardTarget != kafkaHeader.HasForwardTarget || (HasForwardTarget && !ForwardTarget.Equals(kafkaHeader.ForwardTarget)))
			{
				return false;
			}
			if (HasReturnTopic != kafkaHeader.HasReturnTopic || (HasReturnTopic && !ReturnTopic.Equals(kafkaHeader.ReturnTopic)))
			{
				return false;
			}
			if (HasClientId != kafkaHeader.HasClientId || (HasClientId && !ClientId.Equals(kafkaHeader.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static KafkaHeader ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<KafkaHeader>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static KafkaHeader Deserialize(Stream stream, KafkaHeader instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static KafkaHeader DeserializeLengthDelimited(Stream stream)
		{
			KafkaHeader kafkaHeader = new KafkaHeader();
			DeserializeLengthDelimited(stream, kafkaHeader);
			return kafkaHeader;
		}

		public static KafkaHeader DeserializeLengthDelimited(Stream stream, KafkaHeader instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static KafkaHeader Deserialize(Stream stream, KafkaHeader instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ObjectId = 0uL;
			instance.Size = 0u;
			instance.Status = 0u;
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
				case 13:
					instance.ServiceHash = binaryReader.ReadUInt32();
					continue;
				case 16:
					instance.MethodId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.Token = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Size = ProtocolParser.ReadUInt32(stream);
					continue;
				case 48:
					instance.Status = ProtocolParser.ReadUInt32(stream);
					continue;
				case 56:
					instance.Timeout = ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					if (instance.ForwardTarget == null)
					{
						instance.ForwardTarget = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ForwardTarget);
					}
					continue;
				case 74:
					instance.ReturnTopic = ProtocolParser.ReadString(stream);
					continue;
				case 90:
					instance.ClientId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, KafkaHeader instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceHash)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.ServiceHash);
			}
			if (instance.HasMethodId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasSize)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Size);
			}
			if (instance.HasStatus)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Status);
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.Timeout);
			}
			if (instance.HasForwardTarget)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.ForwardTarget.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ForwardTarget);
			}
			if (instance.HasReturnTopic)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReturnTopic));
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServiceHash)
			{
				num++;
				num += 4;
			}
			if (HasMethodId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MethodId);
			}
			if (HasToken)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Token);
			}
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			if (HasSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Size);
			}
			if (HasStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Status);
			}
			if (HasTimeout)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Timeout);
			}
			if (HasForwardTarget)
			{
				num++;
				uint serializedSize = ForwardTarget.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasReturnTopic)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ReturnTopic);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasClientId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
