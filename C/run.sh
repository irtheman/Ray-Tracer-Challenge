#/bin/sh

cd build
cmake ..
make
echo
echo Main Application...
src/rtc
echo
echo Testings...
tests/rtcTests