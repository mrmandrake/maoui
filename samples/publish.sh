#!/bin/sh
rm -rvf ../teka/wwwroot
echo moving $1 in publish wwwroot
mv ./$1/bin/Debug/netstandard2.0/publish ../teka/wwwroot
cd ../Teka
echo running Teka $1
dotnet run $1 

