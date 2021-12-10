$sourceFolder = "C:\_Code\AdventOfCode\AdventOfCode2021\Day09\Pictures\Part2_Testdata_HTML\*" # the source folder containing the files to convert
$destFolder = "C:\_Code\AdventOfCode\AdventOfCode2021\Day09\Pictures\Part2_Testdata_Pictures\" # converted JPGs will be saved here. Folder has to exist.
 
$files = Get-ChildItem -Path $sourceFolder
foreach ($file in $files)
{  
    $outfile = $file.Name -replace '.html','.jpg' # replace fileneding
    
    $outfile = Join-Path $destFolder $outfile

    $command = '& "C:\Program Files\wkhtmltopdf\bin\wkhtmltoimage.exe"' + '--crop-x 0 --crop-w 100 --crop-h 100 "' + $file + '" "' + $outfile +'"'
    Invoke-Expression $command
}

#--crop-h <int>                  Set height for cropping
#--crop-w <int>                  Set width for cropping
#--crop-x <int>                  Set x coordinate for cropping
#--crop-y <int>                  Set y coordinate for cropping