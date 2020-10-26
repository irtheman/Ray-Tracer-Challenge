cmake_minimum_required(VERSION 3.18.4)

project(rtcTests)

# Locate GTest
enable_testing()
find_package(GTest REQUIRED)
include_directories(include ${GTEST_INCLUDE_DIRS})

# find_library(RTC_LIBRARY rtclib HINTS $"{CMAKE_PREFIX_PATH}/build/src" REQUIRED)

file(GLOB TEST_SOURCES *.cpp)
set(SOURCES ${TEST_SOURCES})

# Add test cpp file
add_executable(rtcTests ${SOURCES})

# Link test executable against gtest & gtest_main
target_link_libraries(rtcTests LINK_PUBLIC rtclib m ${GTEST_LIBRARIES} ${GTEST_MAIN_LIBRARIES} gtest_main pthread)

add_test(
    NAME rtcTests
    COMMAND rtcTests
)