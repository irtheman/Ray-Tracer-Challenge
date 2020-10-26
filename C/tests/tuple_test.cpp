#include <math.h>
#include <gtest/gtest.h>
extern "C" {
    #include "tuple.h"
}

TEST(tuple_test_case, tuple_as_point_test)
{
    struct Tuple a = { 4.3, -4.2, 3.1, 1.0 };
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 1.0);
    EXPECT_TRUE(is_point(a));
    EXPECT_FALSE(is_vector(a));
}

TEST(tuple_test_case, tuple_as_vector_test)
{
    struct Tuple a = { 4.3, -4.2, 3.1, 0.0 };
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 0.0);
    EXPECT_FALSE(is_point(a));
    EXPECT_TRUE(is_vector(a));
}

TEST(tuple_test_case, create_point_test)
{
    struct Tuple p = create_point(4, -4, 3);
    struct Tuple e = {4, -4, 3, 1};
    EXPECT_TRUE(is_equal(p, e));
}

TEST(tuple_test_case, create_vector_test)
{
    Tuple v = create_vector(4, -4, 3);
    struct Tuple e = {4, -4, 3, 0};
    EXPECT_TRUE(is_equal(v, e));
}

TEST(tuple_test_case, tuple_addition_test)
{
    struct Tuple a1 = {3, -2, 5, 1};
    struct Tuple a2 = {-2, 3, 1, 0};
    struct Tuple e = {1, 1, 6, 1};
    EXPECT_TRUE(is_equal(tadd(a1, a2), e));
}

TEST(tuple_test_case, point_subtraction_test)
{
    struct Tuple p1 = create_point(3, 2, 1);
    struct Tuple p2 = create_point(5, 6, 7);
    struct Tuple e = create_vector(-2, -4, -6);
    EXPECT_TRUE(is_equal(tsub(p1, p2), e));
}

TEST(tuple_test_case, point_vector_subtraction_test)
{
    struct Tuple p = create_point(3, 2, 1);
    struct Tuple v = create_vector(5, 6, 7);
    struct Tuple e = create_point(-2, -4, -6);
    EXPECT_TRUE(is_equal(tsub(p, v), e));
}

TEST(tuple_test_case, vector_subtraction_test)
{
    struct Tuple v1 = create_vector(3, 2, 1);
    struct Tuple v2 = create_vector(5, 6, 7);
    struct Tuple e = create_vector(-2, -4, -6);
    EXPECT_TRUE(is_equal(tsub(v1, v2), e));
}

TEST(tuple_test_case, vector_subtract_zero_test)
{
    struct Tuple zero = create_vector(0, 0, 0);
    struct Tuple v = create_vector(1, -2, 3);
    EXPECT_TRUE(is_equal(tsub(zero, v), create_vector(-1, 2, -3)));
}

TEST(tuple_test_case, tuple_negating_test)
{
    struct Tuple a = {1, -2, 3, -4};
    struct Tuple e = {-1, 2, -3, 4};
    EXPECT_TRUE(is_equal(tneg(a), e));
}

TEST(tuple_test_case, tuple_multiply_by_scalar_test)
{
    struct Tuple a = {1, -2, 3, -4};
    struct Tuple e = {3.5, -7, 10.5, -14};
    EXPECT_TRUE(is_equal(tmul(a, 3.5), e));
}

TEST(tuple_test_case, tuple_multiply_by_fraction_test)
{
    struct Tuple a = {1, -2, 3, -4};
    struct Tuple e = {0.5, -1, 1.5, -2};
    EXPECT_TRUE(is_equal(tmul(a, 0.5), e));
}

TEST(tuple_test_case, tuple_divide_by_scalar_test)
{
    struct Tuple a = {1, -2, 3, -4};
    struct Tuple e = {0.5, -1, 1.5, -2};
    EXPECT_TRUE(is_equal(tdiv(a, 2), e));
}

TEST(tuple_test_case, vector_magnitude1_test)
{
     struct Tuple v = create_vector(1, 0, 0);
    EXPECT_DOUBLE_EQ(tmag(v), 1);
}

TEST(tuple_test_case, vector_magnitude2_test)
{
     struct Tuple v = create_vector(0, 1, 0);
    EXPECT_DOUBLE_EQ(tmag(v), 1);
}

TEST(tuple_test_case, vector_magnitude3_test)
{
     struct Tuple v = create_vector(0, 0, 1);
    EXPECT_DOUBLE_EQ(tmag(v), 1);
}

TEST(tuple_test_case, vector_magnitude4_test)
{
     struct Tuple v = create_vector(1, 2, 3);
    EXPECT_DOUBLE_EQ(tmag(v), sqrt(14));
}

TEST(tuple_test_case, vector_magnitude5_test)
{
     struct Tuple v = create_vector(-1, -2, -3);
    EXPECT_DOUBLE_EQ(tmag(v), sqrt(14));
}

TEST(tuple_test_case, vector_normalize1_test)
{
     struct Tuple v = create_vector(4, 0, 0);
    EXPECT_TRUE(is_equal(tnorm(v), create_vector(1, 0, 0)));
}

TEST(tuple_test_case, vector_normalize2_test)
{
     struct Tuple v = create_vector(1, 2, 3);
    EXPECT_TRUE(is_equal(tnorm(v), create_vector(1 / sqrt(14), 2 / sqrt(14), 3 / sqrt(14))));
}

TEST(tuple_test_case, vector_magnitude_of_normalize_test)
{
     struct Tuple v = create_vector(1, 2, 3);
     struct Tuple norm = tnorm(v);
    EXPECT_DOUBLE_EQ(tmag(norm), 1);
}

TEST(tuple_test_case, vector_dot_product_test)
{
     struct Tuple a = create_vector(1, 2, 3);
     struct Tuple b = create_vector(2, 3, 4);
    EXPECT_DOUBLE_EQ(vdot(a, b), 20);
}

TEST(tuple_test_case, vector_cross_product_test)
{
     struct Tuple a = create_vector(1, 2, 3);
     struct Tuple b = create_vector(2, 3, 4);
    EXPECT_TRUE(is_equal(vcross(a, b), create_vector(-1, 2, -1)));
    EXPECT_TRUE(is_equal(vcross(b, a), create_vector(1, -2, 1)));
}
