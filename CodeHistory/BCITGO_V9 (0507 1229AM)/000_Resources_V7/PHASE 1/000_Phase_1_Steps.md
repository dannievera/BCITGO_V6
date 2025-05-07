
---

# 🚀 STEP 1: Scaffold Identity (Register/Login/Forgot Password/Reset)

## ✅ How to scaffold Identity Pages:

In Visual Studio:

1. Right-click your project → `Add` → `New Scaffolded Item`
2. Choose **Identity** from the list
3. Click **Add**
4. In the Identity scaffold dialog:

   * Choose your `ApplicationDbContext` (it should detect it)
   * Select the following pages:

✅ **Register**
✅ **Login**
✅ **Logout**
✅ **ForgotPassword**
✅ **ResetPassword**
✅ (optional for now: Manage > Index) → for profile later

5. Click Add

✅ DONE → this will generate:

```
/Areas/Identity/Pages/Account/Register.cshtml
/Areas/Identity/Pages/Account/Login.cshtml
/Areas/Identity/Pages/Account/ForgotPassword.cshtml
/Areas/Identity/Pages/Account/ResetPassword.cshtml
/Areas/Identity/Pages/Account/Logout.cshtml
```

---

# 📌 \[IMPORTANT] Customization for Register.cshtml + Register.cshtml.cs

### ✅ Add FullName to Register.cshtml

```html
<div class="form-group">
    <label asp-for="Input.FullName"></label>
    <input asp-for="Input.FullName" class="form-control" />
    <span asp-validation-for="Input.FullName" class="text-danger"></span>
</div>
```

### ✅ Modify Register.cshtml.cs → Add `FullName` property

```csharp
[Required]
[Display(Name = "Full Name")]
public string FullName { get; set; }
```

Add FullName when creating the User in `OnPostAsync`

```csharp
var user = new User
{
    UserName = Input.Email,
    Email = Input.Email,
    FullName = Input.FullName, // ADD THIS LINE
    EmailVerified = false,     // Optional: email verification later
    AccountStatus = "Active",  // Optional
};
```

---

## ✅ Add Validation for BCIT email only (@my.bcit.ca)

In `OnPostAsync` → before creating user → add:

```csharp
if (!Input.Email.EndsWith("@my.bcit.ca"))
{
    ModelState.AddModelError(string.Empty, "Email must be @my.bcit.ca address.");
    return Page();
}
```

✅ This will prevent registering if not @my.bcit.ca

---

## ✅ OPTIONAL → Enable Email Confirmation (For MVP → SKIP or set as auto verified)

If you do NOT want to send email for now:

In Register → after user is created → do this:

```csharp
user.EmailVerified = true;
await _userManager.UpdateAsync(user);
```

This will skip real email → but simulate verification.

Later (Phase 2) → you can add SendGrid or SMTP for real email.

---

## ✅ Customize Login.cshtml.cs → Validate EmailVerified

In `OnPostAsync` of Login.cshtml.cs → before sign in:

```csharp
if (user != null && !user.EmailVerified)
{
    ModelState.AddModelError(string.Empty, "Please verify your email before logging in.");
    return Page();
}
```

✅ This will block login if not verified.

---

## ✅ Customize Logout.cshtml → no need, automatic.

---

## ✅ Forgot Password / Reset Password → Optional

For MVP:

* You can leave them as default (they are simple pages)
* Later → can connect to real email

---

# ✅ ✅ ✅ After you finish these → REGISTER + LOGIN + LOGOUT + BASIC USER SECURITY → DONE

✅ User can register → with BCIT email only
✅ User must provide Full Name
✅ User is marked verified (auto for now)
✅ User can login
✅ User can logout
✅ Forgot Password → ready (optional)

---

# 🚀 FINAL STEP: TEST

Once coded:

1. Run site
2. Go to `/Identity/Account/Register`
3. Try to register a user → verify @my.bcit.ca
4. Try login → verify error if email not verified
5. Try logout → should work

Once working → **STEP 1 is COMPLETED → USER FLOW WORKING**

---

# ✅ NEXT AFTER THIS:

When done → you are ready for STEP 2:

👉 **Profile Page (View + Edit)** → Will link User model (Full Name, Phone, Bio, Photo)

---

If you want → I can prepare right now:

✅ Ready-made FILES and FULL CODE for:

**\[ProfileController.cs + Views/Profile/Index + Views/Profile/Edit]** → so after Register/Login → next is Profile complete.

Just say:

> "Yes, continue to STEP 2 → Profile"

And I will continue IMMEDIATELY with the full ready code 🚀
