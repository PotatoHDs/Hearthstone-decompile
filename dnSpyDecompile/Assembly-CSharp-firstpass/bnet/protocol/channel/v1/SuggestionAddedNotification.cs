using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B7 RID: 1207
	public class SuggestionAddedNotification : IProtoBuf
	{
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x0010359C File Offset: 0x0010179C
		// (set) Token: 0x06005448 RID: 21576 RVA: 0x001035A4 File Offset: 0x001017A4
		public InvitationSuggestion Suggestion { get; set; }

		// Token: 0x06005449 RID: 21577 RVA: 0x001035AD File Offset: 0x001017AD
		public void SetSuggestion(InvitationSuggestion val)
		{
			this.Suggestion = val;
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600544A RID: 21578 RVA: 0x001035B6 File Offset: 0x001017B6
		// (set) Token: 0x0600544B RID: 21579 RVA: 0x001035BE File Offset: 0x001017BE
		public SubscriberId SubscriberId
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

		// Token: 0x0600544C RID: 21580 RVA: 0x001035D1 File Offset: 0x001017D1
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x001035DC File Offset: 0x001017DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Suggestion.GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0010361C File Offset: 0x0010181C
		public override bool Equals(object obj)
		{
			SuggestionAddedNotification suggestionAddedNotification = obj as SuggestionAddedNotification;
			return suggestionAddedNotification != null && this.Suggestion.Equals(suggestionAddedNotification.Suggestion) && this.HasSubscriberId == suggestionAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(suggestionAddedNotification.SubscriberId));
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00103676 File Offset: 0x00101876
		public static SuggestionAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestionAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x00103680 File Offset: 0x00101880
		public void Deserialize(Stream stream)
		{
			SuggestionAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0010368A File Offset: 0x0010188A
		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance)
		{
			return SuggestionAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00103698 File Offset: 0x00101898
		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SuggestionAddedNotification suggestionAddedNotification = new SuggestionAddedNotification();
			SuggestionAddedNotification.DeserializeLengthDelimited(stream, suggestionAddedNotification);
			return suggestionAddedNotification;
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x001036B4 File Offset: 0x001018B4
		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream, SuggestionAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SuggestionAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x001036DC File Offset: 0x001018DC
		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
				}
				else if (instance.Suggestion == null)
				{
					instance.Suggestion = InvitationSuggestion.DeserializeLengthDelimited(stream);
				}
				else
				{
					InvitationSuggestion.DeserializeLengthDelimited(stream, instance.Suggestion);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x001037AE File Offset: 0x001019AE
		public void Serialize(Stream stream)
		{
			SuggestionAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x001037B8 File Offset: 0x001019B8
		public static void Serialize(Stream stream, SuggestionAddedNotification instance)
		{
			if (instance.Suggestion == null)
			{
				throw new ArgumentNullException("Suggestion", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Suggestion.GetSerializedSize());
			InvitationSuggestion.Serialize(stream, instance.Suggestion);
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00103830 File Offset: 0x00101A30
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Suggestion.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04001ABB RID: 6843
		public bool HasSubscriberId;

		// Token: 0x04001ABC RID: 6844
		private SubscriberId _SubscriberId;
	}
}
