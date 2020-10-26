from rt.tuple import *
from math import isclose, sqrt

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

    def test_tupleAddition(self):
        a1 = Tuple(3, -2, 5, 1)
        a2 = Tuple(-2, 3, 1, 0)
        assert (a1 + a2) == Tuple(1, 1, 6, 1)

    def test_tupleSubstraction(self):
        p1 = Point(3, 2, 1)
        p2 = Point(5, 6, 7)
        assert (p1 - p2) == Vector(-2, -4, -6)

    def test_pointVectorSubstraction(self):
        p = Point(3, 2, 1)
        v = Vector(5, 6, 7)
        assert (p - v) == Point(-2, -4, -6)

    def test_vectorSubtraction(self):
        v1 = Vector(3, 2, 1)
        v2 = Vector(5, 6, 7)
        assert (v1 - v2) == Vector(-2, -4, -6)

    def test_vectorSubtractFromZero(self):
        zero = Vector(0, 0, 0)
        v = Vector(1, -2, 3)
        assert (zero - v) == Vector(-1, 2, -3)
    
    def test_tupleNegating(self):
        a = Tuple(1, -2, 3, -4)
        assert -a == Tuple(-1, 2, -3, 4)

    def test_tupleMultiplyByScalar(self):
        a = Tuple(1, -2, 3, -4)
        assert a * 3.5 == Tuple(3.5, -7, 10.5, -14)
    
    def test_tupleMultiplyByFraction(self):
        a = Tuple(1, -2, 3, -4)
        assert a * 0.5 == Tuple(0.5, -1, 1.5, -2)
    
    def test_tupleDivideByScalar(self):
        a = Tuple(1, -2, 3, -4)
        assert a / 2 == Tuple(0.5, -1, 1.5, -2)

    def test_vectorMagnitude1(self):
        v = Vector(1, 0, 0)
        assert v.magnitude == 1
    
    def test_vectorMagnitude2(self):
        v = Vector(0, 1, 0)
        assert v.magnitude == 1
     
    def test_vectorMagnitude3(self):
        v = Vector(0, 0, 1)
        assert v.magnitude == 1
    
    def test_vectorMagnitude4(self):
        v = Vector(1, 2, 3)
        assert v.magnitude == sqrt(14)
    
    def test_vectorMagnitude5(self):
        v = Vector(-1, -2, -3)
        assert v.magnitude == sqrt(14)
    
    def testV_vectorNormalize1(self):
        v = Vector(4, 0, 0)
        assert v.normalize == Vector(1, 0, 0)
    
    def test_vectorNormalize2(self):
        v = Vector(1, 2, 3)
        assert v.normalize == Vector(1 / sqrt(14), 2 / sqrt(14), 3 / sqrt(14))
    
    def test_vectorMagnitudeOfNormalize(self):
        v = Vector(1, 2, 3)
        norm = v.normalize
        assert norm.magnitude == 1
    
    def test_vectorDotProduct(self):
        a = Vector(1, 2, 3)
        b = Vector(2, 3, 4)
        assert a.dot(b) == 20
    
    def test_vectorCrossProduct(self):
        a = Vector(1, 2, 3)
        b = Vector(2, 3, 4)
        assert a.cross(b) == Vector(-1, 2, -1)
        assert b.cross(a) == Vector(1, -2, 1)