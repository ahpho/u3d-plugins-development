#export ANDROID_NDK=/Users/xsj/Tools/android-ndk-r10e           #旧: 原始代码版本使用r10e，太旧。
#export ANDROID_NDK=/home/kuang/AndroidSdk/android-ndk-r14b     #新: 依赖系统变量，故注释此行。我使用此版。

mkdir -p build_v7a && cd build_v7a
#cmake -DANDROID_ABI=armeabi-v7a -DCMAKE_TOOLCHAIN_FILE=../cmake/android.toolchain.cmake -DANDROID_TOOLCHAIN_NAME=arm-linux-androideabi-clang3.6 -DANDROID_NATIVE_API_LEVEL=android-9 ../
cmake -DANDROID_ABI=armeabi-v7a -DCMAKE_TOOLCHAIN_FILE=../cmake/android.toolchain.cmake -DANDROID_NATIVE_API_LEVEL=android-21 ../
cd ..
cmake --build build_v7a --config Release
mkdir -p ../../../bin/native/Android/libs/armeabi-v7a/
cp build_v7a/libtstunity.so ../../../bin/native/Android/libs/armeabi-v7a/
cp build_v7a/libtstunity.so ../../../example/Assets/Plugins/Native/C/Android/libs/armeabi-v7a/

mkdir -p build_v8a && cd build_v8a
cmake -DANDROID_ABI=arm64-v8a -DCMAKE_TOOLCHAIN_FILE=../cmake/android.toolchain.cmake -DANDROID_NATIVE_API_LEVEL=android-21 ../
cd ..
cmake --build build_v8a --config Release
mkdir -p ../../../bin/native/Android/libs/arm64-v8a/
cp build_v8a/libtstunity.so ../../../bin/native/Android/libs/arm64-v8a/
cp build_v8a/libtstunity.so ../../../example/Assets/Plugins/Native/C/Android/libs/arm64-v8a/

mkdir -p build_x86 && cd build_x86
cmake -DANDROID_ABI=x86 -DCMAKE_TOOLCHAIN_FILE=../cmake/android.toolchain.cmake -DANDROID_NATIVE_API_LEVEL=android-21 ../
cd ..
cmake --build build_x86 --config Release
mkdir -p ../../../bin/native/Android/libs/x86/
cp build_x86/libtstunity.so ../../../bin/native/Android/libs/x86/
cp build_x86/libtstunity.so ../../../example/Assets/Plugins/Native/C/Android/libs/x86/

mkdir -p build_x86_64 && cd build_x86_64
cmake -DANDROID_ABI=x86_64 -DCMAKE_TOOLCHAIN_FILE=../cmake/android.toolchain.cmake -DANDROID_NATIVE_API_LEVEL=android-21 ../
cd ..
cmake --build build_x86_64 --config Release
mkdir -p ../../../bin/native/Android/libs/x86_64/
cp build_x86_64/libtstunity.so ../../../bin/native/Android/libs/x86_64/
cp build_x86_64/libtstunity.so ../../../example/Assets/Plugins/Native/C/Android/libs/x86_64/

#rm -rf ./build_v7a
#rm -rf ./build_v8a
#rm -rf ./build_x86
#rm -rf ./build_x86_64
