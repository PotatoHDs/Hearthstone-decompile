using System.IO;

namespace bnet.protocol.account.v1
{
	public class IgrId : IProtoBuf
	{
		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasExternalId;

		private uint _ExternalId;

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

		public uint ExternalId
		{
			get
			{
				return _ExternalId;
			}
			set
			{
				_ExternalId = value;
				HasExternalId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetExternalId(uint val)
		{
			ExternalId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasExternalId)
			{
				num ^= ExternalId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IgrId igrId = obj as IgrId;
			if (igrId == null)
			{
				return false;
			}
			if (HasGameAccount != igrId.HasGameAccount || (HasGameAccount && !GameAccount.Equals(igrId.GameAccount)))
			{
				return false;
			}
			if (HasExternalId != igrId.HasExternalId || (HasExternalId && !ExternalId.Equals(igrId.ExternalId)))
			{
				return false;
			}
			return true;
		}

		public static IgrId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgrId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IgrId Deserialize(Stream stream, IgrId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IgrId DeserializeLengthDelimited(Stream stream)
		{
			IgrId igrId = new IgrId();
			DeserializeLengthDelimited(stream, igrId);
			return igrId;
		}

		public static IgrId DeserializeLengthDelimited(Stream stream, IgrId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IgrId Deserialize(Stream stream, IgrId instance, long limit)
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
				case 21:
					instance.ExternalId = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, IgrId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasExternalId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.ExternalId);
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
			if (HasExternalId)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
