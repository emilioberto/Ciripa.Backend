Add-Type -A 'System.IO.Compression.FileSystem';
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath

Write-host "Install Frontend.Electron node_modules" -NoNewline
cd ..\..\..\Ciripa.Electron | Out-Null
npm install | Out-Null
cd ..\..\Ciripa\Ciripa.Backend\Ciripa\Ciripa.Executable | Out-Null
Write-host "Done" -ForegroundColor Green

Write-host "Install Frontend.Angular node_modules" -NoNewline
cd ..\..\..\Ciripa.Frontend | Out-Null
npm install | Out-Null
cd ..\..\Ciripa\Ciripa.Backend\Ciripa\Ciripa.Executable | Out-Null
Write-host "Done" -ForegroundColor Green

Write-host "Compiling                    " -NoNewline
dotnet publish -c Release -o "$dir\.publish" | Write-Output
Write-host "Done" -ForegroundColor Green

Write-host "Removing previous publish... " -NoNewline
Remove-Item "$dir\Publish.zip" -ErrorAction Ignore; 
Write-host "Done" -ForegroundColor Green

Write-host "Creating zip...              " -NoNewline
[IO.Compression.ZipFile]::CreateFromDirectory("$dir\.publish", "$dir\Publish.zip"); 
Write-host "Done" -ForegroundColor Green

Write-host "Removing temporary folders..." -NoNewline
Remove-Item "$dir\.publish" -Recurse -ErrorAction Ignore; 
Write-host "Done" -ForegroundColor Green

Write-host "Publish completed" -ForegroundColor Green