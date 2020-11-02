#include <math.h>
#include <gtest/gtest.h>

extern "C" {
#include "color.h"
}

TEST(color_test_case, color_create_test)
{
    Color c = create_color(-0.5, 0.4, 1.7);
    EXPECT_DOUBLE_EQ(c.red, -0.5);
    EXPECT_DOUBLE_EQ(c.green, 0.4);
    EXPECT_DOUBLE_EQ(c.blue, 1.7);
}

TEST(color_test_case, color_add_test)
{
    Color c1 = create_color(0.9, 0.6, 0.75);
    Color c2 = create_color(0.7, 0.1, 0.25);
    EXPECT_TRUE(is_equal_color(addc(c1, c2), create_color(1.6, 0.7, 1.0)));
}

TEST(color_test_case, color_subtract_test)
{
    Color c1 = create_color(0.9, 0.6, 0.75);
    Color c2 = create_color(0.7, 0.1, 0.25);
    EXPECT_TRUE(is_equal_color(subc(c1, c2), create_color(0.2, 0.5, 0.5)));
}

TEST(color_test_case, color_multiply_by_scalar_test)
{
    Color c = create_color(0.2, 0.3, 0.4);
    EXPECT_TRUE(is_equal_color(mulcs(c, 2), create_color(0.4, 0.6, 0.8)));
}

TEST(color_test_case, color_multiply_test)
{
    Color c1 = create_color(1, 0.2, 0.4);
    Color c2 = create_color(0.9, 1, 0.1);
    EXPECT_TRUE(is_equal_color(mulc(c1, c2), create_color(0.9, 0.2, 0.04)));
}