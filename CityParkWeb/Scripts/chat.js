(function ($) {
    $(function () {
        var chatInput = $("#chat-input");
        var userName;
        var map = $("#map");

        //ask for a username
        setTimeout(function () {
            userName = prompt("Please enter a username.");
        }, 0);

        var chat = $.connection.chat;
        var chatWindow = $("#chat-window");

        //this is the function that's run when the "messageReceived" function is called from the server
        chat.client.messageReceived = function (livePositionRequest) {

            var pos = { lat: livePositionRequest.Lat, lng: livePositionRequest.Lon };
            var marker = new google.maps.Marker({
                position: pos,
                map: map
            });
            chatWindow.append("<div><strong>" + livePositionRequest.EmpresaId + ": </strong>" + livePositionRequest.Lat + "</div>");
            
            
        };

        $.connection.hub.start().done(function () {
            chatInput.keydown(function (e) {

                if (e.which === 13) {
                    var text = chatInput.val();

                    //empty the textbox
                    chatInput.val("");

                    //send the message to the server
                    chat.server.sendMessage(userName, text);

                    //focus cursor on the textbox for easy chatting!
                    self.focus();
                }
            });
        });
    });
})(jQuery);