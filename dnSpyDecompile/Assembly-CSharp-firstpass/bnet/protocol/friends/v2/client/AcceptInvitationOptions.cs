using System;
using System.IO;
using bnet.protocol.friends.v2.client.Types;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040D RID: 1037
	public class AcceptInvitationOptions : IProtoBuf
	{
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x000D8ABB File Offset: 0x000D6CBB
		// (set) Token: 0x060044EC RID: 17644 RVA: 0x000D8AC3 File Offset: 0x000D6CC3
		public FriendLevel Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000D8AD3 File Offset: 0x000D6CD3
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x000D8ADC File Offset: 0x000D6CDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			return num;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x000D8B18 File Offset: 0x000D6D18
		public override bool Equals(object obj)
		{
			AcceptInvitationOptions acceptInvitationOptions = obj as AcceptInvitationOptions;
			return acceptInvitationOptions != null && this.HasLevel == acceptInvitationOptions.HasLevel && (!this.HasLevel || this.Level.Equals(acceptInvitationOptions.Level));
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000D8B6B File Offset: 0x000D6D6B
		public static AcceptInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationOptions>(bs, 0, -1);
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000D8B75 File Offset: 0x000D6D75
		public void Deserialize(Stream stream)
		{
			AcceptInvitationOptions.Deserialize(stream, this);
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x000D8B7F File Offset: 0x000D6D7F
		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance)
		{
			return AcceptInvitationOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x000D8B8C File Offset: 0x000D6D8C
		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationOptions acceptInvitationOptions = new AcceptInvitationOptions();
			AcceptInvitationOptions.DeserializeLengthDelimited(stream, acceptInvitationOptions);
			return acceptInvitationOptions;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000D8BA8 File Offset: 0x000D6DA8
		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream, AcceptInvitationOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000D8BD0 File Offset: 0x000D6DD0
		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060044F7 RID: 17655 RVA: 0x000D8C57 File Offset: 0x000D6E57
		public void Serialize(Stream stream)
		{
			AcceptInvitationOptions.Serialize(stream, this);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000D8C60 File Offset: 0x000D6E60
		public static void Serialize(Stream stream, AcceptInvitationOptions instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x000D8C80 File Offset: 0x000D6E80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			return num;
		}

		// Token: 0x0400173B RID: 5947
		public bool HasLevel;

		// Token: 0x0400173C RID: 5948
		private FriendLevel _Level;
	}
}
