#include <math.h>
#include <gtest/gtest.h>
#include "color.h"

TEST(color_test_case, color_create_test)
{
    Color c = Color(-0.5, 0.4, 1.7);
    EXPECT_DOUBLE_EQ(c.red, -0.5);
    EXPECT_DOUBLE_EQ(c.green, 0.4);
    EXPECT_DOUBLE_EQ(c.blue, 1.7);
}

TEST(color_test_case, color_add_test)
{
    Color c1 = Color(0.9, 0.6, 0.75);
    Color c2 = Color(0.7, 0.1, 0.25);
    EXPECT_TRUE(c1 + c2 == Color(1.6, 0.7, 1.0));
}

TEST(color_test_case, color_subtract_test)
{
    Color c1 = Color(0.9, 0.6, 0.75);
    Color c2 = Color(0.7, 0.1, 0.25);
    EXPECT_TRUE(c1 - c2 == Color(0.2, 0.5, 0.5));
}

TEST(color_test_case, color_multiply_by_scalar_test)
{
    Color c = Color(0.2, 0.3, 0.4);
    EXPECT_TRUE(c * 2 == Color(0.4, 0.6, 0.8));
}

TEST(color_test_case, color_multiply_test)
{
    Color c1 = Color(1, 0.2, 0.4);
    Color c2 = Color(0.9, 1, 0.1);
    EXPECT_TRUE(c1 * c2 == Color(0.9, 0.2, 0.04));
}