"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/newsArticleHub").build();

// for index
connection.on("UpdateNewsArticles", () => {
	location.reload();
});

// for manage
connection.on("UpdateNewsArticlesList", () => {
	location.reload();
});

// Start the connection
connection.start().catch(function (err) {
	console.error(err.toString());
});