import sys

from neural_net import Net

n = Net()
n.start_session()
n.train(["1 2 3 4 5 6 1 1 1" for _ in range(10000)])
print(n.propagate([[1.0, 2, 3, 4, 5, 6]]))
