using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FlowPerformanceGame : IProtoBuf
	{
		public bool HasFlowId;

		private string _FlowId;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

		public bool HasUuid;

		private string _Uuid;

		public bool HasGameType;

		private GameType _GameType;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasBoardId;

		private int _BoardId;

		public bool HasScenarioId;

		private int _ScenarioId;

		public string FlowId
		{
			get
			{
				return _FlowId;
			}
			set
			{
				_FlowId = value;
				HasFlowId = value != null;
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

		public string Uuid
		{
			get
			{
				return _Uuid;
			}
			set
			{
				_Uuid = value;
				HasUuid = value != null;
			}
		}

		public GameType GameType
		{
			get
			{
				return _GameType;
			}
			set
			{
				_GameType = value;
				HasGameType = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public int BoardId
		{
			get
			{
				return _BoardId;
			}
			set
			{
				_BoardId = value;
				HasBoardId = true;
			}
		}

		public int ScenarioId
		{
			get
			{
				return _ScenarioId;
			}
			set
			{
				_ScenarioId = value;
				HasScenarioId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFlowId)
			{
				num ^= FlowId.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasUuid)
			{
				num ^= Uuid.GetHashCode();
			}
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasBoardId)
			{
				num ^= BoardId.GetHashCode();
			}
			if (HasScenarioId)
			{
				num ^= ScenarioId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FlowPerformanceGame flowPerformanceGame = obj as FlowPerformanceGame;
			if (flowPerformanceGame == null)
			{
				return false;
			}
			if (HasFlowId != flowPerformanceGame.HasFlowId || (HasFlowId && !FlowId.Equals(flowPerformanceGame.FlowId)))
			{
				return false;
			}
			if (HasDeviceInfo != flowPerformanceGame.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(flowPerformanceGame.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != flowPerformanceGame.HasPlayer || (HasPlayer && !Player.Equals(flowPerformanceGame.Player)))
			{
				return false;
			}
			if (HasUuid != flowPerformanceGame.HasUuid || (HasUuid && !Uuid.Equals(flowPerformanceGame.Uuid)))
			{
				return false;
			}
			if (HasGameType != flowPerformanceGame.HasGameType || (HasGameType && !GameType.Equals(flowPerformanceGame.GameType)))
			{
				return false;
			}
			if (HasFormatType != flowPerformanceGame.HasFormatType || (HasFormatType && !FormatType.Equals(flowPerformanceGame.FormatType)))
			{
				return false;
			}
			if (HasBoardId != flowPerformanceGame.HasBoardId || (HasBoardId && !BoardId.Equals(flowPerformanceGame.BoardId)))
			{
				return false;
			}
			if (HasScenarioId != flowPerformanceGame.HasScenarioId || (HasScenarioId && !ScenarioId.Equals(flowPerformanceGame.ScenarioId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FlowPerformanceGame Deserialize(Stream stream, FlowPerformanceGame instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FlowPerformanceGame DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceGame flowPerformanceGame = new FlowPerformanceGame();
			DeserializeLengthDelimited(stream, flowPerformanceGame);
			return flowPerformanceGame;
		}

		public static FlowPerformanceGame DeserializeLengthDelimited(Stream stream, FlowPerformanceGame instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FlowPerformanceGame Deserialize(Stream stream, FlowPerformanceGame instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					instance.FlowId = ProtocolParser.ReadString(stream);
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
				case 34:
					instance.Uuid = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.BoardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FlowPerformanceGame instance)
		{
			if (instance.HasFlowId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FlowId));
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
			if (instance.HasUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Uuid));
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasBoardId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BoardId);
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFlowId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FlowId);
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
			if (HasUuid)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Uuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasGameType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameType);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasBoardId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BoardId);
			}
			if (HasScenarioId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			}
			return num;
		}
	}
}
