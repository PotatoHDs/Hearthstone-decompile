using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033D RID: 829
	public class SubscribeResult : IProtoBuf
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000AB691 File Offset: 0x000A9891
		// (set) Token: 0x0600335C RID: 13148 RVA: 0x000AB699 File Offset: 0x000A9899
		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000AB6AC File Offset: 0x000A98AC
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000AB6B5 File Offset: 0x000A98B5
		// (set) Token: 0x0600335F RID: 13151 RVA: 0x000AB6BD File Offset: 0x000A98BD
		public uint Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000AB6CD File Offset: 0x000A98CD
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000AB6D8 File Offset: 0x000A98D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000AB724 File Offset: 0x000A9924
		public override bool Equals(object obj)
		{
			SubscribeResult subscribeResult = obj as SubscribeResult;
			return subscribeResult != null && this.HasEntityId == subscribeResult.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(subscribeResult.EntityId)) && this.HasResult == subscribeResult.HasResult && (!this.HasResult || this.Result.Equals(subscribeResult.Result));
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000AB797 File Offset: 0x000A9997
		public static SubscribeResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResult>(bs, 0, -1);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000AB7A1 File Offset: 0x000A99A1
		public void Deserialize(Stream stream)
		{
			SubscribeResult.Deserialize(stream, this);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000AB7AB File Offset: 0x000A99AB
		public static SubscribeResult Deserialize(Stream stream, SubscribeResult instance)
		{
			return SubscribeResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000AB7B8 File Offset: 0x000A99B8
		public static SubscribeResult DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResult subscribeResult = new SubscribeResult();
			SubscribeResult.DeserializeLengthDelimited(stream, subscribeResult);
			return subscribeResult;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000AB7D4 File Offset: 0x000A99D4
		public static SubscribeResult DeserializeLengthDelimited(Stream stream, SubscribeResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResult.Deserialize(stream, instance, num);
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000AB7FC File Offset: 0x000A99FC
		public static SubscribeResult Deserialize(Stream stream, SubscribeResult instance, long limit)
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
					if (num != 16)
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
						instance.Result = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000AB8AE File Offset: 0x000A9AAE
		public void Serialize(Stream stream)
		{
			SubscribeResult.Serialize(stream, this);
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000AB8B8 File Offset: 0x000A9AB8
		public static void Serialize(Stream stream, SubscribeResult instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000AB910 File Offset: 0x000A9B10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			return num;
		}

		// Token: 0x040013FD RID: 5117
		public bool HasEntityId;

		// Token: 0x040013FE RID: 5118
		private EntityId _EntityId;

		// Token: 0x040013FF RID: 5119
		public bool HasResult;

		// Token: 0x04001400 RID: 5120
		private uint _Result;
	}
}
