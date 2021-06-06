using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000376 RID: 886
	public class FindGameResponse : IProtoBuf
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000B8A07 File Offset: 0x000B6C07
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x000B8A0F File Offset: 0x000B6C0F
		public ulong RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000B8A1F File Offset: 0x000B6C1F
		public void SetRequestId(ulong val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000B8A28 File Offset: 0x000B6C28
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x000B8A30 File Offset: 0x000B6C30
		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000B8A40 File Offset: 0x000B6C40
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000B8A49 File Offset: 0x000B6C49
		// (set) Token: 0x06003861 RID: 14433 RVA: 0x000B8A51 File Offset: 0x000B6C51
		public bool Queued
		{
			get
			{
				return this._Queued;
			}
			set
			{
				this._Queued = value;
				this.HasQueued = true;
			}
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000B8A61 File Offset: 0x000B6C61
		public void SetQueued(bool val)
		{
			this.Queued = val;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000B8A6C File Offset: 0x000B6C6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasQueued)
			{
				num ^= this.Queued.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000B8AD4 File Offset: 0x000B6CD4
		public override bool Equals(object obj)
		{
			FindGameResponse findGameResponse = obj as FindGameResponse;
			return findGameResponse != null && this.HasRequestId == findGameResponse.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(findGameResponse.RequestId)) && this.HasFactoryId == findGameResponse.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(findGameResponse.FactoryId)) && this.HasQueued == findGameResponse.HasQueued && (!this.HasQueued || this.Queued.Equals(findGameResponse.Queued));
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000B8B78 File Offset: 0x000B6D78
		public static FindGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindGameResponse>(bs, 0, -1);
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000B8B82 File Offset: 0x000B6D82
		public void Deserialize(Stream stream)
		{
			FindGameResponse.Deserialize(stream, this);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000B8B8C File Offset: 0x000B6D8C
		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance)
		{
			return FindGameResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000B8B98 File Offset: 0x000B6D98
		public static FindGameResponse DeserializeLengthDelimited(Stream stream)
		{
			FindGameResponse findGameResponse = new FindGameResponse();
			FindGameResponse.DeserializeLengthDelimited(stream, findGameResponse);
			return findGameResponse;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000B8BB4 File Offset: 0x000B6DB4
		public static FindGameResponse DeserializeLengthDelimited(Stream stream, FindGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindGameResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000B8BDC File Offset: 0x000B6DDC
		public static FindGameResponse Deserialize(Stream stream, FindGameResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Queued = false;
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
				else if (num != 9)
				{
					if (num != 17)
					{
						if (num != 24)
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
							instance.Queued = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.FactoryId = binaryReader.ReadUInt64();
					}
				}
				else
				{
					instance.RequestId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000B8C98 File Offset: 0x000B6E98
		public void Serialize(Stream stream)
		{
			FindGameResponse.Serialize(stream, this);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000B8CA4 File Offset: 0x000B6EA4
		public static void Serialize(Stream stream, FindGameResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRequestId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.RequestId);
			}
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasQueued)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Queued);
			}
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000B8D0C File Offset: 0x000B6F0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasQueued)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040014F4 RID: 5364
		public bool HasRequestId;

		// Token: 0x040014F5 RID: 5365
		private ulong _RequestId;

		// Token: 0x040014F6 RID: 5366
		public bool HasFactoryId;

		// Token: 0x040014F7 RID: 5367
		private ulong _FactoryId;

		// Token: 0x040014F8 RID: 5368
		public bool HasQueued;

		// Token: 0x040014F9 RID: 5369
		private bool _Queued;
	}
}
