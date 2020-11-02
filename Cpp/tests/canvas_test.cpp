#include <string>
#include <vector>
#include <gtest/gtest.h>
#include "color.h"
#include "canvas.h"

std::vector<std::string> split_string_by_newline(const std::string& str)
{
    auto result = std::vector<std::string>{};
    auto ss = std::stringstream{str};

    for (std::string line; std::getline(ss, line, '\n');)
        result.push_back(line);

    return result;
}

TEST(canvas_test_case, canvas_create_test)
{
    Canvas c = Canvas(10, 20);
    Color black = Color(0, 0, 0);

    EXPECT_EQ(c.Width(), 10);
    EXPECT_EQ(c.Height(), 20);

    for (int i = 0; i < c.Width(); i++)
    {
        for (int j = 0; j < c.Height(); j++)
        {
            EXPECT_TRUE(c(i, j) == black);
        }
    }
}

TEST(canvas_test_case, canvas_write_test)
{
    Canvas c = Canvas(10, 20);
    Color red = Color(1, 0, 0);
    c(2, 3) = red;
    
    EXPECT_TRUE(c(2, 3) == red);
}

TEST(canvas_test_case, canvas_header_test)
{
    Canvas c = Canvas(5, 3);
    std::string ppm = c.GetPPM();
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[0], "P3");
    EXPECT_EQ(lines[1], "5 3");
    EXPECT_EQ(lines[2], "255");    
}

TEST(canvas_test_case, canvas_ppm_pixel_data_test)
{
    Canvas c = Canvas(5, 3);

    Color c1 = Color(1.5, 0, 0);
    Color c2 = Color(0, 0.5, 0);
    Color c3 = Color(-0.5, 0, 1);

    c(0, 0) = c1;
    c(2, 1) = c2;
    c(4, 2) = c3;

    std::string ppm = c.GetPPM();
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[3], "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
    EXPECT_EQ(lines[4], "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0");
    EXPECT_EQ(lines[5], "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");
}

TEST(canvas_test_case, canvas_ppm_long_lines_test)
{
    Canvas c = Canvas(10, 2);
    Color p = Color(1, 0.8, 0.6);

    for (int i = 0; i < c.Width(); i++)
    {
        for (int j = 0; j < c.Height(); j++)
        {
            c(i, j) = p;
        }
    }

    std::string ppm = c.GetPPM();
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[3], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
    EXPECT_EQ(lines[4], "153 255 204 153 255 204 153 255 204 153 255 204 153");
    EXPECT_EQ(lines[5], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
    EXPECT_EQ(lines[6], "153 255 204 153 255 204 153 255 204 153 255 204 153");
}

TEST(canvas_test_case, canvas_ppm_ends_newline_test)
{
    Canvas c = Canvas(5, 3);
    std::string ppm = c.GetPPM();
    EXPECT_TRUE(ppm[ppm.length() - 1] == '\n');
}