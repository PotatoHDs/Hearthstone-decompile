using System.IO;

namespace PegasusUtil
{
	public class SubscribeResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 315
		}

		public enum ResponseResult
		{
			SUCCESS = 1,
			FAILED_UNAVAILABLE
		}

		public bool HasRoute;

		private ulong _Route;

		public bool HasKeepAliveSecs;

		private ulong _KeepAliveSecs;

		public bool HasMaxResubscribeAttempts;

		private ulong _MaxResubscribeAttempts;

		public bool HasPendingResponseTimeout;

		private ulong _PendingResponseTimeout;

		public bool HasPendingSubscribeTimeout;

		private ulong _PendingSubscribeTimeout;

		public bool HasResult;

		private ResponseResult _Result;

		public bool HasRequestMaxWaitSecs;

		private ulong _RequestMaxWaitSecs;

		public ulong Route
		{
			get
			{
				return _Route;
			}
			set
			{
				_Route = value;
				HasRoute = true;
			}
		}

		public ulong KeepAliveSecs
		{
			get
			{
				return _KeepAliveSecs;
			}
			set
			{
				_KeepAliveSecs = value;
				HasKeepAliveSecs = true;
			}
		}

		public ulong MaxResubscribeAttempts
		{
			get
			{
				return _MaxResubscribeAttempts;
			}
			set
			{
				_MaxResubscribeAttempts = value;
				HasMaxResubscribeAttempts = true;
			}
		}

		public ulong PendingResponseTimeout
		{
			get
			{
				return _PendingResponseTimeout;
			}
			set
			{
				_PendingResponseTimeout = value;
				HasPendingResponseTimeout = true;
			}
		}

		public ulong PendingSubscribeTimeout
		{
			get
			{
				return _PendingSubscribeTimeout;
			}
			set
			{
				_PendingSubscribeTimeout = value;
				HasPendingSubscribeTimeout = true;
			}
		}

		public ResponseResult Result
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

		public ulong RequestMaxWaitSecs
		{
			get
			{
				return _RequestMaxWaitSecs;
			}
			set
			{
				_RequestMaxWaitSecs = value;
				HasRequestMaxWaitSecs = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRoute)
			{
				num ^= Route.GetHashCode();
			}
			if (HasKeepAliveSecs)
			{
				num ^= KeepAliveSecs.GetHashCode();
			}
			if (HasMaxResubscribeAttempts)
			{
				num ^= MaxResubscribeAttempts.GetHashCode();
			}
			if (HasPendingResponseTimeout)
			{
				num ^= PendingResponseTimeout.GetHashCode();
			}
			if (HasPendingSubscribeTimeout)
			{
				num ^= PendingSubscribeTimeout.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			if (HasRequestMaxWaitSecs)
			{
				num ^= RequestMaxWaitSecs.GetHashCode();
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
			if (HasRoute != subscribeResponse.HasRoute || (HasRoute && !Route.Equals(subscribeResponse.Route)))
			{
				return false;
			}
			if (HasKeepAliveSecs != subscribeResponse.HasKeepAliveSecs || (HasKeepAliveSecs && !KeepAliveSecs.Equals(subscribeResponse.KeepAliveSecs)))
			{
				return false;
			}
			if (HasMaxResubscribeAttempts != subscribeResponse.HasMaxResubscribeAttempts || (HasMaxResubscribeAttempts && !MaxResubscribeAttempts.Equals(subscribeResponse.MaxResubscribeAttempts)))
			{
				return false;
			}
			if (HasPendingResponseTimeout != subscribeResponse.HasPendingResponseTimeout || (HasPendingResponseTimeout && !PendingResponseTimeout.Equals(subscribeResponse.PendingResponseTimeout)))
			{
				return false;
			}
			if (HasPendingSubscribeTimeout != subscribeResponse.HasPendingSubscribeTimeout || (HasPendingSubscribeTimeout && !PendingSubscribeTimeout.Equals(subscribeResponse.PendingSubscribeTimeout)))
			{
				return false;
			}
			if (HasResult != subscribeResponse.HasResult || (HasResult && !Result.Equals(subscribeResponse.Result)))
			{
				return false;
			}
			if (HasRequestMaxWaitSecs != subscribeResponse.HasRequestMaxWaitSecs || (HasRequestMaxWaitSecs && !RequestMaxWaitSecs.Equals(subscribeResponse.RequestMaxWaitSecs)))
			{
				return false;
			}
			return true;
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
			instance.MaxResubscribeAttempts = 0uL;
			instance.PendingResponseTimeout = 0uL;
			instance.PendingSubscribeTimeout = 0uL;
			instance.Result = ResponseResult.SUCCESS;
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
				case 8:
					instance.Route = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.KeepAliveSecs = ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.MaxResubscribeAttempts = ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.PendingResponseTimeout = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.PendingSubscribeTimeout = ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.Result = (ResponseResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.RequestMaxWaitSecs = ProtocolParser.ReadUInt64(stream);
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
			if (instance.HasRoute)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Route);
			}
			if (instance.HasKeepAliveSecs)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.KeepAliveSecs);
			}
			if (instance.HasMaxResubscribeAttempts)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.MaxResubscribeAttempts);
			}
			if (instance.HasPendingResponseTimeout)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.PendingResponseTimeout);
			}
			if (instance.HasPendingSubscribeTimeout)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.PendingSubscribeTimeout);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Result);
			}
			if (instance.HasRequestMaxWaitSecs)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.RequestMaxWaitSecs);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRoute)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Route);
			}
			if (HasKeepAliveSecs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(KeepAliveSecs);
			}
			if (HasMaxResubscribeAttempts)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(MaxResubscribeAttempts);
			}
			if (HasPendingResponseTimeout)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(PendingResponseTimeout);
			}
			if (HasPendingSubscribeTimeout)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(PendingSubscribeTimeout);
			}
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Result);
			}
			if (HasRequestMaxWaitSecs)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(RequestMaxWaitSecs);
			}
			return num;
		}
	}
}
