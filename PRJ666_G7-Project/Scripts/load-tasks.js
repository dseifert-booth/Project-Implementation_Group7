


$(document).ready(function () {

    $("#shift-ddl").on("change", function () {
        if ($(this).val()) {
            var url = $(this).val();
            $('#schedule-edit-partial').load(url, function () {
                $('#schedule-edit-partial').addClass("well");
            })
        }
    })
})
