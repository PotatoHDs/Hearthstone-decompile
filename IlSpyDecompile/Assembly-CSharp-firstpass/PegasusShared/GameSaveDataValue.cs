using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class GameSaveDataValue : IProtoBuf
	{
		private List<long> _IntValue = new List<long>();

		private List<double> _FloatValue = new List<double>();

		private List<string> _StringValue = new List<string>();

		private List<long> _MapKeys = new List<long>();

		private List<GameSaveDataValue> _MapValues = new List<GameSaveDataValue>();

		public bool HasCreateDateUnixTimestamp;

		private long _CreateDateUnixTimestamp;

		public bool HasModifyDateUnixTimestamp;

		private long _ModifyDateUnixTimestamp;

		public List<long> IntValue
		{
			get
			{
				return _IntValue;
			}
			set
			{
				_IntValue = value;
			}
		}

		public List<double> FloatValue
		{
			get
			{
				return _FloatValue;
			}
			set
			{
				_FloatValue = value;
			}
		}

		public List<string> StringValue
		{
			get
			{
				return _StringValue;
			}
			set
			{
				_StringValue = value;
			}
		}

		public List<long> MapKeys
		{
			get
			{
				return _MapKeys;
			}
			set
			{
				_MapKeys = value;
			}
		}

		public List<GameSaveDataValue> MapValues
		{
			get
			{
				return _MapValues;
			}
			set
			{
				_MapValues = value;
			}
		}

		public long CreateDateUnixTimestamp
		{
			get
			{
				return _CreateDateUnixTimestamp;
			}
			set
			{
				_CreateDateUnixTimestamp = value;
				HasCreateDateUnixTimestamp = true;
			}
		}

		public long ModifyDateUnixTimestamp
		{
			get
			{
				return _ModifyDateUnixTimestamp;
			}
			set
			{
				_ModifyDateUnixTimestamp = value;
				HasModifyDateUnixTimestamp = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (long item in IntValue)
			{
				num ^= item.GetHashCode();
			}
			foreach (double item2 in FloatValue)
			{
				num ^= item2.GetHashCode();
			}
			foreach (string item3 in StringValue)
			{
				num ^= item3.GetHashCode();
			}
			foreach (long mapKey in MapKeys)
			{
				num ^= mapKey.GetHashCode();
			}
			foreach (GameSaveDataValue mapValue in MapValues)
			{
				num ^= mapValue.GetHashCode();
			}
			if (HasCreateDateUnixTimestamp)
			{
				num ^= CreateDateUnixTimestamp.GetHashCode();
			}
			if (HasModifyDateUnixTimestamp)
			{
				num ^= ModifyDateUnixTimestamp.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSaveDataValue gameSaveDataValue = obj as GameSaveDataValue;
			if (gameSaveDataValue == null)
			{
				return false;
			}
			if (IntValue.Count != gameSaveDataValue.IntValue.Count)
			{
				return false;
			}
			for (int i = 0; i < IntValue.Count; i++)
			{
				if (!IntValue[i].Equals(gameSaveDataValue.IntValue[i]))
				{
					return false;
				}
			}
			if (FloatValue.Count != gameSaveDataValue.FloatValue.Count)
			{
				return false;
			}
			for (int j = 0; j < FloatValue.Count; j++)
			{
				if (!FloatValue[j].Equals(gameSaveDataValue.FloatValue[j]))
				{
					return false;
				}
			}
			if (StringValue.Count != gameSaveDataValue.StringValue.Count)
			{
				return false;
			}
			for (int k = 0; k < StringValue.Count; k++)
			{
				if (!StringValue[k].Equals(gameSaveDataValue.StringValue[k]))
				{
					return false;
				}
			}
			if (MapKeys.Count != gameSaveDataValue.MapKeys.Count)
			{
				return false;
			}
			for (int l = 0; l < MapKeys.Count; l++)
			{
				if (!MapKeys[l].Equals(gameSaveDataValue.MapKeys[l]))
				{
					return false;
				}
			}
			if (MapValues.Count != gameSaveDataValue.MapValues.Count)
			{
				return false;
			}
			for (int m = 0; m < MapValues.Count; m++)
			{
				if (!MapValues[m].Equals(gameSaveDataValue.MapValues[m]))
				{
					return false;
				}
			}
			if (HasCreateDateUnixTimestamp != gameSaveDataValue.HasCreateDateUnixTimestamp || (HasCreateDateUnixTimestamp && !CreateDateUnixTimestamp.Equals(gameSaveDataValue.CreateDateUnixTimestamp)))
			{
				return false;
			}
			if (HasModifyDateUnixTimestamp != gameSaveDataValue.HasModifyDateUnixTimestamp || (HasModifyDateUnixTimestamp && !ModifyDateUnixTimestamp.Equals(gameSaveDataValue.ModifyDateUnixTimestamp)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSaveDataValue Deserialize(Stream stream, GameSaveDataValue instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSaveDataValue DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataValue gameSaveDataValue = new GameSaveDataValue();
			DeserializeLengthDelimited(stream, gameSaveDataValue);
			return gameSaveDataValue;
		}

		public static GameSaveDataValue DeserializeLengthDelimited(Stream stream, GameSaveDataValue instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSaveDataValue Deserialize(Stream stream, GameSaveDataValue instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.IntValue == null)
			{
				instance.IntValue = new List<long>();
			}
			if (instance.FloatValue == null)
			{
				instance.FloatValue = new List<double>();
			}
			if (instance.StringValue == null)
			{
				instance.StringValue = new List<string>();
			}
			if (instance.MapKeys == null)
			{
				instance.MapKeys = new List<long>();
			}
			if (instance.MapValues == null)
			{
				instance.MapValues = new List<GameSaveDataValue>();
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
				case 8:
					instance.IntValue.Add((long)ProtocolParser.ReadUInt64(stream));
					continue;
				case 17:
					instance.FloatValue.Add(binaryReader.ReadDouble());
					continue;
				case 26:
					instance.StringValue.Add(ProtocolParser.ReadString(stream));
					continue;
				case 80:
					instance.MapKeys.Add((long)ProtocolParser.ReadUInt64(stream));
					continue;
				case 90:
					instance.MapValues.Add(DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 1000u:
						if (key.WireType == Wire.Varint)
						{
							instance.CreateDateUnixTimestamp = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 1001u:
						if (key.WireType == Wire.Varint)
						{
							instance.ModifyDateUnixTimestamp = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, GameSaveDataValue instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.IntValue.Count > 0)
			{
				foreach (long item in instance.IntValue)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)item);
				}
			}
			if (instance.FloatValue.Count > 0)
			{
				foreach (double item2 in instance.FloatValue)
				{
					stream.WriteByte(17);
					binaryWriter.Write(item2);
				}
			}
			if (instance.StringValue.Count > 0)
			{
				foreach (string item3 in instance.StringValue)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item3));
				}
			}
			if (instance.MapKeys.Count > 0)
			{
				foreach (long mapKey in instance.MapKeys)
				{
					stream.WriteByte(80);
					ProtocolParser.WriteUInt64(stream, (ulong)mapKey);
				}
			}
			if (instance.MapValues.Count > 0)
			{
				foreach (GameSaveDataValue mapValue in instance.MapValues)
				{
					stream.WriteByte(90);
					ProtocolParser.WriteUInt32(stream, mapValue.GetSerializedSize());
					Serialize(stream, mapValue);
				}
			}
			if (instance.HasCreateDateUnixTimestamp)
			{
				stream.WriteByte(192);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CreateDateUnixTimestamp);
			}
			if (instance.HasModifyDateUnixTimestamp)
			{
				stream.WriteByte(200);
				stream.WriteByte(62);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ModifyDateUnixTimestamp);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (IntValue.Count > 0)
			{
				foreach (long item in IntValue)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			if (FloatValue.Count > 0)
			{
				foreach (double item2 in FloatValue)
				{
					_ = item2;
					num++;
					num += 8;
				}
			}
			if (StringValue.Count > 0)
			{
				foreach (string item3 in StringValue)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(item3);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (MapKeys.Count > 0)
			{
				foreach (long mapKey in MapKeys)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)mapKey);
				}
			}
			if (MapValues.Count > 0)
			{
				foreach (GameSaveDataValue mapValue in MapValues)
				{
					num++;
					uint serializedSize = mapValue.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCreateDateUnixTimestamp)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)CreateDateUnixTimestamp);
			}
			if (HasModifyDateUnixTimestamp)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)ModifyDateUnixTimestamp);
			}
			return num;
		}
	}
}
