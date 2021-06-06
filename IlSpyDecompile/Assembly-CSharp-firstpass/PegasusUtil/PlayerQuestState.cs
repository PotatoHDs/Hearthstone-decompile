using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerQuestState : IProtoBuf
	{
		public bool HasQuestId;

		private int _QuestId;

		public bool HasStatus;

		private int _Status;

		public bool HasProgress;

		private int _Progress;

		private List<RewardItemOutput> _RewardItemOutput = new List<RewardItemOutput>();

		public int QuestId
		{
			get
			{
				return _QuestId;
			}
			set
			{
				_QuestId = value;
				HasQuestId = true;
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
			if (HasQuestId)
			{
				num ^= QuestId.GetHashCode();
			}
			if (HasStatus)
			{
				num ^= Status.GetHashCode();
			}
			if (HasProgress)
			{
				num ^= Progress.GetHashCode();
			}
			foreach (RewardItemOutput item in RewardItemOutput)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerQuestState playerQuestState = obj as PlayerQuestState;
			if (playerQuestState == null)
			{
				return false;
			}
			if (HasQuestId != playerQuestState.HasQuestId || (HasQuestId && !QuestId.Equals(playerQuestState.QuestId)))
			{
				return false;
			}
			if (HasStatus != playerQuestState.HasStatus || (HasStatus && !Status.Equals(playerQuestState.Status)))
			{
				return false;
			}
			if (HasProgress != playerQuestState.HasProgress || (HasProgress && !Progress.Equals(playerQuestState.Progress)))
			{
				return false;
			}
			if (RewardItemOutput.Count != playerQuestState.RewardItemOutput.Count)
			{
				return false;
			}
			for (int i = 0; i < RewardItemOutput.Count; i++)
			{
				if (!RewardItemOutput[i].Equals(playerQuestState.RewardItemOutput[i]))
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

		public static PlayerQuestState Deserialize(Stream stream, PlayerQuestState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerQuestState DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestState playerQuestState = new PlayerQuestState();
			DeserializeLengthDelimited(stream, playerQuestState);
			return playerQuestState;
		}

		public static PlayerQuestState DeserializeLengthDelimited(Stream stream, PlayerQuestState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerQuestState Deserialize(Stream stream, PlayerQuestState instance, long limit)
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
					instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Status = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PlayerQuestState instance)
		{
			if (instance.HasQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.QuestId);
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
			if (HasQuestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)QuestId);
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
