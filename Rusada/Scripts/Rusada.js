////function common_datatables_ready(e) {
////	$(document).ready(function () {
////		//$("#" + e).dataTable({
////		//	sDom: "flrtip",
////		//	autoWidth: !0,
////		//	lengthMenu: [
////		//		[10, 15, 20, 25, 50, 100],
////		//		[10, 15, 20, 25, 50, 100]
////		//	]
////		//})
////	})
////}
function LoadContents(e, t, n, a, r, i) {
	void 0 === r && (r = !0), void 0 === i && (i = !0);
	var o =   e,
		s = "content";
	a && (s = a), $.ajax({
		type: "GET",
		url: o,
		data: n,
		beforeSend: function () {
			loading(!0)
		},
		success: function (e, t, n) {
			loading(!0);
			var a = n.getResponseHeader("X-Responded-JSON");
			null != a ? (objheader = JSON.parse(a), window.location.replace(objheader.headers.location)) : $("#" + s).html(e)
		},
		complete: function () {
			loading(!0)
		},
		error: function (e, t, n) {
			loading(!1)
		},
		dataType: "html",
		async: r,
		cache: !1
	}), $(document).ajaxStop(function () {
		loading(!1)
	})
}
function loading(e) {
	e && ($("#enlarge").is(":visible") || $("#enlarge").show()), e || $("#enlarge").hide()
}
function ShowNotification(e, t, n) {
	alert('Database Updated');
	//var a = "fa fa-thumbs-up bounce animated";
	//"danger" == t && (void 0 === n ? (n = !0, a = "fa fa-thumbs-down bounce animated") : a = "fa fa-times"), "error" == t && (void 0 === n ? (n = !0, a = "fa fa-thumbs-down bounce animated") : a = "fa fa-times"), "info" == t && (a = "fa fa-bell swing animated", void 0 === n && (n = !0)), "warning" == t && (a = "fa fa-shield fadeInLeft animated", void 0 === n && (n = !0)), "error" == t && void 0 === n && (n = !0, a = "fa fa-thumbs-down bounce animated"), void 0 === n && (n = !0, a = "fa fa-thumbs-up bounce animated"), $("div[data-notify='container']").remove(), n ? $.notify({
	//	icon: a,
	//	title: "",
	//	message: e
	//}, {
	//	type: t
	//}) : $.notify({
	//	title: "",
	//	message: e
	//}, {
	//	type: t
	//})
}

function parseJsonData(e) {
	var t = "";
	try
	{
		var n = JSON.parse(e);
		t = null != n.Message ? replaceAll(n.Message, "\r\n", "<br /><br />") : replaceAll(n, "\r\n", "<br /><br />")
	}
	catch (n)
	{
		t = e
	}
	return t
}