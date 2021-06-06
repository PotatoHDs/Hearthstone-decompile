using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	public class SubscribeResponse : IProtoBuf
	{
		private List<BlockedPlayer> _BlockedPlayers = new List<BlockedPlayer>();

		private List<RecentPlayer> _RecentPlayers = new List<RecentPlayer>();

		private List<Role> _Role = new List<Role>();

		public List<BlockedPlayer> BlockedPlayers
		{
			get
			{
				return _BlockedPlayers;
			}
			set
			{
				_BlockedPlayers = value;
			}
		}

		public List<BlockedPlayer> BlockedPlayersList => _BlockedPlayers;

		public int BlockedPlayersCount => _BlockedPlayers.Count;

		public List<RecentPlayer> RecentPlayers
		{
			get
			{
				return _RecentPlayers;
			}
			set
			{
				_RecentPlayers = value;
			}
		}

		public List<RecentPlayer> RecentPlayersList => _RecentPlayers;

		public int RecentPlayersCount => _RecentPlayers.Count;

		public List<Role> Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
			}
		}

		public List<Role> RoleList => _Role;

		public int RoleCount => _Role.Count;

		public bool IsInitialized => true;

		public void AddBlockedPlayers(BlockedPlayer val)
		{
			_BlockedPlayers.Add(val);
		}

		public void ClearBlockedPlayers()
		{
			_BlockedPlayers.Clear();
		}

		public void SetBlockedPlayers(List<BlockedPlayer> val)
		{
			BlockedPlayers = val;
		}

		public void AddRecentPlayers(RecentPlayer val)
		{
			_RecentPlayers.Add(val);
		}

		public void ClearRecentPlayers()
		{
			_RecentPlayers.Clear();
		}

		public void SetRecentPlayers(List<RecentPlayer> val)
		{
			RecentPlayers = val;
		}

		public void AddRole(Role val)
		{
			_Role.Add(val);
		}

		public void ClearRole()
		{
			_Role.Clear();
		}

		public void SetRole(List<Role> val)
		{
			Role = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (BlockedPlayer blockedPlayer in BlockedPlayers)
			{
				num ^= blockedPlayer.GetHashCode();
			}
			foreach (RecentPlayer recentPlayer in RecentPlayers)
			{
				num ^= recentPlayer.GetHashCode();
			}
			foreach (Role item in Role)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (BlockedPlayers.Count != subscribeResponse.BlockedPlayers.Count)
			{
				return false;
			}
			for (int i = 0; i < BlockedPlayers.Count; i++)
			{
				if (!BlockedPlayers[i].Equals(subscribeResponse.BlockedPlayers[i]))
				{
					return false;
				}
			}
			if (RecentPlayers.Count != subscribeResponse.RecentPlayers.Count)
			{
				return false;
			}
			for (int j = 0; j < RecentPlayers.Count; j++)
			{
				if (!RecentPlayers[j].Equals(subscribeResponse.RecentPlayers[j]))
				{
					return false;
				}
			}
			if (Role.Count != subscribeResponse.Role.Count)
			{
				return false;
			}
			for (int k = 0; k < Role.Count; k++)
			{
				if (!Role[k].Equals(subscribeResponse.Role[k]))
				{
					return false;
				}
			}
			return true;
		}

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.BlockedPlayers == null)
			{
				instance.BlockedPlayers = new List<BlockedPlayer>();
			}
			if (instance.RecentPlayers == null)
			{
				instance.RecentPlayers = new List<RecentPlayer>();
			}
			if (instance.Role == null)
			{
				instance.Role = new List<Role>();
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
					instance.BlockedPlayers.Add(BlockedPlayer.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.RecentPlayers.Add(RecentPlayer.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.Role.Add(bnet.protocol.Role.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.BlockedPlayers.Count > 0)
			{
				foreach (BlockedPlayer blockedPlayer in instance.BlockedPlayers)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, blockedPlayer.GetSerializedSize());
					BlockedPlayer.Serialize(stream, blockedPlayer);
				}
			}
			if (instance.RecentPlayers.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in instance.RecentPlayers)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, recentPlayer.GetSerializedSize());
					RecentPlayer.Serialize(stream, recentPlayer);
				}
			}
			if (instance.Role.Count <= 0)
			{
				return;
			}
			foreach (Role item in instance.Role)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Role.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (BlockedPlayers.Count > 0)
			{
				foreach (BlockedPlayer blockedPlayer in BlockedPlayers)
				{
					num++;
					uint serializedSize = blockedPlayer.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (RecentPlayers.Count > 0)
			{
				foreach (RecentPlayer recentPlayer in RecentPlayers)
				{
					num++;
					uint serializedSize2 = recentPlayer.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Role.Count > 0)
			{
				foreach (Role item in Role)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
