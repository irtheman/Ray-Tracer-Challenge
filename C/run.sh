#/bin/sh

cd build
cmake ..
make
echo
echo Main Application...
src/rtc
src/projectile
gimp projectile.ppm
echo
echo Testings...
tests/rtcTests