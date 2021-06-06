using System.IO;

namespace PegasusUtil
{
	public class ArenaSession : IProtoBuf
	{
		public bool HasIsActive;

		private bool _IsActive;

		public bool HasIsInRewards;

		private bool _IsInRewards;

		public bool HasInfo;

		private ArenaSeasonInfo _Info;

		public int Wins { get; set; }

		public int Losses { get; set; }

		public bool IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive = value;
				HasIsActive = true;
			}
		}

		public bool IsInRewards
		{
			get
			{
				return _IsInRewards;
			}
			set
			{
				_IsInRewards = value;
				HasIsInRewards = true;
			}
		}

		public ArenaSeasonInfo Info
		{
			get
			{
				return _Info;
			}
			set
			{
				_Info = value;
				HasInfo = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Wins.GetHashCode();
			hashCode ^= Losses.GetHashCode();
			if (HasIsActive)
			{
				hashCode ^= IsActive.GetHashCode();
			}
			if (HasIsInRewards)
			{
				hashCode ^= IsInRewards.GetHashCode();
			}
			if (HasInfo)
			{
				hashCode ^= Info.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ArenaSession arenaSession = obj as ArenaSession;
			if (arenaSession == null)
			{
				return false;
			}
			if (!Wins.Equals(arenaSession.Wins))
			{
				return false;
			}
			if (!Losses.Equals(arenaSession.Losses))
			{
				return false;
			}
			if (HasIsActive != arenaSession.HasIsActive || (HasIsActive && !IsActive.Equals(arenaSession.IsActive)))
			{
				return false;
			}
			if (HasIsInRewards != arenaSession.HasIsInRewards || (HasIsInRewards && !IsInRewards.Equals(arenaSession.IsInRewards)))
			{
				return false;
			}
			if (HasInfo != arenaSession.HasInfo || (HasInfo && !Info.Equals(arenaSession.Info)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ArenaSession Deserialize(Stream stream, ArenaSession instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ArenaSession DeserializeLengthDelimited(Stream stream)
		{
			ArenaSession arenaSession = new ArenaSession();
			DeserializeLengthDelimited(stream, arenaSession);
			return arenaSession;
		}

		public static ArenaSession DeserializeLengthDelimited(Stream stream, ArenaSession instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ArenaSession Deserialize(Stream stream, ArenaSession instance, long limit)
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
				case 8:
					instance.Wins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Losses = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.IsActive = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.IsInRewards = ProtocolParser.ReadBool(stream);
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
							if (instance.Info == null)
							{
								instance.Info = ArenaSeasonInfo.DeserializeLengthDelimited(stream);
							}
							else
							{
								ArenaSeasonInfo.DeserializeLengthDelimited(stream, instance.Info);
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

		public static void Serialize(Stream stream, ArenaSession instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Wins);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Losses);
			if (instance.HasIsActive)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsActive);
			}
			if (instance.HasIsInRewards)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsInRewards);
			}
			if (instance.HasInfo)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
				ArenaSeasonInfo.Serialize(stream, instance.Info);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Wins);
			num += ProtocolParser.SizeOfUInt64((ulong)Losses);
			if (HasIsActive)
			{
				num++;
				num++;
			}
			if (HasIsInRewards)
			{
				num++;
				num++;
			}
			if (HasInfo)
			{
				num += 2;
				uint serializedSize = Info.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2;
		}
	}
}
