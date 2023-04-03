using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

using UnityEngine;

public static class CursorLock
{
	public static bool ActualState => pointerLocked && Cursor.lockState != CursorLockMode.None;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	[StructLayout(LayoutKind.Sequential)]
	struct MSRectStruct
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;

		public static implicit operator RectInt(MSRectStruct rect) => new RectInt(rect.Left,rect.Top,rect.Right - rect.Left,rect.Bottom - rect.Top);
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	static extern bool GetClipCursor(out MSRectStruct lprect);

	static bool pointerLocked
	{
		get
		{
			if(!GetClipCursor(out var lprect))
				return false;

			var rect = (RectInt)lprect;
			return rect.width <= Screen.width && rect.height <= Screen.height;
		}
	}
#elif UNITY_WEBGL
	static bool pointerLocked;
	[AOT.MonoPInvokeCallback(typeof(System.Action<bool>))]
	static void OnPointerLockChanged(bool locked)
	{
		pointerLocked = locked;
	}

	[DllImport("__Internal")]
	private static extern void JS_RegisterLockCursorFunction(System.Action<bool> callback);

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void PointerLockChangedRegister()
	{
		JS_RegisterLockCursorFunction(OnPointerLockChanged);
	}
#else
	const bool pointerLocked = true;
#endif
}
