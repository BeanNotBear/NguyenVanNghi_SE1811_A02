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

function loadDelete(id) {
	$("#deleteAccountID").val(id)
}

function logout() {
	document.cookie.split(";").forEach(function (cookie) {
		document.cookie = cookie
			.replace(/^ +/, "") // Xóa khoảng trắng đầu dòng
			.replace(/=.*/, "=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/"); // Đặt ngày hết hạn về quá khứ
	});
}
