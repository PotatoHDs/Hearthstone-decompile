using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class GameSaveDataRequest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 357,
			System = 0
		}

		private List<long> _KeyIds = new List<long>();

		public bool HasClientToken;

		private int _ClientToken;

		public List<long> KeyIds
		{
			get
			{
				return _KeyIds;
			}
			set
			{
				_KeyIds = value;
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
			foreach (long keyId in KeyIds)
			{
				num ^= keyId.GetHashCode();
			}
			if (HasClientToken)
			{
				num ^= ClientToken.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSaveDataRequest gameSaveDataRequest = obj as GameSaveDataRequest;
			if (gameSaveDataRequest == null)
			{
				return false;
			}
			if (KeyIds.Count != gameSaveDataRequest.KeyIds.Count)
			{
				return false;
			}
			for (int i = 0; i < KeyIds.Count; i++)
			{
				if (!KeyIds[i].Equals(gameSaveDataRequest.KeyIds[i]))
				{
					return false;
				}
			}
			if (HasClientToken != gameSaveDataRequest.HasClientToken || (HasClientToken && !ClientToken.Equals(gameSaveDataRequest.ClientToken)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSaveDataRequest Deserialize(Stream stream, GameSaveDataRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSaveDataRequest DeserializeLengthDelimited(Stream stream)
		{
			GameSaveDataRequest gameSaveDataRequest = new GameSaveDataRequest();
			DeserializeLengthDelimited(stream, gameSaveDataRequest);
			return gameSaveDataRequest;
		}

		public static GameSaveDataRequest DeserializeLengthDelimited(Stream stream, GameSaveDataRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSaveDataRequest Deserialize(Stream stream, GameSaveDataRequest instance, long limit)
		{
			if (instance.KeyIds == null)
			{
				instance.KeyIds = new List<long>();
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
					instance.KeyIds.Add((long)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, GameSaveDataRequest instance)
		{
			if (instance.KeyIds.Count > 0)
			{
				foreach (long keyId in instance.KeyIds)
				{
					stream.WriteByte(8);
					ProtocolParser.WriteUInt64(stream, (ulong)keyId);
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
			if (KeyIds.Count > 0)
			{
				foreach (long keyId in KeyIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)keyId);
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
