using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044F RID: 1103
	public class GetPublicChannelTypesResponse : IProtoBuf
	{
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x000EA4D4 File Offset: 0x000E86D4
		// (set) Token: 0x06004B3B RID: 19259 RVA: 0x000EA4DC File Offset: 0x000E86DC
		public List<PublicChannelType> Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				this._Channel = value;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x000EA4D4 File Offset: 0x000E86D4
		public List<PublicChannelType> ChannelList
		{
			get
			{
				return this._Channel;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x000EA4E5 File Offset: 0x000E86E5
		public int ChannelCount
		{
			get
			{
				return this._Channel.Count;
			}
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x000EA4F2 File Offset: 0x000E86F2
		public void AddChannel(PublicChannelType val)
		{
			this._Channel.Add(val);
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x000EA500 File Offset: 0x000E8700
		public void ClearChannel()
		{
			this._Channel.Clear();
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x000EA50D File Offset: 0x000E870D
		public void SetChannel(List<PublicChannelType> val)
		{
			this.Channel = val;
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x000EA516 File Offset: 0x000E8716
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x000EA51E File Offset: 0x000E871E
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x000EA52E File Offset: 0x000E872E
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x000EA538 File Offset: 0x000E8738
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PublicChannelType publicChannelType in this.Channel)
			{
				num ^= publicChannelType.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x000EA5B4 File Offset: 0x000E87B4
		public override bool Equals(object obj)
		{
			GetPublicChannelTypesResponse getPublicChannelTypesResponse = obj as GetPublicChannelTypesResponse;
			if (getPublicChannelTypesResponse == null)
			{
				return false;
			}
			if (this.Channel.Count != getPublicChannelTypesResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Channel.Count; i++)
			{
				if (!this.Channel[i].Equals(getPublicChannelTypesResponse.Channel[i]))
				{
					return false;
				}
			}
			return this.HasContinuation == getPublicChannelTypesResponse.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getPublicChannelTypesResponse.Continuation));
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x000EA64D File Offset: 0x000E884D
		public static GetPublicChannelTypesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesResponse>(bs, 0, -1);
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x000EA657 File Offset: 0x000E8857
		public void Deserialize(Stream stream)
		{
			GetPublicChannelTypesResponse.Deserialize(stream, this);
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x000EA661 File Offset: 0x000E8861
		public static GetPublicChannelTypesResponse Deserialize(Stream stream, GetPublicChannelTypesResponse instance)
		{
			return GetPublicChannelTypesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x000EA66C File Offset: 0x000E886C
		public static GetPublicChannelTypesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesResponse getPublicChannelTypesResponse = new GetPublicChannelTypesResponse();
			GetPublicChannelTypesResponse.DeserializeLengthDelimited(stream, getPublicChannelTypesResponse);
			return getPublicChannelTypesResponse;
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x000EA688 File Offset: 0x000E8888
		public static GetPublicChannelTypesResponse DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPublicChannelTypesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x000EA6B0 File Offset: 0x000E88B0
		public static GetPublicChannelTypesResponse Deserialize(Stream stream, GetPublicChannelTypesResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<PublicChannelType>();
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
						instance.Continuation = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Channel.Add(PublicChannelType.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x000EA760 File Offset: 0x000E8960
		public void Serialize(Stream stream)
		{
			GetPublicChannelTypesResponse.Serialize(stream, this);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x000EA76C File Offset: 0x000E896C
		public static void Serialize(Stream stream, GetPublicChannelTypesResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (PublicChannelType publicChannelType in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, publicChannelType.GetSerializedSize());
					PublicChannelType.Serialize(stream, publicChannelType);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x000EA800 File Offset: 0x000E8A00
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Channel.Count > 0)
			{
				foreach (PublicChannelType publicChannelType in this.Channel)
				{
					num += 1U;
					uint serializedSize = publicChannelType.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x040018A4 RID: 6308
		private List<PublicChannelType> _Channel = new List<PublicChannelType>();

		// Token: 0x040018A5 RID: 6309
		public bool HasContinuation;

		// Token: 0x040018A6 RID: 6310
		private ulong _Continuation;
	}
}
