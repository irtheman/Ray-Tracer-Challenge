#include "tuple.h"
#include <gtest/gtest.h>

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
