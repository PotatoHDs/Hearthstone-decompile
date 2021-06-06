using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C8 RID: 712
	public class RemoveAllFromChannelRequest : IProtoBuf
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x00091D00 File Offset: 0x0008FF00
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x00091D08 File Offset: 0x0008FF08
		public List<string> JoinToken
		{
			get
			{
				return this._JoinToken;
			}
			set
			{
				this._JoinToken = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00091D00 File Offset: 0x0008FF00
		public List<string> JoinTokenList
		{
			get
			{
				return this._JoinToken;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x00091D11 File Offset: 0x0008FF11
		public int JoinTokenCount
		{
			get
			{
				return this._JoinToken.Count;
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00091D1E File Offset: 0x0008FF1E
		public void AddJoinToken(string val)
		{
			this._JoinToken.Add(val);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00091D2C File Offset: 0x0008FF2C
		public void ClearJoinToken()
		{
			this._JoinToken.Clear();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00091D39 File Offset: 0x0008FF39
		public void SetJoinToken(List<string> val)
		{
			this.JoinToken = val;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00091D42 File Offset: 0x0008FF42
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x00091D4A File Offset: 0x0008FF4A
		public string ChannelUri
		{
			get
			{
				return this._ChannelUri;
			}
			set
			{
				this._ChannelUri = value;
				this.HasChannelUri = (value != null);
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00091D5D File Offset: 0x0008FF5D
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00091D68 File Offset: 0x0008FF68
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (string text in this.JoinToken)
			{
				num ^= text.GetHashCode();
			}
			if (this.HasChannelUri)
			{
				num ^= this.ChannelUri.GetHashCode();
			}
			return num;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00091DE0 File Offset: 0x0008FFE0
		public override bool Equals(object obj)
		{
			RemoveAllFromChannelRequest removeAllFromChannelRequest = obj as RemoveAllFromChannelRequest;
			if (removeAllFromChannelRequest == null)
			{
				return false;
			}
			if (this.JoinToken.Count != removeAllFromChannelRequest.JoinToken.Count)
			{
				return false;
			}
			for (int i = 0; i < this.JoinToken.Count; i++)
			{
				if (!this.JoinToken[i].Equals(removeAllFromChannelRequest.JoinToken[i]))
				{
					return false;
				}
			}
			return this.HasChannelUri == removeAllFromChannelRequest.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(removeAllFromChannelRequest.ChannelUri));
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00091E76 File Offset: 0x00090076
		public static RemoveAllFromChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveAllFromChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00091E80 File Offset: 0x00090080
		public void Deserialize(Stream stream)
		{
			RemoveAllFromChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x00091E8A File Offset: 0x0009008A
		public static RemoveAllFromChannelRequest Deserialize(Stream stream, RemoveAllFromChannelRequest instance)
		{
			return RemoveAllFromChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00091E98 File Offset: 0x00090098
		public static RemoveAllFromChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveAllFromChannelRequest removeAllFromChannelRequest = new RemoveAllFromChannelRequest();
			RemoveAllFromChannelRequest.DeserializeLengthDelimited(stream, removeAllFromChannelRequest);
			return removeAllFromChannelRequest;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00091EB4 File Offset: 0x000900B4
		public static RemoveAllFromChannelRequest DeserializeLengthDelimited(Stream stream, RemoveAllFromChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveAllFromChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00091EDC File Offset: 0x000900DC
		public static RemoveAllFromChannelRequest Deserialize(Stream stream, RemoveAllFromChannelRequest instance, long limit)
		{
			if (instance.JoinToken == null)
			{
				instance.JoinToken = new List<string>();
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
					if (num != 18)
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
						instance.ChannelUri = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.JoinToken.Add(ProtocolParser.ReadString(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00091F8C File Offset: 0x0009018C
		public void Serialize(Stream stream)
		{
			RemoveAllFromChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00091F98 File Offset: 0x00090198
		public static void Serialize(Stream stream, RemoveAllFromChannelRequest instance)
		{
			if (instance.JoinToken.Count > 0)
			{
				foreach (string s in instance.JoinToken)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.HasChannelUri)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00092034 File Offset: 0x00090234
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.JoinToken.Count > 0)
			{
				foreach (string s in this.JoinToken)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (this.HasChannelUri)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x040011D0 RID: 4560
		private List<string> _JoinToken = new List<string>();

		// Token: 0x040011D1 RID: 4561
		public bool HasChannelUri;

		// Token: 0x040011D2 RID: 4562
		private string _ChannelUri;
	}
}
