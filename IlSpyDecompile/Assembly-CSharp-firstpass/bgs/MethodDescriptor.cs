namespace bgs
{
	public class MethodDescriptor
	{
		public delegate IProtoBuf ParseMethod(byte[] bs);

		private string name;

		private uint id;

		private RPCContextDelegate listener;

		private ParseMethod m_parseMethod;

		public string Name => name;

		public uint Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public ParseMethod Parser => m_parseMethod;

		public void RegisterListener(RPCContextDelegate d)
		{
			listener = d;
		}

		public void NotifyListener(RPCContext context)
		{
			if (listener != null)
			{
				listener(context);
			}
		}

		public bool HasListener()
		{
			return listener != null;
		}

		public MethodDescriptor(string n, uint i, ParseMethod parseMethod)
		{
			name = n;
			id = i;
			m_parseMethod = parseMethod;
			if (m_parseMethod == null)
			{
				BattleNet.Log.LogError("MethodDescriptor called with a null method type!");
			}
		}
	}
}
