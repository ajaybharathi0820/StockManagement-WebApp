param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("DropDatabase", "Rollback", "RemoveLast")]
    [string]$Action,

    [Parameter(Mandatory = $true)]
    [string]$ModuleName,

    [string]$TargetMigration
)

# Paths
$solutionRoot = (Get-Item -Path ".." -Verbose).FullName
$moduleProject = "$solutionRoot\StockManagement-WebApp\Source\src\$ModuleName\$ModuleName.Infrastructure"

Write-Host "=== Migration Management for module '$ModuleName' ===" -ForegroundColor Cyan
Set-Location $moduleProject

switch ($Action) {
    "DropDatabase" {
        Write-Host "Dropping database for $ModuleName..." -ForegroundColor Yellow
        dotnet ef database drop -f
    }
    "Rollback" {
        if (-not $TargetMigration) {
            Write-Host "You must specify -TargetMigration for rollback" -ForegroundColor Red
            exit 1
        }
        Write-Host "Rolling back to migration '$TargetMigration'..." -ForegroundColor Yellow
        dotnet ef database update $TargetMigration
    }
    "RemoveLast" {
        Write-Host "Removing last migration..." -ForegroundColor Yellow
        dotnet ef migrations remove
    }
}

Set-Location $solutionRoot
Write-Host "=== Done ===" -ForegroundColor Green
