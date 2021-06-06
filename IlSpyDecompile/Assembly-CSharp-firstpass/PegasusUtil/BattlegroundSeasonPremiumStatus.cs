using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class BattlegroundSeasonPremiumStatus : IProtoBuf
	{
		private List<BattlegroundSeasonRewardType> _PremiumRewardUnlocked = new List<BattlegroundSeasonRewardType>();

		public uint SeasonID { get; set; }

		public uint PackType { get; set; }

		public uint NumPacksOpened { get; set; }

		public List<BattlegroundSeasonRewardType> PremiumRewardUnlocked
		{
			get
			{
				return _PremiumRewardUnlocked;
			}
			set
			{
				_PremiumRewardUnlocked = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= SeasonID.GetHashCode();
			hashCode ^= PackType.GetHashCode();
			hashCode ^= NumPacksOpened.GetHashCode();
			foreach (BattlegroundSeasonRewardType item in PremiumRewardUnlocked)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus = obj as BattlegroundSeasonPremiumStatus;
			if (battlegroundSeasonPremiumStatus == null)
			{
				return false;
			}
			if (!SeasonID.Equals(battlegroundSeasonPremiumStatus.SeasonID))
			{
				return false;
			}
			if (!PackType.Equals(battlegroundSeasonPremiumStatus.PackType))
			{
				return false;
			}
			if (!NumPacksOpened.Equals(battlegroundSeasonPremiumStatus.NumPacksOpened))
			{
				return false;
			}
			if (PremiumRewardUnlocked.Count != battlegroundSeasonPremiumStatus.PremiumRewardUnlocked.Count)
			{
				return false;
			}
			for (int i = 0; i < PremiumRewardUnlocked.Count; i++)
			{
				if (!PremiumRewardUnlocked[i].Equals(battlegroundSeasonPremiumStatus.PremiumRewardUnlocked[i]))
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

		public static BattlegroundSeasonPremiumStatus Deserialize(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlegroundSeasonPremiumStatus DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundSeasonPremiumStatus battlegroundSeasonPremiumStatus = new BattlegroundSeasonPremiumStatus();
			DeserializeLengthDelimited(stream, battlegroundSeasonPremiumStatus);
			return battlegroundSeasonPremiumStatus;
		}

		public static BattlegroundSeasonPremiumStatus DeserializeLengthDelimited(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlegroundSeasonPremiumStatus Deserialize(Stream stream, BattlegroundSeasonPremiumStatus instance, long limit)
		{
			if (instance.PremiumRewardUnlocked == null)
			{
				instance.PremiumRewardUnlocked = new List<BattlegroundSeasonRewardType>();
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
					instance.SeasonID = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.PackType = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.NumPacksOpened = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.PremiumRewardUnlocked.Add((BattlegroundSeasonRewardType)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, BattlegroundSeasonPremiumStatus instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.SeasonID);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.PackType);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.NumPacksOpened);
			if (instance.PremiumRewardUnlocked.Count <= 0)
			{
				return;
			}
			foreach (BattlegroundSeasonRewardType item in instance.PremiumRewardUnlocked)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(SeasonID);
			num += ProtocolParser.SizeOfUInt32(PackType);
			num += ProtocolParser.SizeOfUInt32(NumPacksOpened);
			if (PremiumRewardUnlocked.Count > 0)
			{
				foreach (BattlegroundSeasonRewardType item in PremiumRewardUnlocked)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item);
				}
			}
			return num + 3;
		}
	}
}
