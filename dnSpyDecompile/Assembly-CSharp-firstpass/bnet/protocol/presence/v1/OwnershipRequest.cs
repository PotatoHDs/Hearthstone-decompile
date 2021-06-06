using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033B RID: 827
	public class OwnershipRequest : IProtoBuf
	{
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000AAB7B File Offset: 0x000A8D7B
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x000AAB83 File Offset: 0x000A8D83
		public EntityId EntityId { get; set; }

		// Token: 0x06003322 RID: 13090 RVA: 0x000AAB8C File Offset: 0x000A8D8C
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000AAB95 File Offset: 0x000A8D95
		// (set) Token: 0x06003324 RID: 13092 RVA: 0x000AAB9D File Offset: 0x000A8D9D
		public bool ReleaseOwnership
		{
			get
			{
				return this._ReleaseOwnership;
			}
			set
			{
				this._ReleaseOwnership = value;
				this.HasReleaseOwnership = true;
			}
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000AABAD File Offset: 0x000A8DAD
		public void SetReleaseOwnership(bool val)
		{
			this.ReleaseOwnership = val;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000AABB8 File Offset: 0x000A8DB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			if (this.HasReleaseOwnership)
			{
				num ^= this.ReleaseOwnership.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000AABFC File Offset: 0x000A8DFC
		public override bool Equals(object obj)
		{
			OwnershipRequest ownershipRequest = obj as OwnershipRequest;
			return ownershipRequest != null && this.EntityId.Equals(ownershipRequest.EntityId) && this.HasReleaseOwnership == ownershipRequest.HasReleaseOwnership && (!this.HasReleaseOwnership || this.ReleaseOwnership.Equals(ownershipRequest.ReleaseOwnership));
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000AAC59 File Offset: 0x000A8E59
		public static OwnershipRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<OwnershipRequest>(bs, 0, -1);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000AAC63 File Offset: 0x000A8E63
		public void Deserialize(Stream stream)
		{
			OwnershipRequest.Deserialize(stream, this);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000AAC6D File Offset: 0x000A8E6D
		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance)
		{
			return OwnershipRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000AAC78 File Offset: 0x000A8E78
		public static OwnershipRequest DeserializeLengthDelimited(Stream stream)
		{
			OwnershipRequest ownershipRequest = new OwnershipRequest();
			OwnershipRequest.DeserializeLengthDelimited(stream, ownershipRequest);
			return ownershipRequest;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000AAC94 File Offset: 0x000A8E94
		public static OwnershipRequest DeserializeLengthDelimited(Stream stream, OwnershipRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return OwnershipRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000AACBC File Offset: 0x000A8EBC
		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance, long limit)
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
						instance.ReleaseOwnership = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600332F RID: 13103 RVA: 0x000AAD6E File Offset: 0x000A8F6E
		public void Serialize(Stream stream)
		{
			OwnershipRequest.Serialize(stream, this);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000AAD78 File Offset: 0x000A8F78
		public static void Serialize(Stream stream, OwnershipRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasReleaseOwnership)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ReleaseOwnership);
			}
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000AADE0 File Offset: 0x000A8FE0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReleaseOwnership)
			{
				num += 1U;
				num += 1U;
			}
			return num + 1U;
		}

		// Token: 0x040013F4 RID: 5108
		public bool HasReleaseOwnership;

		// Token: 0x040013F5 RID: 5109
		private bool _ReleaseOwnership;
	}
}
