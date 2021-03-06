# Ray-Tracer-Challenge
My attempt at the Ray Tracer Challenge. I will be starting with C#. I want to follow up with versions of python, C and C++. More may follow over time.  

# Images Created With C#
### Chapter 2
![Chapter 2 Projectile](Images/cs_projectile.png "Projectile")

### Chapter 4
![Chapter 4 Clock](Images/cs_clock.png "Clock")

### Chapter 5
![Chapter 5 Circle](Images/cs_circle.png "Circle")
![Chapter 5 Circle Mod 1](Images/cs_circle_shrinky.png "Circle Shrink Y")
![Chapter 5 Circle Mod 2](Images/cs_circle_shrinkx.png "Circle Shrink X")
![Chapter 5 Circle Mod 3](Images/cs_circle_shrinkrotate.png "Circle Shrink & Rotate")
![Chapter 5 Circle Mod 4](Images/cs_circle_shrinkskew.png "Circle Shrink & Skew")

### Chapter 6
![Chapter 6 Circle](Images/cs_sphere.png "Sphere")
![Chapter 6 Circle Mod 1](Images/cs_sphere_shrinky.png "Sphere Shrink Y")
![Chapter 6 Circle Mod 2](Images/cs_sphere_shrinkx.png "Sphere Shrink X")
![Chapter 6 Circle Mod 3](Images/cs_sphere_shrinkrotate.png "Sphere Shrink & Rotate")
![Chapter 6 Circle Mod 4](Images/cs_sphere_shrinkskew.png "Sphere Shrink & Skew")

### Chapter 7
![Chapter 7 Scene](Images/cs_scene.png "Scene")

### Chapter 8
![Chapter 8 Scene Shadows](Images/cs_scene_shadows.png "Scene With Shadows")
![Chapter 8 Scene Shadows](Images/cs_scene_shadow_puppet.png "Shadow Puppet")

### Chapter 9
![Chapter 9 Plane](Images/cs_plane.png "Plane")
![Chapter 9 Plane With Ceiling](Images/cs_plane_ceiling.png "Plane With Ceiling")
![Chapter 9 Plane Hex Room](Images/cs_plane_hexroom.png "Plane Hex Room")

### Chapter 10
![Chapter 10 Pattern First](Images/cs_pattern_first.png "First Pattern")
![Chapter 10 Pattern](Images/cs_pattern.png "Pattern")
![Chapter 10 Pattern Blended](Images/cs_pattern_blended.png "Pattern Blended")
![Chapter 10 Pattern Nested](Images/cs_pattern_nested.png "Pattern Nested")
![Chapter 10 Pattern Nested 2](Images/cs_pattern_nested2.png "Pattern Nested 2")
![Chapter 10 Pattern Perlin](Images/cs_pattern_perlin.png "Pattern Perlin Noise")

### Chapter 11
![Chapter 11 Reflection](Images/cs_rtr_reflect.png "Reflection")
![Chapter 11 Transparent](Images/cs_rtr_transparent.png "Transparent")
![Chapter 11 Fresnel](Images/cs_rtr_fresnel.png "Fresnel")
![Chapter 11 Under Water](Images/cs_rtr_underwater.png "Under Water")

### Chapter 12
![Chapter 12 Table](Images/cs_cube_table.png "Table")

### Chapter 13
![Chapter 13 Cylinders](Images/cs_cylinders.png "Cylinders")
![Chapter 13 Cylinders](Images/cs_cones.png "Cone")

### Chapter 14
![Chapter 14 Groups](Images/cs_groups_hexagon.png "Hexagon")

### Chapter 15
![Chapter 15 Triangles](Images/cs_triangles.png "Smooth Triangles")

### Chapter 16
![Chapter 16 CSG](Images/cs_csg.png "CSG Difference")

### Bonus Chapter: Bounding Boxes and Hierarchies
![Bonus Chapter Bounding Boxes](Images/cs_triangles.png "Huge Rabbit OBJ File")

### Bonus Chapter: Rendering Soft Shadows
![Bonus Chapter Soft Shadows](Images/cs_arealight_softshadow.png "Area Lighting With 2 Lights")
![Bonus Chapter Soft Shadows](Images/cs_arealight.png "Area Lighting Book Sample")

### Bonus Chapter: Texture Mapping
![Bonus Chapter Texture Mapping](Images/cs_uvmapping.png "Checker Pattern Mapping")
![Bonus Chapter Texture Mapping](Images/cs_cubemapping.png "Cube Mapping")
![Bonus Chapter Texture Mapping](Images/cs_earth.png "UV Image Mapping")
![Bonus Chapter Texture Mapping](Images/cs_skybox.png "Skybox Image Mapping")

All of the following is for Ubuntu 20.04.

# .NET Core
## Installing .NET Core
Get the Microsoft package signing key and add the package repository:
```
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb
```  

Then install the SDK (not the runtime):
```
sudo apt-get update
sudo apt-get install -y apt-transporter-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-3.1
```

Snap can also be used:
```
sudo snap install dotnet-sdk --classic --channel=3.1
sudo snap alias dotnet-sdk.dotnet dotnet
```
## MSTest Unit Testing of .NET Core
Create the solution:
```
dotnet new sln -o <solution name>
```
Create the starting class:
```
dotnet new classlib -o <project name>
ren Class1.cs <class name>.cs
```
Add the new project to the solution:
```
dotnet sln add ./<project folder>/<project name>.csproj
```
Now create the unit test, add it to the solution and reference the project being tested in the unit test project:
```
dotnet new mstest -o <project name>.Tests
dotnet sln add ./<project name>.Tests/<project name>.Tests.csproj
dotnet add ./<project name>.Tests/<project name>.Tests.csproj reference ./<project folder>/<project name>.csproj
```
# C/C++
## Installing gcc / g++
For programming in C and C++ the effort to install both compilers is simply the same:
```
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install -y build-essential autoconf automake
gcc --version
g++ --version
make --version
```
## Unit Testing C/C++ Using Google Test
### Installing CMake
When using a Desktop version of Ubuntu one can use snap:
```
sudo snap install cmake --classic
cmake --version
```
There is also the command line option:
```
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install -y cmake
which cmake
cmake --version
```

### Installing Google Test
Installing Google Test is simple:
```
sudo apt-get install -y googletest
```
Google Test requires a libgtest library to be used. The gtest library is simply source code and must be compiled using cmake:
```
sudo apt-get install -y libgtest-dev
cd /usr/src/gtest
sudo cmake CMakeLists.txt
sudo make
cd lib
sudo cp *.a /usr/lib
```

### Unit Testing C++ With Google Test

### Unit Testing C With Google Test
Name mangling in C++ can make it impossible to use your C code. The C headers used in the Google Test cpp code need to be wrapped in extern "C" { } to stop the name mangling.

This description assumes all source code goes in src, all headers go in include, and all test classes go in test.

Here are some example header files:
```
// include/return_one.h
#ifndef RETURN_ONE_H
#define RETURN_ONE_H

// A C library :)

// A C function that always return 1.
extern int return_one(void);

#endif
```
Here is the C code:
```
// src/return_one.c
#include "return_one.h"

int return_one(void)
{
    return 1;
}
```
The test looks like this:
```
// test/return_one.cpp
#include "gtest/gtest.h"

extern "C" {
#include "return_one.h"
}

TEST(t_return_one, returns_1)
{
    EXPECT_EQ(1,return_one());  
}
```
A main.cpp file is also required:
```
#include "gtest/gtest.h"

int main(int argc, char **argv)
{
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}
```
First, let's add a rarely changing template that downloads gtest if needed. The file is CMakeList<span>.txt</span>.in:
```
project(googletest-download NONE)

include(ExternalProject)
ExternalProject_Add(googletest
  GIT_REPOSITORY    https://github.com/google/googletest.git
  GIT_TAG           release-1.10.0
  SOURCE_DIR        "${CMAKE_BINARY_DIR}/googletest-src"
  BINARY_DIR        "${CMAKE_BINARY_DIR}/googletest-build"
  CONFIGURE_COMMAND ""
  BUILD_COMMAND     ""
  INSTALL_COMMAND   ""
  TEST_COMMAND      ""
)
```
A CMakeLists.txt file is required:
```
cmake_minimum_required(VERSION 3.18)

project(<project name>)

##
### Test definitions ###
##

configure_file(CMakeLists.txt.in
        googletest-download/CMakeLists.txt)
execute_process(COMMAND ${CMAKE_COMMAND} -G "${CMAKE_GENERATOR}" .
        WORKING_DIRECTORY ${CMAKE_BINARY_DIR}/googletest-download )
execute_process(COMMAND ${CMAKE_COMMAND} --build .
        WORKING_DIRECTORY ${CMAKE_BINARY_DIR}/googletest-download )

add_subdirectory(${CMAKE_BINARY_DIR}/googletest-src
        ${CMAKE_BINARY_DIR}/googletest-build)

enable_testing()
add_subdirectory(test)

##
### Source definitions ###
##

include_directories("${PROJECT_SOURCE_DIR}/include")

file(GLOB sources
  "${PROJECT_SOURCE_DIR}/include/*.h"
  "${PROJECT_SOURCE_DIR}/src/*.c")

add_executable(<project name> ${sources})
```
Finally, we need a test CMakeLists.txt. This expects a test file named after the src file with "_tests" appended:
```
include_directories("${PROJECT_SOURCE_DIR}/include")

file(GLOB sources "${PROJECT_SOURCE_DIR}/src/*.c")
list(REMOVE_ITEM sources "${PROJECT_SOURCE_DIR}/src/main.c")

file(GLOB tests "${PROJECT_SOURCE_DIR}/test/*.cpp")
list(REMOVE_ITEM tests "${PROJECT_SOURCE_DIR}/test/main.cpp")

foreach(file ${tests})
  set(name)
  get_filename_component(name ${file} NAME_WE)
  add_executable("${name}_tests"
    ${sources}
    ${file}
    "${PROJECT_SOURCE_DIR}/test/main.cpp")
  target_link_libraries("${name}_tests" gtest_main)
  add_test(NAME ${name} COMMAND "${name}_tests")
endforeach()
```
With all of this in place simply do the following:
```
mkdir build
cd build
cmake ..
make
./<project name>
echo $?
```
See this [example site](https://notes.eatonphil.com/unit-testing-c-code-with-gtest.html) or this [pdf](http://fac-staff.seattleu.edu/zhuy/web/teaching/winter13/cpsc152/Lab4.pdf).

# Python
## Installing Python
Python likely needs to be installed but to be sure simply run:
```
python3 --version
python3.9 --version
```
If python doesn't run and show a version number then let's install:
```
sudo apt update
sudo apt-get install -y build-essential checkinstall
sudo apt-get install -y libreadline-gplv2-dev libncursesw5-dev libssl-dev libsqlite3-dev tk-dev libgdbm-dev libc6-dev libbz2-dev
sudo apt-get install -y software-properties-common
sudo add-apt-repository ppa:deadsnakes/ppa
sudo apt-get update
sudo apt-get install -y python3.9
```

## Installing PyTest
To install PyTest we need Pip installed first:
```
sudo apt-get install python3-pip
pip3 --version
pip3 install --user pipenv
pip3 install -U pytest
pytest --version
```
## Using PyTest
PyTest can get much more complicated and making use of fixtures is recommended. PyTest always looks for file names that start with "test_*.py" or "*_test.py". These are the test code to be evaluated. Inside these test files are test methods that start with "test".

The recommended structure is to put your source code in the src/package folder and put your tests in a tests folder. The test folder needs to have an "\_\_init\_\_.py" file so the test files don't get added with the source code; they are their own package. Each test file is named after the source file being tested.

To use PyTest simply import pytest and the python class that is going to be tested:
```
# test_class.py
import pytest
from class import TestClass

def test_add():
    tc = TestClass()
    assert tc.Add(1, 2) == 3
```
Simply running pytest, there are multiple ways, in the same folder will return the results of all the tests that were evaluated.
```
pytest --pyargs <package>
python3 -m pytest --pyargs <package>
```

# Java
## Installing Java
Installing Java in Ubuntu is pretty simple:
First, download Oracle JDK 15 (jdk-15.0.1_linux-x64_bin.tar.gz for example)
```
sudo mkdir -p /usr/java/oracle
sudo mkdir -p /usr/java/lib
cd /usr/java/oracle
sudo cp /data/setups/jdk-15.0.1_linux-x64_bin.tar.gz jdk-15.0.1_linux-x64_bin.tar.gz
sudo tar -xzvf jdk-15.0.1_linux-x64_bin.tar.gz
```

Next, we have to set up the environment variables:
```
sudo nano /etc/profile
```
and add the following to the profile:
```
JAVA_HOME=/usr/java/oracle/jdk-15.0.1
CLASSPATH=/usr/java/lib
PATH=$PATH:$HOME/bin:$JAVA_HOME/bin
export JAVA_HOME
export CLASSPATH
export PATH
```

When the changes are complete, run the following to reload the profile. A reboot works as well...
```
. /etc/profile
```

## Installing Maven
Maven seems to be popular so the decision was made to try it.
```
sudo apt update
sudo apt install maven
mvn -version
```

## Installing JUnit 5
Download the latest NUnit jar file. It was decided to use junit-platform-console-standalone because Visual Studio Code is often used.
https://search.maven.org/artifact/org.junit.platform/junit-platform-console-standalone/1.7.0/jar
The jar file was copied to /usr/java/lib.
Visual Studio Code's Setting.json had to be configured with the following:
```
"java.home": "/usr/java/oracle/jdk-15.0.1",
"java.project.referencedLibraries": [
    "lib/**/*.jar",
    "/usr/java/lib/junit-platform-console-standalone-1.7.0.jar"
]
```

The mavin pom.xml needs the following changes as well:
```
<dependencies>
    <dependency>
        <groupId>org.junit.jupiter</groupId>
        <artifactId>junit-jupiter-api</artifactId>
        <version>5.7.0</version>
        <scope>test</scope>
    </dependency>
    <dependency>
        <groupId>org.junit.jupiter</groupId>
        <artifactId>junit-jupiter-engine</artifactId>
        <version>5.7.0</version>
        <scope>test</scope>
    </dependency>
</dependencies>

... and ...

        <plugin>
          <artifactId>maven-surefire-plugin</artifactId>
          <version>2.22.2</version>
        </plugin>
        <plugin>
            <artifactId>maven-failsafe-plugin</artifactId>
            <version>2.22.2</version>
        </plugin>        
```

Simply run commands like:
```
mvn test
mvn clean compile test
```