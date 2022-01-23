$(function () {
    $(".fshirje-btn").click(function () {
        if (confirm("Jeni te sigurt qe doni te fshini kete makine!")) {
            var element = $(this);
            var del_id = element.attr("id");
            var info = "id=" + del_id;
            $.ajax({
                type: "POST",
                url: "/Makinats/Delete",
                data: info,
                success: function (data) {
                    if (data) {
                        $("#makine" + del_id).fadeOut();
                    } else {
                        alert('Rekordi nuk mund te fshihet');
                    }
                }
            });
        }
        return false;
    });
});