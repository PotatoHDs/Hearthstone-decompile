using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameStatus : IProtoBuf
	{
		public bool HasIsSuspended;

		private bool _IsSuspended;

		public bool HasIsBanned;

		private bool _IsBanned;

		public bool HasSuspensionExpires;

		private ulong _SuspensionExpires;

		public bool HasProgram;

		private uint _Program;

		public bool HasIsLocked;

		private bool _IsLocked;

		public bool HasIsBamUnlockable;

		private bool _IsBamUnlockable;

		public bool IsSuspended
		{
			get
			{
				return _IsSuspended;
			}
			set
			{
				_IsSuspended = value;
				HasIsSuspended = true;
			}
		}

		public bool IsBanned
		{
			get
			{
				return _IsBanned;
			}
			set
			{
				_IsBanned = value;
				HasIsBanned = true;
			}
		}

		public ulong SuspensionExpires
		{
			get
			{
				return _SuspensionExpires;
			}
			set
			{
				_SuspensionExpires = value;
				HasSuspensionExpires = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public bool IsLocked
		{
			get
			{
				return _IsLocked;
			}
			set
			{
				_IsLocked = value;
				HasIsLocked = true;
			}
		}

		public bool IsBamUnlockable
		{
			get
			{
				return _IsBamUnlockable;
			}
			set
			{
				_IsBamUnlockable = value;
				HasIsBamUnlockable = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIsSuspended(bool val)
		{
			IsSuspended = val;
		}

		public void SetIsBanned(bool val)
		{
			IsBanned = val;
		}

		public void SetSuspensionExpires(ulong val)
		{
			SuspensionExpires = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetIsLocked(bool val)
		{
			IsLocked = val;
		}

		public void SetIsBamUnlockable(bool val)
		{
			IsBamUnlockable = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIsSuspended)
			{
				num ^= IsSuspended.GetHashCode();
			}
			if (HasIsBanned)
			{
				num ^= IsBanned.GetHashCode();
			}
			if (HasSuspensionExpires)
			{
				num ^= SuspensionExpires.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasIsLocked)
			{
				num ^= IsLocked.GetHashCode();
			}
			if (HasIsBamUnlockable)
			{
				num ^= IsBamUnlockable.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameStatus gameStatus = obj as GameStatus;
			if (gameStatus == null)
			{
				return false;
			}
			if (HasIsSuspended != gameStatus.HasIsSuspended || (HasIsSuspended && !IsSuspended.Equals(gameStatus.IsSuspended)))
			{
				return false;
			}
			if (HasIsBanned != gameStatus.HasIsBanned || (HasIsBanned && !IsBanned.Equals(gameStatus.IsBanned)))
			{
				return false;
			}
			if (HasSuspensionExpires != gameStatus.HasSuspensionExpires || (HasSuspensionExpires && !SuspensionExpires.Equals(gameStatus.SuspensionExpires)))
			{
				return false;
			}
			if (HasProgram != gameStatus.HasProgram || (HasProgram && !Program.Equals(gameStatus.Program)))
			{
				return false;
			}
			if (HasIsLocked != gameStatus.HasIsLocked || (HasIsLocked && !IsLocked.Equals(gameStatus.IsLocked)))
			{
				return false;
			}
			if (HasIsBamUnlockable != gameStatus.HasIsBamUnlockable || (HasIsBamUnlockable && !IsBamUnlockable.Equals(gameStatus.IsBamUnlockable)))
			{
				return false;
			}
			return true;
		}

		public static GameStatus ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameStatus>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameStatus Deserialize(Stream stream, GameStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameStatus DeserializeLengthDelimited(Stream stream)
		{
			GameStatus gameStatus = new GameStatus();
			DeserializeLengthDelimited(stream, gameStatus);
			return gameStatus;
		}

		public static GameStatus DeserializeLengthDelimited(Stream stream, GameStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameStatus Deserialize(Stream stream, GameStatus instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 32:
					instance.IsSuspended = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.IsBanned = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.SuspensionExpires = ProtocolParser.ReadUInt64(stream);
					continue;
				case 61:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 64:
					instance.IsLocked = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.IsBamUnlockable = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameStatus instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIsSuspended)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsSuspended);
			}
			if (instance.HasIsBanned)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsBanned);
			}
			if (instance.HasSuspensionExpires)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.SuspensionExpires);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasIsLocked)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsLocked);
			}
			if (instance.HasIsBamUnlockable)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsBamUnlockable);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIsSuspended)
			{
				num++;
				num++;
			}
			if (HasIsBanned)
			{
				num++;
				num++;
			}
			if (HasSuspensionExpires)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SuspensionExpires);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasIsLocked)
			{
				num++;
				num++;
			}
			if (HasIsBamUnlockable)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
