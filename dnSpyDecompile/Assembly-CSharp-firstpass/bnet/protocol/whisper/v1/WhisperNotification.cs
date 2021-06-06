using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002D6 RID: 726
	public class WhisperNotification : IProtoBuf
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x00094274 File Offset: 0x00092474
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x0009427C File Offset: 0x0009247C
		public AccountId SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x0009428F File Offset: 0x0009248F
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x00094298 File Offset: 0x00092498
		// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x000942A0 File Offset: 0x000924A0
		public Whisper Whisper
		{
			get
			{
				return this._Whisper;
			}
			set
			{
				this._Whisper = value;
				this.HasWhisper = (value != null);
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000942B3 File Offset: 0x000924B3
		public void SetWhisper(Whisper val)
		{
			this.Whisper = val;
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000942BC File Offset: 0x000924BC
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000942C4 File Offset: 0x000924C4
		public string SenderBattleTag
		{
			get
			{
				return this._SenderBattleTag;
			}
			set
			{
				this._SenderBattleTag = value;
				this.HasSenderBattleTag = (value != null);
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000942D7 File Offset: 0x000924D7
		public void SetSenderBattleTag(string val)
		{
			this.SenderBattleTag = val;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000942E0 File Offset: 0x000924E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasWhisper)
			{
				num ^= this.Whisper.GetHashCode();
			}
			if (this.HasSenderBattleTag)
			{
				num ^= this.SenderBattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0009433C File Offset: 0x0009253C
		public override bool Equals(object obj)
		{
			WhisperNotification whisperNotification = obj as WhisperNotification;
			return whisperNotification != null && this.HasSubscriberId == whisperNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(whisperNotification.SubscriberId)) && this.HasWhisper == whisperNotification.HasWhisper && (!this.HasWhisper || this.Whisper.Equals(whisperNotification.Whisper)) && this.HasSenderBattleTag == whisperNotification.HasSenderBattleTag && (!this.HasSenderBattleTag || this.SenderBattleTag.Equals(whisperNotification.SenderBattleTag));
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000943D7 File Offset: 0x000925D7
		public static WhisperNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperNotification>(bs, 0, -1);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000943E1 File Offset: 0x000925E1
		public void Deserialize(Stream stream)
		{
			WhisperNotification.Deserialize(stream, this);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000943EB File Offset: 0x000925EB
		public static WhisperNotification Deserialize(Stream stream, WhisperNotification instance)
		{
			return WhisperNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000943F8 File Offset: 0x000925F8
		public static WhisperNotification DeserializeLengthDelimited(Stream stream)
		{
			WhisperNotification whisperNotification = new WhisperNotification();
			WhisperNotification.DeserializeLengthDelimited(stream, whisperNotification);
			return whisperNotification;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00094414 File Offset: 0x00092614
		public static WhisperNotification DeserializeLengthDelimited(Stream stream, WhisperNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WhisperNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0009443C File Offset: 0x0009263C
		public static WhisperNotification Deserialize(Stream stream, WhisperNotification instance, long limit)
		{
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.SenderBattleTag = ProtocolParser.ReadString(stream);
						}
					}
					else if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
					}
				}
				else if (instance.SubscriberId == null)
				{
					instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00094524 File Offset: 0x00092724
		public void Serialize(Stream stream)
		{
			WhisperNotification.Serialize(stream, this);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00094530 File Offset: 0x00092730
		public static void Serialize(Stream stream, WhisperNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasWhisper)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000945C0 File Offset: 0x000927C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize = this.SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasWhisper)
			{
				num += 1U;
				uint serializedSize2 = this.Whisper.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSenderBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SenderBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040011FE RID: 4606
		public bool HasSubscriberId;

		// Token: 0x040011FF RID: 4607
		private AccountId _SubscriberId;

		// Token: 0x04001200 RID: 4608
		public bool HasWhisper;

		// Token: 0x04001201 RID: 4609
		private Whisper _Whisper;

		// Token: 0x04001202 RID: 4610
		public bool HasSenderBattleTag;

		// Token: 0x04001203 RID: 4611
		private string _SenderBattleTag;
	}
}
