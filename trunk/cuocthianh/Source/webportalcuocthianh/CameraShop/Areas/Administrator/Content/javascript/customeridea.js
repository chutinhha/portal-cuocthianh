function reloadGridCustomerIdea(typeid) {
    $('#tSortablecustomeridea'+typeid+'').dataTable({
        "iDisplayLength": 5,
        "aLengthMenu": [5, 10, 25, 50, 100],
        "sPaginationType": "full_numbers",
        "iSortingCols": "STT"
    });
}
//function showPopup() {
//    $(".link-popup").on("click", function (e) {
//        e.preventDefault() ? e.preventDefault() : e.returnValue;
//        window.open($(this).attr("href"), "MsgWindow");
//    });
//}