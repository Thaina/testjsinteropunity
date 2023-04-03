using System;

using UnityEngine;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class JSDate : IDisposable
{
#if UNITY_EDITOR && UNITY_WEBGL
	[UnityEditor.InitializeOnLoadMethod]
	static void SetWebGL()
	{
		if(!UnityEditor.PlayerSettings.WebGL.emscriptenArgs.Contains("--bind"))
			UnityEditor.PlayerSettings.WebGL.emscriptenArgs = string.Join(" ",UnityEditor.PlayerSettings.WebGL.emscriptenArgs,"--bind").Trim();
	}
#endif

	[DllImport("__Internal")]
	static extern void JS_Free(IntPtr handle);
	[DllImport("__Internal")]
	static extern IntPtr JS_GetDate();
	[DllImport("__Internal")]
	static extern double JS_GetTime(IntPtr handle);

	IntPtr _handle;
	public JSDate()
	{
		_handle = JS_GetDate();
	}
	
	public DateTimeOffset Value
	{
		get
		{
			var ms = JS_GetTime(_handle);
			Debug.Log(ms);
			var dt = DateTimeOffset.UnixEpoch + TimeSpan.FromMilliseconds(ms);
			Debug.Log(dt);
			return dt.ToOffset(TimeSpan.FromHours(7));
		}
	}

	~JSDate() => Dispose();
	public void Dispose()
	{
		if(_handle != IntPtr.Zero)
		{
			JS_Free(_handle);
			_handle = IntPtr.Zero;
		}
	}
}
