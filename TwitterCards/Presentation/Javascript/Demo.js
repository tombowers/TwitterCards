$(document).ready(function () {
	"use strict";

	addTweets($('.tweet-container'));

	function addTweets($tweetContainer) {

		// Show loading text
		var loadingText = $('<p class="loading">Getting Tweets...</p>');
		$tweetContainer.append(loadingText);

		// Make request for tweet data from web api
		$.getJSON('../api/data', function (data) {
			loadingText.remove();
			addCard($tweetContainer, data, 0);
		});

		$tweetContainer.on('mouseenter', '.tweet-card .inner', function () {
			$(this).css({ borderBottom: '6px solid #5ea9dd' });
		}).on('mouseleave', '.tweet-card .inner', function () {
			$(this).css({ borderBottom: '6px solid white' });
		});

		$tweetContainer.on('click', '.tweet-card', function () {
			var id = $(this).data('tweet-id');

			var existingTweetDetailElement = $('.tweet-detail*[data-tweet-id="' + id + '"]');
			if (existingTweetDetailElement.length === 1 && existingTweetDetailElement.data('tweet-id') === id) {
				showBackgroundMask();
				appearanceAnimation(existingTweetDetailElement);
			} else {
				$.getJSON('../api/data/tweet/' + id, function(data) {
					showTweetDetail(data);
				});
			}
		});
	}

	// Add, animate, rave, repeat
	function addCard($container, tweetData, index) {
		var tweet = tweetData[index];

		var tweetCard = createTimelineTweetElement(tweet);
		$container.append(tweetCard);
			
		tweetCard
			.transition({ scale: 0.7, duration: 0 })
			.transition({ scale: 1.02, duration: 50 })
			.transition({ scale: 1, duration: 30 }, function () {
				tweetData.length - 1 > index && addCard($container, tweetData, index + 1);
			});
	}

	function showBackgroundMask() {
		var mask = $('.mask');

		if (mask.length === 0) {
			mask = $('<div class="mask"></div>');

			mask.on('click', function () {
				mask.fadeOut();
				hideTweetDetail();
			});

			mask.appendTo('body');
		}

		mask.fadeIn(); // TODO: replace with transit
	}

	function showTweetDetail(tweet) {

		showBackgroundMask();

		var tweetElement = createDetailTweetElement(tweet);
		$('body').append(tweetElement);

		appearanceAnimation(tweetElement);
	}

	function hideTweetDetail() {
		$('.tweet-detail').fadeOut();
	}

	function appearanceAnimation($element) {
		$element.fadeIn();
	}

	function createDetailTweetElement(tweet) {
		var markup = '';
		markup += '<div class="tweet-detail" data-tweet-id="' + tweet.JavascriptId + '">';
		markup += '  <img class="profile-image" src="' + tweet.Author.ProfileImageUrl + '"/>';
		markup += '  <div class="inner">';
		markup += '    <div class="tweet-author">@' + tweet.Author.Handle + '</div>';
		markup += '    <div class="tweet-text">' + tweet.Text + '</div>';
		markup += '  </div>';
		markup += '</div>';

		return $(markup);
	}

	function createTimelineTweetElement(tweet) {
		var markup = '';
		markup += '<div class="tweet-card" data-tweet-id="' + tweet.JavascriptId + '">';
		markup += '  <img class="profile-image" src="' + tweet.Author.ProfileImageUrl + '"/>';
		markup += '  <div class="inner">';
		markup += '    <div class="tweet-author">@' + tweet.Author.Handle + '</div>';
		markup += '    <div class="tweet-text">' + tweet.Text + '</div>';
		markup += '  </div>';
		markup += '</div>';

		return $(markup);
	}
});