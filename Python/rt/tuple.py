from math import isclose, sqrt

class Tuple(object):

    epsilon = 0.00001

    def __init__(self, x, y, z, w):
        self.__x = x
        self.__y = y
        self.__z = z
        self.__w = w

    @property
    def x(self):
        return self.__x

    @x.setter
    def x(self, value):
        self.__x = value

    @property
    def y(self):
        return self.__y

    @y.setter
    def y(self, value):
        self.__y = value

    @property
    def z(self):
        return self.__z

    @z.setter
    def z(self, value):
        self.__z = value

    @property
    def w(self):
        return self.__w

    @w.setter
    def w(self, value):
        self.__w = value

    @property
    def isPoint(self):
        return isclose(self.__w, 1.0, abs_tol = self.epsilon)

    @property
    def isVector(self):
        return isclose(self.__w, 0.0, abs_tol = self.epsilon)
    
    def __add__(self, other):
        if isinstance(other, Tuple):
            return Tuple(
                self.__x + other.x,
                self.__y + other.y,
                self.__z + other.z,
                self.__w + other.w)
    
    def __sub__(self, other):
        if isinstance(other, Tuple):
            return Tuple(
                self.__x - other.x,
                self.__y - other.y,
                self.__z - other.z,
                self.__w - other.w)
    
    def __mul__(self, other):
        if isinstance(other, (int, float)):
            return Tuple(
                self.__x * other,
                self.__y * other,
                self.__z * other,
                self.__w * other)
    
    def __truediv__(self, other):
        if isinstance(other, (int, float)):
            return Tuple(
                self.__x / other,
                self.__y / other,
                self.__z / other,
                self.__w / other)
    
    def __neg__(self):
        return Tuple(-self.__x, -self.__y, -self.__z, -self.__w)

    def __eq__(self, other):
        return isinstance(other, Tuple) and \
               isclose(self.__x, other.x, abs_tol = self.epsilon) and \
               isclose(self.__y, other.y, abs_tol = self.epsilon) and \
               isclose(self.__z, other.z, abs_tol = self.epsilon) and \
               isclose(self.__w, other.w, abs_tol = self.epsilon)

    def __hash__(self):
        return hash(self.__x, self.__y, self.__z, self.__w)

    def __str__(self):
        return f'({self.__x}, {self.__y}, {self.__z}, {self.__w})'



class Point(Tuple):
    def __init__(self, x, y, z):
        super().__init__(x, y, z, 1.0)



class Vector(Tuple):
    def __init__(self, x, y, z):
        super().__init__(x, y, z, 0.0)

    @property
    def magnitude(self):
        return sqrt(self.x * self.x + \
                    self.y * self.y + \
                    self.z * self.z + \
                    self.w * self.w)

    @property
    def normalize(self):
        mag = self.magnitude
        v = Vector(0, 0, 0)
        v.x = self.x / mag
        v.y = self.y / mag
        v.z = self.z / mag
        v.w = self.w / mag
        return v

    def dot(self, other):
        if isinstance(other, Vector):
            return self.x * other.x + \
                   self.y * other.y + \
                   self.z * other.z + \
                   self.w * other.w

    def cross(self, other):
        if isinstance(other, Vector):
            return Vector(self.y * other.z - self.z * other.y,
                          self.z * other.x - self.x * other.z,
                          self.x * other.y - self.y * other.x)