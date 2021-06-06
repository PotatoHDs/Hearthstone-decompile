using System;
using System.Net;

namespace bgs
{
	public class UriUtils
	{
		public static bool GetHostAddressAsIp(string hostName, out string address)
		{
			address = "";
			if (IPAddress.TryParse(hostName, out var address2))
			{
				address = address2.ToString();
				return true;
			}
			return false;
		}

		public static bool GetHostAddressByDns(string hostName, out string address)
		{
			address = "";
			try
			{
				IPAddress[] addressList = Dns.GetHostEntry(hostName).AddressList;
				int num = 0;
				if (num < addressList.Length)
				{
					IPAddress iPAddress = addressList[num];
					address = iPAddress.ToString();
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}

		public static bool GetHostAddress(string hostName, out string address)
		{
			if (GetHostAddressAsIp(hostName, out address))
			{
				return true;
			}
			try
			{
				if (GetHostAddressByDns(hostName, out address))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}
	}
}
