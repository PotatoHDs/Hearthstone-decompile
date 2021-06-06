using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class Player : IProtoBuf
	{
		public bool HasBattleNetIdLo;

		private long _BattleNetIdLo;

		public bool HasGameAccountId;

		private long _GameAccountId;

		public bool HasBnetRegion;

		private string _BnetRegion;

		public bool HasLocale;

		private string _Locale;

		public long BattleNetIdLo
		{
			get
			{
				return _BattleNetIdLo;
			}
			set
			{
				_BattleNetIdLo = value;
				HasBattleNetIdLo = true;
			}
		}

		public long GameAccountId
		{
			get
			{
				return _GameAccountId;
			}
			set
			{
				_GameAccountId = value;
				HasGameAccountId = true;
			}
		}

		public string BnetRegion
		{
			get
			{
				return _BnetRegion;
			}
			set
			{
				_BnetRegion = value;
				HasBnetRegion = value != null;
			}
		}

		public string Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBattleNetIdLo)
			{
				num ^= BattleNetIdLo.GetHashCode();
			}
			if (HasGameAccountId)
			{
				num ^= GameAccountId.GetHashCode();
			}
			if (HasBnetRegion)
			{
				num ^= BnetRegion.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (HasBattleNetIdLo != player.HasBattleNetIdLo || (HasBattleNetIdLo && !BattleNetIdLo.Equals(player.BattleNetIdLo)))
			{
				return false;
			}
			if (HasGameAccountId != player.HasGameAccountId || (HasGameAccountId && !GameAccountId.Equals(player.GameAccountId)))
			{
				return false;
			}
			if (HasBnetRegion != player.HasBnetRegion || (HasBnetRegion && !BnetRegion.Equals(player.BnetRegion)))
			{
				return false;
			}
			if (HasLocale != player.HasLocale || (HasLocale && !Locale.Equals(player.Locale)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Player Deserialize(Stream stream, Player instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			DeserializeLengthDelimited(stream, player);
			return player;
		}

		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Player Deserialize(Stream stream, Player instance, long limit)
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
					instance.BattleNetIdLo = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GameAccountId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.BnetRegion = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Locale = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, Player instance)
		{
			if (instance.HasBattleNetIdLo)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BattleNetIdLo);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameAccountId);
			}
			if (instance.HasBnetRegion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BnetRegion));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBattleNetIdLo)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BattleNetIdLo);
			}
			if (HasGameAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameAccountId);
			}
			if (HasBnetRegion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BnetRegion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLocale)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
