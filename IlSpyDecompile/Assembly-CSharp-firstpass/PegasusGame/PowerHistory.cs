using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class PowerHistory : IProtoBuf
	{
		public enum PacketID
		{
			ID = 19
		}

		private List<PowerHistoryData> _List = new List<PowerHistoryData>();

		public List<PowerHistoryData> List
		{
			get
			{
				return _List;
			}
			set
			{
				_List = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PowerHistoryData item in List)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PowerHistory powerHistory = obj as PowerHistory;
			if (powerHistory == null)
			{
				return false;
			}
			if (List.Count != powerHistory.List.Count)
			{
				return false;
			}
			for (int i = 0; i < List.Count; i++)
			{
				if (!List[i].Equals(powerHistory.List[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PowerHistory Deserialize(Stream stream, PowerHistory instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistory DeserializeLengthDelimited(Stream stream)
		{
			PowerHistory powerHistory = new PowerHistory();
			DeserializeLengthDelimited(stream, powerHistory);
			return powerHistory;
		}

		public static PowerHistory DeserializeLengthDelimited(Stream stream, PowerHistory instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistory Deserialize(Stream stream, PowerHistory instance, long limit)
		{
			if (instance.List == null)
			{
				instance.List = new List<PowerHistoryData>();
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
					instance.List.Add(PowerHistoryData.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PowerHistory instance)
		{
			if (instance.List.Count <= 0)
			{
				return;
			}
			foreach (PowerHistoryData item in instance.List)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PowerHistoryData.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (List.Count > 0)
			{
				foreach (PowerHistoryData item in List)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
