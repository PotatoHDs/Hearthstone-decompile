using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueLeftNotification : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

		public bool HasCancelInitiator;

		private GameAccountHandle _CancelInitiator;

		public RequestId RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = value != null;
			}
		}

		public List<GameAccountHandle> GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
			}
		}

		public List<GameAccountHandle> GameAccountList => _GameAccount;

		public int GameAccountCount => _GameAccount.Count;

		public GameAccountHandle CancelInitiator
		{
			get
			{
				return _CancelInitiator;
			}
			set
			{
				_CancelInitiator = value;
				HasCancelInitiator = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public void AddGameAccount(GameAccountHandle val)
		{
			_GameAccount.Add(val);
		}

		public void ClearGameAccount()
		{
			_GameAccount.Clear();
		}

		public void SetGameAccount(List<GameAccountHandle> val)
		{
			GameAccount = val;
		}

		public void SetCancelInitiator(GameAccountHandle val)
		{
			CancelInitiator = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			foreach (GameAccountHandle item in GameAccount)
			{
				num ^= item.GetHashCode();
			}
			if (HasCancelInitiator)
			{
				num ^= CancelInitiator.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueLeftNotification queueLeftNotification = obj as QueueLeftNotification;
			if (queueLeftNotification == null)
			{
				return false;
			}
			if (HasRequestId != queueLeftNotification.HasRequestId || (HasRequestId && !RequestId.Equals(queueLeftNotification.RequestId)))
			{
				return false;
			}
			if (GameAccount.Count != queueLeftNotification.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < GameAccount.Count; i++)
			{
				if (!GameAccount[i].Equals(queueLeftNotification.GameAccount[i]))
				{
					return false;
				}
			}
			if (HasCancelInitiator != queueLeftNotification.HasCancelInitiator || (HasCancelInitiator && !CancelInitiator.Equals(queueLeftNotification.CancelInitiator)))
			{
				return false;
			}
			return true;
		}

		public static QueueLeftNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueLeftNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueLeftNotification queueLeftNotification = new QueueLeftNotification();
			DeserializeLengthDelimited(stream, queueLeftNotification);
			return queueLeftNotification;
		}

		public static QueueLeftNotification DeserializeLengthDelimited(Stream stream, QueueLeftNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueLeftNotification Deserialize(Stream stream, QueueLeftNotification instance, long limit)
		{
			if (instance.GameAccount == null)
			{
				instance.GameAccount = new List<GameAccountHandle>();
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
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 18:
					instance.GameAccount.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					if (instance.CancelInitiator == null)
					{
						instance.CancelInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.CancelInitiator);
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

		public static void Serialize(Stream stream, QueueLeftNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.GameAccount.Count > 0)
			{
				foreach (GameAccountHandle item in instance.GameAccount)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					GameAccountHandle.Serialize(stream, item);
				}
			}
			if (instance.HasCancelInitiator)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CancelInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.CancelInitiator);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestId)
			{
				num++;
				uint serializedSize = RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (GameAccount.Count > 0)
			{
				foreach (GameAccountHandle item in GameAccount)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasCancelInitiator)
			{
				num++;
				uint serializedSize3 = CancelInitiator.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
