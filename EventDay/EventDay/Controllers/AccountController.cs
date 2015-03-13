using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EventDay.Models;

namespace EventDay.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Podana nazwa użytkownika lub hasło są niepoprawne.");
                }
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Próba zarejestrowania użytkownika
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    //dodaj profil uzytkownika
                    CreateUserProfile(model.UserName, model.Email, DateTime.Now);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            return View(model);
        }

        public ActionResult Delete(string username)
        {
            System.Web.Security.Membership.DeleteUser(username);
            return RedirectToAction("Index","Home");
        }

        //!!!Uwaga brak obsługi błędów!!! Miejsce newralgiczne
        private EventContext db = new EventContext();
        public void CreateUserProfile(string UserName, string Email, DateTime CreateTime)
        {
            var newUser = new UserProfile();
            newUser.UserName = UserName;
            newUser.CreateTime = CreateTime;
            newUser.UpdateTime = CreateTime;
            newUser.Birthday = CreateTime;

            newUser.Email = Email;
            newUser.StatusId = "Active";
            newUser.AccountTypeId = "Private";

            //if (ModelState.IsValid)
            //{
            db.UserProfile.Add(newUser);
            db.SaveChanges();
            //}
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // Metoda ChangePassword powoduje wyjątek
                // zamiast zwrócić wartość false w pewnych scenariuszach błędu.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Bieżące hasło jest niepoprawne lub nowe hasło jest nieprawidłowe.");
                }
            }

            // Dotarcie do tego miejsca wskazuje, że wystąpił błąd, wyświetl ponownie formularz
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Na stronie http://go.microsoft.com/fwlink/?LinkID=177550 znajduje się
            // pełna lista kodów stanu.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Nazwa użytkownika już istnieje. Wprowadź inną nazwę użytkownika.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Nazwa użytkownika dla tego adresu e-mail już istnieje. Wprowadź inny adres e-mail.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Podane hasło jest nieprawidłowe. Wprowadź prawidłową wartość hasła.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Podany adres e-mail jest nieprawidłowy. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Podana odpowiedź dla funkcji odzyskiwania hasła jest nieprawidłowa. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Podane pytanie dla funkcji odzyskiwania hasła jest nieprawidłowe. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Podana nazwa użytkownika jest nieprawidłowa. Sprawdź wartość i spróbuj ponownie.";

                case MembershipCreateStatus.ProviderError:
                    return "Dostawca uwierzytelniania zwrócił błąd. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";

                case MembershipCreateStatus.UserRejected:
                    return "Żądanie utworzenia użytkownika zostało anulowane. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";

                default:
                    return "Wystąpił nieznany błąd. Sprawdź wpis i spróbuj ponownie. Jeśli problem nie zniknie, skontaktuj się z administratorem systemu.";
            }
        }
        #endregion
    }
}
