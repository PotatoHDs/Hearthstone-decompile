using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002DD RID: 733
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x0009597B File Offset: 0x00093B7B
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x00095983 File Offset: 0x00093B83
		public List<WhisperView> View
		{
			get
			{
				return this._View;
			}
			set
			{
				this._View = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0009597B File Offset: 0x00093B7B
		public List<WhisperView> ViewList
		{
			get
			{
				return this._View;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0009598C File Offset: 0x00093B8C
		public int ViewCount
		{
			get
			{
				return this._View.Count;
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x00095999 File Offset: 0x00093B99
		public void AddView(WhisperView val)
		{
			this._View.Add(val);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000959A7 File Offset: 0x00093BA7
		public void ClearView()
		{
			this._View.Clear();
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000959B4 File Offset: 0x00093BB4
		public void SetView(List<WhisperView> val)
		{
			this.View = val;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000959C0 File Offset: 0x00093BC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (WhisperView whisperView in this.View)
			{
				num ^= whisperView.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00095A24 File Offset: 0x00093C24
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (this.View.Count != subscribeResponse.View.Count)
			{
				return false;
			}
			for (int i = 0; i < this.View.Count; i++)
			{
				if (!this.View[i].Equals(subscribeResponse.View[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00095A8F File Offset: 0x00093C8F
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00095A99 File Offset: 0x00093C99
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00095AA3 File Offset: 0x00093CA3
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00095AB0 File Offset: 0x00093CB0
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00095ACC File Offset: 0x00093CCC
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00095AF4 File Offset: 0x00093CF4
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.View == null)
			{
				instance.View = new List<WhisperView>();
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
					instance.View.Add(WhisperView.DeserializeLengthDelimited(stream));
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

		// Token: 0x06002B40 RID: 11072 RVA: 0x00095B8C File Offset: 0x00093D8C
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00095B98 File Offset: 0x00093D98
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.View.Count > 0)
			{
				foreach (WhisperView whisperView in instance.View)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, whisperView.GetSerializedSize());
					WhisperView.Serialize(stream, whisperView);
				}
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00095C10 File Offset: 0x00093E10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.View.Count > 0)
			{
				foreach (WhisperView whisperView in this.View)
				{
					num += 1U;
					uint serializedSize = whisperView.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001220 RID: 4640
		private List<WhisperView> _View = new List<WhisperView>();
	}
}
