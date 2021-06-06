using System;
using System.IO;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000306 RID: 774
	public class FacebookBnetFriendListNotificationResponse : IProtoBuf
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x0009EEC8 File Offset: 0x0009D0C8
		// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x0009EED0 File Offset: 0x0009D0D0
		public bool Continue
		{
			get
			{
				return this._Continue;
			}
			set
			{
				this._Continue = value;
				this.HasContinue = true;
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
		public void SetContinue(bool val)
		{
			this.Continue = val;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0009EEEC File Offset: 0x0009D0EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasContinue)
			{
				num ^= this.Continue.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0009EF20 File Offset: 0x0009D120
		public override bool Equals(object obj)
		{
			FacebookBnetFriendListNotificationResponse facebookBnetFriendListNotificationResponse = obj as FacebookBnetFriendListNotificationResponse;
			return facebookBnetFriendListNotificationResponse != null && this.HasContinue == facebookBnetFriendListNotificationResponse.HasContinue && (!this.HasContinue || this.Continue.Equals(facebookBnetFriendListNotificationResponse.Continue));
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0009EF68 File Offset: 0x0009D168
		public static FacebookBnetFriendListNotificationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriendListNotificationResponse>(bs, 0, -1);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x0009EF72 File Offset: 0x0009D172
		public void Deserialize(Stream stream)
		{
			FacebookBnetFriendListNotificationResponse.Deserialize(stream, this);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0009EF7C File Offset: 0x0009D17C
		public static FacebookBnetFriendListNotificationResponse Deserialize(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			return FacebookBnetFriendListNotificationResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0009EF88 File Offset: 0x0009D188
		public static FacebookBnetFriendListNotificationResponse DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriendListNotificationResponse facebookBnetFriendListNotificationResponse = new FacebookBnetFriendListNotificationResponse();
			FacebookBnetFriendListNotificationResponse.DeserializeLengthDelimited(stream, facebookBnetFriendListNotificationResponse);
			return facebookBnetFriendListNotificationResponse;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0009EFA4 File Offset: 0x0009D1A4
		public static FacebookBnetFriendListNotificationResponse DeserializeLengthDelimited(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FacebookBnetFriendListNotificationResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0009EFCC File Offset: 0x0009D1CC
		public static FacebookBnetFriendListNotificationResponse Deserialize(Stream stream, FacebookBnetFriendListNotificationResponse instance, long limit)
		{
			instance.Continue = true;
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
				else if (num == 8)
				{
					instance.Continue = ProtocolParser.ReadBool(stream);
				}
				else
				{
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

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0009F052 File Offset: 0x0009D252
		public void Serialize(Stream stream)
		{
			FacebookBnetFriendListNotificationResponse.Serialize(stream, this);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0009F05B File Offset: 0x0009D25B
		public static void Serialize(Stream stream, FacebookBnetFriendListNotificationResponse instance)
		{
			if (instance.HasContinue)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Continue);
			}
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0009F078 File Offset: 0x0009D278
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasContinue)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040012DE RID: 4830
		public bool HasContinue;

		// Token: 0x040012DF RID: 4831
		private bool _Continue;
	}
}
