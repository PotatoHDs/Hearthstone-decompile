using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class GuestHeroDbRecord : IProtoBuf
	{
		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public int Id { get; set; }

		public int CardId { get; set; }

		public string UnlockEvent { get; set; }

		public List<LocalizedString> Strings
		{
			get
			{
				return _Strings;
			}
			set
			{
				_Strings = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= CardId.GetHashCode();
			hashCode ^= UnlockEvent.GetHashCode();
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GuestHeroDbRecord guestHeroDbRecord = obj as GuestHeroDbRecord;
			if (guestHeroDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(guestHeroDbRecord.Id))
			{
				return false;
			}
			if (!CardId.Equals(guestHeroDbRecord.CardId))
			{
				return false;
			}
			if (!UnlockEvent.Equals(guestHeroDbRecord.UnlockEvent))
			{
				return false;
			}
			if (Strings.Count != guestHeroDbRecord.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < Strings.Count; i++)
			{
				if (!Strings[i].Equals(guestHeroDbRecord.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GuestHeroDbRecord Deserialize(Stream stream, GuestHeroDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GuestHeroDbRecord DeserializeLengthDelimited(Stream stream)
		{
			GuestHeroDbRecord guestHeroDbRecord = new GuestHeroDbRecord();
			DeserializeLengthDelimited(stream, guestHeroDbRecord);
			return guestHeroDbRecord;
		}

		public static GuestHeroDbRecord DeserializeLengthDelimited(Stream stream, GuestHeroDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GuestHeroDbRecord Deserialize(Stream stream, GuestHeroDbRecord instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
				case 8:
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.UnlockEvent = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, GuestHeroDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardId);
			if (instance.UnlockEvent == null)
			{
				throw new ArgumentNullException("UnlockEvent", "Required by proto specification.");
			}
			stream.WriteByte(42);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UnlockEvent));
			if (instance.Strings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString @string in instance.Strings)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, @string.GetSerializedSize());
				LocalizedString.Serialize(stream, @string);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)CardId);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(UnlockEvent);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num += 2;
					uint serializedSize = @string.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 3;
		}
	}
}
