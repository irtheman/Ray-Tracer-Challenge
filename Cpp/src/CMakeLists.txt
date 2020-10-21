cmake_minimum_required(VERSION 3.18.4)

project(rtc)

include_directories(include)
file(GLOB SOURCES "*.cpp")
list(REMOVE_ITEM SOURCES "main.cpp")

add_library(rtclib STATIC ${SOURCES})

add_executable(rtc main.cpp)

target_link_libraries(rtc rtclib)