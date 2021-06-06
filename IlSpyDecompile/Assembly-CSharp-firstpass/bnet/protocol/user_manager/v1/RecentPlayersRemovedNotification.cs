using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class RecentPlayersRemovedNotification : IProtoBuf
	{
		private List<RecentPlayer> _Player = new List<RecentPlayer>();

		public List<RecentPlayer> Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
			}
		}

		public List<RecentPlayer> PlayerList => _Player;

		public int PlayerCount => _Player.Count;

		public bool IsInitialized => true;

		public void AddPlayer(RecentPlayer val)
		{
			_Player.Add(val);
		}

		public void ClearPlayer()
		{
			_Player.Clear();
		}

		public void SetPlayer(List<RecentPlayer> val)
		{
			Player = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (RecentPlayer item in Player)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RecentPlayersRemovedNotification recentPlayersRemovedNotification = obj as RecentPlayersRemovedNotification;
			if (recentPlayersRemovedNotification == null)
			{
				return false;
			}
			if (Player.Count != recentPlayersRemovedNotification.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(recentPlayersRemovedNotification.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static RecentPlayersRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RecentPlayersRemovedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RecentPlayersRemovedNotification Deserialize(Stream stream, RecentPlayersRemovedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RecentPlayersRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			RecentPlayersRemovedNotification recentPlayersRemovedNotification = new RecentPlayersRemovedNotification();
			DeserializeLengthDelimited(stream, recentPlayersRemovedNotification);
			return recentPlayersRemovedNotification;
		}

		public static RecentPlayersRemovedNotification DeserializeLengthDelimited(Stream stream, RecentPlayersRemovedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RecentPlayersRemovedNotification Deserialize(Stream stream, RecentPlayersRemovedNotification instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<RecentPlayer>();
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
					instance.Player.Add(RecentPlayer.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RecentPlayersRemovedNotification instance)
		{
			if (instance.Player.Count <= 0)
			{
				return;
			}
			foreach (RecentPlayer item in instance.Player)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				RecentPlayer.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Player.Count > 0)
			{
				foreach (RecentPlayer item in Player)
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
