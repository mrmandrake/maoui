#!/bin/bash

cd $1
#rm -rvf ./bin/Debug/netstandard2.0
dotnet clean
dotnet restore
dotnet build -p:Configuration=Debug
pwd
cp -v ~/.nuget/packages/xamarin.forms/4.3.0.947036/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/xamarin.forms/4.3.0.947036/lib/netstandard2.0/*.pdb ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/newtonsoft.json/12.0.2/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/webassembly.bindings/3.0.2/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/webassembly.net.http/3.0.2/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/webassembly.net.websockets/3.0.2/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cd ./bin/Debug/netstandard2.0
mono ~/mono-wasm/packager.exe --copy=always --out=./publish -debug $1.dll 
mkdir ./publish/
mkdir ./publish/css
mkdir ./publish/js
cp -rvf ../../../../../assets/*.* ./publish/
cp -rvf ../../../../../assets/css/*.* ./publish/css/
cp -rvf ../../../../../Maoui/client.js ./publish/maoui.js
cp -rvf ../../../../../assets/js/*.* ./publish/