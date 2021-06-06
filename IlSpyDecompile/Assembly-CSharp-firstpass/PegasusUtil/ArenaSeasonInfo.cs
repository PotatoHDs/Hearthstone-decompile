using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class ArenaSeasonInfo : IProtoBuf
	{
		public bool HasSeason;

		private ArenaSeasonSpec _Season;

		public bool HasSeasonEndingSoonDays;

		private int _SeasonEndingSoonDays;

		public bool HasSeasonEndingSoonPrefab;

		private string _SeasonEndingSoonPrefab;

		public bool HasSeasonEndingSoonPrefabExtra;

		private string _SeasonEndingSoonPrefabExtra;

		public bool HasNextStartSecondsFromNow;

		private ulong _NextStartSecondsFromNow;

		public bool HasNextSeasonId;

		private int _NextSeasonId;

		public bool HasNextSeasonComingSoonDays;

		private int _NextSeasonComingSoonDays;

		public bool HasNextSeasonComingSoonPrefab;

		private string _NextSeasonComingSoonPrefab;

		public bool HasNextSeasonComingSoonPrefabExtra;

		private string _NextSeasonComingSoonPrefabExtra;

		private List<LocalizedString> _NextSeasonStrings = new List<LocalizedString>();

		public ArenaSeasonSpec Season
		{
			get
			{
				return _Season;
			}
			set
			{
				_Season = value;
				HasSeason = value != null;
			}
		}

		public int SeasonEndingSoonDays
		{
			get
			{
				return _SeasonEndingSoonDays;
			}
			set
			{
				_SeasonEndingSoonDays = value;
				HasSeasonEndingSoonDays = true;
			}
		}

		public string SeasonEndingSoonPrefab
		{
			get
			{
				return _SeasonEndingSoonPrefab;
			}
			set
			{
				_SeasonEndingSoonPrefab = value;
				HasSeasonEndingSoonPrefab = value != null;
			}
		}

		public string SeasonEndingSoonPrefabExtra
		{
			get
			{
				return _SeasonEndingSoonPrefabExtra;
			}
			set
			{
				_SeasonEndingSoonPrefabExtra = value;
				HasSeasonEndingSoonPrefabExtra = value != null;
			}
		}

		public ulong NextStartSecondsFromNow
		{
			get
			{
				return _NextStartSecondsFromNow;
			}
			set
			{
				_NextStartSecondsFromNow = value;
				HasNextStartSecondsFromNow = true;
			}
		}

		public int NextSeasonId
		{
			get
			{
				return _NextSeasonId;
			}
			set
			{
				_NextSeasonId = value;
				HasNextSeasonId = true;
			}
		}

		public int NextSeasonComingSoonDays
		{
			get
			{
				return _NextSeasonComingSoonDays;
			}
			set
			{
				_NextSeasonComingSoonDays = value;
				HasNextSeasonComingSoonDays = true;
			}
		}

		public string NextSeasonComingSoonPrefab
		{
			get
			{
				return _NextSeasonComingSoonPrefab;
			}
			set
			{
				_NextSeasonComingSoonPrefab = value;
				HasNextSeasonComingSoonPrefab = value != null;
			}
		}

		public string NextSeasonComingSoonPrefabExtra
		{
			get
			{
				return _NextSeasonComingSoonPrefabExtra;
			}
			set
			{
				_NextSeasonComingSoonPrefabExtra = value;
				HasNextSeasonComingSoonPrefabExtra = value != null;
			}
		}

		public List<LocalizedString> NextSeasonStrings
		{
			get
			{
				return _NextSeasonStrings;
			}
			set
			{
				_NextSeasonStrings = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSeason)
			{
				num ^= Season.GetHashCode();
			}
			if (HasSeasonEndingSoonDays)
			{
				num ^= SeasonEndingSoonDays.GetHashCode();
			}
			if (HasSeasonEndingSoonPrefab)
			{
				num ^= SeasonEndingSoonPrefab.GetHashCode();
			}
			if (HasSeasonEndingSoonPrefabExtra)
			{
				num ^= SeasonEndingSoonPrefabExtra.GetHashCode();
			}
			if (HasNextStartSecondsFromNow)
			{
				num ^= NextStartSecondsFromNow.GetHashCode();
			}
			if (HasNextSeasonId)
			{
				num ^= NextSeasonId.GetHashCode();
			}
			if (HasNextSeasonComingSoonDays)
			{
				num ^= NextSeasonComingSoonDays.GetHashCode();
			}
			if (HasNextSeasonComingSoonPrefab)
			{
				num ^= NextSeasonComingSoonPrefab.GetHashCode();
			}
			if (HasNextSeasonComingSoonPrefabExtra)
			{
				num ^= NextSeasonComingSoonPrefabExtra.GetHashCode();
			}
			foreach (LocalizedString nextSeasonString in NextSeasonStrings)
			{
				num ^= nextSeasonString.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ArenaSeasonInfo arenaSeasonInfo = obj as ArenaSeasonInfo;
			if (arenaSeasonInfo == null)
			{
				return false;
			}
			if (HasSeason != arenaSeasonInfo.HasSeason || (HasSeason && !Season.Equals(arenaSeasonInfo.Season)))
			{
				return false;
			}
			if (HasSeasonEndingSoonDays != arenaSeasonInfo.HasSeasonEndingSoonDays || (HasSeasonEndingSoonDays && !SeasonEndingSoonDays.Equals(arenaSeasonInfo.SeasonEndingSoonDays)))
			{
				return false;
			}
			if (HasSeasonEndingSoonPrefab != arenaSeasonInfo.HasSeasonEndingSoonPrefab || (HasSeasonEndingSoonPrefab && !SeasonEndingSoonPrefab.Equals(arenaSeasonInfo.SeasonEndingSoonPrefab)))
			{
				return false;
			}
			if (HasSeasonEndingSoonPrefabExtra != arenaSeasonInfo.HasSeasonEndingSoonPrefabExtra || (HasSeasonEndingSoonPrefabExtra && !SeasonEndingSoonPrefabExtra.Equals(arenaSeasonInfo.SeasonEndingSoonPrefabExtra)))
			{
				return false;
			}
			if (HasNextStartSecondsFromNow != arenaSeasonInfo.HasNextStartSecondsFromNow || (HasNextStartSecondsFromNow && !NextStartSecondsFromNow.Equals(arenaSeasonInfo.NextStartSecondsFromNow)))
			{
				return false;
			}
			if (HasNextSeasonId != arenaSeasonInfo.HasNextSeasonId || (HasNextSeasonId && !NextSeasonId.Equals(arenaSeasonInfo.NextSeasonId)))
			{
				return false;
			}
			if (HasNextSeasonComingSoonDays != arenaSeasonInfo.HasNextSeasonComingSoonDays || (HasNextSeasonComingSoonDays && !NextSeasonComingSoonDays.Equals(arenaSeasonInfo.NextSeasonComingSoonDays)))
			{
				return false;
			}
			if (HasNextSeasonComingSoonPrefab != arenaSeasonInfo.HasNextSeasonComingSoonPrefab || (HasNextSeasonComingSoonPrefab && !NextSeasonComingSoonPrefab.Equals(arenaSeasonInfo.NextSeasonComingSoonPrefab)))
			{
				return false;
			}
			if (HasNextSeasonComingSoonPrefabExtra != arenaSeasonInfo.HasNextSeasonComingSoonPrefabExtra || (HasNextSeasonComingSoonPrefabExtra && !NextSeasonComingSoonPrefabExtra.Equals(arenaSeasonInfo.NextSeasonComingSoonPrefabExtra)))
			{
				return false;
			}
			if (NextSeasonStrings.Count != arenaSeasonInfo.NextSeasonStrings.Count)
			{
				return false;
			}
			for (int i = 0; i < NextSeasonStrings.Count; i++)
			{
				if (!NextSeasonStrings[i].Equals(arenaSeasonInfo.NextSeasonStrings[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ArenaSeasonInfo Deserialize(Stream stream, ArenaSeasonInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ArenaSeasonInfo DeserializeLengthDelimited(Stream stream)
		{
			ArenaSeasonInfo arenaSeasonInfo = new ArenaSeasonInfo();
			DeserializeLengthDelimited(stream, arenaSeasonInfo);
			return arenaSeasonInfo;
		}

		public static ArenaSeasonInfo DeserializeLengthDelimited(Stream stream, ArenaSeasonInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ArenaSeasonInfo Deserialize(Stream stream, ArenaSeasonInfo instance, long limit)
		{
			if (instance.NextSeasonStrings == null)
			{
				instance.NextSeasonStrings = new List<LocalizedString>();
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
					if (instance.Season == null)
					{
						instance.Season = ArenaSeasonSpec.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSeasonSpec.DeserializeLengthDelimited(stream, instance.Season);
					}
					continue;
				case 24:
					instance.SeasonEndingSoonDays = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.SeasonEndingSoonPrefab = ProtocolParser.ReadString(stream);
					continue;
				case 74:
					instance.SeasonEndingSoonPrefabExtra = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.NextStartSecondsFromNow = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.NextSeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.NextSeasonComingSoonDays = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.NextSeasonComingSoonPrefab = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					instance.NextSeasonComingSoonPrefabExtra = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.NextSeasonStrings.Add(LocalizedString.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ArenaSeasonInfo instance)
		{
			if (instance.HasSeason)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Season.GetSerializedSize());
				ArenaSeasonSpec.Serialize(stream, instance.Season);
			}
			if (instance.HasSeasonEndingSoonDays)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonEndingSoonDays);
			}
			if (instance.HasSeasonEndingSoonPrefab)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SeasonEndingSoonPrefab));
			}
			if (instance.HasSeasonEndingSoonPrefabExtra)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SeasonEndingSoonPrefabExtra));
			}
			if (instance.HasNextStartSecondsFromNow)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.NextStartSecondsFromNow);
			}
			if (instance.HasNextSeasonId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NextSeasonId);
			}
			if (instance.HasNextSeasonComingSoonDays)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NextSeasonComingSoonDays);
			}
			if (instance.HasNextSeasonComingSoonPrefab)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextSeasonComingSoonPrefab));
			}
			if (instance.HasNextSeasonComingSoonPrefabExtra)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NextSeasonComingSoonPrefabExtra));
			}
			if (instance.NextSeasonStrings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString nextSeasonString in instance.NextSeasonStrings)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, nextSeasonString.GetSerializedSize());
				LocalizedString.Serialize(stream, nextSeasonString);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSeason)
			{
				num++;
				uint serializedSize = Season.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSeasonEndingSoonDays)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonEndingSoonDays);
			}
			if (HasSeasonEndingSoonPrefab)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SeasonEndingSoonPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasSeasonEndingSoonPrefabExtra)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(SeasonEndingSoonPrefabExtra);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasNextStartSecondsFromNow)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(NextStartSecondsFromNow);
			}
			if (HasNextSeasonId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NextSeasonId);
			}
			if (HasNextSeasonComingSoonDays)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NextSeasonComingSoonDays);
			}
			if (HasNextSeasonComingSoonPrefab)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(NextSeasonComingSoonPrefab);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasNextSeasonComingSoonPrefabExtra)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(NextSeasonComingSoonPrefabExtra);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (NextSeasonStrings.Count > 0)
			{
				foreach (LocalizedString nextSeasonString in NextSeasonStrings)
				{
					num++;
					uint serializedSize2 = nextSeasonString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
