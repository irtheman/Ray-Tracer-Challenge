from rt.color import *
from math import isclose

class TestColor(object):
    epsilon = 0.00001

    def test_createColor(self):
        c = Color(-0.5, 0.4, 1.7)
        assert isclose(c.red, -0.5, abs_tol=self.epsilon)
        assert isclose(c.green, 0.4, abs_tol=self.epsilon)
        assert isclose(c.blue, 1.7, abs_tol=self.epsilon)

    def test_colorAddition(self):
        c1 = Color(0.9, 0.6, 0.75)
        c2 = Color(0.7, 0.1, 0.25)
        assert (c1 + c2) == Color(1.6, 0.7, 1.0)

    def test_colorSubstraction(self):
        c1 = Color(0.9, 0.6, 0.75)
        c2 = Color(0.7, 0.1, 0.25)
        assert (c1 - c2) == Color(0.2, 0.5, 0.5)

    def test_tupleMultiplyByScalar(self):
        c = Color(0.2, 0.3, 0.4)
        assert c * 2 == Color(0.4, 0.6, 0.8)
    
    def test_tupleMultiply(self):
        c1 = Color(1, 0.2, 0.4)
        c2 = Color(0.9, 1, 0.1)
        assert c1 * c2 == Color(0.9, 0.2, 0.04)
