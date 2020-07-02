// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initialize() {


	$('.tree-view').each(function (index, ele) {
		var data = $(ele).data('tree-view-data');
		$(ele).bstreeview({
			data: data,
			openNodeLinkOnNewTab: false
		});
	});

}

$(initialize);