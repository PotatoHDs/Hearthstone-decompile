using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000414 RID: 1044
	public class UpdateFriendStateOptions : IProtoBuf
	{
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x000DA79A File Offset: 0x000D899A
		// (set) Token: 0x06004597 RID: 17815 RVA: 0x000DA7A2 File Offset: 0x000D89A2
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

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x000DA79A File Offset: 0x000D899A
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x000DA7AB File Offset: 0x000D89AB
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x000DA7B8 File Offset: 0x000D89B8
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x000DA7C6 File Offset: 0x000D89C6
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x000DA7D3 File Offset: 0x000D89D3
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x000DA7DC File Offset: 0x000D89DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x000DA840 File Offset: 0x000D8A40
		public override bool Equals(object obj)
		{
			UpdateFriendStateOptions updateFriendStateOptions = obj as UpdateFriendStateOptions;
			if (updateFriendStateOptions == null)
			{
				return false;
			}
			if (this.Attribute.Count != updateFriendStateOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(updateFriendStateOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000DA8AB File Offset: 0x000D8AAB
		public static UpdateFriendStateOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateOptions>(bs, 0, -1);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x000DA8B5 File Offset: 0x000D8AB5
		public void Deserialize(Stream stream)
		{
			UpdateFriendStateOptions.Deserialize(stream, this);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x000DA8BF File Offset: 0x000D8ABF
		public static UpdateFriendStateOptions Deserialize(Stream stream, UpdateFriendStateOptions instance)
		{
			return UpdateFriendStateOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x000DA8CC File Offset: 0x000D8ACC
		public static UpdateFriendStateOptions DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateOptions updateFriendStateOptions = new UpdateFriendStateOptions();
			UpdateFriendStateOptions.DeserializeLengthDelimited(stream, updateFriendStateOptions);
			return updateFriendStateOptions;
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000DA8E8 File Offset: 0x000D8AE8
		public static UpdateFriendStateOptions DeserializeLengthDelimited(Stream stream, UpdateFriendStateOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000DA910 File Offset: 0x000D8B10
		public static UpdateFriendStateOptions Deserialize(Stream stream, UpdateFriendStateOptions instance, long limit)
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
				else if (num == 10)
				{
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
				}
				else
				{
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

		// Token: 0x060045A6 RID: 17830 RVA: 0x000DA9A8 File Offset: 0x000D8BA8
		public void Serialize(Stream stream)
		{
			UpdateFriendStateOptions.Serialize(stream, this);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x000DA9B4 File Offset: 0x000D8BB4
		public static void Serialize(Stream stream, UpdateFriendStateOptions instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x000DAA2C File Offset: 0x000D8C2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001769 RID: 5993
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
