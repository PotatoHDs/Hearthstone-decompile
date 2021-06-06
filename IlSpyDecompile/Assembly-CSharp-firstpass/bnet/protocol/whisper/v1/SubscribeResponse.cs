using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.whisper.v1
{
	public class SubscribeResponse : IProtoBuf
	{
		private List<WhisperView> _View = new List<WhisperView>();

		public List<WhisperView> View
		{
			get
			{
				return _View;
			}
			set
			{
				_View = value;
			}
		}

		public List<WhisperView> ViewList => _View;

		public int ViewCount => _View.Count;

		public bool IsInitialized => true;

		public void AddView(WhisperView val)
		{
			_View.Add(val);
		}

		public void ClearView()
		{
			_View.Clear();
		}

		public void SetView(List<WhisperView> val)
		{
			View = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (WhisperView item in View)
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
			if (View.Count != subscribeResponse.View.Count)
			{
				return false;
			}
			for (int i = 0; i < View.Count; i++)
			{
				if (!View[i].Equals(subscribeResponse.View[i]))
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
			if (instance.View == null)
			{
				instance.View = new List<WhisperView>();
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
					instance.View.Add(WhisperView.DeserializeLengthDelimited(stream));
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
			if (instance.View.Count <= 0)
			{
				return;
			}
			foreach (WhisperView item in instance.View)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				WhisperView.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (View.Count > 0)
			{
				foreach (WhisperView item in View)
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
