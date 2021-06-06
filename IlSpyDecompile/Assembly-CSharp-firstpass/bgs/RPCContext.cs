using bnet.protocol;

namespace bgs
{
	public class RPCContext
	{
		private Header header;

		private byte[] payload;

		private RPCContextDelegate callback;

		private bool responseReceived;

		public Header Header
		{
			get
			{
				return header;
			}
			set
			{
				header = value;
			}
		}

		public byte[] Payload
		{
			get
			{
				return payload;
			}
			set
			{
				payload = value;
			}
		}

		public RPCContextDelegate Callback
		{
			get
			{
				return callback;
			}
			set
			{
				callback = value;
			}
		}

		public bool ResponseReceived
		{
			get
			{
				return responseReceived;
			}
			set
			{
				responseReceived = value;
			}
		}

		public IProtoBuf Request { get; set; }

		public int PacketId { get; set; }

		public UtilSystemId SystemId { get; set; }

		public int Context { get; set; }
	}
}
