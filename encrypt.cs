using System;
using System.IO;
using System.Security.Cryptography;

internal class Program
{
    public static void Main(string[] args) {
        // INTENT: To ensure the operator has only one argument which should be a file.
        bool incorrectUsage = args.Length != 1;
        if (incorrectUsage)
        {
            Console.WriteLine("Fuck outta here!");
        }
        else 
        { 
            // INTENT: To accept a file as an argument and change its extension.
            FileInfo file = new FileInfo(args[0]);
            string sourceFilename = file.Name;
            string destinationFilename = Path.ChangeExtension(sourceFilename, ".damnmayne");

            /* INTENT: To build the seed; the integer that makes the secret key and the
            initialization vector reproducable. */
            int num = 1234;
            Random seed = new Random(Convert.ToInt32(num));

            // INTENT: To build the initialization vector.
            byte[] iv = new byte[16];
            seed.NextBytes(iv);

            // INTENT: To build the secret key.
            byte[] key = new byte[32];
            seed.NextBytes(key);

            // INTENT: TO build the encrypted file.
            Program.EncryptFile(sourceFilename, destinationFilename, key, iv);

        }
    }

	// Intent: To build an encryption stream. 
    private static byte[] EncryptFile(string sourceFile, string destinationFile, byte[] key, byte[] iv) {
        using (RijndaelManaged rijndaelManaged = new RijndaelManaged()) {
			//Intent: To set up the final encrypted file.
            using (FileStream fileStream = new FileStream(destinationFile, FileMode.Create)) {
				
				//Intent: To activate the encryption.
                using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(key, iv)) {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Write)) {
						
						// Intent: To stream in the original file.
                        using (FileStream fileStream2 = new FileStream(sourceFile, FileMode.Open))
                        {
                            byte[] array = new byte[1024];
                            int x;
                            while ((x = fileStream2.Read(array, 0, array.Length)) != 0) {
                                cryptoStream.Write(array, 0, x);
                            }
                        }
                    }
                }
            }
        }
        return null;
    }
}