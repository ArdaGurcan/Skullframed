mergeInto(LibraryManager.library, {
    GetX: function () {
        //let document.getElementById("unity-canvas").offsetLeft = document.getElementById("unity-canvas").offsetLeft;
        //let document.getElementById("unity-canvas").offsetTop = document.getElementById("unity-canvas").offsetTop;
        // let (document.getElementById("unity-canvas").clientWidth / 8) = document.getElementById("unity-canvas").clientWidth / 8;
        return (
            (document.getElementById("unity-canvas").offsetLeft -
                window.innerWidth / 2 +
                document.getElementById("unity-canvas").clientWidth / 2) /
            (document.getElementById("unity-canvas").clientWidth / 8)
        );
    },
    GetY: function () {
        return (
            -document.getElementById("unity-canvas").offsetTop /
            (document.getElementById("unity-canvas").clientWidth / 8)
        );
    },

    Reset: function (top) {
        // $("canvas").draggable("disable")
        $("#unity-canvas").draggable({
            drag: function (event, ui) {
                return false;
            },
        });
        $("canvas").css("top", 0);
        $("canvas").css("left", 0);
        //$("#unity-canvas").draggable({
        //    drag:function(event,ui){return}
        //  })
    },
    Enable: function () {
        $("canvas").draggable({
            drag: function (event, ui) {
                return;
            },
        });
    },

    SendY: function (y) {
        $("#unity-canvas").draggable({
            drag: function (event, ui) {
                return false;
            },
        });
        $("canvas").css(
            "top",
            (document.getElementById("unity-canvas").offsetTop =
                y * (document.getElementById("unity-canvas").clientWidth / 8))
        );
    },
});
