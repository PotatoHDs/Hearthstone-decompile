using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052E RID: 1326
	public class PrivacyInfo : IProtoBuf
	{
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x0012074E File Offset: 0x0011E94E
		// (set) Token: 0x06005F2F RID: 24367 RVA: 0x00120756 File Offset: 0x0011E956
		public bool IsUsingRid
		{
			get
			{
				return this._IsUsingRid;
			}
			set
			{
				this._IsUsingRid = value;
				this.HasIsUsingRid = true;
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00120766 File Offset: 0x0011E966
		public void SetIsUsingRid(bool val)
		{
			this.IsUsingRid = val;
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x0012076F File Offset: 0x0011E96F
		// (set) Token: 0x06005F32 RID: 24370 RVA: 0x00120777 File Offset: 0x0011E977
		public bool IsVisibleForViewFriends
		{
			get
			{
				return this._IsVisibleForViewFriends;
			}
			set
			{
				this._IsVisibleForViewFriends = value;
				this.HasIsVisibleForViewFriends = true;
			}
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x00120787 File Offset: 0x0011E987
		public void SetIsVisibleForViewFriends(bool val)
		{
			this.IsVisibleForViewFriends = val;
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06005F34 RID: 24372 RVA: 0x00120790 File Offset: 0x0011E990
		// (set) Token: 0x06005F35 RID: 24373 RVA: 0x00120798 File Offset: 0x0011E998
		public bool IsHiddenFromFriendFinder
		{
			get
			{
				return this._IsHiddenFromFriendFinder;
			}
			set
			{
				this._IsHiddenFromFriendFinder = value;
				this.HasIsHiddenFromFriendFinder = true;
			}
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x001207A8 File Offset: 0x0011E9A8
		public void SetIsHiddenFromFriendFinder(bool val)
		{
			this.IsHiddenFromFriendFinder = val;
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x001207B1 File Offset: 0x0011E9B1
		// (set) Token: 0x06005F38 RID: 24376 RVA: 0x001207B9 File Offset: 0x0011E9B9
		public PrivacyInfo.Types.GameInfoPrivacy GameInfoPrivacy
		{
			get
			{
				return this._GameInfoPrivacy;
			}
			set
			{
				this._GameInfoPrivacy = value;
				this.HasGameInfoPrivacy = true;
			}
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x001207C9 File Offset: 0x0011E9C9
		public void SetGameInfoPrivacy(PrivacyInfo.Types.GameInfoPrivacy val)
		{
			this.GameInfoPrivacy = val;
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x001207D2 File Offset: 0x0011E9D2
		// (set) Token: 0x06005F3B RID: 24379 RVA: 0x001207DA File Offset: 0x0011E9DA
		public bool OnlyAllowFriendWhispers
		{
			get
			{
				return this._OnlyAllowFriendWhispers;
			}
			set
			{
				this._OnlyAllowFriendWhispers = value;
				this.HasOnlyAllowFriendWhispers = true;
			}
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001207EA File Offset: 0x0011E9EA
		public void SetOnlyAllowFriendWhispers(bool val)
		{
			this.OnlyAllowFriendWhispers = val;
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001207F4 File Offset: 0x0011E9F4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsUsingRid)
			{
				num ^= this.IsUsingRid.GetHashCode();
			}
			if (this.HasIsVisibleForViewFriends)
			{
				num ^= this.IsVisibleForViewFriends.GetHashCode();
			}
			if (this.HasIsHiddenFromFriendFinder)
			{
				num ^= this.IsHiddenFromFriendFinder.GetHashCode();
			}
			if (this.HasGameInfoPrivacy)
			{
				num ^= this.GameInfoPrivacy.GetHashCode();
			}
			if (this.HasOnlyAllowFriendWhispers)
			{
				num ^= this.OnlyAllowFriendWhispers.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x00120894 File Offset: 0x0011EA94
		public override bool Equals(object obj)
		{
			PrivacyInfo privacyInfo = obj as PrivacyInfo;
			return privacyInfo != null && this.HasIsUsingRid == privacyInfo.HasIsUsingRid && (!this.HasIsUsingRid || this.IsUsingRid.Equals(privacyInfo.IsUsingRid)) && this.HasIsVisibleForViewFriends == privacyInfo.HasIsVisibleForViewFriends && (!this.HasIsVisibleForViewFriends || this.IsVisibleForViewFriends.Equals(privacyInfo.IsVisibleForViewFriends)) && this.HasIsHiddenFromFriendFinder == privacyInfo.HasIsHiddenFromFriendFinder && (!this.HasIsHiddenFromFriendFinder || this.IsHiddenFromFriendFinder.Equals(privacyInfo.IsHiddenFromFriendFinder)) && this.HasGameInfoPrivacy == privacyInfo.HasGameInfoPrivacy && (!this.HasGameInfoPrivacy || this.GameInfoPrivacy.Equals(privacyInfo.GameInfoPrivacy)) && this.HasOnlyAllowFriendWhispers == privacyInfo.HasOnlyAllowFriendWhispers && (!this.HasOnlyAllowFriendWhispers || this.OnlyAllowFriendWhispers.Equals(privacyInfo.OnlyAllowFriendWhispers));
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x0012099F File Offset: 0x0011EB9F
		public static PrivacyInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PrivacyInfo>(bs, 0, -1);
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x001209A9 File Offset: 0x0011EBA9
		public void Deserialize(Stream stream)
		{
			PrivacyInfo.Deserialize(stream, this);
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x001209B3 File Offset: 0x0011EBB3
		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance)
		{
			return PrivacyInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x001209C0 File Offset: 0x0011EBC0
		public static PrivacyInfo DeserializeLengthDelimited(Stream stream)
		{
			PrivacyInfo privacyInfo = new PrivacyInfo();
			PrivacyInfo.DeserializeLengthDelimited(stream, privacyInfo);
			return privacyInfo;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x001209DC File Offset: 0x0011EBDC
		public static PrivacyInfo DeserializeLengthDelimited(Stream stream, PrivacyInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PrivacyInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00120A04 File Offset: 0x0011EC04
		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance, long limit)
		{
			instance.GameInfoPrivacy = PrivacyInfo.Types.GameInfoPrivacy.PRIVACY_FRIENDS;
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 32)
					{
						if (num == 24)
						{
							instance.IsUsingRid = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.IsVisibleForViewFriends = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 40)
						{
							instance.IsHiddenFromFriendFinder = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.GameInfoPrivacy = (PrivacyInfo.Types.GameInfoPrivacy)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 56)
						{
							instance.OnlyAllowFriendWhispers = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00120AF3 File Offset: 0x0011ECF3
		public void Serialize(Stream stream)
		{
			PrivacyInfo.Serialize(stream, this);
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x00120AFC File Offset: 0x0011ECFC
		public static void Serialize(Stream stream, PrivacyInfo instance)
		{
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsVisibleForViewFriends)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsVisibleForViewFriends);
			}
			if (instance.HasIsHiddenFromFriendFinder)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsHiddenFromFriendFinder);
			}
			if (instance.HasGameInfoPrivacy)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameInfoPrivacy));
			}
			if (instance.HasOnlyAllowFriendWhispers)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.OnlyAllowFriendWhispers);
			}
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x00120B98 File Offset: 0x0011ED98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIsUsingRid)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsVisibleForViewFriends)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsHiddenFromFriendFinder)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasGameInfoPrivacy)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameInfoPrivacy));
			}
			if (this.HasOnlyAllowFriendWhispers)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D51 RID: 7505
		public bool HasIsUsingRid;

		// Token: 0x04001D52 RID: 7506
		private bool _IsUsingRid;

		// Token: 0x04001D53 RID: 7507
		public bool HasIsVisibleForViewFriends;

		// Token: 0x04001D54 RID: 7508
		private bool _IsVisibleForViewFriends;

		// Token: 0x04001D55 RID: 7509
		public bool HasIsHiddenFromFriendFinder;

		// Token: 0x04001D56 RID: 7510
		private bool _IsHiddenFromFriendFinder;

		// Token: 0x04001D57 RID: 7511
		public bool HasGameInfoPrivacy;

		// Token: 0x04001D58 RID: 7512
		private PrivacyInfo.Types.GameInfoPrivacy _GameInfoPrivacy;

		// Token: 0x04001D59 RID: 7513
		public bool HasOnlyAllowFriendWhispers;

		// Token: 0x04001D5A RID: 7514
		private bool _OnlyAllowFriendWhispers;

		// Token: 0x020006FF RID: 1791
		public static class Types
		{
			// Token: 0x02000716 RID: 1814
			public enum GameInfoPrivacy
			{
				// Token: 0x0400230C RID: 8972
				PRIVACY_ME,
				// Token: 0x0400230D RID: 8973
				PRIVACY_FRIENDS,
				// Token: 0x0400230E RID: 8974
				PRIVACY_EVERYONE
			}
		}
	}
}
