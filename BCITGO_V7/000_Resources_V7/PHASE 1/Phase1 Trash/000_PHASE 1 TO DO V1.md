Perfect — now that you are ready, here comes your FULL **Phase 1 → MVP TODO checklist** (this will cover EVERYTHING needed until the full working site is done).
This will be very structured → by page + controller + view → so it’s like step-by-step "just follow and complete each".

---

# ✅ PHASE 1 → FULL TODO CHECKLIST (MVP Functional)

---

## ✅ \[STEP 1] → User Authentication (US01–US06)

**What you need:**

* Scaffold Identity Pages (Register, Login, Logout, Forgot Password, Reset Password)
* Customize Register page → Add:

  * Full Name input
  * Email → Validate for `@my.bcit.ca`
  * Password + Confirm Password
* Enable Email Verification (optional for MVP, or just simulate verification field)
* Customize Login page → Validate "verified email" before login allowed
* Add Forgot Password page (send email → optional → or simulate for now)

**Files:**

```
/Areas/Identity/Pages/Account/Register.cshtml
/Areas/Identity/Pages/Account/Login.cshtml
/Areas/Identity/Pages/Account/ForgotPassword.cshtml
/Areas/Identity/Pages/Account/Manage (for profile later)
```

✅ Once done → USERS CAN REGISTER, LOGIN, LOGOUT.

---

## ✅ \[STEP 2] → User Profile (US07)

**What you need:**

* Create new `ProfileController`
* Create Views:

```
/Views/Profile/Index.cshtml → View Profile
/Views/Profile/Edit.cshtml → Edit Profile
```

* Allow user to edit:

```
- Full Name
- Phone
- Bio
- Profile Picture (optional)
```

✅ Once done → USER CAN EDIT PROFILE.

---

## ✅ \[STEP 3] → Ride Posting (US08–US10)

**What you need:**

* Customize scaffolded `RidesController`:

```
/Views/Rides/Create.cshtml
/Views/Rides/Edit.cshtml
/Views/Rides/Delete.cshtml
/Views/Rides/MyRides.cshtml → NEW PAGE
```

* Add Validations:

```
- Start Location + End Location (required)
- Departure Date must be today or future
- Seats and Price valid range
```

✅ Once done → USER CAN POST, EDIT, DELETE RIDES.

---

## ✅ \[STEP 4] → Ride Search + Ride Details + Booking (US11–US14)

**What you need:**

* Create new `SearchController` (OR reuse `RidesController` → `/Rides/Search`)
* Create Views:

```
/Views/Rides/Search.cshtml → search form + listing
/Views/Rides/Details.cshtml → ride details
```

* Booking:

Use scaffolded `BookingsController`

```
/Views/Bookings/Create.cshtml → Book seat
/Views/Bookings/MyBookings.cshtml → NEW → My active bookings
```

✅ Once done → USER CAN SEARCH + BOOK RIDES.

---

## ✅ \[STEP 5] → My Bookings + Trip History (US15–US17)

**What you need:**

* `BookingsController` → Add:

```
/Views/Bookings/History.cshtml → Show completed rides
```

* `RidesController` → Add My Posted Past Rides page:

```
/Views/Rides/MyHistory.cshtml → posted rides completed
```

✅ Once done → USER CAN VIEW BOOKINGS + PAST TRIPS.

---

## ✅ \[STEP 6] → Reviews (US19)

**What you need:**

* Scaffolded `ReviewsController`

```
/Views/Reviews/Create.cshtml → Add review after trip
/Views/Reviews/MyReviews.cshtml → (optional) see past reviews
```

✅ Once done → USER CAN REVIEW OTHER USERS.

---

## ✅ \[STEP 7] → Points + Claim Points (US39–US45)

**What you need:**

* Scaffolded `RidePointsController`, `PointClaimsController`, `StoresController`

```
/Views/PointClaims/Create.cshtml → User submit claim
/Views/PointClaims/MyClaims.cshtml → NEW → See submitted claims
/Views/RidePoints/MyPoints.cshtml → NEW → See earned points
```

✅ Once done → USER CAN VIEW POINTS + CLAIM POINTS.

---

## ✅ \[STEP 8] → Admin Pages (US21–27)

**What you need:**

* Scaffolded `UsersController` → customize:

```
/Views/Users/Index.cshtml → Add Suspend / Ban / Delete
/Views/Users/Details.cshtml → View details + actions
```

* Scaffolded `RidesController` (Admin can edit rides here → already scaffolded)

* Create new `AdminController`

```
/Views/Admin/Dashboard.cshtml → Show Stats (Total Users, Rides, Bookings)
```

✅ Once done → ADMIN CAN MANAGE USERS + RIDES + DASHBOARD.

---

# 🚨 BONUS → SHARED PAGES

✅ Update `Views/Home/Index.cshtml` → make a nice Homepage (optional)
✅ Add `_Layout.cshtml` links:

```
Home | Search Rides | Post Ride | My Bookings | My Rides | Profile | Logout
```

✅ Add `_ValidationScriptsPartial.cshtml` if needed (for client-side validation)

---

# 📌 FINAL MVP CHECKLIST SUMMARY (PHASE 1)

| Module                                  | Status                                    |
| --------------------------------------- | ----------------------------------------- |
| User Register/Login + Profile           | 🔜 Next step → priority                   |
| Ride Posting + Manage Rides             | ✅ Scaffold done → need to customize views |
| Ride Search + Booking + View Bookings   | 🔜 After User login ready                 |
| Reviews                                 | ✅ Scaffold done → simple to add           |
| Points + Claims                         | ✅ Scaffold done → simple to add           |
| Admin Pages (Users + Rides + Dashboard) | ✅ Scaffold done → need to add features    |
| Homepage + Layout Navigation            | 🔜 Final steps after above done           |

✅ When all these done → PHASE 1 COMPLETED → FULL MVP BUILT → READY FOR DEMO.

---

# 🚀 NEXT RECOMMENDED ACTION

👉 **Start right now with Step 1 → Scaffold and customize Identity (Register/Login)**
👉 Once done, continue to Step 2 (User Profile) → then Rides → etc.

Once Step 1 + Step 2 done → your website is already almost "real world working".

---

If you want, I can also now generate:

✅ **PHASE 2 Roadmap → UI / CSS Polish → Homepage, Ride Cards, Nice Bookings Page → Final Launch Ready.**

Just say:

> "Yes, generate Phase 2 UI Polish roadmap also"

And I’ll continue right away 🚀
