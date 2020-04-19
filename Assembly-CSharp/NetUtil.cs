﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class NetUtil
{
	private static Dictionary<string, IPAddress> s_hostCache = new Dictionary<string, IPAddress>();

	private static string s_hostName;

	public static string GetHostName()
	{
		if (!NetUtil.s_hostName.IsNullOrEmpty())
		{
			for (;;)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(NetUtil.GetHostName()).MethodHandle;
			}
			return NetUtil.s_hostName;
		}
		try
		{
			NetUtil.s_hostName = Dns.GetHostName();
			if (NetUtil.s_hostName == null)
			{
				for (;;)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				throw new Exception("GetHostName() returned null");
			}
		}
		catch (Exception ex)
		{
			Log.Warning("Failed to get hostname ({0}), will use localhost", new object[]
			{
				ex.Message
			});
			NetUtil.s_hostName = "localhost";
		}
		return NetUtil.s_hostName;
	}

	public static IPAddress GetIPv4Address(string host)
	{
		if (host.EqualsIgnoreCase("localhost"))
		{
			return IPAddress.Parse("127.0.0.1");
		}
		IPAddress cachedAddress = NetUtil.GetCachedAddress(host);
		if (cachedAddress != null)
		{
			for (;;)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(NetUtil.GetIPv4Address(string)).MethodHandle;
			}
			return cachedAddress;
		}
		int i = 0;
		while (i < 3)
		{
			try
			{
				i++;
				IPAddress[] hostAddresses = Dns.GetHostAddresses(host);
				if (hostAddresses != null)
				{
					foreach (IPAddress ipaddress in hostAddresses)
					{
						if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
						{
							for (;;)
							{
								switch (3)
								{
								case 0:
									continue;
								}
								break;
							}
							NetUtil.SetCachedAddress(host, ipaddress);
							return ipaddress;
						}
					}
					for (;;)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
				}
			}
			catch (Exception)
			{
			}
			if (i < 3)
			{
				for (;;)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				Thread.Sleep(0x64);
				continue;
			}
		}
		for (;;)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			break;
		}
		throw new Exception(string.Format("Could not resolve {0}", host));
	}

	public static string GetIPv4Url(string url)
	{
		UriBuilder uriBuilder = new UriBuilder(url);
		IPAddress ipv4Address = NetUtil.GetIPv4Address(uriBuilder.Host);
		uriBuilder.Host = ipv4Address.ToString();
		return uriBuilder.ToString();
	}

	public static IPAddress GetCachedAddress(string host)
	{
		object obj = NetUtil.s_hostCache;
		IPAddress result;
		lock (obj)
		{
			result = NetUtil.s_hostCache.TryGetValue(host);
		}
		return result;
	}

	public static void SetCachedAddress(string host, IPAddress address)
	{
		object obj = NetUtil.s_hostCache;
		lock (obj)
		{
			NetUtil.s_hostCache[host] = address;
		}
	}

	public static void ClearCachedAddresses()
	{
		NetUtil.s_hostCache.Clear();
	}

	public static bool IsInternalAddress(this IPAddress address)
	{
		if (address.AddressFamily != AddressFamily.InterNetwork)
		{
			for (;;)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(IPAddress.IsInternalAddress()).MethodHandle;
			}
			throw new Exception("This method works for IPv4 addresses only");
		}
		byte[] addressBytes = address.GetAddressBytes();
		if (addressBytes[0] == 0x7F)
		{
			return true;
		}
		if (addressBytes[0] == 0xC0)
		{
			for (;;)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (addressBytes[1] == 0xA8)
			{
				for (;;)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				return true;
			}
		}
		if (addressBytes[0] == 0xA)
		{
			for (;;)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			return true;
		}
		return false;
	}

	public static string GetHostName(string address)
	{
		if (!address.Contains("://"))
		{
			for (;;)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(NetUtil.GetHostName(string)).MethodHandle;
			}
			address = "tcp://" + address;
		}
		Uri uri = new Uri(address);
		return uri.Host;
	}

	public static bool IsInternalAddress(string address)
	{
		return IPAddress.Parse(NetUtil.GetHostName(address)).IsInternalAddress();
	}

	public static IPAddress HexToIPv4Address(string hex)
	{
		if (hex.Length % 2 != 0)
		{
			for (;;)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(NetUtil.HexToIPv4Address(string)).MethodHandle;
			}
			hex = "0" + hex;
		}
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
		}
		return new IPAddress(array);
	}
}
