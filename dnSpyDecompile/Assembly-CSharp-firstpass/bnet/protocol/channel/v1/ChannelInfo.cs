using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D5 RID: 1237
	public class ChannelInfo : IProtoBuf
	{
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x0010B19C File Offset: 0x0010939C
		// (set) Token: 0x06005712 RID: 22290 RVA: 0x0010B1A4 File Offset: 0x001093A4
		public ChannelDescription Description { get; set; }

		// Token: 0x06005713 RID: 22291 RVA: 0x0010B1AD File Offset: 0x001093AD
		public void SetDescription(ChannelDescription val)
		{
			this.Description = val;
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x0010B1B6 File Offset: 0x001093B6
		// (set) Token: 0x06005715 RID: 22293 RVA: 0x0010B1BE File Offset: 0x001093BE
		public List<Member> Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06005716 RID: 22294 RVA: 0x0010B1B6 File Offset: 0x001093B6
		public List<Member> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06005717 RID: 22295 RVA: 0x0010B1C7 File Offset: 0x001093C7
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x0010B1D4 File Offset: 0x001093D4
		public void AddMember(Member val)
		{
			this._Member.Add(val);
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x0010B1E2 File Offset: 0x001093E2
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x0010B1EF File Offset: 0x001093EF
		public void SetMember(List<Member> val)
		{
			this.Member = val;
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x0010B1F8 File Offset: 0x001093F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Description.GetHashCode();
			foreach (Member member in this.Member)
			{
				num ^= member.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x0010B268 File Offset: 0x00109468
		public override bool Equals(object obj)
		{
			ChannelInfo channelInfo = obj as ChannelInfo;
			if (channelInfo == null)
			{
				return false;
			}
			if (!this.Description.Equals(channelInfo.Description))
			{
				return false;
			}
			if (this.Member.Count != channelInfo.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Member.Count; i++)
			{
				if (!this.Member[i].Equals(channelInfo.Member[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x0010B2E8 File Offset: 0x001094E8
		public static ChannelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInfo>(bs, 0, -1);
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x0010B2F2 File Offset: 0x001094F2
		public void Deserialize(Stream stream)
		{
			ChannelInfo.Deserialize(stream, this);
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x0010B2FC File Offset: 0x001094FC
		public static ChannelInfo Deserialize(Stream stream, ChannelInfo instance)
		{
			return ChannelInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x0010B308 File Offset: 0x00109508
		public static ChannelInfo DeserializeLengthDelimited(Stream stream)
		{
			ChannelInfo channelInfo = new ChannelInfo();
			ChannelInfo.DeserializeLengthDelimited(stream, channelInfo);
			return channelInfo;
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0010B324 File Offset: 0x00109524
		public static ChannelInfo DeserializeLengthDelimited(Stream stream, ChannelInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x0010B34C File Offset: 0x0010954C
		public static ChannelInfo Deserialize(Stream stream, ChannelInfo instance, long limit)
		{
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
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
						instance.Member.Add(bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.Description == null)
				{
					instance.Description = ChannelDescription.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelDescription.DeserializeLengthDelimited(stream, instance.Description);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x0010B416 File Offset: 0x00109616
		public void Serialize(Stream stream)
		{
			ChannelInfo.Serialize(stream, this);
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x0010B420 File Offset: 0x00109620
		public static void Serialize(Stream stream, ChannelInfo instance)
		{
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Description.GetSerializedSize());
			ChannelDescription.Serialize(stream, instance.Description);
			if (instance.Member.Count > 0)
			{
				foreach (Member member in instance.Member)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					bnet.protocol.channel.v1.Member.Serialize(stream, member);
				}
			}
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0010B4D4 File Offset: 0x001096D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Description.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Member.Count > 0)
			{
				foreach (Member member in this.Member)
				{
					num += 1U;
					uint serializedSize2 = member.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001B64 RID: 7012
		private List<Member> _Member = new List<Member>();
	}
}
