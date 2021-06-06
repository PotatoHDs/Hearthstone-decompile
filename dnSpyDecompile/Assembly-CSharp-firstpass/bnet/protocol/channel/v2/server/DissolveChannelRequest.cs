using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000495 RID: 1173
	public class DissolveChannelRequest : IProtoBuf
	{
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x000FD34F File Offset: 0x000FB54F
		// (set) Token: 0x060051B4 RID: 20916 RVA: 0x000FD357 File Offset: 0x000FB557
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x000FD36A File Offset: 0x000FB56A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x000FD374 File Offset: 0x000FB574
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x000FD3A4 File Offset: 0x000FB5A4
		public override bool Equals(object obj)
		{
			DissolveChannelRequest dissolveChannelRequest = obj as DissolveChannelRequest;
			return dissolveChannelRequest != null && this.HasChannelId == dissolveChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(dissolveChannelRequest.ChannelId));
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x000FD3E9 File Offset: 0x000FB5E9
		public static DissolveChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DissolveChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x000FD3F3 File Offset: 0x000FB5F3
		public void Deserialize(Stream stream)
		{
			DissolveChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x000FD3FD File Offset: 0x000FB5FD
		public static DissolveChannelRequest Deserialize(Stream stream, DissolveChannelRequest instance)
		{
			return DissolveChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x000FD408 File Offset: 0x000FB608
		public static DissolveChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			DissolveChannelRequest dissolveChannelRequest = new DissolveChannelRequest();
			DissolveChannelRequest.DeserializeLengthDelimited(stream, dissolveChannelRequest);
			return dissolveChannelRequest;
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x000FD424 File Offset: 0x000FB624
		public static DissolveChannelRequest DeserializeLengthDelimited(Stream stream, DissolveChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DissolveChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x000FD44C File Offset: 0x000FB64C
		public static DissolveChannelRequest Deserialize(Stream stream, DissolveChannelRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
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

		// Token: 0x060051BF RID: 20927 RVA: 0x000FD4E6 File Offset: 0x000FB6E6
		public void Serialize(Stream stream)
		{
			DissolveChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x000FD4EF File Offset: 0x000FB6EF
		public static void Serialize(Stream stream, DissolveChannelRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x000FD520 File Offset: 0x000FB720
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001A3F RID: 6719
		public bool HasChannelId;

		// Token: 0x04001A40 RID: 6720
		private ChannelId _ChannelId;
	}
}
