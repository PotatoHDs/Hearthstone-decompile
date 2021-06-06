using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FlowPerformance : IProtoBuf
	{
		public enum FlowType
		{
			SHOP = 1,
			COLLECTION_MANAGER,
			GAME,
			JOURNAL,
			ARENA,
			DUELS,
			ADVENTURE
		}

		public bool HasUniqueId;

		private string _UniqueId;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

		public bool HasFlowType_;

		private FlowType _FlowType_;

		public bool HasAverageFps;

		private float _AverageFps;

		public bool HasDuration;

		private float _Duration;

		public bool HasFpsWarningsThreshold;

		private float _FpsWarningsThreshold;

		public bool HasFpsWarningsTotalOccurences;

		private int _FpsWarningsTotalOccurences;

		public bool HasFpsWarningsTotalTime;

		private float _FpsWarningsTotalTime;

		public bool HasFpsWarningsAverageTime;

		private float _FpsWarningsAverageTime;

		public bool HasFpsWarningsMaxTime;

		private float _FpsWarningsMaxTime;

		public string UniqueId
		{
			get
			{
				return _UniqueId;
			}
			set
			{
				_UniqueId = value;
				HasUniqueId = value != null;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public FlowType FlowType_
		{
			get
			{
				return _FlowType_;
			}
			set
			{
				_FlowType_ = value;
				HasFlowType_ = true;
			}
		}

		public float AverageFps
		{
			get
			{
				return _AverageFps;
			}
			set
			{
				_AverageFps = value;
				HasAverageFps = true;
			}
		}

		public float Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
				HasDuration = true;
			}
		}

		public float FpsWarningsThreshold
		{
			get
			{
				return _FpsWarningsThreshold;
			}
			set
			{
				_FpsWarningsThreshold = value;
				HasFpsWarningsThreshold = true;
			}
		}

		public int FpsWarningsTotalOccurences
		{
			get
			{
				return _FpsWarningsTotalOccurences;
			}
			set
			{
				_FpsWarningsTotalOccurences = value;
				HasFpsWarningsTotalOccurences = true;
			}
		}

		public float FpsWarningsTotalTime
		{
			get
			{
				return _FpsWarningsTotalTime;
			}
			set
			{
				_FpsWarningsTotalTime = value;
				HasFpsWarningsTotalTime = true;
			}
		}

		public float FpsWarningsAverageTime
		{
			get
			{
				return _FpsWarningsAverageTime;
			}
			set
			{
				_FpsWarningsAverageTime = value;
				HasFpsWarningsAverageTime = true;
			}
		}

		public float FpsWarningsMaxTime
		{
			get
			{
				return _FpsWarningsMaxTime;
			}
			set
			{
				_FpsWarningsMaxTime = value;
				HasFpsWarningsMaxTime = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUniqueId)
			{
				num ^= UniqueId.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasFlowType_)
			{
				num ^= FlowType_.GetHashCode();
			}
			if (HasAverageFps)
			{
				num ^= AverageFps.GetHashCode();
			}
			if (HasDuration)
			{
				num ^= Duration.GetHashCode();
			}
			if (HasFpsWarningsThreshold)
			{
				num ^= FpsWarningsThreshold.GetHashCode();
			}
			if (HasFpsWarningsTotalOccurences)
			{
				num ^= FpsWarningsTotalOccurences.GetHashCode();
			}
			if (HasFpsWarningsTotalTime)
			{
				num ^= FpsWarningsTotalTime.GetHashCode();
			}
			if (HasFpsWarningsAverageTime)
			{
				num ^= FpsWarningsAverageTime.GetHashCode();
			}
			if (HasFpsWarningsMaxTime)
			{
				num ^= FpsWarningsMaxTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FlowPerformance flowPerformance = obj as FlowPerformance;
			if (flowPerformance == null)
			{
				return false;
			}
			if (HasUniqueId != flowPerformance.HasUniqueId || (HasUniqueId && !UniqueId.Equals(flowPerformance.UniqueId)))
			{
				return false;
			}
			if (HasDeviceInfo != flowPerformance.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(flowPerformance.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != flowPerformance.HasPlayer || (HasPlayer && !Player.Equals(flowPerformance.Player)))
			{
				return false;
			}
			if (HasFlowType_ != flowPerformance.HasFlowType_ || (HasFlowType_ && !FlowType_.Equals(flowPerformance.FlowType_)))
			{
				return false;
			}
			if (HasAverageFps != flowPerformance.HasAverageFps || (HasAverageFps && !AverageFps.Equals(flowPerformance.AverageFps)))
			{
				return false;
			}
			if (HasDuration != flowPerformance.HasDuration || (HasDuration && !Duration.Equals(flowPerformance.Duration)))
			{
				return false;
			}
			if (HasFpsWarningsThreshold != flowPerformance.HasFpsWarningsThreshold || (HasFpsWarningsThreshold && !FpsWarningsThreshold.Equals(flowPerformance.FpsWarningsThreshold)))
			{
				return false;
			}
			if (HasFpsWarningsTotalOccurences != flowPerformance.HasFpsWarningsTotalOccurences || (HasFpsWarningsTotalOccurences && !FpsWarningsTotalOccurences.Equals(flowPerformance.FpsWarningsTotalOccurences)))
			{
				return false;
			}
			if (HasFpsWarningsTotalTime != flowPerformance.HasFpsWarningsTotalTime || (HasFpsWarningsTotalTime && !FpsWarningsTotalTime.Equals(flowPerformance.FpsWarningsTotalTime)))
			{
				return false;
			}
			if (HasFpsWarningsAverageTime != flowPerformance.HasFpsWarningsAverageTime || (HasFpsWarningsAverageTime && !FpsWarningsAverageTime.Equals(flowPerformance.FpsWarningsAverageTime)))
			{
				return false;
			}
			if (HasFpsWarningsMaxTime != flowPerformance.HasFpsWarningsMaxTime || (HasFpsWarningsMaxTime && !FpsWarningsMaxTime.Equals(flowPerformance.FpsWarningsMaxTime)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FlowPerformance Deserialize(Stream stream, FlowPerformance instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FlowPerformance DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformance flowPerformance = new FlowPerformance();
			DeserializeLengthDelimited(stream, flowPerformance);
			return flowPerformance;
		}

		public static FlowPerformance DeserializeLengthDelimited(Stream stream, FlowPerformance instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FlowPerformance Deserialize(Stream stream, FlowPerformance instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FlowType_ = FlowType.SHOP;
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
					instance.UniqueId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 26:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 32:
					instance.FlowType_ = (FlowType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 45:
					instance.AverageFps = binaryReader.ReadSingle();
					continue;
				case 53:
					instance.Duration = binaryReader.ReadSingle();
					continue;
				case 61:
					instance.FpsWarningsThreshold = binaryReader.ReadSingle();
					continue;
				case 64:
					instance.FpsWarningsTotalOccurences = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 77:
					instance.FpsWarningsTotalTime = binaryReader.ReadSingle();
					continue;
				case 85:
					instance.FpsWarningsAverageTime = binaryReader.ReadSingle();
					continue;
				case 93:
					instance.FpsWarningsMaxTime = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, FlowPerformance instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasUniqueId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UniqueId));
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasFlowType_)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FlowType_);
			}
			if (instance.HasAverageFps)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.AverageFps);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasFpsWarningsThreshold)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.FpsWarningsThreshold);
			}
			if (instance.HasFpsWarningsTotalOccurences)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FpsWarningsTotalOccurences);
			}
			if (instance.HasFpsWarningsTotalTime)
			{
				stream.WriteByte(77);
				binaryWriter.Write(instance.FpsWarningsTotalTime);
			}
			if (instance.HasFpsWarningsAverageTime)
			{
				stream.WriteByte(85);
				binaryWriter.Write(instance.FpsWarningsAverageTime);
			}
			if (instance.HasFpsWarningsMaxTime)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.FpsWarningsMaxTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUniqueId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(UniqueId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPlayer)
			{
				num++;
				uint serializedSize2 = Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasFlowType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FlowType_);
			}
			if (HasAverageFps)
			{
				num++;
				num += 4;
			}
			if (HasDuration)
			{
				num++;
				num += 4;
			}
			if (HasFpsWarningsThreshold)
			{
				num++;
				num += 4;
			}
			if (HasFpsWarningsTotalOccurences)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FpsWarningsTotalOccurences);
			}
			if (HasFpsWarningsTotalTime)
			{
				num++;
				num += 4;
			}
			if (HasFpsWarningsAverageTime)
			{
				num++;
				num += 4;
			}
			if (HasFpsWarningsMaxTime)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
