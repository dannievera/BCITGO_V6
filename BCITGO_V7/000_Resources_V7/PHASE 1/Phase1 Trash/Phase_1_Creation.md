<!-- PHASE 2 -->
<h1 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> PHASE 2 STEP BY STEP</h1>

<!-- STEP 1-->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 1 -  Create Identity Pages </h2>

1. Right click on the project folder, on our case it is >> `BCITGO_V6` folder, select 

In Visual Studio:

1. Right-click your project, on our case it is >> `BCITGO_V6` (right click that then...) → `Add` → `New Scaffolded Item`

2. Choose **Identity** from the list
3. Click **Add**
4. In the Identity scaffold dialog:

   * Under `DbContext class` Choose your `ApplicationDbContext` (it should detect it)
   * Select the following pages:
        - ✅ **Register** (Account\Register)
        - ✅ **Login** (Account\Login)
        - ✅ **Logout** (Account\Logout)
        - ✅ **ForgotPassword** (Account\ForgotPassword)
        - ✅ **ResetPassword** (Account\ResetPassword)
        - ✅ **Manage Index** (optional for now:Account\Manage\Index → for user profile page later, this is optional now)

5. Click **Add**

✅ DONE → this will generate:

```
/Areas/Identity/Pages/Account/Register.cshtml
/Areas/Identity/Pages/Account/Login.cshtml
/Areas/Identity/Pages/Account/ForgotPassword.cshtml
/Areas/Identity/Pages/Account/ResetPassword.cshtml
/Areas/Identity/Pages/Account/Logout.cshtml
```
_________________________________________

<!-- STEP 2-->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 2 -  Customize Identity Based on Needs </h2>

