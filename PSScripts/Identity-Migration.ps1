param(
    [string]$ModuleName = "Polisher",
    [string]$MigrationName = "InitialCreate",
    [string]$DbContext = ""  # Optional DbContext name
)

# Paths
$ProjectPath = "Source/src/$ModuleName/$ModuleName.Infrastructure"
$StartupPath = "Source/src/StockManagement.API"
$MigrationsPath = "Persistence/Migrations"

Write-Host "=== Adding migration '$MigrationName' for module '$ModuleName' ==="

# Add migration command
$addMigrationCmd = @(
    "dotnet ef migrations add $MigrationName",
    "--project $ProjectPath",
    "--startup-project $StartupPath",
    "--output-dir $MigrationsPath"
)

if ($DbContext -ne "") {
    $addMigrationCmd += "--context $DbContext"
}

Invoke-Expression ($addMigrationCmd -join " ")

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Migration failed."
    exit $LASTEXITCODE
}

Write-Host "✅ Migration added successfully."

# Update database command
Write-Host "=== Updating database for module '$ModuleName' ==="

$updateDbCmd = @(
    "dotnet ef database update",
    "--project $ProjectPath",
    "--startup-project $StartupPath"
)

if ($DbContext -ne "") {
    $updateDbCmd += "--context $DbContext"
}

Invoke-Expression ($updateDbCmd -join " ")

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Database update failed."
    exit $LASTEXITCODE
}

Write-Host "✅ Database updated successfully."
