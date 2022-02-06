// Creating functions for the Unity

mergeInto(LibraryManager.library, {
	Closewindow: function (){
		window.close();
	},

	Redirect: function (url){
		window.location.href = Pointer_stringify(url);
	},

	SessionRedirect: function (sessionStorageItem) {
		var location = sessionStorage.getItem(Pointer_stringify(sessionStorageItem));

		if (location != null) {
			window.location.href = location;
		} else {
			window.history.back();
		}	
	}
});