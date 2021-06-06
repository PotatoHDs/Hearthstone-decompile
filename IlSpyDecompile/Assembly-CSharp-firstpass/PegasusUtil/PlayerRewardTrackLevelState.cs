using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerRewardTrackLevelState : IProtoBuf
	{
		public bool HasLevel;

		private int _Level;

		public bool HasFreeRewardStatus;

		private int _FreeRewardStatus;

		public bool HasPaidRewardStatus;

		private int _PaidRewardStatus;

		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();

		public int Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
			}
		}

		public int FreeRewardStatus
		{
			get
			{
				return _FreeRewardStatus;
			}
			set
			{
				_FreeRewardStatus = value;
				HasFreeRewardStatus = true;
			}
		}

		public int PaidRewardStatus
		{
			get
			{
				return _PaidRewardStatus;
			}
			set
			{
				_PaidRewardStatus = value;
				HasPaidRewardStatus = true;
			}
		}

		public List<RewardItemOutput> RewardItemOutput
		{
			get
			{
				return _RewardItemOutput;
			}
			set
			{
				_RewardItemOutput = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			if (HasFreeRewardStatus)
			{
				num ^= FreeRewardStatus.GetHashCode();
			}
			if (HasPaidRewardStatus)
			{
				num ^= PaidRewardStatus.GetHashCode();
			}
			foreach (RewardItemOutput item in RewardItemOutput)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerRewardTrackLevelState playerRewardTrackLevelState = obj as PlayerRewardTrackLevelState;
			if (playerRewardTrackLevelState == null)
			{
				return false;
			}
			if (HasLevel != playerRewardTrackLevelState.HasLevel || (HasLevel && !Level.Equals(playerRewardTrackLevelState.Level)))
			{
				return false;
			}
			if (HasFreeRewardStatus != playerRewardTrackLevelState.HasFreeRewardStatus || (HasFreeRewardStatus && !FreeRewardStatus.Equals(playerRewardTrackLevelState.FreeRewardStatus)))
			{
				return false;
			}
			if (HasPaidRewardStatus != playerRewardTrackLevelState.HasPaidRewardStatus || (HasPaidRewardStatus && !PaidRewardStatus.Equals(playerRewardTrackLevelState.PaidRewardStatus)))
			{
				return false;
			}
			if (RewardItemOutput.Count != playerRewardTrackLevelState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < RewardItemOutput.Count; i++)
			{
				if (!RewardItemOutput[i].Equals(playerRewardTrackLevelState.RewardItemOutput[i]))
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

		public static PlayerRewardTrackLevelState Deserialize(Stream stream, PlayerRewardTrackLevelState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerRewardTrackLevelState DeserializeLengthDelimited(Stream stream)
		{
			PlayerRewardTrackLevelState playerRewardTrackLevelState = new PlayerRewardTrackLevelState();
			DeserializeLengthDelimited(stream, playerRewardTrackLevelState);
			return playerRewardTrackLevelState;
		}

		public static PlayerRewardTrackLevelState DeserializeLengthDelimited(Stream stream, PlayerRewardTrackLevelState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerRewardTrackLevelState Deserialize(Stream stream, PlayerRewardTrackLevelState instance, long limit)
		{
			if (instance.RewardItemOutput == null)
			{
				instance.RewardItemOutput = new List<RewardItemOutput>();
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
					instance.Level = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.FreeRewardStatus = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PaidRewardStatus = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.RewardItemOutput.Add(PegasusUtil.RewardItemOutput.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, PlayerRewardTrackLevelState instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasFreeRewardStatus)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FreeRewardStatus);
			}
			if (instance.HasPaidRewardStatus)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PaidRewardStatus);
			}
			if (instance.RewardItemOutput.Count <= 0)
			{
				return;
			}
			foreach (RewardItemOutput item in instance.RewardItemOutput)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PegasusUtil.RewardItemOutput.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
			}
			if (HasFreeRewardStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FreeRewardStatus);
			}
			if (HasPaidRewardStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PaidRewardStatus);
			}
			if (RewardItemOutput.Count > 0)
			{
				foreach (RewardItemOutput item in RewardItemOutput)
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
