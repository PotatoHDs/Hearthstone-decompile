using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D5 RID: 981
	public class GameMatchmakerFilter : IProtoBuf
	{
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000CD37C File Offset: 0x000CB57C
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x000CD384 File Offset: 0x000CB584
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

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000CD37C File Offset: 0x000CB57C
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x000CD38D File Offset: 0x000CB58D
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000CD39A File Offset: 0x000CB59A
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000CD3A8 File Offset: 0x000CB5A8
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000CD3B5 File Offset: 0x000CB5B5
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000CD424 File Offset: 0x000CB624
		public override bool Equals(object obj)
		{
			GameMatchmakerFilter gameMatchmakerFilter = obj as GameMatchmakerFilter;
			if (gameMatchmakerFilter == null)
			{
				return false;
			}
			if (this.Attribute.Count != gameMatchmakerFilter.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(gameMatchmakerFilter.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000CD48F File Offset: 0x000CB68F
		public static GameMatchmakerFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameMatchmakerFilter>(bs, 0, -1);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000CD499 File Offset: 0x000CB699
		public void Deserialize(Stream stream)
		{
			GameMatchmakerFilter.Deserialize(stream, this);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000CD4A3 File Offset: 0x000CB6A3
		public static GameMatchmakerFilter Deserialize(Stream stream, GameMatchmakerFilter instance)
		{
			return GameMatchmakerFilter.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000CD4B0 File Offset: 0x000CB6B0
		public static GameMatchmakerFilter DeserializeLengthDelimited(Stream stream)
		{
			GameMatchmakerFilter gameMatchmakerFilter = new GameMatchmakerFilter();
			GameMatchmakerFilter.DeserializeLengthDelimited(stream, gameMatchmakerFilter);
			return gameMatchmakerFilter;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000CD4CC File Offset: 0x000CB6CC
		public static GameMatchmakerFilter DeserializeLengthDelimited(Stream stream, GameMatchmakerFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameMatchmakerFilter.Deserialize(stream, instance, num);
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000CD4F4 File Offset: 0x000CB6F4
		public static GameMatchmakerFilter Deserialize(Stream stream, GameMatchmakerFilter instance, long limit)
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

		// Token: 0x0600406C RID: 16492 RVA: 0x000CD58C File Offset: 0x000CB78C
		public void Serialize(Stream stream)
		{
			GameMatchmakerFilter.Serialize(stream, this);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000CD598 File Offset: 0x000CB798
		public static void Serialize(Stream stream, GameMatchmakerFilter instance)
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

		// Token: 0x0600406E RID: 16494 RVA: 0x000CD610 File Offset: 0x000CB810
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

		// Token: 0x04001672 RID: 5746
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
