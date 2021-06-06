using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class RewardTrackUnclaimedNotification : IProtoBuf
	{
		public enum PacketID
		{
			ID = 619,
			System = 0
		}

		private List<RewardTrackUnclaimedRewards> _Notif = new List<RewardTrackUnclaimedRewards>();

		public List<RewardTrackUnclaimedRewards> Notif
		{
			get
			{
				return _Notif;
			}
			set
			{
				_Notif = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (RewardTrackUnclaimedRewards item in Notif)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = obj as RewardTrackUnclaimedNotification;
			if (rewardTrackUnclaimedNotification == null)
			{
				return false;
			}
			if (Notif.Count != rewardTrackUnclaimedNotification.Notif.Count)
			{
				return false;
			}
			for (int i = 0; i < Notif.Count; i++)
			{
				if (!Notif[i].Equals(rewardTrackUnclaimedNotification.Notif[i]))
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

		public static RewardTrackUnclaimedNotification Deserialize(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardTrackUnclaimedNotification DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = new RewardTrackUnclaimedNotification();
			DeserializeLengthDelimited(stream, rewardTrackUnclaimedNotification);
			return rewardTrackUnclaimedNotification;
		}

		public static RewardTrackUnclaimedNotification DeserializeLengthDelimited(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardTrackUnclaimedNotification Deserialize(Stream stream, RewardTrackUnclaimedNotification instance, long limit)
		{
			if (instance.Notif == null)
			{
				instance.Notif = new List<RewardTrackUnclaimedRewards>();
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
					instance.Notif.Add(RewardTrackUnclaimedRewards.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RewardTrackUnclaimedNotification instance)
		{
			if (instance.Notif.Count <= 0)
			{
				return;
			}
			foreach (RewardTrackUnclaimedRewards item in instance.Notif)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RewardTrackUnclaimedRewards.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Notif.Count > 0)
			{
				foreach (RewardTrackUnclaimedRewards item in Notif)
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
