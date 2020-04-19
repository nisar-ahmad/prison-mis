<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <script src="jquery.min.js"></script>
    <script src="swfobject.js"></script>
    <script src="scriptcam.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#webcam").scriptcam({
                showMicrophoneErrors: false,
                onError: onError,
                cornerRadius: 20,
                cornerColor: 'e3e5e2',
                onWebcamReady: onWebcamReady,
                uploadImage: 'upload.gif',
                onPictureAsBase64: base64_tofield_and_image
            });
        });

        function takeSnap() {
            base64_toimage();
            base64_tofield();
        }
        function base64_tofield() {
            $('#formfield').val($.scriptcam.getFrameAsBase64());
        }
        function base64_toimage() {
            $('#image').attr("src", "data:image/png;base64," + $.scriptcam.getFrameAsBase64());
        }
        function base64_tofield_and_image(b64) {
            $('#formfield').val(b64);
            $('#image').attr("src", "data:image/png;base64," + b64);
        }
        function changeCamera() {
            $.scriptcam.changeCamera($('#cameraNames').val());
        }
        function onError(errorId, errorMsg) {
            $("#btn1").attr("disabled", true);
            $("#btn2").attr("disabled", true);
            alert(errorMsg);
        }
        function onWebcamReady(cameraNames, camera, microphoneNames, microphone, volume) {
            $.each(cameraNames, function (index, text) {
                $('#cameraNames').append($('<option></option>').val(index).html(text))
            });
            $('#cameraNames').val(camera);
        }
	    </script>
</head>
<body>
    <div>
        <div style="margin: 5px;">
            <img src="webcamlogo.png" style="vertical-align: text-top" />
            <select id="cameraNames" size="1" onchange="changeCamera()" style="width: 245px; font-size: 11px; height: 25px;">
            </select>
        </div>
        <div id="webcam">
        </div>
        <img id="image" style="width: 200px; height: 153px;" />
        <p><button class="btn btn-small" id="btn1" onclick="takeSnap()">Take Snapshot</button></p>
        <form runat="server" action="/PMIS/Prisoner/Photo" method="post" target="_parent">
            <input type="submit" value="Submit" />
            <input id="PrisonerId" name="PrisonerId" type="hidden" value="<%= Request["PrisonerId"] %>">
            <input id="formfield" name="formfield" type="hidden">
        </form>
    </div>
</body>
</html>
