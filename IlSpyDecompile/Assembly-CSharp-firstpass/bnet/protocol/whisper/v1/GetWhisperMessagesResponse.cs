using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.whisper.v1
{
	public class GetWhisperMessagesResponse : IProtoBuf
	{
		private List<Whisper> _Whisper = new List<Whisper>();

		public bool HasContinuation;

		private ulong _Continuation;

		public List<Whisper> Whisper
		{
			get
			{
				return _Whisper;
			}
			set
			{
				_Whisper = value;
			}
		}

		public List<Whisper> WhisperList => _Whisper;

		public int WhisperCount => _Whisper.Count;

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void AddWhisper(Whisper val)
		{
			_Whisper.Add(val);
		}

		public void ClearWhisper()
		{
			_Whisper.Clear();
		}

		public void SetWhisper(List<Whisper> val)
		{
			Whisper = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (Whisper item in Whisper)
			{
				num ^= item.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetWhisperMessagesResponse getWhisperMessagesResponse = obj as GetWhisperMessagesResponse;
			if (getWhisperMessagesResponse == null)
			{
				return false;
			}
			if (Whisper.Count != getWhisperMessagesResponse.Whisper.Count)
			{
				return false;
			}
			for (int i = 0; i < Whisper.Count; i++)
			{
				if (!Whisper[i].Equals(getWhisperMessagesResponse.Whisper[i]))
				{
					return false;
				}
			}
			if (HasContinuation != getWhisperMessagesResponse.HasContinuation || (HasContinuation && !Continuation.Equals(getWhisperMessagesResponse.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetWhisperMessagesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesResponse getWhisperMessagesResponse = new GetWhisperMessagesResponse();
			DeserializeLengthDelimited(stream, getWhisperMessagesResponse);
			return getWhisperMessagesResponse;
		}

		public static GetWhisperMessagesResponse DeserializeLengthDelimited(Stream stream, GetWhisperMessagesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetWhisperMessagesResponse Deserialize(Stream stream, GetWhisperMessagesResponse instance, long limit)
		{
			if (instance.Whisper == null)
			{
				instance.Whisper = new List<Whisper>();
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
					instance.Whisper.Add(bnet.protocol.whisper.v1.Whisper.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetWhisperMessagesResponse instance)
		{
			if (instance.Whisper.Count > 0)
			{
				foreach (Whisper item in instance.Whisper)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.whisper.v1.Whisper.Serialize(stream, item);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Whisper.Count > 0)
			{
				foreach (Whisper item in Whisper)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
