$(document).ready(function () {
	"use strict";

	addTweets($('.tweet-container'));

	function jsonRequest(url, callback, failure, always) {
		$.ajax(url)
			.done(function (data) {
				callback && callback(data);
			})
			.fail(function () {
				failure && failure();
			})
			.always(function () {
				always && always();
			});
	}

	function addTweets($tweetContainer) {

		// Show loading text
		var loadingText = $('<p class="loading">Getting Tweets...</p>');
		$tweetContainer.append(loadingText);

		// Make request for tweet data from web api
		jsonRequest('../api/data', function(data) {
			loadingText.remove();
			addCard($tweetContainer, data, 0);
		}, function() {
			loadingText.text('Error connecting to Twitter');
		});

		$tweetContainer.on('mouseenter', '.tweet-card .inner', function () {
			$(this).css({ borderBottom: '6px solid #5ea9dd' });
		}).on('mouseleave', '.tweet-card .inner', function () {
			$(this).css({ borderBottom: '6px solid white' });
		});

		$tweetContainer.on('click', '.tweet-card', function (e) {
			var id = $(this).data('tweet-id');

			var $existingTweetDetailElement = $('.tweet-detail*[data-tweet-id="' + id + '"]');
			if ($existingTweetDetailElement.length === 0) {
				jsonRequest('../api/data/tweet/' + id, function(data) {
					$existingTweetDetailElement = createTweetDetail(data);

					showBackgroundMask();
					appearanceAnimation($existingTweetDetailElement);
				});
			} else {
				showBackgroundMask();
				appearanceAnimation($existingTweetDetailElement);
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
				fadeOut(mask);
				hideTweetDetail();
			});

			mask.appendTo('body');
		}

		fadeIn(mask, 0.75);
	}

	function createTweetDetail(tweet) {

		showBackgroundMask();

		var $tweetElement = createDetailTweetElement(tweet);
		$('body').append($tweetElement);

		return ($tweetElement);
	}

	function fadeIn($element, opacity) {
		$element.show().transition({ opacity: opacity || 1 });
	}

	function fadeOut($element) {
		$element.transition({ opacity: 0 }, function() { $element.hide(); });
	}

	function hideTweetDetail() {
		fadeOut($('.tweet-detail'));
	}

	function appearanceAnimation($element) {
		$element
			.show()
			.transition({ scale: 0, opacity: 1, duration: 0 })
			.transition({ scale: 1, duration: 200 });
	}

	function createDetailTweetElement(tweet) {
		var markup = '';
		markup += '<div class="tweet-detail" data-tweet-id="' + tweet.JavascriptId + '">';
		markup += '  <img class="profile-image" src="' + tweet.Author.ProfileImageUrl + '"/>';
		markup += '  <div class="inner">';
		markup += '    <div class="tweet-author">';
		markup += '	     <span class="name">' + tweet.Author.Name + '</span>';
		markup += '	     <span class="screenname">@' + tweet.Author.ScreenName + '</span>';
		markup += '    </div>';
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
		markup += '    <div class="tweet-author">@' + tweet.Author.ScreenName + '</div>';
		markup += '    <div class="tweet-text">' + tweet.Text + '</div>';
		markup += '  </div>';
		markup += '</div>';

		return $(markup);
	}
});