$(document).ready(function () {
    /*Init Socket*/
    const socket = io();

    socket.on('news', function (msg) {
        console.log(msg)
    });

    socket.on('data',function(data){
        console.log(data);
    });


});

