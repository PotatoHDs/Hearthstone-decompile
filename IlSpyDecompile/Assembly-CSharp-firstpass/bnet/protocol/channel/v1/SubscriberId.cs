using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class SubscriberId : IProtoBuf
	{
		public bool HasAccount;

		private AccountId _Account;

		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasProcess;

		private ProcessId _Process;

		public AccountId Account
		{
			get
			{
				return _Account;
			}
			set
			{
				_Account = value;
				HasAccount = value != null;
			}
		}

		public GameAccountHandle GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public ProcessId Process
		{
			get
			{
				return _Process;
			}
			set
			{
				_Process = value;
				HasProcess = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccount(AccountId val)
		{
			Account = val;
		}

		public void SetGameAccount(GameAccountHandle val)
		{
			GameAccount = val;
		}

		public void SetProcess(ProcessId val)
		{
			Process = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccount)
			{
				num ^= Account.GetHashCode();
			}
			if (HasGameAccount)
			{
				num ^= GameAccount.GetHashCode();
			}
			if (HasProcess)
			{
				num ^= Process.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscriberId subscriberId = obj as SubscriberId;
			if (subscriberId == null)
			{
				return false;
			}
			if (HasAccount != subscriberId.HasAccount || (HasAccount && !Account.Equals(subscriberId.Account)))
			{
				return false;
			}
			if (HasGameAccount != subscriberId.HasGameAccount || (HasGameAccount && !GameAccount.Equals(subscriberId.GameAccount)))
			{
				return false;
			}
			if (HasProcess != subscriberId.HasProcess || (HasProcess && !Process.Equals(subscriberId.Process)))
			{
				return false;
			}
			return true;
		}

		public static SubscriberId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriberId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscriberId Deserialize(Stream stream, SubscriberId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscriberId DeserializeLengthDelimited(Stream stream)
		{
			SubscriberId subscriberId = new SubscriberId();
			DeserializeLengthDelimited(stream, subscriberId);
			return subscriberId;
		}

		public static SubscriberId DeserializeLengthDelimited(Stream stream, SubscriberId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscriberId Deserialize(Stream stream, SubscriberId instance, long limit)
		{
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
					if (instance.Account == null)
					{
						instance.Account = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.Account);
					}
					continue;
				case 18:
					if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 26:
					if (instance.Process == null)
					{
						instance.Process = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Process);
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

		public static void Serialize(Stream stream, SubscriberId instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasProcess)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Process.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Process);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccount)
			{
				num++;
				uint serializedSize = Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasGameAccount)
			{
				num++;
				uint serializedSize2 = GameAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasProcess)
			{
				num++;
				uint serializedSize3 = Process.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
