#!/bin/bash

cd $1
dotnet clean
dotnet restore
dotnet build -p:Configuration=Release
pwd
cp -v ~/.nuget/packages/xamarin.forms/4.3.0.947036/lib/netstandard2.0/*.dll ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/xamarin.forms/4.3.0.947036/lib/netstandard2.0/*.pdb ./bin/Debug/netstandard2.0
cp -v ~/.nuget/packages/newtonsoft.json/12.0.2/lib/netstandard1.3/*.dll ./bin/Debug/netstandard2.0
cd ./bin/Release/netstandard2.0
mono ~/mono-wasm/packager.exe --copy=always --out=./publish $1.dll 
mkdir ./publish/release
mkdir ./publish/release/css
mkdir ./publish/release/js
cp -rvf ../../../../../assets/*.* ./publish/release
cp -rvf ../../../../../assets/css/*.* ./publish/release/css/
cp -rvf ../../../../../assets/js/*.* ./publish/release