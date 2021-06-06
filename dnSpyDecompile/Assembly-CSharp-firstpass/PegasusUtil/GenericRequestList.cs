using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CA RID: 202
	public class GenericRequestList : IProtoBuf
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000331D1 File Offset: 0x000313D1
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x000331D9 File Offset: 0x000313D9
		public List<GenericRequest> Requests
		{
			get
			{
				return this._Requests;
			}
			set
			{
				this._Requests = value;
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000331E4 File Offset: 0x000313E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GenericRequest genericRequest in this.Requests)
			{
				num ^= genericRequest.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00033248 File Offset: 0x00031448
		public override bool Equals(object obj)
		{
			GenericRequestList genericRequestList = obj as GenericRequestList;
			if (genericRequestList == null)
			{
				return false;
			}
			if (this.Requests.Count != genericRequestList.Requests.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Requests.Count; i++)
			{
				if (!this.Requests[i].Equals(genericRequestList.Requests[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000332B3 File Offset: 0x000314B3
		public void Deserialize(Stream stream)
		{
			GenericRequestList.Deserialize(stream, this);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000332BD File Offset: 0x000314BD
		public static GenericRequestList Deserialize(Stream stream, GenericRequestList instance)
		{
			return GenericRequestList.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000332C8 File Offset: 0x000314C8
		public static GenericRequestList DeserializeLengthDelimited(Stream stream)
		{
			GenericRequestList genericRequestList = new GenericRequestList();
			GenericRequestList.DeserializeLengthDelimited(stream, genericRequestList);
			return genericRequestList;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000332E4 File Offset: 0x000314E4
		public static GenericRequestList DeserializeLengthDelimited(Stream stream, GenericRequestList instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericRequestList.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003330C File Offset: 0x0003150C
		public static GenericRequestList Deserialize(Stream stream, GenericRequestList instance, long limit)
		{
			if (instance.Requests == null)
			{
				instance.Requests = new List<GenericRequest>();
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
					instance.Requests.Add(GenericRequest.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000333A4 File Offset: 0x000315A4
		public void Serialize(Stream stream)
		{
			GenericRequestList.Serialize(stream, this);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000333B0 File Offset: 0x000315B0
		public static void Serialize(Stream stream, GenericRequestList instance)
		{
			if (instance.Requests.Count > 0)
			{
				foreach (GenericRequest genericRequest in instance.Requests)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, genericRequest.GetSerializedSize());
					GenericRequest.Serialize(stream, genericRequest);
				}
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00033428 File Offset: 0x00031628
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Requests.Count > 0)
			{
				foreach (GenericRequest genericRequest in this.Requests)
				{
					num += 1U;
					uint serializedSize = genericRequest.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040004B0 RID: 1200
		private List<GenericRequest> _Requests = new List<GenericRequest>();

		// Token: 0x020005D8 RID: 1496
		public enum PacketID
		{
			// Token: 0x04001FD3 RID: 8147
			ID = 327,
			// Token: 0x04001FD4 RID: 8148
			System = 0
		}
	}
}
