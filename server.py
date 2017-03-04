#!/usr/bin/env python

import socket

from neural_net import Net

TCP_IP = '127.0.0.1'
TCP_PORT = 43437
BUFFER_SIZE = 10  # Normally 1024, but we want fast response

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

    if mode == train:
        if queue == None:
            queue = ""
            n = Net()
            n.random_weights()

        queue += data;

        if queue.strip()[-3:] == end:
            queue = queue.strip()[:-3]
            n.start_session()
            #n.train(queue.split(",")[:-1])
            n.save_to_file("models/neural")
            queue = None
            mode = None
            break
        continue
    elif mode == play:
        if queue == None:
          queue = ""
          n = Net()
          n.random_weights()
          n.weights_from_file("models/neural")

        queue += data
        print("Got queue data")

        if len(queue.split(",")) > 1:
          parsed = queue.split(",")[-2]
          queue = queue.split(",")[-1]

          result = n.propagate([[float(x) for x in parsed.split(" ")]])
          result = result[0]
          print(result[0], " ", result[1], " ", result[2])
          if (result[0] > result[1] and result[0] > result[2]):
            print("GO UP")
          elif result[1] > result[2]:
            print("GO DOWN")
          else:
            print("STAY IN SAME PLACE")

          conn.send("{} {} {}".format(result[0], result[1], result[2]).encode("utf-8"))
          
        
        continue

conn.close()
