$(document).ready(function () {

	var tweetContainer = $('.tweet-container');
	var loadingText = $('<p class="loading">Getting Tweets...</p>');
	tweetContainer.append(loadingText);

	tweetContainer.on('mouseenter', '.tweet-card .inner', function() {
		$(this).css({ borderBottom: '6px solid #5ea9dd' });
	}).on('mouseleave', '.tweet-card .inner', function () {
		$(this).css({ borderBottom: '6px solid white' });
	});

	$.getJSON('../api/data', function (data) {
		loadingText.remove();
		addCard(tweetContainer, data, 0);
	});

	// Add, animate, rave, repeat
	var addCard = function (container, tweetData, index) {
		var tweet = tweetData[index];

		var tweetCard = $('<div class="tweet-card"><img class="profile-image" src="' + tweet.Author.ProfileImageUrl + '"/><div class="inner"><div class="tweet-author">@' + tweet.Author.Handle + '</div><div class="tweet-text">' + tweet.Text + '</div></div></div>');

		tweetContainer.append(tweetCard);
			
		tweetCard
			.transition({ scale: 0.7, duration: 0 })
			.transition({ scale: 1.02, duration: 50 })
			.transition({ scale: 1, duration: 30 }, function () {
				tweetData.length - 1 > index && addCard(tweetContainer, tweetData, index + 1);
			});
	};
});