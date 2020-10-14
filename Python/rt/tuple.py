from math import isclose

class Tuple(object):

    epsilon = 0.00001

    def __init__(self, x, y, z, w):
        self._x = x
        self._y = y
        self._z = z
        self._w = w

    @property
    def x(self):
        return self._x

    @x.setter
    def x(self, value):
        self._x = value

    @property
    def y(self):
        return self._y

    @y.setter
    def y(self, value):
        self._y = value

    @property
    def z(self):
        return self._z

    @z.setter
    def z(self, value):
        self._z = value

    @property
    def w(self):
        return self._w

    @w.setter
    def w(self, value):
        self._w = value

    @property
    def isPoint(self):
        return isclose(self._w, 1.0, abs_tol = self.epsilon)

    @property
    def isVector(self):
        return isclose(self._w, 0.0, abs_tol = self.epsilon)
        
    def __eq__(self, other):
        return isclose(self._x, other.x, abs_tol = self.epsilon) and \
               isclose(self._y, other.y, abs_tol = self.epsilon) and \
               isclose(self._z, other.z, abs_tol = self.epsilon) and \
               isclose(self._w, other.w, abs_tol = self.epsilon)

    def __hash__(self):
        return hash(self._x, self._y, self._z, self._w)

    def __str__(self):
        return f'({self._x}, {self._y}, {self._z}, {self._w})'

class Point(Tuple):
    def __init__(self, x, y, z):
        super().__init__(x, y, z, 1.0)

class Vector(Tuple):
    def __init__(self, x, y, z):
        super().__init__(x, y, z, 0.0)
