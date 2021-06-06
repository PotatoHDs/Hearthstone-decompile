using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class CancelPurchase : IProtoBuf
	{
		public enum PacketID
		{
			ID = 274,
			System = 1
		}

		public enum CancelReason
		{
			PROVIDER_REPORTED_FAILURE = 1,
			NOT_RECOGNIZED_BY_PROVIDER,
			USER_CANCELED_BEFORE_PAYMENT,
			USER_CANCELING_TO_UNBLOCK,
			CHALLENGE_TIMEOUT,
			CHALLENGE_DENIED,
			CHALLENGE_OTHER_ERROR,
			LEGACY_PURCHASE_ATTEMPT
		}

		public bool HasReason;

		private CancelReason _Reason;

		public bool HasErrorMessage;

		private string _ErrorMessage;

		public bool IsAutoCancel { get; set; }

		public CancelReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public string DeviceId { get; set; }

		public string ErrorMessage
		{
			get
			{
				return _ErrorMessage;
			}
			set
			{
				_ErrorMessage = value;
				HasErrorMessage = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= IsAutoCancel.GetHashCode();
			if (HasReason)
			{
				hashCode ^= Reason.GetHashCode();
			}
			hashCode ^= DeviceId.GetHashCode();
			if (HasErrorMessage)
			{
				hashCode ^= ErrorMessage.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CancelPurchase cancelPurchase = obj as CancelPurchase;
			if (cancelPurchase == null)
			{
				return false;
			}
			if (!IsAutoCancel.Equals(cancelPurchase.IsAutoCancel))
			{
				return false;
			}
			if (HasReason != cancelPurchase.HasReason || (HasReason && !Reason.Equals(cancelPurchase.Reason)))
			{
				return false;
			}
			if (!DeviceId.Equals(cancelPurchase.DeviceId))
			{
				return false;
			}
			if (HasErrorMessage != cancelPurchase.HasErrorMessage || (HasErrorMessage && !ErrorMessage.Equals(cancelPurchase.ErrorMessage)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelPurchase Deserialize(Stream stream, CancelPurchase instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelPurchase DeserializeLengthDelimited(Stream stream)
		{
			CancelPurchase cancelPurchase = new CancelPurchase();
			DeserializeLengthDelimited(stream, cancelPurchase);
			return cancelPurchase;
		}

		public static CancelPurchase DeserializeLengthDelimited(Stream stream, CancelPurchase instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelPurchase Deserialize(Stream stream, CancelPurchase instance, long limit)
		{
			instance.Reason = CancelReason.PROVIDER_REPORTED_FAILURE;
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
					instance.IsAutoCancel = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.Reason = (CancelReason)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.DeviceId = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.ErrorMessage = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, CancelPurchase instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.IsAutoCancel);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
			}
			if (instance.DeviceId == null)
			{
				throw new ArgumentNullException("DeviceId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			if (instance.HasErrorMessage)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorMessage));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num++;
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(DeviceId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasErrorMessage)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ErrorMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2;
		}
	}
}
