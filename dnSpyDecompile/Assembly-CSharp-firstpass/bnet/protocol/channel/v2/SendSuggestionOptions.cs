using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000484 RID: 1156
	public class SendSuggestionOptions : IProtoBuf
	{
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x000F928B File Offset: 0x000F748B
		// (set) Token: 0x0600504B RID: 20555 RVA: 0x000F9293 File Offset: 0x000F7493
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x000F92A6 File Offset: 0x000F74A6
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x0600504D RID: 20557 RVA: 0x000F92AF File Offset: 0x000F74AF
		// (set) Token: 0x0600504E RID: 20558 RVA: 0x000F92B7 File Offset: 0x000F74B7
		public GameAccountHandle TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x000F92CA File Offset: 0x000F74CA
		public void SetTargetId(GameAccountHandle val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005050 RID: 20560 RVA: 0x000F92D3 File Offset: 0x000F74D3
		// (set) Token: 0x06005051 RID: 20561 RVA: 0x000F92DB File Offset: 0x000F74DB
		public GameAccountHandle ApprovalId
		{
			get
			{
				return this._ApprovalId;
			}
			set
			{
				this._ApprovalId = value;
				this.HasApprovalId = (value != null);
			}
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x000F92EE File Offset: 0x000F74EE
		public void SetApprovalId(GameAccountHandle val)
		{
			this.ApprovalId = val;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x000F92F8 File Offset: 0x000F74F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasApprovalId)
			{
				num ^= this.ApprovalId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x000F9354 File Offset: 0x000F7554
		public override bool Equals(object obj)
		{
			SendSuggestionOptions sendSuggestionOptions = obj as SendSuggestionOptions;
			return sendSuggestionOptions != null && this.HasChannelId == sendSuggestionOptions.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(sendSuggestionOptions.ChannelId)) && this.HasTargetId == sendSuggestionOptions.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(sendSuggestionOptions.TargetId)) && this.HasApprovalId == sendSuggestionOptions.HasApprovalId && (!this.HasApprovalId || this.ApprovalId.Equals(sendSuggestionOptions.ApprovalId));
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005055 RID: 20565 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x000F93EF File Offset: 0x000F75EF
		public static SendSuggestionOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendSuggestionOptions>(bs, 0, -1);
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x000F93F9 File Offset: 0x000F75F9
		public void Deserialize(Stream stream)
		{
			SendSuggestionOptions.Deserialize(stream, this);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x000F9403 File Offset: 0x000F7603
		public static SendSuggestionOptions Deserialize(Stream stream, SendSuggestionOptions instance)
		{
			return SendSuggestionOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x000F9410 File Offset: 0x000F7610
		public static SendSuggestionOptions DeserializeLengthDelimited(Stream stream)
		{
			SendSuggestionOptions sendSuggestionOptions = new SendSuggestionOptions();
			SendSuggestionOptions.DeserializeLengthDelimited(stream, sendSuggestionOptions);
			return sendSuggestionOptions;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x000F942C File Offset: 0x000F762C
		public static SendSuggestionOptions DeserializeLengthDelimited(Stream stream, SendSuggestionOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendSuggestionOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x000F9454 File Offset: 0x000F7654
		public static SendSuggestionOptions Deserialize(Stream stream, SendSuggestionOptions instance, long limit)
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
						else if (instance.ApprovalId == null)
						{
							instance.ApprovalId = GameAccountHandle.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.ApprovalId);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x000F9556 File Offset: 0x000F7756
		public void Serialize(Stream stream)
		{
			SendSuggestionOptions.Serialize(stream, this);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x000F9560 File Offset: 0x000F7760
		public static void Serialize(Stream stream, SendSuggestionOptions instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.TargetId);
			}
			if (instance.HasApprovalId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ApprovalId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.ApprovalId);
			}
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x000F95F4 File Offset: 0x000F77F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasApprovalId)
			{
				num += 1U;
				uint serializedSize3 = this.ApprovalId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040019E8 RID: 6632
		public bool HasChannelId;

		// Token: 0x040019E9 RID: 6633
		private ChannelId _ChannelId;

		// Token: 0x040019EA RID: 6634
		public bool HasTargetId;

		// Token: 0x040019EB RID: 6635
		private GameAccountHandle _TargetId;

		// Token: 0x040019EC RID: 6636
		public bool HasApprovalId;

		// Token: 0x040019ED RID: 6637
		private GameAccountHandle _ApprovalId;
	}
}
