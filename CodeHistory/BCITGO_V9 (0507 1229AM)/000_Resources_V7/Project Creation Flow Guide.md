# **📖 PROJECT FLOW CREATION GUIDE 📖** #

<!-- STEP 1 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 1 - INSTALL PACKAGES </h2>

### **1. Install the Required Packages for ASP.NET MVC Project**
- In **Visual Studio 2022**, go to:
  - **Tools** → **NuGet Package Manager** → **Manage NuGet Packages for Solution...**

❗Make sure that all packages should be the same version or lower version with `Microsoft.EntityFrameworkCore` 

### **2.  Required Packages for Your ASP.NET MVC Project**  

#### **✅ Essential for Database & Entity Framework**  
-  `Microsoft.EntityFrameworkCore` **(v8.0.12)**  (For Entity Framework Core ORM)
-  `Microsoft.EntityFrameworkCore.SqlServer` **(v8.0.12)** (For SQL Server)  
-  `Microsoft.EntityFrameworkCore.Tools` **(v8.0.12)** (For Migrations)  
-  `Microsoft.EntityFrameworkCore.Sqlite` **(v8.0.12)** (For smooth deployment live)  


#### **✅ For ASP.NET Identity (Only if Using Authentication)**  
-  `Microsoft.AspNetCore.Identity.EntityFrameworkCore` **(v8.0.12)** (For User Authentication & Identity Management) 
- `Microsoft.AspNetCore.Identity.UI`**(v8.0.12)** (For Prebuilt Identity UI)



#### **✅ For MVC Views & UI Components**  
-  `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation` **(v8.0.00)** (For Razor View Updates Without Restarting)  
-  `Microsoft.AspNetCore.Mvc.NewtonsoftJson` **(v8.0.00)** (For JSON Serialization Support)  



#### **✅ For Paging & Sorting (Required for Your Project)**  
1.  `X.PagedList.Mvc.Core` **(v10.5.7)** (For Pagination & Sorting in Tables)  

#### **✅ Additional Packages for Debugging & Scaffolding** 
1. `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore`  **(v8.0.00)** (For Better EF Core Error Handling & Debugging)
2. `Microsoft.VisualStudio.Web.CodeGeneration.Design` **(v8.0.00)** (For Scaffolding Controllers, Models, & Views)
_______________________________________

<!-- STEP 2 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 2 - Setup Connection String</h2>

### 1. Modify `appsettings.json` and `appsettings.Development.json` to Add the Connection String ####

At the top of:
```csharp
{
  "Logging": {
```

add the code below:
```csharp
{

  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BCITGODB;User ID=Dannie Password=dannie;Trusted_Connection=True;Encrypt=False"

  },


```

<!-- STEP 3 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 3 - Setup Program.cs </h2>

### This configures the application's services, like the database connection and dependency injection, ensuring proper interaction with components like Entity Framework.####
1. Check my code for reference
2. Check comments `-added` and add those things to your code.
_______________________________________


<!-- STEP 4 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 4 - Setup ApplicationDbContext </h2>

### This manages the connection to the database and provides access to the tables (models) in the database for performing operations like adding, updating, or retrieving data. ####


1.  Create `data` folder and create `ApplicationDbContext` class inside.
2. Setup > check my code for reference
_______________________________________


<!-- STEP 5 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 5 - Create DbInitializer (Data Seeding) </h2>

### DbInitializer is for seeding data (if you need to add initial records or test data to your tables) ####

1. Under `data` folder, create `DbInitializert` class inside.
2. Setup > check my code for reference
_______________________________________


<!-- STEP 6 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 6 - Define the Model Classes </h2>

### This represents the structure of your database tables and allows you to interact with the database using Entity Framework Core for data manipulation. ####

1. Under `models` folder, create class for each table. *(see sample below)*
2. Setup and check my code for reference.

<img src="./images/1_ClassSample.png" alt="Model Classes" width="250"/>

_______________________________________

<!-- STEP 7 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 7 -  Migrate and Update Database </h2>

### Important steps in Entity Framework Core because they allow your application to: ####


### **1. Migrate:**
- **Create and update database schemas** automatically based on changes made to your models.
- **Generate migration files** that contain the instructions for altering the database schema (adding tables, columns, removing tables, etc.), ensuring that your database schema is always in sync with your models.
  
  In simple terms, **migrate** applies any pending changes to your database schema, whether that means creating a new table, adding columns, or altering existing tables.

### **2. Update-Database:**
- **Apply migrations** to the actual database.
- After you generate a migration with `Add-Migration`, you must use `Update-Database` to **apply the changes** from that migration to the database. This ensures that the database reflects the changes you've made in your application code (models).
- **Create or update the actual database** by executing the migration commands on the connected database (like adding new tables, columns, etc.).

### **STEPS TO DO**
1. Tools > NuGetPackageManager > Package Manager Console
2. On Package Manager Console type `Add-Migration InitialCreate`
    -  You should be able to see `Build succeeded`.
3. On Package Manager Console type `Update-Database`.
    -  You should be able to see `Done`.
4. Check if it is successful
    - **Option 1:** on SQL Server Explorer, refresh `SQL Server`. You should be able to see the new DB under `databases`
    - **Option 2:** on SSMS , refresh `local host`. You should be able to see the new DB under `databases`. Query on SSMS `SELECT TOP 5 * FROM Donation;` to pull out the first few rows.
5. Run the code >> `Run HTTPS` to push the data on the database.

### **DEBUG DB**
1. If you changed any code related to DB to the following:
    - Since you already did the `Add-Migration InitialCreate` and `Update-Database`, just use `Ctrl + Shift + B` to rebuild the solution
    - Run the code >> `Run HTTPS` to push the data on the database.
    - Refresh DB and check if successful.

2. Redoing the Database. Run the following:
    - `dotnet clean`
    - `dotnet build`
    - `dotnet ef migrations add InitialCreate`
    - `dotnet ef database update` 
    - run https code again

_______________________________________

<!-- STEP 8 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 8 -  Scaffold CRUD Pages </h2>

1. Right click on the `Controllers` folder, select **Add** → **New Scaffolded Item** → **MVC Controller with views, using Entity Framework**.
    - Model Class - *the model you want to scaffold (e.g., `TripPosting`, `User`, etc.)*
    - DbContextClass - Select the `ApplicationDbContext` that you need.

2. To check if it is successful, check the `Views`, the `Donations` folder should have generated CRUD classes.

This will generate the views and controller methods for CRUD operations.
_________________________________________

<!-- STEP 9 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 9 -  Update Controllers </h2>

#### In this step, I will explain the things that you need to edit on each controller and the relation on the `_Layout.cshtml`

### 1. Home Controller - *Add the following code below:*
*  **Index**
   -   Returns the Index view - this is a key-value pair that stores the page title you want to display.

   ```csharp
       public IActionResult Index()
    {
        ViewData["Title"] = "Home";  // Set the page title here - ADDED
        return View();
    }
    ```
    * Relation to `_Layout.cshtml`
    ```csharp
    <title>@ViewData["Title"] BCIT GO</title>
    ```

* **Privacy**
    - Privacy action method: returns the Privacy view, this is a key-value pair that stores the page title you want to display.
    - This title will be used when the Privacy view is loaded.
    ```csharp
        public IActionResult Privacy()
    {
        ViewData["Title"] = "Privacy";  // Set the page title here - ADDED
        return View();
    }
    ```
    * Relation to `_Layout.cshtml`
    ```csharp
    <title>@ViewData["Title"] BCIT GO</title>
    ```
### 2. All other controllers created for CRUD Scaffolding - *Add the following code below:*
* Add `ViewData["Title"]`  to each action method.
    - **Index:** *Set to "Donations".*
    - **Details:** *Set to "Donation Details".*
    - **Create:** *Set to "Create Donation".*
    - **Edit:** *Set to "Edit Donation".*
    - **Delete:** *Set to "Delete Donation".*   
* This ensures that when any of these actions are executed, the page title will be dynamically set in the _Layout.cshtml file.
* Check my code for reference > find comment `-ADDED`

### ⁉️ EXPLANATION: Controller GET - POST Action 

**GET: Donations/Index**

```csharp
public async Task<IActionResult> Index()
{
    ViewData["Title"] = "Donations";  // Set the page title for Donations index
    return View(await _context.Donation.ToListAsync());
}
```

#### **Explanation:**
- **Purpose:** This action is responsible for displaying the list of donations to the user.
- **ViewData["Title"] = "Donations";**: Sets the page title to "Donations" that will be shown in the browser tab (based on _Layout.cshtml).
- **return View(await _context.Donation.ToListAsync());**: Retrieves all the donations from the database using `Entity Framework` (EF) asynchronously and sends them to the view. It calls `ToListAsync()` to fetch all donations and returns the view that will display the list.

---

**GET: Donations/Details/5**

```csharp
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var donation = await _context.Donation
        .FirstOrDefaultAsync(m => m.DonationID == id);
    if (donation == null)
    {
        return NotFound();
    }

    ViewData["Title"] = "Donation Details";  // Set the page title for Donation details
    return View(donation);
}
```

#### **Explanation:**
- **Purpose:** This action fetches the details of a specific donation (identified by its `id`) and displays them to the user.
- **id == null**: If no ID is passed (e.g., a user tries to visit `/Donations/Details/` without an ID), it returns a "NotFound" result.
- **var donation = await _context.Donation...**: It queries the `Donation` table for the specific record where the `DonationID` matches the provided `id`.
- **ViewData["Title"] = "Donation Details";**: Sets the page title to "Donation Details" which will be displayed in the browser tab.
- **return View(donation);**: Passes the donation object to the `Details` view for rendering.

---

**GET: Donations/Create**

```csharp
public IActionResult Create()
{
    ViewData["Title"] = "Create Donation";  // Set the page title for Create donation page
    return View();
}
```

#### **Explanation:**
- **Purpose:** This action displays the form for creating a new donation.
- **ViewData["Title"] = "Create Donation";**: Sets the page title to "Create Donation" for the page.
- **return View();**: Returns an empty form (since it's a GET request, we just want to show the form). The view will allow the user to input new donation details.

---

**POST: Donations/Create**

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("DonationID,Name,Amount,DateTime,UserID")] Donation donation)
{
    if (ModelState.IsValid)
    {
        _context.Add(donation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(donation);
}
```

#### **Explanation:**
- **Purpose:** This action receives the form data submitted by the user (via POST) for creating a new donation.
- **[HttpPost]**: Indicates that this action handles POST requests (form submissions).
- **[ValidateAntiForgeryToken]**: Helps protect against cross-site request forgery (CSRF) attacks by validating that the request comes from a valid source.
- **if (ModelState.IsValid)**: Checks if the model (data) passed by the user is valid (e.g., all required fields filled in correctly).
- **_context.Add(donation);**: Adds the new `donation` record to the `DbContext` (i.e., prepares to insert it into the database).
- **await _context.SaveChangesAsync();**: Asynchronously saves the new donation to the database.
- **return RedirectToAction(nameof(Index));**: After saving, it redirects the user to the `Index` action, which will show the updated list of donations.
- **return View(donation);**: If the model is not valid (e.g., required fields are missing), it redisplays the form with validation errors.

---

**GET: Donations/Edit/5**

```csharp
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var donation = await _context.Donation.FindAsync(id);
    if (donation == null)
    {
        return NotFound();
    }

    ViewData["Title"] = "Edit Donation";  // Set the page title for Edit donation page
    return View(donation);
}
```

#### **Explanation:**
- **Purpose:** This action retrieves the specific donation record (identified by its `id`) and shows the form to edit it.
- **id == null**: If the `id` is not provided, return "NotFound" (similar to the `Details` action).
- **var donation = await _context.Donation.FindAsync(id);**: Fetches the donation record from the database based on the provided `id`.
- **ViewData["Title"] = "Edit Donation";**: Sets the page title to "Edit Donation".
- **return View(donation);**: Passes the `donation` object to the view for editing.

---

**POST: Donations/Edit/5**

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("DonationID,Name,Amount,DateTime,UserID")] Donation donation)
{
    if (id != donation.DonationID)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(donation);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DonationExists(donation.DonationID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(donation);
}
```

#### **Explanation:**
- **Purpose:** This action processes the submitted data from the donation edit form and updates the existing donation in the database.
- **[HttpPost]**: Handles POST requests when the user submits the form.
- **if (id != donation.DonationID)**: Ensures that the `id` in the URL matches the `DonationID` in the form (helps prevent form tampering).
- **ModelState.IsValid**: Validates the form data.
- **_context.Update(donation);**: Updates the donation record in the database.
- **await _context.SaveChangesAsync();**: Saves the updated record to the database.
- **catch (DbUpdateConcurrencyException)**: Handles concurrency issues (e.g., another user has updated the donation at the same time).
- **return RedirectToAction(nameof(Index));**: After updating, the user is redirected to the `Index` action to see the updated list of donations.
- **return View(donation);**: If the form data is not valid, redisplay the edit form with error messages.

---

**GET: Donations/Delete/5**

```csharp
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var donation = await _context.Donation
        .FirstOrDefaultAsync(m => m.DonationID == id);
    if (donation == null)
    {
        return NotFound();
    }

    ViewData["Title"] = "Delete Donation";  // Set the page title for Delete donation page
    return View(donation);
}
```

#### **Explanation:**
- **Purpose:** This action displays the confirmation page for deleting a specific donation.
- **id == null**: If no `id` is passed, return "NotFound".
- **var donation = await _context.Donation...**: Retrieves the donation from the database using the provided `id`.
- **ViewData["Title"] = "Delete Donation";**: Sets the page title to "Delete Donation".
- **return View(donation);**: Passes the donation object to the view to display the delete confirmation.

---

**POST: Donations/Delete/5**

```csharp
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var donation = await _context.Donation.FindAsync(id);
    if (donation != null)
    {
        _context.Donation.Remove(donation);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
```

#### **Explanation:**
- **Purpose:** This action performs the actual deletion of the donation.
- **[HttpPost, ActionName("Delete")]**: Specifies that this is a POST action for the "Delete" view (i.e., confirms the deletion).
- **_context.Donation.Remove(donation);**: Removes the specified donation from the `DbContext`.
- **await _context.SaveChangesAsync();**: Saves the changes to the database (i.e., deletes the donation).
- **return RedirectToAction(nameof(Index));**: Redirects to the `Index` action to display the updated donation list after deletion.

_________________________________________

<!-- STEP 10 -->
<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 10 -  Update _Layout.cshtml </h2>

*The purpose of updating the `_Layout.cshtml` page is to define the common structure, design, and reusable elements (such as headers, footers, and navigation links) that are shared across multiple views in the application, ensuring consistency throughout the site.*

**1. Review your layout page under `views` > `shared` > `_Layout.cshtml`**

  * Check my code, all modified code have comment `-ADDED-`

**2. Ensure that navigation links are correctly set up to link to the appropriate pages.**
  * This should include adding updated links to your top navigation bar and setting the title in the layout for each page.

### ⁉️ VERIFY: _Layout.cshtml
* Run Application `Ctrl + F5` or press `run: https`
* Check navigation links
* Inspect page titles

_________________________________________
<!-- STEP 11 -->

<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 11 -  Update Views </h2>

#### Redirect to `Views` Folder > Folder of a specific page *(ex. Donations)

### 1. Index.cshtml - *Add the following code below:*
  * Check my code as well as reference. Changes have `-ADDED` comment.
  * Apply the code changes below to all `Views`.

Change:
```csharp
@{
    ViewData["Title"] = "Index"; 
}

<h1>Index</h1> 
```

To:
```csharp
@{
    ViewData["Title"] = "Donations"; <!--Change the title of the page - ADDED-->
}

<h1>Donations</h1> <!--Change the title of the page - ADDED-->
```
### ⁉️ VERIFY: Views
* Run Application `Ctrl + F5` or press `run: https`
* Check pages
* Verify the title (check browser tab)
* Navigate through the views 
  * Go through the create, delete, details, and edit pages. Make sure the headers and the title tag match what you specified in the `ViewData["Title"]` and `<h1> tags`.

_________________________________________

<!-- STEP 12 -->

<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 12 -  Update Field Names & Query Strings </h2>

#### Redirect to `Views` Folder > `Folder of a specific page` > `index.cshtml` *(ex. Donations/index.cshtml)*

#### Check my code for reference, modifications have `-ADDED` comment.

### *Check the following code below:*


### 1. **Update the Title**:
This should already be in place, but you can ensure that the title is clear and descriptive for the **Donations Index** page.

```html
@model IEnumerable<BCITGO_FINAL.Models.Donation>

@{
    ViewData["Title"] = "Donations"; <!-- Updated Title for this page -->
}

<h1>Donations</h1> <!-- This is the page's heading -->
```

### 2. **Use Display Name for Table Headers**:
Instead of using `model => model.Name`, `model => model.Amount`, etc., make sure these values are descriptive and user-friendly. You've already done this by using `@Html.DisplayNameFor`.

For instance:
```html
<thead>
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.Name) <!-- Will be shown as "Donor Name" if Display attribute is added -->
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Amount) <!-- Will show as "Donation Amount" -->
        </th>
        <th>
            @Html.DisplayNameFor(model => model.DateTime) <!-- Will show as "Donation Date" -->
        </th>
        <th>
            @Html.DisplayNameFor(model => model.UserID) <!-- Will show as "User ID" -->
        </th>
        <th></th>
    </tr>
</thead>
```

### 3. **Update URLs in Action Links**:
This will ensure that the query strings in the URLs are clean. The `asp-route-id` automatically generates readable URLs.

In the action links (`Edit`, `Details`, `Delete`), make sure you use the proper route to display human-readable URLs for each action, as follows:
```html
<td>
    <a asp-action="Edit" asp-route-id="@item.DonationID">Edit</a> |
    <a asp-action="Details" asp-route-id="@item.DonationID">Details</a> |
    <a asp-action="Delete" asp-route-id="@item.DonationID">Delete</a>
</td>
```

These links will automatically generate routes like `/Donations/Details/1` or `/Donations/Edit/1`, which are more user-friendly.

### 4. **Optional Field Renaming with Display Attributes**:
You can update the **model class** to make field names more human-readable. This involves adding a `Display` attribute to your model fields. Here's how you can update the `Donation` model:

In your **Donation.cs** class, add **Display** attributes:
```csharp
public class Donation
{
    [Key]
    public int DonationID { get; set; }

    [Display(Name = "Donor Name")] // Added Display Name
    public string Name { get; set; }

    [Display(Name = "Donation Amount")] // Added Display Name
    public decimal Amount { get; set; }

    [Display(Name = "Donation Date")] // Added Display Name
    public DateTime DateTime { get; set; }

    [Display(Name = "User ID")] // Added Display Name
    public int UserID { get; set; }
}
```

This will reflect **"Donor Name"**, **"Donation Amount"**, **"Donation Date"**, and **"User ID"** in the view when you use `@Html.DisplayNameFor`.

---

### Summary of what to change:
1. Update the title for the **`Index.cshtml`** page to be more descriptive (already done).
2. Use `@Html.DisplayNameFor` for human-readable column headers.
3. Ensure action links have clean URLs.
4. Optionally, modify the **Donation model** class by adding the `Display` attribute to make the column headers more readable.


_______________________________________________________________

<!-- STEP 13 -->

<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 13 -  Text Filter & SelectList Dropdown </h2>

To implement **Text Filters** and a **SelectList Dropdown** in your ASP.NET MVC project, you can follow these steps:

### **1. Text Filter** *(for searching/filtering based on a text field)*

**You will allow the user to search/filter the list of items (e.g., donations, vehicles, etc.) by entering a text in a search box.**


In your `DonationsController`, update the `Index` or `// GET: Donation` method to accept a search parameter and filter the donations based on the provided text. See code below

```csharp
        // GET: Donations
        public async Task<IActionResult>  Index(string searchString) //ADDED THIS > Index(string searchString) - new
        {
            ViewData["Title"] = "Donations";  // Set the page title for Donations index - ADDED

            //ADDED CODE BELOW - new
            var donations = from d in _context.Donation
                            select d;

            // If search string is provided, filter donations by Name. - ADDED
            if (!string.IsNullOrEmpty(searchString))
            {
                donations = donations.Where(d => d.Name.Contains(searchString)); // Filter by donor name
            }

            ViewData["SearchString"] = searchString;  // Pass searchString to the View


            return View(await _context.Donation.ToListAsync());
        }
```

**Step 2: Update the `Index.cshtml` on `Views` to add a search box.**

In the `Donations Index` view (`Index.cshtml`), add a form where the user can type a search string.

```csharp
<!-- Add Search Box ADDED-->
<p>
    <form method="get">
        <input type="text" name="searchString" placeholder="Search by Donor Name"
               value="@ViewData["SearchString"]" /> <!-- Use ViewData["SearchString"] here -->
        <input type="submit" value="Search" />
    </form>

</p>
```


#### Explanation:
- A **SelectList** is populated with values for **Donation Amounts** (you can populate it dynamically from your database if needed).
- The user can choose a donation amount from the dropdown, and the donations will be filtered based on the selected value.
- The search box (`Text Filter`) will filter donations based on the donor's name, and the dropdown (`SelectList`) will filter based on the selected donation amount.



### **Next Steps**:
1. **Text Filter** and **SelectList Dropdown** functionality should now work for your **Donations** view. You can implement similar filters and dropdowns for other entities such as **Vehicles**, **Users**, etc.
2. **Run your project** and navigate to the donations page. Test the search and dropdown filters to make sure they filter the results as expected.

This will allow users to filter results based on both text input and dropdown selection, improving the UX for your users.

### ⁉️ VERIFY: Text Filter & SelectList Dropdown
* Run Application `Ctrl + F5` or press `run: https`
* Navigate to donation index pages
* Test the search functionality
_____________________________________
<!-- STEP 14 -->

<h2 style="color:red; font-weight: bold; border-bottom: 3px solid #333; padding-bottom: 5px;"> STEP 14 -  Joining Related Tables in Details View </h2>

Ensure that the **Details** page of your entities displays related data. For example, when viewing a `TripPosting`, you should display the driver’s name and vehicle details from the related `Driver` and `Vehicle` tables.

Below is the order of code modification:
#### **1. Modify Models**
  - The models define the data structure and relationships, so they need to be set up before you can fetch and display the related data in the controller and views.

    #### How to Modify Models?
    a. Look at relationships: For instance, `Driver.cs` has a foreign key `UserID`. You need to check if you have the appropriate navigation property `(public User User { get; set; })` in your Driver.cs to fully load related data. 

    add the code below on the last line before "}"

    ```csharp
    public int UserID { get; set; }  // Last Key

    // Navigation property to access the related User
    public User User { get; set; } // Add this line to include the related User

    ```
    b. Add data validation annotations for properties that require validation
    ```csharp
    [Required] //ADD
    [StringLength(100, ErrorMessage = "License plate must be between 5 and 100 characters.", MinimumLength = 5)] //ADD
    public string DrivingLicense { get; set; }

    c. Display Names for User-Friendliness

    ```csharp
    [Display(Name = "User ID")] //ADD
    public int UserID { get; set; }

    [Display(Name = "Driving License")] //ADD
    public string DrivingLicense { get; set; }

    ``` 

    c. Add a constructor to initialize properties with default values. Add on last part
    ```csharp
    public Driver()
    {
        DrivingLicense = string.Empty;  // Initialize DrivingLicense with an empty string
    }
    ```

#### **2. Modify Controllers**
  - The controller handles fetching the data and passing it to the view, so it's essential to modify the controller after the models are set up.
#### **3. Modify Details under Views pages**
  - The view is the last step because it just receives the data from the controller and displays it. Modifying the view is dependent on having the models and controller in place to properly handle and pass the related data.

#### **4. On Package COnsole `dotnet ef migrations add UpdateRelatedData`>**
#### **5. On Package COnsole `dotnet ef database update`>**


____
1. **Modify the Details View for Donations**:
   - Go to `Views/Donations/Details.cshtml`.
   - Add the related **User** data that is associated with the **Donation** in the view.

2. **Add Code to Display Related User Data**:
   In `Details.cshtml`, you need to display properties of the related **User** model (such as **Name**, **Phone Number**, **Description**, etc.) alongside the **Donation** details.

   Example code to display **User Name**:
   ```html
   <dt class="col-sm-2">
       @Html.DisplayNameFor(model => model.User.Name)
   </dt>
   <dd class="col-sm-10">
       @Html.DisplayFor(model => model.User.Name)
   </dd>
   ```

3. **Ensure the Controller Loads Related Data**:
   In your **DonationsController**, ensure that you're including the related **User** data when querying for the **Donation**. You should already have this:

   ```csharp
   var donation = await _context.Donation
       .Include(d => d.User) // This includes the related User data
       .FirstOrDefaultAsync(m => m.DonationID == id);
   ```

#### **What to Check**:
- Ensure that the `Details.cshtml` page is correctly displaying related **User** data.
- Verify that you are using `@Html.DisplayFor(model => model.User.Name)` or similar for the related **User** properties.
- Run the app and test the **Donation Details** page to see if the **User** data is displayed correctly.

This step ensures that when you view a **Donation**'s details, you also see the **User's** information (such as name, email, etc.) associated with that donation.


