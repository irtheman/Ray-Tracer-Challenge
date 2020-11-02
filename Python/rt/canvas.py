from rt.color import *
from math import isclose

class Canvas(object):
    def __init__(self, columns, rows):
        self.__width = columns
        self.__height = rows
        self.__canvas = [[Color(0, 0, 0) for j in range(rows)] for i in range(columns)]

    @property
    def Width(self):
        return self.__width

    @property
    def Height(self):
        return self.__height

    def __getitem__(self, key):
        column = key[0]
        row = key[1]
        if column < 0 or column >= self.__width: column = 0
        if row < 0 or row >= self.__height: row = 0

        return self.__canvas[column][row]

    def __setitem__(self, key, newvalue):
        if isinstance(newvalue, Color):
            column = key[0]
            row = key[1]
            if column < 0 or column >= self.__width: column = 0
            if row < 0 or row >= self.__height: row = 0

            self.__canvas[column][row] = newvalue

    def GetPPM(self):
        ppm = f'P3\n{self.__width} {self.__height}\n255\n'

        rgb = [0, 0, 0]
        length = 0
        for y in range(self.__height):
            for x in range(self.__width):
                c = self.__canvas[x][y]
                rgb[0] = int(round(c.red * 255))
                rgb[1] = int(round(c.green * 255))
                rgb[2] = int(round(c.blue * 255))

                for i in range(3):
                    if rgb[i] < 0: rgb[i] = 0
                    if rgb[i] > 255: rgb[i] = 255

                    num = str(rgb[i])
                    if (length + len(num) + 1 >= 70):
                        ppm += '\n'
                        length = 0
                    elif (length > 0):
                        ppm += ' '
                        length += 1

                    ppm += num
                    length += len(num)

            ppm += '\n'
            length = 0

        return ppm
