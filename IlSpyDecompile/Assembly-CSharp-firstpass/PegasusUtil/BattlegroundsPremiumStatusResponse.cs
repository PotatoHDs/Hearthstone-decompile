using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class BattlegroundsPremiumStatusResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 375,
			System = 0
		}

		private List<BattlegroundSeasonPremiumStatus> _SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>();

		public List<BattlegroundSeasonPremiumStatus> SeasonPremiumStatus
		{
			get
			{
				return _SeasonPremiumStatus;
			}
			set
			{
				_SeasonPremiumStatus = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (BattlegroundSeasonPremiumStatus item in SeasonPremiumStatus)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BattlegroundsPremiumStatusResponse battlegroundsPremiumStatusResponse = obj as BattlegroundsPremiumStatusResponse;
			if (battlegroundsPremiumStatusResponse == null)
			{
				return false;
			}
			if (SeasonPremiumStatus.Count != battlegroundsPremiumStatusResponse.SeasonPremiumStatus.Count)
			{
				return false;
			}
			for (int i = 0; i < SeasonPremiumStatus.Count; i++)
			{
				if (!SeasonPremiumStatus[i].Equals(battlegroundsPremiumStatusResponse.SeasonPremiumStatus[i]))
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

		public static BattlegroundsPremiumStatusResponse Deserialize(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlegroundsPremiumStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsPremiumStatusResponse battlegroundsPremiumStatusResponse = new BattlegroundsPremiumStatusResponse();
			DeserializeLengthDelimited(stream, battlegroundsPremiumStatusResponse);
			return battlegroundsPremiumStatusResponse;
		}

		public static BattlegroundsPremiumStatusResponse DeserializeLengthDelimited(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlegroundsPremiumStatusResponse Deserialize(Stream stream, BattlegroundsPremiumStatusResponse instance, long limit)
		{
			if (instance.SeasonPremiumStatus == null)
			{
				instance.SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>();
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
					instance.SeasonPremiumStatus.Add(BattlegroundSeasonPremiumStatus.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BattlegroundsPremiumStatusResponse instance)
		{
			if (instance.SeasonPremiumStatus.Count <= 0)
			{
				return;
			}
			foreach (BattlegroundSeasonPremiumStatus item in instance.SeasonPremiumStatus)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				BattlegroundSeasonPremiumStatus.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (SeasonPremiumStatus.Count > 0)
			{
				foreach (BattlegroundSeasonPremiumStatus item in SeasonPremiumStatus)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
