#include <gtest/gtest.h>

extern "C" {
#include "tuple.h"
}

TEST(tuple_test_case, tuple_as_point_test)
{
    struct Tuple a = create_tuple(4.3, -4.2, 3.1, 1.0);
    EXPECT_DOUBLE_EQ(a.x, 4.3);
    EXPECT_DOUBLE_EQ(a.y, -4.2);
    EXPECT_DOUBLE_EQ(a.z, 3.1);
    EXPECT_DOUBLE_EQ(a.w, 1.0);
    EXPECT_TRUE(is_point(a));
    EXPECT_FALSE(is_vector(a));
}

TEST(tuple_test_case, tuple_as_vector_test)
{
    struct Tuple a = create_tuple(4.3, -4.2, 3.1, 0.0);
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
    struct Tuple v = create_vector(4, -4, 3);
    struct Tuple e = {4, -4, 3, 0};
    EXPECT_TRUE(is_equal(v, e));
}
