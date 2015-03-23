$(document).ready(function () {

	var tweetContainer = $('.tweet-container');

	$.getJSON('../api/data', function(data) {
		$.each(data, function (key, val) {

			var tweetCard = $('<div class="tweet-card"><div class="tweet-author">@' + val.Author + '</div><div class="tweet-text">'+ val.Text +'</div></div>');
			tweetContainer.append(tweetCard);
		});
	});

});