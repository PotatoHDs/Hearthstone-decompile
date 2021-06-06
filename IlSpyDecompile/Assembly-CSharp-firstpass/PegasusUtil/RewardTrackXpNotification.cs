using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class RewardTrackXpNotification : IProtoBuf
	{
		public enum PacketID
		{
			ID = 617,
			System = 0
		}

		public bool HasRewardSourceType;

		private int _RewardSourceType;

		public bool HasRewardSourceId;

		private int _RewardSourceId;

		public bool HasPrevXp;

		private int _PrevXp;

		public bool HasCurrXp;

		private int _CurrXp;

		public bool HasPrevLevel;

		private int _PrevLevel;

		public bool HasCurrLevel;

		private int _CurrLevel;

		private List<RewardTrackXpChange> _XpChange = new List<RewardTrackXpChange>();

		public int RewardSourceType
		{
			get
			{
				return _RewardSourceType;
			}
			set
			{
				_RewardSourceType = value;
				HasRewardSourceType = true;
			}
		}

		public int RewardSourceId
		{
			get
			{
				return _RewardSourceId;
			}
			set
			{
				_RewardSourceId = value;
				HasRewardSourceId = true;
			}
		}

		public int PrevXp
		{
			get
			{
				return _PrevXp;
			}
			set
			{
				_PrevXp = value;
				HasPrevXp = true;
			}
		}

		public int CurrXp
		{
			get
			{
				return _CurrXp;
			}
			set
			{
				_CurrXp = value;
				HasCurrXp = true;
			}
		}

		public int PrevLevel
		{
			get
			{
				return _PrevLevel;
			}
			set
			{
				_PrevLevel = value;
				HasPrevLevel = true;
			}
		}

		public int CurrLevel
		{
			get
			{
				return _CurrLevel;
			}
			set
			{
				_CurrLevel = value;
				HasCurrLevel = true;
			}
		}

		public List<RewardTrackXpChange> XpChange
		{
			get
			{
				return _XpChange;
			}
			set
			{
				_XpChange = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardSourceType)
			{
				num ^= RewardSourceType.GetHashCode();
			}
			if (HasRewardSourceId)
			{
				num ^= RewardSourceId.GetHashCode();
			}
			if (HasPrevXp)
			{
				num ^= PrevXp.GetHashCode();
			}
			if (HasCurrXp)
			{
				num ^= CurrXp.GetHashCode();
			}
			if (HasPrevLevel)
			{
				num ^= PrevLevel.GetHashCode();
			}
			if (HasCurrLevel)
			{
				num ^= CurrLevel.GetHashCode();
			}
			foreach (RewardTrackXpChange item in XpChange)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardTrackXpNotification rewardTrackXpNotification = obj as RewardTrackXpNotification;
			if (rewardTrackXpNotification == null)
			{
				return false;
			}
			if (HasRewardSourceType != rewardTrackXpNotification.HasRewardSourceType || (HasRewardSourceType && !RewardSourceType.Equals(rewardTrackXpNotification.RewardSourceType)))
			{
				return false;
			}
			if (HasRewardSourceId != rewardTrackXpNotification.HasRewardSourceId || (HasRewardSourceId && !RewardSourceId.Equals(rewardTrackXpNotification.RewardSourceId)))
			{
				return false;
			}
			if (HasPrevXp != rewardTrackXpNotification.HasPrevXp || (HasPrevXp && !PrevXp.Equals(rewardTrackXpNotification.PrevXp)))
			{
				return false;
			}
			if (HasCurrXp != rewardTrackXpNotification.HasCurrXp || (HasCurrXp && !CurrXp.Equals(rewardTrackXpNotification.CurrXp)))
			{
				return false;
			}
			if (HasPrevLevel != rewardTrackXpNotification.HasPrevLevel || (HasPrevLevel && !PrevLevel.Equals(rewardTrackXpNotification.PrevLevel)))
			{
				return false;
			}
			if (HasCurrLevel != rewardTrackXpNotification.HasCurrLevel || (HasCurrLevel && !CurrLevel.Equals(rewardTrackXpNotification.CurrLevel)))
			{
				return false;
			}
			if (XpChange.Count != rewardTrackXpNotification.XpChange.Count)
			{
				return false;
			}
			for (int i = 0; i < XpChange.Count; i++)
			{
				if (!XpChange[i].Equals(rewardTrackXpNotification.XpChange[i]))
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

		public static RewardTrackXpNotification Deserialize(Stream stream, RewardTrackXpNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardTrackXpNotification DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackXpNotification rewardTrackXpNotification = new RewardTrackXpNotification();
			DeserializeLengthDelimited(stream, rewardTrackXpNotification);
			return rewardTrackXpNotification;
		}

		public static RewardTrackXpNotification DeserializeLengthDelimited(Stream stream, RewardTrackXpNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardTrackXpNotification Deserialize(Stream stream, RewardTrackXpNotification instance, long limit)
		{
			if (instance.XpChange == null)
			{
				instance.XpChange = new List<RewardTrackXpChange>();
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
					instance.RewardSourceType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.RewardSourceId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PrevXp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CurrXp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.PrevLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.CurrLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					instance.XpChange.Add(RewardTrackXpChange.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RewardTrackXpNotification instance)
		{
			if (instance.HasRewardSourceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardSourceType);
			}
			if (instance.HasRewardSourceId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardSourceId);
			}
			if (instance.HasPrevXp)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrevXp);
			}
			if (instance.HasCurrXp)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrXp);
			}
			if (instance.HasPrevLevel)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrevLevel);
			}
			if (instance.HasCurrLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrLevel);
			}
			if (instance.XpChange.Count <= 0)
			{
				return;
			}
			foreach (RewardTrackXpChange item in instance.XpChange)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RewardTrackXpChange.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardSourceType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardSourceType);
			}
			if (HasRewardSourceId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardSourceId);
			}
			if (HasPrevXp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrevXp);
			}
			if (HasCurrXp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrXp);
			}
			if (HasPrevLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrevLevel);
			}
			if (HasCurrLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrLevel);
			}
			if (XpChange.Count > 0)
			{
				foreach (RewardTrackXpChange item in XpChange)
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
