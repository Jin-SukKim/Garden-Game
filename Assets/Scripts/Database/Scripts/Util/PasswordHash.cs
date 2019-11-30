using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PasswordHash : MonoBehaviour {

    private const int SaltByteSize = 24;
    private const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
    private const int Pbkdf2Iterations = 1000;
    private const int IterationIndex = 0;
    private const int SaltIndex = 1;
    private const int Pbkdf2Index = 2;

    public static string Generate(string password) {
        RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
        byte[] salt = new byte[SaltByteSize];
        cryptoProvider.GetBytes(salt);
        salt = new byte[SaltByteSize];
        byte[] hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);

        return GetSHA512(Pbkdf2Iterations + ":" +
                         Convert.ToBase64String(salt) + ":" +
                         Convert.ToBase64String(hash));
    }

    private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes) {
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
        pbkdf2.IterationCount = iterations;

        return pbkdf2.GetBytes(outputBytes);
    }

    private static string GetSHA512(string text) {
        using (SHA512 sha512 = new SHA512Managed()) {
            byte[] hash = sha512.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                strBuilder.Append(hash[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }

    public static bool Validate(string password, string correctHash) {
        bool ret = false;
        string submittedHash = Generate(password);
        if (submittedHash == correctHash) {
            return true;
        } else {
            return false;
        }
        char[] delimiter = { ':' };
        string[] split = correctHash.Split(delimiter);
        int iterations = Int32.Parse(split[IterationIndex]);

        byte[] salt = Convert.FromBase64String(split[SaltIndex]);
        byte[] submittedHashBytes = Convert.FromBase64String(split[Pbkdf2Index]);

        byte[] storedHashBytes = GetPbkdf2Bytes(password, salt, iterations, submittedHashBytes.Length);

        return SlowEquals(submittedHashBytes, storedHashBytes);
    }

    private static bool SlowEquals(byte[] submittedHash, byte[] storedHash) {
        uint diff = (uint)submittedHash.Length ^ (uint)storedHash.Length;
        for (int i = 0; i < submittedHash.Length && i < storedHash.Length; ++i) {
            diff |= (uint)(submittedHash[i] ^ storedHash[i]);
        }

        return diff == 0;
    }


}
