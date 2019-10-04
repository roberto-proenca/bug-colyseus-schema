'use strict';

const { Schema, type, MapSchema, ArraySchema } = require('@colyseus/schema');

class GlobalState extends Schema {

    constructor() {
        super();

        this.status = '';
        this.items = new MapSchema();
        // this.things = new ArraySchema();
    }
}

type('string')(GlobalState.prototype, 'status');
type({map: 'int8'})(GlobalState.prototype, 'items');
// type(['string'])(GlobalState.prototype, 'things');

module.exports = {
    GlobalState,
};
