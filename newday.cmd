git checkout main
git fetch --all
git rebase origin/main
git checkout -b %1
dotnet new console -n %1
dotnet sln add %1/%1.csproj
cd tests
dotnet new xunit -n %1tests
cd ..
dotnet sln add tests/%1tests/%1tests.csproj
cd tests/%1tests
dotnet add reference ../../%1/%1.csproj
dotnet add package Shouldly
dotnet restore
cd ../..
git add .
git commit -m "Add %1"
