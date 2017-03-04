#!/usr/bin/env python

import socket

from neural_net import Net

TCP_IP = '127.0.0.1'
TCP_PORT = 43437
BUFFER_SIZE = 20  # Normally 1024, but we want fast response

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)

train = "TRAIN"
play = "PLAY"
close = "CLOSE"
end = "END"

conn, addr = s.accept()

mode = None
queue = None
n = None

while 1:
    data = conn.recv(BUFFER_SIZE)
    data = data.decode("utf-8").strip()

    if mode == train:
        if queue == None:
            queue = []
            n = Net()
        if data == end:
            n.start_session()
            n.train(queue)
            queue = None
            mode = None
            continue

        queue.append(data);
        continue
    elif mode == play:
        print("In Play")
        continue

    if data == train:
        mode = train
        print(data)
    elif data == play:
        mode = play
        print(data)
    elif data == close:
        break
    else:
        assert(False)
conn.close()
