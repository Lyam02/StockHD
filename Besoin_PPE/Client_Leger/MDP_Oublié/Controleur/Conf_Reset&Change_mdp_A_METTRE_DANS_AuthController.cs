// Confirmer reset mdp
//***************************************************************************************//

public IActionResult ConfirmResetPassWord()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> ConfirmResetPassWord (string email, string secretSentence)
{
    var user = await _UserManager.FindByEmailAsync(email);
    if (user != null && user.SecretSentense == secretSentence)
    {
        return RedirectToAction("ChangePassword");
    }
    return RedirectToAction("ConfirmResetPassWord");
}

//***************************************************************************************//

// changer mdp
//***************************************************************************************//

public IActionResult ChangePassword()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> ChangePassword(string email, string password)
{
    var user = await _UserManager.FindByEmailAsync(email);
    if (user != null)
    {
        var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
        var result = await _UserManager.ResetPasswordAsync(user, token, password);
        if (result.Succeeded)
        {
            return RedirectToAction("SignInUser");
        }
    }
    return RedirectToAction("ChangePassword");
}