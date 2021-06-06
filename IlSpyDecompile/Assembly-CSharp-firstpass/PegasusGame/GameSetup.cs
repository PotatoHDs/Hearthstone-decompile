using System.IO;

namespace PegasusGame
{
	public class GameSetup : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x10
		}

		public bool HasKeepAliveFrequencySeconds;

		private uint _KeepAliveFrequencySeconds;

		public bool HasDisconnectWhenStuckSeconds;

		private uint _DisconnectWhenStuckSeconds;

		public bool HasKeepAliveRetry;

		private uint _KeepAliveRetry;

		public bool HasKeepAliveWaitForInternetSeconds;

		private uint _KeepAliveWaitForInternetSeconds;

		public int Board { get; set; }

		public int MaxSecretZoneSizePerPlayer { get; set; }

		public int MaxSecretsPerPlayer { get; set; }

		public int MaxQuestsPerPlayer { get; set; }

		public int MaxFriendlyMinionsPerPlayer { get; set; }

		public uint KeepAliveFrequencySeconds
		{
			get
			{
				return _KeepAliveFrequencySeconds;
			}
			set
			{
				_KeepAliveFrequencySeconds = value;
				HasKeepAliveFrequencySeconds = true;
			}
		}

		public uint DisconnectWhenStuckSeconds
		{
			get
			{
				return _DisconnectWhenStuckSeconds;
			}
			set
			{
				_DisconnectWhenStuckSeconds = value;
				HasDisconnectWhenStuckSeconds = true;
			}
		}

		public uint KeepAliveRetry
		{
			get
			{
				return _KeepAliveRetry;
			}
			set
			{
				_KeepAliveRetry = value;
				HasKeepAliveRetry = true;
			}
		}

		public uint KeepAliveWaitForInternetSeconds
		{
			get
			{
				return _KeepAliveWaitForInternetSeconds;
			}
			set
			{
				_KeepAliveWaitForInternetSeconds = value;
				HasKeepAliveWaitForInternetSeconds = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Board.GetHashCode();
			hashCode ^= MaxSecretZoneSizePerPlayer.GetHashCode();
			hashCode ^= MaxSecretsPerPlayer.GetHashCode();
			hashCode ^= MaxQuestsPerPlayer.GetHashCode();
			hashCode ^= MaxFriendlyMinionsPerPlayer.GetHashCode();
			if (HasKeepAliveFrequencySeconds)
			{
				hashCode ^= KeepAliveFrequencySeconds.GetHashCode();
			}
			if (HasDisconnectWhenStuckSeconds)
			{
				hashCode ^= DisconnectWhenStuckSeconds.GetHashCode();
			}
			if (HasKeepAliveRetry)
			{
				hashCode ^= KeepAliveRetry.GetHashCode();
			}
			if (HasKeepAliveWaitForInternetSeconds)
			{
				hashCode ^= KeepAliveWaitForInternetSeconds.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameSetup gameSetup = obj as GameSetup;
			if (gameSetup == null)
			{
				return false;
			}
			if (!Board.Equals(gameSetup.Board))
			{
				return false;
			}
			if (!MaxSecretZoneSizePerPlayer.Equals(gameSetup.MaxSecretZoneSizePerPlayer))
			{
				return false;
			}
			if (!MaxSecretsPerPlayer.Equals(gameSetup.MaxSecretsPerPlayer))
			{
				return false;
			}
			if (!MaxQuestsPerPlayer.Equals(gameSetup.MaxQuestsPerPlayer))
			{
				return false;
			}
			if (!MaxFriendlyMinionsPerPlayer.Equals(gameSetup.MaxFriendlyMinionsPerPlayer))
			{
				return false;
			}
			if (HasKeepAliveFrequencySeconds != gameSetup.HasKeepAliveFrequencySeconds || (HasKeepAliveFrequencySeconds && !KeepAliveFrequencySeconds.Equals(gameSetup.KeepAliveFrequencySeconds)))
			{
				return false;
			}
			if (HasDisconnectWhenStuckSeconds != gameSetup.HasDisconnectWhenStuckSeconds || (HasDisconnectWhenStuckSeconds && !DisconnectWhenStuckSeconds.Equals(gameSetup.DisconnectWhenStuckSeconds)))
			{
				return false;
			}
			if (HasKeepAliveRetry != gameSetup.HasKeepAliveRetry || (HasKeepAliveRetry && !KeepAliveRetry.Equals(gameSetup.KeepAliveRetry)))
			{
				return false;
			}
			if (HasKeepAliveWaitForInternetSeconds != gameSetup.HasKeepAliveWaitForInternetSeconds || (HasKeepAliveWaitForInternetSeconds && !KeepAliveWaitForInternetSeconds.Equals(gameSetup.KeepAliveWaitForInternetSeconds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSetup Deserialize(Stream stream, GameSetup instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSetup DeserializeLengthDelimited(Stream stream)
		{
			GameSetup gameSetup = new GameSetup();
			DeserializeLengthDelimited(stream, gameSetup);
			return gameSetup;
		}

		public static GameSetup DeserializeLengthDelimited(Stream stream, GameSetup instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSetup Deserialize(Stream stream, GameSetup instance, long limit)
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
					instance.Board = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.MaxSecretZoneSizePerPlayer = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.MaxSecretsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.MaxQuestsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.MaxFriendlyMinionsPerPlayer = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.KeepAliveFrequencySeconds = ProtocolParser.ReadUInt32(stream);
					continue;
				case 56:
					instance.DisconnectWhenStuckSeconds = ProtocolParser.ReadUInt32(stream);
					continue;
				case 64:
					instance.KeepAliveRetry = ProtocolParser.ReadUInt32(stream);
					continue;
				case 72:
					instance.KeepAliveWaitForInternetSeconds = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameSetup instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Board);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxSecretZoneSizePerPlayer);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxSecretsPerPlayer);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxQuestsPerPlayer);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxFriendlyMinionsPerPlayer);
			if (instance.HasKeepAliveFrequencySeconds)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveFrequencySeconds);
			}
			if (instance.HasDisconnectWhenStuckSeconds)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.DisconnectWhenStuckSeconds);
			}
			if (instance.HasKeepAliveRetry)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveRetry);
			}
			if (instance.HasKeepAliveWaitForInternetSeconds)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.KeepAliveWaitForInternetSeconds);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Board);
			num += ProtocolParser.SizeOfUInt64((ulong)MaxSecretZoneSizePerPlayer);
			num += ProtocolParser.SizeOfUInt64((ulong)MaxSecretsPerPlayer);
			num += ProtocolParser.SizeOfUInt64((ulong)MaxQuestsPerPlayer);
			num += ProtocolParser.SizeOfUInt64((ulong)MaxFriendlyMinionsPerPlayer);
			if (HasKeepAliveFrequencySeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(KeepAliveFrequencySeconds);
			}
			if (HasDisconnectWhenStuckSeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(DisconnectWhenStuckSeconds);
			}
			if (HasKeepAliveRetry)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(KeepAliveRetry);
			}
			if (HasKeepAliveWaitForInternetSeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(KeepAliveWaitForInternetSeconds);
			}
			return num + 5;
		}
	}
}
