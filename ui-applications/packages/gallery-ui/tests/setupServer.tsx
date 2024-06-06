import { Server, Socket } from "socket.io";
import { io as ioc, Socket as ClientSocket  } from "socket.io-client";
import { createServer } from "http";
import { PhotoItem } from "../src/types/GalleryApiTypes";

export function handleSocketEvents(io: Server, socket: Socket) {

  const socketsEvents: Record<string, (data: any) => void> = {
    "multiply-by-2": (data) => {
      const result = data * 2;
      socket.emit("multiplied-by-2", result);
    },
  };
  
    for (const event in socketsEvents) {
      socket.on(event, socketsEvents[event]);
    }
  }

export function waitForSocket(emitter: ClientSocket, event: string) {
    return new Promise<any>((resolve) => {
      console.log(global.window.location.href);
      emitter.once(event, resolve);
    });
  }

export async function setupTestServer() {
    const httpServer = createServer();
  
    const io = new Server(httpServer);
    httpServer.listen(3000, "https://localhost:3000/");
  
    const clientSocket = ioc(`${import.meta.env.VITE_GALLERY_SERVICE_URL}`, {
      transports: ["websocket", "polling", "https"],
    });
  
    let serverSocket: Socket | undefined = undefined;
    io.on("connection", (connectedSocket) => {
      serverSocket = connectedSocket;
      handleSocketEvents(io, serverSocket);
    });
  
    await waitForSocket(clientSocket, "connect");
  
    return { io, clientSocket, serverSocket };
  }