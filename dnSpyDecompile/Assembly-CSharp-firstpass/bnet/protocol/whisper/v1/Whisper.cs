using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E6 RID: 742
	public class Whisper : IProtoBuf
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x0009751F File Offset: 0x0009571F
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x00097527 File Offset: 0x00095727
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

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0009753A File Offset: 0x0009573A
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x00097543 File Offset: 0x00095743
		// (set) Token: 0x06002BE4 RID: 11236 RVA: 0x0009754B File Offset: 0x0009574B
		public AccountId RecipientId
		{
			get
			{
				return this._RecipientId;
			}
			set
			{
				this._RecipientId = value;
				this.HasRecipientId = (value != null);
			}
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0009755E File Offset: 0x0009575E
		public void SetRecipientId(AccountId val)
		{
			this.RecipientId = val;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00097567 File Offset: 0x00095767
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x0009756F File Offset: 0x0009576F
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

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00097582 File Offset: 0x00095782
		public void SetContent(string val)
		{
			this.Content = val;
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x0009758B File Offset: 0x0009578B
		// (set) Token: 0x06002BEA RID: 11242 RVA: 0x00097593 File Offset: 0x00095793
		public List<EmbedInfo> Embed
		{
			get
			{
				return this._Embed;
			}
			set
			{
				this._Embed = value;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x0009758B File Offset: 0x0009578B
		public List<EmbedInfo> EmbedList
		{
			get
			{
				return this._Embed;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x0009759C File Offset: 0x0009579C
		public int EmbedCount
		{
			get
			{
				return this._Embed.Count;
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000975A9 File Offset: 0x000957A9
		public void AddEmbed(EmbedInfo val)
		{
			this._Embed.Add(val);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000975B7 File Offset: 0x000957B7
		public void ClearEmbed()
		{
			this._Embed.Clear();
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000975C4 File Offset: 0x000957C4
		public void SetEmbed(List<EmbedInfo> val)
		{
			this.Embed = val;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000975CD File Offset: 0x000957CD
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x000975D5 File Offset: 0x000957D5
		public ulong CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				this._CreationTime = value;
				this.HasCreationTime = true;
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000975E5 File Offset: 0x000957E5
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000975EE File Offset: 0x000957EE
		// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x000975F6 File Offset: 0x000957F6
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00097606 File Offset: 0x00095806
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x0009760F File Offset: 0x0009580F
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x00097617 File Offset: 0x00095817
		public MessageId MessageId
		{
			get
			{
				return this._MessageId;
			}
			set
			{
				this._MessageId = value;
				this.HasMessageId = (value != null);
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0009762A File Offset: 0x0009582A
		public void SetMessageId(MessageId val)
		{
			this.MessageId = val;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00097634 File Offset: 0x00095834
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			if (this.HasRecipientId)
			{
				num ^= this.RecipientId.GetHashCode();
			}
			if (this.HasContent)
			{
				num ^= this.Content.GetHashCode();
			}
			foreach (EmbedInfo embedInfo in this.Embed)
			{
				num ^= embedInfo.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasMessageId)
			{
				num ^= this.MessageId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00097724 File Offset: 0x00095924
		public override bool Equals(object obj)
		{
			Whisper whisper = obj as Whisper;
			if (whisper == null)
			{
				return false;
			}
			if (this.HasSenderId != whisper.HasSenderId || (this.HasSenderId && !this.SenderId.Equals(whisper.SenderId)))
			{
				return false;
			}
			if (this.HasRecipientId != whisper.HasRecipientId || (this.HasRecipientId && !this.RecipientId.Equals(whisper.RecipientId)))
			{
				return false;
			}
			if (this.HasContent != whisper.HasContent || (this.HasContent && !this.Content.Equals(whisper.Content)))
			{
				return false;
			}
			if (this.Embed.Count != whisper.Embed.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Embed.Count; i++)
			{
				if (!this.Embed[i].Equals(whisper.Embed[i]))
				{
					return false;
				}
			}
			return this.HasCreationTime == whisper.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(whisper.CreationTime)) && this.HasProgram == whisper.HasProgram && (!this.HasProgram || this.Program.Equals(whisper.Program)) && this.HasMessageId == whisper.HasMessageId && (!this.HasMessageId || this.MessageId.Equals(whisper.MessageId));
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00097897 File Offset: 0x00095A97
		public static Whisper ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Whisper>(bs, 0, -1);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000978A1 File Offset: 0x00095AA1
		public void Deserialize(Stream stream)
		{
			Whisper.Deserialize(stream, this);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000978AB File Offset: 0x00095AAB
		public static Whisper Deserialize(Stream stream, Whisper instance)
		{
			return Whisper.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000978B8 File Offset: 0x00095AB8
		public static Whisper DeserializeLengthDelimited(Stream stream)
		{
			Whisper whisper = new Whisper();
			Whisper.DeserializeLengthDelimited(stream, whisper);
			return whisper;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000978D4 File Offset: 0x00095AD4
		public static Whisper DeserializeLengthDelimited(Stream stream, Whisper instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Whisper.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000978FC File Offset: 0x00095AFC
		public static Whisper Deserialize(Stream stream, Whisper instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Embed == null)
			{
				instance.Embed = new List<EmbedInfo>();
			}
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									instance.Content = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
							{
								if (instance.RecipientId == null)
								{
									instance.RecipientId = AccountId.DeserializeLengthDelimited(stream);
									continue;
								}
								AccountId.DeserializeLengthDelimited(stream, instance.RecipientId);
								continue;
							}
						}
						else
						{
							if (instance.SenderId == null)
							{
								instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
							continue;
						}
					}
					else if (num <= 48)
					{
						if (num == 34)
						{
							instance.Embed.Add(EmbedInfo.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 48)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 61)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 66)
						{
							if (instance.MessageId == null)
							{
								instance.MessageId = MessageId.DeserializeLengthDelimited(stream);
								continue;
							}
							MessageId.DeserializeLengthDelimited(stream, instance.MessageId);
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

		// Token: 0x06002C02 RID: 11266 RVA: 0x00097AA1 File Offset: 0x00095CA1
		public void Serialize(Stream stream)
		{
			Whisper.Serialize(stream, this);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00097AAC File Offset: 0x00095CAC
		public static void Serialize(Stream stream, Whisper instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasRecipientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RecipientId.GetSerializedSize());
				AccountId.Serialize(stream, instance.RecipientId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Embed.Count > 0)
			{
				foreach (EmbedInfo embedInfo in instance.Embed)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, embedInfo.GetSerializedSize());
					EmbedInfo.Serialize(stream, embedInfo);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(61);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasMessageId)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.MessageId.GetSerializedSize());
				MessageId.Serialize(stream, instance.MessageId);
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00097C10 File Offset: 0x00095E10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize = this.SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRecipientId)
			{
				num += 1U;
				uint serializedSize2 = this.RecipientId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasContent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.Embed.Count > 0)
			{
				foreach (EmbedInfo embedInfo in this.Embed)
				{
					num += 1U;
					uint serializedSize3 = embedInfo.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasMessageId)
			{
				num += 1U;
				uint serializedSize4 = this.MessageId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001240 RID: 4672
		public bool HasSenderId;

		// Token: 0x04001241 RID: 4673
		private AccountId _SenderId;

		// Token: 0x04001242 RID: 4674
		public bool HasRecipientId;

		// Token: 0x04001243 RID: 4675
		private AccountId _RecipientId;

		// Token: 0x04001244 RID: 4676
		public bool HasContent;

		// Token: 0x04001245 RID: 4677
		private string _Content;

		// Token: 0x04001246 RID: 4678
		private List<EmbedInfo> _Embed = new List<EmbedInfo>();

		// Token: 0x04001247 RID: 4679
		public bool HasCreationTime;

		// Token: 0x04001248 RID: 4680
		private ulong _CreationTime;

		// Token: 0x04001249 RID: 4681
		public bool HasProgram;

		// Token: 0x0400124A RID: 4682
		private uint _Program;

		// Token: 0x0400124B RID: 4683
		public bool HasMessageId;

		// Token: 0x0400124C RID: 4684
		private MessageId _MessageId;
	}
}
