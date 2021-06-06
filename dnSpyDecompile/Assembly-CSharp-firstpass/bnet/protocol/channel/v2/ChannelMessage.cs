using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047B RID: 1147
	public class ChannelMessage : IProtoBuf
	{
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x000F5C8F File Offset: 0x000F3E8F
		// (set) Token: 0x06004F3A RID: 20282 RVA: 0x000F5C97 File Offset: 0x000F3E97
		public GameAccountHandle AuthorId
		{
			get
			{
				return this._AuthorId;
			}
			set
			{
				this._AuthorId = value;
				this.HasAuthorId = (value != null);
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x000F5CAA File Offset: 0x000F3EAA
		public void SetAuthorId(GameAccountHandle val)
		{
			this.AuthorId = val;
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x000F5CB3 File Offset: 0x000F3EB3
		// (set) Token: 0x06004F3D RID: 20285 RVA: 0x000F5CBB File Offset: 0x000F3EBB
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

		// Token: 0x06004F3E RID: 20286 RVA: 0x000F5CCE File Offset: 0x000F3ECE
		public void SetContent(string val)
		{
			this.Content = val;
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x000F5CD7 File Offset: 0x000F3ED7
		// (set) Token: 0x06004F40 RID: 20288 RVA: 0x000F5CDF File Offset: 0x000F3EDF
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x000F5CD7 File Offset: 0x000F3ED7
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x000F5CE8 File Offset: 0x000F3EE8
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x000F5CF5 File Offset: 0x000F3EF5
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x000F5D03 File Offset: 0x000F3F03
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x000F5D10 File Offset: 0x000F3F10
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x000F5D19 File Offset: 0x000F3F19
		// (set) Token: 0x06004F47 RID: 20295 RVA: 0x000F5D21 File Offset: 0x000F3F21
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

		// Token: 0x06004F48 RID: 20296 RVA: 0x000F5D31 File Offset: 0x000F3F31
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x000F5D3C File Offset: 0x000F3F3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAuthorId)
			{
				num ^= this.AuthorId.GetHashCode();
			}
			if (this.HasContent)
			{
				num ^= this.Content.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x000F5DE4 File Offset: 0x000F3FE4
		public override bool Equals(object obj)
		{
			ChannelMessage channelMessage = obj as ChannelMessage;
			if (channelMessage == null)
			{
				return false;
			}
			if (this.HasAuthorId != channelMessage.HasAuthorId || (this.HasAuthorId && !this.AuthorId.Equals(channelMessage.AuthorId)))
			{
				return false;
			}
			if (this.HasContent != channelMessage.HasContent || (this.HasContent && !this.Content.Equals(channelMessage.Content)))
			{
				return false;
			}
			if (this.Attribute.Count != channelMessage.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelMessage.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasCreationTime == channelMessage.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(channelMessage.CreationTime));
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x000F5ED3 File Offset: 0x000F40D3
		public static ChannelMessage ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelMessage>(bs, 0, -1);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x000F5EDD File Offset: 0x000F40DD
		public void Deserialize(Stream stream)
		{
			ChannelMessage.Deserialize(stream, this);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000F5EE7 File Offset: 0x000F40E7
		public static ChannelMessage Deserialize(Stream stream, ChannelMessage instance)
		{
			return ChannelMessage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x000F5EF4 File Offset: 0x000F40F4
		public static ChannelMessage DeserializeLengthDelimited(Stream stream)
		{
			ChannelMessage channelMessage = new ChannelMessage();
			ChannelMessage.DeserializeLengthDelimited(stream, channelMessage);
			return channelMessage;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x000F5F10 File Offset: 0x000F4110
		public static ChannelMessage DeserializeLengthDelimited(Stream stream, ChannelMessage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelMessage.Deserialize(stream, instance, num);
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x000F5F38 File Offset: 0x000F4138
		public static ChannelMessage Deserialize(Stream stream, ChannelMessage instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
							if (num == 26)
							{
								instance.Content = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.AuthorId == null)
							{
								instance.AuthorId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AuthorId);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 40)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004F52 RID: 20306 RVA: 0x000F603B File Offset: 0x000F423B
		public void Serialize(Stream stream)
		{
			ChannelMessage.Serialize(stream, this);
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x000F6044 File Offset: 0x000F4244
		public static void Serialize(Stream stream, ChannelMessage instance)
		{
			if (instance.HasAuthorId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AuthorId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AuthorId);
			}
			if (instance.HasContent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Content));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x000F6128 File Offset: 0x000F4328
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAuthorId)
			{
				num += 1U;
				uint serializedSize = this.AuthorId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasContent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Content);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			return num;
		}

		// Token: 0x040019A1 RID: 6561
		public bool HasAuthorId;

		// Token: 0x040019A2 RID: 6562
		private GameAccountHandle _AuthorId;

		// Token: 0x040019A3 RID: 6563
		public bool HasContent;

		// Token: 0x040019A4 RID: 6564
		private string _Content;

		// Token: 0x040019A5 RID: 6565
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x040019A6 RID: 6566
		public bool HasCreationTime;

		// Token: 0x040019A7 RID: 6567
		private ulong _CreationTime;
	}
}
