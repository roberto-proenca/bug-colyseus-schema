'use strict';

const http = require('http');

const express = require('express');
const { Server } = require('colyseus');
const monitor = require("@colyseus/monitor").monitor;

const NSRoom = require('./ns-room');

const port = 2567;
const app = express();

const gameServer = new Server({
    server: http.createServer(app),
    express: app,
  });

console.log('Registering room');
gameServer.define('nsroom', NSRoom);

app.use("/colyseus", monitor(gameServer));
app.listen(8080);

gameServer.onShutdown(function(){
    console.log(`game server is going down.`);
});

gameServer.listen(port);
console.log(`Listening on ws://localhost:${ port }`);
