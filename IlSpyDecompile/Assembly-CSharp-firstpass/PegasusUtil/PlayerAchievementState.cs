using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerAchievementState : IProtoBuf
	{
		public bool HasAchievementId;

		private int _AchievementId;

		public bool HasStatus;

		private int _Status;

		public bool HasProgress;

		private int _Progress;

		public bool HasCompletedDate;

		private long _CompletedDate;

		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();

		public int AchievementId
		{
			get
			{
				return _AchievementId;
			}
			set
			{
				_AchievementId = value;
				HasAchievementId = true;
			}
		}

		public int Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status = value;
				HasStatus = true;
			}
		}

		public int Progress
		{
			get
			{
				return _Progress;
			}
			set
			{
				_Progress = value;
				HasProgress = true;
			}
		}

		public long CompletedDate
		{
			get
			{
				return _CompletedDate;
			}
			set
			{
				_CompletedDate = value;
				HasCompletedDate = true;
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
			if (HasAchievementId)
			{
				num ^= AchievementId.GetHashCode();
			}
			if (HasStatus)
			{
				num ^= Status.GetHashCode();
			}
			if (HasProgress)
			{
				num ^= Progress.GetHashCode();
			}
			if (HasCompletedDate)
			{
				num ^= CompletedDate.GetHashCode();
			}
			foreach (RewardItemOutput item in RewardItemOutput)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerAchievementState playerAchievementState = obj as PlayerAchievementState;
			if (playerAchievementState == null)
			{
				return false;
			}
			if (HasAchievementId != playerAchievementState.HasAchievementId || (HasAchievementId && !AchievementId.Equals(playerAchievementState.AchievementId)))
			{
				return false;
			}
			if (HasStatus != playerAchievementState.HasStatus || (HasStatus && !Status.Equals(playerAchievementState.Status)))
			{
				return false;
			}
			if (HasProgress != playerAchievementState.HasProgress || (HasProgress && !Progress.Equals(playerAchievementState.Progress)))
			{
				return false;
			}
			if (HasCompletedDate != playerAchievementState.HasCompletedDate || (HasCompletedDate && !CompletedDate.Equals(playerAchievementState.CompletedDate)))
			{
				return false;
			}
			if (RewardItemOutput.Count != playerAchievementState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < RewardItemOutput.Count; i++)
			{
				if (!RewardItemOutput[i].Equals(playerAchievementState.RewardItemOutput[i]))
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

		public static PlayerAchievementState Deserialize(Stream stream, PlayerAchievementState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerAchievementState DeserializeLengthDelimited(Stream stream)
		{
			PlayerAchievementState playerAchievementState = new PlayerAchievementState();
			DeserializeLengthDelimited(stream, playerAchievementState);
			return playerAchievementState;
		}

		public static PlayerAchievementState DeserializeLengthDelimited(Stream stream, PlayerAchievementState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerAchievementState Deserialize(Stream stream, PlayerAchievementState instance, long limit)
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
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Status = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CompletedDate = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
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

		public static void Serialize(Stream stream, PlayerAchievementState instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			}
			if (instance.HasStatus)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Status);
			}
			if (instance.HasProgress)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			}
			if (instance.HasCompletedDate)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CompletedDate);
			}
			if (instance.RewardItemOutput.Count <= 0)
			{
				return;
			}
			foreach (RewardItemOutput item in instance.RewardItemOutput)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PegasusUtil.RewardItemOutput.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAchievementId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AchievementId);
			}
			if (HasStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Status);
			}
			if (HasProgress)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Progress);
			}
			if (HasCompletedDate)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CompletedDate);
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
