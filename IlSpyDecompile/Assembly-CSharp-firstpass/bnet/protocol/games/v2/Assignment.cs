using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.games.v2
{
	public class Assignment : IProtoBuf
	{
		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasTeamIndex;

		private uint _TeamIndex;

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

		public uint TeamIndex
		{
			get
			{
				return _TeamIndex;
			}
			set
			{
				_TeamIndex = value;
				HasTeamIndex = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetTeamIndex(uint val)
		{
			TeamIndex = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasTeamIndex)
			{
				num ^= TeamIndex.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Assignment assignment = obj as Assignment;
			if (assignment == null)
			{
				return false;
			}
			if (HasGameAccount != assignment.HasGameAccount || (HasGameAccount && !GameAccount.Equals(assignment.GameAccount)))
			{
				return false;
			}
			if (HasTeamIndex != assignment.HasTeamIndex || (HasTeamIndex && !TeamIndex.Equals(assignment.TeamIndex)))
			{
				return false;
			}
			return true;
		}

		public static Assignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Assignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Assignment Deserialize(Stream stream, Assignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Assignment DeserializeLengthDelimited(Stream stream)
		{
			Assignment assignment = new Assignment();
			DeserializeLengthDelimited(stream, assignment);
			return assignment;
		}

		public static Assignment DeserializeLengthDelimited(Stream stream, Assignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Assignment Deserialize(Stream stream, Assignment instance, long limit)
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
					instance.TeamIndex = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, Assignment instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasTeamIndex)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.TeamIndex);
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
			if (HasTeamIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(TeamIndex);
			}
			return num;
		}
	}
}
