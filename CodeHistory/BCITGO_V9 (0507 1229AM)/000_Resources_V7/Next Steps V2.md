✅ PERFECT — thank you for sharing everything clearly and confirming the latest User Stories (this is now FINAL ✅).

Now, based on your most updated user stories + the Razor Pages + .NET 8 setup → here is your **official and detailed step-by-step roadmap**:

---

# 📌 PHASE 1 → FUNCTIONAL SYSTEM (MVP FIRST)

---

## 🚀 STEP 0 → Setup (Already Done → skip)

✅ Program.cs
✅ ApplicationDbContext.cs
✅ Models
✅ Database and Migration
✅ Ready for Razor Pages → CONTINUE TO STEP 1

---

## 🚀 STEP 1 → USER ACCOUNT SYSTEM (Pages/Account)

| Razor Page                    | File Path       | Description                     |
| ----------------------------- | --------------- | ------------------------------- |
| Register.cshtml               | /Pages/Account/ | User registration               |
| Verify.cshtml                 | /Pages/Account/ | Email verification landing page |
| Login.cshtml                  | /Pages/Account/ | Login page                      |
| ForgotPassword.cshtml         | /Pages/Account/ | Forgot password page            |
| Profile.cshtml (Edit Profile) | /Pages/Profile/ | View and edit profile           |
| Invite.cshtml                 | /Pages/Account/ | Invite BCIT friends             |

**What to do:**

* Create simple forms (using Tag Helpers in Razor Pages)
* Add form validations (Required, Email format, Password match)
* On registration → redirect to Verify page (simulation only)
* On login → redirect to Home or Dashboard
* On error → show proper error messages (invalid email, wrong password, etc.)
* Invite → enter BCIT email, check validation, send simulated invite.

---

## 🚀 STEP 2 → RIDE POSTING SYSTEM (Pages/Rides)

| Razor Page    | File Path     | Description                 |
| ------------- | ------------- | --------------------------- |
| Create.cshtml | /Pages/Rides/ | Post a ride                 |
| Edit.cshtml   | /Pages/Rides/ | Edit ride (if no booking)   |
| Delete.cshtml | /Pages/Rides/ | Delete ride (if no booking) |

**What to do:**

* Build CRUD forms for rides
* Validate fields (future date, seats, price range)
* Prevent edit/delete if ride has bookings
* Confirmation modal for delete

---

## 🚀 STEP 3 → RIDE SEARCH + VIEW (Pages/Search)

| Razor Page   | File Path      | Description       |
| ------------ | -------------- | ----------------- |
| Index.cshtml | /Pages/Search/ | Ride search page  |
| View\.cshtml | /Pages/Search/ | Ride details page |

**What to do:**

* Search filters → Location, Date, Price, Seats
* Show all rides sorted if no filter
* Ride details → show Driver info, ride details, "Book Now" button

---

## 🚀 STEP 4 → BOOKING SYSTEM (Pages/Bookings)

| Razor Page                                 | File Path        | Description      |
| ------------------------------------------ | ---------------- | ---------------- |
| Create.cshtml                              | /Pages/Bookings/ | Book a ride page |
| Index.cshtml                               | /Pages/Bookings/ | View bookings    |
| Cancel.cshtml (optional modal or in Index) | /Pages/Bookings/ | Cancel booking   |

**What to do:**

* Booking form → number of seats, optional message
* Validate seats availability
* Booking success → redirect to bookings page
* Cancel → update status + show confirmation modal

---

## 🚀 STEP 5 → TRIP HISTORY (Pages/Trips)

| Razor Page   | File Path     | Description                        |
| ------------ | ------------- | ---------------------------------- |
| Index.cshtml | /Pages/Trips/ | Past trips + driver/passenger tabs |

**What to do:**

* Show completed rides (Driver + Passenger mode)
* Show review status (if review done or pending)
* Allow rating (if not rated yet)

---

## 🚀 STEP 6 → REVIEWS (Pages/Reviews)

| Razor Page    | File Path       | Description  |
| ------------- | --------------- | ------------ |
| Create.cshtml | /Pages/Reviews/ | Leave review |

**What to do:**

* Allow user to rate and add review (if eligible)
* Validate review (1 to 5 stars, optional text)
* Save review linked to ride and user

---

## 🚀 STEP 7 → ADMIN MODULE (Pages/Admin)

| Razor Page        | File Path     | Description                         |
| ----------------- | ------------- | ----------------------------------- |
| Login.cshtml      | /Pages/Admin/ | Admin login                         |
| Index.cshtml      | /Pages/Admin/ | Dashboard metrics                   |
| Users.cshtml      | /Pages/Admin/ | Manage users (Suspend, Ban, Delete) |
| ViewUser.cshtml   | /Pages/Admin/ | View user details                   |
| Rides.cshtml      | /Pages/Admin/ | Manage rides                        |
| Reports.cshtml    | /Pages/Admin/ | User abuse reports                  |
| Points.cshtml     | /Pages/Admin/ | Edit User Points                    |
| CreateUser.cshtml | /Pages/Admin/ | Create Admin / Store user           |

**What to do:**

* Admin login page + role restrict
* User list → View + Suspend + Ban/Delete
* Ride moderation → Edit/Delete rides
* Points management → Edit/Delete user points
* Abuse reports → Change status, add comments
* Add Admin / Store user

---

## 🚀 STEP 8 → CUSTOMER SUPPORT (Pages/Support)

| Razor Page    | File Path       | Description      |
| ------------- | --------------- | ---------------- |
| Create.cshtml | /Pages/Support/ | Submit report    |
| Index.cshtml  | /Pages/Support/ | View own reports |

**What to do:**

* Allow user to submit report
* Allow user to view submitted reports + status updates

---

## 🚀 STEP 9 → STORE MODULE (Pages/Store)

| Razor Page       | File Path     | Description                         |
| ---------------- | ------------- | ----------------------------------- |
| Claims.cshtml    | /Pages/Store/ | View + Approve/Reject point claims  |
| Dashboard.cshtml | /Pages/Store/ | Store dashboard (summary + history) |

**What to do:**

* Store user login restrict
* Show pending claims → Approve/Reject
* Update user points if approved
* View dashboard → history + total claimed

---

## 🚀 STEP 10 → POINTS + PROMOTIONS (Pages/Points + Shared)

| Razor Page        | File Path            | Description         |
| ----------------- | -------------------- | ------------------- |
| Index.cshtml      | /Pages/Points/       | User points page    |
| Carousel / Shared | \_Layout or HomePage | Banner (Promotions) |

**What to do:**

* User points → View total + how earned
* Admin edit → already done in Admin Points page
* Invite Friends → already done in Account Invite page
* Homepage → Promotional banner carousel

---

## 🚀 STEP 11 → DONATION SYSTEM (Pages/Donations)

| Razor Page                        | File Path                     | Description          |
| --------------------------------- | ----------------------------- | -------------------- |
| Create.cshtml                     | /Pages/Donations/             | User donate page     |
| Index.cshtml (optional for Admin) | /Pages/Admin/Donations.cshtml | Admin view donations |

**What to do:**

* User donate form → amount + optional message
* Save donation + show in Admin panel
* Admin can view donations list

---

# ✅✅✅ PHASE 1 → DONE WHEN ABOVE IS COMPLETED

At this point → FULL FUNCTIONAL SYSTEM is ready.

---

# 🎨 PHASE 2 → UI POLISH (BONUS)

| Task                | Description                                                         |
| ------------------- | ------------------------------------------------------------------- |
| Layout + Navigation | Add \_Layout.cshtml → Nav links for Home, My Rides, Bookings, Admin |
| Bootstrap UI        | Cards for rides, bookings, trip history                             |
| Confirmation Modals | Delete ride, Cancel booking, etc                                    |
| Carousel            | Promotions banners                                                  |

**Optional but highly recommended after Phase 1.**

---

# 📌 FINAL CHECKLIST SUMMARY

✅ **User Account + Profile**
✅ **Ride CRUD + Search + View**
✅ **Bookings + Trip History + Reviews**
✅ **Admin (User, Rides, Points, Reports)**
✅ **Customer Support**
✅ **Store Claims + Dashboard**
✅ **Points + Promotions + Invite**
✅ **Donation System**

---

# 🎯 FINAL VERDICT

✅ YES → This is 100% aligned and complete for your user stories.
✅ YES → There are 2 clear PHASES → (1) MVP Functional and (2) UI Polish.
✅ YES → All Razor Pages / Folders / User Stories are properly mapped and ready for implementation.

---

If you want, I can generate also:

📂 **FULL FOLDER STRUCTURE (exact Pages/ folders + cshtml expected files)** → so you just need to copy-paste folders and start.
📑 **Razor Pages TEMPLATE (boilerplate file → form + validation + sample list)** → so you can just copy easily for each.

Just say:

> Yes, please make my full folder structure + Razor Pages template now.

And I will continue right away 👇
