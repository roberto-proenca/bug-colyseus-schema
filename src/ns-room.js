'use strict';

const { Room } = require('colyseus');
const { GlobalState } = require('./schemas/global-state');

class NSRoom extends Room {
    onCreate (options) {
        console.log(`Room Init ${JSON.stringify(options)} - ${this.roomId}`);

        this.setState(new GlobalState());
        this.setPatchRate(1000 / 20);

        /** @type {number} */
        this.maxClients = 2;
    }

    onJoin (client, options) {
        console.log(`Client: ${client} joined. ${JSON.stringify(options)}`);

        this.state.items['002'] = 1;
        console.log(this.state.items)
        this.state.items['101'] = 1;
        console.log(this.state.items)
        this.state.items['221'] = 1;
        this.state.items['088'] = 1;
        this.state.items['090'] = 1;

        console.log(this.state.items)
    }

    onMessage (client, message) {
        console.log(`Client: ${client.id} sent message ${JSON.stringify(message)}`);

        if (message.type === 'map') {
            if (this.state.items[message.id] !== undefined) {
                if (message.amount === 0) {
                    delete this.state.items[message.id];
                }
                else {
                    this.state.items[message.id] += message.amount;
                }
            }
        }
        console.log(this.state.items)
        // else if (message.type === 'array') {
        //     if (message.amount > 0) {
        //         console.log(`Pushing to things: ${message.id} `);
        //         this.state.things.push(message.id);
        //     }
        //     else {
        //         console.log(`deleting from things: ${message.id} -- ${this.state.things.indexOf(message.id)} - ${this.state.things}`);
        //         if (this.state.things.indexOf(message.id) !== undefined) {
        //             this.state.things.splice(this.state.things.indexOf(message.id), 1);
        //         }

        //         console.log(`after - ${this.state.things}`);
        //     }
        // }
    }

    onLeave (client, consented) {
        console.log(`Client: ${client} left. consented? ${consented}`);
    }

    onDispose () {

    }
}

module.exports = NSRoom;