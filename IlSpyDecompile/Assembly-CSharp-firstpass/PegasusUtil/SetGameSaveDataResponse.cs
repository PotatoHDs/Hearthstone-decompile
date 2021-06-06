using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class SetGameSaveDataResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 360
		}

		private List<GameSaveDataUpdate> _Data = new List<GameSaveDataUpdate>();

		public bool HasClientToken;

		private int _ClientToken;

		public ErrorCode ErrorCode { get; set; }

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
			int hashCode = GetType().GetHashCode();
			hashCode ^= ErrorCode.GetHashCode();
			foreach (GameSaveDataUpdate datum in Data)
			{
				hashCode ^= datum.GetHashCode();
			}
			if (HasClientToken)
			{
				hashCode ^= ClientToken.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SetGameSaveDataResponse setGameSaveDataResponse = obj as SetGameSaveDataResponse;
			if (setGameSaveDataResponse == null)
			{
				return false;
			}
			if (!ErrorCode.Equals(setGameSaveDataResponse.ErrorCode))
			{
				return false;
			}
			if (Data.Count != setGameSaveDataResponse.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < Data.Count; i++)
			{
				if (!Data[i].Equals(setGameSaveDataResponse.Data[i]))
				{
					return false;
				}
			}
			if (HasClientToken != setGameSaveDataResponse.HasClientToken || (HasClientToken && !ClientToken.Equals(setGameSaveDataResponse.ClientToken)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SetGameSaveDataResponse Deserialize(Stream stream, SetGameSaveDataResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SetGameSaveDataResponse DeserializeLengthDelimited(Stream stream)
		{
			SetGameSaveDataResponse setGameSaveDataResponse = new SetGameSaveDataResponse();
			DeserializeLengthDelimited(stream, setGameSaveDataResponse);
			return setGameSaveDataResponse;
		}

		public static SetGameSaveDataResponse DeserializeLengthDelimited(Stream stream, SetGameSaveDataResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SetGameSaveDataResponse Deserialize(Stream stream, SetGameSaveDataResponse instance, long limit)
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
				case 8:
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Data.Add(GameSaveDataUpdate.DeserializeLengthDelimited(stream));
					continue;
				case 24:
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

		public static void Serialize(Stream stream, SetGameSaveDataResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			if (instance.Data.Count > 0)
			{
				foreach (GameSaveDataUpdate datum in instance.Data)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, datum.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, datum);
				}
			}
			if (instance.HasClientToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientToken);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
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
			return num + 1;
		}
	}
}
