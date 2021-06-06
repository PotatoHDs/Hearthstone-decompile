using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	public class RPCMethodConfig : IProtoBuf
	{
		public bool HasServiceName;

		private string _ServiceName;

		public bool HasMethodName;

		private string _MethodName;

		public bool HasFixedCallCost;

		private uint _FixedCallCost;

		public bool HasFixedPacketSize;

		private uint _FixedPacketSize;

		public bool HasVariableMultiplier;

		private float _VariableMultiplier;

		public bool HasMultiplier;

		private float _Multiplier;

		public bool HasRateLimitCount;

		private uint _RateLimitCount;

		public bool HasRateLimitSeconds;

		private uint _RateLimitSeconds;

		public bool HasMaxPacketSize;

		private uint _MaxPacketSize;

		public bool HasMaxEncodedSize;

		private uint _MaxEncodedSize;

		public bool HasTimeout;

		private float _Timeout;

		public bool HasCapBalance;

		private uint _CapBalance;

		public bool HasIncomePerSecond;

		private float _IncomePerSecond;

		public bool HasServiceHash;

		private uint _ServiceHash;

		public bool HasMethodId;

		private uint _MethodId;

		public string ServiceName
		{
			get
			{
				return _ServiceName;
			}
			set
			{
				_ServiceName = value;
				HasServiceName = value != null;
			}
		}

		public string MethodName
		{
			get
			{
				return _MethodName;
			}
			set
			{
				_MethodName = value;
				HasMethodName = value != null;
			}
		}

		public uint FixedCallCost
		{
			get
			{
				return _FixedCallCost;
			}
			set
			{
				_FixedCallCost = value;
				HasFixedCallCost = true;
			}
		}

		public uint FixedPacketSize
		{
			get
			{
				return _FixedPacketSize;
			}
			set
			{
				_FixedPacketSize = value;
				HasFixedPacketSize = true;
			}
		}

		public float VariableMultiplier
		{
			get
			{
				return _VariableMultiplier;
			}
			set
			{
				_VariableMultiplier = value;
				HasVariableMultiplier = true;
			}
		}

		public float Multiplier
		{
			get
			{
				return _Multiplier;
			}
			set
			{
				_Multiplier = value;
				HasMultiplier = true;
			}
		}

		public uint RateLimitCount
		{
			get
			{
				return _RateLimitCount;
			}
			set
			{
				_RateLimitCount = value;
				HasRateLimitCount = true;
			}
		}

		public uint RateLimitSeconds
		{
			get
			{
				return _RateLimitSeconds;
			}
			set
			{
				_RateLimitSeconds = value;
				HasRateLimitSeconds = true;
			}
		}

		public uint MaxPacketSize
		{
			get
			{
				return _MaxPacketSize;
			}
			set
			{
				_MaxPacketSize = value;
				HasMaxPacketSize = true;
			}
		}

		public uint MaxEncodedSize
		{
			get
			{
				return _MaxEncodedSize;
			}
			set
			{
				_MaxEncodedSize = value;
				HasMaxEncodedSize = true;
			}
		}

		public float Timeout
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

		public uint CapBalance
		{
			get
			{
				return _CapBalance;
			}
			set
			{
				_CapBalance = value;
				HasCapBalance = true;
			}
		}

		public float IncomePerSecond
		{
			get
			{
				return _IncomePerSecond;
			}
			set
			{
				_IncomePerSecond = value;
				HasIncomePerSecond = true;
			}
		}

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

		public bool IsInitialized => true;

		public void SetServiceName(string val)
		{
			ServiceName = val;
		}

		public void SetMethodName(string val)
		{
			MethodName = val;
		}

		public void SetFixedCallCost(uint val)
		{
			FixedCallCost = val;
		}

		public void SetFixedPacketSize(uint val)
		{
			FixedPacketSize = val;
		}

		public void SetVariableMultiplier(float val)
		{
			VariableMultiplier = val;
		}

		public void SetMultiplier(float val)
		{
			Multiplier = val;
		}

		public void SetRateLimitCount(uint val)
		{
			RateLimitCount = val;
		}

		public void SetRateLimitSeconds(uint val)
		{
			RateLimitSeconds = val;
		}

		public void SetMaxPacketSize(uint val)
		{
			MaxPacketSize = val;
		}

		public void SetMaxEncodedSize(uint val)
		{
			MaxEncodedSize = val;
		}

		public void SetTimeout(float val)
		{
			Timeout = val;
		}

		public void SetCapBalance(uint val)
		{
			CapBalance = val;
		}

		public void SetIncomePerSecond(float val)
		{
			IncomePerSecond = val;
		}

		public void SetServiceHash(uint val)
		{
			ServiceHash = val;
		}

		public void SetMethodId(uint val)
		{
			MethodId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServiceName)
			{
				num ^= ServiceName.GetHashCode();
			}
			if (HasMethodName)
			{
				num ^= MethodName.GetHashCode();
			}
			if (HasFixedCallCost)
			{
				num ^= FixedCallCost.GetHashCode();
			}
			if (HasFixedPacketSize)
			{
				num ^= FixedPacketSize.GetHashCode();
			}
			if (HasVariableMultiplier)
			{
				num ^= VariableMultiplier.GetHashCode();
			}
			if (HasMultiplier)
			{
				num ^= Multiplier.GetHashCode();
			}
			if (HasRateLimitCount)
			{
				num ^= RateLimitCount.GetHashCode();
			}
			if (HasRateLimitSeconds)
			{
				num ^= RateLimitSeconds.GetHashCode();
			}
			if (HasMaxPacketSize)
			{
				num ^= MaxPacketSize.GetHashCode();
			}
			if (HasMaxEncodedSize)
			{
				num ^= MaxEncodedSize.GetHashCode();
			}
			if (HasTimeout)
			{
				num ^= Timeout.GetHashCode();
			}
			if (HasCapBalance)
			{
				num ^= CapBalance.GetHashCode();
			}
			if (HasIncomePerSecond)
			{
				num ^= IncomePerSecond.GetHashCode();
			}
			if (HasServiceHash)
			{
				num ^= ServiceHash.GetHashCode();
			}
			if (HasMethodId)
			{
				num ^= MethodId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RPCMethodConfig rPCMethodConfig = obj as RPCMethodConfig;
			if (rPCMethodConfig == null)
			{
				return false;
			}
			if (HasServiceName != rPCMethodConfig.HasServiceName || (HasServiceName && !ServiceName.Equals(rPCMethodConfig.ServiceName)))
			{
				return false;
			}
			if (HasMethodName != rPCMethodConfig.HasMethodName || (HasMethodName && !MethodName.Equals(rPCMethodConfig.MethodName)))
			{
				return false;
			}
			if (HasFixedCallCost != rPCMethodConfig.HasFixedCallCost || (HasFixedCallCost && !FixedCallCost.Equals(rPCMethodConfig.FixedCallCost)))
			{
				return false;
			}
			if (HasFixedPacketSize != rPCMethodConfig.HasFixedPacketSize || (HasFixedPacketSize && !FixedPacketSize.Equals(rPCMethodConfig.FixedPacketSize)))
			{
				return false;
			}
			if (HasVariableMultiplier != rPCMethodConfig.HasVariableMultiplier || (HasVariableMultiplier && !VariableMultiplier.Equals(rPCMethodConfig.VariableMultiplier)))
			{
				return false;
			}
			if (HasMultiplier != rPCMethodConfig.HasMultiplier || (HasMultiplier && !Multiplier.Equals(rPCMethodConfig.Multiplier)))
			{
				return false;
			}
			if (HasRateLimitCount != rPCMethodConfig.HasRateLimitCount || (HasRateLimitCount && !RateLimitCount.Equals(rPCMethodConfig.RateLimitCount)))
			{
				return false;
			}
			if (HasRateLimitSeconds != rPCMethodConfig.HasRateLimitSeconds || (HasRateLimitSeconds && !RateLimitSeconds.Equals(rPCMethodConfig.RateLimitSeconds)))
			{
				return false;
			}
			if (HasMaxPacketSize != rPCMethodConfig.HasMaxPacketSize || (HasMaxPacketSize && !MaxPacketSize.Equals(rPCMethodConfig.MaxPacketSize)))
			{
				return false;
			}
			if (HasMaxEncodedSize != rPCMethodConfig.HasMaxEncodedSize || (HasMaxEncodedSize && !MaxEncodedSize.Equals(rPCMethodConfig.MaxEncodedSize)))
			{
				return false;
			}
			if (HasTimeout != rPCMethodConfig.HasTimeout || (HasTimeout && !Timeout.Equals(rPCMethodConfig.Timeout)))
			{
				return false;
			}
			if (HasCapBalance != rPCMethodConfig.HasCapBalance || (HasCapBalance && !CapBalance.Equals(rPCMethodConfig.CapBalance)))
			{
				return false;
			}
			if (HasIncomePerSecond != rPCMethodConfig.HasIncomePerSecond || (HasIncomePerSecond && !IncomePerSecond.Equals(rPCMethodConfig.IncomePerSecond)))
			{
				return false;
			}
			if (HasServiceHash != rPCMethodConfig.HasServiceHash || (HasServiceHash && !ServiceHash.Equals(rPCMethodConfig.ServiceHash)))
			{
				return false;
			}
			if (HasMethodId != rPCMethodConfig.HasMethodId || (HasMethodId && !MethodId.Equals(rPCMethodConfig.MethodId)))
			{
				return false;
			}
			return true;
		}

		public static RPCMethodConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMethodConfig>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMethodConfig rPCMethodConfig = new RPCMethodConfig();
			DeserializeLengthDelimited(stream, rPCMethodConfig);
			return rPCMethodConfig;
		}

		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream, RPCMethodConfig instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FixedCallCost = 1u;
			instance.FixedPacketSize = 0u;
			instance.VariableMultiplier = 0f;
			instance.Multiplier = 1f;
			instance.IncomePerSecond = 0f;
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
					instance.ServiceName = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.MethodName = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.FixedCallCost = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.FixedPacketSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 45:
					instance.VariableMultiplier = binaryReader.ReadSingle();
					continue;
				case 53:
					instance.Multiplier = binaryReader.ReadSingle();
					continue;
				case 56:
					instance.RateLimitCount = ProtocolParser.ReadUInt32(stream);
					continue;
				case 64:
					instance.RateLimitSeconds = ProtocolParser.ReadUInt32(stream);
					continue;
				case 72:
					instance.MaxPacketSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 80:
					instance.MaxEncodedSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 93:
					instance.Timeout = binaryReader.ReadSingle();
					continue;
				case 96:
					instance.CapBalance = ProtocolParser.ReadUInt32(stream);
					continue;
				case 109:
					instance.IncomePerSecond = binaryReader.ReadSingle();
					continue;
				case 112:
					instance.ServiceHash = ProtocolParser.ReadUInt32(stream);
					continue;
				case 120:
					instance.MethodId = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, RPCMethodConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServiceName));
			}
			if (instance.HasMethodName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MethodName));
			}
			if (instance.HasFixedCallCost)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.FixedCallCost);
			}
			if (instance.HasFixedPacketSize)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.FixedPacketSize);
			}
			if (instance.HasVariableMultiplier)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.VariableMultiplier);
			}
			if (instance.HasMultiplier)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Multiplier);
			}
			if (instance.HasRateLimitCount)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitCount);
			}
			if (instance.HasRateLimitSeconds)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitSeconds);
			}
			if (instance.HasMaxPacketSize)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.MaxPacketSize);
			}
			if (instance.HasMaxEncodedSize)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.MaxEncodedSize);
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Timeout);
			}
			if (instance.HasCapBalance)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt32(stream, instance.CapBalance);
			}
			if (instance.HasIncomePerSecond)
			{
				stream.WriteByte(109);
				binaryWriter.Write(instance.IncomePerSecond);
			}
			if (instance.HasServiceHash)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt32(stream, instance.ServiceHash);
			}
			if (instance.HasMethodId)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt32(stream, instance.MethodId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServiceName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ServiceName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasMethodName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(MethodName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFixedCallCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(FixedCallCost);
			}
			if (HasFixedPacketSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(FixedPacketSize);
			}
			if (HasVariableMultiplier)
			{
				num++;
				num += 4;
			}
			if (HasMultiplier)
			{
				num++;
				num += 4;
			}
			if (HasRateLimitCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RateLimitCount);
			}
			if (HasRateLimitSeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RateLimitSeconds);
			}
			if (HasMaxPacketSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxPacketSize);
			}
			if (HasMaxEncodedSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxEncodedSize);
			}
			if (HasTimeout)
			{
				num++;
				num += 4;
			}
			if (HasCapBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CapBalance);
			}
			if (HasIncomePerSecond)
			{
				num++;
				num += 4;
			}
			if (HasServiceHash)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ServiceHash);
			}
			if (HasMethodId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MethodId);
			}
			return num;
		}
	}
}
