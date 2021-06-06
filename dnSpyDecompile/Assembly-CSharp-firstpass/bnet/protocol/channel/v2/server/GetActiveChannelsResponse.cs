using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000499 RID: 1177
	public class GetActiveChannelsResponse : IProtoBuf
	{
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x000FE091 File Offset: 0x000FC291
		// (set) Token: 0x06005209 RID: 21001 RVA: 0x000FE099 File Offset: 0x000FC299
		public List<ActiveChannelDescription> Channel
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

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x000FE091 File Offset: 0x000FC291
		public List<ActiveChannelDescription> ChannelList
		{
			get
			{
				return this._Channel;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x000FE0A2 File Offset: 0x000FC2A2
		public int ChannelCount
		{
			get
			{
				return this._Channel.Count;
			}
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x000FE0AF File Offset: 0x000FC2AF
		public void AddChannel(ActiveChannelDescription val)
		{
			this._Channel.Add(val);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x000FE0BD File Offset: 0x000FC2BD
		public void ClearChannel()
		{
			this._Channel.Clear();
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x000FE0CA File Offset: 0x000FC2CA
		public void SetChannel(List<ActiveChannelDescription> val)
		{
			this.Channel = val;
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x000FE0D3 File Offset: 0x000FC2D3
		// (set) Token: 0x06005210 RID: 21008 RVA: 0x000FE0DB File Offset: 0x000FC2DB
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

		// Token: 0x06005211 RID: 21009 RVA: 0x000FE0EB File Offset: 0x000FC2EB
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x000FE0F4 File Offset: 0x000FC2F4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ActiveChannelDescription activeChannelDescription in this.Channel)
			{
				num ^= activeChannelDescription.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x000FE170 File Offset: 0x000FC370
		public override bool Equals(object obj)
		{
			GetActiveChannelsResponse getActiveChannelsResponse = obj as GetActiveChannelsResponse;
			if (getActiveChannelsResponse == null)
			{
				return false;
			}
			if (this.Channel.Count != getActiveChannelsResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Channel.Count; i++)
			{
				if (!this.Channel[i].Equals(getActiveChannelsResponse.Channel[i]))
				{
					return false;
				}
			}
			return this.HasContinuation == getActiveChannelsResponse.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getActiveChannelsResponse.Continuation));
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x000FE209 File Offset: 0x000FC409
		public static GetActiveChannelsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetActiveChannelsResponse>(bs, 0, -1);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x000FE213 File Offset: 0x000FC413
		public void Deserialize(Stream stream)
		{
			GetActiveChannelsResponse.Deserialize(stream, this);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x000FE21D File Offset: 0x000FC41D
		public static GetActiveChannelsResponse Deserialize(Stream stream, GetActiveChannelsResponse instance)
		{
			return GetActiveChannelsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x000FE228 File Offset: 0x000FC428
		public static GetActiveChannelsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetActiveChannelsResponse getActiveChannelsResponse = new GetActiveChannelsResponse();
			GetActiveChannelsResponse.DeserializeLengthDelimited(stream, getActiveChannelsResponse);
			return getActiveChannelsResponse;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x000FE244 File Offset: 0x000FC444
		public static GetActiveChannelsResponse DeserializeLengthDelimited(Stream stream, GetActiveChannelsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetActiveChannelsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x000FE26C File Offset: 0x000FC46C
		public static GetActiveChannelsResponse Deserialize(Stream stream, GetActiveChannelsResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ActiveChannelDescription>();
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
					instance.Channel.Add(ActiveChannelDescription.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x000FE31C File Offset: 0x000FC51C
		public void Serialize(Stream stream)
		{
			GetActiveChannelsResponse.Serialize(stream, this);
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x000FE328 File Offset: 0x000FC528
		public static void Serialize(Stream stream, GetActiveChannelsResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (ActiveChannelDescription activeChannelDescription in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, activeChannelDescription.GetSerializedSize());
					ActiveChannelDescription.Serialize(stream, activeChannelDescription);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x000FE3BC File Offset: 0x000FC5BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Channel.Count > 0)
			{
				foreach (ActiveChannelDescription activeChannelDescription in this.Channel)
				{
					num += 1U;
					uint serializedSize = activeChannelDescription.GetSerializedSize();
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

		// Token: 0x04001A55 RID: 6741
		private List<ActiveChannelDescription> _Channel = new List<ActiveChannelDescription>();

		// Token: 0x04001A56 RID: 6742
		public bool HasContinuation;

		// Token: 0x04001A57 RID: 6743
		private ulong _Continuation;
	}
}
