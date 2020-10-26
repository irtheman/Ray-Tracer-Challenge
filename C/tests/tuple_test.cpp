#include <math.h>
#include <gtest/gtest.h>
extern "C" {
    #include "tuple.h"
}

TEST(tuple_test_case, tuple_as_point_test)
{
    Tuple a = { 4.3, -4.2, 3.1, 1.0 };
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 1.0);
    EXPECT_TRUE(is_point(a));
    EXPECT_FALSE(is_vector(a));
}

TEST(tuple_test_case, tuple_as_vector_test)
{
    Tuple a = { 4.3, -4.2, 3.1, 0.0 };
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 0.0);
    EXPECT_FALSE(is_point(a));
    EXPECT_TRUE(is_vector(a));
}

TEST(tuple_test_case, create_point_test)
{
    Tuple p = create_point(4, -4, 3);
    Tuple e = {4, -4, 3, 1};
    EXPECT_TRUE(is_equal(p, e));
}

TEST(tuple_test_case, create_vector_test)
{
    Tuple v = create_vector(4, -4, 3);
    Tuple e = {4, -4, 3, 0};
    EXPECT_TRUE(is_equal(v, e));
}

TEST(tuple_test_case, tuple_addition_test)
{
    Tuple a1 = {3, -2, 5, 1};
    Tuple a2 = {-2, 3, 1, 0};
    Tuple e = {1, 1, 6, 1};
    EXPECT_TRUE(is_equal(addt(a1, a2), e));
}

TEST(tuple_test_case, point_subtraction_test)
{
    Tuple p1 = create_point(3, 2, 1);
    Tuple p2 = create_point(5, 6, 7);
    Tuple e = create_vector(-2, -4, -6);
    EXPECT_TRUE(is_equal(subt(p1, p2), e));
}

TEST(tuple_test_case, point_vector_subtraction_test)
{
    Tuple p = create_point(3, 2, 1);
    Tuple v = create_vector(5, 6, 7);
    Tuple e = create_point(-2, -4, -6);
    EXPECT_TRUE(is_equal(subt(p, v), e));
}

TEST(tuple_test_case, vector_subtraction_test)
{
    Tuple v1 = create_vector(3, 2, 1);
    Tuple v2 = create_vector(5, 6, 7);
    Tuple e = create_vector(-2, -4, -6);
    EXPECT_TRUE(is_equal(subt(v1, v2), e));
}

TEST(tuple_test_case, vector_subtract_zero_test)
{
    Tuple zero = create_vector(0, 0, 0);
    Tuple v = create_vector(1, -2, 3);
    EXPECT_TRUE(is_equal(subt(zero, v), create_vector(-1, 2, -3)));
}

TEST(tuple_test_case, tuple_negating_test)
{
    Tuple a = {1, -2, 3, -4};
    Tuple e = {-1, 2, -3, 4};
    EXPECT_TRUE(is_equal(tneg(a), e));
}

TEST(tuple_test_case, tuple_multiply_by_scalar_test)
{
    Tuple a = {1, -2, 3, -4};
    Tuple e = {3.5, -7, 10.5, -14};
    EXPECT_TRUE(is_equal(mult(a, 3.5), e));
}

TEST(tuple_test_case, tuple_multiply_by_fraction_test)
{
    Tuple a = {1, -2, 3, -4};
    Tuple e = {0.5, -1, 1.5, -2};
    EXPECT_TRUE(is_equal(mult(a, 0.5), e));
}

TEST(tuple_test_case, tuple_divide_by_scalar_test)
{
    Tuple a = {1, -2, 3, -4};
    Tuple e = {0.5, -1, 1.5, -2};
    EXPECT_TRUE(is_equal(divt(a, 2), e));
}

TEST(tuple_test_case, vector_magnitude1_test)
{
     Tuple v = create_vector(1, 0, 0);
    EXPECT_DOUBLE_EQ(magv(v), 1);
}

TEST(tuple_test_case, vector_magnitude2_test)
{
     Tuple v = create_vector(0, 1, 0);
    EXPECT_DOUBLE_EQ(magv(v), 1);
}

TEST(tuple_test_case, vector_magnitude3_test)
{
     Tuple v = create_vector(0, 0, 1);
    EXPECT_DOUBLE_EQ(magv(v), 1);
}

TEST(tuple_test_case, vector_magnitude4_test)
{
     Tuple v = create_vector(1, 2, 3);
    EXPECT_DOUBLE_EQ(magv(v), sqrt(14));
}

TEST(tuple_test_case, vector_magnitude5_test)
{
     Tuple v = create_vector(-1, -2, -3);
    EXPECT_DOUBLE_EQ(magv(v), sqrt(14));
}

TEST(tuple_test_case, vector_normalize1_test)
{
     Tuple v = create_vector(4, 0, 0);
    EXPECT_TRUE(is_equal(normv(v), create_vector(1, 0, 0)));
}

TEST(tuple_test_case, vector_normalize2_test)
{
     Tuple v = create_vector(1, 2, 3);
    EXPECT_TRUE(is_equal(normv(v), create_vector(1 / sqrt(14), 2 / sqrt(14), 3 / sqrt(14))));
}

TEST(tuple_test_case, vector_magnitude_of_normalize_test)
{
     Tuple v = create_vector(1, 2, 3);
     Tuple norm = normv(v);
    EXPECT_DOUBLE_EQ(magv(norm), 1);
}

TEST(tuple_test_case, vector_dot_product_test)
{
     Tuple a = create_vector(1, 2, 3);
     Tuple b = create_vector(2, 3, 4);
    EXPECT_DOUBLE_EQ(dotv(a, b), 20);
}

TEST(tuple_test_case, vector_cross_product_test)
{
     Tuple a = create_vector(1, 2, 3);
     Tuple b = create_vector(2, 3, 4);
    EXPECT_TRUE(is_equal(crossv(a, b), create_vector(-1, 2, -1)));
    EXPECT_TRUE(is_equal(crossv(b, a), create_vector(1, -2, 1)));
}
