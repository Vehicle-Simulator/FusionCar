#!/bin/sh

server_build_path=${HOME}/ProjectBlack/Builds/"$SERVER_BUILD_MODE"
"$UNITY_PATH" -batchmode -nographics -disable-assembly-updater -logFile - -projectPath "$PROJECT_PATH" -buildTarget "$BUILD_TARGET" -buildPath "$BUILD_PATH" -buildNumber "$BUILD_NUMBER" -buildDefines "$BUILD_DEFINES" -buildMode "$BUILD_MODE" -executeMethod "$BUILD_METHOD" -buildOptions "$BUILD_OPTIONS" -serverEnv "$BUILD_SERVER_ENV"
