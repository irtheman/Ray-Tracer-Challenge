from rt.tuple import *
from math import isclose

class TestTuple(object):
    epsilon = 0.00001

    def test_tupleAsPoint(self):
        a = Tuple(4.3, -4.2, 3.1, 1.0)
        assert isclose(a.x, 4.3, abs_tol=self.epsilon)
        assert isclose(a.y, -4.2, abs_tol=self.epsilon)
        assert isclose(a.z, 3.1, abs_tol=self.epsilon)
        assert isclose(a.w, 1.0, abs_tol=self.epsilon)
        assert a.isPoint
        assert not a.isVector

    def test_tupleAsVector(self):
        a = Tuple(4.3, -4.2, 3.1, 0.0)
        assert isclose(a.x, 4.3, abs_tol=self.epsilon)
        assert isclose(a.y, -4.2, abs_tol=self.epsilon)
        assert isclose(a.z, 3.1, abs_tol=self.epsilon)
        assert isclose(a.w, 0.0, abs_tol=self.epsilon)
        assert not a.isPoint
        assert a.isVector

    def test_createPoint(self):
        p = Point(4, -4, 3)
        assert p == Tuple(4, -4, 3, 1)

    def test_createVector(self):
        v = Vector(4, -4, 3)
        assert v == Tuple(4, -4, 3, 0) 