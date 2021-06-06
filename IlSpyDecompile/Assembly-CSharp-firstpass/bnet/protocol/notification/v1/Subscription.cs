using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.notification.v1
{
	public class Subscription : IProtoBuf
	{
		private List<Target> _Target = new List<Target>();

		public bool HasSubscriber;

		private bnet.protocol.account.v1.Identity _Subscriber;

		public bool HasDeliveryRequired;

		private bool _DeliveryRequired;

		public List<Target> Target
		{
			get
			{
				return _Target;
			}
			set
			{
				_Target = value;
			}
		}

		public List<Target> TargetList => _Target;

		public int TargetCount => _Target.Count;

		public bnet.protocol.account.v1.Identity Subscriber
		{
			get
			{
				return _Subscriber;
			}
			set
			{
				_Subscriber = value;
				HasSubscriber = value != null;
			}
		}

		public bool DeliveryRequired
		{
			get
			{
				return _DeliveryRequired;
			}
			set
			{
				_DeliveryRequired = value;
				HasDeliveryRequired = true;
			}
		}

		public bool IsInitialized => true;

		public void AddTarget(Target val)
		{
			_Target.Add(val);
		}

		public void ClearTarget()
		{
			_Target.Clear();
		}

		public void SetTarget(List<Target> val)
		{
			Target = val;
		}

		public void SetSubscriber(bnet.protocol.account.v1.Identity val)
		{
			Subscriber = val;
		}

		public void SetDeliveryRequired(bool val)
		{
			DeliveryRequired = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Target item in Target)
			{
				num ^= item.GetHashCode();
			}
			if (HasSubscriber)
			{
				num ^= Subscriber.GetHashCode();
			}
			if (HasDeliveryRequired)
			{
				num ^= DeliveryRequired.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Subscription subscription = obj as Subscription;
			if (subscription == null)
			{
				return false;
			}
			if (Target.Count != subscription.Target.Count)
			{
				return false;
			}
			for (int i = 0; i < Target.Count; i++)
			{
				if (!Target[i].Equals(subscription.Target[i]))
				{
					return false;
				}
			}
			if (HasSubscriber != subscription.HasSubscriber || (HasSubscriber && !Subscriber.Equals(subscription.Subscriber)))
			{
				return false;
			}
			if (HasDeliveryRequired != subscription.HasDeliveryRequired || (HasDeliveryRequired && !DeliveryRequired.Equals(subscription.DeliveryRequired)))
			{
				return false;
			}
			return true;
		}

		public static Subscription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Subscription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Subscription Deserialize(Stream stream, Subscription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Subscription DeserializeLengthDelimited(Stream stream)
		{
			Subscription subscription = new Subscription();
			DeserializeLengthDelimited(stream, subscription);
			return subscription;
		}

		public static Subscription DeserializeLengthDelimited(Stream stream, Subscription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Subscription Deserialize(Stream stream, Subscription instance, long limit)
		{
			if (instance.Target == null)
			{
				instance.Target = new List<Target>();
			}
			instance.DeliveryRequired = false;
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
					instance.Target.Add(bnet.protocol.notification.v1.Target.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					if (instance.Subscriber == null)
					{
						instance.Subscriber = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Subscriber);
					}
					continue;
				case 24:
					instance.DeliveryRequired = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, Subscription instance)
		{
			if (instance.Target.Count > 0)
			{
				foreach (Target item in instance.Target)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.notification.v1.Target.Serialize(stream, item);
				}
			}
			if (instance.HasSubscriber)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Subscriber.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Subscriber);
			}
			if (instance.HasDeliveryRequired)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.DeliveryRequired);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Target.Count > 0)
			{
				foreach (Target item in Target)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasSubscriber)
			{
				num++;
				uint serializedSize2 = Subscriber.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasDeliveryRequired)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
