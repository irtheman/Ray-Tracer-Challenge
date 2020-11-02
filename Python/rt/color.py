from math import isclose

class Color(object):

    epsilon = 0.00001

    def __init__(self, Red, Green, Blue):
        self.__red = Red
        self.__green = Green
        self.__blue = Blue

    @property
    def red(self):
        return self.__red

    @red.setter
    def red(self, value):
        self.__red = value

    @property
    def green(self):
        return self.__green

    @green.setter
    def green(self, value):
        self.__green = value

    @property
    def blue(self):
        return self.__blue

    @blue.setter
    def blue(self, value):
        self.__blue = value
    
    def __add__(self, other):
        if isinstance(other, Color):
            return Color(
                self.__red + other.red,
                self.__green + other.green,
                self.__blue + other.blue)
    
    def __sub__(self, other):
        if isinstance(other, Color):
            return Color(
                self.__red - other.red,
                self.__green - other.green,
                self.__blue - other.blue)
    
    def __mul__(self, other):
        if isinstance(other, (int, float)):
            return Color(
                self.__red * other,
                self.__green * other,
                self.__blue * other)
        elif isinstance(other, Color):
            return Color(
                self.__red * other.red,
                self.__green * other.green,
                self.__blue * other.blue)
    
    def __eq__(self, other):
        return isinstance(other, Color) and \
               isclose(self.__red, other.red, abs_tol = self.epsilon) and \
               isclose(self.__green, other.green, abs_tol = self.epsilon) and \
               isclose(self.__blue, other.blue, abs_tol = self.epsilon)

    def __hash__(self):
        return hash(self.__red, self.__green, self.__blue)

    def __str__(self):
        return f'({self.__red}, {self.__green}, {self.__blue})'
