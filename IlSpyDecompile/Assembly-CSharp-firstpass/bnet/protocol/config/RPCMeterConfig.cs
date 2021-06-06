using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	public class RPCMeterConfig : IProtoBuf
	{
		private List<RPCMethodConfig> _Method = new List<RPCMethodConfig>();

		public bool HasIncomePerSecond;

		private uint _IncomePerSecond;

		public bool HasInitialBalance;

		private uint _InitialBalance;

		public bool HasCapBalance;

		private uint _CapBalance;

		public bool HasStartupPeriod;

		private float _StartupPeriod;

		public List<RPCMethodConfig> Method
		{
			get
			{
				return _Method;
			}
			set
			{
				_Method = value;
			}
		}

		public List<RPCMethodConfig> MethodList => _Method;

		public int MethodCount => _Method.Count;

		public uint IncomePerSecond
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

		public uint InitialBalance
		{
			get
			{
				return _InitialBalance;
			}
			set
			{
				_InitialBalance = value;
				HasInitialBalance = true;
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

		public float StartupPeriod
		{
			get
			{
				return _StartupPeriod;
			}
			set
			{
				_StartupPeriod = value;
				HasStartupPeriod = true;
			}
		}

		public bool IsInitialized => true;

		public void AddMethod(RPCMethodConfig val)
		{
			_Method.Add(val);
		}

		public void ClearMethod()
		{
			_Method.Clear();
		}

		public void SetMethod(List<RPCMethodConfig> val)
		{
			Method = val;
		}

		public void SetIncomePerSecond(uint val)
		{
			IncomePerSecond = val;
		}

		public void SetInitialBalance(uint val)
		{
			InitialBalance = val;
		}

		public void SetCapBalance(uint val)
		{
			CapBalance = val;
		}

		public void SetStartupPeriod(float val)
		{
			StartupPeriod = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (RPCMethodConfig item in Method)
			{
				num ^= item.GetHashCode();
			}
			if (HasIncomePerSecond)
			{
				num ^= IncomePerSecond.GetHashCode();
			}
			if (HasInitialBalance)
			{
				num ^= InitialBalance.GetHashCode();
			}
			if (HasCapBalance)
			{
				num ^= CapBalance.GetHashCode();
			}
			if (HasStartupPeriod)
			{
				num ^= StartupPeriod.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RPCMeterConfig rPCMeterConfig = obj as RPCMeterConfig;
			if (rPCMeterConfig == null)
			{
				return false;
			}
			if (Method.Count != rPCMeterConfig.Method.Count)
			{
				return false;
			}
			for (int i = 0; i < Method.Count; i++)
			{
				if (!Method[i].Equals(rPCMeterConfig.Method[i]))
				{
					return false;
				}
			}
			if (HasIncomePerSecond != rPCMeterConfig.HasIncomePerSecond || (HasIncomePerSecond && !IncomePerSecond.Equals(rPCMeterConfig.IncomePerSecond)))
			{
				return false;
			}
			if (HasInitialBalance != rPCMeterConfig.HasInitialBalance || (HasInitialBalance && !InitialBalance.Equals(rPCMeterConfig.InitialBalance)))
			{
				return false;
			}
			if (HasCapBalance != rPCMeterConfig.HasCapBalance || (HasCapBalance && !CapBalance.Equals(rPCMeterConfig.CapBalance)))
			{
				return false;
			}
			if (HasStartupPeriod != rPCMeterConfig.HasStartupPeriod || (HasStartupPeriod && !StartupPeriod.Equals(rPCMeterConfig.StartupPeriod)))
			{
				return false;
			}
			return true;
		}

		public static RPCMeterConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMeterConfig>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMeterConfig rPCMeterConfig = new RPCMeterConfig();
			DeserializeLengthDelimited(stream, rPCMeterConfig);
			return rPCMeterConfig;
		}

		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream, RPCMeterConfig instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Method == null)
			{
				instance.Method = new List<RPCMethodConfig>();
			}
			instance.IncomePerSecond = 1u;
			instance.StartupPeriod = 0f;
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
					instance.Method.Add(RPCMethodConfig.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.IncomePerSecond = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.InitialBalance = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.CapBalance = ProtocolParser.ReadUInt32(stream);
					continue;
				case 45:
					instance.StartupPeriod = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, RPCMeterConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Method.Count > 0)
			{
				foreach (RPCMethodConfig item in instance.Method)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					RPCMethodConfig.Serialize(stream, item);
				}
			}
			if (instance.HasIncomePerSecond)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.IncomePerSecond);
			}
			if (instance.HasInitialBalance)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.InitialBalance);
			}
			if (instance.HasCapBalance)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.CapBalance);
			}
			if (instance.HasStartupPeriod)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.StartupPeriod);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Method.Count > 0)
			{
				foreach (RPCMethodConfig item in Method)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasIncomePerSecond)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(IncomePerSecond);
			}
			if (HasInitialBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(InitialBalance);
			}
			if (HasCapBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(CapBalance);
			}
			if (HasStartupPeriod)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
