using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class BatchSubscribeResponse : IProtoBuf
	{
		private List<SubscribeResult> _SubscribeFailed = new List<SubscribeResult>();

		public List<SubscribeResult> SubscribeFailed
		{
			get
			{
				return _SubscribeFailed;
			}
			set
			{
				_SubscribeFailed = value;
			}
		}

		public List<SubscribeResult> SubscribeFailedList => _SubscribeFailed;

		public int SubscribeFailedCount => _SubscribeFailed.Count;

		public bool IsInitialized => true;

		public void AddSubscribeFailed(SubscribeResult val)
		{
			_SubscribeFailed.Add(val);
		}

		public void ClearSubscribeFailed()
		{
			_SubscribeFailed.Clear();
		}

		public void SetSubscribeFailed(List<SubscribeResult> val)
		{
			SubscribeFailed = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (SubscribeResult item in SubscribeFailed)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BatchSubscribeResponse batchSubscribeResponse = obj as BatchSubscribeResponse;
			if (batchSubscribeResponse == null)
			{
				return false;
			}
			if (SubscribeFailed.Count != batchSubscribeResponse.SubscribeFailed.Count)
			{
				return false;
			}
			for (int i = 0; i < SubscribeFailed.Count; i++)
			{
				if (!SubscribeFailed[i].Equals(batchSubscribeResponse.SubscribeFailed[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static BatchSubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchSubscribeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BatchSubscribeResponse Deserialize(Stream stream, BatchSubscribeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BatchSubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			BatchSubscribeResponse batchSubscribeResponse = new BatchSubscribeResponse();
			DeserializeLengthDelimited(stream, batchSubscribeResponse);
			return batchSubscribeResponse;
		}

		public static BatchSubscribeResponse DeserializeLengthDelimited(Stream stream, BatchSubscribeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BatchSubscribeResponse Deserialize(Stream stream, BatchSubscribeResponse instance, long limit)
		{
			if (instance.SubscribeFailed == null)
			{
				instance.SubscribeFailed = new List<SubscribeResult>();
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
					instance.SubscribeFailed.Add(SubscribeResult.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, BatchSubscribeResponse instance)
		{
			if (instance.SubscribeFailed.Count <= 0)
			{
				return;
			}
			foreach (SubscribeResult item in instance.SubscribeFailed)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				SubscribeResult.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (SubscribeFailed.Count > 0)
			{
				foreach (SubscribeResult item in SubscribeFailed)
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
