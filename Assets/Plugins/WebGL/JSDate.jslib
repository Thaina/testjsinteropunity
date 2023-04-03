/*global requireHandle, _emval_register, _emval_run_destructors*/

var myLib = {
  JS_GetDate__deps: ['_emval_register'],
	JS_GetDate: function() {
		return __emval_register(new Date());
	},
	
  JS_GetTime__deps: ['$requireHandle'],
	JS_GetTime: function(/** @type {Date} */date) {
		date = requireHandle(date);
		return date.getTime();
	},
	
  JS_Free__deps: ['_emval_run_destructors'],
	JS_Free: function(/** @type {Date} */date) {
		__emval_run_destructors(date);
	},
}

mergeInto(LibraryManager.library,myLib);