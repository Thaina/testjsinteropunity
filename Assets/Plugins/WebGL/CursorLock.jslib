mergeInto(LibraryManager.library,{
	JS_RegisterLockCursorFunction: function(onPointerLockChanged) {
		Module['dynCall_vi'](onPointerLockChanged,document.pointerLockElement == Module["canvas"]);
		document.addEventListener("pointerlockchange",function() {
			Module['dynCall_vi'](onPointerLockChanged,document.pointerLockElement == Module["canvas"]);
		});
	},
});