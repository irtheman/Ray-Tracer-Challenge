cmake_minimum_required(VERSION 3.18.4)
project(rtc)

include_directories(include)
add_subdirectory(src)
add_subdirectory(tests)

# Disable in-source builds to prevent source tree corruption.
if(" ${CMAKE_SOURCE_DIR}" STREQUAL " ${CMAKE_BINARY_DIR}")
  message(FATAL_ERROR "FATAL: In-source builds are not allowed. You should create a separate directory for build files.")
endif()