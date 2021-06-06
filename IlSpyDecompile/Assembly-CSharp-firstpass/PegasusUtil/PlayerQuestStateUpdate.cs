using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class PlayerQuestStateUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 601,
			System = 0
		}

		private List<PlayerQuestState> _Quest = new List<PlayerQuestState>();

		private List<int> _ShowQuestNotificationForPoolType = new List<int>();

		private List<long> _GrantDate = new List<long>();

		public bool HasDeprecatedField2;

		private bool _DeprecatedField2;

		public List<PlayerQuestState> Quest
		{
			get
			{
				return _Quest;
			}
			set
			{
				_Quest = value;
			}
		}

		public List<int> ShowQuestNotificationForPoolType
		{
			get
			{
				return _ShowQuestNotificationForPoolType;
			}
			set
			{
				_ShowQuestNotificationForPoolType = value;
			}
		}

		public List<long> GrantDate
		{
			get
			{
				return _GrantDate;
			}
			set
			{
				_GrantDate = value;
			}
		}

		public bool DeprecatedField2
		{
			get
			{
				return _DeprecatedField2;
			}
			set
			{
				_DeprecatedField2 = value;
				HasDeprecatedField2 = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerQuestState item in Quest)
			{
				num ^= item.GetHashCode();
			}
			foreach (int item2 in ShowQuestNotificationForPoolType)
			{
				num ^= item2.GetHashCode();
			}
			foreach (long item3 in GrantDate)
			{
				num ^= item3.GetHashCode();
			}
			if (HasDeprecatedField2)
			{
				num ^= DeprecatedField2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = obj as PlayerQuestStateUpdate;
			if (playerQuestStateUpdate == null)
			{
				return false;
			}
			if (Quest.Count != playerQuestStateUpdate.Quest.Count)
			{
				return false;
			}
			for (int i = 0; i < Quest.Count; i++)
			{
				if (!Quest[i].Equals(playerQuestStateUpdate.Quest[i]))
				{
					return false;
				}
			}
			if (ShowQuestNotificationForPoolType.Count != playerQuestStateUpdate.ShowQuestNotificationForPoolType.Count)
			{
				return false;
			}
			for (int j = 0; j < ShowQuestNotificationForPoolType.Count; j++)
			{
				if (!ShowQuestNotificationForPoolType[j].Equals(playerQuestStateUpdate.ShowQuestNotificationForPoolType[j]))
				{
					return false;
				}
			}
			if (GrantDate.Count != playerQuestStateUpdate.GrantDate.Count)
			{
				return false;
			}
			for (int k = 0; k < GrantDate.Count; k++)
			{
				if (!GrantDate[k].Equals(playerQuestStateUpdate.GrantDate[k]))
				{
					return false;
				}
			}
			if (HasDeprecatedField2 != playerQuestStateUpdate.HasDeprecatedField2 || (HasDeprecatedField2 && !DeprecatedField2.Equals(playerQuestStateUpdate.DeprecatedField2)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerQuestStateUpdate Deserialize(Stream stream, PlayerQuestStateUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerQuestStateUpdate DeserializeLengthDelimited(Stream stream)
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = new PlayerQuestStateUpdate();
			DeserializeLengthDelimited(stream, playerQuestStateUpdate);
			return playerQuestStateUpdate;
		}

		public static PlayerQuestStateUpdate DeserializeLengthDelimited(Stream stream, PlayerQuestStateUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerQuestStateUpdate Deserialize(Stream stream, PlayerQuestStateUpdate instance, long limit)
		{
			if (instance.Quest == null)
			{
				instance.Quest = new List<PlayerQuestState>();
			}
			if (instance.ShowQuestNotificationForPoolType == null)
			{
				instance.ShowQuestNotificationForPoolType = new List<int>();
			}
			if (instance.GrantDate == null)
			{
				instance.GrantDate = new List<long>();
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
					instance.Quest.Add(PlayerQuestState.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.ShowQuestNotificationForPoolType.Add((int)ProtocolParser.ReadUInt64(stream));
					continue;
				case 32:
					instance.GrantDate.Add((long)ProtocolParser.ReadUInt64(stream));
					continue;
				case 16:
					instance.DeprecatedField2 = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PlayerQuestStateUpdate instance)
		{
			if (instance.Quest.Count > 0)
			{
				foreach (PlayerQuestState item in instance.Quest)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					PlayerQuestState.Serialize(stream, item);
				}
			}
			if (instance.ShowQuestNotificationForPoolType.Count > 0)
			{
				foreach (int item2 in instance.ShowQuestNotificationForPoolType)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)item2);
				}
			}
			if (instance.GrantDate.Count > 0)
			{
				foreach (long item3 in instance.GrantDate)
				{
					stream.WriteByte(32);
					ProtocolParser.WriteUInt64(stream, (ulong)item3);
				}
			}
			if (instance.HasDeprecatedField2)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.DeprecatedField2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Quest.Count > 0)
			{
				foreach (PlayerQuestState item in Quest)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (ShowQuestNotificationForPoolType.Count > 0)
			{
				foreach (int item2 in ShowQuestNotificationForPoolType)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item2);
				}
			}
			if (GrantDate.Count > 0)
			{
				foreach (long item3 in GrantDate)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)item3);
				}
			}
			if (HasDeprecatedField2)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
