cd .\Squares.API
dotnet build
Start-Process chrome.exe "https://localhost:7261/swagger/index.html"
dotnet run --no-build