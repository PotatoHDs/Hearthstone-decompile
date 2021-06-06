using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.presence.v1
{
	public class SubscribeNotification : IProtoBuf
	{
		public bool HasSubscriberId;

		private AccountId _SubscriberId;

		private List<PresenceState> _State = new List<PresenceState>();

		public AccountId SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public List<PresenceState> State
		{
			get
			{
				return _State;
			}
			set
			{
				_State = value;
			}
		}

		public List<PresenceState> StateList => _State;

		public int StateCount => _State.Count;

		public bool IsInitialized => true;

		public void SetSubscriberId(AccountId val)
		{
			SubscriberId = val;
		}

		public void AddState(PresenceState val)
		{
			_State.Add(val);
		}

		public void ClearState()
		{
			_State.Clear();
		}

		public void SetState(List<PresenceState> val)
		{
			State = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSubscriberId)
			{
				num ^= SubscriberId.GetHashCode();
			}
			foreach (PresenceState item in State)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeNotification subscribeNotification = obj as SubscribeNotification;
			if (subscribeNotification == null)
			{
				return false;
			}
			if (HasSubscriberId != subscribeNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(subscribeNotification.SubscriberId)))
			{
				return false;
			}
			if (State.Count != subscribeNotification.State.Count)
			{
				return false;
			}
			for (int i = 0; i < State.Count; i++)
			{
				if (!State[i].Equals(subscribeNotification.State[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static SubscribeNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeNotification Deserialize(Stream stream, SubscribeNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeNotification DeserializeLengthDelimited(Stream stream)
		{
			SubscribeNotification subscribeNotification = new SubscribeNotification();
			DeserializeLengthDelimited(stream, subscribeNotification);
			return subscribeNotification;
		}

		public static SubscribeNotification DeserializeLengthDelimited(Stream stream, SubscribeNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeNotification Deserialize(Stream stream, SubscribeNotification instance, long limit)
		{
			if (instance.State == null)
			{
				instance.State = new List<PresenceState>();
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
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
					continue;
				case 18:
					instance.State.Add(PresenceState.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, SubscribeNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.State.Count <= 0)
			{
				return;
			}
			foreach (PresenceState item in instance.State)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PresenceState.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize = SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (State.Count > 0)
			{
				foreach (PresenceState item in State)
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
