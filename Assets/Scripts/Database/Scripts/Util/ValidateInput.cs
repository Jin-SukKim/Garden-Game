using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateInput : MonoBehaviour {

    public enum Validation {
        usernameEmpty = 0,
        usernameLetterDigits = 1,
        passwordEmpty = 2,
        passwordLessThanFour = 3,
        passwordLetterDigits = 4,
        isValid = 5
    }

    public static Validation checkUsername(string username) {
        string trimmedUsername = username.Trim();

        if (string.IsNullOrEmpty(trimmedUsername)) {
            return Validation.usernameEmpty;
        }

        foreach (char c in trimmedUsername) {
            if (!Char.IsLetterOrDigit(c)) {
                return Validation.usernameLetterDigits;
            }
        }
        return Validation.isValid;
    }


    public static Validation checkPassword(string password) {
        string trimmedUsername = password.Trim();

        if (string.IsNullOrEmpty(trimmedUsername)) {
            return Validation.passwordEmpty;
        }

        if (trimmedUsername.Length < 4) {
            return Validation.passwordLessThanFour;
        }

        foreach (char c in trimmedUsername) {
            if (!Char.IsLetterOrDigit(c)) {
                return Validation.passwordLetterDigits;
            }
        }
        return Validation.isValid;
    }


}
