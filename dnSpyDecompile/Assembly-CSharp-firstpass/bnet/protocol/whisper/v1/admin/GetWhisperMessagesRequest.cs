using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1.admin
{
	// Token: 0x020002E9 RID: 745
	public class GetWhisperMessagesRequest : IProtoBuf
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000983FF File Offset: 0x000965FF
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x00098407 File Offset: 0x00096607
		public AccountId ReceiverId
		{
			get
			{
				return this._ReceiverId;
			}
			set
			{
				this._ReceiverId = value;
				this.HasReceiverId = (value != null);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0009841A File Offset: 0x0009661A
		public void SetReceiverId(AccountId val)
		{
			this.ReceiverId = val;
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x00098423 File Offset: 0x00096623
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x0009842B File Offset: 0x0009662B
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

		// Token: 0x06002C34 RID: 11316 RVA: 0x0009843E File Offset: 0x0009663E
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x00098447 File Offset: 0x00096647
		// (set) Token: 0x06002C36 RID: 11318 RVA: 0x0009844F File Offset: 0x0009664F
		public GetEventOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00098462 File Offset: 0x00096662
		public void SetOptions(GetEventOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0009846C File Offset: 0x0009666C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasReceiverId)
			{
				num ^= this.ReceiverId.GetHashCode();
			}
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000984C8 File Offset: 0x000966C8
		public override bool Equals(object obj)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = obj as GetWhisperMessagesRequest;
			return getWhisperMessagesRequest != null && this.HasReceiverId == getWhisperMessagesRequest.HasReceiverId && (!this.HasReceiverId || this.ReceiverId.Equals(getWhisperMessagesRequest.ReceiverId)) && this.HasSenderId == getWhisperMessagesRequest.HasSenderId && (!this.HasSenderId || this.SenderId.Equals(getWhisperMessagesRequest.SenderId)) && this.HasOptions == getWhisperMessagesRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getWhisperMessagesRequest.Options));
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00098563 File Offset: 0x00096763
		public static GetWhisperMessagesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesRequest>(bs, 0, -1);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0009856D File Offset: 0x0009676D
		public void Deserialize(Stream stream)
		{
			GetWhisperMessagesRequest.Deserialize(stream, this);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x00098577 File Offset: 0x00096777
		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			return GetWhisperMessagesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00098584 File Offset: 0x00096784
		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = new GetWhisperMessagesRequest();
			GetWhisperMessagesRequest.DeserializeLengthDelimited(stream, getWhisperMessagesRequest);
			return getWhisperMessagesRequest;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000985A0 File Offset: 0x000967A0
		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream, GetWhisperMessagesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWhisperMessagesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000985C8 File Offset: 0x000967C8
		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance, long limit)
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
						else if (instance.Options == null)
						{
							instance.Options = GetEventOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							GetEventOptions.DeserializeLengthDelimited(stream, instance.Options);
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
				else if (instance.ReceiverId == null)
				{
					instance.ReceiverId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.ReceiverId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000986CA File Offset: 0x000968CA
		public void Serialize(Stream stream)
		{
			GetWhisperMessagesRequest.Serialize(stream, this);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000986D4 File Offset: 0x000968D4
		public static void Serialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			if (instance.HasReceiverId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ReceiverId.GetSerializedSize());
				AccountId.Serialize(stream, instance.ReceiverId);
			}
			if (instance.HasSenderId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetEventOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00098768 File Offset: 0x00096968
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasReceiverId)
			{
				num += 1U;
				uint serializedSize = this.ReceiverId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize2 = this.SenderId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize3 = this.Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001257 RID: 4695
		public bool HasReceiverId;

		// Token: 0x04001258 RID: 4696
		private AccountId _ReceiverId;

		// Token: 0x04001259 RID: 4697
		public bool HasSenderId;

		// Token: 0x0400125A RID: 4698
		private AccountId _SenderId;

		// Token: 0x0400125B RID: 4699
		public bool HasOptions;

		// Token: 0x0400125C RID: 4700
		private GetEventOptions _Options;
	}
}
