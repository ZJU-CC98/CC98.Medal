// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (!String.format) {
	String.format = function (format) {
		var args = Array.prototype.slice.call(arguments, 1);
		return format.replace(/{(\d+)}/g, function (match, number) {
			return typeof args[number] != 'undefined'
				? args[number]
				: match
				;
		});
	};
}

function initialize() {


	// 消息框关闭支持
	$('.message .close')
		.on('click', function () {
			$(this)
				.closest('.message')
				.transition('fade')
				;
		})
		;

	$('.ui.sidebar')
		.sidebar({
			//context: $('#category-sidebar-container'),
			dimPage: false,
			onVisible: function() {
				$('#toggle-sidebar-link').addClass('active');
			},
			onHide: function() {
				$('#toggle-sidebar-link').removeClass('active');

			}
		})
		.sidebar('attach events', '#toggle-sidebar-link');

	$('.ui.checkbox').checkbox();

	$('.ui.dropdown').dropdown();
	$('select.dropdown').dropdown();

	$('.dropdown[data-setting]').each(function (index, ele) {
		var setting = $(ele).data('setting');
		$(ele).dropdown(JSON5.parse(setting));
	});
}

$(initialize);