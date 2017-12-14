(function ($) {
    $(function () {

        var resultado;

        var chatInput = $("#chat-input");
        var userName;
        var map;
        var chat = $.connection.recived;
        var chatWindow = $("#chat-window");
        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -0.225219, lng: -78.5248 },
            zoom: 15
        });



        var icon1 = {
            url: "../Content/images/ic_policeman.png", // url
            scaledSize: new google.maps.Size(70, 70), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };
        markers = [];
        markersDelete = [];
        a = 0;
        //this is the function that's run when the "messageReceived" function is called from the server
        chat.client.messageReceived = function (livePositionRequest) {


            var pos = { lat: livePositionRequest.Lat, lng: livePositionRequest.Lon };

            if (empresa == livePositionRequest.EmpresaId) {
                var marker;
                if (markers.length == 0) {
                    markers.push({ pos: pos, AgenteId: livePositionRequest.AgenteId, Nombre: livePositionRequest.Nombre });

                    var marker = new google.maps.Marker({
                        position: pos,
                        map: map,
                        title: livePositionRequest.Nombre,
                        icon: icon1
                    });


                    var localTime = moment.utc().toDate();
                    localTime = moment(localTime).format('DD-MM-YYYY hh:mm:ss A');

                    var contentString = '<div id="content">' +
                        '<div id="siteNotice">' +
                        '</div>' +
                        '<h4 id="firstHeading" class="firstHeading">City Park</h1>' +
                        '<div id="bodyContent">' +
                        '<p><b>Nombre del Agente:</b>' + livePositionRequest.Nombre + '.</p>' +
                        '<p><b>Latitud:</b>' + pos.lat + '.</p>' +
                        '<p><b>Longitud:</b>' + pos.lng + '.</p>' +
                        '<p><b>Fecha:</b>' + localTime + '.</p>' +
                        '</div>' +
                        '</div>';

                    var infowindow = new google.maps.InfoWindow({
                        content: contentString
                    });
                    marker.addListener('click', function () {
                        infowindow.open(map, marker);
                    });

                    markersDelete.push(marker);

                } else {
                    for (var i = 0; i < markers.length; i++) {
                        var agenteId = livePositionRequest.AgenteId;
                        var existe = existeAgente(agenteId);
                        if (existe == true) {
                            markersDelete[i].setMap(null);
                            markers[i].pos = pos;
                            var marker = new google.maps.Marker({
                                position: pos,
                                map: map,
                                title: markers[i].Nombre,
                                icon: icon1
                            });
                            markersDelete[i] = marker;

                            var localTime = moment.utc().toDate();
                            localTime = moment(localTime).format('DD-MM-YYYY hh:mm:ss A');

                            var contentString = '<div id="content">' +
                                '<div id="siteNotice">' +
                                '</div>' +
                                '<h4 id="firstHeading" class="firstHeading">City Park</h1>' +
                                '<div id="bodyContent">' +
                                '<p><b>Nombre del Agente:</b>' + markers[i].Nombre + '.</p>' +
                                '<p><b>Latitud:</b>' + markers[i].Lat + '.</p>' +
                                '<p><b>Longitud:</b>' + markers[i].Lon + '.</p>' +
                                '<p><b>Fecha:</b>' + localTime + '.</p>' +
                                '</div>' +
                                '</div>';

                            var infowindow = new google.maps.InfoWindow({
                                content: contentString
                            });
                            marker.addListener('click', function () {
                                infowindow.open(map, marker);
                            });
                        }
                        else {
                            markers.push({ pos: pos, AgenteId: livePositionRequest.AgenteId, Nombre: livePositionRequest.Nombre });

                            var marker = new google.maps.Marker({
                                position: pos,
                                map: map,
                                title: livePositionRequest.Nombre,
                                icon: icon1
                            });


                            var localTime = moment.utc().toDate();
                            localTime = moment(localTime).format('DD-MM-YYYY hh:mm:ss A');

                            var contentString = '<div id="content">' +
                                '<div id="siteNotice">' +
                                '</div>' +
                                '<h4 id="firstHeading" class="firstHeading">City Park</h1>' +
                                '<div id="bodyContent">' +
                                '<p><b>Nombre del Agente:</b>' + livePositionRequest.Nombre + '.</p>' +
                                '<p><b>Latitud:</b>' + pos.lat + '.</p>' +
                                '<p><b>Longitud:</b>' + pos.lng + '.</p>' +
                                '<p><b>Fecha:</b>' + localTime + '.</p>' +
                                '</div>' +
                                '</div>';

                            var infowindow = new google.maps.InfoWindow({
                                content: contentString
                            });
                            marker.addListener('click', function () {
                                infowindow.open(map, marker);
                            });

                            markersDelete.push(marker);
                        }

                    }
                }

            }
        };

        function existeAgente(agenteId) {
            var miresultado = false;
            for (var i = 0; i < markers.length; i++) {
                if (markers[i].AgenteId == agenteId) {
                    miresultado = true;
                    break;
                }
            }
            return miresultado;
        };

        function obtenerInfo(contentString) {
            var localTime = moment.utc().toDate();
            localTime = moment(localTime).format('DD-MM-YYYY hh:mm:ss A');

            contentString = '<div id="content">' +
                '<div id="siteNotice">' +
                '</div>' +
                '<h4 id="firstHeading" class="firstHeading">City Park</h1>' +
                '<div id="bodyContent">' +
                '<p><b>Nombre del Agente:</b>' + mark.Nombre + '.</p>' +
                '<p><b>Latitud:</b>' + pos.lat + '.</p>' +
                '<p><b>Longitud:</b>' + pos.lng + '.</p>' +
                '<p><b>Fecha:</b>' + localTime + '.</p>' +
                '</div>' +
                '</div>';
            return contentString;
        };

        function setMapOnAll(map) {
            for (var i = 0; i < markersDelete.length; i++) {
                markersDelete[i].setMap(map);
            }
        };

        function myFunction() {
            myVar = setInterval(alertFunc, 5000);
        };

        function alertFunc() {
            deleteMarkers();
        }

        function deleteMarkers() {
            clearMarkers();
            markers = [];
        };

        function clearMarkers() {
            setMapOnAll(null);
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