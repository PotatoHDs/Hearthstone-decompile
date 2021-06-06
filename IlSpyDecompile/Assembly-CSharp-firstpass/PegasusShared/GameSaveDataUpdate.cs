using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class GameSaveDataUpdate : IProtoBuf
	{
		public bool HasOwnerType;

		private GameSaveOwnerType _OwnerType;

		public bool HasOwnerId;

		private long _OwnerId;

		private List<GameSaveKey> _Tuple = new List<GameSaveKey>();

		public bool HasValue;

		private GameSaveDataValue _Value;

		public EventType EventType { get; set; }

		public GameSaveOwnerType OwnerType
		{
			get
			{
				return _OwnerType;
			}
			set
			{
				_OwnerType = value;
				HasOwnerType = true;
			}
		}

		public long OwnerId
		{
			get
			{
				return _OwnerId;
			}
			set
			{
				_OwnerId = value;
				HasOwnerId = true;
			}
		}

		public List<GameSaveKey> Tuple
		{
			get
			{
				return _Tuple;
			}
			set
			{
				_Tuple = value;
			}
		}

		public GameSaveDataValue Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				HasValue = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EventType.GetHashCode();
			if (HasOwnerType)
			{
				hashCode ^= OwnerType.GetHashCode();
			}
			if (HasOwnerId)
			{
				hashCode ^= OwnerId.GetHashCode();
			}
			foreach (GameSaveKey item in Tuple)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasValue)
			{
				hashCode ^= Value.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameSaveDataUpdate gameSaveDataUpdate = obj as GameSaveDataUpdate;
			if (gameSaveDataUpdate == null)
			{
				return false;
			}
			if (!EventType.Equals(gameSaveDataUpdate.EventType))
			{
				return false;
			}
			if (HasOwnerType != gameSaveDataUpdate.HasOwnerType || (HasOwnerType && !OwnerType.Equals(gameSaveDataUpdate.OwnerType)))
			{
				return false;
			}
			if (HasOwnerId != gameSaveDataUpdate.HasOwnerId || (HasOwnerId && !OwnerId.Equals(gameSaveDataUpdate.OwnerId)))
			{
				return false;
			}
			if (Tuple.Count != gameSaveDataUpdate.Tuple.Count)
			{
				return false;
			}
			for (int i = 0; i < Tuple.Count; i++)
			{
				if (!Tuple[i].Equals(gameSaveDataUpdate.Tuple[i]))
				{
					return false;
				}
			}
			if (HasValue != gameSaveDataUpdate.HasValue || (HasValue && !Value.Equals(gameSaveDataUpdate.Value)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSaveDataUpdate Deserialize(Stream stream, GameSaveDataUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSaveDataUpdate DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataUpdate gameSaveDataUpdate = new GameSaveDataUpdate();
			DeserializeLengthDelimited(stream, gameSaveDataUpdate);
			return gameSaveDataUpdate;
		}

		public static GameSaveDataUpdate DeserializeLengthDelimited(Stream stream, GameSaveDataUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSaveDataUpdate Deserialize(Stream stream, GameSaveDataUpdate instance, long limit)
		{
			instance.EventType = EventType.EVT_NONE;
			instance.OwnerType = GameSaveOwnerType.GAME_SAVE_OWNER_TYPE_UNKNOWN;
			if (instance.Tuple == null)
			{
				instance.Tuple = new List<GameSaveKey>();
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
					instance.EventType = (EventType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.OwnerType = (GameSaveOwnerType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.OwnerId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.Tuple.Add(GameSaveKey.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.Value == null)
							{
								instance.Value = GameSaveDataValue.DeserializeLengthDelimited(stream);
							}
							else
							{
								GameSaveDataValue.DeserializeLengthDelimited(stream, instance.Value);
							}
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

		public static void Serialize(Stream stream, GameSaveDataUpdate instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EventType);
			if (instance.HasOwnerType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OwnerType);
			}
			if (instance.HasOwnerId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OwnerId);
			}
			if (instance.Tuple.Count > 0)
			{
				foreach (GameSaveKey item in instance.Tuple)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					GameSaveKey.Serialize(stream, item);
				}
			}
			if (instance.HasValue)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Value.GetSerializedSize());
				GameSaveDataValue.Serialize(stream, instance.Value);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)EventType);
			if (HasOwnerType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OwnerType);
			}
			if (HasOwnerId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OwnerId);
			}
			if (Tuple.Count > 0)
			{
				foreach (GameSaveKey item in Tuple)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasValue)
			{
				num += 2;
				uint serializedSize2 = Value.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
