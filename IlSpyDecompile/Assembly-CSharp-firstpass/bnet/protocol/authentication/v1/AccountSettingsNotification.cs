using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	public class AccountSettingsNotification : IProtoBuf
	{
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasIsUsingRid;

		private bool _IsUsingRid;

		public bool HasIsPlayingFromIgr;

		private bool _IsPlayingFromIgr;

		public bool HasCanReceiveVoice;

		private bool _CanReceiveVoice;

		public bool HasCanSendVoice;

		private bool _CanSendVoice;

		public List<AccountLicense> Licenses
		{
			get
			{
				return _Licenses;
			}
			set
			{
				_Licenses = value;
			}
		}

		public List<AccountLicense> LicensesList => _Licenses;

		public int LicensesCount => _Licenses.Count;

		public bool IsUsingRid
		{
			get
			{
				return _IsUsingRid;
			}
			set
			{
				_IsUsingRid = value;
				HasIsUsingRid = true;
			}
		}

		public bool IsPlayingFromIgr
		{
			get
			{
				return _IsPlayingFromIgr;
			}
			set
			{
				_IsPlayingFromIgr = value;
				HasIsPlayingFromIgr = true;
			}
		}

		public bool CanReceiveVoice
		{
			get
			{
				return _CanReceiveVoice;
			}
			set
			{
				_CanReceiveVoice = value;
				HasCanReceiveVoice = true;
			}
		}

		public bool CanSendVoice
		{
			get
			{
				return _CanSendVoice;
			}
			set
			{
				_CanSendVoice = value;
				HasCanSendVoice = true;
			}
		}

		public bool IsInitialized => true;

		public void AddLicenses(AccountLicense val)
		{
			_Licenses.Add(val);
		}

		public void ClearLicenses()
		{
			_Licenses.Clear();
		}

		public void SetLicenses(List<AccountLicense> val)
		{
			Licenses = val;
		}

		public void SetIsUsingRid(bool val)
		{
			IsUsingRid = val;
		}

		public void SetIsPlayingFromIgr(bool val)
		{
			IsPlayingFromIgr = val;
		}

		public void SetCanReceiveVoice(bool val)
		{
			CanReceiveVoice = val;
		}

		public void SetCanSendVoice(bool val)
		{
			CanSendVoice = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AccountLicense license in Licenses)
			{
				num ^= license.GetHashCode();
			}
			if (HasIsUsingRid)
			{
				num ^= IsUsingRid.GetHashCode();
			}
			if (HasIsPlayingFromIgr)
			{
				num ^= IsPlayingFromIgr.GetHashCode();
			}
			if (HasCanReceiveVoice)
			{
				num ^= CanReceiveVoice.GetHashCode();
			}
			if (HasCanSendVoice)
			{
				num ^= CanSendVoice.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountSettingsNotification accountSettingsNotification = obj as AccountSettingsNotification;
			if (accountSettingsNotification == null)
			{
				return false;
			}
			if (Licenses.Count != accountSettingsNotification.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < Licenses.Count; i++)
			{
				if (!Licenses[i].Equals(accountSettingsNotification.Licenses[i]))
				{
					return false;
				}
			}
			if (HasIsUsingRid != accountSettingsNotification.HasIsUsingRid || (HasIsUsingRid && !IsUsingRid.Equals(accountSettingsNotification.IsUsingRid)))
			{
				return false;
			}
			if (HasIsPlayingFromIgr != accountSettingsNotification.HasIsPlayingFromIgr || (HasIsPlayingFromIgr && !IsPlayingFromIgr.Equals(accountSettingsNotification.IsPlayingFromIgr)))
			{
				return false;
			}
			if (HasCanReceiveVoice != accountSettingsNotification.HasCanReceiveVoice || (HasCanReceiveVoice && !CanReceiveVoice.Equals(accountSettingsNotification.CanReceiveVoice)))
			{
				return false;
			}
			if (HasCanSendVoice != accountSettingsNotification.HasCanSendVoice || (HasCanSendVoice && !CanSendVoice.Equals(accountSettingsNotification.CanSendVoice)))
			{
				return false;
			}
			return true;
		}

		public static AccountSettingsNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountSettingsNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountSettingsNotification accountSettingsNotification = new AccountSettingsNotification();
			DeserializeLengthDelimited(stream, accountSettingsNotification);
			return accountSettingsNotification;
		}

		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream, AccountSettingsNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance, long limit)
		{
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.IsUsingRid = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.IsPlayingFromIgr = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.CanSendVoice = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, AccountSettingsNotification instance)
		{
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense license in instance.Licenses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, license.GetSerializedSize());
					AccountLicense.Serialize(stream, license);
				}
			}
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsPlayingFromIgr)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsPlayingFromIgr);
			}
			if (instance.HasCanReceiveVoice)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.CanReceiveVoice);
			}
			if (instance.HasCanSendVoice)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.CanSendVoice);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Licenses.Count > 0)
			{
				foreach (AccountLicense license in Licenses)
				{
					num++;
					uint serializedSize = license.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasIsUsingRid)
			{
				num++;
				num++;
			}
			if (HasIsPlayingFromIgr)
			{
				num++;
				num++;
			}
			if (HasCanReceiveVoice)
			{
				num++;
				num++;
			}
			if (HasCanSendVoice)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
