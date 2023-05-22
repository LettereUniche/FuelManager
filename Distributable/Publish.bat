cls
@echo off

echo "Creating Modcomponent"
7z a -tzip -mx0 FuelManager.modcomponent "auto-mapped" "bundle" "gear-spawns" "localizations" "BuidlInfo.json"
echo "Copying the file to the Publish dir"
xcopy /v /y "FuelManager.modcomponent" "P:\Modding\The Long Dark\MyMods\FuelManager\Release\"