using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000374 RID: 884
	public class ListFactoriesResponse : IProtoBuf
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000B80C3 File Offset: 0x000B62C3
		// (set) Token: 0x06003827 RID: 14375 RVA: 0x000B80CB File Offset: 0x000B62CB
		public List<GameFactoryDescription> Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000B80C3 File Offset: 0x000B62C3
		public List<GameFactoryDescription> DescriptionList
		{
			get
			{
				return this._Description;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000B80D4 File Offset: 0x000B62D4
		public int DescriptionCount
		{
			get
			{
				return this._Description.Count;
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000B80E1 File Offset: 0x000B62E1
		public void AddDescription(GameFactoryDescription val)
		{
			this._Description.Add(val);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000B80EF File Offset: 0x000B62EF
		public void ClearDescription()
		{
			this._Description.Clear();
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000B80FC File Offset: 0x000B62FC
		public void SetDescription(List<GameFactoryDescription> val)
		{
			this.Description = val;
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x000B8105 File Offset: 0x000B6305
		// (set) Token: 0x0600382E RID: 14382 RVA: 0x000B810D File Offset: 0x000B630D
		public uint TotalResults
		{
			get
			{
				return this._TotalResults;
			}
			set
			{
				this._TotalResults = value;
				this.HasTotalResults = true;
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000B811D File Offset: 0x000B631D
		public void SetTotalResults(uint val)
		{
			this.TotalResults = val;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000B8128 File Offset: 0x000B6328
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameFactoryDescription gameFactoryDescription in this.Description)
			{
				num ^= gameFactoryDescription.GetHashCode();
			}
			if (this.HasTotalResults)
			{
				num ^= this.TotalResults.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000B81A4 File Offset: 0x000B63A4
		public override bool Equals(object obj)
		{
			ListFactoriesResponse listFactoriesResponse = obj as ListFactoriesResponse;
			if (listFactoriesResponse == null)
			{
				return false;
			}
			if (this.Description.Count != listFactoriesResponse.Description.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Description.Count; i++)
			{
				if (!this.Description[i].Equals(listFactoriesResponse.Description[i]))
				{
					return false;
				}
			}
			return this.HasTotalResults == listFactoriesResponse.HasTotalResults && (!this.HasTotalResults || this.TotalResults.Equals(listFactoriesResponse.TotalResults));
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000B823D File Offset: 0x000B643D
		public static ListFactoriesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesResponse>(bs, 0, -1);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000B8247 File Offset: 0x000B6447
		public void Deserialize(Stream stream)
		{
			ListFactoriesResponse.Deserialize(stream, this);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000B8251 File Offset: 0x000B6451
		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance)
		{
			return ListFactoriesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000B825C File Offset: 0x000B645C
		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesResponse listFactoriesResponse = new ListFactoriesResponse();
			ListFactoriesResponse.DeserializeLengthDelimited(stream, listFactoriesResponse);
			return listFactoriesResponse;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000B8278 File Offset: 0x000B6478
		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream, ListFactoriesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListFactoriesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000B82A0 File Offset: 0x000B64A0
		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance, long limit)
		{
			if (instance.Description == null)
			{
				instance.Description = new List<GameFactoryDescription>();
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
						instance.TotalResults = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Description.Add(GameFactoryDescription.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000B8350 File Offset: 0x000B6550
		public void Serialize(Stream stream)
		{
			ListFactoriesResponse.Serialize(stream, this);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000B835C File Offset: 0x000B655C
		public static void Serialize(Stream stream, ListFactoriesResponse instance)
		{
			if (instance.Description.Count > 0)
			{
				foreach (GameFactoryDescription gameFactoryDescription in instance.Description)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameFactoryDescription.GetSerializedSize());
					GameFactoryDescription.Serialize(stream, gameFactoryDescription);
				}
			}
			if (instance.HasTotalResults)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.TotalResults);
			}
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000B83F0 File Offset: 0x000B65F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Description.Count > 0)
			{
				foreach (GameFactoryDescription gameFactoryDescription in this.Description)
				{
					num += 1U;
					uint serializedSize = gameFactoryDescription.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasTotalResults)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.TotalResults);
			}
			return num;
		}

		// Token: 0x040014EA RID: 5354
		private List<GameFactoryDescription> _Description = new List<GameFactoryDescription>();

		// Token: 0x040014EB RID: 5355
		public bool HasTotalResults;

		// Token: 0x040014EC RID: 5356
		private uint _TotalResults;
	}
}
