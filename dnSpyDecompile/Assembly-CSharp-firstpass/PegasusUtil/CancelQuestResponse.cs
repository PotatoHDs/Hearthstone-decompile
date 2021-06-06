using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000B4 RID: 180
	public class CancelQuestResponse : IProtoBuf
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0002EF7D File Offset: 0x0002D17D
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0002EF85 File Offset: 0x0002D185
		public int QuestId { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002EF8E File Offset: 0x0002D18E
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0002EF96 File Offset: 0x0002D196
		public bool Success { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002EF9F File Offset: 0x0002D19F
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0002EFA7 File Offset: 0x0002D1A7
		public Date NextQuestCancel
		{
			get
			{
				return this._NextQuestCancel;
			}
			set
			{
				this._NextQuestCancel = value;
				this.HasNextQuestCancel = (value != null);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002EFBC File Offset: 0x0002D1BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.QuestId.GetHashCode();
			num ^= this.Success.GetHashCode();
			if (this.HasNextQuestCancel)
			{
				num ^= this.NextQuestCancel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002F010 File Offset: 0x0002D210
		public override bool Equals(object obj)
		{
			CancelQuestResponse cancelQuestResponse = obj as CancelQuestResponse;
			return cancelQuestResponse != null && this.QuestId.Equals(cancelQuestResponse.QuestId) && this.Success.Equals(cancelQuestResponse.Success) && this.HasNextQuestCancel == cancelQuestResponse.HasNextQuestCancel && (!this.HasNextQuestCancel || this.NextQuestCancel.Equals(cancelQuestResponse.NextQuestCancel));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002F085 File Offset: 0x0002D285
		public void Deserialize(Stream stream)
		{
			CancelQuestResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002F08F File Offset: 0x0002D28F
		public static CancelQuestResponse Deserialize(Stream stream, CancelQuestResponse instance)
		{
			return CancelQuestResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002F09C File Offset: 0x0002D29C
		public static CancelQuestResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelQuestResponse cancelQuestResponse = new CancelQuestResponse();
			CancelQuestResponse.DeserializeLengthDelimited(stream, cancelQuestResponse);
			return cancelQuestResponse;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		public static CancelQuestResponse DeserializeLengthDelimited(Stream stream, CancelQuestResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelQuestResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002F0E0 File Offset: 0x0002D2E0
		public static CancelQuestResponse Deserialize(Stream stream, CancelQuestResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						else if (instance.NextQuestCancel == null)
						{
							instance.NextQuestCancel = Date.DeserializeLengthDelimited(stream);
						}
						else
						{
							Date.DeserializeLengthDelimited(stream, instance.NextQuestCancel);
						}
					}
					else
					{
						instance.Success = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002F1AE File Offset: 0x0002D3AE
		public void Serialize(Stream stream)
		{
			CancelQuestResponse.Serialize(stream, this);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002F1B8 File Offset: 0x0002D3B8
		public static void Serialize(Stream stream, CancelQuestResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestId));
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasNextQuestCancel)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.NextQuestCancel.GetSerializedSize());
				Date.Serialize(stream, instance.NextQuestCancel);
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002F21C File Offset: 0x0002D41C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.QuestId));
			num += 1U;
			if (this.HasNextQuestCancel)
			{
				num += 1U;
				uint serializedSize = this.NextQuestCancel.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2U;
		}

		// Token: 0x04000463 RID: 1123
		public bool HasNextQuestCancel;

		// Token: 0x04000464 RID: 1124
		private Date _NextQuestCancel;

		// Token: 0x020005BF RID: 1471
		public enum PacketID
		{
			// Token: 0x04001F8D RID: 8077
			ID = 282
		}
	}
}
