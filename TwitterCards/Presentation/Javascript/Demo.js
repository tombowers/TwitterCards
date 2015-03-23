$(document).ready(function () {

	var tweetContainer = $('.tweet-container');
	var loadingText = $('<p>Getting Tweets...</p>');
	tweetContainer.append(loadingText);

	tweetContainer.on('mouseenter', '.tweet-card', function() {
		$(this).css({ borderBottom: '6px solid #5ea9dd' });
	}).on('mouseleave', '.tweet-card', function () {
		$(this).css({ borderBottom: '6px solid white' });
	});

	$.getJSON('../api/data', function (data) {
		loadingText.remove();
		addCard(tweetContainer, data, 0);
	});

	// Add, animate, rave, repeat
	var addCard = function (container, tweetData, index) {
		var tweet = tweetData[index];

		var tweetCard = $('<div class="tweet-card"><div class="tweet-author">@' + tweet.Author + '</div><div class="tweet-text">' + tweet.Text + '</div></div>');

		tweetContainer.append(tweetCard);
			
		tweetCard.transition({ scale: 0.6, duration: 0 })
			.transition({ scale: 1, duration: 50 }, function () {
				tweetData.length - 1 > index && addCard(tweetContainer, tweetData, index + 1);
			});
	};
});