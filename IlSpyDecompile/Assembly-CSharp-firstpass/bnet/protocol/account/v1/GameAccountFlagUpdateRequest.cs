using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountFlagUpdateRequest : IProtoBuf
	{
		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasFlag;

		private ulong _Flag;

		public bool HasActive;

		private bool _Active;

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public ulong Flag
		{
			get
			{
				return _Flag;
			}
			set
			{
				_Flag = value;
				HasFlag = true;
			}
		}

		public bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				_Active = value;
				HasActive = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetFlag(ulong val)
		{
			Flag = val;
		}

		public void SetActive(bool val)
		{
			Active = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasFlag)
			{
				num ^= Flag.GetHashCode();
			}
			if (HasActive)
			{
				num ^= Active.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountFlagUpdateRequest gameAccountFlagUpdateRequest = obj as GameAccountFlagUpdateRequest;
			if (gameAccountFlagUpdateRequest == null)
			{
				return false;
			}
			if (HasGameAccount != gameAccountFlagUpdateRequest.HasGameAccount || (HasGameAccount && !GameAccount.Equals(gameAccountFlagUpdateRequest.GameAccount)))
			{
				return false;
			}
			if (HasFlag != gameAccountFlagUpdateRequest.HasFlag || (HasFlag && !Flag.Equals(gameAccountFlagUpdateRequest.Flag)))
			{
				return false;
			}
			if (HasActive != gameAccountFlagUpdateRequest.HasActive || (HasActive && !Active.Equals(gameAccountFlagUpdateRequest.Active)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountFlagUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountFlagUpdateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountFlagUpdateRequest Deserialize(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountFlagUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			GameAccountFlagUpdateRequest gameAccountFlagUpdateRequest = new GameAccountFlagUpdateRequest();
			DeserializeLengthDelimited(stream, gameAccountFlagUpdateRequest);
			return gameAccountFlagUpdateRequest;
		}

		public static GameAccountFlagUpdateRequest DeserializeLengthDelimited(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountFlagUpdateRequest Deserialize(Stream stream, GameAccountFlagUpdateRequest instance, long limit)
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
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 16:
					instance.Flag = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Active = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameAccountFlagUpdateRequest instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasFlag)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Flag);
			}
			if (instance.HasActive)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Active);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameAccount)
			{
				num++;
				uint serializedSize = GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFlag)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Flag);
			}
			if (HasActive)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
