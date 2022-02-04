// Creating functions for the Unity

mergeInto(LibraryManager.library, {
	Closewindow: function (){
		window.close();
	},

	Redirect: function (url){
		window.location.href = Pointer_stringify(url);
	},

	SessionRedirect: function () {
		var location = sessionStorage.getItem("Jam-Game");
		window.location.href = location;
	}
});