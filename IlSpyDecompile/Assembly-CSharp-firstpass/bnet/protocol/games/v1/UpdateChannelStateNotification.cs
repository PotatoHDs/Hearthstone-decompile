using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.games.v1
{
	public class UpdateChannelStateNotification : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		public GameHandle GameHandle { get; set; }

		public bnet.protocol.channel.v1.UpdateChannelStateNotification Note { get; set; }

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetNote(bnet.protocol.channel.v1.UpdateChannelStateNotification val)
		{
			Note = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			hashCode ^= Note.GetHashCode();
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			UpdateChannelStateNotification updateChannelStateNotification = obj as UpdateChannelStateNotification;
			if (updateChannelStateNotification == null)
			{
				return false;
			}
			if (!GameHandle.Equals(updateChannelStateNotification.GameHandle))
			{
				return false;
			}
			if (!Note.Equals(updateChannelStateNotification.Note))
			{
				return false;
			}
			if (HasHost != updateChannelStateNotification.HasHost || (HasHost && !Host.Equals(updateChannelStateNotification.Host)))
			{
				return false;
			}
			return true;
		}

		public static UpdateChannelStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateNotification updateChannelStateNotification = new UpdateChannelStateNotification();
			DeserializeLengthDelimited(stream, updateChannelStateNotification);
			return updateChannelStateNotification;
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream, UpdateChannelStateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance, long limit)
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
				case 10:
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					if (instance.Note == null)
					{
						instance.Note = bnet.protocol.channel.v1.UpdateChannelStateNotification.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.UpdateChannelStateNotification.DeserializeLengthDelimited(stream, instance.Note);
					}
					continue;
				case 26:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		public static void Serialize(Stream stream, UpdateChannelStateNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Note == null)
			{
				throw new ArgumentNullException("Note", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Note.GetSerializedSize());
			bnet.protocol.channel.v1.UpdateChannelStateNotification.Serialize(stream, instance.Note);
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = Note.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasHost)
			{
				num++;
				uint serializedSize3 = Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2;
		}
	}
}
