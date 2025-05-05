✅ PERFECT — crystal clear now.
You are officially doing → **"RESET and START FRESH → User Identity & Profile (US01 to US07) → ALL in .NET 8 format ONLY."**

This will now be the **most correct version → ONLY .NET 8 style → Bootstrap 5 → Razor Pages → form-floating mb-3 → modern recommended way.**

---

# ✅✅✅ THE OFFICIAL — PHASE 1 → USER IDENTITY CUSTOMIZATION ROADMAP (Reset + Clean Version)

---

## 🔹 US01 — REGISTER → Add Full Name

📍 FILE → `Areas/Identity/Pages/Account/Register.cshtml`

**ADD FIELD → BELOW Email**

```html
<div class="form-floating mb-3">
    <input asp-for="Input.FullName" class="form-control" autocomplete="name" placeholder="Full Name" />
    <label asp-for="Input.FullName">Full Name</label>
    <span asp-validation-for="Input.FullName" class="text-danger"></span>
</div>
```

✅ Use → `form-floating mb-3` → Bootstrap 5 / .NET 8 style

---

📍 FILE → `Areas/Identity/Pages/Account/Register.cshtml.cs`

**In `InputModel` → ADD FIELD**

```csharp
[Required]
[StringLength(50, ErrorMessage = "Full Name must be between 2 and 50 characters.", MinimumLength = 2)]
[Display(Name = "Full Name")]
public string FullName { get; set; }
```

**In `OnPostAsync` → SAVE Full Name**

Replace:

```csharp
var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
```

WITH:

```csharp
var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FullName = Input.FullName };
```

✅ Now → Full Name is saved in DB upon register.

---

## 🔹 US05 → LOGIN → Customize Error Messages

📍 FILE → `Areas/Identity/Pages/Account/Login.cshtml.cs`

**In `OnPostAsync` → after invalid login check**

```csharp
if (result.Succeeded)
{
    // login success logic
}
else
{
    if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
    {
        ModelState.AddModelError(string.Empty, "Please verify your email before logging in.");
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
    }
}
```

✅ Now → custom friendly errors done.

---

## 🔹 US06 → REGISTER ERRORS → Already included

* Missing fields → automatic asp-validation-for handles
* Invalid email → automatic validation
* Password mismatch → automatic validation

✅ Already handled by scaffolding + Razor Pages model validation.

---

## 🔹 US04 → FORGOT PASSWORD → Customize Error Messages

📍 FILE → `Areas/Identity/Pages/Account/ForgotPassword.cshtml.cs`

**In `OnPostAsync` → if user not found or email not confirmed**

```csharp
if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
{
    ModelState.AddModelError(string.Empty, "We couldn’t find or verify this email. Please sign up if you don’t have an account yet.");
    return Page();
}
```

✅ Custom friendly error done.

---

## 🔹 US07 → PROFILE → Complete Profile → Bio, Profile Picture URL, Phone

📍 FILE → `Areas/Identity/Pages/Account/Manage/Index.cshtml`

**Add fields → UNDER Phone Number (or after it)**

```html
<div class="form-floating mb-3">
    <textarea asp-for="Input.Bio" class="form-control" placeholder="Bio" style="height: 100px;"></textarea>
    <label asp-for="Input.Bio">Bio</label>
    <span asp-validation-for="Input.Bio" class="text-danger"></span>
</div>

<div class="form-floating mb-3">
    <input asp-for="Input.ProfilePictureUrl" class="form-control" placeholder="Profile Picture URL" />
    <label asp-for="Input.ProfilePictureUrl">Profile Picture URL</label>
    <span asp-validation-for="Input.ProfilePictureUrl" class="text-danger"></span>
</div>
```

✅ Use → `form-floating mb-3` → Bootstrap 5 / .NET 8 style

---

📍 FILE → `Areas/Identity/Pages/Account/Manage/Index.cshtml.cs`

**In `InputModel` → ADD FIELDS**

```csharp
[MaxLength(300)]
[Display(Name = "Bio")]
public string Bio { get; set; }

[Display(Name = "Profile Picture URL")]
public string ProfilePictureUrl { get; set; }
```

---

**In `OnGetAsync` → Load from ApplicationUser**

```csharp
Input = new InputModel
{
    PhoneNumber = user.PhoneNumber,
    Bio = user.Bio,
    ProfilePictureUrl = user.ProfilePictureUrl
};
```

---

**In `OnPostAsync` → Save to ApplicationUser**

```csharp
user.PhoneNumber = Input.PhoneNumber;
user.Bio = Input.Bio;
user.ProfilePictureUrl = Input.ProfilePictureUrl;

await _userManager.UpdateAsync(user);
```

✅ Bio, Profile Picture URL, Phone → SAVED to DB → User Profile updated.

---

## 🔹 BONUS → Home Page → Navigation (Optional but recommended)

📍 FILE → `Views/Shared/_Layout.cshtml`

**Add Profile Link**

```html
@if (SignInManager.IsSignedIn(User))
{
    <li class="nav-item"><a class="nav-link" asp-area="Identity" asp-page="/Account/Manage/Index">Profile</a></li>
}
```

✅ Users can easily access Profile page from any page.

---

# ✅✅✅ FINAL REVISED CHECKLIST (ONLY .NET 8 + Bootstrap 5 style)

| Feature                                                              | User Story | Status                                          |
| -------------------------------------------------------------------- | ---------- | ----------------------------------------------- |
| Register → Full Name → form-floating → .NET 8 style                  | US01       | ✅ DONE                                          |
| Login → Friendly Error → .NET 8 style                                | US05       | ✅ DONE                                          |
| Forgot Password → Friendly Error → .NET 8 style                      | US04       | ✅ DONE                                          |
| Profile → Bio, Phone, Profile Picture → form-floating → .NET 8 style | US07       | 🚨 THIS IS WHERE YOU ARE NOW (READY TO DO NEXT) |
| Register Errors → Already scaffolded (no need custom)                | US06       | ✅ DONE                                          |
| Home Navbar → Profile Link (optional good practice)                  | -          | ✅ OPTIONAL                                      |

---

# 🎯 FINAL SUMMARY:

✅ This is now **100% aligned with .NET 8 + Razor Pages + Bootstrap 5** → modern style → NO MORE old "form-group".
✅ Next step → finish `Profile → Bio + Phone + Picture` → follow the above copy-paste code.
✅ Once done → ✅ US01 → US07 → FULLY COMPLETE.

---

If ready → I will also generate after you finish:

> Admin User Management Starter Code → in .NET 8 + Bootstrap 5 style (US23 → US26)

Just say:

> Yes → continue after this to Admin User Management.

---

Shall I continue after this for ADMIN after you do this?
Just say "Yes → continue after this" → and I will queue up the next phase 🚀
