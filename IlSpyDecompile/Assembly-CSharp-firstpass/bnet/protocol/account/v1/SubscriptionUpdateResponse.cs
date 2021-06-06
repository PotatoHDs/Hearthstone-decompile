using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	public class SubscriptionUpdateResponse : IProtoBuf
	{
		private List<SubscriberReference> _Ref = new List<SubscriberReference>();

		public List<SubscriberReference> Ref
		{
			get
			{
				return _Ref;
			}
			set
			{
				_Ref = value;
			}
		}

		public List<SubscriberReference> RefList => _Ref;

		public int RefCount => _Ref.Count;

		public bool IsInitialized => true;

		public void AddRef(SubscriberReference val)
		{
			_Ref.Add(val);
		}

		public void ClearRef()
		{
			_Ref.Clear();
		}

		public void SetRef(List<SubscriberReference> val)
		{
			Ref = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (SubscriberReference item in Ref)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscriptionUpdateResponse subscriptionUpdateResponse = obj as SubscriptionUpdateResponse;
			if (subscriptionUpdateResponse == null)
			{
				return false;
			}
			if (Ref.Count != subscriptionUpdateResponse.Ref.Count)
			{
				return false;
			}
			for (int i = 0; i < Ref.Count; i++)
			{
				if (!Ref[i].Equals(subscriptionUpdateResponse.Ref[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static SubscriptionUpdateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriptionUpdateResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscriptionUpdateResponse Deserialize(Stream stream, SubscriptionUpdateResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscriptionUpdateResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscriptionUpdateResponse subscriptionUpdateResponse = new SubscriptionUpdateResponse();
			DeserializeLengthDelimited(stream, subscriptionUpdateResponse);
			return subscriptionUpdateResponse;
		}

		public static SubscriptionUpdateResponse DeserializeLengthDelimited(Stream stream, SubscriptionUpdateResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscriptionUpdateResponse Deserialize(Stream stream, SubscriptionUpdateResponse instance, long limit)
		{
			if (instance.Ref == null)
			{
				instance.Ref = new List<SubscriberReference>();
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
					instance.Ref.Add(SubscriberReference.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, SubscriptionUpdateResponse instance)
		{
			if (instance.Ref.Count <= 0)
			{
				return;
			}
			foreach (SubscriberReference item in instance.Ref)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				SubscriberReference.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Ref.Count > 0)
			{
				foreach (SubscriberReference item in Ref)
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
