@echo off
"C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" RailwayIDCardMaker.sln /t:Rebuild /p:Configuration=Debug /p:Platform=x86 /v:minimal
