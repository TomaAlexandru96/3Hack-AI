#!/usr/bin/env python

import socket

from neural_net import Net

TCP_IP = '127.0.0.1'
TCP_PORT = 43437
BUFFER_SIZE = 40  # Normally 1024, but we want fast response

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)

train = "TRIN"
play = "PLAY"
close = "CLOSE"
end = "END"

conn, addr = s.accept()

mode = None
queue = None
n = None

data = conn.recv(4).decode("utf-8")
if data == train:
  mode = train
elif data == play:
  mode = play

while 1:
    data = conn.recv(BUFFER_SIZE)
    data = data.decode("utf-8")
    print(mode)

    if mode == train:
        if queue == None:
            queue = ""
            n = Net()

        queue += data;

        if queue.strip()[-3:] == end:
            queue = queue.strip()[:-3]
            n.start_session()
            n.train(queue.split(",")[:-1])
            queue = None
            mode = None
            break
        continue
    elif mode == play:
        if queue == None:
          queue = ""
          n = Net()

        while 1:
          queue += data;
          data = conn.recv(BUFFER_SIZE).decode("utf-8")
          if not data: break

        print(queue)

        if len(queue.split(",")) > 1:
          parsed = queue.split(",")[-2]
          queue = queue.split(",")[-1]

          print("Split queue into ", parsed, " and ", queue)
          result = n.propagate(parsed.split(" "))
          print("Received result ", result)
          conn.write("% % %" % (result[0], result[1], result[2]))
          
        
        continue

conn.close()
