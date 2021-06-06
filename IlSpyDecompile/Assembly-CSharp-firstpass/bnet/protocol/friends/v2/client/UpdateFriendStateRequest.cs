using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class UpdateFriendStateRequest : IProtoBuf
	{
		public bool HasTargetAccountId;

		private ulong _TargetAccountId;

		public bool HasOptions;

		private UpdateFriendStateOptions _Options;

		public ulong TargetAccountId
		{
			get
			{
				return _TargetAccountId;
			}
			set
			{
				_TargetAccountId = value;
				HasTargetAccountId = true;
			}
		}

		public UpdateFriendStateOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTargetAccountId(ulong val)
		{
			TargetAccountId = val;
		}

		public void SetOptions(UpdateFriendStateOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetAccountId)
			{
				num ^= TargetAccountId.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateFriendStateRequest updateFriendStateRequest = obj as UpdateFriendStateRequest;
			if (updateFriendStateRequest == null)
			{
				return false;
			}
			if (HasTargetAccountId != updateFriendStateRequest.HasTargetAccountId || (HasTargetAccountId && !TargetAccountId.Equals(updateFriendStateRequest.TargetAccountId)))
			{
				return false;
			}
			if (HasOptions != updateFriendStateRequest.HasOptions || (HasOptions && !Options.Equals(updateFriendStateRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static UpdateFriendStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateRequest updateFriendStateRequest = new UpdateFriendStateRequest();
			DeserializeLengthDelimited(stream, updateFriendStateRequest);
			return updateFriendStateRequest;
		}

		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream, UpdateFriendStateRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance, long limit)
		{
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
				case 16:
					instance.TargetAccountId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.Options == null)
					{
						instance.Options = UpdateFriendStateOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateFriendStateOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, UpdateFriendStateRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateFriendStateOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(TargetAccountId);
			}
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
