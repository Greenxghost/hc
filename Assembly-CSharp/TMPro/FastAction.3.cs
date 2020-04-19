﻿using System;
using System.Collections.Generic;

namespace TMPro
{
	public class FastAction<A, B>
	{
		private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

		private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();

		public void Add(Action<A, B> rhs)
		{
			if (this.lookup.ContainsKey(rhs))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(FastAction.Add(Action<A, B>)).MethodHandle;
				}
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		public void Remove(Action<A, B> rhs)
		{
			LinkedListNode<Action<A, B>> node;
			if (this.lookup.TryGetValue(rhs, out node))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(FastAction.Remove(Action<A, B>)).MethodHandle;
				}
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		public void Call(A a, B b)
		{
			for (LinkedListNode<Action<A, B>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b);
			}
		}
	}
}
