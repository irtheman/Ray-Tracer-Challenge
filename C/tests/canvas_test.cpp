#include <string>
#include <vector>
#include <gtest/gtest.h>

extern "C" {
#include "color.h"
#include "canvas.h"
}

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
    Canvas c = create_canvas(10, 20);
    Color black = create_color(0, 0 ,0);

    EXPECT_EQ(c.width, 10);
    EXPECT_EQ(c.height, 20);

    for (int i = 0; i < c.width; i++)
    {
        for (int j = 0; j < c.height; j++)
        {
            EXPECT_TRUE(is_equal_color(getPixel(c, i, j), black));
        }
    }
}

TEST(canvas_test_case, canvas_write_test)
{
    Canvas c = create_canvas(10, 20);
    Color red = create_color(1, 0, 0);
    setPixel(c, 2, 3, red);
    
    EXPECT_TRUE(is_equal_color(getPixel(c, 2, 3), red));
}

TEST(canvas_test_case, canvas_header_test)
{
    Canvas c = create_canvas(5, 3);
    std::string ppm = getPPM(c);
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[0], "P3");
    EXPECT_EQ(lines[1], "5 3");
    EXPECT_EQ(lines[2], "255");    
}

TEST(canvas_test_case, canvas_ppm_pixel_data_test)
{
    Canvas c = create_canvas(5, 3);

    Color c1 = create_color(1.5, 0, 0);
    Color c2 = create_color(0, 0.5, 0);
    Color c3 = create_color(-0.5, 0, 1);

    setPixel(c, 0, 0, c1);
    setPixel(c, 2, 1, c2);
    setPixel(c, 4, 2, c3);

    std::string ppm = getPPM(c);
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[3], "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
    EXPECT_EQ(lines[4], "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0");
    EXPECT_EQ(lines[5], "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");
}

TEST(canvas_test_case, canvas_ppm_long_lines_test)
{
    Canvas c = create_canvas(10, 2);
    Color p = create_color(1, 0.8, 0.6);

    for (int i = 0; i < c.width; i++)
    {
        for (int j = 0; j < c.height; j++)
        {
            setPixel(c, i, j, p);
        }
    }

    std::string ppm = getPPM(c);
    auto lines = split_string_by_newline(ppm);

    EXPECT_EQ(lines[3], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
    EXPECT_EQ(lines[4], "153 255 204 153 255 204 153 255 204 153 255 204 153");
    EXPECT_EQ(lines[5], "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
    EXPECT_EQ(lines[6], "153 255 204 153 255 204 153 255 204 153 255 204 153");
}

TEST(canvas_test_case, canvas_ppm_ends_newline_test)
{
    Canvas c = create_canvas(5, 3);
    std::string ppm = getPPM(c);
    EXPECT_TRUE(ppm[ppm.length() - 1] == '\n');
}