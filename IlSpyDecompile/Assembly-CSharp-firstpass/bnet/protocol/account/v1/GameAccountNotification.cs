using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameAccountNotification : IProtoBuf
	{
		private List<GameAccountList> _GameAccounts = new List<GameAccountList>();

		public bool HasSubscriberId;

		private ulong _SubscriberId;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

		public List<GameAccountList> GameAccounts
		{
			get
			{
				return _GameAccounts;
			}
			set
			{
				_GameAccounts = value;
			}
		}

		public List<GameAccountList> GameAccountsList => _GameAccounts;

		public int GameAccountsCount => _GameAccounts.Count;

		public ulong SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = true;
			}
		}

		public AccountFieldTags AccountTags
		{
			get
			{
				return _AccountTags;
			}
			set
			{
				_AccountTags = value;
				HasAccountTags = value != null;
			}
		}

		public bool IsInitialized => true;

		public void AddGameAccounts(GameAccountList val)
		{
			_GameAccounts.Add(val);
		}

		public void ClearGameAccounts()
		{
			_GameAccounts.Clear();
		}

		public void SetGameAccounts(List<GameAccountList> val)
		{
			GameAccounts = val;
		}

		public void SetSubscriberId(ulong val)
		{
			SubscriberId = val;
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			AccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (GameAccountList gameAccount in GameAccounts)
			{
				num ^= gameAccount.GetHashCode();
			}
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			if (HasAccountTags)
			{
				num ^= AccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountNotification gameAccountNotification = obj as GameAccountNotification;
			if (gameAccountNotification == null)
			{
				return false;
			}
			if (GameAccounts.Count != gameAccountNotification.GameAccounts.Count)
			{
				return false;
			}
			for (int i = 0; i < GameAccounts.Count; i++)
			{
				if (!GameAccounts[i].Equals(gameAccountNotification.GameAccounts[i]))
				{
					return false;
				}
			}
			if (HasSubscriberId != gameAccountNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(gameAccountNotification.SubscriberId)))
			{
				return false;
			}
			if (HasAccountTags != gameAccountNotification.HasAccountTags || (HasAccountTags && !AccountTags.Equals(gameAccountNotification.AccountTags)))
			{
				return false;
			}
			return true;
		}

		public static GameAccountNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameAccountNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountNotification gameAccountNotification = new GameAccountNotification();
			DeserializeLengthDelimited(stream, gameAccountNotification);
			return gameAccountNotification;
		}

		public static GameAccountNotification DeserializeLengthDelimited(Stream stream, GameAccountNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameAccountNotification Deserialize(Stream stream, GameAccountNotification instance, long limit)
		{
			if (instance.GameAccounts == null)
			{
				instance.GameAccounts = new List<GameAccountList>();
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
					instance.GameAccounts.Add(GameAccountList.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.AccountTags == null)
					{
						instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
					}
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

		public static void Serialize(Stream stream, GameAccountNotification instance)
		{
			if (instance.GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccount in instance.GameAccounts)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameAccount.GetSerializedSize());
					GameAccountList.Serialize(stream, gameAccount);
				}
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (GameAccounts.Count > 0)
			{
				foreach (GameAccountList gameAccount in GameAccounts)
				{
					num++;
					uint serializedSize = gameAccount.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasSubscriberId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SubscriberId);
			}
			if (HasAccountTags)
			{
				num++;
				uint serializedSize2 = AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
