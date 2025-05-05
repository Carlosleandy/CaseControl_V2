const events = new Map();

interface Reference {
    callback: any;
    eventName: string;
}

export const registerEvent = (eventName: any, callback: any, unique=false): Reference => {
    const handler = events.get(eventName);
    const ref = { callback, eventName};

    if (!handler)
        events.set(eventName, [callback]);
    else {
        if(!unique)
            handler.push(callback);
    }

    return ref;
}

export const dispatchEvent = (eventName: any, data: any) => {
    const handlers = events.get(eventName);

    if(!handlers) {
        throw new Error('Event not found');
    }

    for(const handler of handlers) {
        handler(data);
    }
}

export const unRegisterEvent = (eventRef: Reference) => {
    const handlers = events.get(eventRef.eventName);

    if(!handlers) {
        throw new Error('Event not found');
    }

    for(let i = 0; i <= handlers.length; i++) {
        if (handlers[i] === eventRef.callback) {
            handlers.splice(i, 1);
            break;
        }
    }
}

export const removeAllEvents = () => {
    events.clear();
}
