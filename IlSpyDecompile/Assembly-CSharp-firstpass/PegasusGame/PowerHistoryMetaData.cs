using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	public class PowerHistoryMetaData : IProtoBuf
	{
		private List<int> _Info = new List<int>();

		public bool HasType;

		private HistoryMeta.Type _Type;

		public bool HasData;

		private int _Data;

		private List<int> _AdditionalData = new List<int>();

		public List<int> Info
		{
			get
			{
				return _Info;
			}
			set
			{
				_Info = value;
			}
		}

		public HistoryMeta.Type Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = true;
			}
		}

		public int Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = true;
			}
		}

		public List<int> AdditionalData
		{
			get
			{
				return _AdditionalData;
			}
			set
			{
				_AdditionalData = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (int item in Info)
			{
				num ^= item.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasData)
			{
				num ^= Data.GetHashCode();
			}
			foreach (int additionalDatum in AdditionalData)
			{
				num ^= additionalDatum.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PowerHistoryMetaData powerHistoryMetaData = obj as PowerHistoryMetaData;
			if (powerHistoryMetaData == null)
			{
				return false;
			}
			if (Info.Count != powerHistoryMetaData.Info.Count)
			{
				return false;
			}
			for (int i = 0; i < Info.Count; i++)
			{
				if (!Info[i].Equals(powerHistoryMetaData.Info[i]))
				{
					return false;
				}
			}
			if (HasType != powerHistoryMetaData.HasType || (HasType && !Type.Equals(powerHistoryMetaData.Type)))
			{
				return false;
			}
			if (HasData != powerHistoryMetaData.HasData || (HasData && !Data.Equals(powerHistoryMetaData.Data)))
			{
				return false;
			}
			if (AdditionalData.Count != powerHistoryMetaData.AdditionalData.Count)
			{
				return false;
			}
			for (int j = 0; j < AdditionalData.Count; j++)
			{
				if (!AdditionalData[j].Equals(powerHistoryMetaData.AdditionalData[j]))
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

		public static PowerHistoryMetaData Deserialize(Stream stream, PowerHistoryMetaData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PowerHistoryMetaData DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryMetaData powerHistoryMetaData = new PowerHistoryMetaData();
			DeserializeLengthDelimited(stream, powerHistoryMetaData);
			return powerHistoryMetaData;
		}

		public static PowerHistoryMetaData DeserializeLengthDelimited(Stream stream, PowerHistoryMetaData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PowerHistoryMetaData Deserialize(Stream stream, PowerHistoryMetaData instance, long limit)
		{
			if (instance.Info == null)
			{
				instance.Info = new List<int>();
			}
			instance.Type = HistoryMeta.Type.TARGET;
			if (instance.AdditionalData == null)
			{
				instance.AdditionalData = new List<int>();
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
				case 18:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Info.Add((int)ProtocolParser.ReadUInt64(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
				case 24:
					instance.Type = (HistoryMeta.Type)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Data = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.AdditionalData.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, PowerHistoryMetaData instance)
		{
			if (instance.Info.Count > 0)
			{
				stream.WriteByte(18);
				uint num = 0u;
				foreach (int item in instance.Info)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (int item2 in instance.Info)
				{
					ProtocolParser.WriteUInt64(stream, (ulong)item2);
				}
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			}
			if (instance.HasData)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Data);
			}
			if (instance.AdditionalData.Count <= 0)
			{
				return;
			}
			foreach (int additionalDatum in instance.AdditionalData)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)additionalDatum);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Info.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (int item in Info)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (HasType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Type);
			}
			if (HasData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Data);
			}
			if (AdditionalData.Count > 0)
			{
				foreach (int additionalDatum in AdditionalData)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)additionalDatum);
				}
				return num;
			}
			return num;
		}
	}
}
