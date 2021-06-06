using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class QueueLeftNotification : IProtoBuf
	{
		public bool HasRequestId;

		private FindGameRequestId _RequestId;

		public bool HasResult;

		private uint _Result;

		private List<GameAccountHandle> _Quitter = new List<GameAccountHandle>();

		public FindGameRequestId RequestId
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

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public List<GameAccountHandle> Quitter
		{
			get
			{
				return _Quitter;
			}
			set
			{
				_Quitter = value;
			}
		}

		public List<GameAccountHandle> QuitterList => _Quitter;

		public int QuitterCount => _Quitter.Count;

		public bool IsInitialized => true;

		public void SetRequestId(FindGameRequestId val)
		{
			RequestId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public void AddQuitter(GameAccountHandle val)
		{
			_Quitter.Add(val);
		}

		public void ClearQuitter()
		{
			_Quitter.Clear();
		}

		public void SetQuitter(List<GameAccountHandle> val)
		{
			Quitter = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			foreach (GameAccountHandle item in Quitter)
			{
				num ^= item.GetHashCode();
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
			if (HasResult != queueLeftNotification.HasResult || (HasResult && !Result.Equals(queueLeftNotification.Result)))
			{
				return false;
			}
			if (Quitter.Count != queueLeftNotification.Quitter.Count)
			{
				return false;
			}
			for (int i = 0; i < Quitter.Count; i++)
			{
				if (!Quitter[i].Equals(queueLeftNotification.Quitter[i]))
				{
					return false;
				}
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
			if (instance.Quitter == null)
			{
				instance.Quitter = new List<GameAccountHandle>();
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
						instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 16:
					instance.Result = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.Quitter.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
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
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
			if (instance.Quitter.Count <= 0)
			{
				return;
			}
			foreach (GameAccountHandle item in instance.Quitter)
			{
				stream.WriteByte(26);
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
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			if (Quitter.Count > 0)
			{
				foreach (GameAccountHandle item in Quitter)
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
