cmake_minimum_required(VERSION 3.18.4)

project(rtc)

include_directories(include)
file(GLOB SOURCES "*.c")
list(REMOVE_ITEM SOURCES "main.c")
list(REMOVE_ITEM SOURCES "projectile.c")

add_library(rtclib STATIC ${SOURCES})

add_executable(rtc main.c)
target_link_libraries(rtc rtclib m)

add_executable(projectile projectile.c)
target_link_libraries(projectile rtclib m)