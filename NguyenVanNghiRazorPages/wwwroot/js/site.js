// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function editAccount(id) {
	var roleMap = {
		0: "Admin",
		1: "Staff",
		2: "Lecturer"
	};
	$.ajax({
		type: 'GET',
		url: 'Account/Index?handler=Edit',
		data: { id: id },
		success: function (data) {
			$("#EditAccountDTO_AccountID").val(data.accountID);
			$("#EditAccountDTO_AccountName").val(data.accountName);
			var roleText = roleMap[data.accountRole];
			$("#EditAccountDTO_AccountRole").val(roleText);
			$("#EditAccountDTO_AccountEmail").val(data.accountEmail);
			console.log(data)
		},
		error: function (xhr, status, error) {
			console.error("Error:", error);
		}
	});
}

function loadDelete(id, tagId) {
	$(`#${tagId}`).val(id)
}

function editNewsArticle(id) {
	var statusMap = {
		0: "Inactive",
		1: "Active",
	};
	$.ajax({
		type: 'GET',
		url: 'Manage?handler=Edit',
		data: { id: id },
		success: function (data) {
			$("#EditNewsArticleDTO_NewsTitle").val(data.newsTitle);
			$("#EditNewsArticleDTO_CategoryId").val(data.categoryId);
			$("#EditNewsArticleDTO_Headline").val(data.headline);
			$("#EditNewsArticleDTO_NewsArticleId").val(data.newsArticleId);
			$("#EditNewsArticleDTO_NewsContent").val(data.newsContent);
			$("#EditNewsArticleDTO_NewsSource").val(data.newsSource);
			var statusText = statusMap[data.newsStatus];
			$("#EditNewsArticleDTO_NewsStatus").val(statusText);
			$("#EditNewsArticleDTO_Tags").val(data.tags);
			
			console.log(data);
		},
		error: function (data) {

		}
	});
}
