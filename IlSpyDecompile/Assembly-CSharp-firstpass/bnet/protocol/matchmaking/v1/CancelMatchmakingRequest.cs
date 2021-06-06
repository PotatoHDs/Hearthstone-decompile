using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	public class CancelMatchmakingRequest : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		private List<GameAccountHandle> _GameAccount = new List<GameAccountHandle>();

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
			return num;
		}

		public override bool Equals(object obj)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = obj as CancelMatchmakingRequest;
			if (cancelMatchmakingRequest == null)
			{
				return false;
			}
			if (HasRequestId != cancelMatchmakingRequest.HasRequestId || (HasRequestId && !RequestId.Equals(cancelMatchmakingRequest.RequestId)))
			{
				return false;
			}
			if (GameAccount.Count != cancelMatchmakingRequest.GameAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < GameAccount.Count; i++)
			{
				if (!GameAccount[i].Equals(cancelMatchmakingRequest.GameAccount[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static CancelMatchmakingRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelMatchmakingRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelMatchmakingRequest Deserialize(Stream stream, CancelMatchmakingRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelMatchmakingRequest DeserializeLengthDelimited(Stream stream)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = new CancelMatchmakingRequest();
			DeserializeLengthDelimited(stream, cancelMatchmakingRequest);
			return cancelMatchmakingRequest;
		}

		public static CancelMatchmakingRequest DeserializeLengthDelimited(Stream stream, CancelMatchmakingRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelMatchmakingRequest Deserialize(Stream stream, CancelMatchmakingRequest instance, long limit)
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

		public static void Serialize(Stream stream, CancelMatchmakingRequest instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.GameAccount.Count <= 0)
			{
				return;
			}
			foreach (GameAccountHandle item in instance.GameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				GameAccountHandle.Serialize(stream, item);
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
				return num;
			}
			return num;
		}
	}
}
