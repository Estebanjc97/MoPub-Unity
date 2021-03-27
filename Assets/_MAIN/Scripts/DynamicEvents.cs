using System;
using UnityEngine.Events;

namespace Esteban
{
    namespace Utils
	{
		namespace Events
		{
			[Serializable]
			public class DynamicStringEvent : UnityEvent<string> { /* ... */ }

			[Serializable]
			public class DynamicIntEvent : UnityEvent<int> { /* ... */ }
		}
	}
}
