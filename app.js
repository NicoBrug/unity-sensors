var express = require('express');
const http = require('http');
var SelfReloadJSON = require('self-reload-json');

var app = express();
app.set('port', process.env.PORT || 3000);
var server = http.createServer(app);

var io = require('socket.io')(server);

app.use(express.static(__dirname + '/public'));

app.get('/', function(req, res, next){
  res.render('./public/index.html');
});


var config = new SelfReloadJSON(__dirname + '/data.json');


io.on('connection', socket => {
  console.log(`Connecté au client ${socket.id}`)
  
  config.on('updated',function(json){
    io.emit('data',json)
  })


});

server.listen(app.get('port'));
