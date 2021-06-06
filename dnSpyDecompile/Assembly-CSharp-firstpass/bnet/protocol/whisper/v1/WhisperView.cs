using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E7 RID: 743
	public class WhisperView : IProtoBuf
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x00097D57 File Offset: 0x00095F57
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x00097D5F File Offset: 0x00095F5F
		public AccountId SenderId
		{
			get
			{
				return this._SenderId;
			}
			set
			{
				this._SenderId = value;
				this.HasSenderId = (value != null);
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00097D72 File Offset: 0x00095F72
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00097D7B File Offset: 0x00095F7B
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x00097D83 File Offset: 0x00095F83
		public ViewMarker ViewMarker
		{
			get
			{
				return this._ViewMarker;
			}
			set
			{
				this._ViewMarker = value;
				this.HasViewMarker = (value != null);
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00097D96 File Offset: 0x00095F96
		public void SetViewMarker(ViewMarker val)
		{
			this.ViewMarker = val;
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00097D9F File Offset: 0x00095F9F
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x00097DA7 File Offset: 0x00095FA7
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

		// Token: 0x06002C0E RID: 11278 RVA: 0x00097DBA File Offset: 0x00095FBA
		public void SetSenderBattleTag(string val)
		{
			this.SenderBattleTag = val;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00097DC4 File Offset: 0x00095FC4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			if (this.HasViewMarker)
			{
				num ^= this.ViewMarker.GetHashCode();
			}
			if (this.HasSenderBattleTag)
			{
				num ^= this.SenderBattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00097E20 File Offset: 0x00096020
		public override bool Equals(object obj)
		{
			WhisperView whisperView = obj as WhisperView;
			return whisperView != null && this.HasSenderId == whisperView.HasSenderId && (!this.HasSenderId || this.SenderId.Equals(whisperView.SenderId)) && this.HasViewMarker == whisperView.HasViewMarker && (!this.HasViewMarker || this.ViewMarker.Equals(whisperView.ViewMarker)) && this.HasSenderBattleTag == whisperView.HasSenderBattleTag && (!this.HasSenderBattleTag || this.SenderBattleTag.Equals(whisperView.SenderBattleTag));
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00097EBB File Offset: 0x000960BB
		public static WhisperView ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WhisperView>(bs, 0, -1);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00097EC5 File Offset: 0x000960C5
		public void Deserialize(Stream stream)
		{
			WhisperView.Deserialize(stream, this);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x00097ECF File Offset: 0x000960CF
		public static WhisperView Deserialize(Stream stream, WhisperView instance)
		{
			return WhisperView.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x00097EDC File Offset: 0x000960DC
		public static WhisperView DeserializeLengthDelimited(Stream stream)
		{
			WhisperView whisperView = new WhisperView();
			WhisperView.DeserializeLengthDelimited(stream, whisperView);
			return whisperView;
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x00097EF8 File Offset: 0x000960F8
		public static WhisperView DeserializeLengthDelimited(Stream stream, WhisperView instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WhisperView.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x00097F20 File Offset: 0x00096120
		public static WhisperView Deserialize(Stream stream, WhisperView instance, long limit)
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
					else if (instance.ViewMarker == null)
					{
						instance.ViewMarker = ViewMarker.DeserializeLengthDelimited(stream);
					}
					else
					{
						ViewMarker.DeserializeLengthDelimited(stream, instance.ViewMarker);
					}
				}
				else if (instance.SenderId == null)
				{
					instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00098008 File Offset: 0x00096208
		public void Serialize(Stream stream)
		{
			WhisperView.Serialize(stream, this);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00098014 File Offset: 0x00096214
		public static void Serialize(Stream stream, WhisperView instance)
		{
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasViewMarker)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ViewMarker.GetSerializedSize());
				ViewMarker.Serialize(stream, instance.ViewMarker);
			}
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
			}
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000980A4 File Offset: 0x000962A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize = this.SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasViewMarker)
			{
				num += 1U;
				uint serializedSize2 = this.ViewMarker.GetSerializedSize();
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

		// Token: 0x0400124D RID: 4685
		public bool HasSenderId;

		// Token: 0x0400124E RID: 4686
		private AccountId _SenderId;

		// Token: 0x0400124F RID: 4687
		public bool HasViewMarker;

		// Token: 0x04001250 RID: 4688
		private ViewMarker _ViewMarker;

		// Token: 0x04001251 RID: 4689
		public bool HasSenderBattleTag;

		// Token: 0x04001252 RID: 4690
		private string _SenderBattleTag;
	}
}
