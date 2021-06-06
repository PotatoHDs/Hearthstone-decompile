using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class SetGameSaveData : IProtoBuf
	{
		public enum PacketID
		{
			ID = 359,
			System = 0
		}

		private List<GameSaveDataUpdate> _Data = new List<GameSaveDataUpdate>();

		public bool HasClientToken;

		private int _ClientToken;

		public List<GameSaveDataUpdate> Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
			}
		}

		public int ClientToken
		{
			get
			{
				return _ClientToken;
			}
			set
			{
				_ClientToken = value;
				HasClientToken = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameSaveDataUpdate datum in Data)
			{
				num ^= datum.GetHashCode();
			}
			if (HasClientToken)
			{
				num ^= ClientToken.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetGameSaveData setGameSaveData = obj as SetGameSaveData;
			if (setGameSaveData == null)
			{
				return false;
			}
			if (Data.Count != setGameSaveData.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < Data.Count; i++)
			{
				if (!Data[i].Equals(setGameSaveData.Data[i]))
				{
					return false;
				}
			}
			if (HasClientToken != setGameSaveData.HasClientToken || (HasClientToken && !ClientToken.Equals(setGameSaveData.ClientToken)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetGameSaveData Deserialize(Stream stream, SetGameSaveData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetGameSaveData DeserializeLengthDelimited(Stream stream)
		{
			SetGameSaveData setGameSaveData = new SetGameSaveData();
			DeserializeLengthDelimited(stream, setGameSaveData);
			return setGameSaveData;
		}

		public static SetGameSaveData DeserializeLengthDelimited(Stream stream, SetGameSaveData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetGameSaveData Deserialize(Stream stream, SetGameSaveData instance, long limit)
		{
			if (instance.Data == null)
			{
				instance.Data = new List<GameSaveDataUpdate>();
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
					instance.Data.Add(GameSaveDataUpdate.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.ClientToken = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SetGameSaveData instance)
		{
			if (instance.Data.Count > 0)
			{
				foreach (GameSaveDataUpdate datum in instance.Data)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, datum.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, datum);
				}
			}
			if (instance.HasClientToken)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientToken);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Data.Count > 0)
			{
				foreach (GameSaveDataUpdate datum in Data)
				{
					num++;
					uint serializedSize = datum.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasClientToken)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientToken);
			}
			return num;
		}
	}
}
