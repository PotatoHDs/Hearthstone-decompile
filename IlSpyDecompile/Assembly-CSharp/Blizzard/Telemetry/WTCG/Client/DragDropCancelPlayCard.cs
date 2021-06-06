using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DragDropCancelPlayCard : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasScenarioId;

		private long _ScenarioId;

		public bool HasCardType;

		private string _CardType;

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

		public long ScenarioId
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

		public string CardType
		{
			get
			{
				return _CardType;
			}
			set
			{
				_CardType = value;
				HasCardType = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasScenarioId)
			{
				num ^= ScenarioId.GetHashCode();
			}
			if (HasCardType)
			{
				num ^= CardType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DragDropCancelPlayCard dragDropCancelPlayCard = obj as DragDropCancelPlayCard;
			if (dragDropCancelPlayCard == null)
			{
				return false;
			}
			if (HasDeviceInfo != dragDropCancelPlayCard.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(dragDropCancelPlayCard.DeviceInfo)))
			{
				return false;
			}
			if (HasScenarioId != dragDropCancelPlayCard.HasScenarioId || (HasScenarioId && !ScenarioId.Equals(dragDropCancelPlayCard.ScenarioId)))
			{
				return false;
			}
			if (HasCardType != dragDropCancelPlayCard.HasCardType || (HasCardType && !CardType.Equals(dragDropCancelPlayCard.CardType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DragDropCancelPlayCard Deserialize(Stream stream, DragDropCancelPlayCard instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DragDropCancelPlayCard DeserializeLengthDelimited(Stream stream)
		{
			DragDropCancelPlayCard dragDropCancelPlayCard = new DragDropCancelPlayCard();
			DeserializeLengthDelimited(stream, dragDropCancelPlayCard);
			return dragDropCancelPlayCard;
		}

		public static DragDropCancelPlayCard DeserializeLengthDelimited(Stream stream, DragDropCancelPlayCard instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DragDropCancelPlayCard Deserialize(Stream stream, DragDropCancelPlayCard instance, long limit)
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 16:
					instance.ScenarioId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.CardType = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, DragDropCancelPlayCard instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			}
			if (instance.HasCardType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CardType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasScenarioId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			}
			if (HasCardType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CardType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
