param(
    [string]$ModuleName = "Polisher",
    [string]$MigrationName = "InitialCreate"
)

# Paths
$ProjectPath = "Source/src/Modules/$ModuleName/$ModuleName.Infrastructure"
$StartupPath = "Source/src/StockManagement.API"
$MigrationsPath = "Persistence/Migrations"

Write-Host "=== Adding migration '$MigrationName' for module '$ModuleName' ==="

# Add migration
dotnet ef migrations add $MigrationName `
    --project $ProjectPath `
    --startup-project $StartupPath `
    --output-dir $MigrationsPath

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Migration failed."
    exit $LASTEXITCODE
}

Write-Host "✅ Migration added successfully."

# Update database
Write-Host "=== Updating database for module '$ModuleName' ==="
dotnet ef database update `
    --project $ProjectPath `
    --startup-project $StartupPath

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Database update failed."
    exit $LASTEXITCODE
}

Write-Host "✅ Database updated successfully."
