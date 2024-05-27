import http, { IncomingMessage, ServerResponse } from "http";
import createWebSocketServer from "./creteWebSocketServer";

function startServer(port: any): Promise<http.Server<typeof IncomingMessage, typeof ServerResponse>> {
    const server = http.createServer();
    createWebSocketServer(server);

    return new Promise((resolve) => {
        server.listen(port, () => resolve(server));
    });
}

function waitForSocketState(socket: { readyState: any; }, state: any) {
    return new Promise<void>(function (resolve) {
        setTimeout(function () {
            if (socket.readyState === state) {
                resolve();
            } else {
                waitForSocketState(socket, state).then(resolve);
            }
        }, 5);
    });
}

export { startServer, waitForSocketState };