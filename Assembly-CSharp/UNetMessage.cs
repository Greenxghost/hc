﻿using System;

public class UNetMessage
{
	public const int MSG_HEADER_SIZE = 9;

	public byte[] Bytes;

	public int NumBytes;

	public byte[] Serialize()
	{
		if (this.Bytes != null)
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(UNetMessage.Serialize()).MethodHandle;
			}
			if (this.NumBytes + 1 >= 9)
			{
				byte[] array = new byte[this.NumBytes + 1];
				array[0] = 0;
				Buffer.BlockCopy(this.Bytes, 0, array, 1, this.NumBytes);
				return array;
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
		}
		Log.Error("BinaryMessage.Serialize invalid message numBytes={0}", new object[]
		{
			this.NumBytes
		});
		return null;
	}

	public void Deserialize(byte[] rawData)
	{
		if (rawData != null)
		{
			if (rawData.Length >= 9)
			{
				this.NumBytes = rawData.Length - 1;
				this.Bytes = new byte[this.NumBytes];
				Buffer.BlockCopy(rawData, 1, this.Bytes, 0, this.NumBytes);
				return;
			}
			for (;;)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(UNetMessage.Deserialize(byte[])).MethodHandle;
			}
		}
		Log.Error("BinaryMessage.Deserialize invalid message bytes {0}", new object[]
		{
			(rawData == null) ? -1 : rawData.Length
		});
	}
}
