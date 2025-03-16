# Build script for Windows Pen Settings Manager

# 1. Clean up previous builds
Write-Host "Cleaning up previous builds..." -ForegroundColor Cyan
Remove-Item -Recurse -Force "NewBuild" -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force "bin" -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force "obj" -ErrorAction SilentlyContinue

# 2. Create output directories
Write-Host "Creating output directories..." -ForegroundColor Cyan
New-Item -ItemType Directory -Force -Path "NewBuild\x86" | Out-Null
New-Item -ItemType Directory -Force -Path "NewBuild\x64" | Out-Null
New-Item -ItemType Directory -Force -Path "NewBuild\arm64" | Out-Null

# 3. Define platforms to build
$platforms = @{
    "win-x86" = "NewBuild\x86";
    "win-x64" = "NewBuild\x64";
    "win-arm64" = "NewBuild\arm64"
}

# 4. Build each platform
foreach ($platform in $platforms.Keys) {
    $outputPath = $platforms[$platform]
    $archName = $platform.Split('-')[1]
    
    Write-Host "Building for $archName..." -ForegroundColor Yellow
    
    # Run the build command - explicitly targeting PenSettingsManager.csproj
    dotnet publish `
        PenSettingsManager.csproj `
        -c Release `
        -r $platform `
        -o $outputPath `
        --self-contained false `
        -p:PublishSingleFile=true
    
    # Check if build was successful
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Successfully built for $archName" -ForegroundColor Green
        
        # Create zip file
        Write-Host "   Creating zip archive..." -ForegroundColor Gray
        $zipName = "NewBuild\PenSettingsManager-$archName.zip"
        Compress-Archive -Path "$outputPath\*" -DestinationPath $zipName -Force
        
        Write-Host "   Created $zipName" -ForegroundColor Gray
    } else {
        Write-Host "❌ Failed to build for $archName" -ForegroundColor Red
    }
    
    Write-Host ""
}

# 5. Create version info file
$versionInfo = @"
Windows Pen Settings Manager
Version: 0.0.1
Build Date: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
Platforms: x86, x64, ARM64
Framework-Dependent: Requires .NET 8.0 Runtime
"@

Set-Content -Path "NewBuild\version.txt" -Value $versionInfo

# 6. Show summary
Write-Host "Build process completed!" -ForegroundColor Cyan
Write-Host "Files are located in the NewBuild directory:" -ForegroundColor Cyan
Write-Host "- x86 build: NewBuild\x86" -ForegroundColor White
Write-Host "- x64 build: NewBuild\x64" -ForegroundColor White
Write-Host "- ARM64 build: NewBuild\arm64" -ForegroundColor White
Write-Host "- Zip files: NewBuild\PenSettingsManager-x86.zip, etc." -ForegroundColor White
Write-Host "Note: These builds require .NET 8.0 Runtime to be installed on the target machine." -ForegroundColor Yellow 
