 
 /* A rajouter dans le model Sign Up si enlevé, ce qui ne devrait pas arriver
 Dans le cas contraire, il faudra supprimer la BDD et la recréer pour ajouter la nouvelle
 colonne dans la table User */
[PersonalData]
[Display(Name = "Phrase secrète")]
public string SecretSentense { get; set; }

  
 // Pour la page de confirmation de reset mdp (avant le reste mdp)
 public class ConfirmResetPassword
 {
     [Required]
     public string Email { get; set; }
     [Required]
     public string SecretSentence { get; set; }
 }


 // Pour la page de reset mdp 
 public class ResetPassword
 {
     [Required]
     public string Email { get; set; }
     [Required]
     [DataType(DataType.Password)]
     [Display(Name = "Nouveau Mot de passe")]
     public string Password { get; set; }
     [Required]
     [DataType(DataType.Password)]
     [Display(Name = "Confirmation Nouveau Mot de passe")]
     [Compare("Password", ErrorMessage = "Les deux mots de passe ne correspondent pas !")]
     public string ConfirmPassword { get; set; }
 }