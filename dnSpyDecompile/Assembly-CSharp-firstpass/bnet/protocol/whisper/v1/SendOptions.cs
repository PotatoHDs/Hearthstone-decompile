using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E8 RID: 744
	public class SendOptions : IProtoBuf
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x00098122 File Offset: 0x00096322
		// (set) Token: 0x06002C1D RID: 11293 RVA: 0x0009812A File Offset: 0x0009632A
		public AccountId TargetId
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

		// Token: 0x06002C1E RID: 11294 RVA: 0x0009813D File Offset: 0x0009633D
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x00098146 File Offset: 0x00096346
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0009814E File Offset: 0x0009634E
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				this._Content = value;
				this.HasContent = (value != null);
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x00098161 File Offset: 0x00096361
		public void SetContent(string val)
		{
			this.Content = val;
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0009816C File Offset: 0x0009636C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasContent)
			{
				num ^= this.Content.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000981B4 File Offset: 0x000963B4
		public override bool Equals(object obj)
		{
			SendOptions sendOptions = obj as SendOptions;
			return sendOptions != null && this.HasTargetId == sendOptions.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(sendOptions.TargetId)) && this.HasContent == sendOptions.HasContent && (!this.HasContent || this.Content.Equals(sendOptions.Content));
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00098224 File Offset: 0x00096424
		public static SendOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendOptions>(bs, 0, -1);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0009822E File Offset: 0x0009642E
		public void Deserialize(Stream stream)
		{
			SendOptions.Deserialize(stream, this);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x00098238 File Offset: 0x00096438
		public static SendOptions Deserialize(Stream stream, SendOptions instance)
		{
			return SendOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x00098244 File Offset: 0x00096444
		public static SendOptions DeserializeLengthDelimited(Stream stream)
		{
			SendOptions sendOptions = new SendOptions();
			SendOptions.DeserializeLengthDelimited(stream, sendOptions);
			return sendOptions;
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x00098260 File Offset: 0x00096460
		public static SendOptions DeserializeLengthDelimited(Stream stream, SendOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x00098288 File Offset: 0x00096488
		public static SendOptions Deserialize(Stream stream, SendOptions instance, long limit)
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
					else
					{
						instance.Content = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.TargetId == null)
				{
					instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0009833A File Offset: 0x0009653A
		public void Serialize(Stream stream)
		{
			SendOptions.Serialize(stream, this);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x00098344 File Offset: 0x00096544
		public static void Serialize(Stream stream, SendOptions instance)
		{
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000983A4 File Offset: 0x000965A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize = this.TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasContent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001253 RID: 4691
		public bool HasTargetId;

		// Token: 0x04001254 RID: 4692
		private AccountId _TargetId;

		// Token: 0x04001255 RID: 4693
		public bool HasContent;

		// Token: 0x04001256 RID: 4694
		private string _Content;
	}
}
