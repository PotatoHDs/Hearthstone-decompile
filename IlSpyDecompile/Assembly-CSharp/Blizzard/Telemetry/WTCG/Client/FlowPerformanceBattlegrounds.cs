using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FlowPerformanceBattlegrounds : IProtoBuf
	{
		public bool HasFlowId;

		private string _FlowId;

		public bool HasGameUuid;

		private string _GameUuid;

		public bool HasTotalRounds;

		private int _TotalRounds;

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

		public string GameUuid
		{
			get
			{
				return _GameUuid;
			}
			set
			{
				_GameUuid = value;
				HasGameUuid = value != null;
			}
		}

		public int TotalRounds
		{
			get
			{
				return _TotalRounds;
			}
			set
			{
				_TotalRounds = value;
				HasTotalRounds = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFlowId)
			{
				num ^= FlowId.GetHashCode();
			}
			if (HasGameUuid)
			{
				num ^= GameUuid.GetHashCode();
			}
			if (HasTotalRounds)
			{
				num ^= TotalRounds.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = obj as FlowPerformanceBattlegrounds;
			if (flowPerformanceBattlegrounds == null)
			{
				return false;
			}
			if (HasFlowId != flowPerformanceBattlegrounds.HasFlowId || (HasFlowId && !FlowId.Equals(flowPerformanceBattlegrounds.FlowId)))
			{
				return false;
			}
			if (HasGameUuid != flowPerformanceBattlegrounds.HasGameUuid || (HasGameUuid && !GameUuid.Equals(flowPerformanceBattlegrounds.GameUuid)))
			{
				return false;
			}
			if (HasTotalRounds != flowPerformanceBattlegrounds.HasTotalRounds || (HasTotalRounds && !TotalRounds.Equals(flowPerformanceBattlegrounds.TotalRounds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FlowPerformanceBattlegrounds Deserialize(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FlowPerformanceBattlegrounds DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = new FlowPerformanceBattlegrounds();
			DeserializeLengthDelimited(stream, flowPerformanceBattlegrounds);
			return flowPerformanceBattlegrounds;
		}

		public static FlowPerformanceBattlegrounds DeserializeLengthDelimited(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FlowPerformanceBattlegrounds Deserialize(Stream stream, FlowPerformanceBattlegrounds instance, long limit)
		{
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
				case 34:
					instance.GameUuid = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.TotalRounds = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FlowPerformanceBattlegrounds instance)
		{
			if (instance.HasFlowId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FlowId));
			}
			if (instance.HasGameUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameUuid));
			}
			if (instance.HasTotalRounds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalRounds);
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
			if (HasGameUuid)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(GameUuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasTotalRounds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalRounds);
			}
			return num;
		}
	}
}
