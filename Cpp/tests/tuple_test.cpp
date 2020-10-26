#include <math.h>
#include <gtest/gtest.h>
#include "tuple.h"

TEST(tuple_test_case, tuple_as_point_test)
{
    Tuple a = Tuple(4.3, -4.2, 3.1, 1.0);
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 1.0);
    EXPECT_TRUE(a.IsPoint());
    EXPECT_FALSE(a.IsVector());
}

TEST(tuple_test_case, tuple_as_vector_test)
{
    Tuple a = Tuple(4.3, -4.2, 3.1, 0.0);
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 0.0);
    EXPECT_FALSE(a.IsPoint());
    EXPECT_TRUE(a.IsVector());
}

TEST(tuple_test_case, create_point_test)
{
    Point p = Point(4, -4, 3);
    EXPECT_TRUE((Tuple)p == Tuple(4, -4, 3, 1));
}

TEST(tuple_test_case, create_vector_test)
{
    Vector v = Vector(4, -4, 3);
    EXPECT_TRUE((Tuple)v == Tuple(4, -4, 3, 0));
}

TEST(tuple_test_case, tuple_addition_test)
{
    Tuple a1 = Tuple(3, -2, 5, 1);
    Tuple a2 = Tuple(-2, 3, 1, 0);
    EXPECT_TRUE(a1 + a2 == Tuple(1, 1, 6, 1));
}

TEST(tuple_test_case, point_subtraction_test)
{
    Point p1 = Point(3, 2, 1);
    Point p2 = Point(5, 6, 7);
    EXPECT_TRUE(p1 - p2 == Vector(-2, -4, -6));
}

TEST(tuple_test_case, point_vector_subtraction_test)
{
    Point p = Point(3, 2, 1);
    Vector v = Vector(5, 6, 7);
    EXPECT_TRUE(p - v == Point(-2, -4, -6));
}

TEST(tuple_test_case, vector_subtraction_test)
{
    Vector v1 = Vector(3, 2, 1);
    Vector v2 = Vector(5, 6, 7);
    EXPECT_TRUE(v1 - v2 == Vector(-2, -4, -6));
}

TEST(tuple_test_case, vector_subtract_zero_test)
{
    Vector zero = Vector(0, 0, 0);
    Vector v = Vector(1, -2, 3);
    EXPECT_TRUE(zero - v == Vector(-1, 2, -3));
}

TEST(tuple_test_case, tuple_negating_test)
{
    Tuple a = Tuple(1, -2, 3, -4);
    EXPECT_TRUE(-a == Tuple(-1, 2, -3, 4));
}

TEST(tuple_test_case, tuple_multiply_by_scalar_test)
{
    Tuple a = Tuple(1, -2, 3, -4);
    EXPECT_TRUE(a * 3.5 == Tuple(3.5, -7, 10.5, -14));
}

TEST(tuple_test_case, tuple_multiply_by_fraction_test)
{
    Tuple a = Tuple(1, -2, 3, -4);
    EXPECT_TRUE(a * 0.5 == Tuple(0.5, -1, 1.5, -2));
}

TEST(tuple_test_case, tuple_divide_by_scalar_test)
{
    Tuple a = Tuple(1, -2, 3, -4);
    EXPECT_TRUE(a / 2 == Tuple(0.5, -1, 1.5, -2));
}

TEST(tuple_test_case, vector_magnitude1_test)
{
    Vector v = Vector(1, 0, 0);
    EXPECT_TRUE(v.Magnitude() == 1);
}

TEST(tuple_test_case, vector_magnitude2_test)
{
    Vector v = Vector(0, 1, 0);
    EXPECT_TRUE(v.Magnitude() == 1);
}

TEST(tuple_test_case, vector_magnitude3_test)
{
    Vector v = Vector(0, 0, 1);
    EXPECT_TRUE(v.Magnitude() == 1);
}

TEST(tuple_test_case, vector_magnitude4_test)
{
    Vector v = Vector(1, 2, 3);
    EXPECT_TRUE(v.Magnitude() == sqrt(14));
}

TEST(tuple_test_case, vector_magnitude5_test)
{
    Vector v = Vector(-1, -2, -3);
    EXPECT_TRUE(v.Magnitude() == sqrt(14));
}

TEST(tuple_test_case, vector_normalize1_test)
{
    Vector v = Vector(4, 0, 0);
    EXPECT_TRUE(v.Normalize() == Vector(1, 0, 0));
}

TEST(tuple_test_case, vector_normalize2_test)
{
    Vector v = Vector(1, 2, 3);
    EXPECT_TRUE(v.Normalize() == Vector(1 / sqrt(14), 2 / sqrt(14), 3 / sqrt(14)));
}

TEST(tuple_test_case, vector_magnitude_of_normalize_test)
{
    Vector v = Vector(1, 2, 3);
    Vector norm = v.Normalize();
    EXPECT_TRUE(norm.Magnitude() == 1);
}

TEST(tuple_test_case, vector_dot_product_test)
{
    Vector a = Vector(1, 2, 3);
    Vector b = Vector(2, 3, 4);
    EXPECT_TRUE(a.Dot(b) == 20);
}

TEST(tuple_test_case, vector_cross_product_test)
{
    Vector a = Vector(1, 2, 3);
    Vector b = Vector(2, 3, 4);
    EXPECT_TRUE(a.Cross(b) == Vector(-1, 2, -1));
    EXPECT_TRUE(b.Cross(a) == Vector(1, -2, 1));
}
