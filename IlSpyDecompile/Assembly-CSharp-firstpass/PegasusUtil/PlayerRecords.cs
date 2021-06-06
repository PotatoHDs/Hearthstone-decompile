using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerRecords : IProtoBuf
	{
		public enum PacketID
		{
			ID = 270
		}

		private List<PlayerRecord> _Records = new List<PlayerRecord>();

		public List<PlayerRecord> Records
		{
			get
			{
				return _Records;
			}
			set
			{
				_Records = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerRecord record in Records)
			{
				num ^= record.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerRecords playerRecords = obj as PlayerRecords;
			if (playerRecords == null)
			{
				return false;
			}
			if (Records.Count != playerRecords.Records.Count)
			{
				return false;
			}
			for (int i = 0; i < Records.Count; i++)
			{
				if (!Records[i].Equals(playerRecords.Records[i]))
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

		public static PlayerRecords Deserialize(Stream stream, PlayerRecords instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerRecords DeserializeLengthDelimited(Stream stream)
		{
			PlayerRecords playerRecords = new PlayerRecords();
			DeserializeLengthDelimited(stream, playerRecords);
			return playerRecords;
		}

		public static PlayerRecords DeserializeLengthDelimited(Stream stream, PlayerRecords instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerRecords Deserialize(Stream stream, PlayerRecords instance, long limit)
		{
			if (instance.Records == null)
			{
				instance.Records = new List<PlayerRecord>();
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
					instance.Records.Add(PlayerRecord.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PlayerRecords instance)
		{
			if (instance.Records.Count <= 0)
			{
				return;
			}
			foreach (PlayerRecord record in instance.Records)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, record.GetSerializedSize());
				PlayerRecord.Serialize(stream, record);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Records.Count > 0)
			{
				foreach (PlayerRecord record in Records)
				{
					num++;
					uint serializedSize = record.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
