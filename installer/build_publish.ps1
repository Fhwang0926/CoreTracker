# set-executionpolicy unrestricted
Write-Output "build nsi file start"
Write-Output "arch : x86_x64"
$version = ""
foreach($line in (git describe --tags $(git rev-list --tags --max-count=1))) { if($line -ne "") { $version = $line } }
if($version -eq "") { Write-Output "no have"; break; }
Write-Output $version

Set-Location ..

# start new tag
$option = $args[0]
if ($option -eq "-h") {
  Write-Output "-t"
  Write-Output "-r [msg]"
  Write-Output "-m [msg]"
  Write-Output "-b [msg]"
  Write-Output "-h Print Help"
  exit
}
$msg = $args[1]
$msbuild = ""
# r:msg / release version update  vx.0.0
# m:msg / major version update    v0.x.0
# b:msg / major version update    v0.0.x
# Release Number, Major Number, Minor Number
# role = http://seorenn.blogspot.com/2012/02/version.html

if (!([string]::IsNullOrEmpty($option)))
{
  if (!($option -Match "-")) {
    # msbuild path if no have option char '-'
    $msbuild = $args[0]
    Write-Output "msbuild location set : $msbuild"
    
  } else {

    if ($msg -eq "") { Write-Output "no have version tag message" exit }

    $version_array = $version.Split(".")
    if ($option -eq "-r") {
      # release new update application
      $version_array[0] = 'v' + [string](([int]($version_array[0].Replace('v', ''))) + 1)
      $version_array[1] = 0
      $version_array[2] = 0
      $msg  = "fix:add or upgrade function or change some logic: $msg"
    } elseif ($option -eq "-m") {
      # add or upgrade function or change some logic
      $version_array[1] = [string](([int]$version_array[1]) + 1)
      $version_array[2] = 0
      $msg  = "fix:bug: $msg"
    } elseif ($option -eq "-b") {
      # bug fix
      $version_array[2] = ([int]$version_array[2]) + 1
      $msg  = "fix: $msg"
    } elseif ($option -eq "-t") {
      # bug fix
      $version_array[2] = ([int]$version_array[2]) + 1
      $msg  = "it's just test"
    }
    $version = [string]::Join(".", $version_array)
    Write-Output "new version : $version"

  }
}
# end new tag

Write-Output "override version start : $version"
((Get-Content -path ./Form1.cs -Raw) -replace '(VERSION = )"v[0-9]{0,}.[0-9]{0,}.[0-9]{0,}"', ('$1'+'"'+$version+'"')) | Set-Content -Path ./Form1.cs
Write-Output "override version set done"

git add .
git commit -m ".$msg"
git push
git tag $version
git push origin $version
Write-Output "git upload done"  